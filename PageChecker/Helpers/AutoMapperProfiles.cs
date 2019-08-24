using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PageCheckerAPI.DTOs.Page;
using PageCheckerAPI.DTOs.User;
using PageCheckerAPI.DTOs.WebsiteText;
using PageCheckerAPI.Models;
using PageCheckerAPI.ViewModels.Page;

namespace PageCheckerAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Page, PageDto>();
            CreateMap<PageDto, Page>();
            CreateMap<AddPageDto, Page>();
            CreateMap<PageDto, PageViewModel>();
            CreateMap<User, UserClaimsDto>();
            CreateMap<EditUserDto, User>();
            CreateMap<User, EditUserDto>();
            CreateMap<AddWebsiteTextDto, WebsiteText>();
            CreateMap<WebsiteText, WebsiteTextDto>();
        }
    }
}
