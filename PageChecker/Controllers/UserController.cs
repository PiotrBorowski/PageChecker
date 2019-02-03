using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PageCheckerAPI.DTOs.User;
using PageCheckerAPI.Models;
using PageCheckerAPI.Services.Interfaces;
using PageCheckerAPI.ViewModels.User;

namespace PageCheckerAPI.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public UserController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            UserClaimsDto user = await _service.Register(userDto);

            if (user == null)
                return BadRequest(ModelState);

            return Ok(userDto.Username);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto userDto)
        {
            UserClaimsDto user = await _service.Login(userDto);
            if (user == null)
                return Unauthorized();

            string tokenString = _service.BuildToken(user);

            var userViewModel = new UserViewModel
            {
                Username = user.UserName,
                Token = tokenString
            };

            return Ok(userViewModel);
        }

    }
}