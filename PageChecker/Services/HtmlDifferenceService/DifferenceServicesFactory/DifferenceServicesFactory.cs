using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using PageCheckerAPI.Models;
using PageCheckerAPI.Services.HtmlDifferenceService.DifferenceServices;

namespace PageCheckerAPI.Services.HtmlDifferenceService.DifferenceServicesFactory
{
    public class DifferenceServicesFactory : IDifferenceServicesFactory
    {
        public IDifferenceService Create(CheckingTypeEnum type)
        {
            switch (type)
            {
                case CheckingTypeEnum.Text:
                    return new TextDifferenceService();

                case CheckingTypeEnum.Full:
                    return new FullDifferenceService();
                
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}
