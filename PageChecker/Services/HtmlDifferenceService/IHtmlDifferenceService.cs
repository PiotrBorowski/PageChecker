using PageCheckerAPI.Models;

namespace PageCheckerAPI.Services.HtmlDifferenceService
{
    public interface IHtmlDifferenceService
    {
        string GetDifference(string html1, string html2, CheckingTypeEnum checkingType);
    }
}
