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
using PageCheckerAPI.Services.HtmlDifferenceService.DifferenceServices;
using PageCheckerAPI.Services.PageDifferenceService;

namespace PageCheckerAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class DifferenceController : Controller
    {
        private readonly IPageDifferenceService _differenceService;
        private readonly IMapper _mapper; 


        public DifferenceController(
            IPageDifferenceService differenceService,
            IMapper mapper
            )
        {
            _differenceService = differenceService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetDifference(Guid guid)
        {
            var diff = await _differenceService.GetDifference(guid);
            if (diff == null)
            {
                return BadRequest();
            }

            return Ok(diff);
        }

        [HttpGet("info")]
        public async Task<IActionResult> GetDifferencesInfo(Guid guid)
        {
            var diff = await _differenceService.GetDifferencesInfo(guid);
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

            var diff = await _differenceService.AddDifference(differenceDto);

            if (diff == null)
                return BadRequest();

            return Ok(diff);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDifference(Guid guid)
        {
            try
            {
                await _differenceService.DeleteDifference(new DeleteDto {Id = guid});
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}