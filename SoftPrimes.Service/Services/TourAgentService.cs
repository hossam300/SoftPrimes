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
    public class TourAgentService : BusinessService<TourAgent, TourAgentDTO>, ITourAgentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IBaseRepository<TourAgent> _repository;
        public TourAgentService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<TourAgent>();
        }
        //private void CustomeMethod()
        //{
        //    var TourAgent=_repository.
        //}
    }
}