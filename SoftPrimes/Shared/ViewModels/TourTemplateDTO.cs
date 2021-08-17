using SoftPrimes.Shared.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Shared.ViewModels
{
    public class TourTemplateDTO
    {
        public int Id { get; set; }
        public string TourNameAr { get; set; }
        public string TourNameEn { get; set; }
        public virtual List<TourCheckPointDTO> CheckPoints { get; set; }
    }
}
