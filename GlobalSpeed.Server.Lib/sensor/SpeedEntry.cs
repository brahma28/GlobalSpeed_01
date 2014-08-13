using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace SpeedLabLogic.Court
{
    public class SpeedEntry
    {

        private void CheckDataSet()
        {
            if (!IsDataSet)
                SetData();
        }


        #region Constructors
        public SpeedEntry(int id)
        {
            _id = id;
        }

        private SpeedEntry(int id, int matid, DateTime entrytime, int interval, int sensortypeid, int instanceid)
        {
            _id = id;
            _matid = matid;
            _entrytime = entrytime;
            _interval = interval;
            _sensortypeid = sensortypeid;
            _instanceid = instanceid;
            IsDataSet = true;
        }

        private SpeedEntry()
        {
        }
        #endregion


        #region Private fields
        private bool IsDataSet;
        private int _id;
        private int _matid;
        private DateTime _entrytime;
        private int _interval;
        private int _sensortypeid;
        private int _instanceid;
        #endregion


        #region Public properties
        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                CheckDataSet();
                _id = value;
            }
        }

        public int MatID
        {
            get
            {
                CheckDataSet();
                return _matid;
            }
            set
            {
                CheckDataSet();
                _matid = value;
            }
        }

        public DateTime EntryTime
        {
            get
            {
                CheckDataSet();
                return _entrytime;
            }
            set
            {
                CheckDataSet();
                _entrytime = value;
            }
        }

        public int Interval
        {
            get
            {
                CheckDataSet();
                return _interval;
            }
            set
            {
                CheckDataSet();
                _interval = value;
            }
        }

        public int SensorTypeID
        {
            get
            {
                CheckDataSet();
                return _sensortypeid;
            }
            set
            {
                CheckDataSet();
                _sensortypeid = value;
            }
        }

        public int InstanceID
        {
            get
            {
                CheckDataSet();
                return _instanceid;
            }
            set
            {
                CheckDataSet();
                _instanceid = value;
            }
        }

        #endregion


        private void SetData()
        {
            
        }

        public static SpeedEntry CreateNew(int matid, DateTime entrytime, int interval, int sensortypeid, int instanceid)
        {
            SpeedEntry speedentry = new SpeedEntry();
            speedentry._id = 0;
            speedentry._matid = matid;
            speedentry._entrytime = entrytime;
            speedentry._interval = interval;
            speedentry._sensortypeid = sensortypeid;
            speedentry._instanceid = instanceid;
            speedentry.IsDataSet = true;
            return speedentry;
                       
        }

        public static SpeedEntry CreateNew(SpeedCourtEntryStruct entry, int instanceid)
        {
            return SpeedEntry.CreateNew(entry.MatID, entry.EntryTime, entry.Interval, (int)enumSensorType.SpeedCourt, instanceid);
        }

        

        public static List<SpeedEntry> ListByInstance(int instanceID)
        {
            return null;
        }

    }


}
