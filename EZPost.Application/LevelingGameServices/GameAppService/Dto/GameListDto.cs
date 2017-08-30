using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using EZPost.LevelingGame;

namespace EZPost.LevelingGameServices.GameAppService.Dto
{
    [AutoMapFrom(typeof(Game))]
    public class GameListDto:EntityDto
    {
        public string GameName { set; get; }

        public string GameDesc { set; get; }

        public string GamePic { set; get; }
    }
}