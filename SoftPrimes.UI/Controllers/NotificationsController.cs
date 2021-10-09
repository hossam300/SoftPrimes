using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftPrimes.Service.IServices;
using SoftPrimes.Shared.Domains;
using SoftPrimes.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftPrimes.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : _BaseController<Notification, NotificationDTO>
    {
        private readonly INotificationService _NotificationService;

        public NotificationsController(INotificationService businessService, IHelperServices.ISessionServices sessionSevices) : base(businessService, sessionSevices)
        {
            this._NotificationService = businessService;
        }
        [HttpGet("GetNotificationCount")]
        public int GetNotificationCount()
        {
            return _NotificationService.GetNotificationCount();
        }

        [HttpGet("GetNotificationList")]
        public List<NotificationDTO> GetNotificationList()
        {
            return _NotificationService.GetNotificationList();
        }
        [HttpPost("ReadNotifications")]
        public List<NotificationDTO> ReadNotifications()
        {
            return _NotificationService.ReadNotifications();
        }
    }
}
