using Abp.Authorization;
using Abp;
using Abp.Notifications;
using Abp.Runtime.Session;
using ManagementSystem.Notification.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
            var totalCount = userNotifications.Count();

            var result = userNotifications.Select(userNotification => new UserNotificationDto
            {
                Id = userNotification.UserNotification.Id,
                TenantId = userNotification.UserNotification.TenantId,
                UserId = userNotification.UserNotification.UserId,
                State = userNotification.UserNotification.State,
                UserNotification = userNotification.UserNotification,
                Notification = userNotification.Notification,
                NotificationCount= totalCount,
                NotificationMessage = ExtractMessage(userNotification.Notification.Data)

            }).ToList();

            return result;
        }
        private string ExtractMessage(object data)
        {
            if (data is Dictionary<string, object> dataDict)
            {
                if (dataDict.TryGetValue("Message", out var messageObj) && messageObj is string message)
                {
                    return message;
                }
            }
            else if (data is string jsonData)
            {
                var dataaDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonData);
                if (dataaDict != null && dataaDict.TryGetValue("Message", out var messageObj) && messageObj is string message)
                {
                    return message;
                }
            }

            return string.Empty; // or handle the case when the message is not found or is not a string
        }
    }
}
