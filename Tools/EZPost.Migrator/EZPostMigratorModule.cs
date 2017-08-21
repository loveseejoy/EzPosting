using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using EZPost.EntityFramework;

namespace EZPost.Migrator
{
    [DependsOn(typeof(EZPostDataModule))]
    public class EZPostMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<EZPostDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}