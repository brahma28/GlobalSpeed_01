using SpeedLabLogic.Court;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalSpeed.Server.Lib
{
    public class SensorProcessor
    {
        public virtual void OnNewSignal(SpeedCourtEntryStruct entry) { }
        public virtual void OnNewSignal(long rfid) { }
    }
}
