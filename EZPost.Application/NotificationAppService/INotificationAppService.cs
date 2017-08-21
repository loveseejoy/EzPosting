using System;
using Abp.Application.Services;
using Abp.Notifications;
using EZPost.Common.WebControl;
using EZPost.NotificationAppService.Dto;

namespace EZPost.NotificationAppService
{
    public interface INotificationAppService:IApplicationService
    {
        IPagedList<UserNotification> GetUserNotifications(GetUserNotificationsInput input);

        int GetUnReadNotificationsCount();

        void SetNotificationAsRead(Guid input);

        void SendMessage(SendMessageInput  input);
    }
}