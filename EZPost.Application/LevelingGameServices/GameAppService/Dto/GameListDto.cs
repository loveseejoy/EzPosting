using Abp.Application.Services.Dto;

namespace EZPost.LevelingGameServices.GameAppService.Dto
{
    public class GameListDto:EntityDto
    {
        public string GameName { set; get; }

        public string GameDesc { set; get; }

        public string GamePic { set; get; }
    }
}