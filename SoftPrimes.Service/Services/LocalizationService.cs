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
    public class LocalizationService : BusinessService<Localization, LocalizationDTO>, ILocalizationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IRepository<Localization> _repository;
        public LocalizationService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<Localization>();
        }
        //private void CustomeMethod()
        //{
        //    var Localization=_repository.
        //}
    }
}