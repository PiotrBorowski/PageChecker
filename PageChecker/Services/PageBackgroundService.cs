using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Hangfire;
using PageCheckerAPI.DTOs.Page;
using PageCheckerAPI.Repositories.Interfaces;
using PageCheckerAPI.Services.Interfaces;

namespace PageCheckerAPI.Services
{
    public class PageBackgroundService : IPageBackgroundService
    {
        private readonly IWebsiteService _websiteService;
        private readonly IPageRepository _repository;

        public PageBackgroundService(IWebsiteService websiteService, IPageRepository repository)
        {
            _websiteService = websiteService;
            _repository = repository;
        }

        public void StartPageChangeChecking(PageDto pageDto)
        {
            RecurringJob.AddOrUpdate(pageDto.PageId.ToString() ,() => CheckChange(pageDto)
           , Cron.MinuteInterval(Convert.ToInt32(pageDto.RefreshRate.TotalMinutes)));
        }

        public void CheckChange(PageDto pageDto)
        {
            //TODO: websiteservice ograniczenei tylko do body
            string webBody = _websiteService.GetBody(pageDto.Url);
            if (!string.Equals(pageDto.Body.Trim(), webBody.Trim()))
            {
                pageDto.HasChanged = true;
                _repository.EditPage(pageDto);
            }
        }

        public void StopPageChangeChecking(string pageId)
        {
            RecurringJob.RemoveIfExists(pageId);
        }
    }
}
