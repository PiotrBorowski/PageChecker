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
using PageCheckerAPI.Helpers;
using PageCheckerAPI.Services.EmailService;
using PageCheckerAPI.Services.PageBackgroundService;
using PageCheckerAPI.Services.PageService;
using PageCheckerAPI.Services.WebsiteService;
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
        private readonly IEmailService _emailService;

        public PageController(IPageService pageService, IPageBackgroundService pageBackService, IWebsiteService websiteService, IMapper mapper, IEmailService emailService)
        {
            _pageService = pageService;
            _pageBackService = pageBackService;
            _mapper = mapper;
            _websiteService = websiteService;
            _emailService = emailService;
        }

        private int GetUserId()
        {
            var userIdClaim = User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            return Int32.Parse(userIdClaim.Value);
        }

        // GET api/page
        [HttpGet]
        public async Task<IActionResult> GetPages()
        {           
            List<PageDto> pageDtos = await _pageService.GetPages(GetUserId());
            List<PageViewModel> pageViewModels = _mapper.Map<List<PageDto>, List<PageViewModel>>(pageDtos);

            return Ok(pageViewModels);
        }

        [HttpGet("StartChecking")]
        public async Task<IActionResult> StartChecking(int pageId)
        {
            var pageDto = await _pageService.GetPage(pageId);
            if (pageDto.UserId != GetUserId())
                return BadRequest();

            _pageBackService.StartPageChangeChecking(pageDto);
            pageDto.Stopped = false;
            await _pageService.EditPage(pageDto);

            return Ok(pageDto);
        }

        [HttpDelete("StopChecking")]
        public async Task<IActionResult> StopChecking(int pageId)
        {
            var pageDto = await _pageService.GetPage(pageId);
            if (pageDto.UserId != GetUserId())
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
                addPageDto.Body = await _websiteService.GetHtml(addPageDto.Url);
            }
            catch (UriFormatException)
            {
                return BadRequest();
            }
            catch (WebException)
            {
                return BadRequest();
            }

            var addResult = await _pageService.AddPage(addPageDto);

            if (addResult == null)
                return BadRequest();

            return Ok(addResult);
        }

        //DELETE api/page
        [HttpDelete]
        public async Task<IActionResult> Delete(int pageId)
        {
            DeletePageDto deletePageDto = new DeletePageDto {PageId = pageId};

            try
            {
                _pageBackService.StopPageChangeChecking(deletePageDto.PageId.ToString());
                await _pageService.DeletePage(deletePageDto, GetUserId());
            }
            catch (InvalidOperationException)
            {
                return BadRequest(deletePageDto);
            }

            return Ok(deletePageDto);
        }

    }
}