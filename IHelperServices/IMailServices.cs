using System.Net.Mail;


namespace IHelperServices
{
    public interface IMailServices : _IHelperService
    {
        void Send(string sender, string[] to, string[] cc, string[] bcc, string title, string body);
        void Test(string email, string subject, string message, bool htmlEnabled);
        void SendNotificationEmail(string email, string subject, string Body, bool htmlEnabled, AlternateView htmlView, string CC_Email, string angularRootPath);
    }
}