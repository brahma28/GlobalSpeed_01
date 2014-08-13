using MvcTEST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcTEST.Controllers
{
    public class LogonController : GSController
    {
        //
        // GET: /Logon/
        private SpeedCourtConfigDbEntities db = new SpeedCourtConfigDbEntities();

        public ActionResult Index()
        {

            if (Session["User"]==null )
                Session["User"] = db.tblUsers.Where(x => x.ID == 1).FirstOrDefault();
            return View();
        }

        //public ActionResult Index( string id, string name,string type, string value)
        //{

        //    var aa = (from a in db.tblUsers
        //                where a.PIN == value
        //                select a);

        //    if (aa.Count() > 0)
        //    {
        //        Session["User"] = aa.First();
        //        switch (aa.FirstOrDefault().Role)
        //        {
        //        //QUICKGAME: nepoznata osoba
        //            case 10:
        //                return RedirectToAction("Index", "UserHome", new { role = 10 });
                        
        //        //IGRAČ:
        //            case 20:
        //                return RedirectToAction("Index", "UserHome", new { role = 20 });

        //        //IGRAČ + internet prava korisnik:
        //            case 30:
        //                return RedirectToAction("Index", "UserHome", new { role = 30 });

        //        //TRENER: 
        //            case 40:
        //                return RedirectToAction("Index", "Home", new { role = 40 });

        //        //VLASNIK: admin
        //            case 50:
        //                return RedirectToAction("Index", "AdminHome", new { role = 50 });


        //            default:
        //                break;
        //        }
        //        //return RedirectToAction("Index", "Home");
        //    }
        //    else
        //    { return RedirectToAction("Index", "Logon"); }
        //    //}

        //    return View();
        //}

        
        public ActionResult Index2( string id, string name,string type, string value)
        {

            var aa = (from a in db.tblUsers
                        where a.PIN == value
                        select a);

            if (aa.Count() > 0)
            {
                //-- Last login --
                aa.FirstOrDefault().LastLogin = DateTime.Now;
                db.SaveChanges();

                //FormsAuthentication.SetAuthCookie(aa.FirstOrDefault().Name, false );

                Session["User"] = aa.First();
                switch (aa.FirstOrDefault().Role)
                {
                //QUICKGAME: nepoznata osoba
                    case 10:
                        return RedirectToAction("Index", "UserHome");
                        
                //IGRAČ:
                    case 20:
                        return RedirectToAction("Index", "UserHome");

                //IGRAČ + internet prava korisnik:
                    case 30:
                        return RedirectToAction("Index", "UserHome");

                //TRENER: 
                    case 40:
                        return RedirectToAction("Index", "Home");

                //VLASNIK: admin
                    case 50:
                        return RedirectToAction("Index", "AdminHome");


                    default:
                        break;
                }
                //return RedirectToAction("Index", "Home");
            }
            else
            { return RedirectToAction("Index", "Logon"); }
            //}

            return View();
        }

        //[HttpPost]
        //public ActionResult Index(tblUser model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //int a1 = 0;
        //        //if (int.TryParse(Request["ss"], out a1))
        //        //{
        //        var aa = (from a in db.tblUsers
        //                 where a.PIN == model.PIN //a1
        //                 select a);

        //        if (aa.Count() > 0)
        //        {
        //            Session["User"] = aa.First();
        //            switch (aa.FirstOrDefault().Role)
        //            {
        //                case 40:
        //                    return RedirectToAction("Index", "Home");
        //                case 10:
        //                    //return RedirectToAction("Index", "UserHome");
        //                case 20:
        //                case 30:
        //                    return RedirectToAction("Index", "UserHome");
        //                case 50:
        //                    return RedirectToAction("Index", "AdminHome");
        //                default:
        //                    break;
        //            }
        //            //return RedirectToAction("Index", "Home");
        //        }
        //        else
        //        { return RedirectToAction("Index", "Logon"); }
        //        //}
        //    }

        //    return View();
        //}

        //
        // GET: /Logon/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Logon/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Logon/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Logon/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Logon/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Logon/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Logon/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
