using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AspR14.BLL.Common
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("leenasba02@gmail.com", "ftqe senk bbfr kvby")    
        };
        return client.SendMailAsync(
        new MailMessage(
            from:"leenasba02@gmail.com",
            to : email,
            subject,
            message)
            {IsBodyHtml = true});    
        }
    }
}