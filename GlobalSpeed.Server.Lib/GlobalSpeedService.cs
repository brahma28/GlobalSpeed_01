using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.IO.Ports;
using SpeedLabLogic.Court;
using Alchemy.Classes;
using Alchemy;
using System.Net;


namespace GlobalSpeed.Server.Lib
{
    public class GlobalSpeedService
    {
        MessageProcessor messageProcessor = null;
        public ICommunication Communication { get; set; }
        public void Start(string[] args = null) 
        {
            messageProcessor = new MessageProcessor(this);

            if (Communication == null)
                Communication = new NullCommunication();
            Communication.WriteLine("Starting GlobalSpeed service...");
            Communication.WriteLine("");
            Init();
        }
        public void Stop() 
        {
            if (RFIDSensor.IsInstanceCreated)
            {
                RFIDSensor.Instance.Destruct();
            }

            if (sc_sensor != null)
            {
                sc_sensor.Close();
                sc_sensor = null;
            }
            if (wsServer != null) 
            { 
                wsServer.Stop();  
            }
            //Environment.Exit(0);
        }

        WebSocketServer wsServer = null;
        //================================================================
                
        static List<UserContext> users = new List<UserContext>();        
        public Game game = null;
        SpeedCourtSensor sc_sensor;
        void Init() 
        {
            //game = new SpeedChaseGame();

            string[] portnames = SerialPort.GetPortNames();
            foreach (string portname in portnames)
            {
                var sc_sensor2 = new SpeedCourtSensor(portname);
                if (sc_sensor2.IsPortValid)
                {
                    Communication.WriteLine("Sensor ID: " + sc_sensor2.SensorID);
                    Communication.WriteLine("Sensor port: " + sc_sensor2.PortName);
                    Communication.WriteLine("Number of mats: " + sc_sensor2.NoOfMats.ToString());
                    Communication.WriteLine("");
                    Communication.WriteLine("SpeedCourt listening started!!");
                    Communication.WriteLine("");

                    sc_sensor2.SpeedCourtSensorNewMessage += SpeedCourtSensorNewMessage;
                    sc_sensor = sc_sensor2;
                }
                else
                {
                    sc_sensor2.Close();
                    sc_sensor2 = null;
                }
            }
            foreach (string portname in portnames)
            {
                RFIDSensor.Create(portname);
                if (RFIDSensor.Instance.IsPortValid)
                {
                    Communication.WriteLine("Sensor ID: " + RFIDSensor.Instance.SensorID);
                    Communication.WriteLine("Sensor port: " + RFIDSensor.Instance.PortName);
                    Communication.WriteLine("");
                    Communication.WriteLine("RFID listening started!!");
                    Communication.WriteLine("");

                    RFIDSensor.Instance.RFIDSensorNewMessage += RFIDSensorNewMessage;
                    break;
                }
                else
                {
                    RFIDSensor.Instance.Destruct();
                }
            }
                        
            wsServer = new WebSocketServer(8080, IPAddress.Any)
            {
                OnReceive = OnReceive,
                OnSend = OnSend,
                OnConnect = OnConnect,
                OnConnected = OnConnected,
                OnDisconnect = OnDisconnect,
                TimeOut = new TimeSpan(0, 5, 0)
            };

            wsServer.Start();
            Communication.WriteLine("Web Socket Server started!!");
            Communication.WriteLine("");
        }

        void RFIDSensorNewMessage(object sender, EventArgs e)
        {
            RFIDSensor rfid_sensor = (RFIDSensor)sender;
            string value = rfid_sensor.CurrentValue;
            Communication.WriteLine("String: " + value + " Long: " + rfid_sensor.CurrentValueRFID.ToString());
            if (messageProcessor.login != null)
                messageProcessor.login.OnNewSignal(rfid_sensor.CurrentValueRFID);
        }
               

        void SpeedCourtSensorNewMessage(object sender, EventArgs e)
        {
            SpeedCourtSensor sensor = (SpeedCourtSensor)sender;
            SpeedCourtEntryStruct lastentry = sensor.GetLastEntry();
            Communication.WriteLine(String.Format("Time: {0}   Interval: {1}  ID: {2}", lastentry.EntryTime.ToString(), lastentry.Interval.ToString(), lastentry.MatID.ToString()));
            //Communication.WriteLine("");
            SpeedEntry.CreateNew(lastentry, 0);

            if (game != null)
                game.OnNewSignal(lastentry);                  
            if(messageProcessor!=null)
                if (messageProcessor.calibration != null)
                    messageProcessor.calibration.OnNewSignal(lastentry); 
        }

        void OnConnect(UserContext context)
        {
            Communication.WriteLine("Client Connection From: " + context.ClientAddress);
            users.Add(context);
            if (game != null)
                game.Player = context;
        }

        void OnConnected(UserContext context)
        {
            Communication.WriteLine("Client Connection From: " +
            context.ClientAddress.ToString());
        }

        void OnReceive(UserContext context)
        {
            Communication.WriteLine("Received Data From: " + context.ClientAddress);

            string msg = context.DataFrame.ToString();
            string ss =msg.Substring(0,2);
            if (ss == "ID" || ss == "CO" || ss =="LO"  || ss =="CA" || ss =="DE")
            {
                Communication.WriteLine("Data: " + msg);
                string[] split = msg.Split(new string[] { "!!" }, StringSplitOptions.RemoveEmptyEntries);
                if (game != null && split[0] != "ID" && split[0] != "LOGON" && split[0] != "CALIBRATE" && split[0] != "DEFRFID")
                    messageProcessor.Do(split[0], split[1], game);
                else
                    messageProcessor.Do(split[0], split[1], context);
            }
        }

        void OnDisconnect(UserContext context)
        {
            Communication.WriteLine("Client Disconnected: " + context.ClientAddress);
        }

        void OnSend(UserContext context)
        {
            Communication.WriteLine("Data Send To: " + context.ClientAddress);
        }        
    }
}
