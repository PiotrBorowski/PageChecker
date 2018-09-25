using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Hangfire;
using PageCheckerAPI.DTOs.Page;
using PageCheckerAPI.Helpers;
using PageCheckerAPI.Repositories.Interfaces;
using PageCheckerAPI.Services.Interfaces;

namespace PageCheckerAPI.Services
{
    public class PageBackgroundService : IPageBackgroundService
    {
        private readonly IWebsiteService _websiteService;
        private readonly IPageRepository _repository;
        private readonly IWebsiteComparer _websiteComparer;

        public PageBackgroundService(IWebsiteService websiteService, IPageRepository repository, IWebsiteComparer websiteComparer)
        {
            _websiteService = websiteService;
            _repository = repository;
            _websiteComparer = websiteComparer;
        }

        public void StartPageChangeChecking(PageDto pageDto)
        {
            RecurringJob.AddOrUpdate(pageDto.PageId.ToString() ,() => CheckChange(pageDto.PageId)
           , Cron.MinuteInterval(Convert.ToInt32(pageDto.RefreshRate.TotalMinutes)));
        }

        public void CheckChange(int pageId)
        {
            //TODO: websiteservice ograniczenei tylko do body
            var pageDto = _repository.GetPage(pageId);
            string webBody = _websiteService.GetHtml(pageDto.Url);

            if (_websiteComparer.Compare(pageDto.Body, webBody, pageDto.CheckingType) == false)
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
