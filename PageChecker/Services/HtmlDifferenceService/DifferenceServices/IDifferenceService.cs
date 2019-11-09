using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PageCheckerAPI.Helpers;

namespace PageCheckerAPI.Services.HtmlDifferenceService.DifferenceServices
{
    public interface IDifferenceService
    {
        List<Diff> GetDifference(string html1, string html2);
        string GetPatchText(string text, List<Diff> diffs);
        string CreatePatch(string patchText, string text);
        string Prettyfy(List<Diff> diffs);
    }
}
