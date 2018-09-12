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
        User Register(UserDto userDto);
        User Login(UserDto userDto);
    }
}
