using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PageCheckerAPI.Services.Interfaces
{
    public interface IWebsiteService
    {
        string GetBody(string url);
    }
}
