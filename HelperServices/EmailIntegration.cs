using IHelperServices.Models;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Xml.Xsl;
using System.Xml;
using System.Text;
using System.Globalization;

namespace EMailIntegration
{
    public class EmailIntegration
    {
        //private MainDbContext _DbContext = new MainDbContext();

        public AppSettings _AppSettings;
        private string SMTPServer { get; set; }
        private string AuthEmailFrom { get; set; }
        private string AuthDomain { get; set; }
        public string EmailFrom { get; set; }
        public int EmailPort { get; set; }
        public string EmailPassword { get; set; }
        public bool EnableSSL { get; set; }
        public string x_uqu_auth { get; set; }
        public string SenderAPIKey { get; set; }
        public string EmailPostActivate { get; set; }
        public string EmailXmlPath { get; set; }
        public string TransNumber { get; set; }
        public string SendEmailDelegationWithURL { get; set; }

        public string NotificationXsltPath { get; set; }
        private readonly string angular_root = "";
        public EmailIntegration(IOptions<AppSettings> appSettings)
        {
            this.angular_root = angular_root;
            _AppSettings = new AppSettings();
            _AppSettings = appSettings.Value;
            this.SMTPServer = this._AppSettings.EmailSetting.SMTPServer;
            this.AuthDomain = this._AppSettings.EmailSetting.AuthDomain;
            this.AuthEmailFrom = this._AppSettings.EmailSetting.AuthEmailFrom;
            this.EmailFrom = this._AppSettings.EmailSetting.EmailFrom;
            this.EmailPort = this._AppSettings.EmailSetting.EmailPort;
            this.EmailPassword = this._AppSettings.EmailSetting.EmailPassword;
            this.EnableSSL = this._AppSettings.EmailSetting.EnableSSL;
            this.x_uqu_auth = this._AppSettings.EmailSetting.x_uqu_auth;
            this.SenderAPIKey = this._AppSettings.EmailSetting.SenderAPIKey;
            this.EmailPostActivate = this._AppSettings.EmailSetting.EmailPostActivate;
            this.SendEmailDelegationWithURL = this._AppSettings.EmailSetting.SendEmailDelegationWithURL;
        }

        public void Send(string sender, string[] to, string[] cc, string[] bcc, string title, string body)
        {
            //throw new NotImplementedException();
        }

        public static string RemoveSpecialChars(string str)
        {
            // Create  a string array and add the special characters you want to remove
            //Replace('\r', ' ').Replace('\n', ' ');
            string[] chars = new string[] { ",", ".", "/", "!", "@", "#", "$", "%", "^", "&", "*", "'", "\"", ";", "_", "(", ")", ":", "|", "[", "]", "\r", "\n" };
            //Iterate the number of times based on the String array length.
            for (int i = 0; i < chars.Length; i++)
            {
                if (str.Contains(chars[i]))
                {
                    str = str.Replace(chars[i], " ");
                }
            }
            return str;
        }

        public void SendNotificationEmail(string email, string subject, string Body, bool htmlEnabled, AlternateView htmlView, string CC_Email, string angularRootPath)
        {
            this.SendEmailDefault(email, RemoveSpecialChars(subject), Body, htmlEnabled, htmlView, CC_Email);
        }

        public void Test(string email, string subject, string message, bool htmlEnabled)
        {
            string usserBedawy = "bedotest1993@gmail.com";
            SmtpClient EmailSettings = new SmtpClient();

            EmailSettings.Host = SMTPServer;
            EmailSettings.Port = EmailPort;
            EmailSettings.UseDefaultCredentials = false;
            EmailSettings.Credentials = new NetworkCredential(EmailFrom, EmailPassword);
            EmailSettings.EnableSsl = EnableSSL;
            EmailSettings.DeliveryMethod = SmtpDeliveryMethod.Network;
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(EmailFrom);
            mailMessage.To.Add(usserBedawy);
            mailMessage.IsBodyHtml = htmlEnabled;
            mailMessage.Body = message;
            mailMessage.Subject = subject;
            try
            {
                EmailSettings.Send(mailMessage);

            }
            catch (Exception ex)
            {
                //string exe = ex.Message;
                //throw;
            }

        }
        private void SendEmailDefault(string email, string subject, string Body, bool htmlEnabled, AlternateView htmlView, string CC_Email, string Attachments = "")
        {
            try
            {

                SmtpClient EmailSettings = new SmtpClient();

                EmailSettings.Host = SMTPServer;
                EmailSettings.Port = EmailPort;
                EmailSettings.UseDefaultCredentials = false;

                // exchange server have to put "AuthEmailFrom" and "AuthDomain" setting value on appsettings
                if (!string.IsNullOrEmpty(this.AuthDomain))
                    EmailSettings.Credentials = new NetworkCredential(AuthEmailFrom, EmailPassword, AuthDomain);
                else if (!string.IsNullOrEmpty(this.AuthEmailFrom))
                    EmailSettings.Credentials = new NetworkCredential(AuthEmailFrom, EmailPassword);
                else
                    EmailSettings.Credentials = new NetworkCredential(EmailFrom, EmailPassword);

                EmailSettings.EnableSsl = EnableSSL;
                EmailSettings.DeliveryMethod = SmtpDeliveryMethod.Network;
                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(EmailFrom)
                };
                // mailMessage.To.Add(email);
                mailMessage.To.Add(new MailAddress(email));
                mailMessage.IsBodyHtml = htmlEnabled;
                mailMessage.Subject = subject;
                if (!string.IsNullOrEmpty(CC_Email))
                    mailMessage.CC.Add(CC_Email);

                if (htmlEnabled)
                {
                    mailMessage.AlternateViews.Add(htmlView);
                }
                if (!String.IsNullOrEmpty(Body))
                {
                    mailMessage.Body = Body;
                }
                if (Attachments != "" && Attachments != null || Attachments != string.Empty)
                {
                    foreach (var item in Attachments.Split(","))
                    {
                        mailMessage.Attachments.Add(new System.Net.Mail.Attachment(item));
                    }

                }
                EmailSettings.Send(mailMessage);

            }
            catch (Exception ex)
            {
            }
        }
    }
}
