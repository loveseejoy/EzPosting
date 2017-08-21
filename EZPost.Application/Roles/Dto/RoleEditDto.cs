using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using EZPost.Authorization.Roles;

namespace EZPost.Roles.Dto
{
    [AutoMap(typeof(Role))]
    public class RoleEditDto
    {
        public int? Id { get; set; }

        [Required(ErrorMessage ="角色名不能为空")]
        public string DisplayName { get; set; }

        public bool IsDefault { get; set; }
    }
}