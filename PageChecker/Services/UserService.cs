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
        private readonly IEmailNotificationService _emailService;

        public UserService(IUserRepository repository, IConfiguration configuration, IMapper mapper, IEmailNotificationService emailService)
        {
            _repository = repository;
            _configuration = configuration;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<UserClaimsDto> Login(AddUserDto userDto)
        {
            User user = await _repository.GetUser(userDto.Username);

            if (user == null)
                return null;

            if (!HashSaltHelper.VerifyPasswordHash(userDto.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            return _mapper.Map<UserClaimsDto>(user);
        }

        public async Task<UserClaimsDto> Register(AddUserDto userDto)
        {
            if (await _repository.GetUser(userDto.Username) != null)
                return null;

            var user = await _repository.Add(userDto);
            return _mapper.Map<UserClaimsDto>(user);
        }

        public async Task<UserClaimsDto> GetUser(int userId)
        {
           var user = await _repository.GetUser(userId);
            return _mapper.Map<UserClaimsDto>(user);
        }

        public async Task SendVerificationLink(int userId)
        {
            var user = await _repository.GetUser(userId);
            string content = BuildVerificationEmailContent(user);
            _emailService.SendEmailNotification(user.Email, "PageChecker Verification", content, true);
        }

        public async Task<bool> Verify(int userId)
        {
            var user = await _repository.GetUser(userId);

            if (user.Verified)
                return true;

            user.Verified = true;

            EditUserDto userDto = new EditUserDto();
            _mapper.Map(user, userDto);

            user = await _repository.EditUser(userDto);

            return user.Verified;
        }

        public string BuildToken(UserClaimsDto userClaimsDto, DateTime expires)
        {
            Claim[] claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userClaimsDto.UserId.ToString()),
                new Claim(ClaimTypes.Name, userClaimsDto.UserName), 
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Issuer"], claims,
                expires: expires, signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string BuildVerificationEmailContent(User user)
        {
            var userClaims = _mapper.Map<UserClaimsDto>(user);

            var token = BuildToken(userClaims, DateTime.Now.AddDays(30));

            return $"<a href=\"htt" + $"p://localhost:3000/verify/{token}\" > <H2>VERIFY</H2> </a> ";
        }
    }
}
