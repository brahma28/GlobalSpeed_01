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
    public class ConfiguratorController : GSController
    {
        private SpeedCourtConfigDbEntities db = new SpeedCourtConfigDbEntities();

        //
        // GET: /Configurator/

        public ActionResult Index()
        {
            var tblspeedentries = db.tblSpeedEntries.Include(t => t.tblSensorType);
            //return View(tblspeedentries.ToList());

            ViewBag.korisnik = GetCurrentUser();


            var gm = db.getGameByUser(1, "");
            ViewBag.games = from g in gm
                            select g;
            var te = db.vwTemplates;
            ViewBag.terens = from t in te
                            select t;
            return View(te.FirstOrDefault());
        }

        //
        // GET: /Configurator/

        public ActionResult Start()
        {
            //var tbltemplate = db.tblTemplates.Where(t => t.IsActive == true).FirstOrDefault();
            var tbltemplate = db.tblTemplates.Where(g => g.IsActive == true).FirstOrDefault();
            ViewBag.korisnik = GetCurrentUser();
            ViewBag.teren = db.tblTemplates.Where(g => g.IsActive == true).FirstOrDefault();

            var te = db.vwTemplates;
            ViewBag.terens = from t in te
                            select t;
            return View("Start", tbltemplate);
        }
        //
        // GET: /Configurator/

        public ActionResult Start1()
        {
            var tbltemplate = db.tblTemplates.Where(t => t.IsActive == true).FirstOrDefault();
            ViewBag.korisnik = GetCurrentUser();
            ViewBag.teren = db.tblTemplates.Where(g => g.IsActive == true).FirstOrDefault();
            var te = db.vwTemplates;
            ViewBag.terens = from t in te
                            select t;
            if (tbltemplate.SpeedCourtAndTrack == true )
            {
                // obavi i konfiguraciju: SPEEDTRACK-a !
                return View("Start23", te.FirstOrDefault());
            }
            else
            {
                // konfiguracija gotova ! (Vracanje na pocetni ekran)
                //return View("AdminHome");  
                return RedirectToAction("index", "Adminhome");
            }

        }
        //
        // GET: /Configurator/

        public ActionResult Start2()
        {
            var tbltemplate = db.tblTemplates.Where(g => g.IsActive == true).FirstOrDefault();

            ViewBag.korisnik = GetCurrentUser();

            var te = db.vwTemplates;
            ViewBag.terens = from t in te
                             select t;
            return View("Start2", te.FirstOrDefault());

        }

        public ActionResult Start20()
        {
            var tbltemplate = db.tblTemplates.Where(g => g.IsActive == true).FirstOrDefault();

            ViewBag.korisnik = GetCurrentUser();

            var te = db.vwTemplates;
            ViewBag.terens = from t in te
                             select t;
            if (tbltemplate.SpeedTrack == true)
            {
                // SPEEDTRACK 
                return View("Start23", tbltemplate);
            }
            else
            {
                return View("Start2", te.FirstOrDefault());
            }
        }

        [HttpPost]
        public ActionResult AjaxSetTerens(string radiobutton)
        {

            radiobutton = HttpUtility.HtmlDecode(radiobutton);
            // odradi storu i upisi u bazu
            var gm = db.setTemplate(1, radiobutton);

            var tbltemplate = db.tblTemplates.Where(g => g.IsActive == true);
            ViewBag.brojpolja = db.tblTemplates.FirstOrDefault().FieldsNo;
            ViewBag.korisnik = GetCurrentUser();

            var te = db.vwTemplates;
            ViewBag.terens = from t in te
                             select t;
            if (radiobutton == "speedcourt" || radiobutton == "speedtrack" || radiobutton == "speedcourtandtrack")
            {
                return View("Start", tbltemplate.FirstOrDefault());
            }
            else
            {
                return View("Start2", te.FirstOrDefault());
            }
        }

        public ActionResult Start3()
        {
            var tbltemplate = db.tblTemplates.Where(g => g.IsActive == true);

            ViewBag.korisnik = GetCurrentUser();

            var te = db.vwTemplates;
            var te1 = db.vwTemplates.FirstOrDefault();
            ViewBag.terens = (from t in te
                             select t).FirstOrDefault();
            ViewBag.teren = db.tblTemplates.Where(g => g.IsActive == true).FirstOrDefault();
            var model = new MyViewModel
            {
                Id = db.tblTemplates.FirstOrDefault().FieldsNo.ToString(),
                Items = new[]
        {
            new SelectListItem { Value = "1", Text = "1" },
            new SelectListItem { Value = "2", Text = "2" },
            new SelectListItem { Value = "3", Text = "3" },
            new SelectListItem { Value = "4", Text = "4" },
            new SelectListItem { Value = "5", Text = "5" },
            new SelectListItem { Value = "6", Text = "6" },
            new SelectListItem { Value = "7", Text = "7" },
            new SelectListItem { Value = "8", Text = "8" },
            new SelectListItem { Value = "9", Text = "9" },
            new SelectListItem { Value = "10", Text = "10" },
            new SelectListItem { Value = "11", Text = "11" },
            new SelectListItem { Value = "12", Text = "12" },
            new SelectListItem { Value = "13", Text = "13" },
            new SelectListItem { Value = "14", Text = "14" },
            new SelectListItem { Value = "15", Text = "15" },
            new SelectListItem { Value = "16", Text = "16" },
            new SelectListItem { Value = "17", Text = "17" },
            new SelectListItem { Value = "18", Text = "18" },
            new SelectListItem { Value = "19", Text = "19" },
            new SelectListItem { Value = "20", Text = "20" },
            new SelectListItem { Value = "21", Text = "21" },
            new SelectListItem { Value = "22", Text = "22" },
            new SelectListItem { Value = "23", Text = "23" },
            new SelectListItem { Value = "24", Text = "24" },
            new SelectListItem { Value = "25", Text = "25" },
            new SelectListItem { Value = "26", Text = "26" },
            new SelectListItem { Value = "27", Text = "27" },
            new SelectListItem { Value = "28", Text = "28" },
            new SelectListItem { Value = "29", Text = "29" },
            new SelectListItem { Value = "30", Text = "30" },
            new SelectListItem { Value = "31", Text = "31" },
            new SelectListItem { Value = "32", Text = "32" },
         
        }
            };
            if (te1.RadioX == 1)
            {
                return View("Start3", model);
            }
            else
            {
                return View("Start4", tbltemplate.FirstOrDefault());
            }



            //return View("Start3", tbltemplate.FirstOrDefault());

        }

        public ActionResult Start4()
        {
            var tbltemplate = db.tblTemplates.Where(g => g.IsActive == true);
            ViewBag.korisnik = GetCurrentUser();
            ViewBag.teren = db.tblTemplates.Where(g => g.IsActive == true).FirstOrDefault();
            ViewBag.terens = db.vwTemplates.FirstOrDefault();
            var te = db.vwTemplates.FirstOrDefault();
            if (te.RadioX == 1)
            {
                return View("Start4", tbltemplate.FirstOrDefault());
            }
            else
            {
                return View("Start6", tbltemplate.FirstOrDefault());
            }
        }

        public ActionResult Start5()
        {
            ViewBag.korisnik = GetCurrentUser();
            var tbltemplate = db.tblTemplates;
            ViewBag.teren = db.tblTemplates.Where(g => g.IsActive == true).FirstOrDefault();
            ViewBag.terens = db.vwTemplates.FirstOrDefault();
            var te = db.vwTemplates.FirstOrDefault();
            if (te.RadioX == 1)
            {
                return View("Start5", tbltemplate.FirstOrDefault());
            }
            else
            {
                return View("Start6", tbltemplate.FirstOrDefault());
            }

        }

        public ActionResult Start6()
        {
            var tbltemplate = db.tblTemplates;

            ViewBag.korisnik = GetCurrentUser();
            ViewBag.teren = db.tblTemplates.Where(g => g.IsActive == true).FirstOrDefault();
            var te = db.vwTemplates;
            ViewBag.terens = from t in te
                             select t;
            return View("Start6", tbltemplate.FirstOrDefault());

        }

        public ActionResult Start23()
        {
            var tbltemplate = db.tblTemplates.Where(t => t.IsActive == true).FirstOrDefault();
            ViewBag.korisnik = GetCurrentUser();
            ViewBag.teren = db.tblTemplates.Where(g => g.IsActive == true).FirstOrDefault();
            var te = db.vwTemplates;
            ViewBag.terens = from t in te
                             select t;
            return View("Start23", tbltemplate);
        }

        [HttpPost]
        public ActionResult AjaxSetFormOfStartField(string radiobutton)
        {

            radiobutton = HttpUtility.HtmlDecode(radiobutton);
            // odradi storu i upisi u bazu
            var gm = db.setFormOfStartField(1, radiobutton);

            var tbltemplate = db.tblTemplates.Where(t => t.IsActive == true).FirstOrDefault();
            ViewBag.korisnik = GetCurrentUser();
            ViewBag.teren = db.tblTemplates.Where(g => g.IsActive == true).FirstOrDefault();
            var te = db.vwTemplates;
            ViewBag.terens = from t in te
                             select t;
            return View("Start23", tbltemplate);

        }

        [HttpPost]
        public ActionResult AjaxSetFieldNo(int fieldno)
        {

            // odradi storu i upisi u bazu
            var gm = db.setFieldNo(fieldno);

            var tbltemplate = db.tblTemplates.Where(t => t.IsActive == true).FirstOrDefault();
            ViewBag.korisnik = GetCurrentUser();
            ViewBag.teren = db.tblTemplates.Where(g => g.IsActive == true).FirstOrDefault();
            var te = db.vwTemplates;
            ViewBag.terens = from t in te
                             select t;
            return View("Start23", tbltemplate);

        }
        public ActionResult Start24()
        {
            var sst = db.setSpeedTeren();
            var tbltemplate = db.tblTemplates.Where(t => t.IsActive == true).FirstOrDefault();
            ViewBag.korisnik = GetCurrentUser();
            ViewBag.teren = db.tblTemplates.Where(g => g.IsActive == true).FirstOrDefault();
            var te = db.vwTemplates;
            ViewBag.terens = from t in te
                             select t;
            return View("Start24", tbltemplate);
        }

        [HttpPost]
        public string AjaxSetPositionXY(string txtID, int x = 0, int y = 0)
        {
            if ( x != 0 && y != 0){
                var sst = db.setPositionXY(txtID,x,y);
            }
            //var tbltemplate = db.tblTemplates.Where(t => t.IsActive == true).FirstOrDefault();
            //ViewBag.korisnik = GetCurrentUser();
            //ViewBag.teren = db.tblTemplates.Where(g => g.IsActive == true).FirstOrDefault();
            //var te = db.vwTemplates;
            //ViewBag.terens = from t in te
            //                 select t;
            //return View("Start24", tbltemplate);
            return string.Empty;
        }

        public ActionResult Start25()
        {
            var tbltemplate = db.tblTemplates.Where(t => t.IsActive == true).FirstOrDefault();
            ViewBag.korisnik = GetCurrentUser();
            ViewBag.teren = db.tblTemplates.Where(g => g.IsActive == true).FirstOrDefault();
            var te = db.vwTemplates;
            ViewBag.terens = from t in te
                             select t;
            return View("Start25", tbltemplate);
        }

        public ActionResult Start26()
        {
            var tbltemplate = db.tblTemplates.Where(t => t.IsActive == true).FirstOrDefault();
            ViewBag.korisnik = GetCurrentUser();
            ViewBag.teren = db.tblTemplates.Where(g => g.IsActive == true).FirstOrDefault();
            var te = db.vwTemplates;
            ViewBag.terens = from t in te
                             select t;
            return View("Start26", tbltemplate);
        }

        [HttpPost]
        public ActionResult AjaxSetStartField(string radiobutton)
        {

            radiobutton = HttpUtility.HtmlDecode(radiobutton);
            // odradi storu i upisi u bazu
            var gm = db.setStartEndField(1, radiobutton, 1);

            var tbltemplate = db.tblTemplates.Where(t => t.IsActive == true).FirstOrDefault();
            ViewBag.korisnik = GetCurrentUser();
            ViewBag.teren = db.tblTemplates.Where(g => g.IsActive == true).FirstOrDefault();
            var te = db.vwTemplates;
            ViewBag.terens = from t in te
                             select t;
            return View("Start26", tbltemplate);

        }

        public ActionResult Start27()
        {
            var tbltemplate = db.tblTemplates.Where(t => t.IsActive == true).FirstOrDefault();
            ViewBag.korisnik = GetCurrentUser();
            ViewBag.teren = db.tblTemplates.Where(g => g.IsActive == true).FirstOrDefault();
            var te = db.vwTemplates;
            ViewBag.terens = from t in te
                             select t;
            return View("Start27", tbltemplate);
        }

        [HttpPost]
        public ActionResult AjaxSetEndField(string radiobutton)
        {

            radiobutton = HttpUtility.HtmlDecode(radiobutton);
            // odradi storu (upisi u bazu) i osvježi stranicu
            var gm = db.setStartEndField(1, radiobutton, 2);

            var tbltemplate = db.tblTemplates.Where(t => t.IsActive == true).FirstOrDefault();
            ViewBag.korisnik = GetCurrentUser();
            ViewBag.teren = db.tblTemplates.Where(g => g.IsActive == true).FirstOrDefault();
            var te = db.vwTemplates;
            ViewBag.terens = from t in te
                             select t;
            return View("Start27", tbltemplate);

        }

        public ActionResult Start28()
        {
            var tbltemplate = db.tblTemplates.Where(t => t.IsActive == true).FirstOrDefault();
            ViewBag.korisnik = GetCurrentUser();
            ViewBag.teren = db.tblTemplates.Where(g => g.IsActive == true).FirstOrDefault();
            var te = db.vwTemplates;
            ViewBag.terens = from t in te
                             select t;
            return View("Start28", tbltemplate);
        }

        [HttpPost]
        public ActionResult AjaxSetFieldName(string broj = "", string ime = "")
        {
            var sfn = db.setFieldName(broj, ime);
            var tbltemplate = db.tblTemplates.Where(t => t.IsActive == true).FirstOrDefault();
            ViewBag.korisnik = GetCurrentUser();
            ViewBag.teren = db.tblTemplates.Where(g => g.IsActive == true).FirstOrDefault();
            var te = db.vwTemplates;
            ViewBag.terens = from t in te
                             select t;
            return View("Start28", tbltemplate);
        }

        public ActionResult Start29FirstTime()
        {
            // brisanje Y pozija u fotoćelijama: za potrebe definiranja udaljenosti među njima
            var gm = db.setClearPhotochellPositionY();
            //
            var tbltemplate = db.tblTemplates.Where(t => t.IsActive == true).FirstOrDefault();
            ViewBag.korisnik = GetCurrentUser();
            ViewBag.teren = db.tblTemplates.Where(g => g.IsActive == true).FirstOrDefault();
            var te = db.vwTemplates;
            ViewBag.terens = from t in te
                             select t;
            return View("Start29", tbltemplate);
        }

        public ActionResult Start29()
        {
            var tbltemplate = db.tblTemplates.Where(t => t.IsActive == true).FirstOrDefault();
            ViewBag.korisnik = GetCurrentUser();
            ViewBag.teren = db.tblTemplates.Where(g => g.IsActive == true).FirstOrDefault();
            var te = db.vwTemplates;
            ViewBag.terens = from t in te
                             select t;
            return View("Start29", tbltemplate);
        }
        [HttpPost]
        public ActionResult AjaxSetFieldDistance(string broj = "", string distanca = "")
        {
            var sfn = db.setFieldDistance(broj, distanca);
            var tbltemplate = db.tblTemplates.Where(t => t.IsActive == true).FirstOrDefault();
            ViewBag.korisnik = GetCurrentUser();
            ViewBag.teren = db.tblTemplates.Where(g => g.IsActive == true).FirstOrDefault();
            var te = db.vwTemplates;
            ViewBag.terens = from t in te
                             select t;
            return View("teren29speedtrack", tbltemplate);
        }


        public ActionResult Games()
        {
            var tblspeedentries = db.tblSpeedEntries.Include(t => t.tblSensorType);
            //return View(tblspeedentries.ToList());

            ViewBag.korisnik = GetCurrentUser();

            // zavisno od userid -> vraca nazive na njegovom jeziku !
            //var gm = db.getGameByUser(1, "");

            var gam = db.vwGames.Where(g => g.ID > 0); ;

            ViewBag.games = from g in gam
                            select g;

            var te = db.vwTemplates;
            ViewBag.terens = from t in te
                            select t;
            return View(te.FirstOrDefault());
        }

        [HttpPost]
        public ActionResult AjaxSetCourtID(int field)
        {
            var sc = db.setCourtID(field);

            var tbltemplate = db.tblTemplates.Where(g => g.IsActive == true);

            ViewBag.korisnik = GetCurrentUser();

            var te = db.vwTemplates;
            ViewBag.terens = (from t in te
                              select t).FirstOrDefault();
            ViewBag.teren = db.tblTemplates.Where(g => g.IsActive == true).FirstOrDefault();
 
            return View("teren1");


        }

        [HttpPost]
        public ActionResult AjaxSetBrojPolja(int broj = 0)
        {

            //broj = HttpUtility.HtmlDecode(broj);
            // odradi storu i upisi ID polja u polje iz baze
            var gm = db.setFieldsNumber(1, broj);

            var tbltemplate = db.tblTemplates;
            ViewBag.brojpolja = db.tblTemplates.FirstOrDefault().FieldsNo;
            ViewBag.korisnik = GetCurrentUser();
            ViewBag.teren = db.tblTemplates.Where(g => g.IsActive == true).FirstOrDefault();
            var te = db.vwTemplates;
            ViewBag.terens = from t in te
                             select t;

           return View("teren");

        }

        [HttpPost]
        public string AjaxSetPhotochellID(int field)
        {
            var sc = db.setPhotochellID(field);

            return string.Empty;

        }

        [HttpPost]
        public ActionResult AjaxSetImePolja(string broj = "", string ime = "")
        {


            var sfn = db.setFieldName(broj,ime);

            var tbltemplate = db.tblTemplates;

            ViewBag.korisnik = GetCurrentUser();
            ViewBag.teren = db.tblTemplates.Where(g => g.IsActive == true).FirstOrDefault();
            var te = db.vwTemplates;
            ViewBag.terens = from t in te
                             select t;
            //return View("Start6", tbltemplate.FirstOrDefault());
            return View("teren6");
        }
        //
        // GET: /Configurator/Details/5

        public ActionResult Details(int id = 0)
        {
            tblTemplate tbltemplate = db.tblTemplates.Find(id);
            if (tbltemplate == null)
            {
                return HttpNotFound();
            }
            return View(tbltemplate);
        }

        //
        // GET: /Configurator/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Configurator/Create

        [HttpPost]
        public ActionResult Create(tblTemplate tbltemplate)
        {
            if (ModelState.IsValid)
            {
                db.tblTemplates.Add(tbltemplate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbltemplate);
        }

        //
        // GET: /Configurator/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tblTemplate tbltemplate = db.tblTemplates.Find(id);
            if (tbltemplate == null)
            {
                return HttpNotFound();
            }
            return View(tbltemplate);
        }

        //
        // POST: /Configurator/Edit/5

        [HttpPost]
        public ActionResult Edit(tblTemplate tbltemplate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbltemplate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbltemplate);
        }

        //
        // GET: /Configurator/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tblTemplate tbltemplate = db.tblTemplates.Find(id);
            if (tbltemplate == null)
            {
                return HttpNotFound();
            }
            return View(tbltemplate);
        }
        //
        // POST: /Configurator/Deletegame/...

        //[HttpPost, ActionName("Deletegame")]
        public ActionResult Deletegame(int id)
        {
            var dg = db.delGame(id, GetCurrentUser().ID);
            return RedirectToAction("Games", "Configurator");
        }
        //
        // POST: /Configurator/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tblTemplate tbltemplate = db.tblTemplates.Find(id);
            db.tblTemplates.Remove(tbltemplate);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult teren()
        {
            var tbltemplate = db.tblTemplates;

            ViewBag.korisnik = GetCurrentUser();
            var te = db.tblTemplates.Where(g => g.IsActive == true).FirstOrDefault();
            //ViewBag.terens = from t in te
            //                 select t;
            ViewBag.teren = db.tblTemplates.Where(g => g.IsActive == true).FirstOrDefault();

            return View("teren");

        }
        //public ActionResult teren2()
        //{
        //    var tbltemplate = db.tblTemplates;

        //    ViewBag.korisnik = GetCurrentUser();
        //    var te = db.tblTemplates.Where(g => g.IsActive == true).FirstOrDefault();
        //    //ViewBag.terens = from t in te
        //    //                 select t;
        //    ViewBag.teren = db.tblTemplates.Where(g => g.IsActive == true).FirstOrDefault();

        //    return View("teren2");

        //}
    }
}