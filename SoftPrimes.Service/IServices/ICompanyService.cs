﻿using SoftPrimes.Shared.Domains;
using SoftPrimes.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Service.IServices
{
  public interface ICompanyService : IBusinessService<Company, CompanyDTO>
  {
    List<CompanyDTO> GetCompanyLookups(string searchText, int take);
  }
}
