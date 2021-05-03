using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Shared.ModelInterfaces
{
    public interface IAuditableUpdate
    {
        string UpdatedBy { get; set; }
        DateTimeOffset? UpdatedOn { get; set; }
    }
}