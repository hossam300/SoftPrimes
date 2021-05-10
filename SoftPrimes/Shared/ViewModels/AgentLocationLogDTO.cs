using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Shared.ViewModels
{
    public class AgentLocationLogDTO
    {
        public int Id { get; set; }
        public string AgentId { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
    }
}
