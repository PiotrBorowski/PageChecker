using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace PageCheckerAPI.DTOs.Facebook
{
    public class AccessTokenDto
    {
        [JsonProperty]
        public string AccessToken { get; set; }
    }
}
