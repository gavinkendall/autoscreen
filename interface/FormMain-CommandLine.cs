using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AutoScreenCapture
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// Parses the command line and processes the commands the user has chosen from the command line.
        /// </summary>
        /// <param name="args"></param>
        private void ParseCommandLineArguments(string[] args)
        {
            try
            {
                // Clear the contents of the command file before parsing the command line arguments.
                // We have the commands in "args" already and, to prevent commands like -exit screwing up
                // the application's ability to read commands on the next run, we need to make sure that the
                // command file is empty at this point in time. We don't want to be taking screenshots every
                // second just because the -capture command was left remaining in the command file!
                //
                // Also, this ensures that a sensitive command such as the -passphrase=x command is
                // immediately removed from the file.
                File.WriteAllText(FileSystem.CommandFile, String.Empty);

                ScreenCapture.AutoStartFromCommandLine = Convert.ToBoolean(Settings.Application.GetByKey("AutoStartFromCommandLine", defaultValue: false).Value);

                foreach (string arg in args)
                {
                    // -debug
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_DEBUG))
                    {
                        Log.DebugMode = !Log.DebugMode;

                        Settings.Application.GetByKey("DebugMode", defaultValue: false).Value = Log.DebugMode;
                        Settings.Application.Save();
                    }

                    // -debug=on
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_DEBUG_ON))
                    {
                        Log.DebugMode = true;

                        Settings.Application.GetByKey("DebugMode", defaultValue: false).Value = true;
                        Settings.Application.Save();
                    }

                    // -debug=off
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_DEBUG_OFF))
                    {
                        Log.DebugMode = false;

                        Settings.Application.GetByKey("DebugMode", defaultValue: false).Value = false;
                        Settings.Application.Save();
                    }

                    // -log
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_LOG))
                    {
                        Log.LoggingEnabled = !Log.LoggingEnabled;

                        Settings.Application.GetByKey("Logging", defaultValue: false).Value = Log.LoggingEnabled;
                        Settings.Application.Save();
                    }

                    // -log=on
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_LOG_ON))
                    {
                        Log.LoggingEnabled = true;

                        Settings.Application.GetByKey("Logging", defaultValue: false).Value = true;
                        Settings.Application.Save();
                    }

                    // -log=off
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_LOG_OFF))
                    {
                        Log.LoggingEnabled = false;

                        Settings.Application.GetByKey("Logging", defaultValue: false).Value = false;
                        Settings.Application.Save();
                    }

                    // -capture
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_CAPTURE))
                    {
                        TakeScreenshot(captureNow: true);
                    }

                    // -start
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_START))
                    {
                        StartScreenCapture();
                        return;
                    }

                    // -stop
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_STOP))
                    {
                        StopScreenCapture();
                        return;
                    }

                    // -exit
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_EXIT))
                    {
                        ExitApplication();
                    }

                    // -showSystemTrayIcon
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_SHOW_SYSTEM_TRAY_ICON))
                    {
                        Settings.User.GetByKey("BoolShowSystemTrayIcon", defaultValue: true).Value = true;
                        Settings.User.Save();

                        notifyIcon.Visible = true;
                    }

                    // -hideSystemTrayIcon
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_HIDE_SYSTEM_TRAY_ICON))
                    {
                        Settings.User.GetByKey("BoolShowSystemTrayIcon", defaultValue: true).Value = false;
                        Settings.User.Save();

                        notifyIcon.Visible = false;
                    }

                    // -initial
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_INITIAL))
                    {
                        checkBoxInitialScreenshot.Checked = !checkBoxInitialScreenshot.Checked;

                        Settings.User.GetByKey("BoolTakeInitialScreenshot", defaultValue: false).Value = checkBoxInitialScreenshot.Checked;
                        Settings.User.Save();
                    }

                    // -initial=on
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_INITIAL_ON))
                    {
                        checkBoxInitialScreenshot.Checked = true;

                        Settings.User.GetByKey("BoolTakeInitialScreenshot", defaultValue: false).Value = true;
                        Settings.User.Save();
                    }

                    // -initial=off
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_INITIAL_OFF))
                    {
                        checkBoxInitialScreenshot.Checked = false;

                        Settings.User.GetByKey("BoolTakeInitialScreenshot", defaultValue: false).Value = false;
                        Settings.User.Save();
                    }

                    // -limit=x
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_LIMIT))
                    {
                        int cmdLimit = Convert.ToInt32(Regex.Match(arg, CommandLineRegex.REGEX_COMMAND_LINE_LIMIT).Groups["Limit"].Value);

                        if (cmdLimit >= CAPTURE_LIMIT_MIN && cmdLimit <= CAPTURE_LIMIT_MAX)
                        {
                            numericUpDownCaptureLimit.Value = cmdLimit;
                            checkBoxCaptureLimit.Checked = true;

                            _screenCapture.Limit = checkBoxCaptureLimit.Checked ? (int)numericUpDownCaptureLimit.Value : 0;
                        }
                    }

                    // -interval=hh:mm:ss.nnn
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_INTERVAL))
                    {
                        int hours = Convert.ToInt32(Regex.Match(arg, CommandLineRegex.REGEX_COMMAND_LINE_INTERVAL).Groups["Hours"].Value);
                        int minutes = Convert.ToInt32(Regex.Match(arg, CommandLineRegex.REGEX_COMMAND_LINE_INTERVAL).Groups["Minutes"].Value);
                        int seconds = Convert.ToInt32(Regex.Match(arg, CommandLineRegex.REGEX_COMMAND_LINE_INTERVAL).Groups["Seconds"].Value);
                        int milliseconds = Convert.ToInt32(Regex.Match(arg, CommandLineRegex.REGEX_COMMAND_LINE_INTERVAL).Groups["Milliseconds"].Value);

                        numericUpDownHoursInterval.Value = hours;
                        numericUpDownMinutesInterval.Value = minutes;
                        numericUpDownSecondsInterval.Value = seconds;
                        numericUpDownMillisecondsInterval.Value = milliseconds;

                        int screenCaptureInterval = GetScreenCaptureInterval();

                        if (screenCaptureInterval > 0)
                        {
                            timerScreenCapture.Stop();
                            timerScreenCapture.Enabled = false;

                            _screenCapture.DateTimePreviousCycle = DateTime.Now;
                            _screenCapture.Delay = screenCaptureInterval;
                            timerScreenCapture.Interval = screenCaptureInterval;

                            Settings.User.GetByKey("IntScreenCaptureInterval", defaultValue: 60000).Value = screenCaptureInterval;
                            Settings.User.Save();

                            timerScreenCapture.Enabled = true;
                            timerScreenCapture.Start();
                        }
                    }

                    //if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_STARTAT))
                    //{
                    //    int hours = Convert.ToInt32(Regex.Match(arg, CommandLineRegex.REGEX_COMMAND_LINE_STARTAT).Groups["Hours"].Value);
                    //    int minutes = Convert.ToInt32(Regex.Match(arg, CommandLineRegex.REGEX_COMMAND_LINE_STARTAT).Groups["Minutes"].Value);
                    //    int seconds = Convert.ToInt32(Regex.Match(arg, CommandLineRegex.REGEX_COMMAND_LINE_STARTAT).Groups["Seconds"].Value);

                    //    //formSchedule.ScheduleCollection.
                    //    formSchedule.ScheduleCollection.GetByName("Command Line Schedule").ModeOneTime = false;
                    //    formSchedule.ScheduleCollection.GetByName("Command Line Schedule").ModePeriod = true;
                    //    formSchedule.ScheduleCollection.GetByName("Command Line Schedule").StartAt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hours, minutes, seconds);
                    //}

                    //if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_STOPAT))
                    //{
                    //    int hours = Convert.ToInt32(Regex.Match(arg, CommandLineRegex.REGEX_COMMAND_LINE_STOPAT).Groups["Hours"].Value);
                    //    int minutes = Convert.ToInt32(Regex.Match(arg, CommandLineRegex.REGEX_COMMAND_LINE_STOPAT).Groups["Minutes"].Value);
                    //    int seconds = Convert.ToInt32(Regex.Match(arg, CommandLineRegex.REGEX_COMMAND_LINE_STOPAT).Groups["Seconds"].Value);
                    //}

                    // -passphrase=x
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_PASSPHRASE))
                    {
                        string passphrase = Regex.Match(arg, CommandLineRegex.REGEX_COMMAND_LINE_PASSPHRASE).Groups["Passphrase"].Value;

                        if (passphrase.Length > 0)
                        {
                            if (string.IsNullOrEmpty(FileSystem.UserSettingsFile))
                            {
                                Config.Load();

                                Settings.Initialize();
                            }

                            Settings.User.GetByKey("StringPassphrase", defaultValue: string.Empty).Value = Security.Hash(passphrase);
                            Settings.User.Save();

                            ScreenCapture.LockScreenCaptureSession = true;
                        }
                    }
                }

                if (ScreenCapture.AutoStartFromCommandLine)
                {
                    StartScreenCapture();
                }
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("FormMain::ParseCommandLineArguments", ex);
            }
        }
    }
}
