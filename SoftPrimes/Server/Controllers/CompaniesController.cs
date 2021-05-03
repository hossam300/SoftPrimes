using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftPrimes.Service.IServices;
using SoftPrimes.Shared.Domains;
using SoftPrimes.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftPrimes.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : _BaseController<Company, CompanyDTO>
    {
        private readonly ICompanyService _companyService;

        public CompaniesController(ICompanyService businessService) : base(businessService)
        {
            this._companyService = businessService;
        }
        [HttpGet("GetAllCompanyDTO")]
        public IActionResult GetAllCompanyDTO()
        {
            var CompanyDTOs = _companyService.GetAllWithoutInclude().Select(x => new CompanyDTO { Id = x.Id, CompanyNameAr = x.CompanyNameAr, CompanyNameEn = x.CompanyNameEn }).ToList();
            return Ok(CompanyDTOs);
        }
    }
}
