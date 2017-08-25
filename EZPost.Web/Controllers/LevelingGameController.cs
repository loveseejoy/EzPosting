using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EZPost.Web.Controllers
{
    public class LevelingGameController : EZPostControllerBase
    {
        // GET: LevelingGame
        public ActionResult Index()
        {
            return View();
        }
    }
}