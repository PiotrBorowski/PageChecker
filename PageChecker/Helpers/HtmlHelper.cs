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

            var htmlNodes = htmlDoc.DocumentNode.SelectNodes("//*[not(self::script) and not(self::style)]/text()");

            StringBuilder result = new StringBuilder();
            bool first = true;

            foreach (var node in htmlNodes)
            {
                if (node.InnerText.Trim() != string.Empty)
                {
                    string toAppend;
                    if (first)
                    {
                        toAppend = node.InnerText;
                        first = false;
                    }
                    else
                    {
                        toAppend = " " + node.InnerText;
                    }

                    result.Append(toAppend);
                }
            }

            return result.ToString();
        }

        public static string GetTextDifference(string html1, string html2)
        {
            StringBuilder result = new StringBuilder();

            var words1 = html1.Split(" ");
            var words2 = html2.Split(" ");
            bool first = true;

            foreach (var word in words2)
            {
                if (!words1.Contains(word))
                {
                    string toAppend;
                    if (first == true)
                    {
                        toAppend = word;
                        first = false;
                    }
                    else
                    {
                        toAppend = " " + word;
                    }

                    result.Append(toAppend);
                }

            }

            return result.ToString();
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


        public static List<string> SplitHtml(string html)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var htmlNodes = htmlDoc.DocumentNode.SelectNodes("//div");
            //var styleNodes = htmlDoc.DocumentNode.SelectNodes("//style");

            var result = new List<string>();
            foreach (var node in htmlNodes)
            {
                //node.AppendChildren(styleNodes);
                node.Attributes.Add("XPath", node.XPath);
       
                result.Add(node.OuterHtml);
            }

            return result;
        }

        public static string GetNode(string html, string xpath)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var node = htmlDoc.DocumentNode.SelectSingleNode(xpath);

            return node.OuterHtml;
        }
    }
}

