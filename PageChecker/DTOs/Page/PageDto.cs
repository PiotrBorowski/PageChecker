using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PageCheckerAPI.Models;

namespace PageCheckerAPI.DTOs.Page
{
    public class PageDto
    {
        [Required]
        public int PageId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public TimeSpan RefreshRate { get; set; }
        [Required]
        public string Url { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public string BodyDifference { get; set; }
        public bool HasChanged { get; set; }
        public bool Stopped { get; set; }
        public CheckingTypeEnum CheckingType { get; set; }
    }
}
