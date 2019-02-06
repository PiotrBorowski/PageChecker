﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PageCheckerAPI.Models
{
    public class User
    {
        public User()
        {
            Verified = false;
        }

        [Key]
        public int UserId { get; set; }
        [Required]
        public bool Verified { get; set; }
        [Required]
        public string UserName { get; set; }
        public string Email { get; set; }
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
    }
}
