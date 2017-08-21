using EZPost.Common.WebControl;

namespace EZPost.Users.Dto
{
    public class GetUserInput: DataTablesParameters
    {
        public  string UserName { set; get; }
    }
}