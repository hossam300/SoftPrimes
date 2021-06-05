using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Shared.ViewModels
{
    public class CheckUniqueDTO
    {
        public string TableName { get; set; }
        public string[] Fields { get; set; }
        public string[] Values { get; set; }
    }
}
