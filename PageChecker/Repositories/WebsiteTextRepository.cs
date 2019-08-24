using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PageCheckerAPI.DataAccess;
using PageCheckerAPI.DTOs.WebsiteText;
using PageCheckerAPI.Models;
using PageCheckerAPI.Repositories.Interfaces;

namespace PageCheckerAPI.Repositories
{
    public class WebsiteTextRepository: IWebsiteTextRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public WebsiteTextRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
        }

        public async Task<WebsiteText> GetWebsiteText(Guid websiteTextId)
        {
            var websiteText =
                await _context.WebsiteTexts.Where(x => x.WebsiteTextId.Equals(websiteTextId)).SingleAsync();

            return websiteText;
        }

        public async Task<WebsiteText> AddWebsiteText(AddWebsiteTextDto websiteTextDto)
        {
            var websiteText = _mapper.Map<WebsiteText>(websiteTextDto);

            await _context.WebsiteTexts.AddAsync(websiteText);

            if (await _context.SaveChangesAsync() > 0)
                return websiteText;

            return null;
        }
    }
}
