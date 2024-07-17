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
using Abp.Application.Services.Dto;

namespace ManagementSystem.Notification
{
    public class NotificationAppServices : ManagementSystemAppServiceBase
    {
        private readonly IAbpSession _abpSession;
        private readonly INotificationStore _notificationStore;
        private readonly IUserNotificationManager _userNotificationManager;

        public NotificationAppServices(
               IAbpSession abpSession,
               INotificationStore notificationStore,
               IUserNotificationManager userNotificationManager)
        {
            _abpSession = abpSession;
            _notificationStore = notificationStore;
            _userNotificationManager = userNotificationManager;
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
            var filteredNotifications = userNotifications.Where(n => n.UserNotification.State == 0).ToList(); 

            var totalCount = filteredNotifications.Count();

            var result = filteredNotifications.Select(userNotification => new UserNotificationDto
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
        public async Task<List<UserNotificationDto>> GetAllUserNotifications()
        {
            var userIdentifier = new UserIdentifier(AbpSession.TenantId,AbpSession.UserId.Value);
            var userNotifications = _notificationStore.GetUserNotificationsWithNotifications(userIdentifier);
            var result = userNotifications.Select(userNotification => new UserNotificationDto
            {
                Id = userNotification.UserNotification.Id,
                TenantId = userNotification.UserNotification.TenantId,
                UserId = userNotification.UserNotification.UserId,
                State = userNotification.UserNotification.State,
                NotificationMessage = ExtractMessage(userNotification.Notification.Data)

            }).ToList();
            return result;
        }
        public async Task ViewNotification(Guid userNotificationId)
        {             
             _notificationStore.UpdateUserNotificationState(AbpSession.TenantId, userNotificationId, UserNotificationState.Read);
        }
        public async Task Delete(Guid userNotificationId)
        {
            _notificationStore.DeleteUserNotification(AbpSession.TenantId, userNotificationId);

        }
        //public async Task Delete(Guid userNotificationId)
        //{
        //    _notificationStore.DeleteAllUserNotifications(AbpSession.TenantId, userNotificationId);

        //}
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

            return string.Empty; 
        }
    }
}
