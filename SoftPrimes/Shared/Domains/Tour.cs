﻿using SoftPrimes.Shared.ModelInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Shared.Domains
{
    public class Tour : IAuditableInsert, IAuditableUpdate, IAuditableDelete
    {
        public int Id { get; set; }
        public string TourNameAr { get; set; }
        public string TourNameEn { get; set; }
        public TourType TourType { get; set; }
        public DateTimeOffset TourDate { get; set; }
        public List<TourCheckPoint> CheckPoints { get; set; }
        // Can it be Multi Agent in same tour?
        public string AgentId { get; set; }
        public Agent Agent { get; set; }
        public TourState TourState { get; set; }
        public double EstimatedDistance { get; set; }
        public DateTimeOffset? EstimatedEndDate { get; set; }
        public List<TourStateLog> TourStateLogs { get; set; }
        public List<TourComment> Comments { get; set; }
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
