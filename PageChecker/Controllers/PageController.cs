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

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            List<PageDto> pageDtos = _service.GetPages();
            List<PageViewModel> pageViewModels = _mapper.Map<List<PageDto>, List<PageViewModel>>(pageDtos);

            return Ok(pageViewModels);
        }
    }
}