using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTEST.Models;
using System.Globalization;
using System.Threading;
using System.IO;
using System.ComponentModel.DataAnnotations;


namespace MvcTEST.Controllers
{
    public class UserController : GSController
    {
        private SpeedCourtConfigDbEntities db = new SpeedCourtConfigDbEntities();

        //
        //
        public ActionResult Index()
        {
            var tblusers = db.tblUsers.Include(t => t.tblSport).Include(t => t.tblUserGroup);

            ViewBag.korisnik = GetCurrentUser();

            var gr = db.getGroupByUser(GetCurrentUser().ID, "");
            ViewBag.groups = from gg in gr
                             select gg;
            var pl = db.getPlayerByUser(GetCurrentUser().ID, "");
            ViewBag.players = from p in pl
                              select p;




            return View();

            //var tblusers = db.tblUsers;
            //return View(tblusers);
        }
        public ActionResult DisplayImage(int id)
        {
            if (id != 0)
            {
                var user = db.tblUsers.Find(id);
                if (user.Picture != null)
                    return File(user.Picture, "image/jpeg");
                else
                {
                    var dir = HttpContext.Server.MapPath("/ImagesApp");
                    var path = Path.Combine( dir, "user-silhouette.png" );
                    return File(path, "image/png");
                }
            }
            else
            {
                var dir = HttpContext.Server.MapPath("/ImagesApp");
                var path = Path.Combine( dir, "user-silhouette.png" );
                return File( path, "image/png" );
            }
        }


        [HttpPost]
        public ActionResult AjaxSearch(string Search)
        {

            Search = HttpUtility.HtmlDecode(Search);

            var gr = db.getGroupByUser(1, Search);
            ViewBag.groups = from gg in gr
                             select gg;
            var pl = db.getPlayerByUser(1, Search);
            ViewBag.players = from p in pl
                              select p;

            return View("User_Search");


        }

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
        // GET: /Benutzer/Create

        public ActionResult Create()
        {
            ViewBag.SportID = new SelectList(db.tblSports, "ID", "Name");
            ViewBag.UserGroupID = new SelectList(db.tblUserGroups, "ID", "Name");

            // var ss = MvcTEST.Models.getNameFromDictionary_Result("Benutzer anlegen", 1);
            return View();
        }

        //
        // POST: /Benutzer/Create

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
        // GET: /Benutzer/Edit/5
        public ActionResult EditFromStart(int id = 0)
        {
            ////CultureInfo hrCulture = new CultureInfo("hr-HR");

            ////Thread.CurrentThread.CurrentUICulture = hrCulture;
            ////Thread.CurrentThread.CurrentCulture = hrCulture;
            //tblUser tbluser;
            //if (id == 0)
            //{ tbluser = new tblUser(); }
            //else
            //{
            //    tbluser = db.tblUsers.Find(id);
            //}
            //if (tbluser == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.SportID = new SelectList(db.tblSports, "ID", "Name", tbluser.SportID);
            //ViewBag.UserGroupID = new SelectList(db.tblUserGroups, "ID", "Name", tbluser.UserGroupID);
            Edit(id);
            return View();
        }

        public ActionResult Edit(int id = 0)
        {
            //CultureInfo hrCulture = new CultureInfo("hr-HR");

            //Thread.CurrentThread.CurrentUICulture = hrCulture;
            //Thread.CurrentThread.CurrentCulture = hrCulture;
            tblUser tbluser;
            if (id == 0)
            { tbluser = new tblUser(); }
            else
            {
                tbluser = db.tblUsers.Find(id);
            }
            if (tbluser == null)
            {
                return HttpNotFound();
            }
            ViewBag.SportID = new SelectList(db.tblSports, "ID", "Name", tbluser.SportID);
            ViewBag.UserGroupID = new SelectList(db.tblUserGroups, "ID", "Name", tbluser.UserGroupID);

            var pp = (from p in db.tblUsers
                      where p.PIN != null
                      orderby p.PIN
                      select p.PIN).ToArray();


            int[] numbersA = Enumerable.Range(0, 10000).ToArray();
            int[] numbersB = pp.Select(x => int.Parse(x)).ToArray();


            IEnumerable<int> aOnlyNumbers = numbersA.Except(numbersB);


            tbluser.PIN = aOnlyNumbers.First().ToString("D4");

            return View(tbluser);
        }

        //
        // POST: /Benutzer/Edit/5

        [HttpPost]
        public ActionResult Edit(tblUser tbluser)
        {
            if (ModelState.IsValid)
            {
                if (tbluser.fileUpload != null)
                {
                    tbluser.Picture = new byte[tbluser.fileUpload.InputStream.Length];
                    tbluser.fileUpload.InputStream.Read(tbluser.Picture, 0, Convert.ToInt32(tbluser.fileUpload.InputStream.Length));
                }
                if (tbluser.ID == 0) { db.tblUsers.Add(tbluser); }
                else
                {
                    db.Entry(tbluser).State = EntityState.Modified;
                }
                long ii;
                if (tbluser.RFID != null)
                {
                    ii = (long)tbluser.RFID;

                    foreach (var item in db.tblUsers)
                    {
                        if (item.RFID == ii && item.ID != tbluser.ID) { item.RFID = 0; }
                    }
                }
                else { ii = 0; tbluser.RFID = 0; }


                tblUser uu =(tblUser)System.Web.HttpContext.Current.Session["User"];
                tbluser.Role = uu.Role - 10; // vlasnik stv. trenera --- trener igrača ...
                //if (tbluser.Role == null) { tbluser.Role = 20; }
                tbluser.LanguageID = uu.LanguageID;
                tbluser.IsActiv = true;



                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SportID = new SelectList(db.tblSports, "ID", "Name", tbluser.SportID);
            ViewBag.UserGroupID = new SelectList(db.tblUserGroups, "ID", "Name", tbluser.UserGroupID);
            return View(tbluser);
        }

        //
        // GET: /Benutzer/Delete/5

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
        // POST: /Benutzer/Delete/5

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

        //#####################

        //    public ActionResult GetImage( int id )
        //{
        //    var imageData = from p in db.tblUsers
        //                    where p.ID == id
        //                    select p.HasPicture;

        //    return File( imageData, "image/jpg" );
        //}




    }
}
