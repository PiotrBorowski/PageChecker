using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using CronEspresso.NETCore;
using Cronos;
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
            string cron;
            if (pageDto.RefreshRate == RefreshRateEnum.Day)
            {
                cron = Cron.Daily(DateTime.Now.TimeOfDay.Hours);
            }
            else if(pageDto.RefreshRate == RefreshRateEnum.FifteenMinutes || pageDto.RefreshRate == RefreshRateEnum.HalfHour)
            {
                cron = Cron.MinuteInterval((int)pageDto.RefreshRate);
            }
            else
            {
                var hours = TimeSpan.FromMinutes((int) pageDto.RefreshRate)
                    .Hours;
                cron = Cron.HourInterval(hours);
            }
            RecurringJob.AddOrUpdate(pageDto.PageId.ToString(), () => CheckChange(pageDto.PageId)
                , cron );
            
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

        public void Trigger(string pageId)
        {
            RecurringJob.Trigger(pageId);
        }

        private MailMessage CreateNotificationMessage(UserClaimsDto user, PageDto pageDto)
        {
            return new MailMessage(_config["Gmail:email"], user.Email, "PageChecker Notification",
                $"Page named:{pageDto.Name}, URL: {pageDto.Url} has changed.");
        }

        private async Task PageChanged(PageDto pageDto, string primaryText, string webBody)
        {
            string text = pageDto.HighAccuracy ? 
                _differenceService.GetDifference(primaryText, webBody, pageDto.CheckingType) : 
                _differenceService.GetPatches(primaryText, webBody, pageDto.CheckingType);

            await _pageDifferenceService.AddDifference(new AddDifferenceDto
            {
                Text = text,
                PageId = pageDto.PageId
            });

            await _websiteTextService.EditText(pageDto.PrimaryTextId, webBody);

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
