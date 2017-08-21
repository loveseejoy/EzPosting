using System.Threading.Tasks;
using Abp.Application.Services;
using EZPost.Authorization.Roles;
using EZPost.Common.WebControl;
using EZPost.Roles.Dto;

namespace EZPost.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task<IPagedList<RolesDto>> GetRoles(GetRolesInput input);
        Task<GetRoleForEditOutput> GetRoleForEdit(int? id);
        Task CreateOrUpdateRole(CreateOrUpdateRoleInput input);
        Task DeleteRole(int id);
    }
}
