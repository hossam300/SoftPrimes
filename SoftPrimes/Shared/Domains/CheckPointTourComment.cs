using SoftPrimes.Shared.ModelInterfaces;
using System;

namespace SoftPrimes.Shared.Domains
{
    public class CheckPointTourComment : IAuditableInsert
    {
        public int Id { get; set; }
        public int TourCheckPointId { get; set; }
        public virtual TourCheckPoint TourCheckPoint { get; set; }
        public int CommentId { get; set; }
        public virtual Comment Comment { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
    }
}