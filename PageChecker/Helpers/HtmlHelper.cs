﻿using System;
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

            var htmlNodes = htmlDoc.DocumentNode.SelectNodes("//body");

            StringBuilder result = new StringBuilder();

            foreach (var node in htmlNodes)
            {
                result.Append(node.InnerText);
            }

            return result.ToString();
        }
    }
}