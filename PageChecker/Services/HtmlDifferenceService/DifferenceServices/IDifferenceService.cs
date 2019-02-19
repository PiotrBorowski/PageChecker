using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PageCheckerAPI.Services.HtmlDifferenceService.DifferenceServices
{
    public interface IDifferenceService
    {
        string GetDifference(string html1, string html2);
    }
}
