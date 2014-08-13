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
    class SpeedMemoryGame : Game
    {
        int[] plates = { 5, 8, 2, 3, 5, 8, 1, 11, 4, 7, 12, 4, 5, 1, 3, 4, 0 };
        int b = 1;
        Random a = new Random();
        int currentPlate = 1;
        //Timer timer = new Timer();

        public SpeedMemoryGame()
        {
            //timer.Elapsed += OnTimer;
            //timer.Interval = 6000;
            //timer.Enabled = true;
        }

        //void OnTimer(object sender, ElapsedEventArgs  e)
        //{
        //    if (Player== null || currentPlate >= plates.Length - 1) return;
        //    Player.Send(string.Format("turnon!!fld{0}", plates[currentPlate++]));

        //    timer.Enabled = false;
        //}

        public override void Start()
        {
            base.Start();
            if (Player == null || currentPlate >= plates.Length - 1) return;
            //Player.Send(string.Format("turnon!!fld{0}", plates[currentPlate++]));
            b = a.Next(1, 13);
            Player.Send(string.Format("turnon!!fld{0}", b));

        }

        public override void OnNewSignal(SpeedCourtEntryStruct entry)
        {
            if (currentPlate > 0 && fldMap[entry.MatID] == b) //plates[currentPlate - 1] )
            {                
                //Player.Send(string.Format("turnoff!!fld{0}", plates[currentPlate - 1]));
                //Player.Send(string.Format("turnon!!fld{0}", plates[currentPlate++]));
                Player.Send(string.Format("turnoff!!fld{0}", b));
                b = a.Next(1, 13);
                Player.Send(string.Format("turnon!!fld{0}", b));

                AddEntry(entry);
            }
        }
    }

        
}
