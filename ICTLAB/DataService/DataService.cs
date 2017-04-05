using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace DataService
{
    public partial class DataService : ServiceBase
    {
        public DataService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Console.WriteLine("starting...");
            
            // Set up a timer to trigger every minute.  
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Enabled = true;

            timer.Interval = 60000; // 60 seconds  
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.Start();
        }

        private void OnTimer(object sender, ElapsedEventArgs e)
        {
            // TODO: Insert monitoring activities here.  
        }

        protected override void OnStop()
        {
            Console.WriteLine("Stopping");
        }
    }
}
