using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.AutoMapper;

namespace EZPost.Users.Dto
{
  
    public class CreateOrUpdateUserInput
    {
        [Required]
        public UserEditDto User { get; set; }

        [Required(ErrorMessage ="������ѡ��һ����ɫ")]
        public string[] AssignedRoleNames { get; set; }

        public string AgentCustomerValue { set; get; }
    }
}