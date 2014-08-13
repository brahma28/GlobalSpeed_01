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
    public class GameTappingController : Controller
    {
        private SpeedCourtConfigDbEntities db = new SpeedCourtConfigDbEntities();

        //
        // GET: /GameTapping/

        public ActionResult Index()
        {
            var tblgames = db.tblGames.Include(t => t.tblCategory);
            return View(tblgames.ToList());
        }

        //
        // GET: /GameTapping/Details/5

        public ActionResult Details(int id = 0)
        {
            tblGame tblgame = db.tblGames.Find(id);
            if (tblgame == null)
            {
                return HttpNotFound();
            }
            return View(tblgame);
        }

        //
        // GET: /GameTapping/Create

        public ActionResult Create()
        {
            ViewBag.game = "Tapping";
            ViewBag.CategoryID = new SelectList(db.tblCategories, "ID", "Name", 4);
            return View();
        }

        //
        // POST: /GameTapping/Create

        [HttpPost]
        public ActionResult Create(tblGame tblgame)
        {
            if (ModelState.IsValid)
            {
                tblgame.CategoryID = 4;
                tblgame.MaxFlyTime = (tblgame.MaxFlyTime != null) ? TimeSpan.FromMilliseconds(tblgame.MaxFlyTime.Value.Days) : TimeSpan.FromMilliseconds(5000);
                tblgame.MinFlyTime = (tblgame.MinFlyTime != null) ? TimeSpan.FromMilliseconds(tblgame.MinFlyTime.Value.Days) : TimeSpan.FromMilliseconds(1);
                tblgame.MaxStayTime = (tblgame.MaxStayTime != null) ? TimeSpan.FromMilliseconds(tblgame.MaxStayTime.Value.Days) : TimeSpan.FromMilliseconds(5000);
                tblgame.MinStayTime = (tblgame.MinStayTime != null) ? TimeSpan.FromMilliseconds(tblgame.MinStayTime.Value.Days) : TimeSpan.FromMilliseconds(1);
                if (tblgame.QuickGamePool == null) { tblgame.QuickGamePool = false; }
                if (tblgame.Countdown == null) { tblgame.Countdown = 3; }
                db.tblGames.Add(tblgame);
                db.SaveChanges();
                return RedirectToAction("Games", "Configurator");
            }

            ViewBag.CategoryID = new SelectList(db.tblCategories, "ID", "Name", tblgame.CategoryID);
            return View(tblgame);
        }

        //
        // GET: /GameTapping/Edit/5

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
        // POST: /GameTapping/Edit/5

        [HttpPost]
        public ActionResult Edit(tblGame tblgame)
        {
            if (ModelState.IsValid)
            {
                if (tblgame.QuickGamePool == null) { tblgame.QuickGamePool = false; }
                db.Entry(tblgame).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Games","Configurator");
            }
            ViewBag.game = db.vwGames.Where(g => g.ID == 30).FirstOrDefault();
            ViewBag.CategoryID = new SelectList(db.tblCategories, "ID", "Name", tblgame.CategoryID);
            return View(tblgame);
        }

        //
        // GET: /GameTapping/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tblGame tblgame = db.tblGames.Find(id);
            if (tblgame == null)
            {
                return HttpNotFound();
            }
            return View(tblgame);
        }

        //
        // POST: /GameTapping/Delete/5

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

        public ActionResult SaveAs(int ID = 0)
        {

            tblGame tblgame = db.tblGames.Where(g => g.ID == ID).FirstOrDefault();
            ViewBag.game = db.vwGames.Where(g => g.ID == ID).FirstOrDefault();
            return View("SaveAs");

        }


        //
        //
        // public ActionResult SaveAsAll(int GameID = 0, string imeigre = "")
        public string SaveAsAll(int GameID = 0, string imeigre = "")
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
        // GET: /GameJumping/SetRandomFields
        [HttpPost]
        public string SetRandomFields(int GameID = 31, string Field = "")
        {

            Field = Field.Replace("fld", "");
            var rf = db.setRandomField(GameID, Field);

            return String.Empty;

        }
        // GET: /GameInOut/RandomFields
        public ActionResult RandomFields(int ID = 0)
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
            return View("RandomFields");

        }

        public ActionResult RandomFieldsAll(int ID = 0)
        {
            if (ID == 0)
            {
                ID = 31;
            };

            tblGame tblgame = db.tblGames.Where(g => g.ID == ID).FirstOrDefault();
            ViewBag.game = db.vwGames.Where(g => g.ID == ID).FirstOrDefault();
            ViewBag.CategoryID = new SelectList(db.tblCategories, "ID", "Name", tblgame.CategoryID);
            return View(tblgame);
            // return View("Edit");

        }
        [HttpPost]
        public ActionResult AjaxSetMiddleFields(string radiobutton, int GameID = 30)
        {
            radiobutton = HttpUtility.HtmlDecode(radiobutton);

            // EXEC setMiddleFields @UserID, @radiobutton, @GameID
            var mf = db.setMiddleFields(1, radiobutton, GameID);

            tblGame tblgame = db.tblGames.Find(GameID);

            if (tblgame == null)
            {
                return HttpNotFound();
            }
            ViewBag.game = db.vwGameFull.Where(g => g.ID == GameID).FirstOrDefault();
            ViewBag.CategoryID = new SelectList(db.tblCategories, "ID", "Name", tblgame.CategoryID);

            return View("Edit", tblgame);


        }

        public string SaveAs2(string Name = "nova igra", bool IsRandom = false, int RandomValue = 0, short RandomType = 0, bool IsTimeLimit = false, int TimeLimit = 0, string Description = "", string Objectives = "", string Definition = "", int NumShownFields = 0, int NumRepetition = 1, string RandomFields = "", bool QuickGamePool = false, int CategoryID = 0, TimeSpan? MinStayTime = null, TimeSpan? MaxStayTime = null, TimeSpan? MinFlyTime = null, TimeSpan? MaxFlyTime = null, int StartField = 0, int Countdown = 0, bool LegLeft = false, bool LegRight = false, int Quantity = 0, int Distance = 0)
        {
            var sng = db.setSaveAss(Name, IsRandom, RandomValue, RandomType, IsTimeLimit, TimeLimit, Description, Objectives, Definition, NumShownFields, NumRepetition, RandomFields, QuickGamePool, CategoryID, MinStayTime, MaxStayTime, MinFlyTime, MaxFlyTime, StartField, Countdown, LegLeft, LegRight, Quantity, Distance);

            return sng.ToString();

        }


    }
}