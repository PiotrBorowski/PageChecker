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

        public bool AddPage(AddPageDto pageDto)
        {
           return _repo.AddPage(pageDto);
        }

        public void DeletePage(DeletePageDto pageDto)
        {
            _repo.DeletePage(pageDto);
        }

        public List<PageDto> GetPages()
        {
            return _repo.GetPages();
        }
    }
}
