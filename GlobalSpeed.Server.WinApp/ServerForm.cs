using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GlobalSpeed.Server.Lib;

namespace GlobalSpeed.Server.WinApp
{
    public partial class ServerForm : Form, ICommunication
    {
        GlobalSpeedService myService;
        public ServerForm()
        {
            myService = new GlobalSpeedService();
            InitializeComponent();
            btnStop.Enabled = false;
        }
                

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            myService.Communication = this;
            tbOutput.Clear();
            myService.Start();
            this.Size = new Size(360, 400);
            tbOutput.Visible = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            myService.Stop();
            this.Size = new Size(360,170);
            tbOutput.Visible = false;
        }

        public void WriteLine(string input)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(WriteLine), new object[] { input });
                return;
            }
            tbOutput.AppendText(input);
            tbOutput.AppendText("\r\n");
        }

        public string ReadLine() { return String.Empty; }

        private void ServerForm_Load(object sender, EventArgs e)
        {

        }

        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnStop_Click(this, null);
            Environment.Exit(0);
        }
    }
}
