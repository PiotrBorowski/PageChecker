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
    }
}
