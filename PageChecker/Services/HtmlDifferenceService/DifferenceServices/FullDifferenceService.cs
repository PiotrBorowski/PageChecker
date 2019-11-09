using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PageCheckerAPI.Helpers;

namespace PageCheckerAPI.Services.HtmlDifferenceService.DifferenceServices
{
    public class FullDifferenceService : IDifferenceService
    {
        private readonly Idiff_match_patch _differ;

        public FullDifferenceService(Idiff_match_patch differ)
        {
            _differ = differ;
        }

        public List<Diff> GetDifference(string html1, string html2)
        {
            var listOfDiffs = _differ.diff_main(html1, html2);
            _differ.diff_cleanupSemantic(listOfDiffs);

            return listOfDiffs;
        }

        public string GetPatchText(string text, List<Diff> diffs)
        {
            var patches = _differ.patch_make(text, diffs);
            return _differ.patch_toText(patches);
        }

        public string CreatePatch(string patchText, string text)
        {
            var patchs = _differ.patch_fromText(patchText);
            var result = _differ.patch_apply(patchs, text);

            return (string)result[0];
        }

        public string Prettyfy(List<Diff> diffs)
        {
            return _differ.diff_prettyHtml(diffs);
        }
    }
}
