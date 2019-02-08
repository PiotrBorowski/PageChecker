using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PageCheckerAPI.DTOs.User
{
    public class AddUserDto
    {
        [Required]
        [StringLength(254, MinimumLength = 4, ErrorMessage = "Username must contain between 4 and 254 characters")]
        public string Username { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [StringLength(14, MinimumLength = 6, ErrorMessage = "Password must contain between 6 and 14 characters")]
        public string Password { get; set; }
    }
}
