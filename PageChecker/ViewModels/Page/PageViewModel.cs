﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PageCheckerAPI.Models;

namespace PageCheckerAPI.ViewModels.Page
{
    public class PageViewModel
    {
        public int PageId { get; set; }
        public string Name { get; set; }
        public TimeSpan RefreshRate { get; set; }
        public bool HasChanged { get; set; }
        public bool Stopped { get; set; }
        public CheckingTypeEnum CheckingType { get; set; }
        public string BodyDifference { get; set; }
        public string Url { get; set; }
    }
}
