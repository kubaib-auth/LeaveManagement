using Abp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Notification.Dto
{
    public class UserNotificationDto
    {
        public Guid Id { get; set; }
        public int? TenantId { get; set; }
        public long UserId { get; set; }
        public UserNotificationState State { get; set; }
        public UserNotificationInfo UserNotification { get; set; }
        public TenantNotificationInfo Notification { get; set; }
    }
}
