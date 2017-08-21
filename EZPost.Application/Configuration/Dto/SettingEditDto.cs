using System.ComponentModel.DataAnnotations;

namespace EZPost.Configuration.Dto
{
    public class SettingEditDto
    {
        [Required]
        public EmailSettingDto EmailSetting { set; get; }
    }
}