using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Threading;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Net.Mail;
using System.Text;
using System.Net;
using MvcTEST.Models;
using MvcTEST.HtmlHelpers;

namespace MvcTEST.Controllers
{
    public class BreakController : GSController
    {
        private SpeedCourtConfigDbEntities db = new SpeedCourtConfigDbEntities();

        //
        // GET: /Break/

        public ActionResult Index()
        {
            return View(db.tblBreaks.ToList());
        }

        //
        // GET: /Break/Details/5

        public ActionResult Details(int id = 0)
        {
            ViewBag.errori = "";
            //ViewBag.users = db.tblUsers.Where(g => g.IsActiv == true).ToList();
            ViewBag.users = db.vwUsers.ToList();
            tblBreak tblbreak = db.tblBreaks.Find(id);
            if (tblbreak == null)
            {
                return HttpNotFound();
            }
            return View(tblbreak);
        }

        //
        // GET: /Break/Create

        public ActionResult Create()
        {
            ViewBag.errori = "";
            ViewBag.CategoryID = new SelectList(db.tblCategories, "ID", "Name");
            ViewBag.users = db.vwUsers.ToList();

            tblBreak br = new tblBreak();
            br.DownOnCount = 3; // odbrojavanje od broja
            br.DownOnTime = 10; // trajanje pauze
            br.Signal1Time = 5; // prvi upozoravajuci signal (ograniciti na max. 1 sec.)
            br.Signal2Time = 3; // signal odbrojavanja (ograniciti na max. 1 sec.)

            return View(br);
            //return View();
        }

        //
        // POST: /Break/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(tblBreak tblbreak)
        {
            ViewBag.errori = "";
            ViewBag.users = db.tblUsers.Where(g => g.IsActiv == true).ToList();
            if (ModelState.IsValid)
            {

                if (tblbreak.fileUpload1 != null)
                {
                    Stream stream1 = Request.Files["fileUpload1"].InputStream;
                    byte[] buffer1 = new byte[stream1.Length];
                    stream1.Read(buffer1, 0, (int)stream1.Length);
                    tblbreak.Signal1Acustic = buffer1;
                }

                if (tblbreak.fileUpload2 != null)
                {
                    Stream stream2 = Request.Files["fileUpload2"].InputStream;
                    byte[] buffer2 = new byte[stream2.Length];
                    stream2.Read(buffer2, 0, (int)stream2.Length);
                    tblbreak.Signal2Acustic = buffer2;
                }

                db.tblBreaks.Add(tblbreak);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblbreak);
        }

        //
        // GET: /Break/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ViewBag.errori = "";
            ViewBag.users = db.vwUsers.ToList();
            tblBreak tblbreak = db.tblBreaks.Find(id);
            if (tblbreak == null)
            {
                return HttpNotFound();
            }
            return View(tblbreak);
        }

        //
        // POST: /Break/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblBreak tblbreak)
        {
            ViewBag.users = db.vwUsers.ToList();
            var allowedExtensions = new[] { ".wav", ".mp3" };
            //var extension = Path.GetExtension(fileUpload1.FileName);
            ViewBag.errori = "";
            if (ModelState.IsValid)
            {

                if (tblbreak.fileUpload1 != null)
                {
                    int iFile1Size = Request.Files["fileUpload1"].ContentLength;
                    if (iFile1Size > 1000000)  // 1MB otprilike
                    {
                        // File is too big so do something here
                        ViewBag.errori = "Accustical 1 zu groß!";
                    }
                    else
                    {
                        Stream stream1 = Request.Files["fileUpload1"].InputStream;
                        byte[] buffer1 = new byte[stream1.Length];
                        stream1.Read(buffer1, 0, (int)stream1.Length);
                        tblbreak.Signal1Acustic = buffer1;
                    }
                }

                if (tblbreak.fileUpload2 != null)
                {
                    int iFile2Size = Request.Files["fileUpload2"].ContentLength;
                    if (iFile2Size > 1000000)  // 1MB otprilike
                    {
                        // File is too big so do something here
                        ViewBag.errori = "Accustical 2 zu groß!";
                    }
                    else
                    {

                        Stream stream2 = Request.Files["fileUpload2"].InputStream;
                        byte[] buffer2 = new byte[stream2.Length];
                        stream2.Read(buffer2, 0, (int)stream2.Length);
                        tblbreak.Signal2Acustic = buffer2;
                    }
                }

                db.Entry(tblbreak).State = EntityState.Modified;
                if (ViewBag.errori == "")
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.users = db.vwUsers.ToList();

                }
            }
            return View(tblbreak);
        }

        //
        // GET: /Break/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ViewBag.errori = "";
            tblBreak tblbreak = db.tblBreaks.Find(id);
            if (tblbreak == null)
            {
                return HttpNotFound();
            }
            return View(tblbreak);
        }

        //
        // POST: /Break/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.errori = "";
            tblBreak tblbreak = db.tblBreaks.Find(id);
            db.tblBreaks.Remove(tblbreak);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        public ActionResult myaudiosignal1(int? id = null)
        {
            if( id.HasValue) 
            {
                var file = Server.MapPath("~/ImagesApp/BeepShort.mp3");
                var ff = (from f in db.tblBreaks
                          where f.BreakID == id
                          select f).FirstOrDefault();
                if (ff.Signal1Acustic != null)
                {
                    return File(ff.Signal1Acustic, "audio/mp3");
                }
                else
                {
                    return File(file, "audio/mp3");
                }
            }
            else
            {
                var file = Server.MapPath("~/ImagesApp/BeepShort.mp3");
                return File(file, "audio/mp3");
            }

        }
        public ActionResult myaudiosignal2(int? id = null)
        {
            if( id.HasValue) 
            {
                var file = Server.MapPath("~/ImagesApp/BeepShort.mp3");
                var ff = (from f in db.tblBreaks
                          where f.BreakID == id
                          select f).FirstOrDefault();
                if (ff.Signal2Acustic != null)
                {
                    return File(ff.Signal2Acustic, "audio/mp3");
                }
                else
                {
                    return File(file, "audio/mp3");
                }
            }
            else
            {
                var file = Server.MapPath("~/ImagesApp/BeepShort.mp3");
                return File(file, "audio/mp3");
            }

        }

    }
}