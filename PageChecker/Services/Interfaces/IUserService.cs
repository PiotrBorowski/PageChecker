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
        Task<UserClaimsDto> Register(AddUserDto userDto);
        Task<UserClaimsDto> Login(AddUserDto userDto);
        Task<UserClaimsDto> GetUser(int userId);
        Task SendVerificationLink(int userId);
        Task<bool> Verify(int userId);
    }
}
