using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace PageCheckerAPI.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient client;
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;

            client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_config["Gmail:email"], _config["Gmail:password"])
            };
        }

        public void SendEmail(MailMessage message)
        {
            try
            {
                client.SendAsync(message, message.To);
            }
            catch (FormatException)
            {
                //TODO: LOGS
            }
        }
    }
}
