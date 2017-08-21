using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Notifications;
using Abp.Runtime.Session;
using Abp.UI;
using EZPost.Authorization;
using EZPost.Authorization.Roles;
using EZPost.Users.Dto;
using Microsoft.AspNet.Identity;
using EZPost.Common;
using EZPost.Common.WebControl;
using EZPost.Dto;

namespace EZPost.Users
{
    /* THIS IS JUST A SAMPLE. */
    [AbpAuthorize(PermissionNames.Pages_Setting_User)]
    public class UserAppService : EZPostAppServiceBase, IUserAppService
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly IPermissionManager _permissionManager;
        private readonly INotificationSubscriptionManager _notificationSubscriptionManager;
        private readonly RoleManager _roleManager;

        public UserAppService(IRepository<User, long> userRepository,
            IPermissionManager permissionManager,
            RoleManager roleManager,
            INotificationSubscriptionManager notificationSubscriptionManager
            )
        {
            _userRepository = userRepository;
            _permissionManager = permissionManager;
            _roleManager = roleManager;
            _notificationSubscriptionManager = notificationSubscriptionManager;
        }

        public async Task<IPagedList<UserListDto>> GetUsers(GetUserInput input)
        {
            var query =  UserManager.Users
                              .Include(u => u.Roles)
                              .WhereIf(!input.UserName.IsNullOrWhiteSpace(), x => x.UserName.Contains(input.UserName));

            var count = await  query.CountAsync();
            var users = await  query.AsNoTracking().OrderBys(input).PageBy(input).ToListAsync();
            var userListDtos = users.MapTo<List<UserListDto>>();
            return new PagedList<UserListDto>(count, userListDtos);
        }

        [AbpAuthorize(PermissionNames.Pages_Setting_User_Edit,PermissionNames.Pages_Setting_User_Add)]
        public async Task<GetUserForEditOutput> GetUserForEdit(int? id)
        {
            //Getting all available roles
            var userRoleDtos = (await _roleManager.Roles
                .OrderBy(r => r.DisplayName)
                .Select(r => new UserRoleDto
                {
                    RoleId = r.Id,
                    RoleName = r.Name,
                    RoleDisplayName = r.DisplayName
                })
                .ToArrayAsync());


            var output = new GetUserForEditOutput
            {
                Roles = userRoleDtos
            };

            if (!id.HasValue)
            {
                //Creating a new user
                output.User = new UserEditDto { IsActive = true };

                foreach (var defaultRole in await _roleManager.Roles.Where(r => r.IsDefault).ToListAsync())
                {
                    var defaultUserRole = userRoleDtos.FirstOrDefault(ur => ur.RoleName == defaultRole.Name);
                    if (defaultUserRole != null)
                    {
                        defaultUserRole.IsAssigned = true;
                    }
                }
            }
            else
            {
                //Editing an existing user
                var user = await UserManager.GetUserByIdAsync(id.Value);

                output.User = user.MapTo<UserEditDto>();

                foreach (var userRoleDto in userRoleDtos)
                {
                    userRoleDto.IsAssigned = await UserManager.IsInRoleAsync(id.Value, userRoleDto.RoleName);
                }
            }
            return output;
        }

        [AbpAuthorize(PermissionNames.Pages_Setting_User_SetPermission)]
        public async Task<GetUserPermissionsForEditOutput> GetUserPermissionsForEdit(int id)
        {
            var user = await UserManager.GetUserByIdAsync(id);
            var permissions = PermissionManager.GetAllPermissions();
            var grantedPermissions = await UserManager.GetGrantedPermissionsAsync(user);

            return new GetUserPermissionsForEditOutput
            {
                Permissions = permissions.MapTo<List<FlatPermissionDto>>().OrderBy(p => p.DisplayName).ToList(),
                GrantedPermissionNames = grantedPermissions.Select(p => p.Name).ToList()
            };
        }

        [AbpAuthorize(PermissionNames.Pages_Setting_User_SetPermission)]
        public async Task UpdateUserPermissions(UpdateUserPermissionsInput input)
        {
            var user = await UserManager.GetUserByIdAsync(input.Id);
            var grantedPermissions = PermissionManager.GetPermissionsFromNamesByValidating(input.GrantedPermissionNames);
            await UserManager.SetGrantedPermissionsAsync(user, grantedPermissions);
        }

        public async Task CreateOrUpdateUser(CreateOrUpdateUserInput input)
        {
            if (input.User.Id.HasValue)
            {
                await UpdateUserAsync(input);
            }
            else
            {
                await CreateUserAsync(input);
            }
        }
        [AbpAuthorize(PermissionNames.Pages_Setting_User_Delete)]
        public async Task DeleteUser(int id)
        {
            if (id == AbpSession.GetUserId())
            {
                throw new UserFriendlyException(L("YouCanNotDeleteOwnAccount"));
            }

            var user = await UserManager.GetUserByIdAsync(id);
            CheckErrors(await UserManager.DeleteAsync(user));
        }


        [AbpAuthorize(PermissionNames.Pages_Setting_User_Edit)]
        protected virtual async Task UpdateUserAsync(CreateOrUpdateUserInput input)
        {
            Debug.Assert(input.User.Id != null, "input.User.Id should be set.");

            var user = await UserManager.FindByIdAsync(input.User.Id.Value);

            //Update user properties
            input.User.MapTo(user); //Passwords is not mapped (see mapping configuration)

            if (!input.User.Password.IsNullOrEmpty())
            {
                CheckErrors(await UserManager.ChangePasswordAsync(user, input.User.Password));
            }

            CheckErrors(await UserManager.UpdateAsync(user));

            //Update roles
            CheckErrors(await UserManager.SetRoles(user, input.AssignedRoleNames));
        
        }

        [AbpAuthorize(PermissionNames.Pages_Setting_User_Add)]
        protected virtual async Task CreateUserAsync(CreateOrUpdateUserInput input)
        {
            var user = input.User.MapTo<User>(); //Passwords is not mapped (see mapping configuration)
            user.TenantId = AbpSession.TenantId;

            //Set password
            if (!input.User.Password.IsNullOrEmpty())
            {
                CheckErrors(await UserManager.PasswordValidator.ValidateAsync(input.User.Password));
            }
            else
            {
                input.User.Password = User.CreateRandomPassword();
            }

            user.Password = new PasswordHasher().HashPassword(input.User.Password);

            //Assign roles
            user.Roles = new Collection<UserRole>();
            foreach (var roleName in input.AssignedRoleNames)
            {
                var role = await _roleManager.GetRoleByNameAsync(roleName);
                user.Roles.Add(new UserRole { RoleId = role.Id });
            }
    

            CheckErrors(await UserManager.CreateAsync(user));
            await CurrentUnitOfWork.SaveChangesAsync(); //To get new user's Id.

            //Notifications  新用户发通知消息
            //await _notificationSubscriptionManager.SubscribeToAllAvailableNotificationsAsync(user.TenantId, user.Id);
            //await _appNotifier.WelcomeToTheApplicationAsync(user);
        }

        private async Task FillRoleNames(List<UserListDto> userListDtos)
        {
            /* This method is optimized to fill role names to given list. */

            var distinctRoleIds = (
                from userListDto in userListDtos
                from userListRoleDto in userListDto.Roles
                select userListRoleDto.RoleId
                ).Distinct();

            var roleNames = new Dictionary<int, string>();
            foreach (var roleId in distinctRoleIds)
            {
                roleNames[roleId] = (await _roleManager.GetRoleByIdAsync(roleId)).DisplayName;
            }

            foreach (var userListDto in userListDtos)
            {
                foreach (var userListRoleDto in userListDto.Roles)
                {
                    userListRoleDto.RoleName = roleNames[userListRoleDto.RoleId];
                }

                userListDto.Roles = userListDto.Roles.OrderBy(r => r.RoleName).ToList();
            }
        }
    }
}