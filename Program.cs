//////////////////////////////////////////////////////////
// Auto Screen Capture 2.0.5
// autoscreen.Program.cs
//
// Written by Gavin Kendall (gavinkendall@gmail.com)
// Thursday, 15 May 2008 - Wednesday, 27 November 2013

using System;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;

namespace autoscreen
{
    static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            // Kill this application's duplicate process in case the user executes a second instance since we want to keep a single instance.
            if (Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location)).Length > 1)
            {
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
            }
        }
    }
}
