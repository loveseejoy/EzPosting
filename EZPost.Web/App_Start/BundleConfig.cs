using System.Web.Optimization;

namespace EZPost.Web
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            //VENDOR RESOURCES

            ////树形组件
            //bundles.Add(new StyleBundle("~/Content/scripts/plugins/tree/css").Include(
            //            "~/Content/scripts/plugins/tree/tree.css"));
            //bundles.Add(new ScriptBundle("~/Content/scripts/plugins/tree/js").Include(
            //           "~/Content/scripts/plugins/tree/tree.js"));

            //表单验证
            bundles.Add(new ScriptBundle("~/Content/scripts/plugins/validator/js").Include(
                      "~/Content/scripts/plugins/validator/validator.js"));
            //日期控件
            bundles.Add(new StyleBundle("~/Content/scripts/plugins/datetime/css").Include(
                        "~/Content/scripts/plugins/datetime/pikaday.css"));
            bundles.Add(new ScriptBundle("~/Content/scripts/plugins/datepicker/js").Include(
                       "~/Content/scripts/plugins/datetime/pikaday.js"));
            ////导向组件
            //bundles.Add(new StyleBundle("~/Content/scripts/plugins/wizard/css").Include(
            //            "~/Content/scripts/plugins/wizard/wizard.css"));
            //bundles.Add(new ScriptBundle("~/Content/scripts/plugins/wizard/js").Include(
            //           "~/Content/scripts/plugins/wizard/wizard.js"));
            //learun
            bundles.Add(new StyleBundle("~/Content/styles/lenton-ui.css").Include(
                        "~/Content/styles/lenton-ui.css"));
            bundles.Add(new ScriptBundle("~/Content/scripts/utils/js").Include(
                      "~/Content/scripts/utils/lenton-form.js",
                       "~/Content/scripts/utils/lenton-ui.js"
                       ));

            //~/Bundles/vendor/js/top (These scripts should be included in the head of the page)
            bundles.Add(
                new ScriptBundle("~/Bundles/vendor/js/top")
                    .Include(
                        "~/Abp/Framework/scripts/utils/ie10fix.js",
                        "~/Scripts/modernizr-2.8.3.js"
                    )
                );

            //~/Bundles/vendor/bottom (Included in the bottom for fast page load)
            bundles.Add(
                new ScriptBundle("~/Bundles/vendor/js/bottom")
                    .Include(
                        "~/Scripts/json2.min.js",

                        //"~/Scripts/jquery-2.2.0.min.js",
                        //"~/Scripts/jquery-ui-1.11.4.min.js",

                       // "~/Scripts/bootstrap.min.js",

                        "~/Scripts/moment-with-locales.min.js",
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.blockUI.js",
                        "~/Scripts/toastr.min.js",
                        "~/Scripts/sweetalert/sweet-alert.min.js",
                        "~/Scripts/others/spinjs/spin.js",
                        "~/Scripts/others/spinjs/jquery.spin.js",

                        "~/Abp/Framework/scripts/abp.js",
                        "~/Abp/Framework/scripts/libs/abp.jquery.js",
                        "~/Abp/Framework/scripts/libs/abp.toastr.js",
                        "~/Abp/Framework/scripts/libs/abp.blockUI.js",
                        "~/Abp/Framework/scripts/libs/abp.spin.js",
                        "~/Abp/Framework/scripts/libs/abp.sweet-alert.js"

                       // "~/Scripts/jquery.signalR-2.2.0.min.js"
                    )
                );

            //APPLICATION RESOURCES

            //~/Bundles/js
            bundles.Add(
                new ScriptBundle("~/Bundles/js")
                    .Include("~/js/main.js")
                );
        }
    }
}