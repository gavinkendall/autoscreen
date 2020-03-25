using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AutoScreenCapture
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// The various regular expressions used in the parsing of the command line arguments.
        /// </summary>
        private const string REGEX_COMMAND_LINE_CONFIG = "^-config=(?<ConfigFile>.+)$";

        private const string REGEX_COMMAND_LINE_INITIAL = "^-initial$";

        private const string REGEX_COMMAND_LINE_LIMIT = @"^-limit=(?<Limit>\d{1,7})$";

        private const string REGEX_COMMAND_LINE_STOPAT = @"^-stopat=(?<Hours>\d{2}):(?<Minutes>\d{2}):(?<Seconds>\d{2})$";

        private const string REGEX_COMMAND_LINE_STARTAT = @"^-startat=(?<Hours>\d{2}):(?<Minutes>\d{2}):(?<Seconds>\d{2})$";

        private const string REGEX_COMMAND_LINE_INTERVAL = @"^-interval=(?<Hours>\d{2}):(?<Minutes>\d{2}):(?<Seconds>\d{2})\.(?<Milliseconds>\d{3})$";

        private const string REGEX_COMMAND_LINE_PASSPHRASE = "^-passphrase=(?<Passphrase>.+)$";

        private const string REGEX_COMMAND_LINE_HIDE_SYSTEM_TRAY_ICON = "^-hideSystemTrayIcon$";

        private const string REGEX_COMMAND_LINE_LOG = "^-log";

        private const string REGEX_COMMAND_LINE_DEBUG = "^-debug";

        /// <summary>
        /// Parses the command line and processes the commands the user has chosen from the command line.
        /// </summary>
        /// <param name="args"></param>
        private void ParseCommandLineArguments(string[] args)
        {
            try
            {
                bool scheduledStart = false;

                checkBoxInitialScreenshot.Checked = false;

                checkBoxCaptureLimit.Checked = false;
                numericUpDownCaptureLimit.Value = CAPTURE_LIMIT_MIN;

                numericUpDownHoursInterval.Value = 0;
                numericUpDownMinutesInterval.Value = CAPTURE_INTERVAL_DEFAULT_IN_MINUTES;
                numericUpDownSecondsInterval.Value = 0;
                numericUpDownMillisecondsInterval.Value = 0;

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

                    // This is going to be changed so that a start time and stop time is used by a new Schedule instead.
                    //if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_STARTAT))
                    //{
                    //    int hours = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_STARTAT).Groups["Hours"].Value);
                    //    int minutes = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_STARTAT).Groups["Minutes"].Value);
                    //    int seconds = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_STARTAT).Groups["Seconds"].Value);
                    //}

                    //if (Regex.IsMatch(arg, REGEX_COMMAND_LINE_STOPAT))
                    //{
                    //    int hours = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_STOPAT).Groups["Hours"].Value);
                    //    int minutes = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_STOPAT).Groups["Minutes"].Value);
                    //    int seconds = Convert.ToInt32(Regex.Match(arg, REGEX_COMMAND_LINE_STOPAT).Groups["Seconds"].Value);
                    //}

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

                InitializeThreads();

                if (!scheduledStart)
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
