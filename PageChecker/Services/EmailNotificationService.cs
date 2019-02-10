﻿using System;
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
        private readonly IConfiguration _config;

        public EmailNotificationService(IConfiguration config)
        {
            _config = config;

            client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_config["Gmail:email"], _config["Gmail:password"])
            };
        }

        public void SendEmailNotification(MailMessage message)
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
