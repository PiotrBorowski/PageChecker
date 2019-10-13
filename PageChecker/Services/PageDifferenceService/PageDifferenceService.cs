using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PageCheckerAPI.DTOs.Difference;
using PageCheckerAPI.DTOs.Shared;
using PageCheckerAPI.Models;
using PageCheckerAPI.Repositories.Interfaces;

namespace PageCheckerAPI.Services.PageDifferenceService
{
    public class PageDifferenceService: IPageDifferenceService
    {
        private readonly IGenericRepository<Difference> _repo;
        private readonly IMapper _mapper;

        public PageDifferenceService(IGenericRepository<Difference> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public Task<List<DifferenceDto>> GetDifferences(Guid pageId)
        {
            throw new NotImplementedException();
        }

        public Task<DifferenceDto> DifferencePage(Guid differenceId)
        {
            throw new NotImplementedException();
        }

        public Task<DifferenceDto> AddDifference(AddDifferenceDto differenceDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteDifference(DeleteDto differenceDto)
        {
            throw new NotImplementedException();
        }
    }
}
