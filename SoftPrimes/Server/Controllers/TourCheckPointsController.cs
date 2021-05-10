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
    public class TourCheckPointsController : _BaseController<TourCheckPoint, TourCheckPointDTO>
    {
        private readonly ITourCheckPointService _TourCheckPointService;

        public TourCheckPointsController(ITourCheckPointService businessService) : base(businessService)
        {
            this._TourCheckPointService = businessService;
        }
        [HttpGet("GetAllTourCheckPointDTO")]
        public IActionResult GetAllTourCheckPointDTO()
        {
            var TourCheckPointDTOs = _TourCheckPointService.GetAllWithoutInclude().Select(x => new TourCheckPointDTO
            {
                Id = x.Id,
                CheckPoint = new CheckPointDTO { CheckPointNameAr = x.CheckPoint.CheckPointNameAr, CheckPointNameEn = x.CheckPoint.CheckPointNameEn, Id = x.CheckPointId, Lat = x.CheckPoint.Lat, Long = x.CheckPoint.Long, QRCode = x.CheckPoint.QRCode },
                CheckPointId = x.CheckPointId,
                TourId = x.TourId,


            }).ToList();
            return Ok(TourCheckPointDTOs);
        }
    }
}
