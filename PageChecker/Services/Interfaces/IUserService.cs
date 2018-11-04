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
        UserClaimsDto Register(UserDto userDto);
        UserClaimsDto Login(UserDto userDto);
        UserClaimsDto GetUser(int userId);
        string BuildToken(UserClaimsDto claimsDto);
    }
}
