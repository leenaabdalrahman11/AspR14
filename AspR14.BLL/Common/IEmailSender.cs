using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspR14.BLL.Common
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string to, string subject, string body);
        
    }
}