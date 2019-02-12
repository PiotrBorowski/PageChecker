using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PageCheckerAPI.Services.Interfaces;

namespace PageCheckerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacebookController : ControllerBase
    {
        private readonly IFacebookService _facebookService;
        private readonly IConfiguration _configuration;

        public FacebookController(IFacebookService facebookService, IConfiguration configuration)
        {
            _facebookService = facebookService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> CreateAppToken()
        {
            var token = await _facebookService.CreateAppAccessTokenAsync(_configuration["Facebook:AppID"],
                _configuration["Facebook:AppSecret"]);

            return Ok(token);
        }
    }
}