using System;
using System.Net.Mail;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using PageCheckerAPI.DTOs.User;
using PageCheckerAPI.Helpers;
using PageCheckerAPI.Models;
using PageCheckerAPI.Repositories.Interfaces;
using PageCheckerAPI.Services.EmailNotificationService;
using PageCheckerAPI.Services.TokenService;

namespace PageCheckerAPI.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IEmailNotificationService _emailService;
        private readonly ITokenService _tokenService;

        public UserService(IUserRepository repository, IConfiguration configuration, IMapper mapper,
            IEmailNotificationService emailService, ITokenService tokenService)
        {
            _repository = repository;
            _configuration = configuration;
            _mapper = mapper;
            _emailService = emailService;
            _tokenService = tokenService;
        }

        public async Task<UserClaimsDto> Login(AddUserDto userDto)
        {
            User user = await _repository.GetUser(userDto.Email);

            if (user == null)
                return null;

            if (!HashSaltHelper.VerifyPasswordHash(userDto.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            return _mapper.Map<UserClaimsDto>(user);
        }

        public async Task<UserClaimsDto> Register(AddUserDto userDto)
        {
            if (await _repository.GetUser(userDto.Email) != null)
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
            var message = new MailMessage(_configuration["Gmail:email"], user.Email, "PageChecker Verification", content);
            message.IsBodyHtml = true;

            _emailService.SendEmailNotification(message);
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

        private string BuildVerificationEmailContent(User user)
        {
            var userClaims = _mapper.Map<UserClaimsDto>(user);

            var token = _tokenService.BuildToken(userClaims, DateTime.Now.AddDays(30));

            return $"<a href=\"htt" + $"p://localhost:3000/verify/{token}\" > <H2>VERIFY</H2> </a> ";
        }
    }
}
