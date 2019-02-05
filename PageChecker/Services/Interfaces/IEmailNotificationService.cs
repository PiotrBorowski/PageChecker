using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PageCheckerAPI.Services.Interfaces
{
    public interface IEmailNotificationService
    {
        void SendEmailNotification(string to, string subject, string content, bool isHtml = false);
    }
}
