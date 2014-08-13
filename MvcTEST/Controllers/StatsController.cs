using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTEST.Models;
using Microsoft.Reporting.WebForms;
//using Models;



namespace MvcTEST.Controllers
{
    public class StatsController : GSController
    {
        private SpeedCourtConfigDbEntities db = new SpeedCourtConfigDbEntities();

        //
        // GET: /Stats/

        public ActionResult Index()
        {
            var tblspeedentries = db.tblSpeedEntries.Include(t => t.tblSensorType);
            //return View(tblspeedentries.ToList());

            ViewBag.korisnik = GetCurrentUser();


            var gm = db.getGameByUser(1, "");
            ViewBag.games = from g in gm
                            select g;
            var gr = db.getGroupByUser(1, "");
            ViewBag.groups = from gg in gr
                             select gg;
            var pl = db.getPlayerByUser(1, "");
            ViewBag.players = from p in pl
                              select p;

            return View();


        }
        //[Authorize]
        public ActionResult Index3()
        {
            var rezultati = db.tblResults.ToList();

            ViewBag.ReportName = "~/Reports/ProbnaPita.rdlc";
            ViewBag.ReportDataSource = new ReportDataSource
            {
                Name = "Rezultati",
                Value = rezultati 
            };
            return View();
        }

        public ActionResult Index2(int id = 0)
        {
            int userid = GetCurrentUser().ID;
            var bb = (from b in db.tblStatsParameters
                      where b.UserID == userid
                      select b).FirstOrDefault();
            //var rezultati = db.getHighscoresTop5(null, userid, DateTime.Parse(bb.DateFrom.Value.Date.ToString())).ToList();
            var rezultati = db.getHighscores(id, null, DateTime.Parse(bb.DateFrom.Value.Date.ToString())).ToList();
            var parametri = db.getStatsParameter(id).ToList();
            var parametrizastoru = new List<ReportParameter>();
            parametrizastoru.Add(new ReportParameter("GameID", id.ToString()));
            parametrizastoru.Add(new ReportParameter("UserID", userid.ToString()));
            parametrizastoru.Add(new ReportParameter("Datum", bb.DateFrom.Value.Date.ToString()));

            ViewBag.ReportName = "~/Reports/Game.rdlc";
            ViewBag.ReportDataSource = new ReportDataSource
            {
                Name = "DatasetGame", // ime dataset-a
                Value = rezultati
            };
            return View();
        }

        public ActionResult ReportGame(int id = 0)
        {
            int userid = GetCurrentUser().ID;
            var bb = (from b in db.tblStatsParameters
                      where b.UserID == userid
                      select b).FirstOrDefault();
            //var rezultati = db.getHighscoresTop5(null, userid, DateTime.Parse(bb.DateFrom.Value.Date.ToString())).ToList();
            var rezultati = db.getHighscores(id, null, DateTime.Parse(bb.DateFrom.Value.Date.ToString())).ToList();
            var parametri = db.getStatsParameter(id).ToList();
            var parametrizastoru = new List<ReportParameter>();
            parametrizastoru.Add(new ReportParameter("GameID", id.ToString()));
            parametrizastoru.Add(new ReportParameter("UserID", userid.ToString()));
            parametrizastoru.Add(new ReportParameter("Datum", bb.DateFrom.Value.Date.ToString()));

            ViewBag.ReportName = "~/Reports/Game.rdlc";
            ViewBag.ReportDataSource = new ReportDataSource
            {
                Name = "DatasetGame", // ime dataset-a
                Value = rezultati
            };
            return View();
        }

        public ActionResult ReportGroupe(int id = 0)
        {
            int userid = GetCurrentUser().ID;
            var bb = (from b in db.tblStatsParameters
                      where b.UserID == userid
                      select b).FirstOrDefault();
            //var rezultati = db.getHighscoresTop5(null, userid, DateTime.Parse(bb.DateFrom.Value.Date.ToString())).ToList();
            var rezultati = db.getHighscores(id, null, DateTime.Parse(bb.DateFrom.Value.Date.ToString())).ToList();
            var parametri = db.getStatsParameter(id).ToList();
            var parametrizastoru = new List<ReportParameter>();
            parametrizastoru.Add(new ReportParameter("GameID", id.ToString()));
            parametrizastoru.Add(new ReportParameter("UserID", userid.ToString()));
            parametrizastoru.Add(new ReportParameter("Datum", bb.DateFrom.Value.Date.ToString()));

            ViewBag.ReportName = "~/Reports/reportGroup.rdlc";
            ViewBag.ReportDataSource = new ReportDataSource
            {
                Name = "DatasetGame", // ime dataset-a
                Value = rezultati
            };
            return View();
        }

        public ActionResult ReportUser(int id = 0)
        {
            int userid = GetCurrentUser().ID;
            var bb = (from b in db.tblStatsParameters
                      where b.UserID == userid
                      select b).FirstOrDefault();
            //var rezultati = db.getHighscoresTop5(null, userid, DateTime.Parse(bb.DateFrom.Value.Date.ToString())).ToList();
            var rezultati = db.getHighscores(id, null, DateTime.Parse(bb.DateFrom.Value.Date.ToString())).ToList();
            var parametri = db.getStatsParameter(id).ToList();
            var parametrizastoru = new List<ReportParameter>();
            parametrizastoru.Add(new ReportParameter("GameID", id.ToString()));
            parametrizastoru.Add(new ReportParameter("UserID", userid.ToString()));
            parametrizastoru.Add(new ReportParameter("Datum", bb.DateFrom.Value.Date.ToString()));

            ViewBag.ReportName = "~/Reports/reportGroup.rdlc";
            ViewBag.ReportDataSource = new ReportDataSource
            {
                Name = "DatasetGame", // ime dataset-a
                Value = rezultati
            };
            return View();
        }

        //
        // GET: /Stats/Details/5

        public ActionResult Details(int id = 0)
        {
            tblSpeedEntry tblspeedentry = db.tblSpeedEntries.Find(id);
            if (tblspeedentry == null)
            {
                return HttpNotFound();
            }
            return View(tblspeedentry);
        }

        //
        // GET: /Stats/Create

        public ActionResult Create()
        {
            ViewBag.SensorTypeID = new SelectList(db.tblSensorTypes, "ID", "SensorType");
            return View();
        }

        //
        // POST: /Stats/Create

        [HttpPost]
        public ActionResult Create(tblSpeedEntry tblspeedentry)
        {
            if (ModelState.IsValid)
            {
                db.tblSpeedEntries.Add(tblspeedentry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SensorTypeID = new SelectList(db.tblSensorTypes, "ID", "SensorType", tblspeedentry.SensorTypeID);
            return View(tblspeedentry);
        }

        //
        // GET: /Stats/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tblSpeedEntry tblspeedentry = db.tblSpeedEntries.Find(id);
            if (tblspeedentry == null)
            {
                return HttpNotFound();
            }
            ViewBag.SensorTypeID = new SelectList(db.tblSensorTypes, "ID", "SensorType", tblspeedentry.SensorTypeID);
            return View(tblspeedentry);
        }

        //
        // POST: /Stats/Edit/5

        [HttpPost]
        public ActionResult Edit(tblSpeedEntry tblspeedentry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblspeedentry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SensorTypeID = new SelectList(db.tblSensorTypes, "ID", "SensorType", tblspeedentry.SensorTypeID);
            return View(tblspeedentry);
        }

        //
        // GET: /Stats/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tblSpeedEntry tblspeedentry = db.tblSpeedEntries.Find(id);
            if (tblspeedentry == null)
            {
                return HttpNotFound();
            }
            return View(tblspeedentry);
        }

        //
        // POST: /Stats/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tblSpeedEntry tblspeedentry = db.tblSpeedEntries.Find(id);
            db.tblSpeedEntries.Remove(tblspeedentry);
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