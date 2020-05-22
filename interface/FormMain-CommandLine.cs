//-----------------------------------------------------------------------
// <copyright file="FormMain-CommandLine.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>All the methods for handling command line arguments.</summary>
//-----------------------------------------------------------------------
using System;
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
                FileSystem.WriteToFile(FileSystem.CommandFile, string.Empty);

                ScreenCapture.AutoStartFromCommandLine = Convert.ToBoolean(Settings.Application.GetByKey("AutoStartFromCommandLine", DefaultSettings.AutoStartFromCommandLine).Value);

                // Create the "Special Schedule" if it doesn't already exist.
                if (_formSchedule.ScheduleCollection.GetByName(ScheduleCollection.SpecialScheduleName) == null)
                {
                    DateTime dtNow = DateTime.Now;

                    Schedule specialSchedule = new Schedule()
                    {
                        Name = ScheduleCollection.SpecialScheduleName,
                        Active = false,
                        ModeOneTime = true,
                        ModePeriod = false,
                        CaptureAt = dtNow,
                        StartAt = dtNow,
                        StopAt = dtNow
                    };

                    _formSchedule.ScheduleCollection.Add(specialSchedule);

                    BuildSchedulesModule();

                    _formSchedule.ScheduleCollection.SaveToXmlFile();
                }

                foreach (string arg in args)
                {
                    // -debug
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_DEBUG))
                    {
                        Log.DebugMode = !Log.DebugMode;

                        Settings.Application.GetByKey("DebugMode", DefaultSettings.DebugMode).Value = Log.DebugMode;
                        Settings.Application.Save();
                    }

                    // -debug=on
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_DEBUG_ON))
                    {
                        Log.DebugMode = true;

                        Settings.Application.GetByKey("DebugMode", DefaultSettings.DebugMode).Value = true;
                        Settings.Application.Save();
                    }

                    // -debug=off
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_DEBUG_OFF))
                    {
                        Log.DebugMode = false;

                        Settings.Application.GetByKey("DebugMode", DefaultSettings.DebugMode).Value = false;
                        Settings.Application.Save();
                    }

                    // -log
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_LOG))
                    {
                        Log.LoggingEnabled = !Log.LoggingEnabled;

                        Settings.Application.GetByKey("Logging", DefaultSettings.Logging).Value = Log.LoggingEnabled;
                        Settings.Application.Save();
                    }

                    // -log=on
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_LOG_ON))
                    {
                        Log.LoggingEnabled = true;

                        Settings.Application.GetByKey("Logging", DefaultSettings.Logging).Value = true;
                        Settings.Application.Save();
                    }

                    // -log=off
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_LOG_OFF))
                    {
                        Log.LoggingEnabled = false;

                        Settings.Application.GetByKey("Logging", DefaultSettings.Logging).Value = false;
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
                        Settings.User.GetByKey("BoolShowSystemTrayIcon", DefaultSettings.BoolShowSystemTrayIcon).Value = true;
                        Settings.User.Save();

                        notifyIcon.Visible = true;
                    }

                    // -hideSystemTrayIcon
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_HIDE_SYSTEM_TRAY_ICON))
                    {
                        Settings.User.GetByKey("BoolShowSystemTrayIcon", DefaultSettings.BoolShowSystemTrayIcon).Value = false;
                        Settings.User.Save();

                        notifyIcon.Visible = false;
                    }

                    // -initial
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_INITIAL))
                    {
                        checkBoxInitialScreenshot.Checked = !checkBoxInitialScreenshot.Checked;

                        Settings.User.GetByKey("BoolTakeInitialScreenshot", DefaultSettings.BoolTakeInitialScreenshot).Value = checkBoxInitialScreenshot.Checked;
                        Settings.User.Save();
                    }

                    // -initial=on
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_INITIAL_ON))
                    {
                        checkBoxInitialScreenshot.Checked = true;

                        Settings.User.GetByKey("BoolTakeInitialScreenshot", DefaultSettings.BoolTakeInitialScreenshot).Value = true;
                        Settings.User.Save();
                    }

                    // -initial=off
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_INITIAL_OFF))
                    {
                        checkBoxInitialScreenshot.Checked = false;

                        Settings.User.GetByKey("BoolTakeInitialScreenshot", DefaultSettings.BoolTakeInitialScreenshot).Value = false;
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
                            _screenCapture.Interval = screenCaptureInterval;
                            timerScreenCapture.Interval = screenCaptureInterval;

                            Settings.User.GetByKey("IntScreenCaptureInterval", DefaultSettings.IntScreenCaptureInterval).Value = screenCaptureInterval;
                            Settings.User.Save();

                            timerScreenCapture.Enabled = true;
                            timerScreenCapture.Start();
                        }
                    }

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

                            Settings.User.GetByKey("StringPassphrase", DefaultSettings.StringPassphrase).Value = Security.Hash(passphrase);
                            Settings.User.Save();

                            ScreenCapture.LockScreenCaptureSession = true;
                        }
                    }

                    // -startat=hh:mm:ss
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_STARTAT))
                    {
                        int hours = Convert.ToInt32(Regex.Match(arg, CommandLineRegex.REGEX_COMMAND_LINE_STARTAT).Groups["Hours"].Value);
                        int minutes = Convert.ToInt32(Regex.Match(arg, CommandLineRegex.REGEX_COMMAND_LINE_STARTAT).Groups["Minutes"].Value);
                        int seconds = Convert.ToInt32(Regex.Match(arg, CommandLineRegex.REGEX_COMMAND_LINE_STARTAT).Groups["Seconds"].Value);

                        _formSchedule.ScheduleCollection.GetByName(ScheduleCollection.SpecialScheduleName).Active = true;
                        _formSchedule.ScheduleCollection.GetByName(ScheduleCollection.SpecialScheduleName).ModeOneTime = false;
                        _formSchedule.ScheduleCollection.GetByName(ScheduleCollection.SpecialScheduleName).ModePeriod = true;
                        _formSchedule.ScheduleCollection.GetByName(ScheduleCollection.SpecialScheduleName).StartAt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hours, minutes, seconds);

                        ApplyDayForSpecialSchedule();

                        BuildSchedulesModule();

                        _formSchedule.ScheduleCollection.SaveToXmlFile();
                    }

                    // -stopat=hh:mm:ss
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_STOPAT))
                    {
                        int hours = Convert.ToInt32(Regex.Match(arg, CommandLineRegex.REGEX_COMMAND_LINE_STOPAT).Groups["Hours"].Value);
                        int minutes = Convert.ToInt32(Regex.Match(arg, CommandLineRegex.REGEX_COMMAND_LINE_STOPAT).Groups["Minutes"].Value);
                        int seconds = Convert.ToInt32(Regex.Match(arg, CommandLineRegex.REGEX_COMMAND_LINE_STOPAT).Groups["Seconds"].Value);

                        _formSchedule.ScheduleCollection.GetByName(ScheduleCollection.SpecialScheduleName).Active = true;
                        _formSchedule.ScheduleCollection.GetByName(ScheduleCollection.SpecialScheduleName).ModeOneTime = false;
                        _formSchedule.ScheduleCollection.GetByName(ScheduleCollection.SpecialScheduleName).ModePeriod = true;
                        _formSchedule.ScheduleCollection.GetByName(ScheduleCollection.SpecialScheduleName).StopAt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hours, minutes, seconds);

                        ApplyDayForSpecialSchedule();

                        BuildSchedulesModule();

                        _formSchedule.ScheduleCollection.SaveToXmlFile();
                    }

                    // -captureat=hh:mm:ss
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_CAPTUREAT))
                    {
                        int hours = Convert.ToInt32(Regex.Match(arg, CommandLineRegex.REGEX_COMMAND_LINE_CAPTUREAT).Groups["Hours"].Value);
                        int minutes = Convert.ToInt32(Regex.Match(arg, CommandLineRegex.REGEX_COMMAND_LINE_CAPTUREAT).Groups["Minutes"].Value);
                        int seconds = Convert.ToInt32(Regex.Match(arg, CommandLineRegex.REGEX_COMMAND_LINE_CAPTUREAT).Groups["Seconds"].Value);

                        _formSchedule.ScheduleCollection.GetByName(ScheduleCollection.SpecialScheduleName).Active = true;
                        _formSchedule.ScheduleCollection.GetByName(ScheduleCollection.SpecialScheduleName).ModeOneTime = true;
                        _formSchedule.ScheduleCollection.GetByName(ScheduleCollection.SpecialScheduleName).ModePeriod = false;
                        _formSchedule.ScheduleCollection.GetByName(ScheduleCollection.SpecialScheduleName).CaptureAt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hours, minutes, seconds);

                        ApplyDayForSpecialSchedule();

                        BuildSchedulesModule();

                        _formSchedule.ScheduleCollection.SaveToXmlFile();
                    }
                }

                if (ScreenCapture.AutoStartFromCommandLine)
                {
                    StartScreenCapture();
                }
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("FormMain-CommandLine::ParseCommandLineArguments", ex);
            }
        }

        private void ApplyDayForSpecialSchedule()
        {
            _formSchedule.ScheduleCollection.GetByName(ScheduleCollection.SpecialScheduleName).Monday = false;
            _formSchedule.ScheduleCollection.GetByName(ScheduleCollection.SpecialScheduleName).Tuesday = false;
            _formSchedule.ScheduleCollection.GetByName(ScheduleCollection.SpecialScheduleName).Wednesday = false;
            _formSchedule.ScheduleCollection.GetByName(ScheduleCollection.SpecialScheduleName).Thursday = false;
            _formSchedule.ScheduleCollection.GetByName(ScheduleCollection.SpecialScheduleName).Friday = false;
            _formSchedule.ScheduleCollection.GetByName(ScheduleCollection.SpecialScheduleName).Saturday = false;
            _formSchedule.ScheduleCollection.GetByName(ScheduleCollection.SpecialScheduleName).Sunday = false;

            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                _formSchedule.ScheduleCollection.GetByName(ScheduleCollection.SpecialScheduleName).Monday = true;
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
            {
                _formSchedule.ScheduleCollection.GetByName(ScheduleCollection.SpecialScheduleName).Tuesday = true;
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
            {
                _formSchedule.ScheduleCollection.GetByName(ScheduleCollection.SpecialScheduleName).Wednesday = true;
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
            {
                _formSchedule.ScheduleCollection.GetByName(ScheduleCollection.SpecialScheduleName).Thursday = true;
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                _formSchedule.ScheduleCollection.GetByName(ScheduleCollection.SpecialScheduleName).Friday = true;
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            {
                _formSchedule.ScheduleCollection.GetByName(ScheduleCollection.SpecialScheduleName).Saturday = true;
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                _formSchedule.ScheduleCollection.GetByName(ScheduleCollection.SpecialScheduleName).Sunday = true;
            }
        }
    }
}
