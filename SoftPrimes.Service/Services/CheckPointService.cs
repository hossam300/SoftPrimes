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
    public class CheckPointService : BusinessService<CheckPoint, CheckPointDTO>, ICheckPointService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IBaseRepository<CheckPoint> _repository;
        public CheckPointService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<CheckPoint>();
        }
        //private void CustomeMethod()
        //{
        //    var CheckPoint=_repository.
        //}
    }
}