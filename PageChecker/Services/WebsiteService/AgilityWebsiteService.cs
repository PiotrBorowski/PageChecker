using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace PageCheckerAPI.Services.WebsiteService
{
    public class AgilityWebsiteService: IWebsiteService
    {
        public async Task<string> GetHtml(string url)
        {
            var website = new HtmlWeb();
            var html = await website.LoadFromWebAsync(url);
            return html.ParsedText;
        }
    }
}
