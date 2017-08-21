using Abp.Auditing;
using EZPost.Users;

namespace EZPost.AuditingLog
{
    public class AuditLogAndUser
    {
        public AuditLog AuditLog { get; set; }

        public User User { get; set; }
    }
}