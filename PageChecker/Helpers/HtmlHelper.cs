using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace PageCheckerAPI.Helpers
{
    public static class HtmlHelper
    {
        public static string GetBodyText(string html)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var htmlNodes = htmlDoc.DocumentNode.SelectNodes("//*[not(self::script)]/text()");

            StringBuilder result = new StringBuilder();

            foreach (var node in htmlNodes)
            {
                result.Append(node.InnerText);
            }

            return result.ToString();
        }

        public static string GetBodyTextDifference(string html1, string html2)
        {
            return string.Join(" ", GetBodyText(html2).Split(' ').Where(item => !GetBodyText(html1).Contains(item)));
        }
    }
}
