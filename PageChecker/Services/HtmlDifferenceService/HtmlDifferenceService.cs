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

            var difference = differ.GetDifference(html1, html2);

            return differ.Prettyfy(difference);
        }

        public string GetPatches(string html1, string html2, CheckingTypeEnum checkingType)
        {
            var differ = _differenceServicesFactory.Create(checkingType);

            var difference = differ.GetDifference(html1, html2);

            return differ.GetPatchText(html1, difference);
        }

        public string ApplyPatches(string patches, string text, CheckingTypeEnum checkingType)
        {
            var differ = _differenceServicesFactory.Create(checkingType);

            var result = differ.CreatePatch(patches, text);

            return result;
        }

        public string Prettyfy(string patches, string text, CheckingTypeEnum checkingType)
        {
            var differ = _differenceServicesFactory.Create(checkingType);

            var secondText = differ.CreatePatch(patches, text);
            var listOfDiffs = differ.GetDifference(text, secondText);

            return differ.Prettyfy(listOfDiffs);
        }
    }
}
