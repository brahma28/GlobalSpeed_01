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
    public class TrainerHomeController : Controller
    {
        private SpeedCourtConfigDbEntities db = new SpeedCourtConfigDbEntities();

        //
        // GET: /TrainerHome/

        public ActionResult Index()
        {
            var tblusers = db.tblUsers.Include(t => t.tblSport).Include(t => t.tblUserGroup);
            return View(tblusers.ToList());
        }

        //
        // GET: /TrainerHome/Details/5

        public ActionResult Details(int id = 0)
        {
            tblUser tbluser = db.tblUsers.Find(id);
            if (tbluser == null)
            {
                return HttpNotFound();
            }
            return View(tbluser);
        }

        //
        // GET: /TrainerHome/Create

        public ActionResult Create()
        {
            ViewBag.SportID = new SelectList(db.tblSports, "ID", "Name");
            ViewBag.UserGroupID = new SelectList(db.tblUserGroups, "ID", "Name");
            return View();
        }

        //
        // POST: /TrainerHome/Create

        [HttpPost]
        public ActionResult Create(tblUser tbluser)
        {
            if (ModelState.IsValid)
            {
                db.tblUsers.Add(tbluser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SportID = new SelectList(db.tblSports, "ID", "Name", tbluser.SportID);
            ViewBag.UserGroupID = new SelectList(db.tblUserGroups, "ID", "Name", tbluser.UserGroupID);
            return View(tbluser);
        }

        //
        // GET: /TrainerHome/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tblUser tbluser = db.tblUsers.Find(id);
            if (tbluser == null)
            {
                return HttpNotFound();
            }
            ViewBag.SportID = new SelectList(db.tblSports, "ID", "Name", tbluser.SportID);
            ViewBag.UserGroupID = new SelectList(db.tblUserGroups, "ID", "Name", tbluser.UserGroupID);
            return View(tbluser);
        }

        //
        // POST: /TrainerHome/Edit/5

        [HttpPost]
        public ActionResult Edit(tblUser tbluser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbluser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SportID = new SelectList(db.tblSports, "ID", "Name", tbluser.SportID);
            ViewBag.UserGroupID = new SelectList(db.tblUserGroups, "ID", "Name", tbluser.UserGroupID);
            return View(tbluser);
        }

        //
        // GET: /TrainerHome/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tblUser tbluser = db.tblUsers.Find(id);
            if (tbluser == null)
            {
                return HttpNotFound();
            }
            return View(tbluser);
        }

        //
        // POST: /TrainerHome/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tblUser tbluser = db.tblUsers.Find(id);
            db.tblUsers.Remove(tbluser);
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