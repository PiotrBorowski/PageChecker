using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PageCheckerAPI.DTOs.Page;

namespace PageCheckerAPI.Services.Interfaces
{
    public interface IPageBackgroundService
    {
        void StartPageChangeChecking(PageDto pageDto);
        void StopPageChangeChecking(string pageId);
    }
}
