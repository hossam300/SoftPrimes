using SoftPrimes.Shared.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Shared.ViewModels
{
    public class CheckPointTourCommentDetailDTO
    {
        public int CheckPointId { get; set; }
        public string Text { get; set; }
        public byte[] File { get; set; }
        public int TourId { get; set; }
        public int AttachmentType { get; set; }
    }
}
