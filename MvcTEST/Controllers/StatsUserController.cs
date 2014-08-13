
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
    public class StatsUserController : GSController
    {
        private SpeedCourtConfigDbEntities db = new SpeedCourtConfigDbEntities();

        //
        // GET: /StatsUser/

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

        public ActionResult UserIndex(int UserID = 0)
        {
            var tblresults = db.tblResults.Include(t => t.tblGame).Include(t => t.tblUser);

            ViewBag.korisnik = GetCurrentUser();
            var datumtermina = DateTime.Now;
            ViewBag.datumtermina = DateTime.Now;
            ViewBag.selecteddates = db.getDatepickerDates(new Nullable<int>(), new Nullable<int>(), UserID, datumtermina.Date).FirstOrDefault().rumpelstiltskin;
            var hs = db.getHighscores(new Nullable<int>(), UserID, datumtermina.Date);

            ViewBag.highscores3 = from h in hs
                                 select h;
            ViewBag.highscores = from h in hs
                                 select h;
            int userid = GetCurrentUser().ID;
            //ViewBag.parametri = (from p in db.tblStatsParameters 
            //                     where p.UserID == userid
            //                     select p).FirstOrDefault();
            ViewBag.parametri = db.getStatsParameter(userid).FirstOrDefault();
            ViewBag.user = db.tblUsers.Where(u => u.ID == UserID).FirstOrDefault();
            var usergroup = db.tblUserGroups.Where(ug => ug.UserID == UserID).FirstOrDefault();
            if (usergroup == null) ViewBag.groupname = string.Empty; else ViewBag.groupname = usergroup.Name;
            return View("Index");
        }

        public ActionResult GameIndex(int id = 0)
        {
            //ID-GameID; UserID
            var tblresults = db.tblResults.Include(t => t.tblGame).Include(t => t.tblUser);

            //return View(tblspeedentries.ToList());

            ViewBag.korisnik = GetCurrentUser();

            // 31 - tapping
            //  0 - user
            // '2013-03-25' - na dan
            // exec [dbo].[getHighscores] 31,3,'2013-03-25'
            var datumtermina = DateTime.Now;


            //var datumtermina = DateTime.ParseExact(dt, "MM/dd/yyyy",provider );
            //var datumtermina = new DateTime(2013, 03, 25);
            ViewBag.selecteddates = db.getDatepickerDates(id, new Nullable<int>(), new Nullable<int>(), new Nullable<DateTime>()).FirstOrDefault().rumpelstiltskin;
            ViewBag.datumtermina = DateTime.Now;
            //var hs = db.getHighscores(id, GetCurrentUser().ID, datumtermina);
            var hs = db.getHighscores(id, new Nullable<int>(), datumtermina.Date);
            //var hs = db.getHighscores(id, new Nullable<int>(), dt);
            ViewBag.highscores = from h in hs
                                 select h;
            ViewBag.highscores3 = from h in hs
                                 select h;
            //ViewBag.resultstitle = db.tblGames.Where(g => g.ID == id).FirstOrDefault().Name;
            ViewBag.game = db.tblGames.Where(g => g.ID == id).FirstOrDefault();
            ViewBag.grupa = string.Empty;
            //return View(tblresults.ToList());
            int userid = GetCurrentUser().ID;
            //ViewBag.parametri = (from p in db.tblStatsParameters
            //                     where p.UserID == userid
            //                     select p).FirstOrDefault();
            ViewBag.parametri = db.getStatsParameter(userid).FirstOrDefault();
            return View("Index");
        }

        //
        // GET: /StatsUser/Details/5

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
        // GET: /StatsUser/Create

        public ActionResult Create()
        {
            ViewBag.GameID = new SelectList(db.tblGames, "ID", "Name");
            ViewBag.UserID = new SelectList(db.tblUsers, "ID", "Username");
            return View();
        }

        //
        // POST: /StatsUser/Create

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
        // GET: /StatsUser/Edit/5

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
        // POST: /StatsUser/Edit/5

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
        // GET: /StatsUser/Delete/5

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
        // POST: /StatsUser/Delete/5

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
        public ActionResult getHighscores(int ID, DateTime datum)
        {
            ViewBag.datumtermina = datum;
            ViewBag.highscores = db.getHighscores(ID, new Nullable<int>(), datum.Date);
            ViewBag.highscores3 = db.getHighscores(ID, new Nullable<int>(), datum.Date);
            return View("highscores3");
        }

        public string getHighscoreDates(int ID, DateTime datum)
        {

            string datumi = null;
            datumi = db.getDatepickerDates(ID, null, null, datum.Date).FirstOrDefault().rumpelstiltskin;
            return datumi;
        }

        public ActionResult StatsUserIndex(int id = 0)
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
            ViewBag.parametri = (from p in db.tblStatsParameters //db.getStatsParameter(GetCurrentUser().ID);
                                 where p.UserID == userid
                                 select p).FirstOrDefault();

            return View("Index");
        }

    }
}