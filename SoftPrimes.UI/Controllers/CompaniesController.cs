﻿using Microsoft.AspNetCore.Http;
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
  public class CompaniesController : _BaseController<Company, CompanyDTO>
  {
    private readonly ICompanyService _companyService;

    public CompaniesController(ICompanyService businessService, IHelperServices.ISessionServices sessionSevices) : base(businessService, sessionSevices)
    {
      this._companyService = businessService;
    }
    [HttpGet("GetCompanyLookups")]
    public List<CompanyDTO> GetCompanyLookups(string searchText, int take = 20)
    {
      return _companyService.GetCompanyLookups(searchText, take);
    }
  }
}
