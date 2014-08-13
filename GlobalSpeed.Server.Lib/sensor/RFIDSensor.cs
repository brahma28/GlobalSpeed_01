using System;
using System.Text;
using System.Globalization;

namespace SpeedLabLogic.Court
{
    public class RFIDSensor : Sensor
    {
        private const string RFID_ID = "MULTITAG-125  01";
        private string _previousvalue;
        private bool _isportvalid;
        static private RFIDSensor _instance;
        static bool _isinstancecreated = false;

        private string PreviousValue
        {
            get { return _previousvalue; }
            set { _previousvalue = value; }
        }

        public bool IsPortValid
        {
            get { return _isportvalid; }
        }

        public string SensorID
        {
            get { return base.SensorId; }
        }

        static public RFIDSensor Instance
        {
            get
            {
                if (_instance == null)
                {
                    //throw new Exception("RFIDSensor instance not cretated!");

                }
                    return _instance;
            }
        }

        public static bool IsInstanceCreated
        {
            get { return RFIDSensor._isinstancecreated; }
        }

        public long CurrentValueRFID
        {
            get
            {
                if (String.IsNullOrEmpty(base.CurrentValue)) return -1;
                return Int64.Parse(base.CurrentValue.TrimStart("U".ToCharArray()), NumberStyles.HexNumber);
            }
        }

        #region Constructors
        private RFIDSensor() { }

        private RFIDSensor(string portname) : base(portname)
        {
            if (Port.IsOpen)
            {
                base.Port.DiscardInBuffer();
                base.Port.DiscardOutBuffer();
                base.Port.Close();
                base.Port.Open();

                //Send message to exit continious reading in order to query for device ID
                string s = ASCIIEncoding.ASCII.GetString(new byte[] { 4, 4 });//EOT, EOT
                base.Port.Write(s);

                //Waiting for response with 3 sec timeout
                DateTime start = DateTime.Now;
                double miliseconds = 0;
                do
                {
                    miliseconds = DateTime.Now.Subtract(start).TotalMilliseconds;
                }
                while (String.IsNullOrEmpty(this.CurrentValue) && miliseconds < Convert.ToDouble(3000));

                string previousValue = this.CurrentValue;
                //Reset comand, reader is set 'continious read mode' and returns ID string with firmware version
                base.Port.Write("x");

                //Waiting for response with 3 sec timeout
                start = DateTime.Now;
                miliseconds = 0;
                do
                {
                    miliseconds = DateTime.Now.Subtract(start).TotalMilliseconds;
                }
                while (this.CurrentValue == previousValue && miliseconds < Convert.ToDouble(3000));

                if (String.IsNullOrEmpty(this.CurrentValue) || this.CurrentValue.Length < 8 || !this.CurrentValue.StartsWith(RFID_ID.Substring(0, 8)))
                {
                    _isportvalid = false;
                }
                else
                {
                    base.SensorId = this.CurrentValue;
                    _isportvalid = true;
                    this.SensorNewMessage += new SensorMessageEventHandler(RFIDSensor_SensorNewMessage);
                }
            }
        }
        #endregion


        #region Events
        public delegate void RFIDSensorMessageEventHandler(object sender, EventArgs e);
        public event RFIDSensorMessageEventHandler RFIDSensorNewMessage;

        public virtual void OnRFIDSensorNewMessage(EventArgs e)
        {
            if (RFIDSensorNewMessage != null)
                RFIDSensorNewMessage(this, e);
        }

        void RFIDSensor_SensorNewMessage(object sender, EventArgs e)
        {
            //if (this.PreviousValue != base.CurrentValue && base.CurrentValue.Length == 11)
            //{
            //    this.PreviousValue = base.CurrentValue;
                this.OnRFIDSensorNewMessage(EventArgs.Empty);
            //}
        }
        #endregion

        public static void Create(string portname)
        {
            _instance = new RFIDSensor(portname);
            _isinstancecreated = true;
        }

        public void Destruct()
        {
            ((Sensor)_instance).Close();
            _instance = null;
            _isinstancecreated = false;
        }
    }
}
