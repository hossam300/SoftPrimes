
using IHelperServices;
using IHelperServices.Models;
using Microsoft.Extensions.Options;
using System.Net.Mail;

using System.IO;
using EMailIntegration;

namespace HelperServices
{
    public class MailServices : _HelperService, IMailServices
    {
        private EmailIntegration EmailIntegration { get; set; }

        public MailServices(IOptions<AppSettings> appSettings)
        {
            this.EmailIntegration = new EmailIntegration(appSettings);
        }

        public void Send(string sender, string[] to, string[] cc, string[] bcc, string title, string body)
        {
            throw new System.NotImplementedException();
        }
        public void SendNotificationEmail(string email, string subject, string Body, bool htmlEnabled, AlternateView htmlView, string CC_Email, string angularRootPath)
        {
            this.EmailIntegration.SendNotificationEmail(email, subject, Body, htmlEnabled, htmlView, CC_Email, angularRootPath);
        }
        public void Test(string email, string subject, string message, bool htmlEnabled)
        {
            throw new System.NotImplementedException();
        }
    }


}