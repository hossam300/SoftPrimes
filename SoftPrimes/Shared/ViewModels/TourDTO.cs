using SoftPrimes.Shared.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Shared.ViewModels
{
    public class TourDTO
    {
        public int Id { get; set; }
        public string TourNameAr { get; set; }
        public string TourNameEn { get; set; }
     
        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
    }
}
