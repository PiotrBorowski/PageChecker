using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PageCheckerAPI.DTOs.Page;
using PageCheckerAPI.Repositories.Interfaces;

namespace PageCheckerAPI.Services.PageService
{
    public class PageService : IPageService
    {
        private readonly IPageRepositoryAsync _repo;

        public PageService(IPageRepositoryAsync repo)
        {
            _repo = repo;
        }

        public async Task<PageDto> AddPage(AddPageDto pageDto)
        {
           return await _repo.AddPage(pageDto);
        }

        public async Task DeletePage(DeletePageDto pageDto, int userId)
        {
            await _repo.DeletePage(pageDto, userId);
        }

        public async Task<List<PageDto>> GetPages(int userId)
        {
            return await _repo.GetPages(userId);
        }

        public async Task<PageDto> GetPage(int pageId)
        {
            return await _repo.GetPage(pageId);
        }

        public async Task<PageDto> EditPage(PageDto pageDto)
        {
            return await _repo.EditPage(pageDto);
        }
    }
}
