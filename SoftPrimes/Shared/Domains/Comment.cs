using SoftPrimes.Shared.ModelInterfaces;
using System;

namespace SoftPrimes.Shared.Domains
{
    public class Comment : IAuditableInsert
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int? AttachmentId { get; set; }
        public virtual Attachment Attachment { get; set; }
        public int? ReplayToComment { get; set; }
        public string CreatedBy { get ; set ; }
        public DateTimeOffset? CreatedOn { get ; set ; }
    }
}