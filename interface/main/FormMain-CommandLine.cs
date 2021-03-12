//-----------------------------------------------------------------------
// <copyright file="FormMain-CommandLine.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2021 Gavin Kendall
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
        /// Regex for parsing the -config command.
        /// </summary>
        internal const string REGEX_COMMAND_LINE_CONFIG = "^-config=(?<ConfigFile>.+)$";

        /// <summary>
        /// Regex for parsing the -initial command.
        /// </summary>
        internal const string REGEX_COMMAND_LINE_INITIAL = "^-initial$";

        /// <summary>
        /// Regex for parsing the -initial=on command.
        /// </summary>
        internal const string REGEX_COMMAND_LINE_INITIAL_ON = "^-initial=on$";

        /// <summary>
        /// Regex for parsing the -initial=off command.
        /// </summary>
        internal const string REGEX_COMMAND_LINE_INITIAL_OFF = "^-initial=off$";

        /// <summary>
        /// Regex for parsing the -limit command.
        /// </summary>
        internal const string REGEX_COMMAND_LINE_LIMIT = @"^-limit=(?<Limit>\d{1,7})$";

        /// <summary>
        /// Regex for parsing the -captureat command.
        /// </summary>
        internal const string REGEX_COMMAND_LINE_CAPTUREAT = @"^-captureat=(?<Hours>\d{2}):(?<Minutes>\d{2}):(?<Seconds>\d{2})$";

        /// <summary>
        /// Regex for parsing the -stopat command.
        /// </summary>
        internal const string REGEX_COMMAND_LINE_STOPAT = @"^-stopat=(?<Hours>\d{2}):(?<Minutes>\d{2}):(?<Seconds>\d{2})$";

        /// <summary>
        /// Regex for parsing the -startat command.
        /// </summary>
        internal const string REGEX_COMMAND_LINE_STARTAT = @"^-startat=(?<Hours>\d{2}):(?<Minutes>\d{2}):(?<Seconds>\d{2})$";

        /// <summary>
        /// Regex for parsing the -interval command.
        /// </summary>
        internal const string REGEX_COMMAND_LINE_INTERVAL = @"^-interval=(?<Hours>\d{2}):(?<Minutes>\d{2}):(?<Seconds>\d{2})\.(?<Milliseconds>\d{3})$";

        /// <summary>
        /// Regex for parsing the -passphrase command.
        /// </summary>
        internal const string REGEX_COMMAND_LINE_PASSPHRASE = "^-passphrase=(?<Passphrase>.+)$";

        /// <summary>
        /// Regex for parsing the -showSystemTrayIcon command.
        /// </summary>
        internal const string REGEX_COMMAND_LINE_SHOW_SYSTEM_TRAY_ICON = "^-showSystemTrayIcon$";

        /// <summary>
        /// Regex for parsing the -hideSystemTrayIcon command.
        /// </summary>
        internal const string REGEX_COMMAND_LINE_HIDE_SYSTEM_TRAY_ICON = "^-hideSystemTrayIcon$";

        /// <summary>
        /// Regex for parsing the -log command.
        /// </summary>
        internal const string REGEX_COMMAND_LINE_LOG = "^-log$";

        /// <summary>
        /// Regex for parsing the -log=on command.
        /// </summary>
        internal const string REGEX_COMMAND_LINE_LOG_ON = "^-log=on$";

        /// <summary>
        /// Regex for parsing the -log=off command.
        /// </summary>
        internal const string REGEX_COMMAND_LINE_LOG_OFF = "^-log=off$";

        /// <summary>
        /// Regex for parsing the -debug command.
        /// </summary>
        internal const string REGEX_COMMAND_LINE_DEBUG = "^-debug$";

        /// <summary>
        /// Regex for parsing the -debug=on command.
        /// </summary>
        internal const string REGEX_COMMAND_LINE_DEBUG_ON = "^-debug=on$";

        /// <summary>
        /// Regex for parsing the -debug=off command.
        /// </summary>
        internal const string REGEX_COMMAND_LINE_DEBUG_OFF = "^-debug=off$";

        /// <summary>
        /// Regex for parsing the -capture command.
        /// </summary>
        internal const string REGEX_COMMAND_LINE_CAPTURE = "^-capture$";

        /// <summary>
        /// Regex for parsing the -start command.
        /// </summary>
        internal const string REGEX_COMMAND_LINE_START = "^-start$";

        /// <summary>
        /// Regex for parsing the -stop command.
        /// </summary>
        internal const string REGEX_COMMAND_LINE_STOP = "^-stop$";

        /// <summary>
        /// Regex for parsing the -exit command.
        /// </summary>
        internal const string REGEX_COMMAND_LINE_EXIT = "^-exit$";

        /// <summary>
        /// Regex for parsing the -activeWindowTitle command.
        /// </summary>
        internal const string REGEX_COMMAND_LINE_ACTIVE_WINDOW_TITLE = "^-activeWindowTitle=(?<ActiveWindowTitle>.+)$";

        /// <summary>
        /// Regex for parsing the -applicationFocus command.
        /// </summary>
        internal const string REGEX_COMMAND_LINE_APPLICATION_FOCUS = "^-applicationFocus=(?<ApplicationFocus>.+)$";

        /// <summary>
        /// Regex for parsing the -label command.
        /// </summary>
        internal const string REGEX_COMMAND_LINE_LABEL = "^-label=(?<Label>.+)$";

        /// <summary>
        /// Regex for parsing the -applicationFocusDelayBefore command.
        /// </summary>
        internal const string REGEX_COMMAND_LINE_APPLICATION_FOCUS_DELAY_BEFORE = @"^-applicationFocusDelayBefore=(?<ApplicationFocusDelayBefore>\d{1,5})$";

        /// <summary>
        /// Regex for parsing the -applicationFocusDelayAfter command.
        /// </summary>
        internal const string REGEX_COMMAND_LINE_APPLICATION_FOCUS_DELAY_AFTER = @"^-applicationFocusDelayAfter=(?<ApplicationFocusDelayAfter>\d{1,5})$";

        /// <summary>
        /// Regex for parsing the -saveScreenshotRefs=on command.
        /// </summary>
        internal const string REGEX_COMMAND_LINE_SAVE_SCREENSHOT_REFS_ON = "^-saveScreenshotRefs=on$";

        /// <summary>
        /// Regex for parsing the -saveScreenshotRefs=off command.
        /// </summary>
        internal const string REGEX_COMMAND_LINE_SAVE_SCREENSHOT_REFS_OFF = "^-saveScreenshotRefs=off$";

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
                _fileSystem.WriteToFile(_fileSystem.CommandFile, string.Empty);

                _screenCapture.AutoStartFromCommandLine = Convert.ToBoolean(_config.Settings.Application.GetByKey("AutoStartFromCommandLine", _config.Settings.DefaultSettings.AutoStartFromCommandLine).Value);

                // Create the "Special Schedule" if it doesn't already exist.
                if (_formSchedule.ScheduleCollection.GetByName(_formSchedule.ScheduleCollection.SpecialScheduleName) == null)
                {
                    DateTime dtNow = DateTime.Now;

                    Schedule specialSchedule = new Schedule()
                    {
                        Name = _formSchedule.ScheduleCollection.SpecialScheduleName,
                        Active = false,
                        ModeOneTime = true,
                        ModePeriod = false,
                        CaptureAt = dtNow,
                        StartAt = dtNow,
                        StopAt = dtNow,
                        ScreenCaptureInterval = _config.Settings.DefaultSettings.ScreenCaptureInterval,
                        Notes = "This schedule is used for the command line arguments -captureat, -startat, and -stopat."
                    };

                    _formSchedule.ScheduleCollection.Add(specialSchedule);

                    BuildSchedulesModule();

                    if (!_formSchedule.ScheduleCollection.SaveToXmlFile(_config.Settings, _fileSystem, _log))
                    {
                        _screenCapture.ApplicationError = true;
                    }
                }

                foreach (string arg in args)
                {
                    // -debug
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_DEBUG))
                    {
                        _log.DebugMode = !_log.DebugMode;

                        _config.Settings.Application.SetValueByKey("DebugMode", _log.DebugMode);

                        if (!_config.Settings.Application.Save(_config.Settings, _fileSystem))
                        {
                            _screenCapture.ApplicationError = true;
                        }
                    }

                    // -debug=on
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_DEBUG_ON))
                    {
                        _log.DebugMode = true;

                        _config.Settings.Application.SetValueByKey("DebugMode", true);

                        if (!_config.Settings.Application.Save(_config.Settings, _fileSystem))
                        {
                            _screenCapture.ApplicationError = true;
                        }
                    }

                    // -debug=off
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_DEBUG_OFF))
                    {
                        _log.DebugMode = false;

                        _config.Settings.Application.SetValueByKey("DebugMode", false);

                        if (!_config.Settings.Application.Save(_config.Settings, _fileSystem))
                        {
                            _screenCapture.ApplicationError = true;
                        }
                    }

                    // -log
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_LOG))
                    {
                        _log.LoggingEnabled = !_log.LoggingEnabled;

                        _config.Settings.Application.SetValueByKey("Logging", _log.LoggingEnabled);

                        if (!_config.Settings.Application.Save(_config.Settings, _fileSystem))
                        {
                            _screenCapture.ApplicationError = true;
                        }
                    }

                    // -log=on
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_LOG_ON))
                    {
                        _log.LoggingEnabled = true;

                        _config.Settings.Application.SetValueByKey("Logging", true);

                        if (!_config.Settings.Application.Save(_config.Settings, _fileSystem))
                        {
                            _screenCapture.ApplicationError = true;
                        }
                    }

                    // -log=off
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_LOG_OFF))
                    {
                        _log.LoggingEnabled = false;

                        _config.Settings.Application.SetValueByKey("Logging", false);

                        if (!_config.Settings.Application.Save(_config.Settings, _fileSystem))
                        {
                            _screenCapture.ApplicationError = true;
                        }
                    }

                    // -capture
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_CAPTURE))
                    {
                        TakeScreenshot(captureNow: true);
                    }

                    // -start
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_START))
                    {
                        StartScreenCapture();
                    }

                    // -stop
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_STOP))
                    {
                        _screenCapture.AutoStartFromCommandLine = false;

                        StopScreenCapture();
                    }

                    // -exit
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_EXIT))
                    {
                        _screenCapture.AutoStartFromCommandLine = false;

                        ExitApplication();
                    }

                    // -showSystemTrayIcon
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_SHOW_SYSTEM_TRAY_ICON))
                    {
                        _config.Settings.User.SetValueByKey("ShowSystemTrayIcon", true);

                        if (!_config.Settings.User.Save(_config.Settings, _fileSystem))
                        {
                            _screenCapture.ApplicationError = true;
                        }

                        notifyIcon.Visible = true;
                    }

                    // -hideSystemTrayIcon
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_HIDE_SYSTEM_TRAY_ICON))
                    {
                        _config.Settings.User.SetValueByKey("ShowSystemTrayIcon", false);

                        if (!_config.Settings.User.Save(_config.Settings, _fileSystem))
                        {
                            _screenCapture.ApplicationError = true;
                        }

                        notifyIcon.Visible = false;
                    }

                    // -initial
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_INITIAL))
                    {
                        checkBoxInitialScreenshot.Checked = !checkBoxInitialScreenshot.Checked;

                        _config.Settings.User.SetValueByKey("TakeInitialScreenshot", checkBoxInitialScreenshot.Checked);

                        if (!_config.Settings.User.Save(_config.Settings, _fileSystem))
                        {
                            _screenCapture.ApplicationError = true;
                        }
                    }

                    // -initial=on
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_INITIAL_ON))
                    {
                        checkBoxInitialScreenshot.Checked = true;

                        _config.Settings.User.SetValueByKey("TakeInitialScreenshot", true);

                        if (!_config.Settings.User.Save(_config.Settings, _fileSystem))
                        {
                            _screenCapture.ApplicationError = true;
                        }
                    }

                    // -initial=off
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_INITIAL_OFF))
                    {
                        checkBoxInitialScreenshot.Checked = false;

                        _config.Settings.User.SetValueByKey("TakeInitialScreenshot", false);

                        if (!_config.Settings.User.Save(_config.Settings, _fileSystem))
                        {
                            _screenCapture.ApplicationError = true;
                        }
                    }

                    // -limit=x
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_LIMIT))
                    {
                        int cmdLimit = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_LIMIT).Groups["Limit"].Value);

                        if (cmdLimit >= CAPTURE_LIMIT_MIN && cmdLimit <= CAPTURE_LIMIT_MAX)
                        {
                            numericUpDownCaptureLimit.Value = cmdLimit;
                            checkBoxCaptureLimit.Checked = true;

                            _screenCapture.Limit = checkBoxCaptureLimit.Checked ? (int)numericUpDownCaptureLimit.Value : 0;
                        }
                    }

                    // -interval=hh:mm:ss.nnn
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_INTERVAL))
                    {
                        int hours = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_INTERVAL).Groups["Hours"].Value);
                        int minutes = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_INTERVAL).Groups["Minutes"].Value);
                        int seconds = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_INTERVAL).Groups["Seconds"].Value);
                        int milliseconds = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_INTERVAL).Groups["Milliseconds"].Value);

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

                            _config.Settings.User.SetValueByKey("ScreenCaptureInterval", screenCaptureInterval);

                            if (!_config.Settings.User.Save(_config.Settings, _fileSystem))
                            {
                                _screenCapture.ApplicationError = true;
                            }

                            timerScreenCapture.Enabled = true;
                            timerScreenCapture.Start();

                            _formSchedule.ScheduleCollection.GetByName(_formSchedule.ScheduleCollection.SpecialScheduleName).ScreenCaptureInterval = screenCaptureInterval;
                        }
                    }

                    // -passphrase="x"
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_PASSPHRASE))
                    {
                        string passphrase = Regex.Match(arg, REGEX_COMMAND_LINE_PASSPHRASE).Groups["Passphrase"].Value;

                        if (passphrase.Length > 0)
                        {
                            if (string.IsNullOrEmpty(_fileSystem.UserSettingsFile))
                            {
                                _config.Load(_fileSystem);
                            }

                            passphrase = passphrase.Trim();

                            _config.Settings.User.SetValueByKey("Passphrase", _security.Hash(passphrase));

                            if (!_config.Settings.User.Save(_config.Settings, _fileSystem))
                            {
                                _screenCapture.ApplicationError = true;
                            }

                            _screenCapture.LockScreenCaptureSession = true;
                        }
                    }

                    // -startat=hh:mm:ss
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_STARTAT))
                    {
                        _screenCapture.AutoStartFromCommandLine = false;

                        int hours = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_STARTAT).Groups["Hours"].Value);
                        int minutes = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_STARTAT).Groups["Minutes"].Value);
                        int seconds = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_STARTAT).Groups["Seconds"].Value);

                        _formSchedule.ScheduleCollection.GetByName(_formSchedule.ScheduleCollection.SpecialScheduleName).Active = true;
                        _formSchedule.ScheduleCollection.GetByName(_formSchedule.ScheduleCollection.SpecialScheduleName).ModeOneTime = false;
                        _formSchedule.ScheduleCollection.GetByName(_formSchedule.ScheduleCollection.SpecialScheduleName).ModePeriod = true;
                        _formSchedule.ScheduleCollection.GetByName(_formSchedule.ScheduleCollection.SpecialScheduleName).StartAt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hours, minutes, seconds);

                        ApplyDayForSpecialSchedule();

                        BuildSchedulesModule();

                        if (!_formSchedule.ScheduleCollection.SaveToXmlFile(_config.Settings, _fileSystem, _log))
                        {
                            _screenCapture.ApplicationError = true;
                        }
                    }

                    // -stopat=hh:mm:ss
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_STOPAT))
                    {
                        _screenCapture.AutoStartFromCommandLine = false;

                        int hours = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_STOPAT).Groups["Hours"].Value);
                        int minutes = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_STOPAT).Groups["Minutes"].Value);
                        int seconds = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_STOPAT).Groups["Seconds"].Value);

                        _formSchedule.ScheduleCollection.GetByName(_formSchedule.ScheduleCollection.SpecialScheduleName).Active = true;
                        _formSchedule.ScheduleCollection.GetByName(_formSchedule.ScheduleCollection.SpecialScheduleName).ModeOneTime = false;
                        _formSchedule.ScheduleCollection.GetByName(_formSchedule.ScheduleCollection.SpecialScheduleName).ModePeriod = true;
                        _formSchedule.ScheduleCollection.GetByName(_formSchedule.ScheduleCollection.SpecialScheduleName).StopAt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hours, minutes, seconds);

                        ApplyDayForSpecialSchedule();

                        BuildSchedulesModule();

                        if (!_formSchedule.ScheduleCollection.SaveToXmlFile(_config.Settings, _fileSystem, _log))
                        {
                            _screenCapture.ApplicationError = true;
                        }
                    }

                    // -captureat=hh:mm:ss
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_CAPTUREAT))
                    {
                        _screenCapture.AutoStartFromCommandLine = false;

                        int hours = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_CAPTUREAT).Groups["Hours"].Value);
                        int minutes = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_CAPTUREAT).Groups["Minutes"].Value);
                        int seconds = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_CAPTUREAT).Groups["Seconds"].Value);

                        _formSchedule.ScheduleCollection.GetByName(_formSchedule.ScheduleCollection.SpecialScheduleName).Active = true;
                        _formSchedule.ScheduleCollection.GetByName(_formSchedule.ScheduleCollection.SpecialScheduleName).ModeOneTime = true;
                        _formSchedule.ScheduleCollection.GetByName(_formSchedule.ScheduleCollection.SpecialScheduleName).ModePeriod = false;
                        _formSchedule.ScheduleCollection.GetByName(_formSchedule.ScheduleCollection.SpecialScheduleName).CaptureAt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hours, minutes, seconds);

                        ApplyDayForSpecialSchedule();

                        BuildSchedulesModule();

                        if (!_formSchedule.ScheduleCollection.SaveToXmlFile(_config.Settings, _fileSystem, _log))
                        {
                            _screenCapture.ApplicationError = true;
                        }
                    }

                    // -activeWindowTitle="x"
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_ACTIVE_WINDOW_TITLE))
                    {
                        string activeWindowTitle = Regex.Match(arg, REGEX_COMMAND_LINE_ACTIVE_WINDOW_TITLE).Groups["ActiveWindowTitle"].Value;

                        if (activeWindowTitle.Length > 0)
                        {
                            if (string.IsNullOrEmpty(_fileSystem.UserSettingsFile))
                            {
                                _config.Load(_fileSystem);
                            }

                            SetActiveWindowTitle(activeWindowTitle);
                        }
                    }

                    // -applicationFocus="x"
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_APPLICATION_FOCUS))
                    {
                        string applicationFocus = Regex.Match(arg, REGEX_COMMAND_LINE_APPLICATION_FOCUS).Groups["ApplicationFocus"].Value;

                        if (applicationFocus.Length > 0)
                        {
                            if (string.IsNullOrEmpty(_fileSystem.UserSettingsFile))
                            {
                                _config.Load(_fileSystem);
                            }

                            SetApplicationFocus(applicationFocus);
                        }
                    }

                    // -label="x"
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_LABEL))
                    {
                        string label = Regex.Match(arg, REGEX_COMMAND_LINE_LABEL).Groups["Label"].Value;

                        if (label.Length > 0)
                        {
                            if (string.IsNullOrEmpty(_fileSystem.UserSettingsFile))
                            {
                                _config.Load(_fileSystem);
                            }

                            ApplyLabel(label);
                        }
                    }

                    // -applicationFocusDelayBefore=x
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_APPLICATION_FOCUS_DELAY_BEFORE))
                    {
                        int delayBefore = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_APPLICATION_FOCUS_DELAY_BEFORE).Groups["ApplicationFocusDelayBefore"].Value);

                        _config.Settings.User.SetValueByKey("ApplicationFocusDelayBefore", delayBefore);

                        if (!_config.Settings.User.Save(_config.Settings, _fileSystem))
                        {
                            _screenCapture.ApplicationError = true;
                        }

                        numericUpDownApplicationFocusDelayBefore.Value = delayBefore;
                    }

                    // -applicationFocusDelayAfter=x
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_APPLICATION_FOCUS_DELAY_AFTER))
                    {
                        int delayAfter = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_APPLICATION_FOCUS_DELAY_AFTER).Groups["ApplicationFocusDelayAfter"].Value);

                        _config.Settings.User.SetValueByKey("ApplicationFocusDelayAfter", delayAfter);

                        if (!_config.Settings.User.Save(_config.Settings, _fileSystem))
                        {
                            _screenCapture.ApplicationError = true;
                        }

                        numericUpDownApplicationFocusDelayAfter.Value = delayAfter;
                    }

                    // -saveScreenshotRefs=on
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_SAVE_SCREENSHOT_REFS_ON))
                    {
                        _config.Settings.User.SetValueByKey("SaveScreenshotRefs", true);

                        if (!_config.Settings.User.Save(_config.Settings, _fileSystem))
                        {
                            _screenCapture.ApplicationError = true;
                        }
                    }

                    // -saveScreenshotRefs=off
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_SAVE_SCREENSHOT_REFS_OFF))
                    {
                        _config.Settings.User.SetValueByKey("SaveScreenshotRefs", false);

                        if (!_config.Settings.User.Save(_config.Settings, _fileSystem))
                        {
                            _screenCapture.ApplicationError = true;
                        }
                    }
                }

                if (_screenCapture.AutoStartFromCommandLine)
                {
                    StartScreenCapture();
                }
            }
            catch (Exception ex)
            {
                _screenCapture.ApplicationError = true;
                _log.WriteExceptionMessage("FormMain-CommandLine::ParseCommandLineArguments", ex);
            }
        }

        private void ApplyDayForSpecialSchedule()
        {
            _formSchedule.ScheduleCollection.GetByName(_formSchedule.ScheduleCollection.SpecialScheduleName).Monday = false;
            _formSchedule.ScheduleCollection.GetByName(_formSchedule.ScheduleCollection.SpecialScheduleName).Tuesday = false;
            _formSchedule.ScheduleCollection.GetByName(_formSchedule.ScheduleCollection.SpecialScheduleName).Wednesday = false;
            _formSchedule.ScheduleCollection.GetByName(_formSchedule.ScheduleCollection.SpecialScheduleName).Thursday = false;
            _formSchedule.ScheduleCollection.GetByName(_formSchedule.ScheduleCollection.SpecialScheduleName).Friday = false;
            _formSchedule.ScheduleCollection.GetByName(_formSchedule.ScheduleCollection.SpecialScheduleName).Saturday = false;
            _formSchedule.ScheduleCollection.GetByName(_formSchedule.ScheduleCollection.SpecialScheduleName).Sunday = false;

            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                _formSchedule.ScheduleCollection.GetByName(_formSchedule.ScheduleCollection.SpecialScheduleName).Monday = true;
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
            {
                _formSchedule.ScheduleCollection.GetByName(_formSchedule.ScheduleCollection.SpecialScheduleName).Tuesday = true;
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
            {
                _formSchedule.ScheduleCollection.GetByName(_formSchedule.ScheduleCollection.SpecialScheduleName).Wednesday = true;
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
            {
                _formSchedule.ScheduleCollection.GetByName(_formSchedule.ScheduleCollection.SpecialScheduleName).Thursday = true;
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                _formSchedule.ScheduleCollection.GetByName(_formSchedule.ScheduleCollection.SpecialScheduleName).Friday = true;
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            {
                _formSchedule.ScheduleCollection.GetByName(_formSchedule.ScheduleCollection.SpecialScheduleName).Saturday = true;
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                _formSchedule.ScheduleCollection.GetByName(_formSchedule.ScheduleCollection.SpecialScheduleName).Sunday = true;
            }
        }
    }
}
