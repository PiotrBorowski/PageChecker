using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PageCheckerAPI.DTOs.Page;

namespace PageCheckerAPI.Repositories.Interfaces
{
    public interface IPageRepository
    {
        //TODO: Async
        List<PageDto> GetPages(int userId);
        bool AddPage(AddPageDto pageDto);
        void DeletePage(DeletePageDto pageDto, int userId);
    }
}
