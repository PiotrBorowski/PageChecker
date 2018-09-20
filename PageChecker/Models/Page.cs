using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PageCheckerAPI.Models
{
    public class Page
    {
        [Key]
        public int PageId { get; set; }

        public TimeSpan RefreshSpan { get; set; }

        [Column(TypeName = "text")]
        public string Body { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
