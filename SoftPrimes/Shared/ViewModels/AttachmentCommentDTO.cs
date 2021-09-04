using SoftPrimes.Shared.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Shared.ViewModels
{
    public class AttachmentCommentDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int? AttachmentId { get; set; }
        public string AttachmentName { get; set; }
        public AttachmentType AttachmentType { get; set; }
        public string AttachmentUrl { get; set; }
        public int? ReplayToComment { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
    }
}
