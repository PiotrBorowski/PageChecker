using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PageCheckerAPI.DTOs.Page;

namespace PageCheckerAPI.Repositories.Interfaces
{
    public interface IPageRepository
    {
        List<PageDto> GetPages();
        bool AddPage(PageDto pageDto);
    }
}
