using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Shared.ViewModels
{
    public class AgentDetailsDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string JobTitle { get; set; }
        public string Mobile { get; set; }
        public DateTime BirthDate { get; set; }
        public string FullNameAr { get; set; }
        public string FullNameEn { get; set; }
    }
}
