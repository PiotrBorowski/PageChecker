using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Hangfire;
using PageCheckerAPI.DTOs.Page;
using PageCheckerAPI.Helpers;
using PageCheckerAPI.Models;
using PageCheckerAPI.Repositories.Interfaces;
using PageCheckerAPI.Services.Interfaces;

namespace PageCheckerAPI.Services
{
    public class PageBackgroundService : IPageBackgroundService
    {
        private readonly IWebsiteService _websiteService;
        private readonly IPageService _pageService;
        private readonly IEmailNotificationService _emailNotification;
        private readonly IUserService _userService;
        private readonly IHtmlDifferenceService _differenceService; 
        public PageBackgroundService(
            IWebsiteService websiteService, 
            IPageService pageService,  
            IEmailNotificationService emailNotification,
            IUserService userService,
            IHtmlDifferenceService differenceService)
        {
            _websiteService = websiteService;
            _pageService = pageService;
            _emailNotification = emailNotification;
            _userService = userService;
            _differenceService = differenceService;
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

                if (HtmlHelper.Compare(pageDto.Body, webBody, pageDto.CheckingType) == false)
                {
                    pageDto.HasChanged = true;

                    pageDto.BodyDifference =
                        _differenceService.GetDifference(pageDto.Body, webBody, pageDto.CheckingType);

                    _pageService.EditPage(pageDto);
                  
                    //notification
                    var user = _userService.GetUser(pageDto.UserId);
                    if (user.Email == string.Empty)
                    {
                        _emailNotification.SendEmailNotification(user.UserName,
                            $"Page named:{pageDto.Name}, URL: {pageDto.Url} has changed.");
                    }
                }
            }
            catch (WebException)
            {
                //TODO: LOGS
            }

        }

        public void StopPageChangeChecking(string pageId)
        {
            RecurringJob.RemoveIfExists(pageId);
        }
    }
}
