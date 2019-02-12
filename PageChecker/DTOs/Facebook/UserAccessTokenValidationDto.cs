using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PageCheckerAPI.DTOs.Facebook
{
    public class UserAccessTokenValidationDto
    {
        public Data Data { get; set; }
    }

    public class Data
    {
        public bool IsValid { get; set; }
    }
}
