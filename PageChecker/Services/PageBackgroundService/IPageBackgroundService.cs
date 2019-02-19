using PageCheckerAPI.DTOs.Page;

namespace PageCheckerAPI.Services.PageBackgroundService
{
    public interface IPageBackgroundService
    {
        void StartPageChangeChecking(PageDto pageDto);
        void StopPageChangeChecking(string pageId);
    }
}
