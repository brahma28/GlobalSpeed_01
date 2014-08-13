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
    public class GameDMSJumpController : GSController
    {
        private SpeedCourtConfigDbEntities db = new SpeedCourtConfigDbEntities();

        //
        // GET: /GameDMSJump/

        public ActionResult Index()
        {
            var tblgames = db.tblGames.Include(t => t.tblCategory);
            return View(tblgames.ToList());
        }

        //
        // GET: /GameDMSJump/Details/5

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
        // GET: /GameDMSJump/Create

        public ActionResult Create()
        {
            string newid = db.getNewIDtblGame().FirstOrDefault().ToString();
            ViewBag.game = "DMS Jump " + newid;
            ViewBag.CategoryName = "DMS JUMP";
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
            gg.IsActiv = true;
            //gg.StartField = 0;
            gg.NumRepetition = 1;
            gg.NumberErrors = 3;
            gg.NumberCycles = 3;
            gg.ReactionTypeID = 1;
            gg.LegLeft = true;
            gg.LegRight = false;
            gg.QuickGamePool = false;
            gg.CategoryID = 10; // "DMS Jump" - tblCategory
            return View(gg);
        }

        //
        // POST: /GameDMSJump/Create

        [HttpPost]
        public ActionResult Create(tblGame tblgame)
        {
            string newid = db.getNewIDtblGame().FirstOrDefault().ToString();
            ViewBag.game = "DMS Jump " + newid;
            ViewBag.CategoryName = "DMS JUMP";
            if (ModelState.IsValid)
            {
                db.tblGames.Add(tblgame);
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("Games", "Configurator");
            }

            ViewBag.CategoryID = new SelectList(db.tblCategories, "ID", "Name", tblgame.CategoryID);
            return View(tblgame);
        }

        //
        // GET: /GameDMSJump/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tblGame tblgame = db.tblGames.Find(id);

            if (tblgame == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryName = "JUMP";
            ViewBag.CategoryID = new SelectList(db.tblCategories, "ID", "Name", tblgame.CategoryID);
            return View(tblgame);
        }

        //
        // POST: /GameDMSJump/Edit/5

        [HttpPost]
        public ActionResult Edit(tblGame tblgame)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblgame).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("Games", "Configurator");
            }
            ViewBag.CategoryName = "JUMP";
            ViewBag.CategoryID = new SelectList(db.tblCategories, "ID", "Name", tblgame.CategoryID);
            return View(tblgame);
        }

        //
        // GET: /GameDMSJump/Delete/5

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
        // POST: /GameDMSJump/Delete/5

        [HttpPost, ActionName("Delete")]
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

        public string RandomFieldsAll(int ID)
        {
            tblGame tblgame = db.tblGames.Where(g => g.ID == ID).FirstOrDefault();
            ViewBag.teren = db.tblTemplates.Where(t => t.IsActive == true).FirstOrDefault();
            return tblgame.RandomFields;
        }

        public ActionResult RandomFields()
        {
            ViewBag.teren = db.tblTemplates.Where(g => g.IsActive == true).FirstOrDefault();
            return View();
        }




    }
}