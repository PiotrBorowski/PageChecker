using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PageCheckerAPI.Helpers;
using PageCheckerAPI.Models;
using PageCheckerAPI.Services.Interfaces;

namespace PageCheckerAPI.Services
{
    public class WebsiteComparer : IWebsiteComparer
    {
        public bool Compare(string html1, string html2, CheckingTypeEnum type)
        {
            switch (type)
            {
                case CheckingTypeEnum.Full:
                    return FullCompare(html1, html2);

                case CheckingTypeEnum.Text:
                    return TextCompare(html1, html2);

                default:
                    return FullCompare(html1, html2);
            }
        }

        private bool FullCompare(string html1, string html2)
        {
            return string.Equals(html1.Trim(), html2.Trim());
        }

        private bool TextCompare(string html1, string html2)
        {
            var body1 = HtmlHelper.GetBodyText(html1);
            var body2 = HtmlHelper.GetBodyText(html2);

            return string.Equals(body1, body2);
        }
    }
}
