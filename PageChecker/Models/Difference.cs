using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PageCheckerAPI.Models
{
    public class Difference
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid DifferenceId { get; set; }
        [Required]
        [Column(TypeName = "ntext")]
        public string Text { get; set; }
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public Guid PageId { get; set; }
        public Page Page { get; set; }
    }
}
