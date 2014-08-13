using Alchemy.Classes;
using MvcTEST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalSpeed.Server.Lib
{
    public class MessageProcessor : Dictionary<string, Action<string, Game>>
    {
        GlobalSpeedService Service = null;
        public MessageProcessor(GlobalSpeedService service)
        {
            Service = service;
            Add("ID", IDMessage);
            Add("COUNTDOWN", COUNTDOWNMessage);
            Add("CALIBRATE", Calibrate);
            Add("LOGON", Logon);
            Add("DEFRFID", DefRFID);
            
        }

        public void Do(string msgName, string parameters, Game game)
        {
            this[msgName](parameters, game);
        }

        UserContext uc;
        public Calibration calibration;
        public Login login;
        public void Do(string msgName, string parameters, UserContext player)
        {
            uc = player;
            this[msgName](parameters, null);
        }

        private void Calibrate(string arg, Game game)
        {
            calibration = new Calibration(uc);
        }

        private void Logon(string arg, Game game)
        {
            login = new Login(uc);
        }

        private void DefRFID(string arg, Game game)
        {
            login = new Login(uc);
            login.defRFID = true;
        }

        private void IDMessage(string arg, Game game)
        {
            //arg = arg.Replace("&", ""); 
            var IDs = arg.Split(';');
            int userID = int.Parse(IDs[0]);
            int gameID = int.Parse(IDs[1]);
            SpeedCourtConfigDbEntities db = new SpeedCourtConfigDbEntities();
            var tblGame = db.tblGames.Find(gameID);
            //var tblGame = db.tblGames.Where(x => x.ID == gameID).FirstOrDefault();
            var category = db.tblCategories.Find(tblGame.CategoryID);
            
            switch (category.Name){
                case "In Out":
                    Service.game = new InOutGame();
                    break;
                case "Speed Chase": 
                    Service.game = new SpeedChaseGame();
                    break;
                case "Speed Memory":
                    Service.game = new SpeedMemoryGame();
                    break;
                case "Jump": 
                    Service.game = new JumpingGame();
                    break;
                case "Tapping":
                    Service.game = new TappingGame();
                    break;
                case "Reaction":
                    Service.game = new ReactionGame();  //id=8 in database  
                    break;
                default:
                    Service.game = new UniversalGame();
                    break;
            }
            Service.game.tblGame = tblGame;
            Service.game.Player = uc;
            Service.game.User = db.tblUsers.Find(userID);
            if (!string.IsNullOrEmpty(Service.game.tblGame.Description) && Service.game.tblGame.DescriptionDuration != null)
                Service.game.ShowInformation(Service.game.tblGame.Description, (int)Service.game.tblGame.DescriptionDuration + (Service.game.tblGame.Countdown!=null ? (int)Service.game.tblGame.Countdown:0));
            else
                Service.game.Player.Send("countdown!!" + Service.game.tblGame.Countdown.ToString());
            //Service.game.Player.Send("message!!game over");
            string fldMap = db.getFieldMap().SingleOrDefault();
            Service.game.Map(fldMap);
        }

        private void COUNTDOWNMessage(string arg, Game game)
        {
            game.Start();
        }
    }
}
