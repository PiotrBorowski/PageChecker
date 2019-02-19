using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PageCheckerAPI.Models;
using PageCheckerAPI.Services.HtmlDifferenceService.DifferenceServices;

namespace PageCheckerAPI.Services.HtmlDifferenceService.DifferenceServicesFactory
{
    public interface IDifferenceServicesFactory
    {
        IDifferenceService Create(CheckingTypeEnum type);
    }
}
