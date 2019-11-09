using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using PageCheckerAPI.Helpers;
using PageCheckerAPI.Models;
using PageCheckerAPI.Services.HtmlDifferenceService.DifferenceServices;

namespace PageCheckerAPI.Services.HtmlDifferenceService.DifferenceServicesFactory
{
    public class DifferenceServicesFactory : IDifferenceServicesFactory
    {
        private readonly Idiff_match_patch _differ;

        public DifferenceServicesFactory(Idiff_match_patch differ)
        {
            _differ = differ;
        }

        public IDifferenceService Create(CheckingTypeEnum type)
        {
            switch (type)
            {
                case CheckingTypeEnum.Text:
                    return new TextDifferenceService(_differ);

                case CheckingTypeEnum.Full:
                case CheckingTypeEnum.Element:
                    return new FullDifferenceService(_differ);
                
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}
