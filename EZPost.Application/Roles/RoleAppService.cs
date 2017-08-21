using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Extensions;
using EZPost.Authorization;
using EZPost.Authorization.Roles;
using EZPost.Common;
using EZPost.Common.WebControl;
using EZPost.Dto;
using EZPost.Roles.Dto;

namespace EZPost.Roles
{
    /* THIS IS JUST A SAMPLE. */
    [AbpAuthorize(PermissionNames.Pages_Setting_Role)]
    public class RoleAppService : EZPostAppServiceBase,IRoleAppService
    {
        private readonly RoleManager _roleManager;
        private readonly IPermissionManager _permissionManager;

        public RoleAppService(RoleManager roleManager, IPermissionManager permissionManager)
        {
            _roleManager = roleManager;
            _permissionManager = permissionManager;
        }


        public async Task<IPagedList<RolesDto>> GetRoles(GetRolesInput input)
        {
            var query =  _roleManager.Roles.WhereIf(!input.RoleName.IsNullOrWhiteSpace(),x => x.DisplayName == input.RoleName);
            var count = await query.CountAsync();
            var roles = await query.OrderBys(input).PageBy(input).ToListAsync();
            var rolesDtos = roles.MapTo<List<RolesDto>>();
            return  new PagedList<RolesDto>(count, rolesDtos);
        }
        [AbpAuthorize(PermissionNames.Pages_Setting_Role_Edit,PermissionNames.Pages_Setting_Role_Add)]
        public async Task<GetRoleForEditOutput> GetRoleForEdit(int? id)
        {
            var permissions = PermissionManager.GetAllPermissions();
            var grantedPermissions = new Permission[0];
            RoleEditDto roleEditDto;

            if (id.HasValue)
            {
                var role = await _roleManager.GetRoleByIdAsync(id.Value);
                grantedPermissions = (await _roleManager.GetGrantedPermissionsAsync(role)).ToArray();
                roleEditDto = role.MapTo<RoleEditDto>();
            }
            else
            {
                roleEditDto = new RoleEditDto();
            }

            return new GetRoleForEditOutput
            {
                Role = roleEditDto,
                Permissions = permissions.MapTo<List<FlatPermissionDto>>().OrderBy(p => p.DisplayName).ToList(),
                GrantedPermissionNames = grantedPermissions.Select(p => p.Name).ToList()
            };
        }
       
        public async Task CreateOrUpdateRole(CreateOrUpdateRoleInput input)
        {
            if (input.Role.Id.HasValue)
            {
                await UpdateRoleAsync(input);
            }
            else
            {
                await CreateRoleAsync(input);
            }
        }

        [AbpAuthorize(PermissionNames.Pages_Setting_Role_Edit)]
        protected virtual async Task UpdateRoleAsync(CreateOrUpdateRoleInput input)
        {
            Debug.Assert(input.Role.Id != null, "input.Role.Id should be set.");

            var role = await _roleManager.GetRoleByIdAsync(input.Role.Id.Value);
            role.DisplayName = input.Role.DisplayName;
            role.IsDefault = input.Role.IsDefault;
            await UpdateRolePermissions(role, input.GrantedPermissionNames);
        }

        [AbpAuthorize(PermissionNames.Pages_Setting_Role_Add)]
        protected virtual async Task CreateRoleAsync(CreateOrUpdateRoleInput input)
        {
            var role = new Role(AbpSession.TenantId, input.Role.DisplayName) { IsDefault = input.Role.IsDefault,Name=input.Role.DisplayName };
            CheckErrors(await _roleManager.CreateAsync(role));
            await CurrentUnitOfWork.SaveChangesAsync(); //It's done to get Id of the role.
            await UpdateRolePermissions(role, input.GrantedPermissionNames);
        }

        private async Task UpdateRolePermissions(Role role, List<string> grantedPermissionNames)
        {
            var grantedPermissions = _permissionManager
                .GetAllPermissions()
                .Where(p => grantedPermissionNames.Contains(p.Name))
                .ToList();
            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);
        }
        [AbpAuthorize(PermissionNames.Pages_Setting_Role_Delete)]
        public async Task DeleteRole(int id)
        {
            var role = await _roleManager.GetRoleByIdAsync(id);
            CheckErrors(await _roleManager.DeleteAsync(role));
        }
    }
}