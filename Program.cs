//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
using System;
using System.IO;
using System.Diagnostics;
using System.Reflection;
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
            // Introduce -capture, -start, and -stop commands here so we can drop "capture", "start", and "stop" files into a command folder
            // for the running instance of Auto Screen Capture to monitor and determine what command to execute.
            foreach (string arg in args)
            {
                if (arg.Equals("-capture"))
                {
                    // Create an empty file named "capture" in the command folder.
                }
                else if (arg.Equals("-start"))
                {
                    // Create an empty file named "start" in the command folder.
                }
                else if (arg.Equals("stop"))
                {
                    // Create an empty file name "stop" in the command folder.
                }
            }

            using (new Mutex(false, ((GuidAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(GuidAttribute), false)).Value, out bool createdNew))
            {
                if (createdNew)
                {
                    Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;

                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new FormMain(args));
                }
                else
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
                        string appVersion = "[(v" + Settings.ApplicationVersion + ") ";

                        using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + FileSystem.StartupErrorFile, true))
                        {
                            sw.WriteLine(appVersion + DateTime.Now.ToString(MacroParser.DateFormat + " " + MacroParser.TimeFormat) + "] An existing instance of autoscreen is already running so this instance is being terminated. If you want to run this instance and terminate all other running instances run autoscreen again with the -kill command, wait for all running instances to terminate, and then run autoscreen normally");

                            sw.Flush();
                            sw.Close();
                        }
                    }
                }
            }
        }
    }
}