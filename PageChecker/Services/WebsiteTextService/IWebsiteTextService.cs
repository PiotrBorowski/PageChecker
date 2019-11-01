using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PageCheckerAPI.DTOs.WebsiteText;

namespace PageCheckerAPI.Services.WebsiteTextService
{
    public interface IWebsiteTextService
    {
        Task<WebsiteTextDto> AddText(AddWebsiteTextDto textDto);
        Task<WebsiteTextDto> GetText(Guid textId);
        Task EditText(Guid guid, string text);
    }
}
