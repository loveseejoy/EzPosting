using System.Linq;
using Abp.Configuration;
using Abp.Localization;
using Abp.Net.Mail;
using EZPost.EntityFramework;

namespace EZPost.Migrations.SeedData
{
    public class DefaultSettingsCreator
    {
        private readonly EZPostDbContext _context;

        public DefaultSettingsCreator(EZPostDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            //Emailing
            AddSettingIfNotExists(EmailSettingNames.DefaultFromAddress, "no-reply@linexsolutions.com");
            AddSettingIfNotExists(EmailSettingNames.DefaultFromDisplayName, "no-reply@linexsolutions.com");

            //Languages
            AddSettingIfNotExists(LocalizationSettingNames.DefaultLanguage, "en");
        }

        private void AddSettingIfNotExists(string name, string value, int? tenantId = null)
        {
            if (_context.Settings.Any(s => s.Name == name && s.TenantId == tenantId && s.UserId == null))
            {
                return;
            }

            _context.Settings.Add(new Setting(tenantId, null, name, value));
            _context.SaveChanges();
        }
    }
}