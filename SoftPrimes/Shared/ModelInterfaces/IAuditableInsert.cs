using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Shared.ModelInterfaces
{
    public interface IAuditableInsert
    {
        string CreatedBy { get; set; }
        DateTimeOffset? CreatedOn { get; set; }
    }
}