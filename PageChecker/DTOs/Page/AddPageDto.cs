using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PageCheckerAPI.Models;

namespace PageCheckerAPI.DTOs.Page
{
    public class AddPageDto
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Refresh Rate is required")]
        public TimeSpan RefreshRate { get; set; }
        [Required(ErrorMessage = "Url is required")]
        public string Url { get; set; }
        [Required(ErrorMessage = "Choose checking type")]
        public CheckingTypeEnum CheckingType { get; set; }
        public string Body { get; set; }
    }
}
