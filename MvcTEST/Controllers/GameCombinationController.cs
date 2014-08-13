using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTEST.Models;

namespace MvcTEST.Controllers
{
    public class GameCombinationController : Controller
    {
        private SpeedCourtConfigDbEntities db = new SpeedCourtConfigDbEntities();

        //
        // GET: /GameCombination/

        public ActionResult Index()
        {
            ViewBag.errori = "";
            ViewBag.gamecombination = db.vwGameCombinations.ToList();
            return View(db.tblGameCombinations.ToList());
            //return View(db.vwGameCombinations.ToList());
        }

        //
        // GET: /GameCombination/Details/5

        public ActionResult Details(int id = 0)
        {
            ViewBag.errori = "";
            tblGameCombination tblgamecombination = db.tblGameCombinations.Find(id);
            ViewBag.gamecombination = db.vwGameCombinations.Where(gc => gc.GameCombinationID == id ).FirstOrDefault();
            if (tblgamecombination == null)
            {
                return HttpNotFound();
            }
            ViewBag.games = db.tblGames.Where(g => g.IsActiv == true).ToList();
            return View(tblgamecombination);
        }

        //
        // GET: /GameCombination/Create

        public ActionResult Create()
        {
            ViewBag.errori = "";
            string newid = db.getNewIDtblGameCombination().FirstOrDefault().ToString();
            tblGameCombination gc = new tblGameCombination();
            gc.CombinationName = "Combination " + newid;
            gc.Active = true;
            //gc.Modification = DateTime.Now;
            //gc.Modification = System.DateTime.ToShortDateString();
            gc.Modification = System.DateTime.Now;
            ViewBag.games = db.tblGames.Where(g => g.IsActiv == true).ToList();

            return View(gc);
        }

        //
        // POST: /GameCombination/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tblGameCombination tblgamecombination)
        {
            ViewBag.errori = "";
            if (ModelState.IsValid)
            {
                db.tblGameCombinations.Add(tblgamecombination);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.games = db.tblGames.Where(g => g.IsActiv == true).ToList();
            return View(tblgamecombination);
        }

        //
        // GET: /GameCombination/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ViewBag.errori = "";
            tblGameCombination tblgamecombination = db.tblGameCombinations.Find(id);
            if (tblgamecombination == null)
            {
                return HttpNotFound();
            }
            ViewBag.Modification = db.tblGameCombinations.Where(gc => gc.GameCombinationID == id).FirstOrDefault();
            ViewBag.games = db.tblGames.Where(g => g.IsActiv == true).ToList();
            return View(tblgamecombination);
        }

        //
        // POST: /GameCombination/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblGameCombination tblgamecombination)
        {
            ViewBag.errori = "";
            if (ModelState.IsValid)
            {
                db.Entry(tblgamecombination).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Modification = db.tblGameCombinations.Where(gc => gc.GameCombinationID == tblgamecombination.GameCombinationID).FirstOrDefault();
            ViewBag.games = db.tblGames.Where(g => g.IsActiv == true).ToList();
            return View(tblgamecombination);
        }

        //
        // GET: /GameCombination/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ViewBag.errori = "";
            tblGameCombination tblgamecombination = db.tblGameCombinations.Find(id);
            if (tblgamecombination == null)
            {
                return HttpNotFound();
            }
            ViewBag.games = db.tblGames.Where(g => g.IsActiv == true).ToList();
            return View(tblgamecombination);
        }

        //
        // POST: /GameCombination/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.errori = "";
            tblGameCombination tblgamecombination = db.tblGameCombinations.Find(id);
            db.tblGameCombinations.Remove(tblgamecombination);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}