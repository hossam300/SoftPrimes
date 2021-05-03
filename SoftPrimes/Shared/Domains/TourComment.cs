using SoftPrimes.Shared.ModelInterfaces;
using System;

namespace SoftPrimes.Shared.Domains
{
    public class TourComment : IAuditableInsert
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public Tour Tour { get; set; }
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
        public string CreatedBy { get ; set ; }
        public DateTimeOffset? CreatedOn { get ; set ; }
    }
}