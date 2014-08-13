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
    
    public class JumpingGame : Game
    {
        int[] plates = { 1, 1 };
        int currentPlate = 0;

        public override void Start()
        {
            base.Start();
            if (Player == null || currentPlate >= plates.Length - 1) return;
            Player.Send(string.Format("turnon!!fld{0}", plates[currentPlate++]));

        }

        public override void OnNewSignal(SpeedCourtEntryStruct entry)
        {
            //fldMap = new int[] { 0, 5, 6, 7, 9, 12, 11, 10, 8, 3, 1, 2, 4, 13, 14, 15, 16 };

            int notcurrent = 1 - currentPlate;
            if (fldMap[entry.MatID] == plates[notcurrent])
            {
                Player.Send(string.Format("turnoff!!fld{0}", plates[notcurrent]));
                Player.Send(string.Format("turnon!!fld{0}", plates[currentPlate]));
                currentPlate = 1 - currentPlate;
                AddEntry(entry);
            }
        }
    }
        
}
