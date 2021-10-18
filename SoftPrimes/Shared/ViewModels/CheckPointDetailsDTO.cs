using SoftPrimes.Shared.Domains;
using System;
using System.Collections.Generic;

namespace SoftPrimes.Shared.ViewModels
{
    public class CheckPointDetailsDTO
    {
        public int Id { get; set; }
        public string CheckPointNameAr { get; set; }
        public string CheckPointNameEn { get; set; }
        public double EstimatedDistance { get; set; }
        public string LocationName { get; set; }
        public double Lat{ get; set; }
        public double Long { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public string QRCode { get; set; }
        public TourCheckPointState CheckPointState { get; set; }
        public List<AttachmentCommentDTO> Comments { get; set; }
    }

}