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
    public class GameSpeedChaseController : GSController
    {
        private SpeedCourtConfigDbEntities db = new SpeedCourtConfigDbEntities();

        //
        // GET: /GameSpeedChase/

        public ActionResult Index()
        {
            var tblgames = db.tblGames.Include(t => t.tblCategory);
            return View(tblgames.ToList());
        }

        //
        // GET: /GameSpeedChase/Details/5

        public ActionResult Details(int id = 0)
        {
            tblGame tblgame = db.tblGames.Find(id);
            if (tblgame == null)
            {
                return HttpNotFound();
            }
            return View(tblgame);
        }

        //
        // GET: /GameSpeedChase/Create

        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.tblCategories, "ID", "Name");
            return View();
        }

        //
        // POST: /GameSpeedChase/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tblGame tblgame)
        {
            if (ModelState.IsValid)
            {
                db.tblGames.Add(tblgame);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.tblCategories, "ID", "Name", tblgame.CategoryID);
            return View(tblgame);
        }

        //
        // GET: /GameSpeedChase/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tblGame tblgame = db.tblGames.Find(id);
            if (tblgame == null)
            {
                return HttpNotFound();
            }
            ViewBag.game = db.vwGameFull.Where(g => g.ID == id).FirstOrDefault();
            ViewBag.CategoryID = new SelectList(db.tblCategories, "ID", "Name", tblgame.CategoryID);
            return View(tblgame);
        }

        //
        // POST: /GameReaction/Edit/5

        [HttpPost]
        public ActionResult Edit(tblGame tblgame)
        {
            if (ModelState.IsValid)
            {
                tblgame.ReactionTime = (tblgame.ReactionTime != null) ? TimeSpan.FromMilliseconds(tblgame.ReactionTime.Value.Days) : TimeSpan.FromMilliseconds(1);
                db.Entry(tblgame).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Games", "Configurator");
            }
            ViewBag.CategoryID = new SelectList(db.tblCategories, "ID", "Name", tblgame.CategoryID);
            return View(tblgame);
        }

        //
        // GET: /GameSpeedChase/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tblGame tblgame = db.tblGames.Find(id);
            if (tblgame == null)
            {
                return HttpNotFound();
            }
            return View(tblgame);
        }

        //
        // POST: /GameSpeedChase/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblGame tblgame = db.tblGames.Find(id);
            db.tblGames.Remove(tblgame);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public string SaveAs3(string Name = "chase ...", bool IsRandom = false, int RandomValue = 0, short RandomType = 0, bool IsTimeLimit = false, int TimeLimit = 0, string Description = "", string Objectives = "", string Definition = "", int NumShownFields = 0, int NumRepetition = 1, string RandomFields = "", bool QuickGamePool = false, int CategoryID = 0, TimeSpan? MinStayTime = null, TimeSpan? MaxStayTime = null, TimeSpan? MinFlyTime = null, TimeSpan? MaxFlyTime = null, int StartField = 0, int Countdown = 0, bool LegLeft = false, bool LegRight = false, int Quantity = 0, int Distance = 0, int ReactionSignalID = 0, int Reaction = 0, TimeSpan? ReactionTime = null, int ReactionTypeID = 0, int MemoryTypeID = 0)
        {
            //var sng = db.setSaveAss(Name, IsRandom, RandomValue, RandomType, IsTimeLimit, TimeLimit, Description, Objectives, Definition, NumShownFields, NumRepetition, RandomFields, QuickGamePool, CategoryID, MinStayTime, MaxStayTime, MinFlyTime, MaxFlyTime, StartField, Countdown, LegLeft, LegRight, Quantity, Distance);
            // CategoryID = 1 ( chase )
            var sng = db.setSaveAs3(Name, IsRandom, RandomValue, RandomType, IsTimeLimit, TimeLimit, Description, Objectives, Definition, NumShownFields, NumRepetition, RandomFields, QuickGamePool, 1, MinStayTime, MaxStayTime, MinFlyTime, MaxFlyTime, StartField, Countdown, LegLeft, LegRight, Quantity, Distance, ReactionSignalID, Reaction, ReactionTime, ReactionTypeID, MemoryTypeID);
            return sng.ToString();
            //return RedirectToAction("Games", "Configurator");
        }


    }
}