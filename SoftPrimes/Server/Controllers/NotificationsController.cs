using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftPrimes.Service.IServices;
using SoftPrimes.Shared.Domains;
using SoftPrimes.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftPrimes.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : _BaseController<Notification, NotificationDTO>
    {
        private readonly INotificationService _NotificationService;

        public NotificationsController(INotificationService businessService) : base(businessService)
        {
            this._NotificationService = businessService;
        }
        [HttpGet("GetAllNotificationDTO")]
        public IActionResult GetAllNotificationDTO()
        {
            var NotificationDTOs = _NotificationService.GetAllWithoutInclude().Select(x => new NotificationDTO
            {
                Id = x.Id,
                CreatedBy = x.CreatedBy,
                CreatedOn = x.CreatedOn,
                NotificationType = x.NotificationType,
                Text = x.Text,
                ToAgentId = x.ToAgentId
            }).ToList();
            return Ok(NotificationDTOs);
        }
    }
}
