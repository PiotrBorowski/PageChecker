using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PageCheckerAPI.DTOs.User;

namespace PageCheckerAPI.Services.Interfaces
{
    public interface ITokenService
    {
        string BuildToken(UserClaimsDto userClaimsDto, DateTime expires);
    }
}
