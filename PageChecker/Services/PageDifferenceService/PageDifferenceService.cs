using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PageCheckerAPI.DTOs.Difference;
using PageCheckerAPI.DTOs.Shared;
using PageCheckerAPI.Models;
using PageCheckerAPI.Repositories.Interfaces;
using PageCheckerAPI.Services.DateTimeService;

namespace PageCheckerAPI.Services.PageDifferenceService
{
    public class PageDifferenceService: IPageDifferenceService
    {
        private readonly IGenericRepository<Difference> _repo;
        private readonly IMapper _mapper;
        private IDateTimeService _dateTimeService;

        public PageDifferenceService(IGenericRepository<Difference> repo, IDateTimeService dateTimeService ,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _dateTimeService = dateTimeService;
        }

        public async Task<List<DifferenceInfoDto>> GetDifferencesInfo(Guid pageId)
        {
            var diffs = await _repo.FindBy(x => x.PageId.Equals(pageId)).ToListAsync();
            var diffsDtos = _mapper.Map<List<Difference>, List<DifferenceInfoDto>>(diffs);
            return diffsDtos;
        }

        public async Task<DifferenceDto> GetDifference(Guid differenceId)
        {
            var diff = await _repo.FindBy(x => x.DifferenceId.Equals(differenceId)).SingleAsync();
            var diffDto = _mapper.Map<DifferenceDto>(diff);
            return diffDto;
        }

        public async Task<DifferenceDto> AddDifference(AddDifferenceDto differenceDto)
        {
            var diff = _mapper.Map<Difference>(differenceDto);
            diff.Date = _dateTimeService.GetCurrentDateTime();

            if (await _repo.Add(diff) != null)
            {
                return _mapper.Map<DifferenceDto>(diff);
            }

            return null;
        }

        public async Task DeleteDifference(DeleteDto differenceDto)
        {
            var diffToDelete = await _repo.FindBy(x => x.DifferenceId.Equals(differenceDto.Id)).SingleAsync();

            await _repo.Delete(diffToDelete);
        }
    }
}
