using SoftPrimes.Shared.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Shared.ViewModels
{
    public class AgentTourCheckPointDTO
    {
        public string  AgentId { get; set; }
        public List<TourCheckPoint> TourCheckPoints { get; set; }
    }
}
