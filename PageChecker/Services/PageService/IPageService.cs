using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PageCheckerAPI.DTOs.Page;

namespace PageCheckerAPI.Services.PageService
{
    public interface IPageService
    {
        Task<List<PageDto>> GetPages(Guid userId);
        Task<PageDto> GetPage(Guid pageId);
        Task<PageDto> AddPage(AddPageDto pageDto);
        Task DeletePage(DeletePageDto pageDto, Guid userId);
        Task<PageDto> EditPage(PageDto pageDto);
    }
}
