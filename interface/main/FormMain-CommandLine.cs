//-----------------------------------------------------------------------
// <copyright file="FormMain-CommandLine.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
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
using System.Diagnostics;
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
        /// Regex for parsing the -activeWindowTitleMatch command.
        /// </summary>
        internal const string REGEX_COMMAND_LINE_ACTIVE_WINDOW_TITLE_MATCH = "^-activeWindowTitleMatch=(?<ActiveWindowTitleMatch>.+)$"; // The command line option -activeWindowTitleMatch is the same as -activeWindowTitle (to maintain backwards compatibility with previous versions since -activeWindowTitle is the old command line option)

        /// <summary>
        /// Regex for parsing the -activeWindowTitleNoMatch command.
        /// </summary>
        internal const string REGEX_COMMAND_LINE_ACTIVE_WINDOW_TITLE_NO_MATCH = "^-activeWindowTitleNoMatch=(?<ActiveWindowTitleNoMatch>.+)$";

        /// <summary>
        /// Regex for parsing the -activeWindowTitleMatchType command.
        /// </summary>
        internal const string REGEX_COMMAND_LINE_ACTIVE_WINDOW_TITLE_MATCH_TYPE = @"^-activeWindowTitleMatchType=(?<ActiveWindowTitleMatchType>\d{1})$";

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
        /// Regex for parsing the -show command.
        /// </summary>
        internal const string REGEX_COMMAND_LINE_SHOW = "^-show$";

        /// <summary>
        /// Regex for parsing the -hide command.
        /// </summary>
        internal const string REGEX_COMMAND_LINE_HIDE = "^-hide$";

        /// <summary>
        /// Parse commands issued externally via the command line.
        /// </summary>
        private void ParseCommandLineArguments()
        {
            if (!_fileSystem.FileExists(_fileSystem.CommandFile))
            {
                _fileSystem.CreateFile(_fileSystem.CommandFile);
            }

            // Parse commands issued externally via the command line.
            if (_fileSystem.FileExists(_fileSystem.CommandFile))
            {
                string[] args = _fileSystem.ReadFromFile(_fileSystem.CommandFile);

                if (args.Length > 0)
                {
                    ParseCommandLineArguments(args);
                }
            }
        }

        /// <summary>
        /// Shows a command line terminal at the location of the application's executable.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripDropDownButtonCommandLine_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd.exe");
                processStartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;

                Process process = new Process
                {
                    StartInfo = processStartInfo
                };

                process.Start();
            }
            catch (Exception ex)
            {
                _log.WriteExceptionMessage("Unable to start command line", ex);
            }
        }

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

                        ShowSystemTrayIcon();
                    }

                    // -hideSystemTrayIcon
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_HIDE_SYSTEM_TRAY_ICON))
                    {
                        _config.Settings.User.SetValueByKey("ShowSystemTrayIcon", false);

                        if (!_config.Settings.User.Save(_config.Settings, _fileSystem))
                        {
                            _screenCapture.ApplicationError = true;
                        }

                        HideSystemTrayIcon();
                    }

                    // -initial
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_INITIAL))
                    {
                        _formSetup.checkBoxInitialScreenshot.Checked = !_formSetup.checkBoxInitialScreenshot.Checked;

                        _config.Settings.User.SetValueByKey("TakeInitialScreenshot", _formSetup.checkBoxInitialScreenshot.Checked);

                        if (!_config.Settings.User.Save(_config.Settings, _fileSystem))
                        {
                            _screenCapture.ApplicationError = true;
                        }
                    }

                    // -initial=on
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_INITIAL_ON))
                    {
                        _formSetup.checkBoxInitialScreenshot.Checked = true;

                        _config.Settings.User.SetValueByKey("TakeInitialScreenshot", true);

                        if (!_config.Settings.User.Save(_config.Settings, _fileSystem))
                        {
                            _screenCapture.ApplicationError = true;
                        }
                    }

                    // -initial=off
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_INITIAL_OFF))
                    {
                        _formSetup.checkBoxInitialScreenshot.Checked = false;

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
                            _formSetup.numericUpDownCaptureLimit.Value = cmdLimit;
                            _formSetup.checkBoxCaptureLimit.Checked = true;

                            _screenCapture.Limit = _formSetup.checkBoxCaptureLimit.Checked ? (int)_formSetup.numericUpDownCaptureLimit.Value : 0;
                        }
                    }

                    // -interval=hh:mm:ss.nnn
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_INTERVAL))
                    {
                        int hours = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_INTERVAL).Groups["Hours"].Value);
                        int minutes = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_INTERVAL).Groups["Minutes"].Value);
                        int seconds = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_INTERVAL).Groups["Seconds"].Value);
                        int milliseconds = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_INTERVAL).Groups["Milliseconds"].Value);

                        _formSetup.numericUpDownHoursInterval.Value = hours;
                        _formSetup.numericUpDownMinutesInterval.Value = minutes;
                        _formSetup.numericUpDownSecondsInterval.Value = seconds;
                        _formSetup.numericUpDownMillisecondsInterval.Value = milliseconds;

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

                            _formSchedule.ScheduleCollection.SpecialScheduleScreenCaptureInterval = screenCaptureInterval;
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

                            string passphraseLastUpdated = DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss");

                            _config.Settings.User.SetValueByKey("PassphraseLastUpdated", passphraseLastUpdated);

                            _config.Settings.User.SetValueByKey("Passphrase", _security.Hash(passphrase));

                            _screenCapture.LockScreenCaptureSession = true;

                            if (!_config.Settings.User.Save(_config.Settings, _fileSystem))
                            {
                                _screenCapture.ApplicationError = true;
                            }
                        }
                    }

                    // -startat=hh:mm:ss
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_STARTAT))
                    {
                        _screenCapture.AutoStartFromCommandLine = false;

                        int hours = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_STARTAT).Groups["Hours"].Value);
                        int minutes = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_STARTAT).Groups["Minutes"].Value);
                        int seconds = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_STARTAT).Groups["Seconds"].Value);

                        _formSchedule.ScheduleCollection.SpecialScheduleEnabled = true;
                        _formSchedule.ScheduleCollection.SpecialScheduleModeOneTime = false;
                        _formSchedule.ScheduleCollection.SpecialScheduleModePeriod = true;
                        _formSchedule.ScheduleCollection.SpecialScheduleStartAt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hours, minutes, seconds);
                    }

                    // -stopat=hh:mm:ss
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_STOPAT))
                    {
                        _screenCapture.AutoStartFromCommandLine = false;

                        int hours = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_STOPAT).Groups["Hours"].Value);
                        int minutes = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_STOPAT).Groups["Minutes"].Value);
                        int seconds = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_STOPAT).Groups["Seconds"].Value);

                        _formSchedule.ScheduleCollection.SpecialScheduleEnabled = true;
                        _formSchedule.ScheduleCollection.SpecialScheduleModeOneTime = false;
                        _formSchedule.ScheduleCollection.SpecialScheduleModePeriod = true;
                        _formSchedule.ScheduleCollection.SpecialScheduleStopAt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hours, minutes, seconds);
                    }

                    // -captureat=hh:mm:ss
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_CAPTUREAT))
                    {
                        _screenCapture.AutoStartFromCommandLine = false;

                        int hours = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_CAPTUREAT).Groups["Hours"].Value);
                        int minutes = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_CAPTUREAT).Groups["Minutes"].Value);
                        int seconds = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_CAPTUREAT).Groups["Seconds"].Value);

                        _formSchedule.ScheduleCollection.SpecialScheduleEnabled = true;
                        _formSchedule.ScheduleCollection.SpecialScheduleModeOneTime = true;
                        _formSchedule.ScheduleCollection.SpecialScheduleModePeriod = false;
                        _formSchedule.ScheduleCollection.SpecialScheduleCaptureAt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hours, minutes, seconds);
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

                            SetActiveWindowTitleAsMatch(activeWindowTitle);
                        }
                    }

                    // -activeWindowTitleMatch="x"
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_ACTIVE_WINDOW_TITLE_MATCH))
                    {
                        string activeWindowTitle = Regex.Match(arg, REGEX_COMMAND_LINE_ACTIVE_WINDOW_TITLE_MATCH).Groups["ActiveWindowTitleMatch"].Value;

                        if (activeWindowTitle.Length > 0)
                        {
                            if (string.IsNullOrEmpty(_fileSystem.UserSettingsFile))
                            {
                                _config.Load(_fileSystem);
                            }

                            SetActiveWindowTitleAsMatch(activeWindowTitle);
                        }
                    }

                    // -activeWindowTitleNoMatch="x"
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_ACTIVE_WINDOW_TITLE_NO_MATCH))
                    {
                        string activeWindowTitle = Regex.Match(arg, REGEX_COMMAND_LINE_ACTIVE_WINDOW_TITLE_NO_MATCH).Groups["ActiveWindowTitleNoMatch"].Value;

                        if (activeWindowTitle.Length > 0)
                        {
                            if (string.IsNullOrEmpty(_fileSystem.UserSettingsFile))
                            {
                                _config.Load(_fileSystem);
                            }

                            //SetActiveWindowTitleAsNoMatch(activeWindowTitle);
                        }
                    }

                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_ACTIVE_WINDOW_TITLE_MATCH_TYPE))
                    {
                        int.TryParse(Regex.Match(arg, REGEX_COMMAND_LINE_ACTIVE_WINDOW_TITLE_MATCH_TYPE).Groups["ActiveWindowTitleMatchType"].Value, out int activeWindowTitleMatchType);

                        switch (activeWindowTitleMatchType)
                        {
                            case 1:
                                _formSetup.radioButtonCaseSensitiveMatch.Checked = true;
                                break;

                            case 2:
                                _formSetup.radioButtonCaseInsensitiveMatch.Checked = true;
                                break;

                            case 3:
                                _formSetup.radioButtonRegularExpressionMatch.Checked = true;
                                break;
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

                            _formSetup.SetApplicationFocus(applicationFocus);
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

                        _formSetup.numericUpDownApplicationFocusDelayBefore.Value = delayBefore;
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

                        _formSetup.numericUpDownApplicationFocusDelayAfter.Value = delayAfter;
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

                    // -show
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_SHOW))
                    {
                        _config.Settings.User.SetValueByKey("ShowSystemTrayIcon", true);

                        if (!_config.Settings.User.Save(_config.Settings, _fileSystem))
                        {
                            _screenCapture.ApplicationError = true;
                        }

                        ShowSystemTrayIcon();

                        ShowInterface();

                        foreach (Trigger trigger in _formTrigger.TriggerCollection)
                        {
                            if (trigger.ActionType == TriggerActionType.ShowInterface)
                            {
                                trigger.Enable = true;
                            }
                        }

                        BuildTriggersModule();

                        if (!_formTrigger.TriggerCollection.SaveToXmlFile(_config, _fileSystem, _log))
                        {
                            _screenCapture.ApplicationError = true;
                        }
                    }

                    // -hide
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_HIDE))
                    {
                        _config.Settings.User.SetValueByKey("ShowSystemTrayIcon", false);

                        if (!_config.Settings.User.Save(_config.Settings, _fileSystem))
                        {
                            _screenCapture.ApplicationError = true;
                        }

                        HideSystemTrayIcon();

                        HideInterface();

                        foreach (Trigger trigger in _formTrigger.TriggerCollection)
                        {
                            if (trigger.ActionType == TriggerActionType.ShowInterface)
                            {
                                trigger.Enable = false;
                            }
                        }

                        BuildTriggersModule();

                        if (!_formTrigger.TriggerCollection.SaveToXmlFile(_config, _fileSystem, _log))
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
    }
}
