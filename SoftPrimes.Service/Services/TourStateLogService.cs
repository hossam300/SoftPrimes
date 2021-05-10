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
    public class TourStateLogService : BusinessService<TourStateLog, TourStateLogDTO>, ITourStateLogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IBaseRepository<TourStateLog> _repository;
        public TourStateLogService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<TourStateLog>();
        }
        //private void CustomeMethod()
        //{
        //    var TourStateLog=_repository.
        //}
    }
}