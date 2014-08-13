using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTEST.Models;
using System.Globalization;
using System.Net.Mail;
using System.Text;
using System.Net;
using System.IO;

namespace MvcTEST.Controllers
{
    public class UserHomeController : GSController
    {
        private SpeedCourtConfigDbEntities db = new SpeedCourtConfigDbEntities();

        //
        // GET: /UserHome/

        public ActionResult Index()
        {
            //var tblusers = db.tblUsers.Include(t => t.tblSport).Include(t => t.tblUserGroup);
            //return View(tblusers.ToList());

            ViewBag.korisnik = GetCurrentUser();
            ViewBag.Message = "";

            //var datumtermina = DateTime.Now;
            //var ee = db.getTerminInDay(datumtermina);
            //var ss = from w in ee
            //         select w;

            ViewBag.podaci = from b in db.getResult_Part1(GetCurrentUser().ID)
                             select b;
            ViewBag.podaci2 = from c in db.getResult_Part2(GetCurrentUser().ID)
                              select c;
            ViewBag.podaci_news = from d in db.getNews(GetCurrentUser().ID)
                                  select d;
            ViewBag.podaci_last5 = from e in db.getLast5Games(GetCurrentUser().ID)
                                   select e;


            return View();


        }

        //
        // GET: /UserHome/Details/5

        //public ActionResult Details(int id = 0)
        //{
        //    tblUser tbluser = db.tblUsers.Find(id);
        //    if (tbluser == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tbluser);
        //}

        ////
        //// GET: /UserHome/Create

        //public ActionResult Create()
        //{
        //    ViewBag.SportID = new SelectList(db.tblSports, "ID", "Name");
        //    ViewBag.UserGroupID = new SelectList(db.tblUserGroups, "ID", "Name");
        //    return View();
        //}

        ////
        //// POST: /UserHome/Create

        //[HttpPost]
        //public ActionResult Create(tblUser tbluser)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.tblUsers.Add(tbluser);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.SportID = new SelectList(db.tblSports, "ID", "Name", tbluser.SportID);
        //    ViewBag.UserGroupID = new SelectList(db.tblUserGroups, "ID", "Name", tbluser.UserGroupID);
        //    return View(tbluser);
        //}

        ////
        //// GET: /UserHome/Edit/5

        //public ActionResult Edit(int id = 0)
        //{
        //    tblUser tbluser = db.tblUsers.Find(id);
        //    if (tbluser == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.SportID = new SelectList(db.tblSports, "ID", "Name", tbluser.SportID);
        //    ViewBag.UserGroupID = new SelectList(db.tblUserGroups, "ID", "Name", tbluser.UserGroupID);
        //    return View(tbluser);
        //}

        ////
        //// POST: /UserHome/Edit/5

        //[HttpPost]
        //public ActionResult Edit(tblUser tbluser)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(tbluser).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.SportID = new SelectList(db.tblSports, "ID", "Name", tbluser.SportID);
        //    ViewBag.UserGroupID = new SelectList(db.tblUserGroups, "ID", "Name", tbluser.UserGroupID);
        //    return View(tbluser);
        //}

        ////
        //// GET: /UserHome/Delete/5

        //public ActionResult Delete(int id = 0)
        //{
        //    tblUser tbluser = db.tblUsers.Find(id);
        //    if (tbluser == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tbluser);
        //}

        ////
        //// POST: /UserHome/Delete/5

        //[HttpPost, ActionName("Delete")]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    tblUser tbluser = db.tblUsers.Find(id);
        //    db.tblUsers.Remove(tbluser);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        public String SendMail(int ID, DateTime datum)
        {

            StringBuilder body = new StringBuilder();

            var hs = db.getHighscores(ID, new Nullable<int>(), datum);
            var brojac = 0;
            foreach (MvcTEST.Models.getHighscores_Result item in hs)
            {
                brojac++;
                body.AppendLine(brojac.ToString() + "\t" + item.Sur_Name + "\t" + item.Grupa + "\t" + item.Rezultat);
            }
            MailMessage mail = new MailMessage("globalspeed.informations@gmail.com", GetCurrentUser().Email);


            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("globalspeed.informations@gmail.com", "Dm17in17"),
                EnableSsl = true
            };

            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.UseDefaultCredentials = false;

            mail.Subject = "GlobalSpeed ergebnisse " + datum;
            mail.Body = body.ToString();
            client.Send(mail);

            return "Mail erfolgreich versendet!";
            //return "Mail uspjesno poslan!";
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //[FacebookAuthorize("email")]
        //public class UserController : Controller
        //{
        //    [FacebookAuthorize("user_photos")]
        //    public async Task<ActionResult> Profile(FacebookContext context)
        //    {
        //        // Implementation removed for clarity
        //    }
        //}




    }
}