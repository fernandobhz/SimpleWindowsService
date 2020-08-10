using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;

namespace SimpleWindowsService
{
    public partial class SimpleService : ServiceBase
    {
    
        public SimpleService()
        {
            InitializeComponent();

            // Initialize eventLogSimple 
            if (!System.Diagnostics.EventLog.SourceExists("SimpleSource")) 
                System.Diagnostics.EventLog.CreateEventSource("SimpleSource", "SimpleLog");
            eventLogSimple.Source = "SimpleSource";
            eventLogSimple.Log = "SimpleLog";

        }
        //http://msdn.microsoft.com/en-us/library/ms733069.aspx
        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            eventLogSimple.WriteEntry("Hello world from Simple Service!");
            w("Iniciei4");

            // Create timer object and set timer tick to an hour
            MyTimer = new System.Timers.Timer(10 * 1000);

            MyTimer.Elapsed += new System.Timers.ElapsedEventHandler
                                (this.ServiceTimer_Tick);
            MyTimer.Enabled = true;
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            eventLogSimple.WriteEntry("Simple Service stopped!");
            w("Parei4");
        }


        private void w(String s)
        {
            System.IO.StreamWriter f = new System.IO.StreamWriter(@"C:\log.txt", true);

            f.WriteLine(s);
            f.Close();

            f.Dispose();
        }

        private System.Timers.Timer MyTimer = null;

        private void ServiceTimer_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            String s = DateTime.Now.ToString();

            w(s);
            System.Windows.Forms.MessageBox.Show(s);
        }
    }
}
