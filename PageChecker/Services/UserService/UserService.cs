using System;
using System.Net.Mail;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PageCheckerAPI.DTOs.User;
using PageCheckerAPI.Helpers;
using PageCheckerAPI.Models;
using PageCheckerAPI.Repositories.Interfaces;
using PageCheckerAPI.Services.EmailService;
using PageCheckerAPI.Services.TokenService;

namespace PageCheckerAPI.Services.UserService
{
    public class UserService : IUserService
    {
        //private readonly IUserRepository _repository;
        private readonly IGenericRepository<User> _genericRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ITokenService _tokenService;

        public UserService(
            IGenericRepository<User> genericRepository,
            IConfiguration configuration, 
            IMapper mapper,
            IEmailService emailService, 
            ITokenService tokenService)
        {
            _genericRepository = genericRepository;
            _configuration = configuration;
            _mapper = mapper;
            _emailService = emailService;
            _tokenService = tokenService;
        }

        public async Task<UserClaimsDto> Login(AddUserDto userDto)
        {
            User user = await _genericRepository.FindBy(x => x.Email == userDto.Email).SingleAsync();

            if (user == null)
                return null;

            if (!HashSaltHelper.VerifyPasswordHash(userDto.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            return _mapper.Map<UserClaimsDto>(user);
        }

        public async Task<UserClaimsDto> Register(AddUserDto userDto)
        {
            if(await _genericRepository.FindBy(x => x.Email == userDto.Email).AnyAsync())
                return null;

            User user = new User { UserName = userDto.Username, Email = userDto.Email };

            if (!string.IsNullOrEmpty(userDto.Password))
            {
                byte[] passwordHash, passwordSalt;
                HashSaltHelper.GeneratePasswordHashAndSalt(userDto.Password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                await _genericRepository.Add(user);
                return _mapper.Map<UserClaimsDto>(user);
            }

            return null;
        }

        public async Task<UserClaimsDto> GetUser(Guid userId)
        {
            var user = await _genericRepository.FindBy(x => x.UserId.Equals(userId)).SingleAsync();
            return _mapper.Map<UserClaimsDto>(user);
        }

        public async Task SendVerificationLink(Guid userId)
        {
            var user = await _genericRepository.FindBy(x => x.UserId.Equals(userId)).SingleAsync();
            string content = BuildVerificationEmailContent(user);
            var message = new MailMessage(_configuration["Gmail:email"], user.Email, "PageChecker Verification",
                content) {IsBodyHtml = true};

            _emailService.SendEmail(message);
        }

        public async Task<bool> Verify(Guid userId)
        {
            var user = await _genericRepository.FindBy(x => x.UserId.Equals(userId)).SingleAsync();

            if (user.Verified)
                return true;

            user.Verified = true;

            await _genericRepository.Edit(user);
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
