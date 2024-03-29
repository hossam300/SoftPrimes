﻿using AutoMapper;
using SoftPrimes.BLL.BaseObjects.RepositoriesInterfaces;
using SoftPrimes.Service.IServices;
using SoftPrimes.Shared.Domains;
using SoftPrimes.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Service.Services
{
    
    public class CompanyService : BusinessService<Company, CompanyDTO>, ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IRepository<Company> _repository;
        public CompanyService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<Company>();
        }

    public List<CompanyDTO> GetCompanyLookups(string searchText, int take)
    {
      if (searchText == "" || string.IsNullOrEmpty(searchText) || searchText == null)
      {
        return _repository.GetAll().Select(x => new CompanyDTO
        {
          Id = x.Id,
          CompanyNameAr = x.CompanyNameAr,
          CompanyNameEn = x.CompanyNameEn
        }).Take(take).ToList();
      }
      else
      {
        return _repository.GetAll().Where(x => x.CompanyNameAr.Contains(searchText) || x.CompanyNameEn.Contains(searchText))
            .Select(x => new CompanyDTO
            {
              Id = x.Id,
              CompanyNameAr = x.CompanyNameAr,
              CompanyNameEn = x.CompanyNameEn
            }).Take(take).ToList();
      }
    }
    //private void CustomeMethod()
    //{
    //    var company=_repository.
    //}
  }
}