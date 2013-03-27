using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;


namespace SMDBMonitor
{
    public partial class Service1 : ServiceBase
    {
        private Monitor monitor;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            this.monitor = new Monitor();
            this.monitor.Start();
        }

        protected override void OnStop()
        {
            this.monitor.Stop();
        }
    }
}
