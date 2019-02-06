using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PageCheckerAPI.DTOs.User
{
    public class EditUserDto
    {
        public int UserId { get; set; }
        public bool Verified { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
