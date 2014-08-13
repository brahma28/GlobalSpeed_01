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
    
    public class InOutGame : Game
    {
        Random rnd = new Random();        
        int currentPlate = 0;
        int lastplate = 0;
        List<int> randomFields = new List<int>();
        public override void Start()
        {
            base.Start();
            var fields = tblGame.RandomFields.Split(',');
            fields.ToList().ForEach(s => randomFields.Add(Convert.ToInt32(s)));
            //if (Player == null || currentPlate >= plates.Length - 1) return;
            Player.Send(string.Format("turnon!!fld1"));

        }

        public override void OnNewSignal(SpeedCourtEntryStruct entry)
        {
            //fldMap = new int[] { 0, 5, 6, 7, 9, 12, 11, 10, 8, 3, 1, 2, 4, 13, 14, 15, 16 };
            
            if (currentPlate == 0 &&  fldMap[entry.MatID] ==1)
            {
                Player.Send(string.Format("turnoff!!fld{0}", 1));
                lastplate = RandomPlate();
                Player.Send(string.Format("turnon!!fld{0}", lastplate));
                currentPlate = 1 - currentPlate;
                AddEntry(entry);
            }
            else if (currentPlate == 1 && fldMap[entry.MatID] == lastplate)
            {
                Player.Send(string.Format("turnoff!!fld{0}", lastplate));
                Player.Send(string.Format("turnon!!fld{0}", 1));
                currentPlate = 1 - currentPlate;
                AddEntry(entry);
            }
            
        }

        private int RandomPlate()
        {
            int[] centre = {1,2,3,4};
            int val = rnd.Next(1, 17);
            while ((!randomFields.Contains(val)) || (centre.Contains(val)))
                val = rnd.Next(1, 17);
            return val;
        }
    }

    
}
