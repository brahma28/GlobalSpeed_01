
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
    public class StatsGroupController : GSController
    {
        private SpeedCourtConfigDbEntities db = new SpeedCourtConfigDbEntities();

        //
        // GET: /StatsGroup/

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

        //
        // GET: /StatsGroup/Details/5
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
        // GET: /StatsGroup/Create

        public ActionResult Create()
        {
            ViewBag.GameID = new SelectList(db.tblGames, "ID", "Name");
            ViewBag.UserID = new SelectList(db.tblUsers, "ID", "Username");
            return View();
        }

        //
        // POST: /StatsGroup/Create

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
        // GET: /StatsGroup/Edit/5

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
        // POST: /StatsGroup/Edit/5

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
        // GET: /StatsGroup/Delete/5

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
        // POST: /StatsGroup/Delete/5

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

        public ActionResult getHighscores(DateTime datum)
        {
            //var datumtermina = DateTime.ParseExact(datum, "MM/dd/yyyy", provider);
            //var tblresults = db.tblResults.Include(t => t.tblGame).Include(t => t.tblUser);
            //ViewBag.korisnik = GetCurrentUser();

            ViewBag.datumtermina = datum;

            ViewBag.highscores = db.getHighscoresTop5(new Nullable<int>(), GetCurrentUser().ID, datum.Date);

            //IEnumerable<string> ch = db.getHighscoresTop5(new Nullable<int>(), GetCurrentUser().ID, datum.Date).Select(c => c.ToString() );

            ViewBag.highscores2 =  ViewBag.highscores;
            return View("highscores2");
        }

        public string getHighscoreDates(int ID, DateTime datum)
        {

            string datumi = null;
            datumi = db.getDatepickerDates(ID, null, null, datum.Date).FirstOrDefault().rumpelstiltskin;
            return datumi;
        }

        public ActionResult StatsGroupIndex(int id = 0)
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
            ViewBag.grupaID = id;
            int userid = GetCurrentUser().ID;
            //ViewBag.parametri = (from p in db.tblStatsParameters 
            //                     where p.UserID == userid
            //                     select p).FirstOrDefault();
            ViewBag.parametri = db.getStatsParameter(userid).FirstOrDefault();

            return View("Index");
        }
        public ActionResult GroupeIndex(int GroupeID = 0)
        {
            //ID-GameID; UserID
            var tblresults = db.tblResults.Include(t => t.tblGame).Include(t => t.tblUser);

            //return View(tblspeedentries.ToList());

            ViewBag.korisnik = GetCurrentUser();
            var datumtermina = DateTime.Now;
            ViewBag.selecteddates = db.getDatepickerDates(new Nullable<int>(), GroupeID, new Nullable<int>(), new Nullable<DateTime>()).FirstOrDefault().rumpelstiltskin;
            ViewBag.datumtermina = DateTime.Now;

            var hst5 = db.getHighscoresTop5(GroupeID, new Nullable<int>(), datumtermina.Date);

            ViewBag.highscores2 = db.getHighscoresTop5(GroupeID, new Nullable<int>(), datumtermina.Date);

            ViewBag.highscores = db.getHighscoresTop5(GroupeID, new Nullable<int>(), datumtermina.Date);
            int userid = ViewBag.korisnik.ID;
            //ViewBag.parametri = (from p in db.tblStatsParameters 
            //                     where p.UserID == userid
            //                     select p).FirstOrDefault();
            ViewBag.parametri = db.getStatsParameter(userid).FirstOrDefault();
            //ViewBag.usergroupe = db.tblUserGroups.Where(g => g.ID == GroupeID).FirstOrDefault();
            ViewBag.game = db.tblGames.Where(g => g.ID == GroupeID).FirstOrDefault();
            ViewBag.grupaID = GroupeID;
            ViewBag.usergroupe = db.getUserGroup(userid).FirstOrDefault();
            return View("Index");
        }
    }
}

