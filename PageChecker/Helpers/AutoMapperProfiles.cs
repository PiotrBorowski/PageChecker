using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PageCheckerAPI.DTOs.Page;
using PageCheckerAPI.DTOs.User;
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
        }
    }
}
