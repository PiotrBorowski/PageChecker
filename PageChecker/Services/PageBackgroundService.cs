using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
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
        private readonly IPageService _pageService;
        private readonly IWebsiteComparer _websiteComparer;

        public PageBackgroundService(IWebsiteService websiteService, IPageService pageService, IWebsiteComparer websiteComparer)
        {
            _websiteService = websiteService;
            _pageService = pageService;
            _websiteComparer = websiteComparer;
        }

        public void StartPageChangeChecking(PageDto pageDto)
        {
            RecurringJob.AddOrUpdate(pageDto.PageId.ToString() ,() => CheckChange(pageDto.PageId)
           , Cron.MinuteInterval(Convert.ToInt32(pageDto.RefreshRate.TotalMinutes)));
        }

        public void CheckChange(int pageId)
        {
            var pageDto = _pageService.GetPage(pageId);

            try
            {
                string webBody = _websiteService.GetHtml(pageDto.Url);

                if (_websiteComparer.Compare(pageDto.Body, webBody, pageDto.CheckingType) == false)
                {
                    pageDto.HasChanged = true;
                    _pageService.EditPage(pageDto);
                }
            }
            catch (WebException)
            {
                
            }

        }

        public void StopPageChangeChecking(string pageId)
        {
            RecurringJob.RemoveIfExists(pageId);
        }
    }
}
