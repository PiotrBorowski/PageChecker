using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using PageCheckerAPI.Helpers;
using PageCheckerAPI.Models;
using PageCheckerAPI.Services.HtmlDifferenceService.DifferenceServicesFactory;

namespace PageCheckerAPI.Services.HtmlDifferenceService
{
    public class HtmlDifferenceService : IHtmlDifferenceService
    {
        private readonly IDifferenceServicesFactory _differenceServicesFactory;

        public HtmlDifferenceService(IDifferenceServicesFactory differenceServicesFactory)
        {
            _differenceServicesFactory = differenceServicesFactory;
        }

        public string GetDifference(string html1, string html2, CheckingTypeEnum checkingType)
        {
            var differ = _differenceServicesFactory.Create(checkingType);

            return differ.GetDifference(html1, html2);
        }
    }
}
