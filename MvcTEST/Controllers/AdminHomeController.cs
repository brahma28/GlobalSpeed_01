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
    public class AdminHomeController : GSController
    {
        private SpeedCourtConfigDbEntities db = new SpeedCourtConfigDbEntities();

        //
        // GET: /AdminHome/

        public ActionResult Index()
        {
            ViewBag.korisnik = GetCurrentUser();

            tblUser tbluser = db.tblUsers.Find(GetCurrentUser().ID);
            if (tbluser == null)
            {
                return HttpNotFound();
            }
            return View("Index");

        }
        public ActionResult Language()
        {
            ViewBag.korisnik = GetCurrentUser();

            tblUser tbluser = db.tblUsers.Find(GetCurrentUser().ID);
            if (tbluser == null)
            {
                return HttpNotFound();
            }
            return View("Language",tbluser);

        }

        [HttpPost]
        public ActionResult AjaxSetLanguage(string radiobutton)
        {

            radiobutton = HttpUtility.HtmlDecode(radiobutton);
            // odradi storu i upisi jezik u bazu
            var gm = db.setLanguage(1, radiobutton);
            GetCurrentUser().LanguageID = Convert.ToInt32(radiobutton);
            ViewBag.korisnik = GetCurrentUser();
            //ViewBag.jezik =
            var tblusers = db.tblUsers.Include(t => t.tblSport).Include(t => t.tblUserGroup);
            return View("Language", tblusers);

        }

        //[HttpPost]
        //public ActionResult SetLanguageOK()
        //{


        //    var radiobutton = "1";
        //     odradi storu i upisi jezik u bazu
        //    var gm = db.setLanguage(1, radiobutton);
        //    GetCurrentUser().LanguageID = Convert.ToInt32(radiobutton);
        //    ViewBag.korisnik = GetCurrentUser();
        //    ViewBag.jezik =
        //    var tblusers = db.tblUsers.Include(t => t.tblSport).Include(t => t.tblUserGroup);
        //    return View("Language", tblusers);

        //}
        //
        // GET: /AdminHome/Details/5

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
        // GET: /AdminHome/Create

        public ActionResult Create()
        {
            ViewBag.SportID = new SelectList(db.tblSports, "ID", "Name");
            ViewBag.UserGroupID = new SelectList(db.tblUserGroups, "ID", "Name");
            return View();
        }

        //
        // POST: /AdminHome/Create

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
        // GET: /AdminHome/Edit/5

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
        // POST: /AdminHome/Edit/5

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
        // GET: /AdminHome/Delete/5

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
        // POST: /AdminHome/Delete/5

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