using Abp.Application.Navigation;
using Abp.Localization;
using EZPost.Authorization;

namespace EZPost.Web
{
    /// <summary>
    /// This class defines menus for the application.
    /// It uses ABP's menu system.
    /// When you add menu items here, they are automatically appear in angular application.
    /// See Views/Layout/_TopMenu.cshtml file to know how to render menu.
    /// </summary>
    public class EZPostNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        "Home",
                        L("主页"),
                        url: "/Home/Dashbord",
                        icon: "fa fa-home",
                        requiresAuthentication: true
                        ))
             .AddItem(
                    new MenuItemDefinition(
                        "LevelingGame",
                        L("代练管理"),
                        icon: "fa fa-home",
                        requiresAuthentication: true
                        ).AddItem(new MenuItemDefinition(
                            "Game",
                            L("游戏管理"),
                            url: "/Game/Index",
                            icon: "fa fa-home",
                            requiresAuthentication: true
                            )))
                .AddItem(
                new MenuItemDefinition(
                    "SystemSetting",
                    L("系统设置"),
                    icon: "fa fa-globe",
                    requiredPermissionName: PermissionNames.Pages_Setting
                        ).AddItem(
                            new MenuItemDefinition(
                            "Users",
                            L("用户管理"),
                            url: "/Setting/Users",
                            icon: "fa fa-users",
                            requiredPermissionName: PermissionNames.Pages_Setting_User
                            )).AddItem(
                            new MenuItemDefinition(
                            "Roles",
                            L("角色管理"),
                            url: "/Setting/Role",
                            icon: "fa fa-globe",
                            requiredPermissionName: PermissionNames.Pages_Setting_Role
                            )).AddItem(
                            new MenuItemDefinition(
                            "AuditingLog",
                            L("审计日志"),
                            url: "/AudtingLog/Index",
                            icon: "fa fa-globe",
                            requiredPermissionName: PermissionNames.Pages_Setting_AuditLogs
                            )).AddItem(
                            new MenuItemDefinition(
                            "Configuration",
                            L("数据配置"),
                            url: "/Setting/ConfigurationSetting",
                            icon: "fa fa-globe",
                            requiredPermissionName: PermissionNames.Pages_Setting_Configuration
                            ))
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, EZPostConsts.LocalizationSourceName);
        }
    }
}
