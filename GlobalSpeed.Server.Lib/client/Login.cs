using Alchemy.Classes;
using MvcTEST.Models;
using SpeedLabLogic.Court;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalSpeed.Server.Lib
{
    public class Login: SensorProcessor
    {
        public bool defRFID = false;
        UserContext ctx;
        public Login(UserContext context)
        {
            ctx = context;
        }

        public override void OnNewSignal(long rfid)
        {
            SpeedCourtConfigDbEntities db = new SpeedCourtConfigDbEntities();
            
            
            
            if(defRFID)
                ctx.Send(String.Format("rfid!!{0}", rfid.ToString()));
            else 
            {
                var user = db.tblUsers.Where(u => u.RFID == rfid).FirstOrDefault();
                if (user != null)
                    ctx.Send(String.Format("pin!!{0}", user.PIN));
            }
            
        }
    }
}
