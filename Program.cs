//////////////////////////////////////////////////////////
// Auto Screen Capture 2.1.0
// autoscreen.Program.cs
//
// Developed by Gavin Kendall
// Thursday, 15 May 2008 - Monday, 12 March 2018

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

            SetProcessDpiAwareness(ProcessDPIAwareness.ProcessPerMonitorDPIAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain(args));
        }
    }
}
