using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreFront.DATA.EF;
using System.Data.Entity;
using System.Data;
using PagedList;
using PagedList.Mvc;

namespace StoreFront2._0.Controllers
{
    public class FiltersController : Controller
    {
        private StoreFrontEntities1 db = new StoreFrontEntities1();

        public ActionResult GamesGrid()
        {
            ViewBag.Developers = db.Developers.Select(x => x.DeveloperName).ToList();

            List<VideoGame> games = db.VideoGames.ToList();
            return View(games);
        }

        public ActionResult Paging(string searchString, int page = 1)
        {
            int pageSize = 6;
            var games = db.VideoGames.OrderBy(g => g.GameTitle).ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                games = (from g in games
                         where g.GameTitle.ToLower().Contains(searchString.ToLower())
                         select g).ToList();
            }
            ViewBag.SearchString = searchString;

            return View(games.ToPagedList(page, pageSize));
        }
        // GET: Filters
        public ActionResult Index()
        {
            return View();
        }
    }
}