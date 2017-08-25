using System.ComponentModel.DataAnnotations;

namespace EZPost.LevelingGameServices.GameAppService.Dto
{
    public class CreateOrUpdateGameInput
    {
        [Required]
        public  GameEditDto Game { set; get; }
    }
}