using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreFront.DATA.EF;
using StoreFront.UI.MVC.Utilities;
using System.Drawing;
using StoreFront.UI.MVC.Models;
using StoreFront2._0.Models;

namespace StoreFront2._0.Controllers
{
    public class VideoGamesController : Controller
    {
        private StoreFrontEntities1 db = new StoreFrontEntities1();

        // GET: VideoGames
        public ActionResult Index()
        {
            var videoGames = db.VideoGames.Include(v => v.Developer).Include(v => v.Platform).Include(v => v.StockStatus);
            return View(videoGames.ToList());
        }

        // GET: VideoGames/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoGame videoGame = db.VideoGames.Find(id);
            if (videoGame == null)
            {
                return HttpNotFound();
            }
            return View(videoGame);
        }

        #region Add to Cart
        public ActionResult AddToCart(int quantity, int gameID)
        {
            Dictionary<int, CartItemViewModel> shoppingCart = null;

            if (Session["cart"] != null)
            {
                shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];
            }
            else
            {
                shoppingCart = new Dictionary<int, CartItemViewModel>();
            }

            VideoGame game = db.VideoGames.Where(g => g.GameID == gameID).FirstOrDefault();
            if (game == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                CartItemViewModel item = new CartItemViewModel(quantity, game);

                if (shoppingCart.ContainsKey(game.GameID))
                {
                    shoppingCart[game.GameID].Quantity += quantity;
                }
                else
                {
                    shoppingCart.Add(game.GameID, item);
                }

                Session["cart"] = shoppingCart;
            }

            return RedirectToAction("Index", "ShoppingCart");
        }
        #endregion

        // GET: VideoGames/Create
        public ActionResult Create()
        {
            ViewBag.DeveloperID = new SelectList(db.Developers, "DeveloperID", "DeveloperName");
            ViewBag.PlatformID = new SelectList(db.Platforms, "PlatfromID", "PlatformName");
            ViewBag.StatusID = new SelectList(db.StockStatuses, "StatusID", "StockStatus1");
            return View();
        }

        // POST: VideoGames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GameID,GameTitle,YearReleased,DeveloperID,PlatformID,StatusID,Description,IMG")] VideoGame videoGame, HttpPostedFileBase gameCover)
        {
            if (ModelState.IsValid)
            {
                string file = "NoImage.png";
                if (gameCover != null)
                {
                    file = gameCover.FileName;
                    string ext = file.Substring(file.LastIndexOf('.'));
                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif", ".jfif" };

                    if (goodExts.Contains(ext.ToLower()) && gameCover.ContentLength <= 4194304)
                    {
                        file = Guid.NewGuid() + ext;
                        #region Resize
                        string savePath = Server.MapPath("~/Content/images/");
                        Image convertedImage = Image.FromStream(gameCover.InputStream);
                        int maxImageSize = 500;
                        int maxThumbSize = 100;
                        ImageUtility.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);
                        #endregion
                    }

                    videoGame.IMG = file;
                }
                db.VideoGames.Add(videoGame);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeveloperID = new SelectList(db.Developers, "DeveloperID", "DeveloperName", videoGame.DeveloperID);
            ViewBag.PlatformID = new SelectList(db.Platforms, "PlatfromID", "PlatformName", videoGame.PlatformID);
            ViewBag.StatusID = new SelectList(db.StockStatuses, "StatusID", "StockStatus1", videoGame.StatusID);
            return View(videoGame);
        }

        // GET: VideoGames/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoGame videoGame = db.VideoGames.Find(id);
            if (videoGame == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeveloperID = new SelectList(db.Developers, "DeveloperID", "DeveloperName", videoGame.DeveloperID);
            ViewBag.PlatformID = new SelectList(db.Platforms, "PlatfromID", "PlatformName", videoGame.PlatformID);
            ViewBag.StatusID = new SelectList(db.StockStatuses, "StatusID", "StockStatus1", videoGame.StatusID);
            return View(videoGame);
        }

        // POST: VideoGames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GameID,GameTitle,YearReleased,DeveloperID,PlatformID,StatusID,Description,IMG")] VideoGame videoGame, HttpPostedFileBase gameCover)
        {
            if (ModelState.IsValid)
            {
                string file = "NoImage.png";
                if (gameCover != null)
                {
                    file = gameCover.FileName;
                    string ext = file.Substring(file.LastIndexOf('.'));
                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif", ".jfif" };

                    if (goodExts.Contains(ext.ToLower()) && gameCover.ContentLength <= 4194304)
                    {
                        file = Guid.NewGuid() + ext;
                        #region Resize
                        string savePath = Server.MapPath("~/Content/images/");
                        Image convertedImage = Image.FromStream(gameCover.InputStream);
                        int maxImageSize = 500;
                        int maxThumbSize = 100;
                        ImageUtility.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);
                        #endregion
                    }

                    videoGame.IMG = file;
                }
                db.Entry(videoGame).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeveloperID = new SelectList(db.Developers, "DeveloperID", "DeveloperName", videoGame.DeveloperID);
            ViewBag.PlatformID = new SelectList(db.Platforms, "PlatfromID", "PlatformName", videoGame.PlatformID);
            ViewBag.StatusID = new SelectList(db.StockStatuses, "StatusID", "StockStatus1", videoGame.StatusID);
            return View(videoGame);
        }

        // GET: VideoGames/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoGame videoGame = db.VideoGames.Find(id);
            if (videoGame == null)
            {
                return HttpNotFound();
            }
            return View(videoGame);
        }

        // POST: VideoGames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VideoGame videoGame = db.VideoGames.Find(id);
            string path = Server.MapPath("~/Content/images/");
            ImageUtility.Delete(path, videoGame.IMG);
            db.VideoGames.Remove(videoGame);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
