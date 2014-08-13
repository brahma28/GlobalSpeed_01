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
    public class GameInOutController : GSController
    {
        private SpeedCourtConfigDbEntities db = new SpeedCourtConfigDbEntities();

        //
        // GET: /GameInOut/

        public ActionResult Index()
        {
            var tblgames = db.tblGames.Include(t => t.tblCategory);
            return View(tblgames.ToList());
        }

        //public string RandomFieldsAll(int ID = 0)
        public string RandomFieldsAll(int ID)
        {
            tblGame tblgame = db.tblGames.Where(g => g.ID == ID).FirstOrDefault();
            //ViewBag.game = db.vwGames.Where(g => g.ID == ID).FirstOrDefault();
            //ViewBag.CategoryID = new SelectList(db.tblCategories, "ID", "Name", tblgame.CategoryID);
            //return View(tblgame);  
            return tblgame.RandomFields;

        }

        //
        // GET: /GameInOut/RandomFields
        public ActionResult RandomFields(int ID)
        {
            // ViewBag.randomfields = db.getRandomFields(GameID);
            // tblGame tblgame = db.tblGames.Where(g => g.ID == GameID).FirstOrDefault();
            tblGame tblgame = db.tblGames.Where(g => g.ID == ID).FirstOrDefault();
            // ViewBag.game = db.vwGames.Where(g => g.ID == GameID).FirstOrDefault();
            ViewBag.game = db.vwGames.Where(g => g.ID == ID).FirstOrDefault();

            //if (tblgame == null)
            //{
            //    return HttpNotFound();
            //}
            return View("RandomFields",ID);

        }

        

        //
        //
        public ActionResult SaveAs(int ID)
        {

            tblGame tblgame = db.tblGames.Where(g => g.ID == ID).FirstOrDefault();
            ViewBag.game = db.vwGames.Where(g => g.ID == ID).FirstOrDefault();
            return View("SaveAs");

        }


        //
        //
        // public ActionResult SaveAsAll(int GameID = 0, string imeigre = "")
        public string SaveAsAll(int GameID, string imeigre = "")
        {


        // EXECUTE @RC = [dbo].[setGameSaveAs]  @GameID, @Name, @GameID_new OUTPUT
            int GameIDnew = 0;
            // GameIDnew = db.setGameSaveAsNew(GameID, imeigre).FirstOrDefault();
            // GameIDnew = (int)db.setGameSaveAsNew(GameID, imeigre).FirstOrDefault();
        //
            tblGame tblgame = db.tblGames.Where(g => g.ID == GameIDnew).FirstOrDefault();
            ViewBag.game = db.vwGames.Where(g => g.ID == GameIDnew).FirstOrDefault();
            ViewBag.game.CategoryName = imeigre;
            //return View("Edit");

            return String.Empty;
        }

        // funkcija koja: ON / OFF random polja za igru: IN / OUT
        // GET: /GameInOut/SetRandomFields
        [HttpPost]
        public string SetRandomFields(int GameID, string Field = "")
        {
            Field = Field.Replace("fld", "");
            var rf = db.setRandomField(GameID, Field);
            return String.Empty;
        }

        //[HttpPost]
        //public string SetRandomFields(string Field = "")
        //{
        //    Field = Field.Replace("fld", "");
        //    var rf = db.setRandomField(Field);
        //    return String.Empty;
        //}

        public ActionResult Details(int id)
        {
            tblGame tblgame = db.tblGames.Find(id);
            if (tblgame == null)
            {
                return HttpNotFound();
            }
            return View(tblgame);
        }

        //
        // GET: /GameInOut/Create

        public ActionResult Create()
        {
            //ViewBag.CategoryID = new SelectList(db.tblCategories, "ID", "Name");
            ViewBag.teren = db.tblTemplates.Where(g => g.IsActive == true).FirstOrDefault();
            ViewBag.game = "In Out";
            ViewBag.CategoryID = new SelectList(db.tblCategories, "ID", "Name", 2);
            return View();
        }

        //
        // POST: /GameInOut/Create

        [HttpPost]
        public ActionResult Create(tblGame tblgame)
        {
            if (ModelState.IsValid)
            {
                tblgame.CategoryID = 2;
                tblgame.MaxFlyTime = (tblgame.MaxFlyTime != null) ? TimeSpan.FromMilliseconds(tblgame.MaxFlyTime.Value.Days) : TimeSpan.FromMilliseconds(5000);
                tblgame.MinFlyTime = (tblgame.MinFlyTime != null) ? TimeSpan.FromMilliseconds(tblgame.MinFlyTime.Value.Days) : TimeSpan.FromMilliseconds(1);
                tblgame.MaxStayTime = (tblgame.MaxStayTime != null) ? TimeSpan.FromMilliseconds(tblgame.MaxStayTime.Value.Days) : TimeSpan.FromMilliseconds(5000);
                tblgame.MinStayTime = (tblgame.MinStayTime != null) ? TimeSpan.FromMilliseconds(tblgame.MinStayTime.Value.Days) : TimeSpan.FromMilliseconds(1);
                db.tblGames.Add(tblgame);
                db.SaveChanges();
                return RedirectToAction("Games", "Configurator");
            }
            ViewBag.game = "In Out";
            ViewBag.CategoryID = new SelectList(db.tblCategories, "ID", "Name", 2);
            return View(tblgame);
        }

        //
        // GET: /GameInOut/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tblGame tblgame = db.tblGames.Find(id);
            if (tblgame == null)
            {
                return HttpNotFound();
            }
            ViewBag.game = db.vwGames.Where(g => g.ID == id).FirstOrDefault();
            ViewBag.CategoryID = new SelectList(db.tblCategories, "ID", "Name", tblgame.CategoryID);
            return View(tblgame);
        }

        //
        // POST: /GameInOut/Edit/5

        [HttpPost]
        public ActionResult Edit(tblGame tblgame)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblgame).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Games","Configurator");
            }
            ViewBag.CategoryID = new SelectList(db.tblCategories, "ID", "Name", tblgame.CategoryID);
            return View(tblgame);
        }

        //
        // GET: /GameInOut/Delete/5

        public ActionResult Delete(int id)
        {
            tblGame tblgame = db.tblGames.Find(id);
            if (tblgame == null)
            {
                return HttpNotFound();
            }
            return View(tblgame);
        }

        //
        // POST: /GameInOut/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tblGame tblgame = db.tblGames.Find(id);
            db.tblGames.Remove(tblgame);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public string SaveAs2(string Name = "nova igra", bool IsRandom = false, int RandomValue = 0, short RandomType = 0, bool IsTimeLimit = false, int TimeLimit = 0, string Description = "", string Objectives = "", string Definition = "", int NumShownFields = 0, int NumRepetition = 1, string RandomFields = "", bool QuickGamePool = false, int CategoryID = 0, TimeSpan? MinStayTime = null, TimeSpan? MaxStayTime = null, TimeSpan? MinFlyTime = null, TimeSpan? MaxFlyTime = null, int StartField = 0, int Countdown = 0, bool LegLeft = false, bool LegRight = false, int Quantity = 0, int Distance = 0)
        {
            //var sng = db.setSaveAs(Name, IsRandom, RandomValue, RandomType, IsTimeLimit, TimeLimit, Description, Objectives, Definition, NumShownFields, NumRepetition, RandomFields, QuickGamePool, CategoryID, MinStayTime.ToString("h:mm tt"), MaxStayTime.ToString("h:mm tt"), MinFlyTime.ToString("h:mm tt"), MaxFlyTime.ToString("h:mm tt"), StartField, Countdown, LegLeft, LegRight);
            //TimeSpan mst = MinStayTime.Value.Subtract(
            TimeSpan msft = MaxFlyTime.Value;

            var sng = db.setSaveAss(Name, IsRandom, RandomValue, RandomType, IsTimeLimit, TimeLimit, Description, Objectives, Definition, NumShownFields, NumRepetition, RandomFields, QuickGamePool, CategoryID, MinStayTime, MaxStayTime, MinFlyTime, MaxFlyTime, StartField, Countdown, LegLeft, LegRight, Quantity, Distance);

            return sng.ToString();

        }

        public ActionResult teren()
        {
            //var pp = from p in db.tblGames 
            //         where p.
            ViewBag.teren = db.tblTemplates.Where(g => g.IsActive == true).FirstOrDefault();
            return View();
        }
    }
}