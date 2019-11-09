using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PageCheckerAPI.DTOs.Page;
using PageCheckerAPI.DTOs.Shared;
using PageCheckerAPI.Models;
using PageCheckerAPI.Repositories.Interfaces;
using PageCheckerAPI.Services.DateTimeService;

namespace PageCheckerAPI.Services.PageService
{
    public class PageService : IPageService
    {
        private readonly IGenericRepository<Page> _repo;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _dateTimeService;

        public PageService(IGenericRepository<Page> repo, IDateTimeService dateTimeService ,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _dateTimeService = dateTimeService;
        }

        public async Task<PageDto> AddPage(AddPageDto pageDto)
        {
            var page = _mapper.Map<Page>(pageDto);
            page.CreationDate = _dateTimeService.GetCurrentDateTime();
            if (await _repo.Add(page) != null)
            {
                return _mapper.Map<PageDto>(page);
            }

            return null;
        }

        public async Task DeletePage(DeleteDto pageDto, Guid userId)
        {
            var pageToDelete = await _repo.FindBy(x => x.PageId.Equals(pageDto.Id)).SingleAsync();

            if (!pageToDelete.UserId.Equals(userId))
                return;

            await _repo.Delete(pageToDelete);
        }

        public async Task<List<PageDto>> GetPages(Guid userId)
        {
            var pages = await _repo.FindBy(x => x.UserId == userId).ToListAsync();
            var pageDtos = _mapper.Map<List<Page>, List <PageDto>> (pages);

            return pageDtos;
        }

        public async Task<PageDto> GetPage(Guid pageId)
        {
            var page = await _repo.FindBy(x => x.PageId == pageId).SingleAsync();
            var pageDto = _mapper.Map<PageDto>(page);

            return pageDto;
        }

        public async Task<PageDto> EditPage(PageDto pageDto)
        {
            var page = _mapper.Map<Page>(pageDto);
            if (await _repo.Edit(page))
                return pageDto;

            return null;
        }
    }
}
