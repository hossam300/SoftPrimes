using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftPrimes.Service.IServices;
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
    public class LocalizationsController : _BaseController<Localization, LocalizationDTO>
    {
        private readonly ILocalizationService _LocalizationsService;

        public LocalizationsController(ILocalizationService businessService, IHelperServices.ISessionServices sessionSevices) : base(businessService, sessionSevices)
        {
            this._LocalizationsService = businessService;
        }
        [HttpGet]
        [Route("json/{culture}")]
        [AllowAnonymous]
        public string Json(string culture)
        {
            //CultureInfo.CurrentCulture = string.IsNullOrEmpty(culture) ? CultureInfo.CurrentCulture : new CultureInfo(culture);
            //var json = _ILocalizationServices.GetJson();
            //return new FileContentResult(Encoding.UTF8.GetBytes(json), "application/json");
            return this._LocalizationsService.GetJson(culture);
        }
        [HttpGet]
        [Route("GetLastUpDateTime")]
        [AllowAnonymous]
        public DateTime GetLastUpDateTime()
        {
            return this._LocalizationsService.GetLastLocalizationUpdateTime();
        }
    }
}
