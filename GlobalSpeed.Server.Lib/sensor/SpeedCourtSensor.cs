using System;
using System.Collections;
using System.Collections.Generic;

namespace SpeedLabLogic.Court
{
    /// <summary>
    /// Contains ID of activated sensor (0 in case of deactivation), time of entry and
    /// interval since last entry in miliseconds.
    /// </summary>
    public struct SpeedCourtEntryStruct
    {
        private int _matid;
        private DateTime _entrytime;
        private int _interval;
        private bool _hasvalue;

        public int MatID
        {
            get { return _matid; }
        }

        public DateTime EntryTime
        {
            get { return _entrytime; }
        }

        public int Interval
        {
            get { return _interval; }
        }

        public bool HasValue
        {
            get { return _hasvalue; }
        }

        public SpeedCourtEntryStruct(int matid, DateTime entrytime, int interval)
        {
            _matid = matid;
            _entrytime = entrytime;
            _interval = interval;
            _hasvalue = true;
        }

        /// <summary>
        /// Constructor that represents empty struct with HasValue == false;
        /// </summary>
        /// <param name="isempty">Value is whatever</param>
        public SpeedCourtEntryStruct(bool isempty)
        {
            _matid = -1;
            _entrytime = DateTime.Now;
            _interval = 0;
            _hasvalue = false;
        }
    }


    /// <summary>
    /// Handles SpeedCourt hardware input
    /// </summary>
    public class SpeedCourtSensor : Sensor
    {
        private const string SPEED_COURT_ID = "KMS-AUSW-USB01.C V003 2012-08-17";//"KMS-AUSWERTER02.C V001 2011-07-18";

        #region Private properties
        private int _noofmats = 16;//must be configured from outside and must be dividable by 4!
        List<SpeedCourtEntryStruct> _loglist;
        private bool _isportvalid;
        private int _currentvaluelength = -1;
        #endregion


        #region Public properties

        /// <summary>
        /// Number of connected mats
        /// </summary>
        public int NoOfMats
        {
            get
            {
                return _noofmats;
            }
        }

        public List<SpeedCourtEntryStruct> LogList
        {
            get
            {
                if (_loglist == null) _loglist = new List<SpeedCourtEntryStruct>();
                return _loglist; 
            }
        }

        public bool IsPortValid
        {
            get { return _isportvalid; }
        }

        public string SensorID
        {
            get { return base.SensorId; }
        }

        public int CurentValueLength
        {
            get
            {
                if(_currentvaluelength < 0)
                {
                    if(this.NoOfMats <= 24)
                    {
                        _currentvaluelength = 19;
                    }
                    else if(this.NoOfMats > 24 && this.NoOfMats <= 32)
                    {
                        _currentvaluelength = 21;
                    }
                    else
                    {
                        throw new Exception("Number of sensor is greater than 32 which is not allowed!!!!");
                    }
                }
                return _currentvaluelength;
            }
        }
        #endregion


        #region Constructors
        private SpeedCourtSensor() { }

        public SpeedCourtSensor(string portname) : base(portname)
        {
            //Port identification
            this.Port.Write("\r");

            //Waiting for identification with 3 sec timeout 
            DateTime start = DateTime.Now;
            double miliseconds = 0;
            do
            {
                miliseconds = DateTime.Now.Subtract(start).TotalMilliseconds;
            }
            while (String.IsNullOrEmpty(this.CurrentValue) && miliseconds < Convert.ToDouble(3000));
                        
            if (String.IsNullOrEmpty(this.CurrentValue) || this.CurrentValue.Length < 8 || !this.CurrentValue.StartsWith(SPEED_COURT_ID.Substring(0, 8)))
            {
                _isportvalid = false;
            }
            else
            {
                base.SensorId = this.CurrentValue;
                _isportvalid = true;
                //Query for number of mats
                this.QueryNoOfMats();
                this._noofmats = Convert.ToInt32(this.CurrentValue);
                this.SensorNewMessage += new SensorMessageEventHandler(SpeedCourtSensor_SensorNewMessage);
            }
        }
        #endregion


        #region Events
        public delegate void SpeedCourtSensorMessageEventHandler(object sender, EventArgs e);
        public event SpeedCourtSensorMessageEventHandler SpeedCourtSensorNewMessage;

        public virtual void OnSpeedCourtSensorNewMessage(EventArgs e)
        {
            if (SpeedCourtSensorNewMessage != null)
                SpeedCourtSensorNewMessage(this, e);
        }

        void SpeedCourtSensor_SensorNewMessage(object sender, EventArgs e)
        {
            //if (base.CurrentValue.Length != this.CurentValueLength)
            //{
            //    return;
            //}

            SpeedCourtEntryStruct entry = this.ParsePortValue(base.CurrentValue);
            if (entry.MatID > 0)//Sensor switching off is ignored
            {
                this.LogList.Add(entry);
                this.OnSpeedCourtSensorNewMessage(EventArgs.Empty);
            }
        }
        #endregion

        #region Instance Methodes

        #region Public Methodes
        public SpeedCourtEntryStruct GetLastEntry()
        {
            SpeedCourtEntryStruct lastentry = new SpeedCourtEntryStruct(true);
            if(this.LogList.Count > 0) lastentry = this.LogList[this.LogList.Count - 1];
            return lastentry;
        }

        /// <summary>
        /// Checks number of connected mats and refreshes NoOfMats value
        /// </summary>
        public void QueryNoOfMats()
        {
            //Query for number of mats
            string previousValue = base.CurrentValue;
            base.SendMessage("b");

            //Waiting for number of mats
            do{}while (this.CurrentValue == previousValue);

            this._noofmats = Convert.ToInt32(this.CurrentValue);
        }

        public void ClearCurrentEntries()
        {
            this.LogList.Clear();
        }
        #endregion

        #region Private Methods
        private SpeedCourtEntryStruct ParsePortValue(string portvalue)
        {
            int mateid = 0;
            DateTime entrytime = DateTime.Now;
            int interval = 0;
            SpeedCourtEntryStruct previousentry = this.GetLastEntry();

            if (previousentry.HasValue) interval = Convert.ToInt32(entrytime.Subtract(previousentry.EntryTime).TotalMilliseconds);

            int maskLength = this.NoOfMats / 4 + (this.NoOfMats % 4 != 0 ? 1 : 0);
            int maskStartIndex = portvalue.Length - maskLength;
            string mask = portvalue.Substring(maskStartIndex, maskLength);

            BitArray bitmask = Util.Methods.ConvertHexToBitArray(mask);
            for (int i = bitmask.Count; i > 0 ; i--)
            {
                int index = i - 1;
                if (index < 0) throw new Exception("Invalid index!");
                if (bitmask[index])
                {
                    mateid = bitmask.Count - index;
                    if (mateid > this.NoOfMats) mateid = 0;
                    break;
                }
            }

            SpeedCourtEntryStruct entry = new SpeedCourtEntryStruct(mateid, entrytime, interval);
            return entry;
        }
        #endregion

        #endregion
    }
}
