using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace EZPost.Authorization
{
    public class EZPostAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //Common permissions
            var pages = context.GetPermissionOrNull(PermissionNames.Pages);
            if (pages == null)
            {
                pages = context.CreatePermission(PermissionNames.Pages, L("Pages"));
            }


            var notification = pages.CreateChildPermission(PermissionNames.Pages_Notification, L("消息管理"));
            notification.CreateChildPermission(PermissionNames.Pages_Notification_SendMessage, L("发送消息"));


            var settings = pages.CreateChildPermission(PermissionNames.Pages_Setting,L("系统设置"));
            var user= settings.CreateChildPermission(PermissionNames.Pages_Setting_User, L("用户管理"));
            user.CreateChildPermission(PermissionNames.Pages_Setting_User_Add, L("添加用户"));
            user.CreateChildPermission(PermissionNames.Pages_Setting_User_Edit, L("修改用户"));
            user.CreateChildPermission(PermissionNames.Pages_Setting_User_Delete, L("删除用户"));
            user.CreateChildPermission(PermissionNames.Pages_Setting_User_SetPermission, L("编辑用户权限"));
            var role= settings.CreateChildPermission(PermissionNames.Pages_Setting_Role, L("角色管理"));
            role.CreateChildPermission(PermissionNames.Pages_Setting_Role_Add, L("添加用户"));
            role.CreateChildPermission(PermissionNames.Pages_Setting_Role_Edit, L("修改用户"));
            role.CreateChildPermission(PermissionNames.Pages_Setting_Role_Delete, L("删除用户"));

            settings.CreateChildPermission(PermissionNames.Pages_Setting_AuditLogs, L("审计日志"));
            settings.CreateChildPermission(PermissionNames.Pages_Setting_Configuration, L("数据配置"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, EZPostConsts.LocalizationSourceName);
        }
    }
}
