using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using PageCheckerAPI.DTOs.Facebook;
using PageCheckerAPI.DTOs.User;
using PageCheckerAPI.Repositories.Interfaces;
using PageCheckerAPI.Services.Interfaces;

namespace PageCheckerAPI.Services
{
    public class FacebookService : IFacebookService
    {
        private readonly HttpClient _client;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public FacebookService(IUserRepository userRepository, IMapper mapper)
        {
            _client = new HttpClient();
            _userRepository = userRepository;
            _mapper = mapper;
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

        public async Task<UserClaimsDto> Login(FacebookUserDto userDto)
        {
            var user = await _userRepository.GetUser(userDto.Email);

            if(user != null)
                return _mapper.Map<UserClaimsDto>(user);

            await _userRepository.Add(new AddUserDto
            {
                Email = userDto.Email,
                Username = userDto.FirstName + " " + userDto.LastName
            });

            var userClaims = _userRepository.GetUser(userDto.Email);


            return _mapper.Map<UserClaimsDto>(userClaims);
        }
    }
}
