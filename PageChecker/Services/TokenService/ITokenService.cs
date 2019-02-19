using System;
using PageCheckerAPI.DTOs.User;

namespace PageCheckerAPI.Services.TokenService
{
    public interface ITokenService
    {
        string BuildToken(UserClaimsDto userClaimsDto, DateTime expires);
    }
}
