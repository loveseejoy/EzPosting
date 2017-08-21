using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.AutoMapper;

namespace EZPost.Users.Dto
{
    [AutoMap(typeof(User))]
    public class UserEditDto
    {
        public int? Id { set; get; }

        [Required(ErrorMessage ="用户名必须输入")]
        [StringLength(AbpUserBase.MaxUserNameLength)]
        public string UserName { get; set; }

      
        [StringLength(User.MaxNameLength)]
        public string Name { get; set; }

      
        [StringLength(User.MaxSurnameLength)]
        public string Surname { get; set; }

    
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        [StringLength(User.MaxPlainPasswordLength)]
        [DisableAuditing]
        public string Password { get; set; }

        public bool IsActive { get; set; }

    }
}