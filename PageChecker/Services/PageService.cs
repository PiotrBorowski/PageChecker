using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PageCheckerAPI.DTOs.Page;
using PageCheckerAPI.Repositories.Interfaces;
using PageCheckerAPI.Services.Interfaces;

namespace PageCheckerAPI.Services
{
    public class PageService : IPageService
    {
        private readonly IPageRepository _repo;

        public PageService(IPageRepository repo)
        {
            _repo = repo;
        }

        public PageDto AddPage(AddPageDto pageDto)
        {
           return _repo.AddPage(pageDto);
        }

        public void DeletePage(DeletePageDto pageDto, int userId)
        {
            _repo.DeletePage(pageDto, userId);
        }

        public List<PageDto> GetPages(int userId)
        {
            return _repo.GetPages(userId);
        }

        public PageDto GetPage(int pageId)
        {
            return _repo.GetPage(pageId);
        }
    }
}
