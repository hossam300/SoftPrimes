using SoftPrimes.Shared.ModelInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Shared.Domains
{
    public class AgentLocationLog : IAuditableInsert
    {
        public int Id { get; set; }
        public string AgentId { get; set; }
        public virtual Agent Agent { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
    }
}
