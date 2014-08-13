using Alchemy.Classes;
using SpeedLabLogic.Court;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalSpeed.Server.Lib
{
    public class Calibration: SensorProcessor
    {
        UserContext ctx;
        public Calibration(UserContext context)
        {
            ctx = context;
        }

        public override void OnNewSignal(SpeedCourtEntryStruct entry)
        {
            ctx.Send(String.Format("pressed!!{0}", entry.MatID));
        }
    }
}
