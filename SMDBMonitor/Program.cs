using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace SMDBMonitor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            /*if (args.Contains("-console"))
            {*/
            System.Console.Title = "SmartDashboard Monitor";

            if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\StartSmartDashboard.bat"))
            {
                System.Console.WriteLine("SmartDashboard Monitor - press 'q' to quit.");
                Args.FindDriverStation = args.Contains("-dsoff") ? false : true;

                var app = new Monitor();
                app.Start();

                while (Console.ReadKey().KeyChar != 'q')
                {

                }
                app.Stop();
            }
            else
            {
                System.Console.WriteLine("StartSmartDashboard.bat not found in directory. Program will now exit.");
                System.Threading.Thread.Sleep(2000);
            }

            //Changed to console app
            //Research shows desktop not really supported from Windows Service in Vista and up; therefore, no calls to apps =(
           /* }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
		        { 
			        new Service1() 
		        };
                ServiceBase.Run(ServicesToRun);
            }*/
        }
    }
}
