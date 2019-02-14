using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _config;

        public PageBackgroundService(
            IWebsiteService websiteService, 
            IPageService pageService,  
            IEmailNotificationService emailNotification,
            IUserService userService,
            IHtmlDifferenceService differenceService,
            IConfiguration config)
        {
            _websiteService = websiteService;
            _pageService = pageService;
            _emailNotification = emailNotification;
            _userService = userService;
            _differenceService = differenceService;
            _config = config;
        }

        public void StartPageChangeChecking(PageDto pageDto)
        {
            RecurringJob.AddOrUpdate(pageDto.PageId.ToString() ,() => CheckChange(pageDto.PageId)
           , Cron.MinuteInterval(Convert.ToInt32(pageDto.RefreshRate.TotalMinutes)));
        }

        public async Task CheckChange(int pageId)
        {
            var pageDto = await _pageService.GetPage(pageId);

            try
            {
                string webBody = await _websiteService.GetHtml(pageDto.Url);

                //if page changed now
                if (HtmlHelper.Compare(pageDto.Body, webBody, pageDto.CheckingType) == false)
                {
                    pageDto.HasChanged = true;
                    pageDto.Stopped = true;

                    pageDto.BodyDifference =
                        _differenceService.GetDifference(pageDto.Body, webBody, pageDto.CheckingType);

                    await _pageService.EditPage(pageDto);
                  
                    //notification
                    var user = await _userService.GetUser(pageDto.UserId);
                    var message = new MailMessage(_config["Gmail:email"], user.Email, "PageChecker Notification",
                        $"Page named:{pageDto.Name}, URL: {pageDto.Url} has changed.");

                    _emailNotification.SendEmailNotification(message);
                    StopPageChangeChecking(pageId.ToString());
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
