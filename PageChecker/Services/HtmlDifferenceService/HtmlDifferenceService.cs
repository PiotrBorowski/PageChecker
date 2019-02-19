using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using PageCheckerAPI.Helpers;
using PageCheckerAPI.Models;

namespace PageCheckerAPI.Services.HtmlDifferenceService
{
    public class HtmlDifferenceService : IHtmlDifferenceService
    {
        private readonly diff_match_patch _differ;

        public HtmlDifferenceService()
        {
            _differ = new diff_match_patch();
        }

        public string GetDifference(string html1, string html2, CheckingTypeEnum checkingType)
        {
            List<Diff> listOfDiffs;
            switch (checkingType)
            {
                case CheckingTypeEnum.Text:
                    var pageBodyText = HtmlHelper.GetBodyText(html1);
                    var webBodyText = HtmlHelper.GetBodyText(html2);
                    listOfDiffs = _differ.diff_main(pageBodyText, webBodyText);
                    break;

                case CheckingTypeEnum.Full:
                    listOfDiffs = _differ.diff_main(html1, html2);
                    break;

                default:
                    throw new InvalidEnumArgumentException();

            }

            return _differ.diff_prettyHtml(listOfDiffs);
        }
    }
}
