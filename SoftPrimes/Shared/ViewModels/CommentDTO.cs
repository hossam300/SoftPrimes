using System;

namespace SoftPrimes.Shared.ViewModels
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int? AttachmentId { get; set; }
        public AttachmentDTO Attachment { get; set; }
        public int? ReplayToComment { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
    }
}