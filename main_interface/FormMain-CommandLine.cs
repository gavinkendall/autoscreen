namespace AutoScreenCapture
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Net;
    using System.Net.Mail;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using System.Collections.Generic;
    using AutoScreenCapture.Properties;

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
                #region Default Values for Command Line Arguments

                checkBoxInitialScreenshot.Checked = false;

                checkBoxCaptureLimit.Checked = false;
                numericUpDownCaptureLimit.Value = CAPTURE_LIMIT_MIN;

                numericUpDownHoursInterval.Value = 0;
                numericUpDownMinutesInterval.Value = CAPTURE_INTERVAL_DEFAULT_IN_MINUTES;
                numericUpDownSecondsInterval.Value = 0;
                numericUpDownMillisecondsInterval.Value = 0;

                checkBoxScheduleStopAt.Checked = false;
                checkBoxScheduleStartAt.Checked = false;
                checkBoxScheduleOnTheseDays.Checked = false;

                #endregion Default Values for Command Line Arguments

                #region Command Line Argument Parsing

                ScreenCapture.RunningFromCommandLine = true;

                // Because ordering is important I want to make sure that we pick up the configuration file first.
                // This will avoid scenarios like "autoscreen.exe -debug -config" creating all the default folders
                // and files (thanks to -debug being the first argument) before -config is parsed.
                foreach (string arg in args)
                {
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_CONFIG))
                    {
                        string configFile = Regex.Match(arg, REGEX_COMMAND_LINE_CONFIG).Groups["ConfigFile"].Value;

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

                LoadSettings();

                // Now that everything is configured according to the config files we can go ahead and run normally.
                foreach (string arg in args)
                {
                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_LOG))
                    {
                        Log.Enabled = true;

                        if (string.IsNullOrEmpty(FileSystem.ApplicationSettingsFile))
                        {
                            Config.Load();

                            Settings.Initialize();
                        }

                        Settings.Application.GetByKey("Logging", defaultValue: false).Value = true;
                        Settings.Application.Save();
                    }

                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_DEBUG))
                    {
                        Log.DebugMode = true;

                        if (string.IsNullOrEmpty(FileSystem.ApplicationSettingsFile))
                        {
                            Config.Load();

                            Settings.Initialize();
                        }

                        Settings.Application.GetByKey("DebugMode", defaultValue: false).Value = true;
                        Settings.Application.Save();
                    }

                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_INITIAL))
                    {
                        checkBoxInitialScreenshot.Checked = true;
                    }

                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_LIMIT))
                    {
                        int cmdLimit = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_LIMIT).Groups["Limit"].Value);

                        if (cmdLimit >= CAPTURE_LIMIT_MIN && cmdLimit <= CAPTURE_LIMIT_MAX)
                        {
                            numericUpDownCaptureLimit.Value = cmdLimit;
                            checkBoxCaptureLimit.Checked = true;
                        }
                    }

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
                    }

                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_STARTAT))
                    {
                        int hours = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_STARTAT).Groups["Hours"].Value);
                        int minutes = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_STARTAT).Groups["Minutes"].Value);
                        int seconds = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_STARTAT).Groups["Seconds"].Value);

                        dateTimePickerScheduleStartAt.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hours, minutes, seconds);

                        checkBoxScheduleStartAt.Checked = true;
                    }

                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_STOPAT))
                    {
                        int hours = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_STOPAT).Groups["Hours"].Value);
                        int minutes = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_STOPAT).Groups["Minutes"].Value);
                        int seconds = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_STOPAT).Groups["Seconds"].Value);

                        dateTimePickerScheduleStopAt.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hours, minutes, seconds);

                        checkBoxScheduleStopAt.Checked = true;
                    }

                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_PASSPHRASE))
                    {
                        string passphrase = Regex.Match(arg, REGEX_COMMAND_LINE_PASSPHRASE).Groups["Passphrase"].Value;

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

                    if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_HIDE_SYSTEM_TRAY_ICON))
                    {
                        Settings.User.GetByKey("BoolShowSystemTrayIcon", defaultValue: true).Value = false;
                        Settings.User.Save();

                        notifyIcon.Visible = false;
                    }
                }

                #endregion Command Line Argument Parsing

                InitializeThreads();

                if (!checkBoxScheduleStartAt.Checked)
                {
                    StartScreenCapture();
                }
            }
            catch (Exception ex)
            {
                Log.Write("FormMain::ParseCommandLine", ex);
            }
        }
    }
}
