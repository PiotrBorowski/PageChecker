using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PageCheckerAPI.DTOs.WebsiteText;
using PageCheckerAPI.Models;

namespace PageCheckerAPI.Repositories.Interfaces
{
    interface IWebsiteTextRepository
    {
        Task<WebsiteText> GetWebsiteText(Guid websiteTextId); 
        Task<WebsiteText> AddWebsiteText(AddWebsiteTextDto websiteTextDto);
    }
}
