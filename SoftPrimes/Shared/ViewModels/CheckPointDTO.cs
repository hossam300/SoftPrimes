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
        public long Lat { get; set; }
        public long Long { get; set; }
        public byte[] QRCode { get; set; }
    }
}
