using Abp.Authorization;
using EZPost.Authorization.Roles;
using EZPost.MultiTenancy;
using EZPost.Users;

namespace EZPost.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
