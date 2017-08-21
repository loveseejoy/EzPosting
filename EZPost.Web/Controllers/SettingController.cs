using Abp.Web.Mvc.Authorization;
using EZPost.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EZPost.Roles;
using EZPost.Roles.Dto;
using EZPost.Users;
using EZPost.Users.Dto;
using Abp.Extensions;
using Microsoft.Ajax.Utilities;

namespace EZPost.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Setting)]
    public class SettingController : EZPostControllerBase
    {
        private readonly IUserAppService _userAppService;
        private readonly IRoleAppService _roleAppService;
        public SettingController(IUserAppService userAppService,
            IRoleAppService roleAppService)
        {
            _userAppService = userAppService;
            _roleAppService = roleAppService;
        }

        #region Role
        [AbpMvcAuthorize(PermissionNames.Pages_Setting_Role)]
        public ActionResult Role()
        {
            return View();
        }

        [AbpMvcAuthorize(PermissionNames.Pages_Setting_Role)]
        public async  Task<JsonResult>GetRoles(GetRolesInput input)
        {
            var data = await _roleAppService.GetRoles(input);
            return DataJsonResult(data, input.Draw);
        }

        [AbpMvcAuthorize(PermissionNames.Pages_Setting_Role_Add,PermissionNames.Pages_Setting_Role_Edit)]
        public async Task<ActionResult> CreateOrUpdateRole(int? id)
        {
            var output = await _roleAppService.GetRoleForEdit(id);
            return View(output);
        }

        [AbpMvcAuthorize(PermissionNames.Pages_Setting_Role_Delete)]
        public async Task<JsonResult> DeleteRoe()
        {
            var id = Convert.ToInt32(Request.Params["id"]);
            await _roleAppService.DeleteRole(id);
            return Success("Success");
        }

        #endregion

        #region User
        [AbpMvcAuthorize(PermissionNames.Pages_Setting_User)]
        public ActionResult Users()
        {
            return View();
        }

        [AbpMvcAuthorize(PermissionNames.Pages_Setting_User)]
        public async Task<JsonResult> GetUsers(GetUserInput input)
        {
            var data = await _userAppService.GetUsers(input);
            return DataJsonResult(data, input.Draw);
        }

        [AbpMvcAuthorize(PermissionNames.Pages_Setting_User_Add, PermissionNames.Pages_Setting_User_Edit)]
        public async Task<ActionResult> CreateOrUpdateUser(int? id)
        {
            var model = await _userAppService.GetUserForEdit(id);
            return View(model);
        }

        [AbpMvcAuthorize(PermissionNames.Pages_Setting_User_Add, PermissionNames.Pages_Setting_User_Edit)]
        public async Task<JsonResult> SaveUser(CreateOrUpdateUserInput input)
        {
            ////将所有角色转为数组
            var assignedRoleNames = input.AssignedRoleNames.FirstOrDefault().Split(",", StringSplitOptions.RemoveEmptyEntries);
            input.AssignedRoleNames = assignedRoleNames.ToArray();
            //随创建一个email
            input.User.EmailAddress = input.User.EmailAddress ?? Guid.NewGuid().ToString()+"ezposot@ezpost.com";
            //name,urname默认等于username
            input.User.Name = input.User.UserName;
            input.User.Surname = input.User.UserName;
            await    _userAppService.CreateOrUpdateUser(input);
            return Success("Success");
        }

        [AbpMvcAuthorize(PermissionNames.Pages_Setting_User_Delete)]
        public async Task<JsonResult> DeleteUser()
        {
            var id = Convert.ToInt32(Request.Params["id"]);
            await _userAppService.DeleteUser(id);
            return Success("Success");
        }

        [AbpMvcAuthorize(PermissionNames.Pages_Setting_User_SetPermission)]
        public async Task<ActionResult>UserPermission(int id)
        {
            var model = await _userAppService.GetUserPermissionsForEdit(id);
            model.Id = id;
            return View(model);
        }

        [AbpMvcAuthorize(PermissionNames.Pages_Setting_User_SetPermission)]
        public async Task<JsonResult> SaveUserPermission(UpdateUserPermissionsInput input)
        {
            //将所有用户权限转为数组
            var grantedPermissionNameArray = input.GrantedPermissionNames.FirstOrDefault().Split(",", StringSplitOptions.RemoveEmptyEntries);
            input.GrantedPermissionNames = grantedPermissionNameArray.ToList();
            await _userAppService.UpdateUserPermissions(input);
            return Success("Success");
        }
        #endregion

        #region 配置设置
        [AbpMvcAuthorize(PermissionNames.Pages_Setting_Configuration)]
        public ActionResult ConfigurationSetting()
        {
            return View();
        }
        #endregion
    }
}