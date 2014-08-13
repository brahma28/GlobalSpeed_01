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
    public class CoachController : Controller
    {
        private SpeedCourtConfigDbEntities db = new SpeedCourtConfigDbEntities();

        //

        // GET: /Coach/

        public ActionResult Index()
        {
            var tbltreneri = db.tblTrainers.Select(t => t.TrainerID);       
            var tblusers = db.tblUsers.Include(t => t.tblSport).Include(t => t.tblUserGroup)
                           .Where(u => tbltreneri.Contains(u.ID));
            return View(tblusers.ToList());
        }

        //
        // GET: /Coach/Details/5

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
        // GET: /Coach/Create

        public ActionResult Create()
        {
            ViewBag.SportID = new SelectList(db.tblSports, "ID", "Name");
            ViewBag.UserGroupID = new SelectList(db.tblUserGroups, "ID", "Name");
            return View();
        }

        //
        // POST: /Coach/Create

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
        // GET: /Coach/Edit/5

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
        // POST: /Coach/Edit/5

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
        // GET: /Coach/Delete/5

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
        // POST: /Coach/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(tblUser user)
        {
            tblTrainer tbltrainer = db.tblTrainers.FirstOrDefault(t => t.TrainerID == user.ID);
            db.tblTrainers.Remove(tbltrainer);
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