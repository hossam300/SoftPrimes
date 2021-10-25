using SoftPrimes.Shared.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Shared.ViewModels
{
    public class HomeTourDTO
    {
        public int TourId { get; set; }
        public string TourNameAr { get; set; }
        public string TourNameEn { get; set; }
        public string TourType { get; set; }
        public int TourTypeId { get; set; }
        public DateTime TourDate { get; set; }
        public float EstimateDistance { get; set; }
        public string LocationAr { get; set; }
        public string LocationEn { get; set; }
        public string TimeDuration { get; set; }
        public int CountOfLocations { get; set; }
        public string AgentId { get; set; }
        public AgentDTO Agent { get; set; }
        public TourState TourState { get; set; }
    }
}
