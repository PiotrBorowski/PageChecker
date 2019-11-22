using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PageCheckerAPI.Models;
using PageCheckerAPI.Repositories.Interfaces;

namespace PageCheckerAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        private readonly IGenericRepository<Page> _pageRepo;
        private readonly IGenericRepository<User> _userRepo;


        public AdminController(IGenericRepository<Page> pageRepo, IGenericRepository<User> userRepo)
        {
            _pageRepo = pageRepo;
            _userRepo = userRepo;
        }

        // GET api/Admin
        [HttpGet("GroupedPages")]
        public async Task<IActionResult> GroupedPages()
        {
            var sites = await _pageRepo.GetAll();
            var grouped = sites.GroupBy(x => x.Url).Select(y => new {Url = y.Key, Count = y.Count()});

            return Ok(grouped);
        }

        // GET api/Admin
        [HttpGet("Users")]
        public async Task<IActionResult> Users()
        {
            var users = await _userRepo.GetAll();
            var selected = users.Select(x => new {x.Email, x.Verified, x.UserId, x.Role});

            return Ok(selected);
        }

        [HttpGet("UserPages")]
        public async Task<IActionResult> UserPages(string email)
        {
            try
            {
                var user = _userRepo.FindBy(x => x.Email.Equals(email)).Single();
                var pages = _pageRepo.FindBy(x => x.UserId == user.UserId).ToList();
                return Ok(pages);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}