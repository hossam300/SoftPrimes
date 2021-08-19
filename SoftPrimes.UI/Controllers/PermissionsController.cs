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
    public class PermissionsController : _BaseController<Permission, PermissionDTO>
    {
        private readonly IPermissionService _PermissionService;

        public PermissionsController(IPermissionService businessService, IHelperServices.ISessionServices sessionSevices) : base(businessService, sessionSevices)
        {
            this._PermissionService = businessService;
        }
        public List<PermissionDTO> GetPermissionLookups(string searchText, int take = 20)
        {
            return _PermissionService.GetPermissionLookups(searchText, take);
        }
    }
}
