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
    public class GameResultsController : GSController
    {
        private SpeedCourtConfigDbEntities db = new SpeedCourtConfigDbEntities();


        //
        // GET: /GameResults/Details/5

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
        // GET: /GameResults/Create

        public ActionResult Create()
        {
            ViewBag.GameID = new SelectList(db.tblGames, "ID", "Name");
            ViewBag.UserID = new SelectList(db.tblUsers, "ID", "Username");
            return View();
        }

        //
        // POST: /GameResults/Create

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
        // GET: /GameResults/Edit/5

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
        // POST: /GameResults/Edit/5

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
        // GET: /GameResults/Delete/5

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
        // POST: /GameResults/Delete/5

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
    }
}