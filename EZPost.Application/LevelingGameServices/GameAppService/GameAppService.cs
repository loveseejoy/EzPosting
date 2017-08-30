using System.Collections.Generic;
using System.Linq;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using EZPost.Common;
using EZPost.Common.WebControl;
using EZPost.LevelingGame;
using EZPost.LevelingGameServices.GameAppService.Dto;

namespace EZPost.LevelingGameServices.GameAppService
{
    public class GameAppService: EZPostAppServiceBase,IGameAppService
    {
        private readonly IRepository<Game> _gameRepository;

        public GameAppService(IRepository<Game> gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public IPagedList<GameListDto> Gets(GetGamesInput input)
        {
            var query = _gameRepository.GetAll();
            var count =  query.Count();
            var list =  query.OrderBys(input).PageBy(input).ToList();
            var listDto  = list.MapTo<List<GameListDto>>();
            return new PagedList<GameListDto>(count, listDto);
        }

        public GetGameForEditOutput GetEdit(int? id)
        {
            if (!id.HasValue)
            {
                return new GetGameForEditOutput
                {
                    Game = new GameEditDto()
                };
            }
            else
            {
                var entity = _gameRepository.Get(id.Value);
                var model = entity.MapTo<GameEditDto>();
                return new GetGameForEditOutput
                {
                    Game = model
                };
            }
        }

        public void CreateOrUpdate(CreateOrUpdateGameInput input)
        {
            if (!input.Game.Id.HasValue)
            {
                var entity = input.Game.MapTo<Game>();
                _gameRepository.Insert(entity);
            }
            else
            {
                var entity = _gameRepository.Get(input.Game.Id.Value);
                input.Game.MapTo(entity);
                _gameRepository.Update(entity);
            }
        }

        public void DeleteRole(int id)
        {
           _gameRepository.Delete(id);
        }
    }
}