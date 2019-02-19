using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;


namespace PageCheckerAPI.Services.EmailService
{
    public interface IEmailService
    {
        void SendEmail(MailMessage message);
    }
}
