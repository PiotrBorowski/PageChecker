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
        Task<User> Add(UserDto user);
        Task<User> GetUser(string username);
        Task<User> GetUser(int userId);
        Task<User> EditUser(EditUserDto user);
    }
}
