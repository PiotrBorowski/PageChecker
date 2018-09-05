using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PageCheckerAPI.Models
{
    public class Page
    {
        [Key]
        public int PageId { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
