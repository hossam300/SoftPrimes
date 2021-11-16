using SoftPrimes.Shared.ModelInterfaces;
using System;
using System.Collections.Generic;

namespace SoftPrimes.Shared.Domains
{
    public class TourCheckPoint : IAuditableInsert, IAuditableUpdate, IAuditableDelete
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public virtual TourAgent Tour { get; set; }
        public int CheckPointId { get; set; }
        public virtual CheckPoint CheckPoint { get; set; }
        public virtual List<CheckPointTourComment> CheckPointTourComments { get; set; }
        public TourCheckPointState TourCheckPointState { get; set; } = TourCheckPointState.New;
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTimeOffset? DeletedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
        public double? CheckoutLat { get; set; }
        public double? CheckoutLong { get; set; }
        public DateTime? CheckoutDate { get; set; }
    }
    public enum TourCheckPointState
    {
        New = 1,
        InProgress = 2,
        Completed = 3,
        Cancled = 4
    }
}