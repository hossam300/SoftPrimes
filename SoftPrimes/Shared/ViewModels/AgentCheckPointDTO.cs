using SoftPrimes.Shared.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Shared.ViewModels
{
    public class AgentCheckPointDTO
    {
        public int Id { get; set; }
        public string CheckPointNameAr { get; set; }
        public string CheckPointNameEn { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string LocationText { get; set; }
        public double distanceToNextPoint { get; set; }
        public TourCheckPointState CheckPointState { get; set; }
        public string AgentId { get; set; }
        public AgentDetailsDTO Agent { get; set; }
    }
}
