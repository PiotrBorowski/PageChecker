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
        public Guid PageId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public RefreshRateEnum RefreshRate { get; set; }
        [Required]
        public string Url { get; set; }
        public string Name { get; set; }
        public Guid PrimaryTextId { get; set; }
        public bool HasChanged { get; set; }
        public bool Stopped { get; set; }
        public CheckingTypeEnum CheckingType { get; set; }
        public string ElementXPath { get; set; }
        public DateTime CreationDate { get; set; }
        public bool HighAccuracy { get; set; }
    }
}
