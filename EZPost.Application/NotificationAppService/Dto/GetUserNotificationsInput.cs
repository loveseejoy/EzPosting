using Abp.Notifications;
using EZPost.Common.WebControl;

namespace EZPost.NotificationAppService.Dto
{
    public class GetUserNotificationsInput:DataTablesParameters
    {
        public UserNotificationState? State { get; set; }
    }
}