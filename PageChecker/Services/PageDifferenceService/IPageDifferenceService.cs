using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PageCheckerAPI.DTOs.Difference;
using PageCheckerAPI.DTOs.Page;
using PageCheckerAPI.DTOs.Shared;

namespace PageCheckerAPI.Services.PageDifferenceService
{
    interface IPageDifferenceService
    {
        Task<List<DifferenceDto>> GetDifferences(Guid pageId);
        Task<DifferenceDto> DifferencePage(Guid differenceId);
        Task<DifferenceDto> AddDifference(AddDifferenceDto differenceDto);
        Task DeleteDifference(DeleteDto differenceDto);
    }
}
