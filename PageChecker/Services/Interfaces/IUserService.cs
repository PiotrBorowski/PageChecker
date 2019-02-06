using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PageCheckerAPI.DTOs.User;
using PageCheckerAPI.Models;

namespace PageCheckerAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserClaimsDto> Register(UserDto userDto);
        Task<UserClaimsDto> Login(UserDto userDto);
        Task<UserClaimsDto> GetUser(int userId);
        Task SendVerificationLink(int userId);
        Task<bool> Verify(int userId);
        string BuildToken(UserClaimsDto claimsDto, DateTime expires);
    }
}
