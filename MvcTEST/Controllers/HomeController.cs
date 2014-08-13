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
    public class HomeController : GSController 
    {
        public string Ping()
        {
            return "";
        }

        private SpeedCourtConfigDbEntities db = new SpeedCourtConfigDbEntities();

        //[Authorize(Roles="40")]
        public ActionResult Index()
        {
            ViewBag.korisnik = GetCurrentUser();
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            //var vwTerms = from a in db.vwTerms
            ViewBag.datumtermina = DateTime.Now;
            //var datumtermina = DateTime.Now.AddDays(-365);
            var datumtermina = DateTime.Now;
            var ee = db.getTerminInDay(datumtermina);
            var ss = from w in ee
                     //where a.DateStart.ToShortDateString == "22.10.2012"
                     select w;
            //var dd = ViewBag.ss.
            //ViewBag.podaci = from b in db.vwResult_Part1
            //                 select b;
            //ViewBag.podaci2 = from c in db.vwResult_Part2
            //                  select c;
            ViewBag.podaci = from b in db.vwResult_Part01
                             select b;
            ViewBag.podaci2 = from c in db.vwResult_Part02
                              select c;
            return View(ss);

        }

        //var date = $('#datepicker').datepicker({ dateFormat: 'dd-mm-yy' }).val();

        [HttpPost]
        public ActionResult Index(string id)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;

            var datumtermina = Convert.ToDateTime(id, CultureInfo.InvariantCulture); //DateTime.ParseExact(id,"MM/dd/yyyy",provider);
            ViewBag.datumtermina = datumtermina;            
            var ee = db.getTerminInDay(datumtermina);
            var ss = from w in ee
                     select w;


            //DateTime.Parse()
                //return View();
                return View("Termini",ss);
        }

        [HttpPost]

        public ActionResult Index2()
        {
            ViewBag.korisnik = GetCurrentUser();
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            //var vwTerms = from a in db.vwTerms
            var dt = Request["datum"];
            var datumtermina = DateTime.Now ;
            ViewBag.datumtermina = datumtermina;
            ViewBag.datumtermina = DateTime.Now;
            DateTime dateValue = DateTime.Now;
            DateTime.TryParse(dt, out datumtermina);
            //DateTime.TryParseExact (dt, out datumtermina);
            var ee = db.getTerminInDay(datumtermina);
            var ss = from w in ee
                     //where a.DateStart.ToShortDateString == "22.10.2012"
                     select w;
            //var dd = ViewBag.ss.
            ViewBag.podaci = from b in db.vwResult_Part1
                             select b;
            ViewBag.podaci2 = from c in db.vwResult_Part2
                              select c;

            return View(ss);

        }
        public string getTerminDates(int ID, DateTime datum)
        {

            string datumi = null;
            datumi = db.getDatepickerDates(ID, null, null, datum.Date).FirstOrDefault().rumpelstiltskin;
            return datumi;
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
