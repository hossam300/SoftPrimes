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
    public class CommentService : BusinessService<Comment, CommentDTO>, ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IBaseRepository<Comment> _repository;
        public CommentService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<Comment>();
        }
        //private void CustomeMethod()
        //{
        //    var Comment=_repository.
        //}
    }
}