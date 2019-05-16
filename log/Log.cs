//-----------------------------------------------------------------------
// <copyright file="Log.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    using System;
    using System.IO;
    using System.Threading;

    public static class Log
    {
        // Required when multiple threads are writing to the same log file.
        private static Mutex _mutexWriteFile = new Mutex();

        public static bool Enabled { get; set; }

        private static readonly string extension = ".txt";
        private static readonly string logFile = "autoscreen-log";
        private static readonly string errorFile = "autoscreen-error";

        public static void Write(string message)
        {
            Write(message, null);
        }

        public static void Write(string message, Exception ex)
        {
            try
            {
                _mutexWriteFile.WaitOne();

                if (Enabled)
                {
                    if (!Directory.Exists(FileSystem.DebugFolder))
                    {
                        Directory.CreateDirectory(FileSystem.DebugFolder);
                    }

                    if (!Directory.Exists(FileSystem.LogsFolder))
                    {
                        Directory.CreateDirectory(FileSystem.LogsFolder);
                    }

                    if (ex != null)
                    {
                        using (StreamWriter sw = new StreamWriter(FileSystem.DebugFolder + errorFile + extension, true))
                        {
                            sw.WriteLine("[(v" + Settings.Application.GetByKey("Version", defaultValue: Settings.ApplicationVersion).Value + ") " +
                                         DateTime.Now.ToString(MacroParser.DateFormat + " " + MacroParser.TimeFormat) + "] " + message + " - Error Message: " +
                                         ex.Message + "\nInner Exception: " + (ex.InnerException != null ? ex.InnerException.Message : string.Empty) + "\nSource: " + ex.Source + "\nStack Trace: " + ex.StackTrace);

                            sw.Flush();
                            sw.Close();
                        }

                        using (StreamWriter sw = new StreamWriter(FileSystem.LogsFolder + logFile + extension, true))
                        {
                            sw.WriteLine("[(v" + Settings.Application.GetByKey("Version", defaultValue: Settings.ApplicationVersion).Value + ") " +
                                         DateTime.Now.ToString(MacroParser.DateFormat + " " + MacroParser.TimeFormat) + "] " + message + " - Error Message: " +
                                         ex.Message + "\nInner Exception: " + (ex.InnerException != null ? ex.InnerException.Message : string.Empty) + "\nSource: " + ex.Source + "\nStack Trace: " + ex.StackTrace);

                            sw.Flush();
                            sw.Close();
                        }
                    }
                    else
                    {
                        // Write to the main log file.
                        using (StreamWriter sw = new StreamWriter(FileSystem.LogsFolder + logFile + extension, true))
                        {
                            sw.WriteLine("[(v" + Settings.Application.GetByKey("Version", defaultValue: Settings.ApplicationVersion).Value + ") " +
                                         DateTime.Now.ToString(MacroParser.DateFormat + " " + MacroParser.TimeFormat) + "] " + message);

                            sw.Flush();
                            sw.Close();
                        }

                        // Create a date-stamped directory if it does not already exist.
                        if (!Directory.Exists(FileSystem.LogsFolder + DateTime.Now.ToString(MacroParser.DateFormat)))
                        {
                            Directory.CreateDirectory(FileSystem.LogsFolder + DateTime.Now.ToString(MacroParser.DateFormat));
                        }

                        // Write to a log file within a directory representing the day when the message was logged.
                        using (StreamWriter sw = new StreamWriter(
                            FileSystem.LogsFolder + DateTime.Now.ToString(MacroParser.DateFormat) + FileSystem.PathDelimiter +
                            logFile + "_" + DateTime.Now.ToString(MacroParser.DateFormat) + extension, true))
                        {
                            sw.WriteLine("[(v" + Settings.Application.GetByKey("Version", defaultValue: Settings.ApplicationVersion).Value + ") " +
                                         DateTime.Now.ToString(MacroParser.DateFormat + " " + MacroParser.TimeFormat) + "] " + message);

                            sw.Flush();
                            sw.Close();
                        }
                    }
                }
            }
            finally
            {
                _mutexWriteFile.ReleaseMutex();
            }
        }
    }
}