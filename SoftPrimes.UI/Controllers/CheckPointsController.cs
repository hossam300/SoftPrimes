using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftPrimes.Service.IServices;
using SoftPrimes.Service.Services;
using SoftPrimes.Shared.Domains;
using SoftPrimes.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftPrimes.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckPointsController : _BaseController<CheckPoint, CheckPointDTO>
    {
        private readonly ICheckPointService _CheckPointService;

        public CheckPointsController(ICheckPointService businessService, IHelperServices.ISessionServices sessionSevices) : base(businessService, sessionSevices)
        {
            this._CheckPointService = businessService;
        }
        [HttpGet("GetCheckPointLookups")]
        public List<CheckPointDTO> GetCheckPointLookups(string searchText, int take = 20)
        {
            return _CheckPointService.GetCheckPointLookups(searchText, take);
        }
    }
}
