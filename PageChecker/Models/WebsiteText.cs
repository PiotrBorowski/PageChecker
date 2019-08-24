using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PageCheckerAPI.Models
{
    public class WebsiteText
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid WebsiteTextId { get; set; }
        [Required]
        [Column(TypeName = "ntext")]
        public string Text { get; set; }
    }
}
