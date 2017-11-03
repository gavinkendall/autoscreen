//////////////////////////////////////////////////////////
// Auto Screen Capture 2.0.6.2
// autoscreen.Program.cs
//
// Written by Gavin Kendall (gavinkendall@gmail.com)
// Thursday, 15 May 2008 - Friday, 3 November 2017

using System;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace autoscreen
{
    public static class Program
    {
        private enum ProcessDPIAwareness
        {
            ProcessDPIUnaware = 0,
            ProcessSystemDPIAware = 1,
            ProcessPerMonitorDPIAware = 2
        }

        [DllImport("shcore.dll")]
        private static extern int SetProcessDpiAwareness(ProcessDPIAwareness value);

        [STAThread]
        public static void Main(string[] args)
        {
            Log.Enabled = Properties.Settings.Default.DebugMode;

            for (int i = 0; i < args.Length; i++)
            {
                if (!string.IsNullOrEmpty(args[i]) && args[i].Equals("-debug"))
                {
                    Log.Enabled = true;
                    Properties.Settings.Default.DebugMode = true;

                    break;
                }
            }

            // Kill this application's duplicate process in case the user executes a second instance since we want to keep a single instance.
            if (Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location)).Length > 1)
            {
                Log.Write("A duplicate instance of the application was found running. Exiting.");
                Process.GetCurrentProcess().Kill();
            }
            else
            {
                // Make sure we're running on Windows 8 or higher.
                if (Environment.OSVersion.Version.Major >= 6 && Environment.OSVersion.Version.Minor >= 2)
                {
                    SetProcessDpiAwareness(ProcessDPIAwareness.ProcessPerMonitorDPIAware);
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new FormMain(args));
                }
                else
                {
                    Log.Write("The version of Windows is not supported (" + Environment.OSVersion.Version.Major + "." + Environment.OSVersion.Version.Minor + "). Windows 8 or higher is required.");
                }
            }
        }
    }
}
