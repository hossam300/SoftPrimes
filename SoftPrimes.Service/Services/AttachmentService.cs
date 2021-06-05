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
    public class AttachmentService : BusinessService<Attachment, AttachmentDTO>, IAttachmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IRepository<Attachment> _repository;
        public AttachmentService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<Attachment>();
        }
        //private void CustomeMethod()
        //{
        //    var Attachment=_repository.
        //}
    }
}