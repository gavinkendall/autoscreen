//-----------------------------------------------------------------------
// <copyright file="FormMain.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
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

    /// <summary>
    /// The application's main window.
    /// </summary>
    public partial class FormMain : Form
    {
        private FormEditor formEditor = new FormEditor();
        private FormTrigger formTrigger = new FormTrigger();
        private FormRegion formRegion = new FormRegion();
        private FormScreen formScreen = new FormScreen();
        private FormEnterPassphrase formEnterPassphrase = new FormEnterPassphrase();

        private ScreenCapture _screenCapture;
        private ImageFormatCollection _imageFormatCollection;
        private MacroTagCollection _macroTagCollection;
        private ScreenshotCollection _screenshotCollection;

        /// <summary>
        /// Threads for background operations.
        /// </summary>
        private BackgroundWorker runScreenshotSearchThread = null;

        private BackgroundWorker runDateSearchThread = null;

        private BackgroundWorker runDeleteSlidesThread = null;

        private BackgroundWorker runFilterSearchThread = null;

        private BackgroundWorker runSaveScreenshotsThread = null;

        /// <summary>
        /// Delegates for the threads.
        /// </summary>
        private delegate void RunSlideSearchDelegate(DoWorkEventArgs e);

        private delegate void RunDateSearchDelegate(DoWorkEventArgs e);

        private delegate void RunTitleSearchDelegate(DoWorkEventArgs e);

        /// <summary>
        /// Default settings used by the command line parser.
        /// </summary>
        private const int CAPTURE_LIMIT_MIN = 0;

        private const int CAPTURE_LIMIT_MAX = 9999;
        private const int CAPTURE_INTERVAL_DEFAULT_IN_MINUTES = 1;

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
        /// Constructor for the main form. Arguments from the command line can be passed to it.
        /// </summary>
        /// <param name="args">Arguments from the command line</param>
        public FormMain(string[] args)
        {
            InitializeComponent();

            if (args.Length > 0)
            {
                ParseCommandLineArguments(args);
            }
            else
            {
                Config.Load();

                Settings.Initialize();

                LoadSettings();

                InitializeThreads();
            }

            Text = (string)Settings.Application.GetByKey("Name", defaultValue: Settings.ApplicationName).Value;

            // Get rid of the old "slides" directory that may still remain from an old version of the application.
            DeleteSlides();
        }

        /// <summary>
        /// When this form loads we'll need to delete slides and then search for dates and slides.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            SearchFilterValues();
            SearchDates();
            SearchScreenshots();

            Log.Write("Running triggers of condition type ApplicationStartup");
            RunTriggersOfConditionType(TriggerConditionType.ApplicationStartup);
        }

        private void InitializeThreads()
        {
            Log.Write("Initializing threads");

            runDeleteSlidesThread = new BackgroundWorker
            {
                WorkerReportsProgress = false,
                WorkerSupportsCancellation = true
            };
            runDeleteSlidesThread.DoWork += new DoWorkEventHandler(DoWork_runDeleteSlidesThread);

            runDateSearchThread = new BackgroundWorker
            {
                WorkerReportsProgress = false,
                WorkerSupportsCancellation = true
            };
            runDateSearchThread.DoWork += new DoWorkEventHandler(DoWork_runDateSearchThread);

            runScreenshotSearchThread = new BackgroundWorker
            {
                WorkerReportsProgress = false,
                WorkerSupportsCancellation = true
            };
            runScreenshotSearchThread.DoWork += new DoWorkEventHandler(DoWork_runScreenshotSearchThread);

            runFilterSearchThread = new BackgroundWorker
            {
                WorkerReportsProgress = false,
                WorkerSupportsCancellation = true
            };
            runFilterSearchThread.DoWork += new DoWorkEventHandler(DoWork_runFilterSearchThread);

            runSaveScreenshotsThread = new BackgroundWorker
            {
                WorkerReportsProgress = false,
                WorkerSupportsCancellation = true
            };
            runSaveScreenshotsThread.DoWork += new DoWorkEventHandler(DoWork_runSaveScreenshotsThread);
        }

        /// <summary>
        /// Loads the user's saved settings.
        /// </summary>
        private void LoadSettings()
        {
            try
            {
                Log.Write("*** Welcome to " + Settings.ApplicationName + " " + Settings.ApplicationVersion + " ***");
                Log.Write("Starting application");
                Log.Write("At this point the application should be able to run normally");
                Log.Write("but it would be a good idea to check what we found in your autoscreen.conf file");
                Log.Write("Your autoscreen.conf file is \"" + FileSystem.ConfigFile + "\"");
                Log.Write("The name and location of it can be changed with the -config command line argument:");
                Log.Write("autoscreen.exe -config=C:\\MyAutoScreenCapture.conf");
                Log.Write("Checking what we loaded from your autoscreen.conf file ...");
                Log.Write("ApplicationSettingsFile=" + FileSystem.ApplicationSettingsFile);
                Log.Write("UserSettingsFile=" + FileSystem.UserSettingsFile);
                Log.Write("DebugFolder=" + FileSystem.DebugFolder);
                Log.Write("LogsFolder=" + FileSystem.LogsFolder);
                Log.Write("ScreenshotsFolder=" + FileSystem.ScreenshotsFolder);
                Log.Write("ScreenshotsFile=" + FileSystem.ScreenshotsFile);
                Log.Write("TriggersFile=" + FileSystem.TriggersFile);
                Log.Write("ScreensFile=" + FileSystem.ScreensFile);
                Log.Write("RegionsFile=" + FileSystem.RegionsFile);
                Log.Write("EditorsFile=" + FileSystem.EditorsFile);

                Log.Write("It looks like I successfully parsed your \"" + FileSystem.ConfigFile + "\" file.");
                Log.Write("I'm now going to attempt to load your personal settings and any screenshots you have taken.");

                Log.Write("Loading user settings");
                Settings.User.Load();
                Log.Write("User settings loaded");

                Log.Write("Attempting upgrade of user settings from old version of application (if needed)");
                Settings.User.Upgrade();

                Log.Write("Initializing screen capture");
                _screenCapture = new ScreenCapture();

                Log.Write("Initializing image format collection");
                _imageFormatCollection = new ImageFormatCollection();

                Log.Write("Initializing macro tag collection");
                _macroTagCollection = new MacroTagCollection();

                Log.Write("Initializing editor collection");
                formEditor.EditorCollection.Load();
                Log.Write("Number of editors loaded = " + formEditor.EditorCollection.Count);

                Log.Write("Initializing trigger collection");
                formTrigger.TriggerCollection.Load();
                Log.Write("Number of triggers loaded = " + formTrigger.TriggerCollection.Count);

                Log.Write("Initializing region collection");
                formRegion.RegionCollection.Load(_imageFormatCollection);
                Log.Write("Number of regions loaded = " + formRegion.RegionCollection.Count);

                Log.Write("Initializing screen collection");
                formScreen.ScreenCollection.Load(_imageFormatCollection);
                Log.Write("Number of screens loaded = " + formScreen.ScreenCollection.Count);

                Log.Write("Building screens module");
                BuildScreensModule();

                Log.Write("Building editors module");
                BuildEditorsModule();

                Log.Write("Building triggers module");
                BuildTriggersModule();

                Log.Write("Building regions module");
                BuildRegionsModule();

                Log.Write("Building screenshot preview context menu");
                BuildScreenshotPreviewContextualMenu();

                Log.Write("Building view tab pages");
                BuildViewTabPages();

                Log.Write("Initializing screenshot collection");
                _screenshotCollection = new ScreenshotCollection();

                Log.Write("Loading screenshots into the screenshot collection to generate a history of what was captured");
                _screenshotCollection.Load(_imageFormatCollection, formScreen.ScreenCollection, formRegion.RegionCollection);

                int screenCaptureInterval = Convert.ToInt32(Settings.User.GetByKey("IntScreenCaptureInterval", defaultValue: 60000).Value);
                Log.Write("IntScreenCaptureInterval = " + screenCaptureInterval);

                if (screenCaptureInterval == 0)
                {
                    screenCaptureInterval = 60000;
                    Log.Write("WARNING: Screen capture interval was found to be 0 so 60,000 milliseconds (or 1 minute) is being used as the default value");
                }

                Log.Write("Assigning screen capture interval value to its appropriate hour, minute, second, and millisecond variables");

                decimal screenCaptureIntervalHours = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(screenCaptureInterval)).Hours);
                Log.Write("Hours = " + screenCaptureIntervalHours);

                decimal screenCaptureIntervalMinutes = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(screenCaptureInterval)).Minutes);
                Log.Write("Minutes = " + screenCaptureIntervalMinutes);

                decimal screenCaptureIntervalSeconds = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(screenCaptureInterval)).Seconds);
                Log.Write("Seconds = " + screenCaptureIntervalSeconds);

                decimal screenCaptureIntervalMilliseconds = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(screenCaptureInterval)).Milliseconds);
                Log.Write("Milliseconds = " + screenCaptureIntervalMilliseconds);

                numericUpDownHoursInterval.Value = screenCaptureIntervalHours;
                numericUpDownMinutesInterval.Value = screenCaptureIntervalMinutes;
                numericUpDownSecondsInterval.Value = screenCaptureIntervalSeconds;
                numericUpDownMillisecondsInterval.Value = screenCaptureIntervalMilliseconds;

                numericUpDownCaptureLimit.Value = Convert.ToInt32(Settings.User.GetByKey("IntCaptureLimit", defaultValue: 0).Value);
                Log.Write("IntCaptureLimit = " + numericUpDownCaptureLimit.Value);

                checkBoxCaptureLimit.Checked = Convert.ToBoolean(Settings.User.GetByKey("BoolCaptureLimit", defaultValue: false).Value);
                Log.Write("BoolCaptureLimit = " + checkBoxCaptureLimit.Checked);

                checkBoxInitialScreenshot.Checked = Convert.ToBoolean(Settings.User.GetByKey("BoolTakeInitialScreenshot", defaultValue: false).Value);
                Log.Write("BoolTakeInitialScreenshot = " + checkBoxInitialScreenshot.Checked);

                notifyIcon.Visible = Convert.ToBoolean(Settings.User.GetByKey("BoolShowSystemTrayIcon", defaultValue: true).Value);
                Log.Write("BoolShowSystemTrayIcon = " + notifyIcon.Visible);
                checkBoxScheduleStopAt.Checked = Convert.ToBoolean(Settings.User.GetByKey("BoolCaptureStopAt", defaultValue: false).Value);
                Log.Write("BoolCaptureStopAt = " + checkBoxScheduleStopAt.Checked);

                checkBoxScheduleStartAt.Checked = Convert.ToBoolean(Settings.User.GetByKey("BoolCaptureStartAt", defaultValue: false).Value);
                Log.Write("BoolCaptureStartAt = " + checkBoxScheduleStartAt.Checked);

                checkBoxSunday.Checked = Convert.ToBoolean(Settings.User.GetByKey("BoolCaptureOnSunday", defaultValue: false).Value);
                Log.Write("BoolCaptureOnSunday = " + checkBoxSunday.Checked);

                checkBoxMonday.Checked = Convert.ToBoolean(Settings.User.GetByKey("BoolCaptureOnMonday", defaultValue: false).Value);
                Log.Write("BoolCaptureOnMonday = " + checkBoxMonday.Checked);

                checkBoxTuesday.Checked = Convert.ToBoolean(Settings.User.GetByKey("BoolCaptureOnTuesday", defaultValue: false).Value);
                Log.Write("BoolCaptureOnTuesday = " + checkBoxTuesday.Checked);

                checkBoxWednesday.Checked = Convert.ToBoolean(Settings.User.GetByKey("BoolCaptureOnWednesday", defaultValue: false).Value);
                Log.Write("BoolCaptureOnWednesday = " + checkBoxWednesday.Checked);

                checkBoxThursday.Checked = Convert.ToBoolean(Settings.User.GetByKey("BoolCaptureOnThursday", defaultValue: false).Value);
                Log.Write("BoolCaptureOnThursday = " + checkBoxThursday.Checked);

                checkBoxFriday.Checked = Convert.ToBoolean(Settings.User.GetByKey("BoolCaptureOnFriday", defaultValue: false).Value);
                Log.Write("BoolCaptureOnFriday = " + checkBoxFriday.Checked);

                checkBoxSaturday.Checked = Convert.ToBoolean(Settings.User.GetByKey("BoolCaptureOnSaturday", defaultValue: false).Value);
                Log.Write("BoolCaptureOnSaturday = " + checkBoxSaturday.Checked);

                checkBoxScheduleOnTheseDays.Checked = Convert.ToBoolean(Settings.User.GetByKey("BoolCaptureOnTheseDays", defaultValue: false).Value);
                Log.Write("BoolCaptureOnTheseDays = " + checkBoxScheduleOnTheseDays.Checked);

                dateTimePickerScheduleStartAt.Value = DateTime.Parse(Settings.User.GetByKey("DateTimeCaptureStartAt", defaultValue: new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0)).Value.ToString());
                Log.Write("DateTimeCaptureStartAt = " + dateTimePickerScheduleStartAt.Value);

                dateTimePickerScheduleStopAt.Value = DateTime.Parse(Settings.User.GetByKey("DateTimeCaptureStopAt", defaultValue: new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 0, 0)).Value.ToString());
                Log.Write("DateTimeCaptureStopAt = " + dateTimePickerScheduleStopAt.Value);

                numericUpDownKeepScreenshotsForDays.Value = Convert.ToDecimal(Settings.User.GetByKey("IntKeepScreenshotsForDays", defaultValue: 30).Value);
                Log.Write("IntKeepScreenshotsForDays = " + numericUpDownKeepScreenshotsForDays.Value);

                comboBoxScreenshotLabel.Text = Settings.User.GetByKey("StringScreenshotLabel", defaultValue: string.Empty).Value.ToString();
                Log.Write("StringScreenshotLabel = " + comboBoxScreenshotLabel.Text);

                checkBoxScreenshotLabel.Checked = Convert.ToBoolean(Settings.User.GetByKey("BoolApplyScreenshotLabel", defaultValue: false).Value);

                EnableStartCapture();

                CaptureLimitCheck();
            }
            catch (Exception ex)
            {
                Log.Write("FormMain::LoadSettings", ex);
            }
        }

        /// <summary>
        /// When this form is closing we can either exit the application or just close this window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown)
            {
                DisableStopCapture();
                EnableStartCapture();

                _screenCapture.Count = 0;
                _screenCapture.Running = false;

                notifyIcon.Icon = Resources.autoscreen;

                // Hide the system tray icon.
                notifyIcon.Visible = false;

                Log.Write("Hiding interface on forced application exit because Windows is shutting down");
                HideInterface();

                Log.Write("Saving screenshots on forced application exit because Windows is shutting down");
                _screenshotCollection.Save((int)numericUpDownKeepScreenshotsForDays.Value);

                if (runDateSearchThread != null && runDateSearchThread.IsBusy)
                {
                    runDateSearchThread.CancelAsync();
                }

                if (runScreenshotSearchThread != null && runScreenshotSearchThread.IsBusy)
                {
                    runScreenshotSearchThread.CancelAsync();
                }

                Log.Write("Bye!");

                // Exit.
                Environment.Exit(0);
            }
            else
            {
                Log.Write("Running triggers of condition type InterfaceClosing");
                RunTriggersOfConditionType(TriggerConditionType.InterfaceClosing);

                // If there isn't a Trigger for "InterfaceClosing" that performs an action
                // then make sure we cancel this event so that nothing happens. We want the user
                // to use a Trigger, and decide what they want to do, when closing the interface window.
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Searches for dates. They should be in the format yyyy-mm-dd.
        /// </summary>
        private void SearchDates()
        {
            Log.Write("Searching for dates");

            if (runDateSearchThread != null && !runDateSearchThread.IsBusy)
            {
                runDateSearchThread.RunWorkerAsync();
            }
        }

        private void DeleteSlides()
        {
            Log.Write("Deleting slides directory from old version of application (if needed)");

            if (runDeleteSlidesThread != null && !runDeleteSlidesThread.IsBusy)
            {
                runDeleteSlidesThread.RunWorkerAsync();
            }
        }

        private void SaveScreenshots()
        {
            if (runSaveScreenshotsThread != null && !runSaveScreenshotsThread.IsBusy)
            {
                runSaveScreenshotsThread.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Searches for screenshots.
        /// </summary>
        private void SearchScreenshots()
        {
            Log.Write("Searching for screenshots");

            Slideshow.Index = 0;
            Slideshow.Count = 0;

            listBoxScreenshots.BeginUpdate();

            listBoxScreenshots.DataSource = null;

            if (runScreenshotSearchThread != null && !runScreenshotSearchThread.IsBusy)
            {
                runScreenshotSearchThread.RunWorkerAsync();
            }

            listBoxScreenshots.EndUpdate();
        }

        private void SearchFilterValues()
        {
            comboBoxFilterValue.BeginUpdate();

            if (runFilterSearchThread != null && !runFilterSearchThread.IsBusy)
            {
                runFilterSearchThread.RunWorkerAsync();
            }

            comboBoxFilterValue.EndUpdate();
        }

        /// <summary>
        /// This thread is responsible for finding slides.
        /// </summary>
        /// <param name="e"></param>
        private void RunScreenshotSearch(DoWorkEventArgs e)
        {
            if (listBoxScreenshots.InvokeRequired)
            {
                listBoxScreenshots.Invoke(new RunSlideSearchDelegate(RunScreenshotSearch), new object[] {e});
            }
            else
            {
                listBoxScreenshots.DisplayMember = "Value";
                listBoxScreenshots.ValueMember = "Name";
                listBoxScreenshots.DataSource = _screenshotCollection.GetSlides(comboBoxFilterType.Text, comboBoxFilterValue.Text, monthCalendar.SelectionStart.ToString(MacroParser.DateFormat));

                if (listBoxScreenshots.Items.Count > 0)
                {
                    listBoxScreenshots.SelectedIndex = listBoxScreenshots.Items.Count - 1;
                }
            }
        }

        /// <summary>
        /// This thread is responsible for figuring out what days screenshots were taken.
        /// </summary>
        /// <param name="e"></param>
        private void RunDateSearch(DoWorkEventArgs e)
        {
            if (monthCalendar.InvokeRequired)
            {
                monthCalendar.Invoke(new RunDateSearchDelegate(RunDateSearch), new object[] {e});
            }
            else
            {
                List<string> dates = _screenshotCollection.GetDates(comboBoxFilterType.Text, comboBoxFilterValue.Text);

                DateTime[] boldedDates = new DateTime[dates.Count];

                for (int i = 0; i < dates.Count; i++)
                {
                    boldedDates.SetValue(ConvertDateStringToDateTime(dates[i].ToString()), i);
                }

                monthCalendar.BoldedDates = boldedDates;
            }
        }

        /// <summary>
        /// This thread is responsible for deleting all the slides remaining from an old version of the application
        /// since we no longer use slides or support the Slideshow module going forward.
        /// </summary>
        /// <param name="e"></param>
        private void RunDeleteSlides(DoWorkEventArgs e)
        {
            FileSystem.DeleteFilesInDirectory(FileSystem.SlidesFolder);
        }

        private void RunSaveScreenshots(DoWorkEventArgs e)
        {
            _screenshotCollection.Save((int)numericUpDownKeepScreenshotsForDays.Value);
        }

        private void RunFilterSearch(DoWorkEventArgs e)
        {
            if (comboBoxFilterValue.InvokeRequired)
            {
                comboBoxFilterValue.Invoke(new RunTitleSearchDelegate(RunFilterSearch), new object[] {e});
            }
            else
            {
                if (comboBoxFilterType.SelectedItem != null && !string.IsNullOrEmpty(comboBoxFilterType.Text))
                {
                    List<string> filterValueList = _screenshotCollection.GetFilterValueList(comboBoxFilterType.Text);
                    filterValueList.Add(string.Empty);
                    filterValueList.Sort();

                    comboBoxFilterValue.DataSource = filterValueList;
                }
            }
        }

        /// <summary>
        /// Saves the user's settings.
        /// </summary>
        private void SaveSettings()
        {
            try
            {
                Log.Write("Saving settings");

                Stopwatch stopwatch = new Stopwatch();

                stopwatch.Start();

                Settings.User.GetByKey("IntScreenCaptureInterval", defaultValue: 60000).Value = GetScreenCaptureInterval();
                Settings.User.GetByKey("IntCaptureLimit", defaultValue: 0).Value = numericUpDownCaptureLimit.Value;
                Settings.User.GetByKey("BoolCaptureLimit", defaultValue: false).Value = checkBoxCaptureLimit.Checked;
                Settings.User.GetByKey("BoolTakeInitialScreenshot", defaultValue: false).Value = checkBoxInitialScreenshot.Checked;
                Settings.User.GetByKey("BoolCaptureStopAt", defaultValue: false).Value = checkBoxScheduleStopAt.Checked;
                Settings.User.GetByKey("BoolCaptureStartAt", defaultValue: false).Value = checkBoxScheduleStartAt.Checked;
                Settings.User.GetByKey("BoolCaptureOnSunday", defaultValue: false).Value = checkBoxSunday.Checked;
                Settings.User.GetByKey("BoolCaptureOnMonday", defaultValue: false).Value = checkBoxMonday.Checked;
                Settings.User.GetByKey("BoolCaptureOnTuesday", defaultValue: false).Value = checkBoxTuesday.Checked;
                Settings.User.GetByKey("BoolCaptureOnWednesday", defaultValue: false).Value = checkBoxWednesday.Checked;
                Settings.User.GetByKey("BoolCaptureOnThursday", defaultValue: false).Value = checkBoxThursday.Checked;
                Settings.User.GetByKey("BoolCaptureOnFriday", defaultValue: false).Value = checkBoxFriday.Checked;
                Settings.User.GetByKey("BoolCaptureOnSaturday", defaultValue: false).Value = checkBoxSaturday.Checked;
                Settings.User.GetByKey("BoolCaptureOnTheseDays", defaultValue: false).Value = checkBoxScheduleOnTheseDays.Checked;
                Settings.User.GetByKey("DateTimeCaptureStopAt",  defaultValue: new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 0, 0)).Value = dateTimePickerScheduleStopAt.Value;
                Settings.User.GetByKey("DateTimeCaptureStartAt", defaultValue: new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0)).Value = dateTimePickerScheduleStartAt.Value;
                Settings.User.GetByKey("IntKeepScreenshotsForDays", defaultValue: 30).Value = numericUpDownKeepScreenshotsForDays.Value;
                Settings.User.GetByKey("StringScreenshotLabel", defaultValue: string.Empty).Value = comboBoxScreenshotLabel.Text;
                Settings.User.GetByKey("BoolApplyScreenshotLabel", defaultValue: false).Value = checkBoxScreenshotLabel.Checked;

                Settings.User.Save();

                Log.Write("Settings saved");

                stopwatch.Stop();

                Log.Write("It took " + stopwatch.ElapsedMilliseconds + " milliseconds to save user settings");
            }
            catch (Exception ex)
            {
                Log.Write("FormMain::SaveSettings", ex);
            }
        }

        /// <summary>
        /// Converts the string representation of a date into a DateTime object. Used by the RunDateSearch thread so we can set bolded dates in the calendar.
        /// </summary>
        /// <param name="date">A string representation of a date (such as "2019-02-06").</param>
        /// <returns>A DateTime object based on the provided date string.</returns>
        private DateTime ConvertDateStringToDateTime(string date)
        {
            return new DateTime(Convert.ToInt32(date.Substring(0, 4)), Convert.ToInt32(date.Substring(5, 2)), Convert.ToInt32(date.Substring(8, 2)));
        }

        /// <summary>
        /// Shows the interface.
        /// </summary>
        private void ShowInterface()
        {
            try
            {
                Log.Write("Showing interface");

                if (ScreenCapture.LockScreenCaptureSession && !formEnterPassphrase.Visible)
                {
                    Log.Write("Screen capture session is locked. Challenging user to enter correct passphrase to unlock");
                    formEnterPassphrase.ShowDialog(this);
                }

                // This is intentional. Do not rewrite these statements as an if/else
                // because as soon as lockScreenCaptureSession is set to false we want
                // to continue with normal functionality.
                if (!ScreenCapture.LockScreenCaptureSession)
                {
                    Settings.User.GetByKey("StringPassphrase", defaultValue: false).Value = string.Empty;
                    SaveSettings();

                    Opacity = 100;

                    SearchDates();
                    SearchScreenshots();

                    PopulateLabelList();

                    Show();

                    Visible = true;
                    ShowInTaskbar = true;

                    // If the window is mimimized then show it when the user wants to open the window.
                    if (WindowState == FormWindowState.Minimized)
                    {
                        WindowState = FormWindowState.Normal;
                    }

                    Focus();

                    Log.Write("Running triggers of condition type InterfaceShowing");
                    RunTriggersOfConditionType(TriggerConditionType.InterfaceShowing);
                }
            }
            catch (Exception ex)
            {
                Log.Write("FormMain::ShowInterface", ex);
            }
        }

        /// <summary>
        /// Hides the interface.
        /// </summary>
        private void HideInterface()
        {
            try
            {
                Log.Write("Hiding interface");

                Opacity = 0;

                Hide();
                Visible = false;
                ShowInTaskbar = false;

                Log.Write("Running triggers of condition type InterfaceHiding");
                RunTriggersOfConditionType(TriggerConditionType.InterfaceHiding);
            }
            catch (Exception ex)
            {
                Log.Write("FormMain::HideInterface", ex);
            }
        }

        /// <summary>
        /// Stops the screen capture session that's currently running.
        /// </summary>
        private void StopScreenCapture()
        {
            if (_screenCapture.Running)
            {
                Log.Write("Stopping screen capture");

                if (ScreenCapture.LockScreenCaptureSession && !formEnterPassphrase.Visible)
                {
                    Log.Write("Screen capture session is locked. Challenging user to enter correct passphrase to unlock");
                    formEnterPassphrase.ShowDialog(this);
                }

                // This is intentional. Do not rewrite these statements as an if/else
                // because as soon as lockScreenCaptureSession is set to false we want
                // to continue with normal functionality.
                if (!ScreenCapture.LockScreenCaptureSession)
                {
                    Settings.User.GetByKey("StringPassphrase", defaultValue: false).Value = string.Empty;
                    SaveSettings();

                    DisableStopCapture();
                    EnableStartCapture();

                    _screenCapture.Count = 0;
                    _screenCapture.Running = false;

                    notifyIcon.Icon = Resources.autoscreen;

                    SearchFilterValues();
                    SearchDates();

                    Log.Write("Running triggers of condition type ScreenCaptureStopped");
                    RunTriggersOfConditionType(TriggerConditionType.ScreenCaptureStopped);
                }
            }
        }

        /// <summary>
        /// Starts a screen capture session.
        /// </summary>
        private void StartScreenCapture()
        {
            int screenCaptureInterval = GetScreenCaptureInterval();
            
            if (!_screenCapture.Running && screenCaptureInterval > 0)
            {
                SaveSettings();

                // Stop the date search thread if it's busy.
                if (runDateSearchThread != null && runDateSearchThread.IsBusy)
                {
                    runDateSearchThread.CancelAsync();
                }

                // Stop the slide search thread if it's busy.
                if (runScreenshotSearchThread != null && runScreenshotSearchThread.IsBusy)
                {
                    runScreenshotSearchThread.CancelAsync();
                }

                DisableStartCapture();
                EnableStopScreenCapture();

                // Setup the properties for the screen capture class.
                _screenCapture.Delay = screenCaptureInterval;
                _screenCapture.Limit = checkBoxCaptureLimit.Checked ? (int) numericUpDownCaptureLimit.Value : 0;

                if (Settings.User.GetByKey("StringPassphrase", defaultValue: string.Empty).Value.ToString().Length > 0)
                {
                    ScreenCapture.LockScreenCaptureSession = true;
                }
                else
                {
                    ScreenCapture.LockScreenCaptureSession = false;
                }

                Log.Write("Starting screen capture");

                _screenCapture.Running = true;

                notifyIcon.Icon = Resources.autoscreen_running;

                _screenCapture.DateTimeStartCapture = DateTime.Now;

                if (checkBoxInitialScreenshot.Checked)
                {
                    Log.Write("Taking initial screenshots");

                    TakeScreenshot();
                }

                // Start taking screenshots.

                timerScreenCapture.Interval = screenCaptureInterval;

                Log.Write("Running triggers of condition type ScreenCaptureStarted");
                RunTriggersOfConditionType(TriggerConditionType.ScreenCaptureStarted);
            }
        }

        /// <summary>
        /// Whenever the user clicks on a screenshot in the list of screenshots then make sure to update the appropriate image.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectedIndexChanged_listBoxScreenshots(object sender, EventArgs e)
        {
            Slideshow.Index = listBoxScreenshots.SelectedIndex;
            Slideshow.Count = listBoxScreenshots.Items.Count;

            ShowScreenshotBySlideIndex();
        }

        private void ShowScreenshotBySlideIndex()
        {
            textBoxScreenshotTitle.Text = string.Empty;
            textBoxScreenshotFormat.Text = string.Empty;
            textBoxScreenshotWidth.Text = string.Empty;
            textBoxScreenshotHeight.Text = string.Empty;
            textBoxScreenshotDate.Text = string.Empty;
            textBoxScreenshotTime.Text = string.Empty;

            TabPage selectedTabPage = tabControlViews.SelectedTab;

            if (selectedTabPage != null)
            {
                ToolStrip toolStrip = (ToolStrip) selectedTabPage.Controls[selectedTabPage.Name + "toolStrip"];

                ToolStripTextBox toolStripTextBox = (ToolStripTextBox) toolStrip.Items[selectedTabPage.Name + "toolStripTextBoxFilename"];

                PictureBox pictureBox = (PictureBox) selectedTabPage.Controls[selectedTabPage.Name + "pictureBox"];

                Screenshot selectedScreenshot = new Screenshot();

                if (Slideshow.Index >= 0 && Slideshow.Index <= (Slideshow.Count - 1))
                {
                    Slideshow.SelectedSlide = (Slide) listBoxScreenshots.Items[Slideshow.Index];

                    if (selectedTabPage.Tag.GetType() == typeof(Screen))
                    {
                        Screen screen = (Screen) selectedTabPage.Tag;
                        selectedScreenshot = _screenshotCollection.GetScreenshot(Slideshow.SelectedSlide.Name, screen.ViewId);
                    }

                    if (selectedTabPage.Tag.GetType() == typeof(Region))
                    {
                        Region region = (Region) selectedTabPage.Tag;
                        selectedScreenshot = _screenshotCollection.GetScreenshot(Slideshow.SelectedSlide.Name, region.ViewId);
                    }
                }

                string path = selectedScreenshot.Path;

                if (!string.IsNullOrEmpty(path))
                {
                    toolStripTextBox.Text = Path.GetFileName(path);
                    toolStripTextBox.ToolTipText = path;

                    FileInfo fileInfo = new FileInfo(path);

                    if (fileInfo.Directory != null && fileInfo.Directory.Root.Exists)
                    {
                        string dirName = Path.GetDirectoryName(path);

                        if (!string.IsNullOrEmpty(dirName))
                        {
                            if (Directory.Exists(dirName) && File.Exists(path))
                            {
                                toolStripTextBox.BackColor = Color.PaleGreen;
                            }
                            else
                            {
                                toolStripTextBox.BackColor = Color.PaleVioletRed;
                                toolStripTextBox.ToolTipText = $"Could not find or access image file at path \"{path}\"";
                            }
                        }
                    }

                    pictureBox.Image = _screenCapture.GetImageByPath(path);

                    if (pictureBox.Image != null)
                    {
                        textBoxScreenshotTitle.Text = selectedScreenshot.WindowTitle;
                        textBoxScreenshotFormat.Text = selectedScreenshot.Format.Name;

                        textBoxScreenshotWidth.Text = pictureBox.Image.Width.ToString();
                        textBoxScreenshotHeight.Text = pictureBox.Image.Height.ToString();

                        textBoxScreenshotDate.Text = selectedScreenshot.Date;
                        textBoxScreenshotTime.Text = selectedScreenshot.Time;
                    }
                }
                else
                {
                    toolStripTextBox.Text = string.Empty;
                    toolStripTextBox.BackColor = Color.PaleVioletRed;
                    toolStripTextBox.ToolTipText = "Could not find or access image file";

                    pictureBox.Image = null;
                }
            }
        }

        /// <summary>
        /// Converts the given hours, minutes, seconds, and milliseconds into an aggregate milliseconds value.
        /// </summary>
        /// <param name="hours">The number of hours to be converted.</param>
        /// <param name="minutes">The number of minutes to be converted.</param>
        /// <param name="seconds">The number of seconds to be converted.</param>
        /// <param name="milliseconds">The number of milliseconds to be converted.</param>
        /// <returns></returns>
        private int ConvertIntoMilliseconds(int hours, int minutes, int seconds, int milliseconds)
        {
            return 1000 * (hours * 3600 + minutes * 60 + seconds) + milliseconds;
        }

        /// <summary>
        /// Returns the screen capture interval. This value will be used as the screen capture timer's interval property.
        /// </summary>
        /// <returns></returns>
        private int GetScreenCaptureInterval()
        {
            return ConvertIntoMilliseconds((int) numericUpDownHoursInterval.Value,
                (int) numericUpDownMinutesInterval.Value, (int) numericUpDownSecondsInterval.Value,
                (int) numericUpDownMillisecondsInterval.Value);
        }

        /// <summary>
        /// Shows the list of screenshots when a date on the calendar has been selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateSelected_monthCalendar(object sender, DateRangeEventArgs e)
        {
            ShowScreenshots();
        }

        /// <summary>
        /// Shows the list of screenshots.
        /// </summary>
        private void ShowScreenshots()
        {
            SearchScreenshots();

            if (!tabControlModules.SelectedTab.Name.Equals("tabPageScreenshots"))
            {
                tabControlModules.SelectedTab = tabControlModules.TabPages["tabPageScreenshots"];
            }

            ShowScreenshotBySlideIndex();
        }

        /// <summary>
        /// Starts a screen capture session.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_toolStripMenuItemStartScreenCapture(object sender, EventArgs e)
        {
            StartScreenCapture();
        }

        /// <summary>
        /// Stops the currently running screen capture session.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_toolStripMenuItemStopScreenCapture(object sender, EventArgs e)
        {
            StopScreenCapture();
        }

        /// <summary>
        /// Exits the application from the system tray icon menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_toolStripMenuItemExit(object sender, EventArgs e)
        {
            ExitApplication();
        }

        /// <summary>
        /// Exits the application.
        /// </summary>
        private void ExitApplication()
        {
            Log.Write("Exiting application");

            if (ScreenCapture.LockScreenCaptureSession && !formEnterPassphrase.Visible)
            {
                Log.Write("Screen capture session is locked. Challenging user to enter correct passphrase to unlock");
                formEnterPassphrase.ShowDialog(this);
            }

            // This is intentional. Do not rewrite these statements as an if/else
            // because as soon as lockScreenCaptureSession is set to false we want
            // to continue with normal functionality.
            if (!ScreenCapture.LockScreenCaptureSession)
            {
                Log.Write("Running triggers of condition type ApplicationExit");
                RunTriggersOfConditionType(TriggerConditionType.ApplicationExit);

                Settings.User.GetByKey("StringPassphrase", defaultValue: false).Value = string.Empty;
                SaveSettings();

                DisableStopCapture();
                EnableStartCapture();

                _screenCapture.Count = 0;
                _screenCapture.Running = false;

                notifyIcon.Icon = Resources.autoscreen;

                // Hide the system tray icon.
                notifyIcon.Visible = false;

                Log.Write("Hiding interface on clean application exit");
                HideInterface();

                Log.Write("Saving screenshots on clean application exit");
                _screenshotCollection.Save((int)numericUpDownKeepScreenshotsForDays.Value);

                if (runDateSearchThread != null && runDateSearchThread.IsBusy)
                {
                    runDateSearchThread.CancelAsync();
                }

                if (runScreenshotSearchThread != null && runScreenshotSearchThread.IsBusy)
                {
                    runScreenshotSearchThread.CancelAsync();
                }

                Log.Write("Bye!");

                // Exit.
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Runs the slide search thread.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoWork_runScreenshotSearchThread(object sender, DoWorkEventArgs e)
        {
            RunScreenshotSearch(e);
        }

        /// <summary>
        /// Runs the date search thread.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoWork_runDateSearchThread(object sender, DoWorkEventArgs e)
        {
            RunDateSearch(e);
        }

        /// <summary>
        /// Runs the delete slides thread.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoWork_runDeleteSlidesThread(object sender, DoWorkEventArgs e)
        {
            RunDeleteSlides(e);
        }

        private void DoWork_runSaveScreenshotsThread(object sender, DoWorkEventArgs e)
        {
            RunSaveScreenshots(e);
        }

        private void DoWork_runFilterSearchThread(object sender, DoWorkEventArgs e)
        {
            RunFilterSearch(e);
        }

        /// <summary>
        /// Figures out if the "Start Capture" controls should be enabled or disabled.
        /// </summary>
        private void EnableStartCapture()
        {
            if (GetScreenCaptureInterval() > 0)
            {
                toolStripSplitButtonStartScreenCapture.Enabled = true;
                toolStripMenuItemStartScreenCapture.Enabled = true;

                numericUpDownHoursInterval.Enabled = true;
                checkBoxInitialScreenshot.Enabled = true;
                numericUpDownMinutesInterval.Enabled = true;
                checkBoxCaptureLimit.Enabled = true;
                numericUpDownCaptureLimit.Enabled = true;
                numericUpDownSecondsInterval.Enabled = true;
                numericUpDownMillisecondsInterval.Enabled = true;
                numericUpDownKeepScreenshotsForDays.Enabled = true;

                checkBoxScheduleStartAt.Enabled = true;
                checkBoxScheduleStopAt.Enabled = true;
                checkBoxScheduleOnTheseDays.Enabled = true;
                checkBoxSunday.Enabled = true;
                checkBoxMonday.Enabled = true;
                checkBoxTuesday.Enabled = true;
                checkBoxWednesday.Enabled = true;
                checkBoxThursday.Enabled = true;
                checkBoxFriday.Enabled = true;
                checkBoxSaturday.Enabled = true;
                dateTimePickerScheduleStartAt.Enabled = true;
                dateTimePickerScheduleStopAt.Enabled = true;

                checkBoxScreenshotLabel.Enabled = true;
                comboBoxScreenshotLabel.Enabled = true;
            }
            else
            {
                DisableStartCapture();
            }
        }

        /// <summary>
        /// Enables the "Stop Capture" controls.
        /// </summary>
        private void EnableStopScreenCapture()
        {
            toolStripSplitButtonStopScreenCapture.Enabled = true;
            toolStripMenuItemStopScreenCapture.Enabled = true;

            numericUpDownHoursInterval.Enabled = false;
            checkBoxInitialScreenshot.Enabled = false;
            numericUpDownMinutesInterval.Enabled = false;
            checkBoxCaptureLimit.Enabled = false;
            numericUpDownCaptureLimit.Enabled = false;
            numericUpDownSecondsInterval.Enabled = false;
            numericUpDownMillisecondsInterval.Enabled = false;
            numericUpDownKeepScreenshotsForDays.Enabled = false;

            checkBoxScheduleStartAt.Enabled = false;
            checkBoxScheduleStopAt.Enabled = false;
            checkBoxScheduleOnTheseDays.Enabled = false;
            checkBoxSunday.Enabled = false;
            checkBoxMonday.Enabled = false;
            checkBoxTuesday.Enabled = false;
            checkBoxWednesday.Enabled = false;
            checkBoxThursday.Enabled = false;
            checkBoxFriday.Enabled = false;
            checkBoxSaturday.Enabled = false;
            dateTimePickerScheduleStartAt.Enabled = false;
            dateTimePickerScheduleStopAt.Enabled = false;

            checkBoxScreenshotLabel.Enabled = false;
            comboBoxScreenshotLabel.Enabled = false;
        }

        /// <summary>
        /// Disables the "Stop Capture" controls.
        /// </summary>
        private void DisableStopCapture()
        {
            toolStripSplitButtonStopScreenCapture.Enabled = false;
            toolStripMenuItemStopScreenCapture.Enabled = false;
        }

        /// <summary>
        /// Disables the "Start Capture" controls.
        /// </summary>
        private void DisableStartCapture()
        {
            toolStripSplitButtonStartScreenCapture.Enabled = false;
            toolStripMenuItemStartScreenCapture.Enabled = false;
        }

        /// <summary>
        /// Shows the interface.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_toolStripMenuItemShowInterface(object sender, EventArgs e)
        {
            ShowInterface();
        }

        /// <summary>
        /// Hides the interface.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_toolStripMenuItemHideInterface(object sender, EventArgs e)
        {
            HideInterface();
        }

        /// <summary>
        /// Shows the "About" window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_toolStripMenuItemAbout(object sender, EventArgs e)
        {
            MessageBox.Show(
                Settings.Application.GetByKey("Name", defaultValue: Settings.ApplicationName).Value + " " +
                Settings.Application.GetByKey("Version", defaultValue: Settings.ApplicationVersion).Value +
                " (\"" + Settings.ApplicationCodename + "\")\nDeveloped by Gavin Kendall (2008 - 2020)", "About",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        /// <summary>
        /// Opens Windows Explorer to show the location of the selected screenshot.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_toolStripMenuItemShowScreenshotLocation(object sender, EventArgs e)
        {
            if (listBoxScreenshots.SelectedIndex > -1)
            {
                Screenshot selectedScreenshot = new Screenshot();

                if (tabControlViews.SelectedTab.Tag.GetType() == typeof(Screen))
                {
                    Screen screen = (Screen) tabControlViews.SelectedTab.Tag;
                    selectedScreenshot =
                        _screenshotCollection.GetScreenshot(Slideshow.SelectedSlide.Name, screen.ViewId);
                }

                if (tabControlViews.SelectedTab.Tag.GetType() == typeof(Region))
                {
                    Region region = (Region) tabControlViews.SelectedTab.Tag;
                    selectedScreenshot =
                        _screenshotCollection.GetScreenshot(Slideshow.SelectedSlide.Name, region.ViewId);
                }

                if (selectedScreenshot != null && !string.IsNullOrEmpty(selectedScreenshot.Path) &&
                    File.Exists(selectedScreenshot.Path))
                {
                    Process.Start(FileSystem.FileManager, "/select,\"" + selectedScreenshot.Path + "\"");
                }
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

        /// <summary>
        /// Builds the sub-menus for the contextual menu that appears when the user right-clicks on the selected screenshot.
        /// </summary>
        private void BuildScreenshotPreviewContextualMenu()
        {
            contextMenuStripScreenshotPreview.Items.Clear();

            ToolStripMenuItem showScreenshotLocationToolStripItem = new ToolStripMenuItem("Show Screenshot Location");
            showScreenshotLocationToolStripItem.Click +=
                new EventHandler(Click_toolStripMenuItemShowScreenshotLocation);

            ToolStripMenuItem addNewEditorToolStripItem = new ToolStripMenuItem("Add New Editor ...");
            addNewEditorToolStripItem.Click += new EventHandler(Click_addEditorToolStripMenuItem);

            contextMenuStripScreenshotPreview.Items.Add(showScreenshotLocationToolStripItem);
            contextMenuStripScreenshotPreview.Items.Add(new ToolStripSeparator());
            contextMenuStripScreenshotPreview.Items.Add(addNewEditorToolStripItem);

            foreach (Editor editor in formEditor.EditorCollection)
            {
                if (editor != null && File.Exists(editor.Application))
                {
                    // ****************** EDITORS LIST IN CONTEXTUAL MENU *************************
                    // Add the Editor to the screenshot preview contextual menu.

                    contextMenuStripScreenshotPreview.Items.Add(editor.Name,
                        Icon.ExtractAssociatedIcon(editor.Application).ToBitmap(), Click_runEditor);
                    // ****************************************************************************
                }
            }
        }

        private void BuildEditorsModule()
        {
            int xPosEditor = 5;
            int yPosEditor = 3;

            const int EDITOR_HEIGHT = 20;
            const int CHECKBOX_WIDTH = 20;
            const int CHECKBOX_HEIGHT = 20;
            const int X_POS_EDITOR_ICON = 20;
            const int BIG_BUTTON_WIDTH = 205;
            const int BIG_BUTTON_HEIGHT = 25;
            const int SMALL_IMAGE_WIDTH = 20;
            const int SMALL_IMAGE_HEIGHT = 20;
            const int SMALL_BUTTON_WIDTH = 27;
            const int SMALL_BUTTON_HEIGHT = 20;
            const int X_POS_EDITOR_TEXTBOX = 48;
            const int X_POS_EDITOR_BUTTON = 178;
            const int EDITOR_TEXTBOX_WIDTH = 125;
            const int Y_POS_EDITOR_INCREMENT = 23;
            const int EDITOR_TEXTBOX_MAX_LENGTH = 50;

            const string EDIT_BUTTON_TEXT = "...";

            tabPageEditors.Controls.Clear();

            // The button for adding a new Editor.
            Button buttonAddNewEditor = new Button
            {
                Size = new Size(BIG_BUTTON_WIDTH, BIG_BUTTON_HEIGHT),
                Location = new Point(xPosEditor, yPosEditor),
                Text = "Add New Editor ...",
                TabStop = false
            };
            buttonAddNewEditor.Click += new EventHandler(Click_addEditorToolStripMenuItem);
            tabPageEditors.Controls.Add(buttonAddNewEditor);

            // Move down and then add the "Remove Selected Editors" button.
            yPosEditor += 27;

            Button buttonRemoveSelectedEditors = new Button
            {
                Size = new Size(BIG_BUTTON_WIDTH, BIG_BUTTON_HEIGHT),
                Location = new Point(xPosEditor, yPosEditor),
                Text = "Remove Selected Editors",
                TabStop = false
            };
            buttonRemoveSelectedEditors.Click += new EventHandler(Click_removeSelectedEditors);
            tabPageEditors.Controls.Add(buttonRemoveSelectedEditors);

            // Move down a bit so we can start populating the Editors tab page with a list of Editors.
            yPosEditor += 28;

            foreach (Editor editor in formEditor.EditorCollection)
            {
                if (editor != null && File.Exists(editor.Application))
                {
                    // ****************** EDITORS LIST IN EDITORS TAB PAGE ************************
                    // Add the Editor to the list of Editors in the Editors tab page.

                    // Add a checkbox so that the user has the ability to remove the selected Editor.
                    CheckBox checkboxEditor = new CheckBox
                    {
                        Size = new Size(CHECKBOX_WIDTH, CHECKBOX_HEIGHT),
                        Location = new Point(xPosEditor, yPosEditor),
                        Tag = editor,
                        TabStop = false
                    };
                    tabPageEditors.Controls.Add(checkboxEditor);

                    // Add an image showing the application icon of the Editor.
                    PictureBox pictureBoxEditor = new PictureBox
                    {
                        Size = new Size(SMALL_IMAGE_WIDTH, SMALL_IMAGE_HEIGHT),
                        Location = new Point(xPosEditor + X_POS_EDITOR_ICON, yPosEditor),
                        Image = Icon.ExtractAssociatedIcon(editor.Application).ToBitmap(),
                        SizeMode = PictureBoxSizeMode.StretchImage
                    };
                    tabPageEditors.Controls.Add(pictureBoxEditor);

                    // Add a read-only text box showing the application name of the Editor.
                    TextBox textBoxEditor = new TextBox
                    {
                        Width = EDITOR_TEXTBOX_WIDTH,
                        Height = EDITOR_HEIGHT,
                        MaxLength = EDITOR_TEXTBOX_MAX_LENGTH,
                        Location = new Point(xPosEditor + X_POS_EDITOR_TEXTBOX, yPosEditor),
                        Text = editor.Name,
                        ReadOnly = true,
                        TabStop = false
                    };
                    tabPageEditors.Controls.Add(textBoxEditor);

                    // Add a button so that the user can change the Editor.
                    Button buttonChangeEditor = new Button
                    {
                        Size = new Size(SMALL_BUTTON_WIDTH, SMALL_BUTTON_HEIGHT),
                        Location = new Point(xPosEditor + X_POS_EDITOR_BUTTON, yPosEditor),
                        Text = EDIT_BUTTON_TEXT,
                        Tag = editor,
                        TabStop = false
                    };
                    buttonChangeEditor.Click += new EventHandler(Click_buttonChangeEditor);
                    tabPageEditors.Controls.Add(buttonChangeEditor);

                    // Move down the Editors tab page so we're ready to loop around again and add the next Editor to it.
                    yPosEditor += Y_POS_EDITOR_INCREMENT;
                    // ****************************************************************************
                }
            }
        }

        private void BuildTriggersModule()
        {
            int xPosTrigger = 5;
            int yPosTrigger = 3;

            const int TRIGGER_HEIGHT = 20;
            const int CHECKBOX_WIDTH = 20;
            const int CHECKBOX_HEIGHT = 20;
            const int BIG_BUTTON_WIDTH = 205;
            const int BIG_BUTTON_HEIGHT = 25;
            const int SMALL_BUTTON_WIDTH = 27;
            const int SMALL_BUTTON_HEIGHT = 20;
            const int X_POS_TRIGGER_TEXTBOX = 20;
            const int X_POS_TRIGGER_BUTTON = 178;
            const int TRIGGER_TEXTBOX_WIDTH = 153;
            const int Y_POS_TRIGGER_INCREMENT = 23;
            const int TRIGGER_TEXTBOX_MAX_LENGTH = 50;

            const string EDIT_BUTTON_TEXT = "...";

            tabPageTriggers.Controls.Clear();

            // The button for adding a new Trigger.
            Button buttonAddNewTrigger = new Button
            {
                Size = new Size(BIG_BUTTON_WIDTH, BIG_BUTTON_HEIGHT),
                Location = new Point(xPosTrigger, yPosTrigger),
                Text = "Add New Trigger ...",
                TabStop = false
            };
            buttonAddNewTrigger.Click += new EventHandler(Click_addTrigger);
            tabPageTriggers.Controls.Add(buttonAddNewTrigger);

            // Move down and then add the "Remove Selected Triggers" button.
            yPosTrigger += 27;

            Button buttonRemoveSelectedTriggers = new Button
            {
                Size = new Size(BIG_BUTTON_WIDTH, BIG_BUTTON_HEIGHT),
                Location = new Point(xPosTrigger, yPosTrigger),
                Text = "Remove Selected Triggers",
                TabStop = false
            };
            buttonRemoveSelectedTriggers.Click += new EventHandler(Click_removeSelectedTriggers);
            tabPageTriggers.Controls.Add(buttonRemoveSelectedTriggers);

            // Move down a bit so we can start populating the Triggers tab page with a list of Triggers.
            yPosTrigger += 28;

            foreach (Trigger trigger in formTrigger.TriggerCollection)
            {
                // Add a checkbox so that the user has the ability to remove the selected Trigger.
                CheckBox checkboxTrigger = new CheckBox
                {
                    Size = new Size(CHECKBOX_WIDTH, CHECKBOX_HEIGHT),
                    Location = new Point(xPosTrigger, yPosTrigger),
                    Tag = trigger,
                    TabStop = false
                };
                tabPageTriggers.Controls.Add(checkboxTrigger);

                // Add a read-only text box showing the name of the Trigger.
                TextBox textBoxTrigger = new TextBox
                {
                    Width = TRIGGER_TEXTBOX_WIDTH,
                    Height = TRIGGER_HEIGHT,
                    MaxLength = TRIGGER_TEXTBOX_MAX_LENGTH,
                    Location = new Point(xPosTrigger + X_POS_TRIGGER_TEXTBOX, yPosTrigger),
                    Text = trigger.Name,
                    ReadOnly = true,
                    TabStop = false
                };
                tabPageTriggers.Controls.Add(textBoxTrigger);

                // Add a button so that the user can change the Trigger.
                Button buttonChangeTrigger = new Button
                {
                    Size = new Size(SMALL_BUTTON_WIDTH, SMALL_BUTTON_HEIGHT),
                    Location = new Point(xPosTrigger + X_POS_TRIGGER_BUTTON, yPosTrigger),
                    Text = EDIT_BUTTON_TEXT,
                    Tag = trigger,
                    TabStop = false
                };
                buttonChangeTrigger.Click += new EventHandler(Click_buttonChangeTrigger);
                tabPageTriggers.Controls.Add(buttonChangeTrigger);

                // Move down the Triggers tab page so we're ready to loop around again and add the next Trigger to it.
                yPosTrigger += Y_POS_TRIGGER_INCREMENT;
            }
        }

        private void BuildScreensModule()
        {
            int xPosScreen = 5;
            int yPosScreen = 3;

            const int SCREEN_HEIGHT = 20;
            const int CHECKBOX_WIDTH = 20;
            const int CHECKBOX_HEIGHT = 20;
            const int BIG_BUTTON_WIDTH = 205;
            const int BIG_BUTTON_HEIGHT = 25;
            const int SMALL_BUTTON_WIDTH = 27;
            const int SMALL_BUTTON_HEIGHT = 20;
            const int X_POS_SCREEN_TEXTBOX = 20;
            const int X_POS_SCREEN_BUTTON = 178;
            const int SCREEN_TEXTBOX_WIDTH = 153;
            const int Y_POS_SCREEN_INCREMENT = 23;
            const int SCREEN_TEXTBOX_MAX_LENGTH = 50;

            const string EDIT_BUTTON_TEXT = "...";

            tabPageScreens.Controls.Clear();

            // The button for adding a new Screen.
            Button buttonAddNewScreen = new Button
            {
                Size = new Size(BIG_BUTTON_WIDTH, BIG_BUTTON_HEIGHT),
                Location = new Point(xPosScreen, yPosScreen),
                Text = "Add New Screen ...",
                TabStop = false
            };
            buttonAddNewScreen.Click += new EventHandler(Click_addScreen);
            tabPageScreens.Controls.Add(buttonAddNewScreen);

            // Move down and then add the "Remove Selected Screens" button.
            yPosScreen += 27;

            Button buttonRemoveSelectedScreens = new Button
            {
                Size = new Size(BIG_BUTTON_WIDTH, BIG_BUTTON_HEIGHT),
                Location = new Point(xPosScreen, yPosScreen),
                Text = "Remove Selected Screens",
                TabStop = false
            };
            buttonRemoveSelectedScreens.Click += new EventHandler(Click_removeSelectedScreens);
            tabPageScreens.Controls.Add(buttonRemoveSelectedScreens);

            // Move down a bit so we can start populating the Screens tab page with a list of Screens.
            yPosScreen += 28;

            foreach (Screen screen in formScreen.ScreenCollection)
            {
                // Add a checkbox so that the user has the ability to remove the selected Screen.
                CheckBox checkboxScreen = new CheckBox
                {
                    Size = new Size(CHECKBOX_WIDTH, CHECKBOX_HEIGHT),
                    Location = new Point(xPosScreen, yPosScreen),
                    Tag = screen,
                    TabStop = false
                };
                tabPageScreens.Controls.Add(checkboxScreen);

                // Add a read-only text box showing the name of the Screen.
                TextBox textBoxScreen = new TextBox
                {
                    Width = SCREEN_TEXTBOX_WIDTH,
                    Height = SCREEN_HEIGHT,
                    MaxLength = SCREEN_TEXTBOX_MAX_LENGTH,
                    Location = new Point(xPosScreen + X_POS_SCREEN_TEXTBOX, yPosScreen),
                    Text = screen.Name,
                    ReadOnly = true,
                    TabStop = false
                };
                tabPageScreens.Controls.Add(textBoxScreen);

                // Add a button so that the user can change the Screen.
                Button buttonChangeScreen = new Button
                {
                    Size = new Size(SMALL_BUTTON_WIDTH, SMALL_BUTTON_HEIGHT),
                    Location = new Point(xPosScreen + X_POS_SCREEN_BUTTON, yPosScreen),
                    Text = EDIT_BUTTON_TEXT,
                    Tag = screen,
                    TabStop = false
                };
                buttonChangeScreen.Click += new EventHandler(Click_changeScreen);
                tabPageScreens.Controls.Add(buttonChangeScreen);

                // Move down the Screens tab page so we're ready to loop around again and add the next Screen to it.
                yPosScreen += Y_POS_SCREEN_INCREMENT;
            }
        }

        private void BuildRegionsModule()
        {
            int xPosRegion = 5;
            int yPosRegion = 3;

            const int REGION_HEIGHT = 20;
            const int CHECKBOX_WIDTH = 20;
            const int CHECKBOX_HEIGHT = 20;
            const int BIG_BUTTON_WIDTH = 205;
            const int BIG_BUTTON_HEIGHT = 25;
            const int SMALL_BUTTON_WIDTH = 27;
            const int SMALL_BUTTON_HEIGHT = 20;
            const int X_POS_REGION_TEXTBOX = 20;
            const int X_POS_REGION_BUTTON = 178;
            const int REGION_TEXTBOX_WIDTH = 153;
            const int Y_POS_REGION_INCREMENT = 23;
            const int REGION_TEXTBOX_MAX_LENGTH = 50;

            const string EDIT_BUTTON_TEXT = "...";

            tabPageRegions.Controls.Clear();

            // The button for adding a new Region.
            Button buttonAddNewRegion = new Button
            {
                Size = new Size(BIG_BUTTON_WIDTH, BIG_BUTTON_HEIGHT),
                Location = new Point(xPosRegion, yPosRegion),
                Text = "Add New Region ...",
                TabStop = false
            };
            buttonAddNewRegion.Click += new EventHandler(Click_addRegion);
            tabPageRegions.Controls.Add(buttonAddNewRegion);

            // Move down and then add the "Remove Selected Regions" button.
            yPosRegion += 27;

            Button buttonRemoveSelectedRegions = new Button
            {
                Size = new Size(BIG_BUTTON_WIDTH, BIG_BUTTON_HEIGHT),
                Location = new Point(xPosRegion, yPosRegion),
                Text = "Remove Selected Regions",
                TabStop = false
            };
            buttonRemoveSelectedRegions.Click += new EventHandler(Click_removeSelectedRegions);
            tabPageRegions.Controls.Add(buttonRemoveSelectedRegions);

            // Move down a bit so we can start populating the Regions tab page with a list of Regions.
            yPosRegion += 28;

            foreach (Region region in formRegion.RegionCollection)
            {
                // Add a checkbox so that the user has the ability to remove the selected Region.
                CheckBox checkboxRegion = new CheckBox
                {
                    Size = new Size(CHECKBOX_WIDTH, CHECKBOX_HEIGHT),
                    Location = new Point(xPosRegion, yPosRegion),
                    Tag = region,
                    TabStop = false
                };
                tabPageRegions.Controls.Add(checkboxRegion);

                // Add a read-only text box showing the name of the Region.
                TextBox textBoxRegion = new TextBox
                {
                    Width = REGION_TEXTBOX_WIDTH,
                    Height = REGION_HEIGHT,
                    MaxLength = REGION_TEXTBOX_MAX_LENGTH,
                    Location = new Point(xPosRegion + X_POS_REGION_TEXTBOX, yPosRegion),
                    Text = region.Name,
                    ReadOnly = true,
                    TabStop = false
                };
                tabPageRegions.Controls.Add(textBoxRegion);

                // Add a button so that the user can change the Region.
                Button buttonChangeRegion = new Button
                {
                    Size = new Size(SMALL_BUTTON_WIDTH, SMALL_BUTTON_HEIGHT),
                    Location = new Point(xPosRegion + X_POS_REGION_BUTTON, yPosRegion),
                    Text = EDIT_BUTTON_TEXT,
                    Tag = region,
                    TabStop = false
                };
                buttonChangeRegion.Click += new EventHandler(Click_changeRegion);
                tabPageRegions.Controls.Add(buttonChangeRegion);

                // Move down the Regions tab page so we're ready to loop around again and add the next Region to it.
                yPosRegion += Y_POS_REGION_INCREMENT;
            }
        }

        private void BuildViewTabPages()
        {
            tabControlViews.Controls.Clear();

            foreach (Screen screen in formScreen.ScreenCollection)
            {
                ToolStrip toolStripScreen = new ToolStrip
                {
                    Name = screen.Name + "toolStrip",
                    GripStyle = ToolStripGripStyle.Hidden,
                    Tag = screen
                };

                toolStripScreen = BuildViewTabPageToolStripItems(toolStripScreen, screen.Name);

                PictureBox pictureBoxScreen = new PictureBox
                {
                    Name = screen.Name + "pictureBox",
                    BackColor = Color.Black,
                    Location = new Point(4, 29),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    ContextMenuStrip = contextMenuStripScreenshotPreview,
                    Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left,
                };

                TabPage tabPageScreen = new TabPage
                {
                    Name = screen.Name,
                    Text = screen.Name,
                    Tag = screen
                };

                tabPageScreen.Controls.Add(toolStripScreen);
                tabPageScreen.Controls.Add(pictureBoxScreen);

                pictureBoxScreen.Width = (tabPageScreen.Width - 10);
                pictureBoxScreen.Height = (tabPageScreen.Height - 30);

                tabControlViews.Controls.Add(tabPageScreen);
            }

            foreach (Region region in formRegion.RegionCollection)
            {
                ToolStrip toolStripRegion = new ToolStrip
                {
                    Name = region.Name + "toolStrip",
                    GripStyle = ToolStripGripStyle.Hidden,
                    Tag = region
                };

                toolStripRegion = BuildViewTabPageToolStripItems(toolStripRegion, region.Name);

                PictureBox pictureBoxRegion = new PictureBox
                {
                    Name = region.Name + "pictureBox",
                    BackColor = Color.Black,
                    Location = new Point(4, 29),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    ContextMenuStrip = contextMenuStripScreenshotPreview,
                    Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left,
                };

                TabPage tabPageRegion = new TabPage
                {
                    Name = region.Name,
                    Text = region.Name,
                    Tag = region
                };

                tabPageRegion.Controls.Add(toolStripRegion);
                tabPageRegion.Controls.Add(pictureBoxRegion);

                pictureBoxRegion.Width = (tabPageRegion.Width - 10);
                pictureBoxRegion.Height = (tabPageRegion.Height - 30);

                tabControlViews.Controls.Add(tabPageRegion);
            }

            ShowScreenshotBySlideIndex();
        }

        private ToolStrip BuildViewTabPageToolStripItems(ToolStrip toolStrip, string name)
        {
            ToolStripSplitButton toolStripSplitButtonEdit = new ToolStripSplitButton
            {
                Text = "Edit",
                Alignment = ToolStripItemAlignment.Left,
                AutoToolTip = false,
                Image = Resources.edit
            };

            ToolStripButton toolStripButtonEmail = new ToolStripButton
            {
                Text = "Email",
                Alignment = ToolStripItemAlignment.Left,
                AutoToolTip = false,
                Image = Resources.email
            };

            toolStripButtonEmail.Click += new EventHandler(Click_emailScreenshot);

            toolStripSplitButtonEdit.DropDown.Items.Add("Add New Editor ...", null, Click_addEditorToolStripMenuItem);

            foreach (Editor editor in formEditor.EditorCollection)
            {
                if (editor != null && File.Exists(editor.Application))
                {
                    toolStripSplitButtonEdit.DropDown.Items.Add(editor.Name, Icon.ExtractAssociatedIcon(editor.Application).ToBitmap(), Click_runEditor);
                }
            }

            ToolStripSplitButton toolStripSplitButtonConfigure = new ToolStripSplitButton
            {
                Text = "Configure",
                Alignment = ToolStripItemAlignment.Right,
                AutoToolTip = false,
                Image = Resources.options
            };

            toolStripSplitButtonConfigure.DropDown.Items.Add("Add New Screen", null, Click_addScreen);
            toolStripSplitButtonConfigure.DropDown.Items.Add("Add New Region", null, Click_addRegion);

            toolStripSplitButtonConfigure.DropDown.Items.Add(new ToolStripSeparator());

            if (toolStrip.Tag is Screen)
            {
                ToolStripMenuItem toolStripMenuItemChangeScreen = new ToolStripMenuItem
                {
                    Text = "Change Screen",
                    Tag = toolStrip.Tag
                };

                toolStripMenuItemChangeScreen.Click += new EventHandler(Click_changeScreen);

                toolStripSplitButtonConfigure.DropDown.Items.Add(toolStripMenuItemChangeScreen);

                ToolStripMenuItem toolStripMenuItemRemoveScreen = new ToolStripMenuItem
                {
                    Text = "Remove Screen",
                    Tag = toolStrip.Tag
                };

                toolStripMenuItemRemoveScreen.Click += new EventHandler(Click_removeScreen);

                toolStripSplitButtonConfigure.DropDown.Items.Add(toolStripMenuItemRemoveScreen);
            }

            if (toolStrip.Tag is Region)
            {
                ToolStripMenuItem toolStripMenuItemRegion = new ToolStripMenuItem
                {
                    Text = "Change Region",
                    Tag = toolStrip.Tag
                };

                toolStripMenuItemRegion.Click += new EventHandler(Click_changeRegion);

                toolStripSplitButtonConfigure.DropDown.Items.Add(toolStripMenuItemRegion);

                ToolStripMenuItem toolStripMenuItemRemoveRegion = new ToolStripMenuItem
                {
                    Text = "Remove Region",
                    Tag = toolStrip.Tag
                };

                toolStripMenuItemRemoveRegion.Click += new EventHandler(Click_removeRegion);

                toolStripSplitButtonConfigure.DropDown.Items.Add(toolStripMenuItemRemoveRegion);
            }

            ToolStripItem toolStripLabelFilename = new ToolStripLabel
            {
                Text = "Filename:",
                Alignment = ToolStripItemAlignment.Right,
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            ToolStripItem toolstripTextBoxFilename = new ToolStripTextBox
            {
                Name = name + "toolStripTextBoxFilename",
                Alignment = ToolStripItemAlignment.Right,
                AutoSize = false,
                ReadOnly = true,
                Width = 200,
                BackColor = Color.LightYellow,
                Text = string.Empty
            };

            ToolStripItem toolstripButtonOpenFolder = new ToolStripButton
            {
                Image = Resources.openfolder,
                Alignment = ToolStripItemAlignment.Right,
                AutoToolTip = false,
                ToolTipText = "Show Screenshot Location",
                DisplayStyle = ToolStripItemDisplayStyle.Image
            };

            toolstripButtonOpenFolder.Click += new EventHandler(Click_toolStripMenuItemShowScreenshotLocation);

            toolStrip.Items.Add(toolStripButtonEmail);
            toolStrip.Items.Add(toolStripSplitButtonEdit);
            toolStrip.Items.Add(toolStripSplitButtonConfigure);
            toolStrip.Items.Add(new ToolStripSeparator { Alignment = ToolStripItemAlignment.Right });
            toolStrip.Items.Add(toolstripButtonOpenFolder);
            toolStrip.Items.Add(toolstripTextBoxFilename);
            toolStrip.Items.Add(toolStripLabelFilename);
            toolStrip.Items.Add(new ToolStripSeparator { Alignment = ToolStripItemAlignment.Right });

            return toolStrip;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="screenshot"></param>
        private void EmailScreenshot(Screenshot screenshot, bool prompt)
        {
            try
            {
                if (screenshot == null || string.IsNullOrEmpty(screenshot.Path)) return;

                string from = Settings.Application.GetByKey("EmailMessageFrom", string.Empty).Value.ToString();
                string to = Settings.Application.GetByKey("EmailMessageTo", string.Empty).Value.ToString();
                string cc = Settings.Application.GetByKey("EmailMessageCC", string.Empty).Value.ToString();
                string bcc = Settings.Application.GetByKey("EmailMessageBCC", string.Empty).Value.ToString();
                string subject = Settings.Application.GetByKey("EmailMessageSubject", string.Empty).Value.ToString();
                string body = Settings.Application.GetByKey("EmailMessageBody", string.Empty).Value.ToString();

                if (string.IsNullOrEmpty(@from) || string.IsNullOrEmpty(to)) return;

                var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(@from);
                mailMessage.To.Add(to);

                if (!string.IsNullOrEmpty(cc))
                    mailMessage.CC.Add(cc);

                if (!string.IsNullOrEmpty(bcc))
                    mailMessage.Bcc.Add(bcc);

                if (!string.IsNullOrEmpty(subject))
                    mailMessage.Subject = subject;

                if (!string.IsNullOrEmpty(body))
                    mailMessage.Body = body;

                mailMessage.IsBodyHtml = false;

                mailMessage.Attachments.Add(new Attachment(screenshot.Path));

                if (mailMessage.Attachments == null || mailMessage.Attachments.Count <= 0) return;

                string host = Settings.Application.GetByKey("EmailServerHost", string.Empty).Value.ToString();
                int port = Convert.ToInt32(Settings.Application.GetByKey("EmailServerPort", string.Empty).Value);

                string username = Settings.Application.GetByKey("EmailClientUsername", string.Empty).Value.ToString();
                string password = Settings.Application.GetByKey("EmailClientPassword", string.Empty).Value.ToString();

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return;

                var smtpClient = new SmtpClient(host, port);

                smtpClient.EnableSsl = Convert.ToBoolean(Settings.Application.GetByKey("EmailServerEnableSSL", string.Empty).Value);
                smtpClient.Credentials = new NetworkCredential(username, password);

                if (prompt)
                {
                    DialogResult dialogResult = MessageBox.Show("Do you want to email this screenshot?", "Email Screenshot", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Yes)
                    {
                        smtpClient.Send(mailMessage);
                    }
                }
                else
                {
                    smtpClient.Send(mailMessage);
                }
            }
            catch (Exception ex)
            {
                Log.Write("FormMain::EmailScreenshot", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="triggerActionType"></param>
        private void EmailScreenshot(TriggerActionType triggerActionType)
        {
            if (triggerActionType == TriggerActionType.EmailScreenshot && _screenCapture.Running)
            {
                Screenshot screenshot = _screenshotCollection.Get(_screenshotCollection.Count - 1);

                if (screenshot != null && screenshot.Slide != null && !string.IsNullOrEmpty(screenshot.Path))
                {
                    EmailScreenshot(screenshot, prompt: false);
                }
            }
        }

        #region Click Event Handlers

        #region Email

        /// <summary>
        /// Emails screenshots using the EmailSever and EmailMessage settings in application settings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_emailScreenshot(object sender, EventArgs e)
        {
            Screenshot screenshot = null;

            if (tabControlViews.SelectedTab.Tag.GetType() == typeof(Screen))
            {
                var screen = (Screen)tabControlViews.SelectedTab.Tag;

                screenshot = _screenshotCollection.GetScreenshot(Slideshow.SelectedSlide.Name, screen.ViewId);
            }

            if (tabControlViews.SelectedTab.Tag.GetType() == typeof(Region))
            {
                var region = (Region)tabControlViews.SelectedTab.Tag;

                screenshot = _screenshotCollection.GetScreenshot(Slideshow.SelectedSlide.Name, region.ViewId);
            }

            EmailScreenshot(screenshot, prompt: true);
        }

        #endregion

        #region Editor

        /// <summary>
        /// Shows the "Add Editor" window to enable the user to add a chosen Editor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_addEditorToolStripMenuItem(object sender, EventArgs e)
        {
            formEditor.EditorObject = null;

            formEditor.ShowDialog(this);

            if (formEditor.DialogResult == DialogResult.OK)
            {
                BuildEditorsModule();
                BuildViewTabPages();
                BuildScreenshotPreviewContextualMenu();

                formEditor.EditorCollection.Save();
            }
        }

        /// <summary>
        /// Removes the selected Editors from the Editors tab page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_removeSelectedEditors(object sender, EventArgs e)
        {
            int countBeforeRemoval = formEditor.EditorCollection.Count;

            foreach (Control control in tabPageEditors.Controls)
            {
                if (control.GetType().Equals(typeof(CheckBox)))
                {
                    CheckBox checkBox = (CheckBox) control;

                    if (checkBox.Checked)
                    {
                        Editor editor = formEditor.EditorCollection.Get((Editor) checkBox.Tag);
                        formEditor.EditorCollection.Remove(editor);
                    }
                }
            }

            if (countBeforeRemoval > formEditor.EditorCollection.Count)
            {
                BuildEditorsModule();
                BuildViewTabPages();
                BuildScreenshotPreviewContextualMenu();

                formEditor.EditorCollection.Save();
            }
        }

        /// <summary>
        /// Runs the chosen image editor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_runEditor(object sender, EventArgs e)
        {
            if (listBoxScreenshots.SelectedIndex > -1)
            {
                Editor editor = formEditor.EditorCollection.GetByName(sender.ToString());
                RunEditor(editor);
            }
        }

        /// <summary>
        /// Shows the "Change Editor" window to enable the user to edit a chosen Editor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_buttonChangeEditor(object sender, EventArgs e)
        {
            Button buttonSelected = (Button) sender;

            if (buttonSelected.Tag != null)
            {
                formEditor.EditorObject = (Editor) buttonSelected.Tag;

                formEditor.ShowDialog(this);

                if (formEditor.DialogResult == DialogResult.OK)
                {
                    BuildEditorsModule();
                    BuildViewTabPages();
                    BuildScreenshotPreviewContextualMenu();

                    formEditor.EditorCollection.Save();
                }
            }
        }

        /// <summary>
        /// Executes a chosen image editor from the interface.
        /// </summary>
        /// <param name="editor">The image editor to execute.</param>
        private void RunEditor(Editor editor)
        {
            if (editor != null)
            {
                if (tabControlViews.SelectedTab.Tag.GetType() == typeof(Screen))
                {
                    Screen screen = (Screen) tabControlViews.SelectedTab.Tag;
                    RunEditor(editor, _screenshotCollection.GetScreenshot(Slideshow.SelectedSlide.Name, screen.ViewId));
                }

                if (tabControlViews.SelectedTab.Tag.GetType() == typeof(Region))
                {
                    Region region = (Region) tabControlViews.SelectedTab.Tag;
                    RunEditor(editor, _screenshotCollection.GetScreenshot(Slideshow.SelectedSlide.Name, region.ViewId));
                }
            }
        }

        /// <summary>
        /// Executes a chosen image editor from a Trigger to open the last screenshot taken with the image editor.
        /// </summary>
        /// <param name="editor">The image editor to execute.</param>
        /// <param name="triggerActionType">The trigger's action type.</param>
        private void RunEditor(Editor editor, TriggerActionType triggerActionType)
        {
            if (editor != null && triggerActionType == TriggerActionType.RunEditor && _screenCapture.Running)
            {
                Screenshot screenshot = _screenshotCollection.Get(_screenshotCollection.Count - 1);

                if (screenshot != null && screenshot.Slide != null && !string.IsNullOrEmpty(screenshot.Path))
                {
                    Log.Write("Running editor (based on TriggerActionType.RunEditor) \"" + editor.Name + "\" using screenshot path \"" + screenshot.Path + "\"");

                    RunEditor(editor, screenshot);
                }
            }
        }

        /// <summary>
        /// Runs the editor using the specified screenshot.
        /// </summary>
        /// <param name="editor">The editor to use.</param>
        /// <param name="screenshot">The screenshot to use.</param>
        private void RunEditor(Editor editor, Screenshot screenshot)
        {
            // Execute the chosen image editor. If the %screenshot% argument happens to be included
            // then we'll use that argument as the screenshot file path when executing the image editor.
            if (editor != null && (screenshot != null && !string.IsNullOrEmpty(screenshot.Path) && File.Exists(screenshot.Path)))
            {
                Log.Write("Starting process for editor \"" + editor.Name + "\" ...");
                Log.Write("Application: " + editor.Application);
                Log.Write("Arguments before %screenshot% tag replacement: " + editor.Arguments);
                Log.Write("Arguments after %screenshot% tag replacement: " + editor.Arguments.Replace("%screenshot%", "\"" + screenshot.Path + "\""));

                Process.Start(editor.Application, editor.Arguments.Replace("%screenshot%", "\"" + screenshot.Path + "\""));
            }
        }

        #endregion Editor

        #region Trigger

        /// <summary>
        /// Shows the "Add Trigger" window to enable the user to add a chosen Trigger.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_addTrigger(object sender, EventArgs e)
        {
            formTrigger.TriggerObject = null;

            formTrigger.EditorCollection = formEditor.EditorCollection;

            formTrigger.ShowDialog(this);

            if (formTrigger.DialogResult == DialogResult.OK)
            {
                BuildTriggersModule();

                formTrigger.TriggerCollection.Save();
            }
        }

        /// <summary>
        /// Removes the selected Triggers from the Triggers tab page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_removeSelectedTriggers(object sender, EventArgs e)
        {
            int countBeforeRemoval = formTrigger.TriggerCollection.Count;

            foreach (Control control in tabPageTriggers.Controls)
            {
                if (control.GetType().Equals(typeof(CheckBox)))
                {
                    CheckBox checkBox = (CheckBox) control;

                    if (checkBox.Checked)
                    {
                        Trigger trigger = formTrigger.TriggerCollection.Get((Trigger) checkBox.Tag);
                        formTrigger.TriggerCollection.Remove(trigger);
                    }
                }
            }

            if (countBeforeRemoval > formTrigger.TriggerCollection.Count)
            {
                BuildTriggersModule();

                formTrigger.TriggerCollection.Save();
            }
        }

        /// <summary>
        /// Shows the "Change Trigger" window to enable the user to edit a chosen Trigger.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_buttonChangeTrigger(object sender, EventArgs e)
        {
            Button buttonSelected = (Button) sender;

            if (buttonSelected.Tag != null)
            {
                formTrigger.TriggerObject = (Trigger) buttonSelected.Tag;

                formTrigger.EditorCollection = formEditor.EditorCollection;

                formTrigger.ShowDialog(this);

                if (formTrigger.DialogResult == DialogResult.OK)
                {
                    BuildTriggersModule();

                    formTrigger.TriggerCollection.Save();
                }
            }
        }

        #endregion Trigger

        #region Region

        /// <summary>
        /// Shows the "Add Region" window to enable the user to add a chosen Region.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_addRegion(object sender, EventArgs e)
        {
            formRegion.RegionObject = null;
            formRegion.ImageFormatCollection = _imageFormatCollection;
            formRegion.MacroTagCollection = _macroTagCollection;
            formRegion.screenCapture = _screenCapture;

            formRegion.ShowDialog(this);

            if (formRegion.DialogResult == DialogResult.OK)
            {
                BuildRegionsModule();
                BuildViewTabPages();

                formRegion.RegionCollection.Save();
            }
        }

        /// <summary>
        /// Removes the selected Regions from the Regions tab page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_removeSelectedRegions(object sender, EventArgs e)
        {
            int countBeforeRemoval = formRegion.RegionCollection.Count;

            foreach (Control control in tabPageRegions.Controls)
            {
                if (control.GetType().Equals(typeof(CheckBox)))
                {
                    CheckBox checkBox = (CheckBox) control;

                    if (checkBox.Checked)
                    {
                        Region region = formRegion.RegionCollection.Get((Region) checkBox.Tag);
                        formRegion.RegionCollection.Remove(region);
                    }
                }
            }

            if (countBeforeRemoval > formRegion.RegionCollection.Count)
            {
                BuildRegionsModule();
                BuildViewTabPages();

                formRegion.RegionCollection.Save();
            }
        }

        /// <summary>
        /// Shows the "Change Region" window to enable the user to edit a chosen Region.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_changeRegion(object sender, EventArgs e)
        {
            Region region = new Region();

            if (sender is Button)
            {
                Button buttonSelected = (Button)sender;
                region = (Region)buttonSelected.Tag;
            }

            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem toolStripMenuItemSelected = (ToolStripMenuItem)sender;
                region = (Region)toolStripMenuItemSelected.Tag;
            }

            formRegion.RegionObject = region;
            formRegion.ImageFormatCollection = _imageFormatCollection;
            formRegion.MacroTagCollection = _macroTagCollection;
            formRegion.screenCapture = _screenCapture;

            formRegion.ShowDialog(this);

            if (formRegion.DialogResult == DialogResult.OK)
            {
                BuildRegionsModule();
                BuildViewTabPages();

                formRegion.RegionCollection.Save();
            }
        }

        private void Click_removeRegion(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem toolStripMenuItemSelected = (ToolStripMenuItem)sender;
                Region regionSelected = (Region)toolStripMenuItemSelected.Tag;

                DialogResult dialogResult = MessageBox.Show("Do you want to remove the region named \"" + regionSelected.Name + "\"?", "Remove Region", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    Region region = formRegion.RegionCollection.Get(regionSelected);
                    formRegion.RegionCollection.Remove(region);

                    BuildRegionsModule();
                    BuildViewTabPages();

                    formRegion.RegionCollection.Save();
                }
            }
        }

        #endregion Region

        #region Screen

        /// <summary>
        /// Shows the "Add Screen" window to enable the user to add a chosen Screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_addScreen(object sender, EventArgs e)
        {
            formScreen.ScreenObject = null;
            formScreen.ImageFormatCollection = _imageFormatCollection;
            formScreen.MacroTagCollection = _macroTagCollection;
            formScreen.ScreenCapture = _screenCapture;

            formScreen.ShowDialog(this);

            if (formScreen.DialogResult == DialogResult.OK)
            {
                BuildScreensModule();
                BuildViewTabPages();

                formScreen.ScreenCollection.Save();
            }
        }

        /// <summary>
        /// Removes the selected Screens from the Screens tab page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_removeSelectedScreens(object sender, EventArgs e)
        {
            int countBeforeRemoval = formScreen.ScreenCollection.Count;

            foreach (Control control in tabPageScreens.Controls)
            {
                if (control.GetType().Equals(typeof(CheckBox)))
                {
                    CheckBox checkBox = (CheckBox) control;

                    if (checkBox.Checked)
                    {
                        Screen screen = formScreen.ScreenCollection.Get((Screen) checkBox.Tag);
                        formScreen.ScreenCollection.Remove(screen);
                    }
                }
            }

            if (countBeforeRemoval > formScreen.ScreenCollection.Count)
            {
                BuildScreensModule();
                BuildViewTabPages();

                formScreen.ScreenCollection.Save();
            }
        }

        /// <summary>
        /// Shows the "Change Screen" window to enable the user to edit a chosen Screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_changeScreen(object sender, EventArgs e)
        {
            Screen screen = new Screen();

            if (sender is Button)
            {
                Button buttonSelected = (Button)sender;
                screen = (Screen)buttonSelected.Tag;
            }

            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem toolStripMenuItemSelected = (ToolStripMenuItem)sender;
                screen = (Screen)toolStripMenuItemSelected.Tag;
            }

            formScreen.ScreenObject = screen;
            formScreen.ImageFormatCollection = _imageFormatCollection;
            formScreen.MacroTagCollection = _macroTagCollection;
            formScreen.ScreenCapture = _screenCapture;

            formScreen.ShowDialog(this);

            if (formScreen.DialogResult == DialogResult.OK)
            {
                BuildScreensModule();
                BuildViewTabPages();

                formScreen.ScreenCollection.Save();
            }
        }

        private void Click_removeScreen(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem toolStripMenuItemSelected = (ToolStripMenuItem)sender;
                Screen screenSelected = (Screen)toolStripMenuItemSelected.Tag;

                DialogResult dialogResult = MessageBox.Show("Do you want to remove the screen named \"" + screenSelected.Name + "\"?", "Remove Screen", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    Screen screen = formScreen.ScreenCollection.Get(screenSelected);
                    formScreen.ScreenCollection.Remove(screen);

                    BuildScreensModule();
                    BuildViewTabPages();

                    formScreen.ScreenCollection.Save();
                }
            }
        }

        #endregion Screen

        #region Passphrase

        /// <summary>
        /// Sets the passphrase chosen by the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_buttonSetPassphrase(object sender, EventArgs e)
        {
            if (textBoxPassphrase.Text.Length > 0)
            {
                Settings.User.GetByKey("StringPassphrase", defaultValue: string.Empty).Value = Security.Hash(textBoxPassphrase.Text);
                SaveSettings();

                textBoxPassphrase.Clear();

                ScreenCapture.LockScreenCaptureSession = true;

                MessageBox.Show("The passphrase you entered has been securely stored as a SHA-512 hash and your session has been locked.", "Passphrase Set", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion Passphrase

        #region Apply Label

        private void Click_applyLabel(object sender, EventArgs e)
        {
            if (sender != null && sender is ToolStripDropDownItem)
            {
                ToolStripDropDownItem toolStripDropDownItem = (ToolStripDropDownItem)sender;

                if (!string.IsNullOrEmpty(toolStripDropDownItem.Text))
                {
                    checkBoxScreenshotLabel.Checked = true;
                    comboBoxScreenshotLabel.Text = toolStripDropDownItem.Text;
                    SaveSettings();
                }
            }
        }

        #endregion

        #endregion Click Event Handlers

        /// <summary>
        /// The timer for taking screenshots.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tick_timerScreenCapture(object sender, EventArgs e)
        {
            if (_screenCapture.Running)
            {
                if (_screenCapture.Limit >= ScreenCapture.CAPTURE_LIMIT_MIN &&
                    _screenCapture.Limit <= ScreenCapture.CAPTURE_LIMIT_MAX)
                {
                    if (_screenCapture.Count < _screenCapture.Limit)
                    {
                        TakeScreenshot();
                    }

                    if (_screenCapture.Count == _screenCapture.Limit)
                    {
                        Log.Write("Running triggers of condition type LimitReached");
                        RunTriggersOfConditionType(TriggerConditionType.LimitReached);
                    }
                }
                else
                {
                    TakeScreenshot();
                }
            }
            else
            {
                StopScreenCapture();
            }
        }

        /// <summary>
        /// Takes a screenshot of each available region and screen.
        /// </summary>
        private void TakeScreenshot()
        {
            formScreen.RefreshScreenDictionary();

            if (_screenCapture.GetScreenImages(0, 0, 0, 0, 0, false, 100, out Bitmap bitmap))
            {
                _screenCapture.Count++;

                _screenCapture.DateTimePreviousCycle = DateTime.Now;

                _screenCapture.ActiveWindowTitle = _screenCapture.GetActiveWindowTitle();

                _screenCapture.ActiveWindowProcessName = _screenCapture.GetActiveWindowProcessName();

                RunRegionCaptures();

                RunScreenCaptures();
            }
        }

        /// <summary>
        /// Checks the capture limit when the checkbox is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckedChanged_checkBoxCaptureLimit(object sender, EventArgs e)
        {
            CaptureLimitCheck();
        }

        /// <summary>
        /// Checks the capture limit.
        /// </summary>
        private void CaptureLimitCheck()
        {
            if (checkBoxCaptureLimit.Checked)
            {
                numericUpDownCaptureLimit.Enabled = true;

                _screenCapture.Count = 0;
                _screenCapture.Limit = (int) numericUpDownCaptureLimit.Value;
            }
            else
            {
                numericUpDownCaptureLimit.Enabled = false;
            }
        }

        /// <summary>
        /// Enables the checkboxes for the days that could be selected when setting up a scheduled screen capture session.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckedChanged_checkBoxScheduleOnTheseDays(object sender, EventArgs e)
        {
            if (checkBoxScheduleOnTheseDays.Checked)
            {
                checkBoxSaturday.Enabled = true;
                checkBoxSunday.Enabled = true;
                checkBoxMonday.Enabled = true;
                checkBoxTuesday.Enabled = true;
                checkBoxWednesday.Enabled = true;
                checkBoxThursday.Enabled = true;
                checkBoxFriday.Enabled = true;
            }
            else
            {
                checkBoxSaturday.Enabled = false;
                checkBoxSunday.Enabled = false;
                checkBoxMonday.Enabled = false;
                checkBoxTuesday.Enabled = false;
                checkBoxWednesday.Enabled = false;
                checkBoxThursday.Enabled = false;
                checkBoxFriday.Enabled = false;
            }
        }

        /// <summary>
        /// The timer used for starting scheduled screen capture sessions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tick_timerScheduledCaptureStart(object sender, EventArgs e)
        {
            if (checkBoxScheduleStartAt.Checked)
            {
                if (checkBoxScheduleOnTheseDays.Checked)
                {
                    if (((DateTime.Now.DayOfWeek == DayOfWeek.Saturday && checkBoxSaturday.Checked) ||
                         (DateTime.Now.DayOfWeek == DayOfWeek.Sunday && checkBoxSunday.Checked) ||
                         (DateTime.Now.DayOfWeek == DayOfWeek.Monday && checkBoxMonday.Checked) ||
                         (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday && checkBoxTuesday.Checked) ||
                         (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday && checkBoxWednesday.Checked) ||
                         (DateTime.Now.DayOfWeek == DayOfWeek.Thursday && checkBoxThursday.Checked) ||
                         (DateTime.Now.DayOfWeek == DayOfWeek.Friday && checkBoxFriday.Checked)) &&
                        ((DateTime.Now.Hour == dateTimePickerScheduleStartAt.Value.Hour) &&
                         (DateTime.Now.Minute == dateTimePickerScheduleStartAt.Value.Minute) &&
                         (DateTime.Now.Second == dateTimePickerScheduleStartAt.Value.Second)))
                    {
                        StartScreenCapture();
                    }
                }
                else
                {
                    if ((DateTime.Now.Hour == dateTimePickerScheduleStartAt.Value.Hour) &&
                        (DateTime.Now.Minute == dateTimePickerScheduleStartAt.Value.Minute) &&
                        (DateTime.Now.Second == dateTimePickerScheduleStartAt.Value.Second))
                    {
                        StartScreenCapture();
                    }
                }
            }
        }

        /// <summary>
        /// The timer used for stopping scheduled screen capture sessions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tick_timerScheduledCaptureStop(object sender, EventArgs e)
        {
            if (checkBoxScheduleStopAt.Checked)
            {
                if (checkBoxScheduleOnTheseDays.Checked)
                {
                    if (((DateTime.Now.DayOfWeek == DayOfWeek.Saturday && checkBoxSaturday.Checked) ||
                         (DateTime.Now.DayOfWeek == DayOfWeek.Sunday && checkBoxSunday.Checked) ||
                         (DateTime.Now.DayOfWeek == DayOfWeek.Monday && checkBoxMonday.Checked) ||
                         (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday && checkBoxTuesday.Checked) ||
                         (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday && checkBoxWednesday.Checked) ||
                         (DateTime.Now.DayOfWeek == DayOfWeek.Thursday && checkBoxThursday.Checked) ||
                         (DateTime.Now.DayOfWeek == DayOfWeek.Friday && checkBoxFriday.Checked)) &&
                        ((DateTime.Now.Hour == dateTimePickerScheduleStopAt.Value.Hour) &&
                         (DateTime.Now.Minute == dateTimePickerScheduleStopAt.Value.Minute) &&
                         (DateTime.Now.Second == dateTimePickerScheduleStopAt.Value.Second)))
                    {
                        StopScreenCapture();
                    }
                }
                else
                {
                    if ((DateTime.Now.Hour == dateTimePickerScheduleStopAt.Value.Hour) &&
                        (DateTime.Now.Minute == dateTimePickerScheduleStopAt.Value.Minute) &&
                        (DateTime.Now.Second == dateTimePickerScheduleStopAt.Value.Second))
                    {
                        StopScreenCapture();
                    }
                }
            }
        }

        /// <summary>
        /// Determines when we enable the "Set" button for passphrase.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextChanged_textBoxPassphrase(object sender, EventArgs e)
        {
            if (textBoxPassphrase.Text.Length > 0)
            {
                buttonSetPassphrase.Enabled = true;
            }
            else
            {
                buttonSetPassphrase.Enabled = false;
            }
        }

        /// <summary>
        /// Saves the user's settings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveSettings(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void RunTriggersOfConditionType(TriggerConditionType conditionType)
        {
            foreach (Trigger trigger in formTrigger.TriggerCollection)
            {
                // Don't show the interface on startup if we're running from the command line.
                if (ScreenCapture.RunningFromCommandLine &&
                    trigger.ConditionType == TriggerConditionType.ApplicationStartup &&
                    trigger.ActionType == TriggerActionType.ShowInterface)
                {
                    continue;
                }

                if (trigger.ConditionType == conditionType)
                {
                    // These actions need to directly correspond with the TriggerActionType class.
                    switch (trigger.ActionType)
                    {
                        case TriggerActionType.ExitApplication:
                            ExitApplication();
                            break;

                        case TriggerActionType.HideInterface:
                            HideInterface();
                            break;

                        case TriggerActionType.RunEditor:
                            Editor editor = formEditor.EditorCollection.GetByName(trigger.Editor);
                            RunEditor(editor, TriggerActionType.RunEditor);
                            break;

                        case TriggerActionType.ShowInterface:
                            ShowInterface();
                            break;

                        case TriggerActionType.StartScreenCapture:
                            StartScreenCapture();
                            break;

                        case TriggerActionType.StopScreenCapture:
                            StopScreenCapture();
                            break;

                        case TriggerActionType.EmailScreenshot:
                            EmailScreenshot(TriggerActionType.EmailScreenshot);
                            break;
                    }
                }
            }
        }

        private void RunRegionCaptures()
        {
            Log.Write("Running region captures");

            foreach (Region region in formRegion.RegionCollection)
            {
                MacroParser.screenCapture = _screenCapture;

                if (!string.IsNullOrEmpty(_screenCapture.ActiveWindowTitle))
                {
                    if (_screenCapture.GetScreenImages(-1, region.X, region.Y, region.Width, region.Height, region.Mouse, region.ResolutionRatio, out Bitmap bitmap))
                    {
                        if (_screenCapture.TakeScreenshot(
                            path: FileSystem.CorrectScreenshotsFolderPath(MacroParser.ParseTagsForUserAndMachine(region.Folder)) + MacroParser.ParseTags(region.Name, region.Macro, -1, region.Format, _screenCapture.ActiveWindowTitle),
                            format: region.Format,
                            component: -1,
                            screenshotType: ScreenshotType.Region,
                            jpegQuality: region.JpegQuality,
                            viewId: region.ViewId,
                            bitmap: bitmap,
                            label: checkBoxScreenshotLabel.Checked ? comboBoxScreenshotLabel.Text : string.Empty,
                            windowTitle: _screenCapture.ActiveWindowTitle,
                            processName: _screenCapture.ActiveWindowProcessName,
                            screenshotCollection: _screenshotCollection
                        ))
                        {
                            ScreenshotTakenWithSuccess();
                        }
                        else
                        {
                            ScreenshotTakenWithFailure();
                        }
                    }
                }
            }
        }

        private void RunScreenCaptures()
        {
            Log.Write("Running screen captures");

            foreach (Screen screen in formScreen.ScreenCollection)
            {
                if (screen.Component == 0)
                {
                    MacroParser.screenCapture = _screenCapture;

                    // Active Window
                    if (!string.IsNullOrEmpty(_screenCapture.ActiveWindowTitle))
                    {
                        if (_screenCapture.GetScreenImages(screen.Component, 0, 0, 0, 0, false, screen.ResolutionRatio, out Bitmap bitmap))
                        {
                            if (_screenCapture.TakeScreenshot(
                                path: FileSystem.CorrectScreenshotsFolderPath(MacroParser.ParseTagsForUserAndMachine(screen.Folder)) + MacroParser.ParseTags(screen.Name, screen.Macro, screen.Component, screen.Format, _screenCapture.ActiveWindowTitle),
                                format: screen.Format,
                                component: screen.Component,
                                screenshotType: ScreenshotType.ActiveWindow,
                                jpegQuality: screen.JpegQuality,
                                viewId: screen.ViewId,
                                bitmap: bitmap,
                                label: checkBoxScreenshotLabel.Checked ? comboBoxScreenshotLabel.Text : string.Empty,
                                windowTitle: _screenCapture.ActiveWindowTitle,
                                processName: _screenCapture.ActiveWindowProcessName,
                                screenshotCollection: _screenshotCollection
                            ))
                            {
                                ScreenshotTakenWithSuccess();
                            }
                            else
                            {
                                ScreenshotTakenWithFailure();
                                break;
                            }
                        }
                    }
                }
                else
                {
                    if (formScreen.ScreenDictionary.ContainsKey(screen.Component))
                    {
                        MacroParser.screenCapture = _screenCapture;

                        if (!string.IsNullOrEmpty(_screenCapture.ActiveWindowTitle))
                        {
                            // Screen X
                            if (_screenCapture.GetScreenImages(screen.Component,
                            formScreen.ScreenDictionary[screen.Component].Bounds.X,
                            formScreen.ScreenDictionary[screen.Component].Bounds.Y,
                            formScreen.ScreenDictionary[screen.Component].Bounds.Width,
                            formScreen.ScreenDictionary[screen.Component].Bounds.Height, screen.Mouse, screen.ResolutionRatio, out Bitmap bitmap))
                            {
                                if (_screenCapture.TakeScreenshot(
                                    path: FileSystem.CorrectScreenshotsFolderPath(MacroParser.ParseTagsForUserAndMachine(screen.Folder)) + MacroParser.ParseTags(screen.Name, screen.Macro, screen.Component, screen.Format, _screenCapture.ActiveWindowTitle),
                                    format: screen.Format,
                                    component: screen.Component,
                                    screenshotType: ScreenshotType.Screen,
                                    jpegQuality: screen.JpegQuality,
                                    viewId: screen.ViewId,
                                    bitmap: bitmap,
                                    label: checkBoxScreenshotLabel.Checked ? comboBoxScreenshotLabel.Text : string.Empty,
                                    windowTitle: _screenCapture.ActiveWindowTitle,
                                    processName: _screenCapture.ActiveWindowProcessName,
                                    screenshotCollection: _screenshotCollection
                                ))
                                {
                                    ScreenshotTakenWithSuccess();
                                }
                                else
                                {
                                    ScreenshotTakenWithFailure();
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ScreenshotTakenWithSuccess()
        {
            Log.Write("Running triggers of condition type ScreenshotTaken");
            RunTriggersOfConditionType(TriggerConditionType.ScreenshotTaken);
        }

        private void ScreenshotTakenWithFailure()
        {
            Log.Write("Application encountered error while taking a screenshot. Stopping screen capture");
            StopScreenCapture();
        }

        /// <summary>
        /// Displays the remaining time for when the next screenshot will be taken
        /// when the mouse pointer moves over the system tray icon.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon_MouseMove(object sender, MouseEventArgs e)
        {
            if (_screenCapture.Running)
            {
                if (_screenCapture.PerformingMaintenance)
                {
                    notifyIcon.Text = "Performing maintenance ...";
                }
                else
                {
                    int remainingHours = _screenCapture.TimeRemainingForNextScreenshot.Hours;
                    int remainingMinutes = _screenCapture.TimeRemainingForNextScreenshot.Minutes;
                    int remainingSeconds = _screenCapture.TimeRemainingForNextScreenshot.Seconds;
                    int remainingMilliseconds = _screenCapture.TimeRemainingForNextScreenshot.Milliseconds;

                    string remainingHoursStr = (remainingHours > 0
                        ? remainingHours.ToString() + " hour" + (remainingHours > 1 ? "s" : string.Empty) + ", "
                        : string.Empty);
                    string remainingMinutesStr = (remainingMinutes > 0
                        ? remainingMinutes.ToString() + " minute" + (remainingMinutes > 1 ? "s" : string.Empty) + ", "
                        : string.Empty);

                    string remainingTimeStr = string.Empty;

                    if (remainingSeconds < 1)
                    {
                        remainingTimeStr = "0." + remainingMilliseconds.ToString() + " milliseconds";
                    }
                    else
                    {
                        remainingTimeStr = remainingHoursStr + remainingMinutesStr + remainingSeconds.ToString() +
                                           " second" + (remainingSeconds > 1 ? "s" : string.Empty) + " at " +
                                           _screenCapture.DateTimeNextCycle.ToLongTimeString();
                    }

                    notifyIcon.Text = "Next capture in " + remainingTimeStr;
                }
            }
            else
            {
                notifyIcon.Text = Settings.Application.GetByKey("Name", defaultValue: Settings.ApplicationName).Value.ToString();
            }
        }

        private void tabControlViews_Selected(object sender, TabControlEventArgs e)
        {
            ShowScreenshotBySlideIndex();
        }

        private void comboBoxFilterValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxFilterValue.SelectedIndex > 0)
            {
                SearchDates();
                ShowScreenshots();
            }
        }

        private void buttonRefreshFilterValues_Click(object sender, EventArgs e)
        {
            SearchFilterValues();
            SearchDates();
            ShowScreenshots();
        }

        private void toolStripSplitButtonSaveSettings_ButtonClick(object sender, EventArgs e)
        {
            toolStripSplitButtonSaveSettings.Enabled = false;

            SaveSettings();

            toolStripSplitButtonSaveSettings.Enabled = true;
        }

        private void comboBoxFilterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBoxFilterType.Text))
            {
                comboBoxFilterValue.Enabled = true;
                SearchFilterValues();
            }
            else
            {
                SearchFilterValues();
                SearchDates();
                ShowScreenshots();

                if (comboBoxFilterValue.Items.Count > 1)
                {
                    comboBoxFilterValue.SelectedIndex = 0;
                }

                comboBoxFilterValue.Enabled = false;
            }
        }

        /// <summary>
        /// Saves screenshots every minute.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerPerformMaintenance_Tick(object sender, EventArgs e)
        {
            _screenCapture.PerformingMaintenance = true;

            SaveScreenshots();

            _screenCapture.PerformingMaintenance = false;
        }

        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.Visible)
            {
                HideInterface();
            }
            else
            {
                ShowInterface();
            }
        }

        private void ComboBoxScreenshotLabel_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach(ToolStripMenuItem label in toolStripMenuItemApplyLabel.DropDownItems)
            {
                label.Checked = label.Text.Equals(comboBoxScreenshotLabel.Text);
            }
        }

        private void PopulateLabelList()
        {
            try
            {
                if (ScreenCapture.LockScreenCaptureSession)
                {
                    toolStripSeparatorApplyLabel.Visible = false;
                    toolStripMenuItemApplyLabel.Visible = false;

                    return;
                }

                toolStripSeparatorApplyLabel.Visible = true;
                toolStripMenuItemApplyLabel.Visible = true;

                List<string> labels = _screenshotCollection.GetLabels();
                labels.Sort();

                comboBoxScreenshotLabel.DataSource = labels;
                comboBoxScreenshotLabel.Text = Settings.User.GetByKey("StringScreenshotLabel", defaultValue: string.Empty).Value.ToString();

                toolStripMenuItemApplyLabel.DropDownItems.Clear();

                foreach (string label in labels)
                {
                    ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem()
                    {
                        Text = label,
                        Checked = label.Equals(comboBoxScreenshotLabel.Text),
                        CheckOnClick = true
                    };

                    toolStripMenuItem.Click += new EventHandler(Click_applyLabel);

                    toolStripMenuItemApplyLabel.DropDownItems.Add(toolStripMenuItem);
                }
            }
            catch (Exception ex)
            {
                Log.Write("FormMain::PopulateLabelList", ex);
            }
        }

        private void ContextMenuStripSystemTrayIcon_Opening(object sender, CancelEventArgs e)
        {
            PopulateLabelList();
        }
    }
}