using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Abp.Configuration.Startup;
using Abp.Hangfire;
using Abp.Hangfire.Configuration;
using Abp.IO;
using Abp.Zero.Configuration;
using Abp.Modules;
using Abp.Web.Mvc;
using Abp.Web.SignalR;
using EZPost.Api;
using Hangfire;

namespace EZPost.Web
{
    [DependsOn(
        typeof(EZPostDataModule),
        typeof(EZPostApplicationModule),
        typeof(EZPostWebApiModule),
        typeof(AbpWebSignalRModule),
        typeof(AbpHangfireModule), // ENABLE TO USE HANGFIRE INSTEAD OF DEFAULT JOB MANAGER
        typeof(AbpWebMvcModule))]
    public class EZPostWebModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Enable database based localization
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            //Configure navigation/menu
            Configuration.Navigation.Providers.Add<EZPostNavigationProvider>();

            //暂时禁用AntiForgery
            Configuration.Modules.AbpWeb().AntiForgery.IsEnabled = false;


            //Configure Hangfire - ENABLE TO USE HANGFIRE INSTEAD OF DEFAULT JOB MANAGER
            Configuration.BackgroundJobs.UseHangfire(configuration =>
            {
                configuration.GlobalConfiguration.UseSqlServerStorage("Default");
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public override void PostInitialize()
        {
            var server = HttpContext.Current.Server;
            var appFolders = IocManager.Resolve<AppFolders>();

            appFolders.Images = server.MapPath("~/Temp/Images/");
            appFolders.ReturnImages = "/Temp/Images/";
            try { DirectoryHelper.CreateIfNotExists(appFolders.Images); } catch { }
        }
    }
}
