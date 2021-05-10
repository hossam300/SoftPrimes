using SoftPrimes.Shared.Domains;
using System;

namespace SoftPrimes.Shared.ViewModels
{
    public class TourStateLogDTO
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public TourAgentDTO Tour { get; set; }
        public TourState TourState { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
    }
}