using SoftPrimes.Shared.ModelInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Shared.Domains
{
    public class Localization : IAuditableInsert, IAuditableUpdate, IAuditableDelete
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string ValueAr { get; set; }
        public string ValueEn { get; set; }
        public string CreatedBy { get ; set ; }
        public DateTimeOffset? CreatedOn { get ; set ; }
        public string UpdatedBy { get ; set ; }
        public DateTimeOffset? UpdatedOn { get ; set ; }
        public string DeletedBy { get ; set ; }
        public DateTimeOffset? DeletedOn { get ; set ; }
    }
}
