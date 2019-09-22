using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PageCheckerAPI.Helpers;

namespace PageCheckerAPI.Services.HtmlDifferenceService.DifferenceServices
{
    public class FullDifferenceService : IDifferenceService
    {
        private readonly diff_match_patch _differ;

        public FullDifferenceService()
        {
            _differ = new diff_match_patch();
        }

        public string GetDifference(string html1, string html2)
        {
            var listOfDiffs = _differ.diff_main(html1, html2);
            _differ.diff_cleanupSemantic(listOfDiffs);
            
            return _differ.diff_prettyHtml(listOfDiffs);
        }
    }
}
