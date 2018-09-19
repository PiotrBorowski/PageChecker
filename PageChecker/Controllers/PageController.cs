using System;
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
        private readonly IPageService _service;
        private readonly IMapper _mapper;

        public PageController(IPageService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public void RunInBackground()
        {
            Console.WriteLine("Dodano strone");
        }

        // GET api/page
        [HttpGet]
        public IActionResult Get()
        {
            var userIdClaim = User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            List<PageDto> pageDtos = _service.GetPages(Int32.Parse(userIdClaim.Value));
            List<PageViewModel> pageViewModels = _mapper.Map<List<PageDto>, List<PageViewModel>>(pageDtos);

            return Ok(pageViewModels);
        }

        // POST api/page
        [HttpPost]
        public IActionResult Post([FromBody]AddPageDto addPageDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //user id from user claims
            var userIdClaim = User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            addPageDto.UserId = Int32.Parse(userIdClaim.Value);

            var addResult = _service.AddPage(addPageDto);

            if (addResult == false)
                return BadRequest();

            BackgroundJob.Enqueue(() => RunInBackground());

            return Ok();
        }

        //DELETE api/page
        [HttpDelete]
        public IActionResult Delete(int pageId)
        {
            DeletePageDto deletePageDto = new DeletePageDto {PageId = pageId};

            var userIdClaim = User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            try
            {
                _service.DeletePage(deletePageDto, Int32.Parse(userIdClaim.Value));
            }
            catch (InvalidOperationException)
            {
                return BadRequest(deletePageDto);
            }

            return Ok(deletePageDto);
        }
    }
}