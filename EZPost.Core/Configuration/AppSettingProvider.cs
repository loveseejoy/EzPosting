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
      


                  //Email

                    new SettingDefinition(EmailSettingNames.DefaultFromAddress,"3816703@qq.com"),
                    new SettingDefinition(EmailSettingNames.DefaultFromDisplayName,"3816703@qq.com"),
                    new SettingDefinition(EmailSettingNames.Smtp.Host,"smtp.qq.com"),
                    new SettingDefinition(EmailSettingNames.Smtp.Port,"587"),
                    new SettingDefinition(EmailSettingNames.Smtp.UserName,"3816703@qq.com"),
                    new SettingDefinition(EmailSettingNames.Smtp.Password,"xxx"),
                    new SettingDefinition(EmailSettingNames.Smtp.Domain,""),
                    new SettingDefinition(EmailSettingNames.Smtp.EnableSsl,"true"),
                    new SettingDefinition(EmailSettingNames.Smtp.UseDefaultCredentials,"false")
             };
        }
    }
}