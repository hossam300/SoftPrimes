using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Shared.ViewModels
{
    public class LocalizationDTO
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string ValueAr { get; set; }
        public string ValueEn { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
    }
}
