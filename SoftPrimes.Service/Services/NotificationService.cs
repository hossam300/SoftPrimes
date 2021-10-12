using AutoMapper;
using IHelperServices;
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
    public class NotificationService : BusinessService<Notification, NotificationDTO>, INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISessionServices _sessionServices;
        private IRepository<Notification> _repository;
        public NotificationService(IUnitOfWork unitOfWork, IMapper mapper, IHelperServices.ISessionServices sessionServices) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _sessionServices = sessionServices;
            _repository = _unitOfWork.GetRepository<Notification>();
        }

        public int GetNotificationCount()
        {
            return _repository.GetAll().Where(c => c.ToAgentId == _sessionServices.UserId && !c.IsReaded).Count();
        }

        public List<NotificationDTO> GetNotificationList()
        {
            return _repository.GetAll().Where(c => c.ToAgentId == _sessionServices.UserId && !c.IsReaded)
                .Select(x => new NotificationDTO()
                {
                    Id = x.Id,
                    CreatedBy = x.CreatedBy,
                    CreatedOn = x.CreatedOn,
                    IsReaded = x.IsReaded,
                    NotificationType = x.NotificationType,
                    Text = x.Text,
                    ToAgentId = x.ToAgentId,
                    ToAgent = new AgentDTO
                    {
                        Id = x.ToAgent.Id,
                        UserName = x.ToAgent.UserName,
                        FullNameAr = x.ToAgent.FullNameAr,
                        FullNameEn = x.ToAgent.FullNameEn,
                        Image = x.ToAgent.Image
                    }
                }).ToList();
        }

        public List<NotificationDTO> ReadNotifications()
        {
            //List<NotificationDTO> notifications = new List<NotificationDTO>();
            //foreach (var item in notificationIds)
            //{
            var notfications = _repository.GetAll().Where(x => x.ToAgentId == _sessionServices.UserId ).ToList();
            foreach (var notfication in notfications)
            {
                notfication.IsReaded = true;
                _repository.Update(notfication);
            }

            return _Mapper.Map(notfications, typeof(List<Notification>), typeof(List<NotificationDTO>)) as List<NotificationDTO>;
            //}
            // return notifications;
        }
        //private void CustomeMethod()
        //{
        //    var Notification=_repository.
        //}
    }
}