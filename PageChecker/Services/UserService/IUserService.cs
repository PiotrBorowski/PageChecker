using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PageCheckerAPI.DTOs.User;
using PageCheckerAPI.Models;

namespace PageCheckerAPI.Services.UserService
{
    public interface IUserService
    {
        Task<UserClaimsDto> Register(AddUserDto userDto);
        Task<UserClaimsDto> Login(AddUserDto userDto);
        Task<UserClaimsDto> GetUser(Guid userId);
        Task SendVerificationLink(Guid userId);
        Task<bool> Verify(Guid userId);
    }
}
