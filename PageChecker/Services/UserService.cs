using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PageCheckerAPI.DTOs.User;
using PageCheckerAPI.Helpers;
using PageCheckerAPI.Models;
using PageCheckerAPI.Repositories.Interfaces;
using PageCheckerAPI.Services.Interfaces;

namespace PageCheckerAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public User Login(UserDto userDto)
        {
            User user = _repository.GetUser(userDto.Username);

            if (user == null)
                return null;

            if (!HashSaltHelper.VerifyPasswordHash(userDto.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public User Register(UserDto userDto)
        {
            if (_repository.GetUser(userDto.Username) != null)
                return null;

            return _repository.Add(userDto);
        }

        public string BuildToken(UserClaimsDto userClaimsDto)
        {
            Claim[] claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userClaimsDto.UserId.ToString()),
                new Claim(ClaimTypes.Name, userClaimsDto.UserName), 
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Issuer"], claims,
                expires: DateTime.Now.AddMinutes(30), signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
