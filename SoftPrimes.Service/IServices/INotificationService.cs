using SoftPrimes.Shared.Domains;
using SoftPrimes.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Service.IServices
{
    public interface INotificationService : IBusinessService<Notification, NotificationDTO>
    {
        int GetNotificationCount();
        List<NotificationDTO> GetNotificationList();
        List<NotificationDTO> ReadNotifications();
    }
}
