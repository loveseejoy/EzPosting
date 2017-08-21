using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EZPost.Roles.Dto
{
    public class CreateOrUpdateRoleInput
    {
        [Required]
        public RoleEditDto Role { get; set; }

        [Required(ErrorMessage ="请选择权限")]
        public List<string> GrantedPermissionNames { get; set; }
    }
}