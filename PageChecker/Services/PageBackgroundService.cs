using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Hangfire;
using PageCheckerAPI.DTOs.Page;
using PageCheckerAPI.Services.Interfaces;

namespace PageCheckerAPI.Services
{
    public class PageBackgroundService : IPageBackgroundService
    {
        public void StartPageChangeChecking(PageDto pageDto)
        {
            RecurringJob.AddOrUpdate(pageDto.PageId.ToString() ,() => Action(pageDto)
           , Cron.MinuteInterval(Convert.ToInt32(pageDto.RefreshSpan.TotalMinutes)));
        }

        public void Action(PageDto pageDto)
        {
            Console.WriteLine(pageDto.PageId);
            Console.WriteLine(pageDto.Url);
        }

        public void StopPageChangeChecking(string pageId)
        {
            RecurringJob.RemoveIfExists(pageId);
        }
    }
}
