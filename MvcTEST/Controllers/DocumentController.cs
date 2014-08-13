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
    public class DocumentController : Controller
    {
        private SpeedCourtConfigDbEntities db = new SpeedCourtConfigDbEntities();

        //
        // GET: /Document/

        public ActionResult Index()
        {
            return View(db.tblDocuments.ToList());
        }

        //
        // GET: /Document/Details/5

        public ActionResult Details(int id = 0)
        {
            tblDocument tbldocument = db.tblDocuments.Find(id);
            if (tbldocument == null)
            {
                return HttpNotFound();
            }
            return View(tbldocument);
        }

        //
        // GET: /Document/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Document/Create

        [HttpPost]
        public ActionResult Create(tblDocument tbldocument)
        {
            if (ModelState.IsValid)
            {
                db.tblDocuments.Add(tbldocument);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbldocument);
        }

        //
        // GET: /Document/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tblDocument tbldocument = db.tblDocuments.Find(id);
            if (tbldocument == null)
            {
                return HttpNotFound();
            }
            return View(tbldocument);
        }

        //
        // POST: /Document/Edit/5

        [HttpPost]
        public ActionResult Edit(tblDocument tbldocument)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbldocument).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbldocument);
        }

        //
        // GET: /Document/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tblDocument tbldocument = db.tblDocuments.Find(id);
            if (tbldocument == null)
            {
                return HttpNotFound();
            }
            return View(tbldocument);
        }

        //
        // POST: /Document/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tblDocument tbldocument = db.tblDocuments.Find(id);
            db.tblDocuments.Remove(tbldocument);
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