using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;


namespace PageCheckerAPI.Services.EmailNotificationService
{
    public interface IEmailNotificationService
    {
        void SendEmailNotification(MailMessage message);
    }
}
