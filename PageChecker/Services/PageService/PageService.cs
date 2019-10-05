﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PageCheckerAPI.DTOs.Page;
using PageCheckerAPI.Models;
using PageCheckerAPI.Repositories.Interfaces;

namespace PageCheckerAPI.Services.PageService
{
    public class PageService : IPageService
    {
        private readonly IGenericRepository<Page> _repo;
        private readonly IMapper _mapper;

        public PageService(IGenericRepository<Page> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PageDto> AddPage(AddPageDto pageDto)
        {
            var page = _mapper.Map<Page>(pageDto);

            if (await _repo.Add(page) != null)
            {
                return _mapper.Map<PageDto>(page);
            }

            return null;
        }

        public async Task DeletePage(DeletePageDto pageDto, Guid userId)
        {
            var pageToDelete = await _repo.FindBy(x => x.PageId.Equals(pageDto.PageId)).SingleAsync();

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