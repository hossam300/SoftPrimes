using SoftPrimes.Shared.Domains;

namespace SoftPrimes.Shared.ViewModels
{
    public class AgentCheckPointDetailsDTO
    {
        public int Id { get; set; }
        public string CheckPointNameAr { get; set; }
        public string CheckPointNameEn { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string LocationText { get; set; }
        public string QRCode { get; set; }
        public double distanceToNextPoint { get; set; }
        public TourCheckPointState CheckPointState { get; set; }
    }
}