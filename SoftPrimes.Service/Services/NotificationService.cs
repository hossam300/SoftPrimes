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
    public class NotificationService : BusinessService<Notification, NotificationDTO>, INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IBaseRepository<Notification> _repository;
        public NotificationService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<Notification>();
        }
        //private void CustomeMethod()
        //{
        //    var Notification=_repository.
        //}
    }
}