using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Shared.ViewModels
{
    public class AgentLoginLogDTO
    {
        public int Id { get; set; }
        public string AccessToken { get; set; }
        public string MacId { get; set; }
        public string MobileType { get; set; }
        public string Network { get; set; }
        public string SerailNumber { get; set; }
        public string AgentId { get; set; }
        public AgentDTO Agent { get; set; }

        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
    }
}
