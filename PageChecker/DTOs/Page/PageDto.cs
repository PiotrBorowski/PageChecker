using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PageCheckerAPI.DTOs.Page
{
    public class PageDto
    {
        [Required]
        public int PageId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public TimeSpan RefreshSpan { get; set; }
        [Required]
        public string Url { get; set; }
        public string Body { get; set; }
        public bool HasChanged { get; set; }
    }
}
