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
    public class TerminsController : Controller
    {
        private SpeedCourtConfigDbEntities db = new SpeedCourtConfigDbEntities();

        //
        // GET: /Termins/

        public ActionResult Index()
        {
            var tblterms = db.tblTerms.Include(t => t.tblUser);
            return View(tblterms.ToList());
        }

        //
        // GET: /Termins/Details/5

        public ActionResult Details(int id = 0)
        {
            tblTerm tblterm = db.tblTerms.Find(id);
            if (tblterm == null)
            {
                return HttpNotFound();
            }
            return View(tblterm);
        }

        //
        // GET: /Termins/Create

        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.tblUsers, "ID", "Username");
            return View();
        }

        //
        // POST: /Termins/Create

        [HttpPost]
        public ActionResult Create(tblTerm tblterm)
        {
            if (ModelState.IsValid)
            {
                db.tblTerms.Add(tblterm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.tblUsers, "ID", "Username", tblterm.UserID);
            return View(tblterm);
        }

        //
        // GET: /Termins/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tblTerm tblterm = db.tblTerms.Find(id);
            if (tblterm == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.tblUsers, "ID", "Username", tblterm.UserID);
            return View(tblterm);
        }

        //
        // POST: /Termins/Edit/5

        [HttpPost]
        public ActionResult Edit(tblTerm tblterm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblterm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.tblUsers, "ID", "Username", tblterm.UserID);
            return View(tblterm);
        }

        //
        // GET: /Termins/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tblTerm tblterm = db.tblTerms.Find(id);
            if (tblterm == null)
            {
                return HttpNotFound();
            }
            return View(tblterm);
        }

        //
        // POST: /Termins/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tblTerm tblterm = db.tblTerms.Find(id);
            db.tblTerms.Remove(tblterm);
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