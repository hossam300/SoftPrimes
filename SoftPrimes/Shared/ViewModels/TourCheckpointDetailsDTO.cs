using SoftPrimes.Shared.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Shared.ViewModels
{
   public class TourCheckpointDetailsDTO
    {
        public int Id { get; set; }
        public string TourNameAr { get; set; }
        public string TourNameEn { get; set; }
        public DateTimeOffset TourDate { get; set; }
        public TourState TourState { get; set; }
        public List<CommentDetailsDTO> AdminCommnets { get; set; }
        public List<CheckPointDetailsDTO> CheckPoints { get; set; }
    }
}
