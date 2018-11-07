using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace PageCheckerAPI.DTOs.User
{
    public class UserClaimsDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
