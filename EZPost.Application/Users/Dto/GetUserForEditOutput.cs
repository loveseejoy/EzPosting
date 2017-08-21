using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EZPost.Users.Dto
{
    public class GetUserForEditOutput
    {
        public UserEditDto User { get; set; }

        public UserRoleDto[] Roles { get; set; }

        public  string AssignedRoleNames { set; get; }
    }
}