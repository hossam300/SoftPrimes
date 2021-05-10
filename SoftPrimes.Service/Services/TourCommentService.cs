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
    public class TourCommentService : BusinessService<TourComment, TourCommentDTO>, ITourCommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IBaseRepository<TourComment> _repository;
        public TourCommentService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<TourComment>();
        }
        //private void CustomeMethod()
        //{
        //    var TourComment=_repository.
        //}
    }
}