﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PageCheckerAPI.DTOs.Page;
using PageCheckerAPI.Services.Interfaces;
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

        public PageController(IPageService pageService, IPageBackgroundService pageBackService, IWebsiteService websiteService, IMapper mapper)
        {
            _pageService = pageService;
            _pageBackService = pageBackService;
            _mapper = mapper;
            _websiteService = websiteService;
        }

        private int GetUserId()
        {
            var userIdClaim = User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            return Int32.Parse(userIdClaim.Value);
        }

        // GET api/page
        [HttpGet]
        public IActionResult Get()
        {           
            List<PageDto> pageDtos = _pageService.GetPages(GetUserId());
            List<PageViewModel> pageViewModels = _mapper.Map<List<PageDto>, List<PageViewModel>>(pageDtos);

            return Ok(pageViewModels);
        }

        [HttpGet("StartChecking")]
        public IActionResult StartChecking(int pageId)
        {
            var pageDto = _pageService.GetPage(pageId);
            if (pageDto.UserId != GetUserId())
                return BadRequest();

            _pageBackService.StartPageChangeChecking(pageDto);
            pageDto.Stopped = false;
            _pageService.EditPage(pageDto);

            return Ok(pageDto);
        }

        [HttpDelete("StopChecking")]
        public IActionResult StopChecking(int pageId)
        {
            var pageDto = _pageService.GetPage(pageId);
            if (pageDto.UserId != GetUserId())
                return BadRequest();

            _pageBackService.StopPageChangeChecking(pageDto.PageId.ToString());
            pageDto.Stopped = true;
            _pageService.EditPage(pageDto);

            return Ok(pageDto);
        }

        // POST api/page
        [HttpPost]
        public IActionResult Post([FromBody]AddPageDto addPageDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            addPageDto.UserId = GetUserId();
            addPageDto.Body = _websiteService.GetBody(addPageDto.Url);

            var addResult = _pageService.AddPage(addPageDto);

            if (addResult == null)
                return BadRequest();

            return Ok(addResult);
        }

        //DELETE api/page
        [HttpDelete]
        public IActionResult Delete(int pageId)
        {
            DeletePageDto deletePageDto = new DeletePageDto {PageId = pageId};

            try
            {
                _pageService.DeletePage(deletePageDto, GetUserId());
                _pageBackService.StopPageChangeChecking(deletePageDto.PageId.ToString());
            }
            catch (InvalidOperationException)
            {
                return BadRequest(deletePageDto);
            }

            return Ok(deletePageDto);
        }
    }
}