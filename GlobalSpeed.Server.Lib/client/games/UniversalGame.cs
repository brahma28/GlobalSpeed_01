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
    
    public class UniversalGame : Game
    {
        public override void Start()
        {
            
            Player.Send("message!!This game type is not yet implemented...");

        }
    }
}
