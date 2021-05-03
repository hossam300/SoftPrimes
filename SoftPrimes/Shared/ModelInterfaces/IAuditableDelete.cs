using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Shared.ModelInterfaces
{
    public interface IAuditableDelete
    {
        string DeletedBy { get; set; }
        DateTimeOffset? DeletedOn { get; set; }
    }
}