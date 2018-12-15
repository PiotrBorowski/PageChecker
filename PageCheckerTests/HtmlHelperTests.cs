using System;
using PageCheckerAPI.Helpers;
using Xunit;

namespace PageCheckerTests
{
    public class HtmlHelperTests
    {
        [Fact]
        public void TextDifferenceCheck()
        {
            var word1 = "ALA MA KOTA";
            var word2 = "ALA MIALA KOTA";

            var result = HtmlHelper.GetTextDifference(word1, word2);

            Assert.Equal("MIALA", result);
        }

        [Fact]
        public void TextDifferenceCheck_DifferentSentences()
        {
            string word1 = "ABCD";
            string word2 = "AB CD";

            string result = HtmlHelper.GetTextDifference(word1, word2);

            Assert.Equal(word2, result);
        }

        [Fact]
        public void GetBodyText_TextFromTitleAndBody()
        {
            string html = @"<!DOCTYPE html><html lang=""pl"">
<head> <meta charset=""UTF-8"" /><title>Test Title !!!</title> 
<style type=""text/css""> table, tr, td, th { border: 1px solid #1e1e1e; }
                                table { border-collapse: collapse; } th, td { padding: 5px; } 
                                tr:nth-child(odd) { background-color: #efefef; } </style> 
                                </head> 
                                <body> 
                                <div>
                                <h1>Test Header</h1> <h2>Header H2</h2> <a href=""http://www.zsk.ict.pwr.wroc.pl/zsk/dyd/intinz/gk/lab/cw_5_dz/"">Link</a> <h2>Header H2</h2>  
                                </div> 
                                </body>
                                </html> ";

            string result = HtmlHelper.GetBodyText(html);

            Assert.Equal("Test Title !!! Test Header Header H2 Link Header H2", result);
        }
    }
}
