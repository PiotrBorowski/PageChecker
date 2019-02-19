using System.Threading.Tasks;

namespace PageCheckerAPI.Services.WebsiteService
{
    public interface IWebsiteService
    {
        Task<string> GetHtml(string url);
    }
}
