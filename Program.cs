//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
using System;
using System.IO;
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
        [STAThread]
        private static void Main(string[] args)
        {
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
                    ParseCommandLineArguments(args);
                }
                else
                {
                    // Normally we could use the -config command to specify the configuration file to use, but if we
                    // have no commands to parse then we'll load the settings from the default configuration file.
                    Config.Load();

                    Settings.Initialize();
                }

                // This block of code figures out if we're already running an instance of the application.
                using (new Mutex(false, ((GuidAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(GuidAttribute), false)).Value, out bool createdNew))
                {
                    if (createdNew)
                    {
                        // If we're not already running then start a new instance of the application.
                        Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;

                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new FormMain(args));
                    }
                    else
                    {
                        if (args.Length == 0)
                        {
                            // We've determined that an instance is already running so let's assume the user just opened
                            // the application without specifying any command line arguments and, if so, we should inform
                            // them about the -kill command which can be used to terminate all running instances.
                            string appVersion = "[(v" + Settings.ApplicationVersion + ") ";

                            using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + FileSystem.StartupErrorFile, true))
                            {
                                sw.WriteLine(appVersion + DateTime.Now.ToString(MacroParser.DateFormat + " " + MacroParser.TimeFormat) + "] An existing instance of autoscreen is already running");

                                sw.Flush();
                                sw.Close();
                            }
                        }
                    }
                }
            }
        }

        private static void CreateFile(string filename)
        {
            File.Create(FileSystem.CommandFolder + filename).Dispose();
        }

        private static void ParseCommandLineArguments(string[] args)
        {
            // Because ordering is important I want to make sure that we pick up the configuration file first.
            // This will avoid scenarios like "autoscreen.exe -debug -config" creating all the default folders
            // and files (thanks to -debug being the first argument) before -config is parsed.
            //
            // This is also why we're parsing the command line arguments in Program before we initialize FormMain.

            foreach (string arg in args)
            {
                if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_CONFIG))
                {
                    string configFile = Regex.Match(arg, CommandLineRegex.REGEX_COMMAND_LINE_CONFIG).Groups["ConfigFile"].Value;

                    if (configFile.Length > 0)
                    {
                        FileSystem.ConfigFile = configFile;

                        Config.Load();

                        Settings.Initialize();
                    }
                }
            }

            // We didn't get a -config command line argument so just load the default config.
            if (Settings.Application == null)
            {
                Config.Load();

                Settings.Initialize();
            }

            // Load user settings.
            if (string.IsNullOrEmpty(FileSystem.UserSettingsFile))
            {
                Config.Load();

                Settings.Initialize();
            }

            // All of these commands can be externally issued to an already running instance.
            // The current running instance monitors the Command folder for special empty files
            // that are named after the command being issued so we can perform a certain action
            // based on the command being given.
            foreach (string arg in args)
            {
                if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_DEBUG))
                {
                    CreateFile("debug");
                }

                if(Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_DEBUG_ON))
                {
                    CreateFile("debug_on");
                }

                if(Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_DEBUG_OFF))
                {
                    CreateFile("debug_off");
                }

                if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_LOG))
                {
                    CreateFile("log");
                }

                if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_LOG_ON))
                {
                    CreateFile("log_on");
                }

                if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_LOG_OFF))
                {
                    CreateFile("log_off");
                }

                if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_CAPTURE))
                {
                    CreateFile("capture");
                }

                if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_START))
                {
                    CreateFile("start");
                }

                if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_STOP))
                {
                    CreateFile("stop");
                }

                if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_EXIT))
                {
                    CreateFile("exit");
                }
            }
        }
    }
}