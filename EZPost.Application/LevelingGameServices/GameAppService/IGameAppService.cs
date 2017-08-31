using Abp.Application.Services;
using EZPost.Common.WebControl;
using EZPost.LevelingGameServices.GameAppService.Dto;

namespace EZPost.LevelingGameServices.GameAppService
{
    public interface IGameAppService:IApplicationService
    {
        IPagedList<GameListDto> Gets(GetGamesInput input);
       GetGameForEditOutput GetEdit(int? id);
       void CreateOrUpdate(CreateOrUpdateGameInput input);
        void Delete(int id);
    }
}