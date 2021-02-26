//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2021 Gavin Kendall
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

        [STAThread]
        private static void Main(string[] args)
        {
            Config config = new Config();
            FileSystem fileSystem = new FileSystem();

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
                    ParseCommandLineArguments(args, config);
                }
                else
                {
                    // Normally we could use the -config command to specify the configuration file to use, but if we
                    // have no commands to parse then we'll load the settings from the default configuration file.
                    config.Load(fileSystem);
                }

                // This block of code figures out if we're already running an instance of the application.
                using (new Mutex(false, ((GuidAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(GuidAttribute), false)).Value, out bool createdNew))
                {
                    if (createdNew)
                    {
                        // If we're not already running then start a new instance of the application.
                        Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;

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
                        if (args.Length == 0 && Convert.ToBoolean(config.Settings.Application.GetByKey("ShowStartupError", config.Settings.DefaultSettings.ShowStartupError).Value))
                        {
                            // We've determined that an existing instance is already running. We should write out an error message informing the user.
                            string appVersion = "[(v" + config.Settings.ApplicationVersion + ") ";

                            fileSystem.AppendToFile(fileSystem.StartupErrorFile, appVersion + DateTime.Now.ToString(config.MacroParser.DateFormat + " " + config.MacroParser.TimeFormat) + "] Cannot start " + config.Settings.ApplicationName + " because an existing instance of the application is already running. To disable this error message set \"ShowStartupError\" to \"False\" in \"" + config.FileSystem.ApplicationSettingsFile + "\"");
                        }
                    }
                }
            }
        }

        private static void ParseCommandLineArguments(string[] args, Config config)
        {
            FileSystem fileSystem = new FileSystem();

            // Because ordering is important I want to make sure that we pick up the configuration file first.
            // This will avoid scenarios like "autoscreen.exe -debug -config" creating all the default folders
            // and files (thanks to -debug being the first argument) before -config is parsed.

            const string REGEX_COMMAND_LINE_CONFIG = "^-config=(?<ConfigFile>.+)$";

            foreach (string arg in args)
            {
                if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_CONFIG))
                {
                    string configFile = Regex.Match(arg, REGEX_COMMAND_LINE_CONFIG).Groups["ConfigFile"].Value;

                    if (configFile.Length > 0)
                    {
                        fileSystem.ConfigFile = configFile;
                        config.Load(fileSystem);
                    }
                }
            }

            // We didn't get a -config command line argument so just load the default config
            // and let the application parse any other command line options.
            if (config.Settings == null)
            {
                config.Load(fileSystem);
            }

            // All of these commands can be externally issued to an already running instance.
            // The current running instance monitors the command file for the commands in the file.
            foreach (string arg in args)
            {
                fileSystem.AppendToFile(fileSystem.CommandFile, arg);
            }
        }
    }
}