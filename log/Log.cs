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

    /// <summary>
    /// 
    /// </summary>
    public static class Log
    {
        private static Mutex _mutexWriteFile = new Mutex();

        /// <summary>
        /// 
        /// </summary>
        public static bool Enabled { get; set; }

        private static readonly string extension = ".txt";
        private static readonly string logFile = "autoscreen-log";
        private static readonly string errorFile = "autoscreen-error";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public static void Write(string message)
        {
            Write(message, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public static void Write(string message, Exception ex)
        {
            try
            {
                _mutexWriteFile.WaitOne();

                if (ex != null)
                {
                    Enabled = true;
                }

                if (Enabled)
                {
                    if (string.IsNullOrEmpty(FileSystem.DebugFolder))
                    {
                        FileSystem.DebugFolder = AppDomain.CurrentDomain.BaseDirectory + @"!autoscreen" + FileSystem.PathDelimiter + "debug" + FileSystem.PathDelimiter;
                    }

                    if (string.IsNullOrEmpty(FileSystem.LogsFolder))
                    {
                        FileSystem.LogsFolder = FileSystem.DebugFolder + "logs" + FileSystem.PathDelimiter;
                    }

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

                        // If we encounter an exception error it's probably better to just error out on exit.
                        Environment.Exit(1);
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