using SoftPrimes.Shared.ModelInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Shared.Domains
{
    public class Notification : IAuditableInsert
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public NotificationType NotificationType { get; set; }
        public string ToAgentId { get; set; }
        public virtual Agent ToAgent { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public bool IsReaded { get; set; }
    }
    public enum NotificationType
    {
        NewTour = 1,
        Alert = 2,
    }
}
