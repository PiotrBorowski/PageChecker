using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PageCheckerAPI.DataAccess;
using PageCheckerAPI.DTOs.Page;
using PageCheckerAPI.Models;
using PageCheckerAPI.Repositories.Interfaces;

namespace PageCheckerAPI.Repositories
{
    public class PageRepository : IPageRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PageRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool AddPage(AddPageDto pageDto)
        {
            var page = _mapper.Map<Page>(pageDto);
            
            _context.Pages.Add(page);

            return _context.SaveChanges() > 0;
        }

        public void DeletePage(DeletePageDto pageDto, int userId)
        {
            var pageToDelete = _context.Pages.Single(p => p.PageId == pageDto.PageId);

            if(pageToDelete.UserId != userId)
                return;

            _context.Pages.Remove(pageToDelete);
            _context.SaveChanges();
        }

        public List<PageDto> GetPages(int userId)
        {
            List<Page> pages = _context.Pages.Where(x => x.UserId == userId).ToList();
            List<PageDto> pageDtos = _mapper.Map<List<Page>, List<PageDto>> (pages);

            return pageDtos;
        }
    }
}
