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
using PageCheckerAPI.DTOs.User;
using PageCheckerAPI.Helpers;
using PageCheckerAPI.Services.EmailService;
using PageCheckerAPI.Services.HtmlDifferenceService;
using PageCheckerAPI.Services.PageService;
using PageCheckerAPI.Services.UserService;
using PageCheckerAPI.Services.WebsiteService;

namespace PageCheckerAPI.Services.PageBackgroundService
{
    public class PageBackgroundService : IPageBackgroundService
    {
        private readonly IWebsiteService _websiteService;
        private readonly IPageService _pageService;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;
        private readonly IHtmlDifferenceService _differenceService;
        private readonly IConfiguration _config;

        public PageBackgroundService(
            IWebsiteService websiteService, 
            IPageService pageService,  
            IEmailService emailService,
            IUserService userService,
            IHtmlDifferenceService differenceService,
            IConfiguration config)
        {
            _websiteService = websiteService;
            _pageService = pageService;
            _emailService = emailService;
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
                  
                    var user = await _userService.GetUser(pageDto.UserId);
                    //notification               
                    var message = CreateNotificationMessage(user, pageDto);
                    _emailService.SendEmail(message);

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

        private MailMessage CreateNotificationMessage(UserClaimsDto user, PageDto pageDto)
        {
            return new MailMessage(_config["Gmail:email"], user.Email, "PageChecker Notification",
                $"Page named:{pageDto.Name}, URL: {pageDto.Url} has changed.");
        }
    }
}
