using System.Globalization;
using Abp.Authorization;
using Abp.Configuration;
using Abp.Net.Mail;
using EZPost.Authorization;
using EZPost.Configuration.Dto;

namespace EZPost.Configuration
{
    [AbpAuthorize(PermissionNames.Pages_Setting_Configuration)]
    public class SettingAppService:EZPostAppServiceBase,ISettingAppService
    {
        public SettingEditDto GetAllSettings()
        {
            var setting = new SettingEditDto
            {
                //Email
                EmailSetting = new EmailSettingDto
                {
                    DefaultFromAddress = SettingManager.GetSettingValue(EmailSettingNames.DefaultFromAddress),
                    DefaultFromDisplayName = SettingManager.GetSettingValue(EmailSettingNames.DefaultFromDisplayName),
                    SmtpHost = SettingManager.GetSettingValue(EmailSettingNames.Smtp.Host),
                    SmtpPort = SettingManager.GetSettingValue<int>(EmailSettingNames.Smtp.Port),
                    SmtpUserName = SettingManager.GetSettingValue(EmailSettingNames.Smtp.UserName),
                    SmtpPassword = SettingManager.GetSettingValue(EmailSettingNames.Smtp.Password),
                    SmtpDomain = SettingManager.GetSettingValue(EmailSettingNames.Smtp.Domain),
                    SmtpEnableSsl = SettingManager.GetSettingValue<bool>(EmailSettingNames.Smtp.EnableSsl),
                    SmtpUseDefaultCredentials =SettingManager.GetSettingValue<bool>(EmailSettingNames.Smtp.UseDefaultCredentials)
                }
            };
            return setting;
        }

        public void UpdateAllSettings(SettingEditDto input)
        {
            //Email
             SettingManager.ChangeSettingForApplication(EmailSettingNames.DefaultFromAddress, input.EmailSetting.DefaultFromAddress);
             SettingManager.ChangeSettingForApplication(EmailSettingNames.DefaultFromDisplayName, input.EmailSetting.DefaultFromDisplayName);
             SettingManager.ChangeSettingForApplication(EmailSettingNames.Smtp.Host, input.EmailSetting.SmtpHost);
             SettingManager.ChangeSettingForApplication(EmailSettingNames.Smtp.Port, input.EmailSetting.SmtpPort.ToString(CultureInfo.InvariantCulture));
             SettingManager.ChangeSettingForApplication(EmailSettingNames.Smtp.UserName, input.EmailSetting.SmtpUserName);
             SettingManager.ChangeSettingForApplication(EmailSettingNames.Smtp.Password, input.EmailSetting.SmtpPassword);
             SettingManager.ChangeSettingForApplication(EmailSettingNames.Smtp.Domain, input.EmailSetting.SmtpDomain);
             SettingManager.ChangeSettingForApplication(EmailSettingNames.Smtp.EnableSsl, input.EmailSetting.SmtpEnableSsl.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
             SettingManager.ChangeSettingForApplication(EmailSettingNames.Smtp.UseDefaultCredentials, input.EmailSetting.SmtpUseDefaultCredentials.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
        }
    }
}