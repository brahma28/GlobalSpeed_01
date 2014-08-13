using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTEST.Models;
using System.Globalization;
using System.Threading;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Net.Mail;
using System.Text;
using System.Net;
using MvcTEST.HtmlHelpers;


namespace MvcTEST.Controllers
{
    public class GameDMSQuickController : GSController
    {
        private SpeedCourtConfigDbEntities db = new SpeedCourtConfigDbEntities();

        //
        // GET: /GameDMSQuick/

        public ActionResult Index()
        {
            var tblgames = db.tblGames.Include(t => t.tblCategory);
            return View(tblgames.ToList());
        }

        //
        // GET: /GameDMSQuick/Details/5

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
        // GET: /GameDMSQuick/Create

        public ActionResult Create()
        {
            string newid = db.getNewIDtblGame().FirstOrDefault().ToString();
            ViewBag.game = "DMS Quick " + newid;
            ViewBag.CategoryName = "DMS QUICK";
            ViewBag.CategoryID = new SelectList(db.tblCategories, "ID", "Name");
            tblGame gg = new tblGame();
            gg.Countdown = 3;
            gg.Description = LanguageExtensions.Translate("Bitte beschreiben Sie das Spiele!");
            gg.DescriptionDuration = 3;
            gg.DescriptionDurationSecMin = false;
            //gg.MaxFlyTime = 5;
            gg.MinFlyTime = 50;
            //gg.MaxStayTime = 5;
            gg.MinStayTime = 50;
            gg.NumShownFields = 2;
            gg.TimeLimit = 10;
            gg.TimeLimitSecMin = false;
            gg.StartField = "1";
            gg.StartFieldID = 1;
            //gg.StartField = 0;
            gg.NumRepetition = 1;
            gg.NumberErrors = 3;
            gg.NumberCycles = 3;
            gg.ReactionTypeID = 1;
            gg.LegLeft = true;
            gg.LegRight = false;
            gg.QuickGamePool = false;
            gg.IsActiv = true;
            gg.CategoryID = 11; // "DMS Quick" - tblCategory
            gg.RandomFields = db.getTrainingDirection(1).FirstOrDefault(); // gg.ReactionTypeID
            gg.RandomFieldsID = db.getTrainingDirectionID(1).FirstOrDefault(); // gg.ReactionTypeID
            return View(gg);
        }

        //
        // POST: /GameDMSQuick/Create

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(tblGame tblgame)
        {
            string newid = db.getNewIDtblGame().FirstOrDefault().ToString();
            ViewBag.game = "DMS Quick " + newid;

            if (ModelState.IsValid)
            {
                db.tblGames.Add(tblgame);
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("Games", "Configurator");
            }
            ViewBag.CategoryName = "DMS QUICK";
            ViewBag.CategoryID = new SelectList(db.tblCategories, "ID", "Name", tblgame.CategoryID);
            return View(tblgame);
        }

        //
        // GET: /GameDMSQuick/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tblGame tblgame = db.tblGames.Find(id);
            if (tblgame == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryName = "DMS QUICK";
            ViewBag.CategoryID = new SelectList(db.tblCategories, "ID", "Name", tblgame.CategoryID);
            return View(tblgame);
        }

        //
        // POST: /GameDMSQuick/Edit/5

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(tblGame tblgame)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblgame).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("Games", "Configurator");
            }
            ViewBag.CategoryName = "DMS QUICK";
            ViewBag.CategoryID = new SelectList(db.tblCategories, "ID", "Name", tblgame.CategoryID);
            return View(tblgame);
        }

        //
        // GET: /GameDMSQuick/Delete/5

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
        // POST: /GameDMSQuick/Delete/5

        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblGame tblgame = db.tblGames.Find(id);
            db.tblGames.Remove(tblgame);
            db.SaveChanges();
            //return RedirectToAction("Index");
            return RedirectToAction("Games", "Configurator");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        [HttpPost]
        public string SetTrainingDirection(int gameID, int reactionTypeID)
        {

            var stc = db.setTrainingDirection(gameID, reactionTypeID);

            string rndfld = db.getTrainingDirectionID(reactionTypeID).FirstOrDefault();

            return rndfld;

        }
        [HttpPost]
        public string GetTrainingDirection(int reactionTypeID)
        {

            string rndfld = db.getTrainingDirectionID(reactionTypeID).FirstOrDefault();


            return rndfld;

        }
    }
}