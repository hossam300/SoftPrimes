using SoftPrimes.Shared.ModelInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Shared.Domains
{
    public class Attachment : IAuditableInsert, IAuditableUpdate, IAuditableDelete
    {
        public int Id { get; set; }
        public string AttachmentName { get; set; }
        public AttachmentType AttachmentType { get; set; }
        public string AttachmentUrl { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
        public string DeletedBy { get; set; }
        public DateTimeOffset? DeletedOn { get; set; }
    }
    public enum AttachmentType
    {
        Image = 1,
        Voice = 2,
        Video = 3
    }
}
