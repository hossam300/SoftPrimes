using System;

namespace SoftPrimes.Shared.Domains
{
    public class Company
    {
        public int Id { get; set; }
        public string CompanyNameAr { get; set; }
        public string CompanyNameEn { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyImageUrl { get; set; }
        public string Phone { get; set; }
        public DateTime VaildFrom { get; set; }
        public DateTime VaildTo { get; set; }
    }
}