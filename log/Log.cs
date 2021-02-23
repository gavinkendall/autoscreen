//-----------------------------------------------------------------------
// <copyright file="Log.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>Writes messages to log files.</summary>
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program. If not, see <https://www.gnu.org/licenses/>.
//-----------------------------------------------------------------------
using System;
using System.Threading;

namespace AutoScreenCapture
{
    /// <summary>
    /// A class for logging messages to text files.
    /// </summary>
    public class Log
    {
        private Settings _settings;
        private FileSystem _fileSystem;
        private MacroParser _macroParser;

        private readonly string _extension = ".txt";
        private Mutex _mutexWriteFile = new Mutex();

        /// <summary>
        /// Debug Mode. This can be controlled from the command line with the -debug command.
        /// </summary>
        public bool DebugMode { get; set;}

        /// <summary>
        /// Logging. This can either be enabled or disabled by the application settings or via the -log command line argument.
        /// </summary>
        public bool LoggingEnabled { get; set; }

        /// <summary>
        /// A class for logging messages to text files.
        /// </summary>
        public Log(Settings settings, FileSystem fileSystem, MacroParser macroParser)
        {
            _settings = settings;
            _fileSystem = fileSystem;
            _macroParser = macroParser;

            DebugMode = Convert.ToBoolean(settings.Application.GetByKey("DebugMode", settings.DefaultSettings.DebugMode).Value);
            LoggingEnabled = Convert.ToBoolean(settings.Application.GetByKey("Logging", settings.DefaultSettings.Logging).Value);
        }

        /// <summary>
        /// Writes a message to the log files in the logs folder.
        /// </summary>
        /// <param name="message">The message to write.</param>
        public void WriteMessage(string message)
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
        public void WriteErrorMessage(string message)
        {
            Write(message, writeError: true, null);
        }

        /// <summary>
        /// Writes an exception error to the error file and log files.
        /// </summary>
        /// <param name="message">The message to write.</param>
        /// <param name="ex">The exception received from the .NET Framework.</param>
        public void WriteExceptionMessage(string message, Exception ex)
        {
            Write(message, writeError: false, ex);
        }

        /// <summary>
        /// Writes a debug message to the log files.
        /// </summary>
        /// <param name="message">The message to write.</param>
        public void WriteDebugMessage(string message)
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
        private void Write(string message, bool writeError, Exception ex)
        {
            try
            {
                _mutexWriteFile.WaitOne();

                string appVersion = "[(v" + _settings.ApplicationVersion + ") ";

                if (string.IsNullOrEmpty(_fileSystem.DebugFolder))
                {
                    _fileSystem.DebugFolder = AppDomain.CurrentDomain.BaseDirectory + @"!autoscreen" + _fileSystem.PathDelimiter + "debug" + _fileSystem.PathDelimiter;
                }

                if (string.IsNullOrEmpty(_fileSystem.LogsFolder))
                {
                    _fileSystem.LogsFolder = _fileSystem.DebugFolder + "logs" + _fileSystem.PathDelimiter;
                }

                if (!_fileSystem.DirectoryExists(_fileSystem.DebugFolder))
                {
                    _fileSystem.CreateDirectory(_fileSystem.DebugFolder);
                }

                if (!_fileSystem.DirectoryExists(_fileSystem.LogsFolder))
                {
                    _fileSystem.CreateDirectory(_fileSystem.LogsFolder);
                }

                // These are just general errors from the application so, if we have one, then write it out to the error file.
                if (writeError)
                {
                    _fileSystem.AppendToFile(_fileSystem.DebugFolder + _fileSystem.ErrorFile, appVersion + DateTime.Now.ToString(_macroParser.DateFormat + " " + _macroParser.TimeFormat) + "] ERROR: " + message);
                }

                // Log any exception errors we encounter.
                if (ex != null)
                {
                    string exceptionError = appVersion + DateTime.Now.ToString(_macroParser.DateFormat + " " + _macroParser.TimeFormat) + "] " + message + " - Exception Message: " + ex.Message + "\nInner Exception: " + (ex.InnerException != null ? ex.InnerException.Message : string.Empty) + "\nSource: " + ex.Source + "\nStack Trace: " + ex.StackTrace;

                    _fileSystem.AppendToFile(_fileSystem.DebugFolder + _fileSystem.ErrorFile, exceptionError);
                    _fileSystem.AppendToFile(_fileSystem.LogsFolder + _fileSystem.LogFile + _extension, exceptionError);

                    // If we encounter an exception error it's probably better to just error out on exit
                    // but we'll let the user decide if that's what they really want to do.
                    if (_settings.Application == null || Convert.ToBoolean(_settings.Application.GetByKey("ExitOnError", _settings.DefaultSettings.ExitOnError).Value))
                    {
                        Environment.Exit(1);
                    }
                }
                else
                {
                    // Write to the main log file.
                    _fileSystem.AppendToFile(_fileSystem.LogsFolder + _fileSystem.LogFile + _extension, appVersion + DateTime.Now.ToString(_macroParser.DateFormat + " " + _macroParser.TimeFormat) + "] " + message);

                    // Create a date-stamped directory if it does not already exist.
                    if (!_fileSystem.DirectoryExists(_fileSystem.LogsFolder + DateTime.Now.ToString(_macroParser.DateFormat)))
                    {
                        _fileSystem.CreateDirectory(_fileSystem.LogsFolder + DateTime.Now.ToString(_macroParser.DateFormat));
                    }

                    // Write to a log file within a directory representing the day when the message was logged.
                    _fileSystem.AppendToFile(_fileSystem.LogsFolder + DateTime.Now.ToString(_macroParser.DateFormat) + _fileSystem.PathDelimiter + _fileSystem.LogFile + "_" + DateTime.Now.ToString(_macroParser.DateFormat) + ".txt", appVersion + DateTime.Now.ToString(_macroParser.DateFormat + " " + _macroParser.TimeFormat) + "] " + message);
                }
            }
            finally
            {
                _mutexWriteFile.ReleaseMutex();
            }
        }
    }
}