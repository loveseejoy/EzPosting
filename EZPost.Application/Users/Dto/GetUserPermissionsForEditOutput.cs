using System.Collections.Generic;
using EZPost.Dto;

namespace EZPost.Users.Dto
{
    public class GetUserPermissionsForEditOutput
    {
        public int Id { set; get; }

        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}