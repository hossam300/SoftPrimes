using System;

namespace SoftPrimes.Shared.ViewModels
{
    public class TourCommentDTO
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public TourAgentDTO Tour { get; set; }
        public int CommentId { get; set; }
        public CommentDTO Comment { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public AgentDTO CreatedByUser { get; set; }
    }
}