using Abp;
using Abp.Notifications;

namespace EZPost.NotificationAppService.Dto
{
    public class SendMessageInput
    {
        //通知的用户
        public UserIdentifier[] Users { set; get; }

        //通知内容
        public string Message { set; get; }

        //通知类型
        public NotificationSeverity Severity { set; get; } = NotificationSeverity.Info;

    }
}