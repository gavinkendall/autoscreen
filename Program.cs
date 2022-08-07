//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>The main entry for Auto Screen Capture.</summary>
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
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace AutoScreenCapture
{
    internal static class Program
    {
        private enum ProcessDPIAwareness
        {
            ProcessDPIUnaware = 0,
            ProcessSystemDPIAware = 1,
            ProcessPerMonitorDPIAware = 2
        }

        [DllImport("shcore.dll")]
        private static extern int SetProcessDpiAwareness(ProcessDPIAwareness value);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">A list of command line arguments that could be given to the application.</param>
        [STAThread]
        private static void Main(string[] args)
        {
            Config config = new Config();
            FileSystem fileSystem = new FileSystem();

            // Look for the -kill command. We want to find all the instances of Auto Screen Capture and kill them.
            if (args.Length == 1 && !string.IsNullOrEmpty(args[0]) && args[0].Equals("-kill"))
            {
                // Find all instances of autoscreen and kill them.
                foreach (var process in Process.GetProcessesByName("autoscreen"))
                {
                    process.Kill();
                }
            }
            else
            {
                // Parse any command line arguments before we start a new instance so we can issue commands externally
                // such as -debug, -log, -capture, -start, -stop, and -exit to the instance which is already running.
                if (args.Length > 0)
                {
                    if (!ParseCommandLineArguments(args, config))
                    {
                        WriteStartupError(config, fileSystem, "Cannot start application. Missing or invalid configuration file. Please use the latest version of autoscreen.conf from https://sourceforge.net/projects/autoscreen/files/");
                    }
                }
                else
                {
                    // Normally we could use the -config command to specify the configuration file to use, but if we
                    // have no commands to parse then we'll load the settings from the default configuration file.
                    if (!config.Load(fileSystem))
                    {
                        WriteStartupError(config, fileSystem, "Cannot start application. Missing or invalid configuration file. Please use the latest version of autoscreen.conf from https://sourceforge.net/projects/autoscreen/files/");
                    }
                }

                // This block of code figures out if we're already running an instance of the application.
                using (new Mutex(false, ((GuidAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(GuidAttribute), false)).Value, out bool createdNew))
                {
                    if (createdNew)
                    {
                        // If we're not already running then start a new instance of the application.
                        Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;

                        // Sets DPI awareness if we're running on a version of Windows that supports DPI scaling.
                        if ((Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 3) ||
                            Environment.OSVersion.Version.Major >= 10)
                        {
                            Application.EnableVisualStyles();
                            SetProcessDpiAwareness(ProcessDPIAwareness.ProcessPerMonitorDPIAware);
                        }

                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new FormMain(config));
                    }
                    else
                    {
                        if (args.Length == 0)
                        {
                            WriteStartupError(config, fileSystem, "Cannot start application. An existing instance of the application is already running.");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Parse the given command line arguments.
        /// </summary>
        /// <param name="args">The command line arguments to parse.</param>
        /// <param name="config">The config file to use.</param>
        /// <returns>True if parsing command line arguments is successful. False if parsing command line arguments unsuccessful.</returns>
        private static bool ParseCommandLineArguments(string[] args, Config config)
        {
            bool cleanStartup = false;
            FileSystem fileSystem = new FileSystem();

            // Make sure we parse for the most important command line options here.

            const string REGEX_COMMAND_LINE_CONFIG = "^-config=(?<ConfigFile>.+)$";
            const string REGEX_COMMAND_LINE_CLEAN_STARTUP = "^-cleanStartup$";

            foreach (string arg in args)
            {
                if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_CONFIG))
                {
                    string configFile = Regex.Match(arg, REGEX_COMMAND_LINE_CONFIG).Groups["ConfigFile"].Value;

                    if (configFile.Length > 0)
                    {
                        fileSystem.ConfigFile = configFile;
                        
                        if (!config.Load(fileSystem))
                        {
                            return false;
                        }
                    }
                }

                if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_CLEAN_STARTUP))
                {
                    cleanStartup = true;
                }
            }

            // We didn't get a -config or -cleanStartup command line argument so just load the default config
            // and let the application parse any other command line options that were given to it.
            if (config.Settings == null)
            {
                if (!config.Load(fileSystem, cleanStartup))
                {
                    return false;
                }
            }

            // All of these commands can be externally issued to an already running instance.
            // The current running instance monitors the command file for the commands in the file.
            foreach (string arg in args)
            {
                fileSystem.AppendToFile(fileSystem.CommandFile, arg);
            }

            return true;
        }

        private static void WriteStartupError(Config config, FileSystem fileSystem, string message)
        {
            if (config == null)
            {
                fileSystem.AppendToFile(fileSystem.StartupErrorFile, "Cannot start application. Missing configuration.");
            }
            else
            {
                string appVersion = "[(v" + config.Settings.ApplicationVersion + ") ";

                fileSystem.AppendToFile(fileSystem.StartupErrorFile, appVersion + DateTime.Now.ToString(config.MacroParser.DateFormat + " " + config.MacroParser.TimeFormat) + "] " + message);
            }

            Environment.Exit(1);
        }
    }
}