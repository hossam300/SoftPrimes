using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Shared.ViewModels
{
   public class CheckPointDTO
    {
        public int Id { get; set; }
        public string CheckPointNameAr { get; set; }
        public string CheckPointNameEn { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string LocationText { get; set; }
        public byte[] QRCode { get; set; }
    }
}
