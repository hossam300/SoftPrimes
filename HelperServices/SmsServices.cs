using IHelperServices;
using IHelperServices.Models;
using Microsoft.Extensions.Options;
using SoftPrimes.Shared.Domains;

namespace HelperServices
{
    public class SmsServices : _HelperService, ISmsServices
    {
     //   private SmsIntegrations SmsIntegration { get; set; }
        public SmsServices(IOptions<AppSettings> appSettings)
        {
          //  this.SmsIntegration = new SmsIntegrations(appSettings);
        }

        public void Send(string to, string[] bodyParameters , string TextMessage, string templateCode)
        {
          //  this.SmsIntegration.Send(to, bodyParameters, TextMessage, templateCode);
        }
    }
}