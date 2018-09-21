using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PageCheckerAPI.ViewModels.Page
{
    public class PageViewModel
    {
        public int PageId { get; set; }
        public TimeSpan RefreshRate { get; set; }
        public bool HasChanged { get; set; }
        public string Url { get; set; }
    }
}
