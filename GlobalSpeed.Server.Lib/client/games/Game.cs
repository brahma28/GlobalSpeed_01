using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Alchemy.Classes;
using SpeedLabLogic.Court;
using MvcTEST.Models;

namespace GlobalSpeed.Server.Lib
{
    public class Game: SensorProcessor
    {        
        public UserContext Player { get; set; }
        public tblUser User { get; set; }
        protected List<tblSpeedEntry> entries = new List<tblSpeedEntry>();
        protected DateTime startTime;

        
        public virtual void Start()
        {
            if (tblGame.TimeLimit.HasValue && tblGame.TimeLimit!=0)
            {                
                timer.Interval = (double)tblGame.TimeLimit * 1000;
                timer.Enabled = true;
                startTime = DateTime.Now;
            }
        }
        public tblGame tblGame = null;

        public int[] fldMap = { 0, 5, 6, 7, 9, 12, 11, 10, 8, 3, 1, 2, 4, 13, 14, 15, 16 };
        Timer timer = new Timer();
        Timer timer2 = new Timer();
        //int contacts = 0;        
        public void ShowInformation(string message, int pause) 
        {
            Player.Send(string.Format("message!!{0}", message));
            timer2.Elapsed += timer2_Elapsed;
            timer2.Interval = (double)pause * 1000;
            timer2.Enabled = true;
        }

        public void Map(string map)
        {
            string[] maps = map.Split(',');
            fldMap = new int[maps.Count()];
            for (int i = 0; i < maps.Count(); i++)
                fldMap[i] = Convert.ToInt32(maps[i]);
        }

        public Game()
        {
            //fldMap = new int[] { 0, 5, 6, 7, 9, 12, 11, 10, 8, 3, 1, 2, 4, 13, 14, 15, 16 };
            timer.Elapsed += timer_Elapsed;
            //startTime = DateTime.Now;
        }

        void timer2_Elapsed(object sender, ElapsedEventArgs e)
        {
            Player.Send("countdown!!" + tblGame.Countdown.ToString());
            timer2.Enabled = false;
        }

        void GameOver()
        {
            DateTime baseline = new DateTime(1901, 1, 1);
            TimeSpan diff = DateTime.Now.Subtract(startTime);
            Player.Send(string.Format("gameover!!{0} kontakte </br> {1} (Sec.)", entries.Count, diff.ToString("ss':'ff")));
            SpeedCourtConfigDbEntities db = new SpeedCourtConfigDbEntities();
            

            var tblResult = new tblResult
            {
                ContactNo = entries.Count,
                Distance = 0,
                GameID = tblGame.ID,
                UserID = User.ID,
                ResultDate = DateTime.Now,
                LapTime = baseline + diff
            };

            db.tblResults.Add(tblResult);
            db.SaveChanges();

            entries.ForEach(entry =>
            {
                entry.tblResult = tblResult;
                db.tblSpeedEntries.Add(entry);
            });
            db.SaveChanges();
            timer.Enabled = false;
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            GameOver();                       
        }

        protected int timelimit;
        protected int contactlimit;

        protected void AddEntry(SpeedCourtEntryStruct entry)
        {
            entries.Add(new tblSpeedEntry
                        {
                            EntryTime = DateTime.Now,
                            Interval = 0,
                            GameID = tblGame.ID,
                            InstanceID = 0,
                            SensorTypeID = 1,
                            UserID = User.ID,
                            MatID = entry.MatID
                        });

            if(tblGame.Quantity.HasValue)
                if(tblGame.Quantity!=0)
                    if (entries.Count == tblGame.Quantity)
                        GameOver();
        }
    }

    
}
