using EZPost.Common.WebControl;

namespace EZPost.Roles.Dto
{
    public class GetRolesInput: DataTablesParameters
    {
        public  string RoleName { set; get; }
    }
}