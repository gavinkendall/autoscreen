//////////////////////////////////////////////////////////
// Auto Screen Capture 2.1.3
// autoscreen.Log.cs
//
// Developed by Gavin Kendall
// Thursday, 15 May 2008 - Tuesday, 24 April 2018

using System;
using System.IO;

namespace autoscreen
{
    public static class Log
    {
        public static bool Enabled { get; set; }

        private static readonly string logfile = AppDomain.CurrentDomain.BaseDirectory + "autoscreen.log";

        public static void Write(string message)
        {
            Write(message, null);
        }

        public static void Write(string message, Exception ex)
        {
            if (Enabled)
            {
                using (StreamWriter sw = new StreamWriter(logfile, true))
                {
                    if (ex != null)
                    {
                        sw.WriteLine("[(v" + Properties.Settings.Default.ApplicationVersion + ") " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "] " + message + " - Error: " + ex.Message);
                    }
                    else
                    {
                        sw.WriteLine("[(v" + Properties.Settings.Default.ApplicationVersion + ") " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "] " + message);
                    }

                    sw.Flush();
                    sw.Close();
                }
            }
        }
    }
}