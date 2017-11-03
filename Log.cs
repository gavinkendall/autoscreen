//////////////////////////////////////////////////////////
// Auto Screen Capture 2.0.6.3
// autoscreen.Keylogger.cs
//
// Written by Gavin Kendall (gavinkendall@gmail.com)
// Thursday, 15 May 2008 - Friday, 3 November 2017

using System;
using System.IO;

namespace autoscreen
{
    public static class Log
    {
        public static bool Enabled = false;
        private static readonly string logfile = AppDomain.CurrentDomain.BaseDirectory + "\\autoscreen.log";

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