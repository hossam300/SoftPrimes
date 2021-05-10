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
    public class CheckPointTourCommentService : BusinessService<CheckPointTourComment, CheckPointTourCommentDTO>, ICheckPointTourCommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IBaseRepository<CheckPointTourComment> _repository;
        public CheckPointTourCommentService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<CheckPointTourComment>();
        }
        //private void CustomeMethod()
        //{
        //    var CheckPointTourComment=_repository.
        //}
    }
}