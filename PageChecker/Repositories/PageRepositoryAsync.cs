using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PageCheckerAPI.DataAccess;
using PageCheckerAPI.DTOs.Page;
using PageCheckerAPI.Models;
using PageCheckerAPI.Repositories.Interfaces;

namespace PageCheckerAPI.Repositories
{
    public class PageRepositoryAsync : IPageRepositoryAsync
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PageRepositoryAsync(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PageDto>> GetPages(Guid userId)
        {
            List<Page> pages = await _context.Pages.Where(x => x.UserId.Equals(userId)).ToListAsync();
            List<PageDto> pageDtos = _mapper.Map<List<Page>, List<PageDto>>(pages);

            return pageDtos;
        }

        public async Task<PageDto> GetPage(Guid pageId)
        {
            Page page = await _context.Pages.SingleOrDefaultAsync(x => x.PageId.Equals(pageId));
            PageDto pageDto = _mapper.Map<PageDto>(page);

            return pageDto;
        }

        public async Task<PageDto> AddPage(AddPageDto pageDto)
        {
            var page = _mapper.Map<Page>(pageDto);

            await _context.Pages.AddAsync(page);

            if (await _context.SaveChangesAsync() > 0)
                return _mapper.Map<PageDto>(page);

            return null;
        }

        public async Task DeletePage(DeletePageDto pageDto, Guid userId)
        {
            var pageToDelete = await _context.Pages.SingleAsync(p => p.PageId.Equals(pageDto.PageId));

            if (!pageToDelete.UserId.Equals(userId))
                return;

            _context.Pages.Remove(pageToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<PageDto> EditPage(PageDto pageDto)
        {
            var page = await _context.Pages.SingleOrDefaultAsync(x => x.PageId.Equals(pageDto.PageId));

            _mapper.Map<PageDto, Page>(pageDto, page);

            if (await _context.SaveChangesAsync() > 0)
            {
                pageDto = _mapper.Map<PageDto>(page);
                return pageDto;
            }

            return null;
        }
    }
}
