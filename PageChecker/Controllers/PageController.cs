using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PageCheckerAPI.DTOs.Page;
using PageCheckerAPI.DTOs.Shared;
using PageCheckerAPI.DTOs.WebsiteText;
using PageCheckerAPI.Helpers;
using PageCheckerAPI.Models;
using PageCheckerAPI.Repositories.Interfaces;
using PageCheckerAPI.Services.EmailService;
using PageCheckerAPI.Services.PageBackgroundService;
using PageCheckerAPI.Services.PageService;
using PageCheckerAPI.Services.WebsiteService;
using PageCheckerAPI.Services.WebsiteTextService;
using PageCheckerAPI.ViewModels.Page;

namespace PageCheckerAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class PageController : Controller
    {
        private readonly IPageService _pageService;
        private readonly IMapper _mapper;
        private readonly IPageBackgroundService _pageBackService;
        private readonly IWebsiteService _websiteService;
        private readonly IWebsiteTextService _websiteTextService;

        public PageController(
            IPageService pageService,
            IPageBackgroundService pageBackService,
            IWebsiteService websiteService,
            IWebsiteTextService websiteTextService,
            IMapper mapper)
        {
            _pageService = pageService;
            _pageBackService = pageBackService;
            _mapper = mapper;
            _websiteService = websiteService;
            _websiteTextService = websiteTextService;
        }

        private Guid GetUserId()
        {
            var userIdClaim = User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            return Guid.Parse(userIdClaim.Value);
        }

        // GET api/page
        [HttpGet]
        public async Task<IActionResult> GetPages()
        {           
            List<PageDto> pageDtos = await _pageService.GetPages(GetUserId());
            List<PageViewModel> pageViewModels = _mapper.Map<List<PageDto>, List<PageViewModel>>(pageDtos);

            return Ok(pageViewModels);
        }

        [HttpGet("Details")]
        public async Task<IActionResult> GetPage(Guid pageId)
        {
            PageDto page = await _pageService.GetPage(pageId);
            if (page == null)
            {
                return BadRequest();
            }

            PageViewModel pageViewModel = _mapper.Map<PageViewModel>(page);

            return Ok(pageViewModel);
        }

        //[HttpGet("Difference")]
        //public async Task<IActionResult> GetDifference(Guid websiteTextId)
        //{
        //    var text = await _websiteTextService.GetText(websiteTextId);
        //    if (text == null)
        //    {
        //        return BadRequest();
        //    }

        //    return Ok(text);
        //}

        [HttpGet("StartChecking")]
        public async Task<IActionResult> StartChecking(Guid pageId)
        {
            var pageDto = await _pageService.GetPage(pageId);
            if (!pageDto.UserId.Equals(GetUserId()))
                return BadRequest();

            _pageBackService.StartPageChangeChecking(pageDto);
            pageDto.Stopped = false;
            await _pageService.EditPage(pageDto);

            return Ok(pageDto);
        }

        [HttpDelete("StopChecking")]
        public async Task<IActionResult> StopChecking(Guid pageId)
        {
            var pageDto = await _pageService.GetPage(pageId);
            if (!pageDto.UserId.Equals(GetUserId()))
                return BadRequest();

            _pageBackService.StopPageChangeChecking(pageDto.PageId.ToString());
            pageDto.Stopped = true;
            await _pageService.EditPage(pageDto);

            return Ok(pageDto);
        }

        // POST api/page
        [HttpPost]
        public async Task<IActionResult> AddPage([FromBody]AddPageDto addPageDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                addPageDto.UserId = GetUserId();
                var text = await _websiteService.GetHtml(addPageDto.Url);

                if (addPageDto.CheckingType == CheckingTypeEnum.Element)
                {
                    text = HtmlHelper.GetNode(text, addPageDto.ElementXPath);
                }

                WebsiteTextDto primaryText = await _websiteTextService.AddText(new AddWebsiteTextDto()
                {
                    Text = text
                });

                addPageDto.PrimaryTextId = primaryText.WebsiteTextId;

                var addResult = await _pageService.AddPage(addPageDto);

                if (addResult == null)
                    return BadRequest();

                return Ok(addResult);
            }
            catch (UriFormatException)
            {
                return BadRequest();
            }
            catch (WebException)
            {
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }


        }

        //DELETE api/page
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid pageId)
        {
            DeleteDto deletePageDto = new DeleteDto {Id = pageId};

            try
            {
                _pageBackService.StopPageChangeChecking(deletePageDto.Id.ToString());
                await _pageService.DeletePage(deletePageDto, GetUserId());
            }
            catch (InvalidOperationException)
            {
                return BadRequest(deletePageDto);
            }

            return Ok(deletePageDto);
        }

        [HttpPost("Split")]
        public async Task<IActionResult> SplitPage([FromBody] NewSplitDto dto)
        {
            var text = await _websiteService.GetHtml(dto.Url);

            return Ok(HtmlHelper.SplitHtml(text));
        }
    }
}