
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTEST.Models;
using System.Globalization;
using System.Net.Mail;
using System.Text;
using System.Net;

namespace MvcTEST.Controllers
{
    public class StatsGameController : GSController
    {
        private SpeedCourtConfigDbEntities db = new SpeedCourtConfigDbEntities();

        //
        // GET: /StatsGame/

        public ActionResult Index()
        {
            var tblresults = db.tblResults.Include(t => t.tblGame).Include(t => t.tblUser);
            ViewBag.parametri = db.getStatsParameter(GetCurrentUser().ID);
            return View(tblresults.ToList());
        }

        [HttpPost]
        public string AjaxSetStatsParameter(string radiobutton)
        {
            var sst = db.setStatsParameter(radiobutton, GetCurrentUser().ID);
            return string.Empty;
        }

        public string getHighscoreDates(int ID, DateTime datum)
        {

            string datumi = null;
            datumi = db.getDatepickerDates(ID, null, null, datum.Date).FirstOrDefault().rumpelstiltskin;
            return datumi;
        }

        public ActionResult getHighscores(int ID, DateTime datum)
        {
            //var datumtermina = DateTime.ParseExact(datum, "MM/dd/yyyy", provider);
            //var tblresults = db.tblResults.Include(t => t.tblGame).Include(t => t.tblUser);
            //ViewBag.korisnik = GetCurrentUser();

            ViewBag.datumtermina = datum;

            //ViewBag.highscores = db.getHighscores(ID, GetCurrentUser().ID, datum.Date);
            // ne treba na UserID !!!
            ViewBag.highscores = db.getHighscores(ID, new Nullable<int>(), datum.Date);
            ViewBag.highscores1 = db.getHighscores(ID, new Nullable<int>(), datum.Date);
            return View("highscores1");
        }

        //
        // GET: /StatsGame/Details/5

        public ActionResult Details(int id = 0)
        {
            tblResult tblresult = db.tblResults.Find(id);
            if (tblresult == null)
            {
                return HttpNotFound();
            }
            return View(tblresult);
        }

        //
        // GET: /StatsGame/Create

        public ActionResult Create()
        {
            ViewBag.GameID = new SelectList(db.tblGames, "ID", "Name");
            ViewBag.UserID = new SelectList(db.tblUsers, "ID", "Username");
            return View();
        }

        //
        // POST: /StatsGame/Create

        [HttpPost]
        public ActionResult Create(tblResult tblresult)
        {
            if (ModelState.IsValid)
            {
                db.tblResults.Add(tblresult);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GameID = new SelectList(db.tblGames, "ID", "Name", tblresult.GameID);
            ViewBag.UserID = new SelectList(db.tblUsers, "ID", "Username", tblresult.UserID);
            return View(tblresult);
        }

        //
        // GET: /StatsGame/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tblResult tblresult = db.tblResults.Find(id);
            if (tblresult == null)
            {
                return HttpNotFound();
            }
            ViewBag.GameID = new SelectList(db.tblGames, "ID", "Name", tblresult.GameID);
            ViewBag.UserID = new SelectList(db.tblUsers, "ID", "Username", tblresult.UserID);
            return View(tblresult);
        }

        //
        // POST: /StatsGame/Edit/5

        [HttpPost]
        public ActionResult Edit(tblResult tblresult)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblresult).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GameID = new SelectList(db.tblGames, "ID", "Name", tblresult.GameID);
            ViewBag.UserID = new SelectList(db.tblUsers, "ID", "Username", tblresult.UserID);
            return View(tblresult);
        }

        //
        // GET: /StatsGame/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tblResult tblresult = db.tblResults.Find(id);
            if (tblresult == null)
            {
                return HttpNotFound();
            }
            return View(tblresult);
        }

        //
        // POST: /StatsGame/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tblResult tblresult = db.tblResults.Find(id);
            db.tblResults.Remove(tblresult);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


        public ActionResult StatsGameIndex(int id = 0)
        {
            //ID-GameID; UserID
            var tblresults = db.tblResults.Include(t => t.tblGame).Include(t => t.tblUser);
            ViewBag.korisnik = GetCurrentUser();
            var datumtermina = DateTime.Now;
            ViewBag.selecteddates = db.getDatepickerDates(id, new Nullable<int>(), new Nullable<int>(), new Nullable<DateTime>()).FirstOrDefault().rumpelstiltskin;
            ViewBag.datumtermina = DateTime.Now;
            var hs = db.getHighscores(id, new Nullable<int>(), datumtermina.Date);
            ViewBag.highscores = from h in hs
                                 select h;
            ViewBag.game = db.tblGames.Where(g => g.ID == id).FirstOrDefault();
            ViewBag.grupa = string.Empty;
            int userid = GetCurrentUser().ID;
            //ViewBag.parametri = (from p in db.tblStatsParameters 
            //                 where p.UserID == userid
            //                 select p).FirstOrDefault();
            ViewBag.parametri = db.getStatsParameter(userid).FirstOrDefault();


            return View("Index");
        }

    }
}