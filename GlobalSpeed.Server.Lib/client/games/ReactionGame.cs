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
    class ReactionGame: Game
    {
        int[] plates = { 1, 2 };
        int currentPlate = 0;

        public override void Start()
        {
            base.Start();
            if (Player == null || currentPlate >= plates.Length - 1) return;
            Player.Send(string.Format("turnon!!fld{0}", plates[currentPlate++]));

        }

        public override void OnNewSignal(SpeedCourtEntryStruct entry)
        {
            int notcurrent = 1 - currentPlate;
            //if ((fldMap.Length - 1) >= entry.MatID)
            //{
            //int[] fldMap = { 0, 5, 6, 7, 9, 12, 11, 10, 8, 3, 1, 2, 4, 13, 14, 15, 16 };

            if (fldMap[entry.MatID] == plates[notcurrent])
            {
                Player.Send(string.Format("turnoffnoeffects!!fld{0}", plates[notcurrent]));
                Player.Send(string.Format("turnon!!fld{0}", plates[currentPlate]));
                currentPlate = 1 - currentPlate;
                AddEntry(entry);
            }
            //}
        }
    }
}
