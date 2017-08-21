using Abp.Application.Services;
using EZPost.Configuration.Dto;

namespace EZPost.Configuration
{
    public interface ISettingAppService:IApplicationService
    {
        SettingEditDto GetAllSettings();
        void UpdateAllSettings(SettingEditDto input);
    }
}