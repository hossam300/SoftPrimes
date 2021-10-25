using SoftPrimes.Shared.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Shared.ViewModels
{
    public class TourCreateDTO
    {
        public int Id { get; set; }
        public TourType TourType { get; set; }
        public int? TourId { get; set; }
        public string TourName { get; set; }
        public DateTime TourDate { get; set; }
        public double EstimatedDistance { get; set; }
        public List<PointLocationDTO> PointLocations { get; set; }
        public string AgentId { get; set; }
        public int CaptureLocation { get; set; }
        public bool IsTemplate { get; set; }
        public string AdminComment { get; set; }
    }
}
