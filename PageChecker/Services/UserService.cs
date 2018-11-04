using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IConfiguration configuration, IMapper mapper)
        {
            _repository = repository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public UserClaimsDto Login(UserDto userDto)
        {
            User user = _repository.GetUser(userDto.Username);

            if (user == null)
                return null;

            if (!HashSaltHelper.VerifyPasswordHash(userDto.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            return _mapper.Map<UserClaimsDto>(user);
        }

        public UserClaimsDto Register(UserDto userDto)
        {
            if (_repository.GetUser(userDto.Username) != null)
                return null;

            var user = _repository.Add(userDto);
            return _mapper.Map<UserClaimsDto>(userDto);
        }

        public UserClaimsDto GetUser(int userId)
        {
           var user = _repository.GetUser(userId);
            return _mapper.Map<UserClaimsDto>(user);
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
                expires: DateTime.Now.AddDays(1), signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
