using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PageCheckerAPI.DTOs.Page;

namespace PageCheckerAPI.Services.Interfaces
{
    public interface IPageService
    {
        List<PageDto> GetPages(int userId);
        PageDto GetPage(int pageId);
        PageDto AddPage(AddPageDto pageDto);
        void DeletePage(DeletePageDto pageDto, int userId);
        PageDto EditPage(PageDto pageDto);
    }
}
