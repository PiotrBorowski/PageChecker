using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PageCheckerAPI.DTOs.User;
using PageCheckerAPI.Models;
using PageCheckerAPI.Services.TokenService;
using PageCheckerAPI.Services.UserService;
using PageCheckerAPI.ViewModels.User;

namespace PageCheckerAPI.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _service;
        private readonly ITokenService _tokenService;

        public UserController(IUserService service, ITokenService tokenService)
        {
            _service = service;
            _tokenService = tokenService;
        }

        [HttpGet("verify")]
        public async Task<IActionResult> Verify()
        {
            var userIdClaim = User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            
            if (userIdClaim == null)
                return BadRequest();

            Guid userId = Guid.Parse(userIdClaim.Value);

            if (!await _service.Verify(userId))
                return BadRequest();

            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AddUserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            UserClaimsDto user = await _service.Register(userDto);

            if (user == null)
                return BadRequest(ModelState);

            await _service.SendVerificationLink(user.UserId);
            return Ok(userDto.Username);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AddUserDto userDto)
        {
            UserClaimsDto user = await _service.Login(userDto);

            if (user == null)
                return Unauthorized();

            if (!user.Verified)
                return Forbid();

            string tokenString = _tokenService.BuildToken(user, DateTime.Now.AddDays(1));

            var userViewModel = new UserViewModel
            {
                Username = user.UserName,
                Token = tokenString
            };

            return Ok(userViewModel);
        }

    }
}