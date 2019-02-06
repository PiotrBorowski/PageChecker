using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper _mapper;

        public UserRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<User> Add(UserDto userDto)
        {
            User user = new User {UserName = userDto.Username, Email = userDto.Email};

            byte[] passwordHash, passwordSalt;
            HashSaltHelper.GeneratePasswordHashAndSalt(userDto.Password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> GetUser(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<User> GetUser(int userId)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<User> EditUser(EditUserDto user)
        {
            var userToEdit = await _context.Users.SingleOrDefaultAsync(x => x.UserId == user.UserId);

            _mapper.Map(user, userToEdit);

            if (await _context.SaveChangesAsync() > 0)
            {
                return userToEdit;
            }

            return null;
        }
    }
}
