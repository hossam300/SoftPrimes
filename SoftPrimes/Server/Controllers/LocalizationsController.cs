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
    public class LocalizationsController : _BaseController<Localization, LocalizationDTO>
    {
        private readonly ILocalizationService _LocalizationsService;

        public LocalizationsController(ILocalizationService businessService) : base(businessService)
        {
            this._LocalizationsService = businessService;
        }
        [HttpGet("GetAllLocalizationsDTO")]
        public IActionResult GetAllLocalizationsDTO()
        {
            var LocalizationsDTOs = _LocalizationsService.GetAllWithoutInclude().Select(x => new LocalizationDTO
            {
                Id = x.Id,
                CreatedBy = x.CreatedBy,
                CreatedOn = x.CreatedOn,
                Key = x.Key,
                ValueAr = x.ValueAr,
                ValueEn = x.ValueEn
            }).ToList();
            return Ok(LocalizationsDTOs);
        }
    }
}
