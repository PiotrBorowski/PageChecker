using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PageCheckerAPI.DTOs.WebsiteText;
using PageCheckerAPI.Models;
using PageCheckerAPI.Repositories.Interfaces;

namespace PageCheckerAPI.Services.WebsiteTextService
{
    public class WebsiteTextService: IWebsiteTextService
    {
        private readonly IGenericRepository<WebsiteText> _repo;
        private readonly IMapper _mapper;

        public WebsiteTextService(IGenericRepository<WebsiteText> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<WebsiteTextDto> AddText(AddWebsiteTextDto textDto)
        {
            var text =_mapper.Map<WebsiteText>(textDto);
            text = await _repo.Add(text);

            return _mapper.Map<WebsiteTextDto>(text);
        }

        public async Task<WebsiteTextDto> GetText(Guid textId)
        {
            var text = await _repo.FindBy(x => x.WebsiteTextId == textId).SingleAsync();
            return _mapper.Map<WebsiteTextDto>(text);
        }

        public async Task EditText(Guid guid, string text)
        {
            await _repo.Edit(new WebsiteText
                {
                    WebsiteTextId = guid,
                    Text = text
                }
            );
        }
    }
}
