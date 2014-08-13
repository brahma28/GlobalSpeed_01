﻿using System;
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
    public class GameReactionController : GSController
    {
        private SpeedCourtConfigDbEntities db = new SpeedCourtConfigDbEntities();

        //
        // GET: /GameReaction/

        public ActionResult Index()
        {
            var tblgames = db.tblGames.Include(t => t.tblCategory);
            return View(tblgames.ToList());

        }

        //
        // GET: /GameReaction/Details/5

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
        // GET: /GameReaction/Create

        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.tblCategories, "ID", "Name");
            return View();
        }

        //
        // POST: /GameReaction/Create

        [HttpPost]
        public ActionResult Create(tblGame tblgame)
        {
            if (ModelState.IsValid)
            {
                tblgame.ReactionTime = (tblgame.ReactionTime != null) ? TimeSpan.FromMilliseconds(tblgame.ReactionTime.Value.Days) : TimeSpan.FromMilliseconds(5000);
                db.tblGames.Add(tblgame);
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("Games", "Configurator");
            }

            ViewBag.CategoryID = new SelectList(db.tblCategories, "ID", "Name", tblgame.CategoryID);
            return View(tblgame);
        }

        //
        // GET: /GameReaction/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tblGame tblgame = db.tblGames.Find(id);
            if (tblgame == null)
            {
                return HttpNotFound();
            }
            ViewBag.game = db.vwGameFull.Where(g => g.ID == id).FirstOrDefault();
            ViewBag.CategoryID = new SelectList(db.tblCategories, "ID", "Name", tblgame.CategoryID);
            return View(tblgame);
        }

        //
        // POST: /GameReaction/Edit/5

        [HttpPost]
        public ActionResult Edit(tblGame tblgame)
        {
            if (ModelState.IsValid)
            {
                tblgame.ReactionTime = (tblgame.ReactionTime != null) ? TimeSpan.FromMilliseconds(tblgame.ReactionTime.Value.Days) : TimeSpan.FromMilliseconds(1);
                db.Entry(tblgame).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Games","Configurator");
            }
            ViewBag.CategoryID = new SelectList(db.tblCategories, "ID", "Name", tblgame.CategoryID);
            return View(tblgame);
        }

        //
        // GET: /GameReaction/Delete/5

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
        // POST: /GameReaction/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            //tblResult tblResult = db.tblResult.Find(gameid);
            //db.tblResult.Remove(tblResult);
            //db.SaveChanges();
            //IQueryable<tblResult> tblRes = db.tblResults.Where(x => x.GameID == id);
            //var tblRes = (from p in db.tblResults
            //                   select p);
            ////db.tblResults.Where(x => x.GameID == id);
            //foreach (var item in tblRes)
            //{
            //    db.tblResults.del
            //}

            //db.tblResults.RemoveAll(tblRes);

            //tblGame tblgame = db.tblGames.Find(db.tblResults.Where(x => x.GameID==id).Select(s=>s.ID));
            //db.tblGames.Remove(tblgame);
            
            
            //db.SaveChanges();

            // potrebno je brisati pomocu store jer je potrebno izbrisati GameID iz tabele tblResult
            var dg = db.delGame(id, GetCurrentUser().ID);

            //return RedirectToAction("Index");
            return RedirectToAction("Games", "Configurator");
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

        public string SaveAs3(string Name = "reaction ...", bool IsRandom = false, int RandomValue = 0, short RandomType = 0, bool IsTimeLimit = false, int TimeLimit = 0, string Description = "", string Objectives = "", string Definition = "", int NumShownFields = 0, int NumRepetition = 1, string RandomFields = "", bool QuickGamePool = false, int CategoryID = 0, TimeSpan? MinStayTime = null, TimeSpan? MaxStayTime = null, TimeSpan? MinFlyTime = null, TimeSpan? MaxFlyTime = null, int StartField = 0, int Countdown = 0, bool LegLeft = false, bool LegRight = false, int Quantity = 0, int Distance = 0, int ReactionSignalID = 0, int Reaction = 0,TimeSpan? ReactionTime = null , int ReactionTypeID = 0,int MemoryTypeID = 0)
        {
            //var sng = db.setSaveAss(Name, IsRandom, RandomValue, RandomType, IsTimeLimit, TimeLimit, Description, Objectives, Definition, NumShownFields, NumRepetition, RandomFields, QuickGamePool, CategoryID, MinStayTime, MaxStayTime, MinFlyTime, MaxFlyTime, StartField, Countdown, LegLeft, LegRight, Quantity, Distance);
            // CategoryID = 8 ( reaction )
            var sng = db.setSaveAs3(Name, IsRandom, RandomValue, RandomType, IsTimeLimit, TimeLimit, Description, Objectives, Definition, NumShownFields, NumRepetition, RandomFields, QuickGamePool, 8, MinStayTime, MaxStayTime, MinFlyTime, MaxFlyTime, StartField, Countdown, LegLeft, LegRight, Quantity, Distance, ReactionSignalID, Reaction, ReactionTime, ReactionTypeID, MemoryTypeID);
            return sng.ToString();
            //return RedirectToAction("Games", "Configurator");
        }



    }
}