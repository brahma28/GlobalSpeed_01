using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTEST.Models;
using System.Diagnostics;

namespace MvcTEST.Controllers
{
    public class ActionController : GSController
    {
        private SpeedCourtConfigDbEntities db = new SpeedCourtConfigDbEntities();

        //
        // GET: /Action/

        public ActionResult Index()
        {            
            return View();
        }

        public ActionResult QUICKGAME()
        {
            //var tblspeedentries = db.tblSpeedEntries.Include(t => t.tblSensorType);
            //ViewBag.korisnik = GetCurrentUser();
            //// all users
            //var pl = db.getPlayerByUser(1, "");
            //ViewBag.players = from p in pl
            //                  select p;
            //// all games
            //var gm = db.getGameByUser(1, "");
            //ViewBag.games = from g in gm
            //                select g;

            ViewBag.userid = GetCurrentUser().ID;
            //ViewBag.gameid = new a
            var ss = new GameData { gameid = 26, userid = GetCurrentUser().ID };


            return View("PlayGame", ss);
        }

        public ActionResult PlayGame()
        {
            return View();
        }

        public ActionResult Igra()
        {
            Process iis = new Process();
            iis.StartInfo.FileName =  Request.MapPath("~/GAME/Igra.exe");
            iis.StartInfo.Arguments = "nešto";

            //iis.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            iis.Start();

            return View("Index");
        }
        public ActionResult StartGame()
        {

            var tblspeedentries = db.tblSpeedEntries.Include(t => t.tblSensorType);
            ViewBag.korisnik = GetCurrentUser();
            // all users
            var pl = db.getPlayerByUser(1, "");
            ViewBag.players = from p in pl
                              select p;
            // all games
            var gm = db.getGameByUser(1, ""); 
            ViewBag.games = from g in gm
                            select g;

            ViewBag.userid = new SelectList(pl, "id", "name");
            ViewBag.gameid = new SelectList(gm, "id", "name");

            return View();
        }

        [HttpPost]
        public ActionResult StartGame(GameData data)
        {

            
            return View("PlayGame", data);
        }
        public ActionResult ChooseGame()
        {
            return View("NothingYet");
        }

        public ActionResult Documents()
        {
            return View("NothingYet");
        }

        ////
        //// GET: /Action/Details/5

        //public ActionResult Details(int id = 0)
        //{
        //    tblSpeedEntry tblspeedentry = db.tblSpeedEntries.Find(id);
        //    if (tblspeedentry == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblspeedentry);
        //}

        ////
        //// GET: /Action/Create

        //public ActionResult Create()
        //{
        //    ViewBag.SensorTypeID = new SelectList(db.tblSensorTypes, "ID", "SensorType");
        //    return View();
        //}

        ////
        //// POST: /Action/Create

        //[HttpPost]
        //public ActionResult Create(tblSpeedEntry tblspeedentry)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.tblSpeedEntries.Add(tblspeedentry);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.SensorTypeID = new SelectList(db.tblSensorTypes, "ID", "SensorType", tblspeedentry.SensorTypeID);
        //    return View(tblspeedentry);
        //}

        ////
        //// GET: /Action/Edit/5

        //public ActionResult Edit(int id = 0)
        //{
        //    tblSpeedEntry tblspeedentry = db.tblSpeedEntries.Find(id);
        //    if (tblspeedentry == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.SensorTypeID = new SelectList(db.tblSensorTypes, "ID", "SensorType", tblspeedentry.SensorTypeID);
        //    return View(tblspeedentry);
        //}

        ////
        //// POST: /Action/Edit/5

        //[HttpPost]
        //public ActionResult Edit(tblSpeedEntry tblspeedentry)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(tblspeedentry).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.SensorTypeID = new SelectList(db.tblSensorTypes, "ID", "SensorType", tblspeedentry.SensorTypeID);
        //    return View(tblspeedentry);
        //}

        ////
        //// GET: /Action/Delete/5

        //public ActionResult Delete(int id = 0)
        //{
        //    tblSpeedEntry tblspeedentry = db.tblSpeedEntries.Find(id);
        //    if (tblspeedentry == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblspeedentry);
        //}

        ////
        //// POST: /Action/Delete/5

        //[HttpPost, ActionName("Delete")]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    tblSpeedEntry tblspeedentry = db.tblSpeedEntries.Find(id);
        //    db.tblSpeedEntries.Remove(tblspeedentry);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    db.Dispose();
        //    base.Dispose(disposing);
        //}
    }
}