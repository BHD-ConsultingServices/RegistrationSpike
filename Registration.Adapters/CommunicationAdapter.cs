using Registration.Adapters.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Registration.Contracts;
using System.Net.Configuration;
using System.Configuration;
using System.Net.Mail;
using Registration.Adapters.Properties;

namespace Registration.Adapters
{
    public class CommunicationAdapter : ICommunicationAdapter
    {
        public ResultCode SendEmail(string toAddress, string subject, string body)
        {
            var server = "127.0.0.1";// Settings.Default.smtpServer;
            var port = 25; // Settings.Default.smptPort;

            var client = new SmtpClient(server, port);
            var mail = new MailMessage("admin@colorEvents.co.za", toAddress, subject, body);

            mail.Subject = subject;
            mail.Body = body;

            client.Send(mail);

            return ResultCode.Success;
        }
    }
}
