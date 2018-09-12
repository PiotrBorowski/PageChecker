using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault.Models;
using PageCheckerAPI.DataAccess;
using PageCheckerAPI.DTOs.User;
using PageCheckerAPI.Helpers;
using PageCheckerAPI.Models;
using PageCheckerAPI.Repositories.Interfaces;

namespace PageCheckerAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public User Add(UserDto userDto)
        {
            User user = new User {UserName = userDto.Username};

            byte[] passwordHash, passwordSalt;
            HashSaltHelper.GeneratePasswordHashAndSalt(userDto.Password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public User GetUser(string username)
        {
            return _context.Users.SingleOrDefault(x => x.UserName == username);
        }
    }
}
