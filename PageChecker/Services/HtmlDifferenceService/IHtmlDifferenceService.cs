using PageCheckerAPI.Models;

namespace PageCheckerAPI.Services.HtmlDifferenceService
{
    public interface IHtmlDifferenceService
    {
        string GetDifference(string html1, string html2, CheckingTypeEnum checkingType);
        string GetPatches(string html1, string html2, CheckingTypeEnum checkingType);
        string ApplyPatches(string patches, string text, CheckingTypeEnum checkingType);
        string Prettyfy(string patches, string text, CheckingTypeEnum checkingType);
    }
}

