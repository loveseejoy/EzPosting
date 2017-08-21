using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using EZPost.Common.WebControl;
using EZPost.Users.Dto;

namespace EZPost.Users
{
    public interface IUserAppService : IApplicationService
    {

        Task<IPagedList<UserListDto>> GetUsers(GetUserInput input);

        Task<GetUserForEditOutput> GetUserForEdit(int? id);

        Task<GetUserPermissionsForEditOutput> GetUserPermissionsForEdit(int id);

        Task UpdateUserPermissions(UpdateUserPermissionsInput input);

        Task CreateOrUpdateUser(CreateOrUpdateUserInput input);

        Task DeleteUser(int id);
    }
}