using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PageCheckerAPI.DTOs.Facebook;
using PageCheckerAPI.DTOs.User;

namespace PageCheckerAPI.Services.Interfaces
{
    public interface IFacebookService
    {
        Task<FacebookUserDto> GetUserDataAsync(string token);
        Task<bool> CheckTokenValidityAsync(string userToken, string appToken);
        Task<string> CreateAppAccessTokenAsync(string appId, string appSecret);
        Task<UserClaimsDto> Login(FacebookUserDto userDto);
    }
}
