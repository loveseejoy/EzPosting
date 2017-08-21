using Abp.Web.Mvc.Views;

namespace EZPost.Web.Views
{
    public abstract class EZPostWebViewPageBase : EZPostWebViewPageBase<dynamic>
    {

    }

    public abstract class EZPostWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected EZPostWebViewPageBase()
        {
            LocalizationSourceName = EZPostConsts.LocalizationSourceName;
        }
    }
}