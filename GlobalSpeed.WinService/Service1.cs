using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using GlobalSpeed.Server.Lib;

namespace GlobalSpeed.WinService
{
    public partial class Service1 : ServiceBase, ICommunication
    {
        GlobalSpeedService myService;
        public Service1()
        {
            myService = new GlobalSpeedService();
            InitializeComponent();
            myService.Communication = this;

            if (!System.Diagnostics.EventLog.SourceExists("GlobalSpeed"))
            {
                System.Diagnostics.EventLog.CreateEventSource("GlobalSpeed", "GSLog");
            }
            eventLog1.Source = "GlobalSpeed";
            eventLog1.Log = "GSLog";
        }

        protected override void OnStart(string[] args)
        {
            myService.Start(args);
        }

        protected override void OnStop()
        {
            myService.Stop();
        }

        public void WriteLine(string input)
        {
            eventLog1.WriteEntry(input);
        }

        public string ReadLine() { return String.Empty; }
    }
}
