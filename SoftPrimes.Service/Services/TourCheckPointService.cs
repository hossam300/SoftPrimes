using AutoMapper;
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
    public class TourCheckPointService : BusinessService<TourCheckPoint, TourCheckPointDTO>, ITourCheckPointService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IRepository<TourCheckPoint> _repository;
        public TourCheckPointService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<TourCheckPoint>();
        }
        //private void CustomeMethod()
        //{
        //    var TourCheckPoint=_repository.
        //}
    }
}