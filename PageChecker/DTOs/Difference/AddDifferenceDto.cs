using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PageCheckerAPI.DTOs.Difference
{
    public class AddDifferenceDto
    {
        [Required]
        public string Text { get; set; }
        [Required]
        public Guid PageId { get; set; }
    }
}
