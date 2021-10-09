using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Shared.ViewModels
{
    public class LocationQrCodeDTO
    {
        public int CheckPointId { get; set; }
        public byte[] QRCode { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }

    }
}
