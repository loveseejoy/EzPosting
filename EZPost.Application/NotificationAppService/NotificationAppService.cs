using System;
using System.Linq;
using Abp;
using Abp.Authorization;
using Abp.Notifications;
using Abp.Runtime.Session;
using EZPost.Common.WebControl;
using EZPost.NotificationAppService.Dto;

namespace EZPost.NotificationAppService
{
    [AbpAuthorize]
    public class NotificationAppService:EZPostAppServiceBase,INotificationAppService
    {
        //通知管理
        private readonly IUserNotificationManager _userNotificationManager;
        private readonly INotificationPublisher _notificationPublisher;
      
        public NotificationAppService(IUserNotificationManager userNotificationManager, INotificationPublisher notificationPublisher)
        {
            _userNotificationManager = userNotificationManager;
            _notificationPublisher = notificationPublisher;
        }

        public int GetUnReadNotificationsCount()
        {
            var totalCount =  _userNotificationManager.GetUserNotificationCount(
               AbpSession.ToUserIdentifier(), UserNotificationState.Unread
               );
            return totalCount;
        }

        public IPagedList<UserNotification> GetUserNotifications(GetUserNotificationsInput input)
        {
            var totalCount = _userNotificationManager.GetUserNotificationCount(
               AbpSession.ToUserIdentifier(), UserNotificationState.Unread
               );

            var notifications = _userNotificationManager.GetUserNotifications(
                AbpSession.ToUserIdentifier(), input.State,input.Start,input.Length
            );

            return  new PagedList<UserNotification>(notifications,input.Start,input.Length,totalCount);
        }

        public void SendMessage(SendMessageInput input)
        {
            if (input.Users != null)
            {
                _notificationPublisher.Publish("App.SendMessage",
                new MessageNotificationData(input.Message),
                severity: input.Severity,
                userIds: input.Users);
            }
            else
            {
                var users = UserManager.Users.ToList().Select(x => new UserIdentifier(x.TenantId,x.Id)).ToArray();
                _notificationPublisher.Publish("App.SendMessage",
                  new MessageNotificationData(input.Message),
                  severity: input.Severity,
                  userIds: users);
            }
        }

        public void SetNotificationAsRead(Guid input)
        {
            var userNotification =  _userNotificationManager.GetUserNotification(AbpSession.TenantId, input);
            if (userNotification.UserId != AbpSession.GetUserId())
            {
                throw new ApplicationException(string.Format("Given user notification id ({0}) is not belong to the current user ({1})", input, AbpSession.GetUserId()));
            }

             _userNotificationManager.UpdateUserNotificationState(AbpSession.TenantId, input, UserNotificationState.Read);
        }
    }
}