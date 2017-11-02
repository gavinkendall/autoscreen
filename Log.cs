using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

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
                        sw.WriteLine("[" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "] " + message + " - Error: " + ex.Message);
                    }
                    else
                    {
                        sw.WriteLine("[" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "] " + message);
                    }

                    sw.Flush();
                    sw.Close();
                }
            }
        }
    }
}