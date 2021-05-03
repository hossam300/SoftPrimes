using SoftPrimes.Shared.ModelInterfaces;
using System;
using System.Collections.Generic;

namespace SoftPrimes.Shared.Domains
{
    public class TourCheckPoint : IAuditableInsert, IAuditableUpdate, IAuditableDelete
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public Tour Tour { get; set; }
        public int CheckPointId { get; set; }
        public CheckPoint CheckPoint { get; set; }
        public List<CheckPointTourComment> CheckPointTourComments { get; set; }
        public TourCheckPointState TourCheckPointState { get; set; }
        public string DeletedBy { get; set; }
        public DateTimeOffset? DeletedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
    }
    public enum TourCheckPointState
    {
        New = 1,
        InProgress = 2,
        Completed = 3,
        Cancled = 4
    }
}