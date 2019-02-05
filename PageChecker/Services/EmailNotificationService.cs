using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PageCheckerAPI.Services.Interfaces;

namespace PageCheckerAPI.Services
{
    public class EmailNotificationService : IEmailNotificationService
    {
        private SmtpClient client;
        private MailMessage message;
        private IConfiguration _config;

        private const string Email = "pagecheckersite@gmail.com";

        public EmailNotificationService(IConfiguration config)
        {
            _config = config;

            client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(Email, _config["Gmail:password"]);
        }

        public void SendEmailNotification(string emailTo, string subject,string content, bool isHtml = false)
        {
            try
            {
                message = new MailMessage(Email, emailTo);

                message.Body = content;
                message.Subject = subject;
                message.IsBodyHtml = isHtml;

                client.SendAsync(message, emailTo);
            }
            catch (FormatException)
            {
                //TODO: LOGS
            }
        }
    }
}
