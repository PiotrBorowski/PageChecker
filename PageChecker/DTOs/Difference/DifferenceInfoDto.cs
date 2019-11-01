using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PageCheckerAPI.DTOs.Difference
{
    public class DifferenceInfoDto
    {
        [Required]
        public Guid DifferenceId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public Guid PageId { get; set; }
    }
}
