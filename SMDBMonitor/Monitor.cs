using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Management;
using System.Diagnostics;
using System.IO;

namespace SMDBMonitor
{
    public class Monitor
    {
        private Timer _stateTimer;
        private TimerCallback _timerDelegate;

        public void Start()
        {
            _timerDelegate = new TimerCallback(CheckSMDB);
            _stateTimer = new Timer(_timerDelegate, null, 500, 500);
        }

        private void CheckSMDB(object stateObj)
        {
            string query = "Select ProcessId From Win32_Process Where CommandLine Like '%SmartDashboard.jar%'";

            try
            {
                List<Process> smdb;
                using (var results = new ManagementObjectSearcher(query).Get())
                {
                    smdb = results.Cast<ManagementObject>().Select(mo => Process.GetProcessById((int)(uint)mo["ProcessId"])).ToList();
                }

                if (smdb == null || smdb.Count == 0)
                {
                    if (!Args.FindDriverStation || (Args.FindDriverStation && Process.GetProcessesByName("Driver Station").Length > 0))
                    {
                        using (Process process = new Process())
                        {
                            process.StartInfo = new ProcessStartInfo(AppDomain.CurrentDomain.BaseDirectory + @"\StartSmartDashboard.bat");
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            process.Start();
                            Console.WriteLine("(" + DateTime.Now + ") - SmartDashboard not found. Launching SmartDashboard...");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("(" + DateTime.Now + ") - Exception Occurred (CheckSMDB): MSG: " + ex.Message);
            }
        }

        public void Stop()
        {
            _stateTimer.Dispose();
        }
    }
}
