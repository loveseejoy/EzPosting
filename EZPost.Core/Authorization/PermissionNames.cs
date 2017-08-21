using System.CodeDom;

namespace EZPost.Authorization
{
    public static class PermissionNames
    {
        public const string Pages = "Pages";

        public const string Pages_Setting = "Pages.Setting";
        public const string Pages_Setting_User = "Pages.Setting.User";
        public const string Pages_Setting_User_Delete = "Pages.Setting.User.Delete";
        public const string Pages_Setting_User_Add = "Pages.Setting.User.Add";
        public const string Pages_Setting_User_Edit = "Pages.Setting.User.Edit";
        public const string Pages_Setting_User_SetPermission = "Pages.Setting.User.SetPermission";
        public const string Pages_Setting_Role = "Pages.Setting.Role";
        public const string Pages_Setting_Role_Add = "Pages.Setting.Role.Add";
        public const string Pages_Setting_Role_Edit = "Pages.Setting.Role.Edit";
        public const string Pages_Setting_Role_Delete = "Pages.Setting.Role.Delete";
        public const string Pages_Setting_AuditLogs = "Pages.Setting.AuditLogs";
        public const string Pages_Setting_Configuration = "Pages.Setting.Configuration";

        public const string Pages_Notification = "Pages.Notification";
        public const string Pages_Notification_SendMessage = "Pages.Notification.SendMessage";

    }
}