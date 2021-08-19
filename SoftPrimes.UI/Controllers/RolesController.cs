﻿using Microsoft.AspNetCore.Http;
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
    public class RolesController : _BaseController<Role, RoleDTO>
    {
        private readonly IRoleService _RoleService;

        public RolesController(IRoleService businessService, IHelperServices.ISessionServices sessionSevices) : base(businessService, sessionSevices)
        {
            this._RoleService = businessService;
        }
        public List<RoleDTO> GetRoleLookups(string searchText)
{
            return _RoleService.GetRoleLookups(searchText);
        }
    }
}
