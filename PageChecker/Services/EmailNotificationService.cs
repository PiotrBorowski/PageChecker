using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using PageCheckerAPI.Services.Interfaces;

namespace PageCheckerAPI.Services
{
    public class EmailNotificationService : IEmailNotificationService
    {
        private SmtpClient client;
        private MailMessage message;

        private const string Email = "pagecheckersite@gmail.com";

        public EmailNotificationService()
        {
            client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(Email, "pageadmin123!");
        }

        public void SendEmailNotification(string emailTo, string content)
        {
            try
            {
                message = new MailMessage(Email, emailTo);

                message.Body = content;
                message.Subject = "PageChecker Notification";

                client.Send(message);
            }
            catch (FormatException)
            {
                //TODO: LOGS
            }
        }
    }
}
