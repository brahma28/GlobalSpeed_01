using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTEST.Models;
using System.Globalization;

namespace MvcTEST.Controllers
{
    public class ResultsController : GSController
    {
        private SpeedCourtConfigDbEntities db = new SpeedCourtConfigDbEntities();

        //
        // GET: /Results/

        public ActionResult Index()
        {
            var tblspeedentries = db.tblSpeedEntries.Include(t => t.tblSensorType);
            //return View(tblspeedentries.ToList());

            ViewBag.korisnik = GetCurrentUser();

            var ss = Request.Cookies["user"];

            var gm = db.getGameByUser(GetCurrentUser().ID, "");
            ViewBag.games = from g in gm
                             select g;
            var gr = db.getGroupByUser(GetCurrentUser().ID, "");
            ViewBag.groups = from gg in gr
                              select gg;
            var pl = db.getPlayerByUser(GetCurrentUser().ID, "");
            ViewBag.players = from p in pl
                              select p;

            return View();


        }
        [HttpPost]
        public ActionResult AjaxSearch(string Search)
        {

            Search = HttpUtility.HtmlDecode(Search);

            var gm = db.getGameByUser(GetCurrentUser().ID, Search);
            ViewBag.games = from g in gm
                             select g;
            var gr = db.getGroupByUser(GetCurrentUser().ID, Search);
            ViewBag.groups = from gg in gr
                              select gg;
            var pl = db.getPlayerByUser(GetCurrentUser().ID, Search);
            ViewBag.players = from p in pl
                              select p;

            return View("Result_Search");


        }

        //
        // GET: /Results/Details/5

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
        // GET: /Results/Create

        public ActionResult Create()
        {
            ViewBag.SensorTypeID = new SelectList(db.tblSensorTypes, "ID", "SensorType");
            return View();
        }

        //
        // POST: /Results/Create

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
        // GET: /Results/Edit/5

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
        // POST: /Results/Edit/5

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
        // GET: /Results/Delete/5

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
        // POST: /Results/Delete/5

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