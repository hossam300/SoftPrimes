using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<Agent> _userManager;
        public ToursController(ITourService businessService, UserManager<Agent> userManager, IHelperServices.ISessionServices sessionSevices) : base(businessService, sessionSevices)
        {
            this._TourService = businessService;
            _userManager = userManager;
        }


        [HttpGet("GetTodayTours")]
        public async Task<List<HomeTourDTO>> GetTodayTours(float lat, float longs)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return _TourService.GetTodayTours(lat, longs, user.Id);
        }
        [HttpGet("GetTourHistory")]
        public async Task<List<HomeTourDTO>> GetTourHistory(float lat, float longs)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return _TourService.GetTourHistory(lat, longs, user.Id);
        }
        [HttpGet("GetTourPoints")]
        public List<TourCheckPointDTO> GetTourPoints(int tourId)
        {
            return _TourService.GetTourPoints(tourId);
        }
        [HttpGet("GetAdminComments")]
        public List<TourCommentDTO> GetAdminComments(int tourId)
        {
            return _TourService.GetAdminComments(tourId);
        }
        [HttpGet("ChangeTourState")]
        public bool ChangeTourState(int TourId, int State)
        {
            return _TourService.ChangeTourState(TourId, State);
        }
    }
}
