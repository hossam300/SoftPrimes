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
    public class ToursController : _BaseController<Tour, TourDTO>
    {
        private readonly ITourService _TourService;

        public ToursController(ITourService businessService) : base(businessService)
        {
            this._TourService = businessService;
        }
        [HttpGet("GetAllTourDTO")]
        public IActionResult GetAllTourDTO()
        {
            var TourDTOs = _TourService.GetAllWithoutInclude().Select(x => new TourDTO
            {
                Id = x.Id,
                CreatedBy = x.CreatedBy,
                CreatedOn = x.CreatedOn,
                TourNameAr = x.TourNameAr,
                TourNameEn = x.TourNameEn,
                TourType = x.TourType
            }).ToList();
            return Ok(TourDTOs);
        }
    }
}
