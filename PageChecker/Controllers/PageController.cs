using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PageCheckerAPI.DTOs.Page;
using PageCheckerAPI.Services.Interfaces;
using PageCheckerAPI.ViewModels.Page;

namespace PageCheckerAPI.Controllers
{
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

        // GET api/page
        [HttpGet]
        public IActionResult Get()
        {
            List<PageDto> pageDtos = _service.GetPages();
            List<PageViewModel> pageViewModels = _mapper.Map<List<PageDto>, List<PageViewModel>>(pageDtos);

            return Ok(pageViewModels);
        }

        // POST api/page
        [HttpPost]
        public IActionResult Post([FromBody]AddPageDto addPageDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var addResult = _service.AddPage(addPageDto);

            if (addResult == false)
                return BadRequest();

            return Ok();
        }

        //DELETE api/page
        [HttpDelete]
        public IActionResult Delete(int pageId)
        {
            DeletePageDto deletePageDto = new DeletePageDto {PageId = pageId};

            try
            {
                _service.DeletePage(deletePageDto);
            }
            catch (InvalidOperationException)
            {
                return BadRequest(deletePageDto);
            }

            return Ok(deletePageDto);
        }
    }
}