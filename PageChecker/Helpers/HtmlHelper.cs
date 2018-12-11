using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using PageCheckerAPI.Models;

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

        public static string GetTextDifference(string html1, string html2)
        {
            StringBuilder result = new StringBuilder();

            var words2 = html2.Split(" ");

            foreach (var word in words2)
            {
                if (!html1.Contains(word))
                    result.Append(word+" ");
            }

            return result.ToString();
            //return string.Join(" ", GetBodyText(html2).Split(' ').Where(item => !GetBodyText(html1).Contains(item)));
        }

        public static bool Compare(string html1, string html2, CheckingTypeEnum type)
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

        private static bool FullCompare(string html1, string html2)
        {
            return string.Equals(html1.Trim(), html2.Trim());
        }

        private static bool TextCompare(string html1, string html2)
        {
            var body1 = GetBodyText(html1);
            var body2 = GetBodyText(html2);

            return string.Equals(body1, body2);
        }
    }
}

