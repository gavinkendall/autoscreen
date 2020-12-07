//-----------------------------------------------------------------------
// <copyright file="FormMain-CommandLine.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>All the methods for handling command line arguments.</summary>
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
                        StopAt = dtNow,
                        ScreenCaptureInterval = DefaultSettings.ScreenCaptureInterval,
                        Notes = "This schedule is used for the command line arguments -captureat, -startat, and -stopat."
                    };

                    _formSchedule.ScheduleCollection.Add(specialSchedule);

                    BuildSchedulesModule();

                    if (!_formSchedule.ScheduleCollection.SaveToXmlFile())
                    {
                        _screenCapture.ApplicationError = true;
                    }
                }

                foreach (string arg in args)
                {
                    // -debug
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_DEBUG))
                    {
                        Log.DebugMode = !Log.DebugMode;

                        Settings.Application.SetValueByKey("DebugMode", Log.DebugMode);

                        if (!Settings.Application.Save())
                        {
                            _screenCapture.ApplicationError = true;
                        }
                    }

                    // -debug=on
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_DEBUG_ON))
                    {
                        Log.DebugMode = true;

                        Settings.Application.SetValueByKey("DebugMode", true);

                        if (!Settings.Application.Save())
                        {
                            _screenCapture.ApplicationError = true;
                        }
                    }

                    // -debug=off
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_DEBUG_OFF))
                    {
                        Log.DebugMode = false;

                        Settings.Application.SetValueByKey("DebugMode", false);
                        
                        if (!Settings.Application.Save())
                        {
                            _screenCapture.ApplicationError = true;
                        }
                    }

                    // -log
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_LOG))
                    {
                        Log.LoggingEnabled = !Log.LoggingEnabled;

                        Settings.Application.SetValueByKey("Logging", Log.LoggingEnabled);

                        if (!Settings.Application.Save())
                        {
                            _screenCapture.ApplicationError = true;
                        }
                    }

                    // -log=on
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_LOG_ON))
                    {
                        Log.LoggingEnabled = true;

                        Settings.Application.SetValueByKey("Logging", true);

                        if (!Settings.Application.Save())
                        {
                            _screenCapture.ApplicationError = true;
                        }
                    }

                    // -log=off
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_LOG_OFF))
                    {
                        Log.LoggingEnabled = false;

                        Settings.Application.SetValueByKey("Logging", false);

                        if (!Settings.Application.Save())
                        {
                            _screenCapture.ApplicationError = true;
                        }
                    }

                    // -capture
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_CAPTURE))
                    {
                        RefreshApplicationFocusList();

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
                        Settings.User.SetValueByKey("ShowSystemTrayIcon", true);

                        if (!Settings.User.Save())
                        {
                            _screenCapture.ApplicationError = true;
                        }

                        notifyIcon.Visible = true;
                    }

                    // -hideSystemTrayIcon
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_HIDE_SYSTEM_TRAY_ICON))
                    {
                        Settings.User.SetValueByKey("ShowSystemTrayIcon", false);

                        if (!Settings.User.Save())
                        {
                            _screenCapture.ApplicationError = true;
                        }

                        notifyIcon.Visible = false;
                    }

                    // -initial
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_INITIAL))
                    {
                        checkBoxInitialScreenshot.Checked = !checkBoxInitialScreenshot.Checked;

                        Settings.User.SetValueByKey("TakeInitialScreenshot", checkBoxInitialScreenshot.Checked);

                        if (!Settings.User.Save())
                        {
                            _screenCapture.ApplicationError = true;
                        }
                    }

                    // -initial=on
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_INITIAL_ON))
                    {
                        checkBoxInitialScreenshot.Checked = true;

                        Settings.User.SetValueByKey("TakeInitialScreenshot", true);

                        if (!Settings.User.Save())
                        {
                            _screenCapture.ApplicationError = true;
                        }
                    }

                    // -initial=off
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_INITIAL_OFF))
                    {
                        checkBoxInitialScreenshot.Checked = false;

                        Settings.User.SetValueByKey("TakeInitialScreenshot", false);

                        if (!Settings.User.Save())
                        {
                            _screenCapture.ApplicationError = true;
                        }
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

                            Settings.User.SetValueByKey("ScreenCaptureInterval", screenCaptureInterval);

                            if (!Settings.User.Save())
                            {
                                _screenCapture.ApplicationError = true;
                            }

                            timerScreenCapture.Enabled = true;
                            timerScreenCapture.Start();

                            _formSchedule.ScheduleCollection.GetByName(ScheduleCollection.SpecialScheduleName).ScreenCaptureInterval = screenCaptureInterval;
                        }
                    }

                    // -passphrase="x"
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_PASSPHRASE))
                    {
                        string passphrase = Regex.Match(arg, CommandLineRegex.REGEX_COMMAND_LINE_PASSPHRASE).Groups["Passphrase"].Value;

                        if (passphrase.Length > 0)
                        {
                            if (string.IsNullOrEmpty(FileSystem.UserSettingsFile))
                            {
                                Config.Load();
                            }

                            passphrase = passphrase.Trim();

                            Settings.User.SetValueByKey("Passphrase", Security.Hash(passphrase));

                            if (!Settings.User.Save())
                            {
                                _screenCapture.ApplicationError = true;
                            }

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

                        if (!_formSchedule.ScheduleCollection.SaveToXmlFile())
                        {
                            _screenCapture.ApplicationError = true;
                        }
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

                        if (!_formSchedule.ScheduleCollection.SaveToXmlFile())
                        {
                            _screenCapture.ApplicationError = true;
                        }
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

                        if (!_formSchedule.ScheduleCollection.SaveToXmlFile())
                        {
                            _screenCapture.ApplicationError = true;
                        }
                    }

                    // -activeWindowTitle="x"
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_ACTIVE_WINDOW_TITLE))
                    {
                        string activeWindowTitle = Regex.Match(arg, CommandLineRegex.REGEX_COMMAND_LINE_ACTIVE_WINDOW_TITLE).Groups["ActiveWindowTitle"].Value;

                        if (string.IsNullOrEmpty(FileSystem.UserSettingsFile))
                        {
                            Config.Load();
                        }

                        if (string.IsNullOrEmpty(activeWindowTitle))
                        {
                            Settings.User.SetValueByKey("ActiveWindowTitleCaptureCheck", false);

                            checkBoxActiveWindowTitle.Checked = false;
                        }
                        else
                        {
                            activeWindowTitle = activeWindowTitle.Trim();

                            Settings.User.SetValueByKey("ActiveWindowTitleCaptureCheck", true);
                            Settings.User.SetValueByKey("ActiveWindowTitleCaptureText", activeWindowTitle);

                            checkBoxActiveWindowTitle.Checked = true;
                            textBoxActiveWindowTitle.Text = activeWindowTitle;

                            _screenCapture.ActiveWindowTitle = activeWindowTitle;
                        }

                        if (!Settings.User.Save())
                        {
                            _screenCapture.ApplicationError = true;
                        }
                    }

                    // -applicationFocus="x"
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_APPLICATION_FOCUS))
                    {
                        string applicationFocus = Regex.Match(arg, CommandLineRegex.REGEX_COMMAND_LINE_APPLICATION_FOCUS).Groups["ApplicationFocus"].Value;

                        if (string.IsNullOrEmpty(FileSystem.UserSettingsFile))
                        {
                            Config.Load();
                        }

                        if (string.IsNullOrEmpty(applicationFocus))
                        {
                            Settings.User.SetValueByKey("ApplicationFocus", string.Empty);
                        }
                        else
                        {
                            applicationFocus = applicationFocus.Trim();

                            Settings.User.SetValueByKey("ApplicationFocus", applicationFocus);
                        }

                        if (!Settings.User.Save())
                        {
                            _screenCapture.ApplicationError = true;
                        }

                        RefreshApplicationFocusList();

                        DoApplicationFocus();
                    }

                    // -label="x"
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_LABEL))
                    {
                        string label = Regex.Match(arg, CommandLineRegex.REGEX_COMMAND_LINE_LABEL).Groups["Label"].Value;

                        if (string.IsNullOrEmpty(FileSystem.UserSettingsFile))
                        {
                            Config.Load();
                        }

                        if (string.IsNullOrEmpty(label))
                        {
                            Settings.User.SetValueByKey("ApplyScreenshotLabel", false);

                            checkBoxScreenshotLabel.Checked = false;
                        }
                        else
                        {
                            label = label.Trim();

                            Settings.User.SetValueByKey("ApplyScreenshotLabel", true);
                            Settings.User.SetValueByKey("ScreenshotLabel", label);

                            if (!Settings.User.Save())
                            {
                                _screenCapture.ApplicationError = true;
                            }

                            checkBoxScreenshotLabel.Checked = true;
                            comboBoxScreenshotLabel.Text = label;
                        }
                    }

                    // -applicationFocusDelayBefore=x
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_APPLICATION_FOCUS_DELAY_BEFORE))
                    {
                        int delayBefore = Convert.ToInt32(Regex.Match(arg, CommandLineRegex.REGEX_COMMAND_LINE_APPLICATION_FOCUS_DELAY_BEFORE).Groups["ApplicationFocusDelayBefore"].Value);

                        Settings.User.SetValueByKey("ApplicationFocusDelayBefore", delayBefore);

                        if (!Settings.User.Save())
                        {
                            _screenCapture.ApplicationError = true;
                        }

                        numericUpDownApplicationFocusDelayBefore.Value = delayBefore;
                    }

                    // -applicationFocusDelayAfter=x
                    if (Regex.IsMatch(arg, CommandLineRegex.REGEX_COMMAND_LINE_APPLICATION_FOCUS_DELAY_AFTER))
                    {
                        int delayAfter = Convert.ToInt32(Regex.Match(arg, CommandLineRegex.REGEX_COMMAND_LINE_APPLICATION_FOCUS_DELAY_AFTER).Groups["ApplicationFocusDelayAfter"].Value);

                        Settings.User.SetValueByKey("ApplicationFocusDelayAfter", delayAfter);

                        if (!Settings.User.Save())
                        {
                            _screenCapture.ApplicationError = true;
                        }

                        numericUpDownApplicationFocusDelayAfter.Value = delayAfter;
                    }
                }

                if (ScreenCapture.AutoStartFromCommandLine)
                {
                    StartScreenCapture();
                }
            }
            catch (Exception ex)
            {
                _screenCapture.ApplicationError = true;
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
