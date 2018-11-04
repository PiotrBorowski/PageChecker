using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PageCheckerAPI.DTOs.User;
using PageCheckerAPI.Models;

namespace PageCheckerAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User Add(UserDto user);
        User GetUser(string username);
        User GetUser(int userId);
    }
}
