//////////////////////////////////////////////////////////
// Auto Screen Capture 2.0.6
// autoscreen.Program.cs
//
// Written by Gavin Kendall (gavinkendall@gmail.com)
// Thursday, 15 May 2008 - Thursday, 2 November 2017

using System;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;

namespace autoscreen
{
    static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (!string.IsNullOrEmpty(args[i]) && args[i].Equals("-debug"))
                {
                    Log.Enabled = true;

                    Log.Write("*** WELCOME TO AUTO SCREEN CAPTURE " + Properties.Settings.Default.ApplicationVersion + " ***");

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
                // Make sure we're running on Windows Vista or higher.
                if (Environment.OSVersion.Version.Major >= 6)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new FormMain(args));
                }
                else
                {
                    Log.Write("The version of Windows is not supported (" + Environment.OSVersion.Version.Major + "." + Environment.OSVersion.Version.Minor + "). Windows Vista or higher is required.");
                }
            }
        }
    }
}
