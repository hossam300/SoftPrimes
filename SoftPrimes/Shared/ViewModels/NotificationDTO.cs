using SoftPrimes.Shared.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Shared.ViewModels
{
  public  class NotificationDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public NotificationType NotificationType { get; set; }
        public string ToAgentId { get; set; }
        public AgentDTO ToAgent { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
    }
}
