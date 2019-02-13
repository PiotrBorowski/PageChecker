using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace PageCheckerAPI.DTOs.Facebook
{
    public class UserAccessTokenValidationDto
    {
        public Data Data { get; set; }
    }

    public class Data
    {
        [JsonProperty(PropertyName = "is_valid")]
        public bool IsValid { get; set; }
    }
}
