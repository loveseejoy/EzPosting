using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EZPost.LevelingGameServices.GameAppService;
using EZPost.LevelingGameServices.GameAppService.Dto;

namespace EZPost.Web.Controllers
{
    public class GameController : EZPostControllerBase
    {

        private readonly IGameAppService _gameAppService;

        public GameController(IGameAppService gameAppService)
        {
            _gameAppService = gameAppService;
        }

        #region 游戏管理
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateOrUpdate(int? id)
        {
            return View(id);
        }

        [HttpPost]
        public JsonResult Gets(GetGamesInput input)
        {
            var data = _gameAppService.Gets(input);
            return DataJsonResult(data, input.Draw);
        }
        #endregion
    }
}