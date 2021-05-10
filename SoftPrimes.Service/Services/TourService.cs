using AutoMapper;
using SoftPrimes.BLL.BaseObjects.ReSoftPrimesitoriesInterfaces;
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
    public class TourService : BusinessService<Tour, TourDTO>, ITourService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IBaseRepository<Tour> _repository;
        public TourService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<Tour>();
        }
        //private void CustomeMethod()
        //{
        //    var Tour=_repository.
        //}
    }
}