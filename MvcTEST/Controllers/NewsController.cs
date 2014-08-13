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
    public class NewsController : Controller
    {
        private SpeedCourtConfigDbEntities db = new SpeedCourtConfigDbEntities();

        //
        // GET: /News/

        public ActionResult Index()
        {
            return View(db.tblNews.ToList());
        }

        //
        // GET: /News/Details/5

        public ActionResult Details(int id = 0)
        {
            tblNew tblnew = db.tblNews.Find(id);
            if (tblnew == null)
            {
                return HttpNotFound();
            }
            return View(tblnew);
        }

        //
        // GET: /News/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /News/Create

        [HttpPost]
        public ActionResult Create(tblNew tblnew)
        {
            if (ModelState.IsValid)
            {
                db.tblNews.Add(tblnew);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblnew);
        }

        //
        // GET: /News/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tblNew tblnew = db.tblNews.Find(id);
            if (tblnew == null)
            {
                return HttpNotFound();
            }
            return View(tblnew);
        }

        //
        // POST: /News/Edit/5

        [HttpPost]
        public ActionResult Edit(tblNew tblnew)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblnew).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblnew);
        }

        //
        // GET: /News/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tblNew tblnew = db.tblNews.Find(id);
            if (tblnew == null)
            {
                return HttpNotFound();
            }
            return View(tblnew);
        }

        //
        // POST: /News/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tblNew tblnew = db.tblNews.Find(id);
            db.tblNews.Remove(tblnew);
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