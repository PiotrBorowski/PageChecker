using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.Extensions.Configuration;
using PageCheckerAPI.DTOs.Difference;
using PageCheckerAPI.DTOs.Page;
using PageCheckerAPI.DTOs.User;
using PageCheckerAPI.DTOs.WebsiteText;
using PageCheckerAPI.Helpers;
using PageCheckerAPI.Models;
using PageCheckerAPI.Repositories.Interfaces;
using PageCheckerAPI.Services.EmailService;
using PageCheckerAPI.Services.HtmlDifferenceService;
using PageCheckerAPI.Services.PageDifferenceService;
using PageCheckerAPI.Services.PageService;
using PageCheckerAPI.Services.UserService;
using PageCheckerAPI.Services.WebsiteService;
using PageCheckerAPI.Services.WebsiteTextService;

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
        private readonly IWebsiteTextService _websiteTextService;
        private readonly IPageDifferenceService _pageDifferenceService;

        public PageBackgroundService(
            IWebsiteService websiteService, 
            IWebsiteTextService websiteTextService,
            IPageService pageService,  
            IEmailService emailService,
            IUserService userService,
            IHtmlDifferenceService differenceService,
            IConfiguration config,
            IPageDifferenceService pageDifferenceService)
        {
            _websiteService = websiteService;
            _pageService = pageService;
            _emailService = emailService;
            _userService = userService;
            _differenceService = differenceService;
            _config = config;
            _websiteTextService = websiteTextService;
            _pageDifferenceService = pageDifferenceService;
        }

        public void StartPageChangeChecking(PageDto pageDto)
        {
            if (pageDto.RefreshRate == RefreshRateEnum.Day)
            {
                RecurringJob.AddOrUpdate(pageDto.PageId.ToString(), () => CheckChange(pageDto.PageId)
                    , Cron.DayInterval(1));
            }
            else
            {
                RecurringJob.AddOrUpdate(pageDto.PageId.ToString(), () => CheckChange(pageDto.PageId)
                    , Cron.MinuteInterval(Convert.ToInt32(pageDto.RefreshRate)));
            }
        }

        public async Task CheckChange(Guid pageId)
        {
            var pageDto = await _pageService.GetPage(pageId);

            try
            {
                string webBody = await _websiteService.GetHtml(pageDto.Url);
                var primaryText = await _websiteTextService.GetText(pageDto.PrimaryTextId);

                if (pageDto.CheckingType == CheckingTypeEnum.Element)
                {
                    var webElement = HtmlHelper.GetNode(webBody, pageDto.ElementXPath);

                    if (HtmlHelper.Compare(primaryText.Text, webElement, CheckingTypeEnum.Full) == false)
                    {
                       await PageChanged(pageDto, primaryText.Text, webElement);
                    }
                }

                //if page changed now
                if (HtmlHelper.Compare(primaryText.Text, webBody, pageDto.CheckingType) == false)
                {
                   await PageChanged(pageDto, primaryText.Text, webBody);
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

        private async Task PageChanged(PageDto pageDto, string primaryText, string webBody)
        {
            var text = _differenceService.GetDifference(primaryText, webBody, pageDto.CheckingType);

            await _pageDifferenceService.AddDifference(new AddDifferenceDto
            {
                Text = text,
                PageId = pageDto.PageId
            });

            pageDto.HasChanged = true;
            pageDto.Stopped = true;
            await _pageService.EditPage(pageDto);
       
            var user = await _userService.GetUser(pageDto.UserId);
            //notification               
            var message = CreateNotificationMessage(user, pageDto);
            _emailService.SendEmail(message);

            StopPageChangeChecking(pageDto.PageId.ToString());
        }
    }
}
