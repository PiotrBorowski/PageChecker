using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PageCheckerAPI.DTOs.Facebook;
using PageCheckerAPI.DTOs.User;
using PageCheckerAPI.Services.Interfaces;

namespace PageCheckerAPI.Services
{
    public class FacebookService : IFacebookService
    {
        private readonly HttpClient _client;

        public FacebookService()
        {
            _client = new HttpClient();
        }

        public async Task<FacebookUserDto> GetUserDataAsync(string userToken)
        {
            var userInfoResponse = await _client.GetStringAsync($"https://" + $"graph.facebook.com/v2.8/me?fields=id,email,first_name,last_name,name,picture&access_token={userToken}");
            var userInfo = JsonConvert.DeserializeObject<FacebookUserDto>(userInfoResponse);

            return userInfo;
        }

        public async Task<bool> CheckTokenValidityAsync(string userToken, string appToken)
        {
            var userAccessTokenValidationResponse = await _client.GetStringAsync($"https://" + $"graph.facebook.com/debug_token?input_token={userToken}&access_token={appToken}");
            var userAccessTokenValidation = JsonConvert.DeserializeObject<UserAccessTokenValidationDto>(userAccessTokenValidationResponse);

            return userAccessTokenValidation.Data.IsValid;
        }

        public async Task<string> CreateAppAccessTokenAsync(string appId, string appSecret)
        {
            var appAccessTokenResponse = await _client.GetStringAsync($"https://" + $"graph.facebook.com/oauth/access_token?client_id={appId}&client_secret={appSecret}&grant_type=client_credentials");
            var appAccessToken = JsonConvert.DeserializeObject<AccessTokenDto>(appAccessTokenResponse);

            return appAccessToken.AccessToken;
        }
    }
}
