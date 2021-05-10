using SoftPrimes.Shared.Domains;
using System.Collections.Generic;

namespace SoftPrimes.Shared.ViewModels
{
    public class TourCheckPointDTO
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public TourAgentDTO Tour { get; set; }
        public int CheckPointId { get; set; }
        public CheckPointDTO CheckPoint { get; set; }
        public List<CheckPointTourCommentDTO> CheckPointTourComments { get; set; }
        public TourCheckPointState TourCheckPointState { get; set; }
    }
}