using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PageCheckerAPI.Services.FacebookService;
using PageCheckerAPI.Services.TokenService;
using PageCheckerAPI.ViewModels.User;

namespace PageCheckerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacebookController : ControllerBase
    {
        private readonly IFacebookService _facebookService;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;

        public FacebookController(IFacebookService facebookService, IConfiguration configuration, ITokenService tokenService)
        {
            _facebookService = facebookService;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string userAccessToken)
        {
            if (userAccessToken == string.Empty)
                return BadRequest();

            var appToken = await _facebookService.CreateAppAccessTokenAsync(_configuration["Facebook:AppID"],
                _configuration["Facebook:AppSecret"]);

            var userIsValid = await _facebookService.CheckTokenValidityAsync(userAccessToken, appToken);

            if (!userIsValid)
            {
                return BadRequest();
            }

            var userData = await _facebookService.GetUserDataAsync(userAccessToken);
            var userClaims = await _facebookService.Login(userData);
            var token = _tokenService.BuildToken(userClaims, DateTime.Now.AddDays(1));

            return Ok(new UserViewModel{Token = token, Username = userClaims.Email});
        }
    }
}