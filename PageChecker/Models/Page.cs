using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageCheckerAPI.Models
{
    public enum CheckingTypeEnum
    {
        Full,
        Text
    }

    public class Page
    {
        public Page()
        {
            HasChanged = false;
            Stopped = false;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PageId { get; set; }
        public string Name { get; set; }
        public TimeSpan RefreshRate { get; set; }
        public bool HasChanged { get; set; }
        public bool Stopped { get; set; }
        public CheckingTypeEnum CheckingType { get; set; }

        [ForeignKey("WebsiteText")]
        public Guid PrimaryTextId { get; set; }
        public WebsiteText PrimaryText { get; set; }

        [ForeignKey("WebsiteText")]
        public Guid SecondaryTextId { get; set; }
        public WebsiteText SecondaryText { get; set; }

        [Required]
        public string Url { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
