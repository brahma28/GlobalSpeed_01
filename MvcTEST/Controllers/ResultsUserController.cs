
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
    public class ResultsUserController : GSController
    {
        private SpeedCourtConfigDbEntities db = new SpeedCourtConfigDbEntities();

        //
        // GET: /ResultsUser/

        public ActionResult Index()
        {
            var tblresults = db.tblResults.Include(t => t.tblGame).Include(t => t.tblUser);
            //ViewBag.user = db.tblUsers.Where(u => u.ID == id).FirstOrDefault();
            return View(tblresults.ToList());
        }


        public string getHighscoreDates(int ID, DateTime datum)
        {

            string datumi = null;
            datumi = db.getDatepickerDates(null, null, ID, datum.Date).FirstOrDefault().rumpelstiltskin;
            return datumi;
        }

        public ActionResult getHighscores(int UserID, DateTime datum)
        {
            //var datumtermina = DateTime.ParseExact(datum, "MM/dd/yyyy", provider);
            //var tblresults = db.tblResults.Include(t => t.tblGame).Include(t => t.tblUser);
            //ViewBag.korisnik = GetCurrentUser();

            ViewBag.datumtermina = datum;

            ViewBag.highscores = db.getHighscoresAll( new Nullable<int>(), UserID, datum.Date);

            return View("Highscores");
        }

        public ActionResult Profil(int UserID = 0, int resultid = 0)
        {

            ViewBag.userprofil = db.getUserProfil(UserID).FirstOrDefault();
            ViewBag.user = db.vwUsers.Where(u => u.ID == UserID).FirstOrDefault();
            ViewBag.rezultat = db.vwResults.Where(u => u.ID == resultid).FirstOrDefault();
            return View("Profil");
        }
        public ActionResult BenutzerProfil(int UserID = 0)
        {

            ViewBag.userprofil = db.getUserProfil(UserID).FirstOrDefault();

            return View("BenutzerProfil");
        }
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
        //
        // GET: /GameResults/

        public ActionResult UserIndex(int UserID = 0)
        {
            var tblresults = db.tblResults.Include(t => t.tblGame).Include(t => t.tblUser);

            ViewBag.korisnik = GetCurrentUser();
            var datumtermina = DateTime.Now;
            ViewBag.datumtermina = DateTime.Now;
            ViewBag.selecteddates = db.getDatepickerDates(new Nullable<int>(), new Nullable<int>(), UserID, datumtermina.Date).FirstOrDefault().rumpelstiltskin;
            var hs = db.getHighscoresAll(new Nullable<int>(), UserID, datumtermina.Date);
            ViewBag.highscores = from h in hs
                                 select h;
            ViewBag.user = db.tblUsers.Where(u => u.ID == UserID).FirstOrDefault();
            var usergroup  = db.tblUserGroups.Where(ug => ug.UserID == UserID).FirstOrDefault();
            if (usergroup == null) ViewBag.groupname = string.Empty; else ViewBag.groupname = usergroup.Name;
            return View("Index");
        }
        
        public ActionResult DisplayImage(int id)
        {

            var user = db.tblUsers.Find(id);
            if (user.Picture != null)
                return File(user.Picture, "image/jpg");
            else
            {
                var dir = HttpContext.Server.MapPath("/ImagesApp");
                var path = Path.Combine( dir, "user-silhouette.png" );
                return File(path, "image/png");
            }
          
        }


        //
        // GET: /ResultsUser/Details/5

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
        // GET: /ResultsUser/Create

        public ActionResult Create()
        {
            ViewBag.GameID = new SelectList(db.tblGames, "ID", "Name");
            ViewBag.UserID = new SelectList(db.tblUsers, "ID", "Username");
            return View();
        }

        //
        // POST: /ResultsUser/Create

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
        // GET: /ResultsUser/Edit/5

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
        // POST: /ResultsUser/Edit/5

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
        // GET: /ResultsUser/Delete/5

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
        // POST: /ResultsUser/Delete/5

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