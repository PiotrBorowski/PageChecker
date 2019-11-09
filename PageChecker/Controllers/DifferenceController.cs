using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PageCheckerAPI.DTOs.Difference;
using PageCheckerAPI.DTOs.Shared;
using PageCheckerAPI.Services.HtmlDifferenceService;
using PageCheckerAPI.Services.HtmlDifferenceService.DifferenceServices;
using PageCheckerAPI.Services.PageDifferenceService;
using PageCheckerAPI.Services.PageService;
using PageCheckerAPI.Services.WebsiteTextService;

namespace PageCheckerAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class DifferenceController : Controller
    {
        private readonly IPageDifferenceService _pageDifferenceService;
        private readonly IPageService _pageService;
        private readonly IHtmlDifferenceService _htmlDifferenceService;
        private readonly IWebsiteTextService _websiteTextService;


        public DifferenceController(
            IPageDifferenceService pageDifferenceService,
            IPageService pageService,
            IWebsiteTextService websiteTextService,
            IHtmlDifferenceService htmlDifferenceService,
            IMapper mapper
            )
        {
            _pageDifferenceService = pageDifferenceService;
            _pageService = pageService;
            _htmlDifferenceService = htmlDifferenceService;
            _websiteTextService = websiteTextService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDifference(Guid guid)
        {
            var diff = await _pageDifferenceService.GetDifference(guid);
            if (diff == null)
            {
                return BadRequest();
            }
            var page = await _pageService.GetPage(diff.PageId);
            if (!page.HighAccuracy)
            {
                var websiteText = await _websiteTextService.GetText(page.PrimaryTextId);
                diff.Text = _htmlDifferenceService.Prettyfy(diff.Text, websiteText.Text, page.CheckingType);
            }

            return Ok(diff);
        }

        [HttpGet("info")]
        public async Task<IActionResult> GetDifferencesInfo(Guid guid)
        {
            var diff = await _pageDifferenceService.GetDifferencesInfo(guid);
            if (diff == null)
            {
                return BadRequest();
            }

            return Ok(diff);
        }

        [HttpPost]
        public async Task<IActionResult> AddDifference([FromBody] AddDifferenceDto differenceDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var diff = await _pageDifferenceService.AddDifference(differenceDto);

            if (diff == null)
                return BadRequest();

            return Ok(diff);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDifference(Guid guid)
        {
            try
            {
                await _pageDifferenceService.DeleteDifference(new DeleteDto {Id = guid});
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}