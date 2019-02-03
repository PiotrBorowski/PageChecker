using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PageCheckerAPI.DTOs.Page;

namespace PageCheckerAPI.Repositories.Interfaces
{
    public interface IPageRepositoryAsync
    {
        Task<List<PageDto>> GetPages(int userId);
        Task<PageDto> GetPage(int pageId);
        Task<PageDto> AddPage(AddPageDto pageDto);
        Task DeletePage(DeletePageDto pageDto, int userId);
        Task<PageDto> EditPage(PageDto pageDto);
    }
}
