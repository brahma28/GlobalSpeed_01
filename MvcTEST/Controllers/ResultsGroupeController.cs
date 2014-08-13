
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

namespace MvcTEST.Controllers
{
    public class ResultsGroupeController : GSController
    {
        private SpeedCourtConfigDbEntities db = new SpeedCourtConfigDbEntities();

        //
        // GET: /ResultsGroupe/

        public ActionResult Index()
        {
            var tblresults = db.tblResults.Include(t => t.tblGame).Include(t => t.tblUser);
            return View(tblresults.ToList());
        }


        public string getHighscoreDates(int ID, DateTime datum)
        {
            // ID = GroupID 
            string datumi = null;
            datumi = db.getDatepickerDates(null, ID, null, datum.Date).FirstOrDefault().rumpelstiltskin;
            return datumi;
        }


        public ActionResult getHighscores(DateTime datum)
        {
            //var datumtermina = DateTime.ParseExact(datum, "MM/dd/yyyy", provider);
            //var tblresults = db.tblResults.Include(t => t.tblGame).Include(t => t.tblUser);
            //ViewBag.korisnik = GetCurrentUser();

            ViewBag.datumtermina = datum;

            ViewBag.highscores1 = db.getHighscoresTop5P1(new Nullable<int>(), GetCurrentUser().ID, datum.Date);
            ViewBag.highscores2 = db.getHighscoresTop5P2(new Nullable<int>(), GetCurrentUser().ID, datum.Date);
            return View("Highscores");
        }

        public String SendMail(int ID, DateTime datum)
        {


            StringBuilder body = new StringBuilder();

            var hs = db.getHighscores(ID, new Nullable<int>(), datum);
            //var brojac = 0;
            //foreach (MvcTEST.Models.getHighscoresTop5P2_Result item in hs)
            //{
            //    brojac++;
            //    body.AppendLine(brojac.ToString() + "\t" + item.Sur_Name + "\t" + item.Grupa + "\t" + item.Rezultat);
            //}
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



        //
        // GET: /GameResults/

        public ActionResult GroupeIndex(int GroupeID = 0)
        {
            //ID-GameID; UserID
            var tblresults = db.tblResults.Include(t => t.tblGame).Include(t => t.tblUser);

            //return View(tblspeedentries.ToList());

            ViewBag.korisnik = GetCurrentUser();

            // 31 - tapping
            //  0 - user
            // '2013-03-25' - na dan
            // exec [dbo].[getHighscores] 31,3,'2013-03-25'
            var datumtermina = DateTime.Now;


            //var datumtermina = DateTime.ParseExact(dt, "MM/dd/yyyy",provider );
            //var datumtermina = new DateTime(2013, 03, 25);
            ViewBag.selecteddates = db.getDatepickerDates(new Nullable<int>(), GroupeID, new Nullable<int>(), new Nullable<DateTime>()).FirstOrDefault().rumpelstiltskin;
            ViewBag.datumtermina = DateTime.Now;
            //var hs = db.getHighscores(id, GetCurrentUser().ID, datumtermina);
            var hs1 = db.getHighscoresTop5P1(GroupeID, new Nullable<int>(), datumtermina.Date);
            //var hs = db.getHighscores(id, new Nullable<int>(), dt);
            ViewBag.highscores1 = from h1 in hs1
                                 select h1;
            var hs2 = db.getHighscoresTop5P2(GroupeID, new Nullable<int>(), datumtermina.Date);
            ViewBag.highscores2 = from h2 in hs2
                                 select h2;

            //ViewBag.resultstitle = db.tblGames.Where(g => g.ID == id).FirstOrDefault().Name;
            ViewBag.usergroupe = db.tblUserGroups.Where(g => g.ID == GroupeID).FirstOrDefault();
            //return View(tblresults.ToList());
            return View("Index");
        }
        //
        // GET: /ResultsGroupe/Details/5

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
        // GET: /ResultsGroupe/Create

        public ActionResult Create()
        {
            ViewBag.GameID = new SelectList(db.tblGames, "ID", "Name");
            ViewBag.UserID = new SelectList(db.tblUsers, "ID", "Username");
            return View();
        }

        //
        // POST: /ResultsGroupe/Create

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
        // GET: /ResultsGroupe/Edit/5

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
        // POST: /ResultsGroupe/Edit/5

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
        // GET: /ResultsGroupe/Delete/5

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
        // POST: /ResultsGroupe/Delete/5

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