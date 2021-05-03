using SoftPrimes.Shared.ModelInterfaces;
using System;

namespace SoftPrimes.Shared.Domains
{
    public class CheckPoint : IAuditableInsert, IAuditableUpdate, IAuditableDelete
    {
        public int Id { get; set; }
        public string CheckPointNameAr { get; set; }
        public string CheckPointNameEn { get; set; }
        public long Lat { get; set; }
        public long Long { get; set; }
        public byte[] QRCode { get; set; }
        public string DeletedBy { get; set; }
        public DateTimeOffset? DeletedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
    }
}