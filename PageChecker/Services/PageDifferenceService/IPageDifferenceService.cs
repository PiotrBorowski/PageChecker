using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PageCheckerAPI.DTOs.Difference;
using PageCheckerAPI.DTOs.Page;
using PageCheckerAPI.DTOs.Shared;

namespace PageCheckerAPI.Services.PageDifferenceService
{
    public interface IPageDifferenceService
    {
        Task<List<DifferenceInfoDto>> GetDifferencesInfo(Guid pageId);
        Task<DifferenceDto> GetDifference(Guid differenceId);
        Task<DifferenceDto> AddDifference(AddDifferenceDto differenceDto);
        Task DeleteDifference(DeleteDto differenceDto);
    }
}
