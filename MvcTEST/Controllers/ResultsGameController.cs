
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
    public class ResultsGameController : GSController
    {
        private SpeedCourtConfigDbEntities db = new SpeedCourtConfigDbEntities();

        //
        // GET: /ResultsGame/

        public ActionResult Index()
        {

            var tblresults = db.tblResults.Include(t => t.tblGame).Include(t => t.tblUser);
            return View(tblresults.ToList());
        }


        public string getHighscoreDates(int ID, DateTime datum)
        {

            string datumi = null;
            datumi = db.getDatepickerDates(ID, null, null, datum.Date).FirstOrDefault().rumpelstiltskin;
            return datumi;
        }

        public ActionResult getHighscores(int ID, DateTime datum)
        {
            //var datumtermina = DateTime.ParseExact(datum, "MM/dd/yyyy", provider);
            //var tblresults = db.tblResults.Include(t => t.tblGame).Include(t => t.tblUser);
            //ViewBag.korisnik = GetCurrentUser();

            ViewBag.datumtermina = datum;

            //ViewBag.highscores = db.getHighscores(ID, GetCurrentUser().ID, datum.Date);
            // ne treba na UserID !!!
            ViewBag.highscores = db.getHighscores(ID, new Nullable<int>(), datum.Date);
            return View("Highscores");
        }

        public String SendMail( int ID, DateTime datum)
        {

         
            StringBuilder body = new StringBuilder();

            var hs = db.getHighscores(ID, new Nullable<int>(), datum);
            var brojac = 0;
            foreach (MvcTEST.Models.getHighscores_Result item in hs)
            {
                brojac++;
                body.AppendLine(brojac.ToString() + "\t" + item.Sur_Name + "\t" + item.Grupa + "\t" + item.Rezultat);
            }
            MailMessage mail = new MailMessage("globalspeed.informations@gmail.com", GetCurrentUser().Email );
            

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

        public ActionResult GameIndex(int id = 0)
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
            ViewBag.selecteddates = db.getDatepickerDates(id, new Nullable<int>(), new Nullable<int>(), new Nullable<DateTime>()).FirstOrDefault().rumpelstiltskin;
            ViewBag.datumtermina = DateTime.Now;
            //var hs = db.getHighscores(id, GetCurrentUser().ID, datumtermina);
            var hs = db.getHighscores(id, new Nullable<int>(), datumtermina.Date);
            //var hs = db.getHighscores(id, new Nullable<int>(), dt);
            ViewBag.highscores = from h in hs
                                 select h;
            //ViewBag.resultstitle = db.tblGames.Where(g => g.ID == id).FirstOrDefault().Name;
            ViewBag.game = db.tblGames.Where(g => g.ID == id).FirstOrDefault();
            ViewBag.grupa = string.Empty;
            //return View(tblresults.ToList());
            return View("Index");
        }
        public ActionResult GameIndex2(int id = 0)
        {
            //id= ResultID ; GameID ; UserID
            var reza = db.tblResults.Where(r => r.ID == id).FirstOrDefault();
            var GameID = reza.GameID;

            var tblresults = db.tblResults.Include(t => t.tblGame).Include(t => t.tblUser);
            ViewBag.korisnik = GetCurrentUser();
            var datumtermina = DateTime.Now;
            ViewBag.selecteddates = db.getDatepickerDates(GameID, new Nullable<int>(), new Nullable<int>(), new Nullable<DateTime>()).FirstOrDefault().rumpelstiltskin;
            ViewBag.datumtermina = DateTime.Now;
            var hs = db.getHighscores(GameID, new Nullable<int>(), datumtermina.Date);
            ViewBag.highscores = from h in hs
                                 select h;
            ViewBag.game = db.tblGames.Where(g => g.ID == GameID).FirstOrDefault();
            ViewBag.grupa = string.Empty;
            return View("Index");
        }

        public ActionResult GameUserIndex(int groupid = 0, int gameid = 0, int userid = 0, DateTime? datumtermina = null)
        {
            //id = GroupeID; GameID; UserID; 
            var tblresults = db.tblResults.Include(t => t.tblGame).Include(t => t.tblUser);

            ViewBag.korisnik = GetCurrentUser();


            if (!datumtermina.HasValue)
            {
                datumtermina = DateTime.Now;
            }
            ViewBag.datumtermina = datumtermina;

            ViewBag.selecteddates = db.getDatepickerDates(gameid, null, userid, new Nullable<DateTime>()).FirstOrDefault().rumpelstiltskin;

            //var hs = db.getHighscores(id, GetCurrentUser().ID, datumtermina);
            var hs = db.getHighscores(gameid, userid, datumtermina.Value.Date);
            //var hs = db.getHighscores(id, new Nullable<int>(), dt);
            ViewBag.highscores = from h in hs
                                 select h;
            //ViewBag.resultstitle = db.tblGames.Where(g => g.ID == id).FirstOrDefault().Name;
            ViewBag.game = db.tblGames.Where(g => g.ID == gameid).FirstOrDefault();
            //ViewBag.grupa = db.tblUserGroups.Where(ug => ug.ID == groupid).FirstOrDefault().Name.ToUpper();
            //return View(tblresults.ToList());
            return View("Index");
        }
        public ActionResult GameGroupIndex(int groupid = 0, int gameid = 0, int userid = 0, DateTime? datumtermina = null)
        {
            //id = GroupeID; GameID; UserID; 
            var tblresults = db.tblResults.Include(t => t.tblGame).Include(t => t.tblUser);

            ViewBag.korisnik = GetCurrentUser();


            if (!datumtermina.HasValue)
            {
                datumtermina = DateTime.Now;
            }
            ViewBag.datumtermina = datumtermina;

            ViewBag.selecteddates = db.getDatepickerDates(gameid, groupid, userid, new Nullable<DateTime>()).FirstOrDefault().rumpelstiltskin;

            //var hs = db.getHighscores(id, GetCurrentUser().ID, datumtermina);
            var hs = db.getHighscores(gameid, userid, datumtermina.Value.Date);
            //var hs = db.getHighscores(id, new Nullable<int>(), dt);
            ViewBag.highscores = from h in hs
                                 select h;
            //ViewBag.resultstitle = db.tblGames.Where(g => g.ID == id).FirstOrDefault().Name;
            ViewBag.game = db.tblGames.Where(g => g.ID == gameid).FirstOrDefault();
            //ViewBag.grupa = db.tblUserGroups.Where(ug => ug.ID == groupid).FirstOrDefault().Name.ToUpper();
            //return View(tblresults.ToList());
            return View("Index");
        }
        //
        // GET: /GameResults/

        public ActionResult GroupIndex(int id = 0)
        {
            //ID-GameID; UserID
            var tblresults = db.tblResults.Include(t => t.tblGame).Include(t => t.tblUser);

            //return View(tblspeedentries.ToList());

            ViewBag.korisnik = GetCurrentUser();

            // 31 - tapping
            //  0 - user
            // '2013-03-25' - na dan
            // exec [dbo].[getHighscores] 31,3,'2013-03-25'
            //var datumtermina = DateTime.Now;
            var datumtermina = new DateTime(2013, 03, 25);

            var hs = db.getHighscores(id, 3, datumtermina);
            ViewBag.highscores = from h in hs
                                 select h;

            //return View(tblresults.ToList());
            return View();
        }

        //
        // GET: /ResultsGame/Details/5

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
        // GET: /ResultsGame/Create

        public ActionResult Create()
        {
            ViewBag.GameID = new SelectList(db.tblGames, "ID", "Name");
            ViewBag.UserID = new SelectList(db.tblUsers, "ID", "Username");
            return View();
        }

        //
        // POST: /ResultsGame/Create

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
        // GET: /ResultsGame/Edit/5

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
        // POST: /ResultsGame/Edit/5

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
        // GET: /ResultsGame/Delete/5

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
        // POST: /ResultsGame/Delete/5

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