using Abp.Authorization;
using Abp;
using Abp.Notifications;
using Abp.Runtime.Session;
using ManagementSystem.Notification.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Notification
{
    public class NotificationAppServices : ManagementSystemAppServiceBase
    {
        private readonly IAbpSession _abpSession;
        private readonly INotificationStore _notificationStore;

        public NotificationAppServices(
               IAbpSession abpSession,
               INotificationStore notificationStore)
        {
            _abpSession = abpSession;
            _notificationStore = notificationStore;
        }

        public async Task<List<UserNotificationDto>> GetUserNotifications()
        {
            long? userId = AbpSession.UserId;

            if (!userId.HasValue)
            {
                throw new AbpAuthorizationException("User is not logged in.");
            }

            var userIdentifier = new UserIdentifier(AbpSession.TenantId, userId.Value);

            var userNotifications =  _notificationStore.GetUserNotificationsWithNotifications(userIdentifier);

            var result = userNotifications.Select(userNotification => new UserNotificationDto
            {
                Id = userNotification.UserNotification.Id,
                TenantId = userNotification.UserNotification.TenantId,
                UserId = userNotification.UserNotification.UserId,
                State = userNotification.UserNotification.State,
                UserNotification = userNotification.UserNotification,
                Notification = userNotification.Notification
            }).ToList();

            return result;
        }
    }
}
