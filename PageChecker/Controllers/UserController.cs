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
        public IActionResult Register([FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            User user = _service.Register(userDto);

            if (user == null)
                return BadRequest(ModelState);

            return Ok(userDto.Username);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserDto userDto)
        {
            User user = _service.Login(userDto);
            if (user == null)
                return Unauthorized();

            UserClaimsDto clamDto = _mapper.Map<UserClaimsDto>(user);
            string tokenString = _service.BuildToken(clamDto);

            //it's working but it IS NOT VISIBLE from browser, so that's good :P
            HttpContext.Response.Cookies.Append("token", tokenString, new CookieOptions
            {
                Expires = DateTime.Now.AddHours(1)
            });

            return Ok(tokenString);
        }

        [HttpGet("cookie")]
        public IActionResult Get()
        {
            return Ok(Request.Cookies["token"]);
        }
    }
}