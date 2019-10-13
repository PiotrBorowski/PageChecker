using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PageCheckerAPI.Helpers;

namespace PageCheckerAPI.Services.HtmlDifferenceService.DifferenceServices
{
    public class TextDifferenceService : IDifferenceService
    {
        private readonly diff_match_patch _differ;

        public TextDifferenceService()
        {
            _differ = new diff_match_patch();
        }

        public string GetDifference(string html1, string html2)
        {
            var pageBodyText = HtmlHelper.GetBodyText(html1);
            var webBodyText = HtmlHelper.GetBodyText(html2);
            var listOfDiffs = _differ.diff_main(pageBodyText, webBodyText);
            _differ.diff_cleanupSemantic(listOfDiffs);

            return _differ.diff_prettyHtml(listOfDiffs);
        }
    }
}
