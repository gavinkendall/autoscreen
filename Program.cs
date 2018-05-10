//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public static class Program
    {
        private enum DPIaware
        {
            ProcessDPIUnaware = 0,
            ProcessSystemDPIAware = 1,
            ProcessPerMonitorDPIAware = 2
        }

        [DllImport("shcore.dll")]
        private static extern int SetProcessDpiAwareness(DPIaware value);

        [STAThread]
        public static void Main(string[] args)
        {
            Log.Enabled = Properties.Settings.Default.DebugMode;

            foreach (string arg in args)
            {
                if (!string.IsNullOrEmpty(arg) && arg.Equals("-debug"))
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
                    SetProcessDpiAwareness(DPIaware.ProcessPerMonitorDPIAware);
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