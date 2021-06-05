using SoftPrimes.Shared.ModelInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Shared.Domains
{
    public class TourAgent : IAuditableInsert, IAuditableUpdate, IAuditableDelete
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public virtual Tour Tour { get; set; }
        public TourType TourType { get; set; }
        public DateTimeOffset TourDate { get; set; }
        public virtual List<TourCheckPoint> CheckPoints { get; set; }
        public string AgentId { get; set; }
        public virtual Agent Agent { get; set; }
        public TourState TourState { get; set; }
        public double EstimatedDistance { get; set; }
        public DateTimeOffset? EstimatedEndDate { get; set; }
        public virtual List<TourStateLog> TourStateLogs { get; set; }
        public virtual List<TourComment> Comments { get; set; }
        public string DeletedBy { get; set; }
        public DateTimeOffset? DeletedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
    }
    public enum TourState
    {
        New = 1,
        InProgress = 2,
        Complete = 3,
        NotCompleted = 4,
        Cancled = 5
    }
    public enum TourType
    {
        TourPoints = 1,
        Monitoring = 2
    }
}
