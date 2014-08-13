using System;
using System.IO;
using System.IO.Ports;

namespace SpeedLabLogic.Court
{
    public enum enumSensorType
    {
        SpeedCourt = 1,
        SpeedTrack = 2,
        RFID = 3
    }

    public class Sensor
    {
        #region Private properties
        private string _portname;
        private SerialPort _port;
        private string _currentvalue;
        private string _sensorid;
        #endregion


        #region Public properties
        public string PortName
        {
            get { return _portname; }
        }

        protected SerialPort Port
        {
            get
            {
                if (_port == null)
                {
                    _port = new SerialPort(this._portname, 9600, Parity.None, 8, StopBits.One);

                    if (!_port.IsOpen)
                    { 
                        _port.DtrEnable = true;
                        _port.RtsEnable = true;
                        try
                        {
                            _port.Open();
                        }
                        catch{}
                    }
                }
                return _port;
            }
        }

        protected string SensorId
        {
            get { return _sensorid; }
            set { _sensorid = value; }
        }

        public string CurrentValue
        {
            get { return _currentvalue; }
        }

        protected delegate void SensorMessageEventHandler(object sender, EventArgs e);
        protected event SensorMessageEventHandler SensorNewMessage;


        protected virtual void OnSensorNewMessage(EventArgs e)
        {
            if (SensorNewMessage != null)
                SensorNewMessage(this, e);
        }
        #endregion


        #region Constructors
        public Sensor(string portname)
        {
            _portname = portname;
            this.Port.DataReceived += new SerialDataReceivedEventHandler(Port_DataReceived);
        }

        protected Sensor() { }
        #endregion



        void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string buffer = null;
            try
            {
                buffer = this.Port.ReadTo("\r");
            }
            catch (IOException)
            {
                return;
            }

            if (!String.IsNullOrEmpty(buffer))
            {
                //Identification string for SpeedCourt and SpeedTrack comes with "\n\n" at the beggining of string
                //and all responses from SpeedTrack, SpeedCourt and RFID reader come with "\n" at the beggining
                this._currentvalue = buffer.Trim("\n".ToCharArray());
                this.OnSensorNewMessage(EventArgs.Empty);
            }
        }

        internal void SendMessage(string message)
        {
            this.Port.Write(message);
        }

        public void Close()
        {
            if (Port.IsOpen)
            {
                this.Port.DataReceived -= Port_DataReceived;
                this.Port.DiscardInBuffer();
                this.Port.DiscardOutBuffer();
                this.Port.Close();
            }
        }
    }
}
