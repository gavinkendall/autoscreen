//-----------------------------------------------------------------------
// <copyright file="Log.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
using System;
using System.IO;
using System.Threading;

namespace AutoScreenCapture
{
    /// <summary>
    /// 
    /// </summary>
    public static class Log
    {
        private static Mutex _mutexWriteFile = new Mutex();

        /// <summary>
        /// 
        /// </summary>
        public static bool DebugMode { get; set;}

        /// <summary>
        /// 
        /// </summary>
        public static bool LoggingEnabled { get; set; }

        private static readonly string extension = ".txt";
        private static readonly string logFile = "autoscreen-log";
        private static readonly string errorFile = "autoscreen-error";

        /// <summary>
        /// Writes a message to the log files in the logs folder.
        /// </summary>
        /// <param name="message">The message to write.</param>
        public static void WriteMessage(string message)
        {
            if (LoggingEnabled || DebugMode)
            {
                Write(message, writeError: false, null);
            }
        }

        /// <summary>
        /// Writes a message to the error file in the debug folder.
        /// </summary>
        /// <param name="message">The message to write.</param>
        public static void WriteErrorMessage(string message)
        {
            Write(message, writeError: true, null);
        }

        /// <summary>
        /// Writes an exception error to the error file and log files.
        /// </summary>
        /// <param name="message">The message to write.</param>
        /// <param name="ex">The exception received from the .NET Framework.</param>
        public static void WriteExceptionMessage(string message, Exception ex)
        {
            Write(message, writeError: false, ex);
        }

        /// <summary>
        /// Writes a debug message to the log files.
        /// </summary>
        /// <param name="message">The message to write.</param>
        public static void WriteDebugMessage(string message)
        {
            if (DebugMode)
            {
                Write(message, writeError: false, null);
            }
        }

        /// <summary>
        /// Writes a message (whether it be an error or just a general message) and the exception (if any).
        /// </summary>
        /// <param name="message">The message to write.</param>
        /// <param name="writeError">Determines if we write the message to the error file in the debug folder.</param>
        /// <param name="ex">The exception received from the .NET Framework.</param>
        private static void Write(string message, bool writeError, Exception ex)
        {
            try
            {
                _mutexWriteFile.WaitOne();

                string appVersion = "[(v" + Settings.Application.GetByKey("Version", defaultValue: Settings.ApplicationVersion).Value + ") ";

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

                // These are just general errors from the application so, if we have one, then write it out to the error file.
                if (writeError)
                {
                    using (StreamWriter sw = new StreamWriter(FileSystem.DebugFolder + errorFile + extension, true))
                    {
                        sw.WriteLine(appVersion + DateTime.Now.ToString(MacroParser.DateFormat + " " + MacroParser.TimeFormat) + "] ERROR: " + message);

                        sw.Flush();
                        sw.Close();
                    }
                }

                // Log any exception errors we encounter.
                if (ex != null)
                {
                    using (StreamWriter sw = new StreamWriter(FileSystem.DebugFolder + errorFile + extension, true))
                    {
                        sw.WriteLine(appVersion +
                                     DateTime.Now.ToString(MacroParser.DateFormat + " " + MacroParser.TimeFormat) + "] " + message + " - Exception Message: " +
                                     ex.Message + "\nInner Exception: " + (ex.InnerException != null ? ex.InnerException.Message : string.Empty) + "\nSource: " + ex.Source + "\nStack Trace: " + ex.StackTrace);

                        sw.Flush();
                        sw.Close();
                    }

                    using (StreamWriter sw = new StreamWriter(FileSystem.LogsFolder + logFile + extension, true))
                    {
                        sw.WriteLine(appVersion +
                                     DateTime.Now.ToString(MacroParser.DateFormat + " " + MacroParser.TimeFormat) + "] " + message + " - Exception Message: " +
                                     ex.Message + "\nInner Exception: " + (ex.InnerException != null ? ex.InnerException.Message : string.Empty) + "\nSource: " + ex.Source + "\nStack Trace: " + ex.StackTrace);

                        sw.Flush();
                        sw.Close();
                    }

                    // If we encounter an exception error it's probably better to just error out on exit
                    // but we'll let the user decide if that's what they really want to do.
                    if (Convert.ToBoolean(Settings.Application.GetByKey("ExitOnError", false).Value))
                    {
                        Environment.Exit(1);
                    }
                }
                else
                {
                    // Write to the main log file.
                    using (StreamWriter sw = new StreamWriter(FileSystem.LogsFolder + logFile + extension, true))
                    {
                        sw.WriteLine(appVersion + DateTime.Now.ToString(MacroParser.DateFormat + " " + MacroParser.TimeFormat) + "] " + message);

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
                        sw.WriteLine(appVersion + DateTime.Now.ToString(MacroParser.DateFormat + " " + MacroParser.TimeFormat) + "] " + message);

                        sw.Flush();
                        sw.Close();
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