using System.Data.Common;
using System.Data.Entity;
using Abp.Zero.EntityFramework;
using EZPost.Authorization.Roles;
using EZPost.MultiTenancy;
using EZPost.Users;

namespace EZPost.EntityFramework
{
    public class EZPostDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public EZPostDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in EZPostDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of EZPostDbContext since ABP automatically handles it.
         */
        public EZPostDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public EZPostDbContext(DbConnection connection)
            : base(connection, true)
        {

        }
    }
}
