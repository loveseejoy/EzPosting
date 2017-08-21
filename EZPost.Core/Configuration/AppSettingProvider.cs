using System.Collections.Generic;
using System.Configuration;
using Abp.Configuration;
using Abp.Net.Mail;

namespace EZPost.Configuration
{
    public class AppSettingProvider: SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                 new SettingDefinition(AppSettings.DataBase.SamConnectionString,ConfigurationManager.AppSettings[AppSettings.DataBase.SamConnectionString]),
                 new SettingDefinition(AppSettings.DhlFtp.FtpAddress,ConfigurationManager.AppSettings[AppSettings.DhlFtp.FtpAddress]),
                 new SettingDefinition(AppSettings.DhlFtp.FtpPort,ConfigurationManager.AppSettings[AppSettings.DhlFtp.FtpPort]),
                  new SettingDefinition(AppSettings.DhlFtp.FtpUserName,ConfigurationManager.AppSettings[AppSettings.DhlFtp.FtpUserName]),
                  new SettingDefinition(AppSettings.DhlFtp.FtpPassword,ConfigurationManager.AppSettings[AppSettings.DhlFtp.FtpPassword]),
                  new SettingDefinition(AppSettings.DhlFtp.FtpPath,ConfigurationManager.AppSettings[AppSettings.DhlFtp.FtpPath]),


                  //Email

                    new SettingDefinition(EmailSettingNames.DefaultFromAddress,"no-reply@linexsolutions.com"),
                    new SettingDefinition(EmailSettingNames.DefaultFromDisplayName,"no-reply@linexsolutions.com"),
                    new SettingDefinition(EmailSettingNames.Smtp.Host,"smtp.hgcbizmail.com"),
                    new SettingDefinition(EmailSettingNames.Smtp.Port,"587"),
                    new SettingDefinition(EmailSettingNames.Smtp.UserName,"no-reply@linexsolutions.com"),
                    new SettingDefinition(EmailSettingNames.Smtp.Password,"2017Nor#liN%Sol"),
                    new SettingDefinition(EmailSettingNames.Smtp.Domain,""),
                    new SettingDefinition(EmailSettingNames.Smtp.EnableSsl,"true"),
                    new SettingDefinition(EmailSettingNames.Smtp.UseDefaultCredentials,"false")
             };
        }
    }
}