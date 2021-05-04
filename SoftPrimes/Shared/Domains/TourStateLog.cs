using SoftPrimes.Shared.ModelInterfaces;
using System;

namespace SoftPrimes.Shared.Domains
{
    public class TourStateLog : IAuditableInsert
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public TourAgent Tour { get; set; }
        public TourState TourState { get; set; }
        public string CreatedBy { get ; set ; }
        public DateTimeOffset? CreatedOn { get ; set ; }
    }
}