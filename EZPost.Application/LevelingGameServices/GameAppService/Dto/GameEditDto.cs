using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using EZPost.LevelingGame;

namespace EZPost.LevelingGameServices.GameAppService.Dto
{

    [AutoMap(typeof(Game))]
    public class GameEditDto
    {
        public  int? Id { set; get; }

        [Required(ErrorMessage = "游戏名称必须填")]
        public string GameName { set; get; }

        public string GameDesc { set; get; }

        public string GamePic { set; get; }


    }
}