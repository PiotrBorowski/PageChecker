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
        Task<User> Add(AddUserDto user);
        Task<User> GetUser(string email);
        Task<User> GetUser(int userId);
        Task<User> EditUser(EditUserDto user);
    }
}
