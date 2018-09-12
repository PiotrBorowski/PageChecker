using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PageCheckerAPI.DTOs.User;
using PageCheckerAPI.Helpers;
using PageCheckerAPI.Models;
using PageCheckerAPI.Repositories.Interfaces;
using PageCheckerAPI.Services.Interfaces;

namespace PageCheckerAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public User Login(UserDto userDto)
        {
            User user = _repository.GetUser(userDto.Username);

            if (user == null)
                return null;

            if (!HashSaltHelper.VerifyPasswordHash(userDto.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public User Register(UserDto userDto)
        {
            if (_repository.GetUser(userDto.Username) != null)
                return null;

            return _repository.Add(userDto);
        }
    }
}
