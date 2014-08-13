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
    public class GroupeController : GSController
    {
        private SpeedCourtConfigDbEntities db = new SpeedCourtConfigDbEntities();

        //
        // GET: /Groupe/

        public ActionResult Index()
        {
            //return View(db.tblUserGroups.ToList());
            return View(db.vwUserGroups.ToList());
        }

        //
        // GET: /Groupe/Details/5

        public ActionResult Details(int id = 0)
        {

            ViewBag.korisnik = GetCurrentUser();
            ViewBag.Message = "";

            tblUserGroup tblusergroup = db.tblUserGroups.Find(id);
            if (tblusergroup == null)
            {
                return HttpNotFound();
            }

            //ViewBag.clanovigrupe = from g in db.getUsersInGroup(id,GetCurrentUser().ID)
            //                 select g;

            return View(tblusergroup);
        }

        //
        // GET: /Groupe/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Groupe/Create

        [HttpPost]
        public ActionResult Create(tblUserGroup tblusergroup)
        {
            if (ModelState.IsValid)
            {
                tblusergroup.Active = true;
                db.tblUserGroups.Add(tblusergroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblusergroup);
        }

        //
        // GET: /Groupe/Edit/5

        public ActionResult Edit(int id = 0)
        {
            //if (id == 0)
            //{
            //    var tblusergroup = from p in db.tblUserGroups
            //                                select p;
            //    return View(tblusergroup);
            //}
            //else
            //{
                tblUserGroup tblusergroup = db.tblUserGroups.Find(id);
                if (tblusergroup == null)
                {
                    return HttpNotFound();
                }
                return View(tblusergroup);
            //}
        }

        //
        // POST: /Groupe/Edit/5

        [HttpPost]
        public ActionResult Edit(tblUserGroup tblusergroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblusergroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblusergroup);
        }

        //
        // GET: /Groupe/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tblUserGroup tblusergroup = db.tblUserGroups.Find(id);
            if (tblusergroup == null)
            {
                return HttpNotFound();
            }
            return View(tblusergroup);
        }

        //
        // POST: /Groupe/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tblUserGroup tblusergroup = db.tblUserGroups.Find(id);
            //var gg = (from p in db.tblUsers
            //          where p.UserGroupID == tblusergroup.ID
            //          select p).ToList(); // we have  f-ja  fnUsersInGroup() -store.p.
            //if (gg.Count == 0)
            //{
            db.tblUserGroups.Remove(tblusergroup);
            db.SaveChanges();
            //}
            //else { ViewBag.have_user = "Kann nicht gelöscht!";  }  // brisanje nije moguće jer postoje korisnici
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}