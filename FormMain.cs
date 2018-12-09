//-----------------------------------------------------------------------
// <copyright file="FormMain.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    using Microsoft.Win32;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    /// <summary>
    /// The application's main window.
    /// </summary>
    public partial class FormMain : Form
    {
        private FormEditor formEditor = new FormEditor();
        private FormTrigger formTrigger = new FormTrigger();
        private FormRegion formRegion = new FormRegion();
        private FormEnterPassphrase formEnterPassphrase = new FormEnterPassphrase();

        private ImageFormatCollection _imageFormatCollection;

        /// <summary>
        /// Threads for background operations.
        /// </summary>
        private BackgroundWorker runSlideSearchThread = null;

        private BackgroundWorker runDateSearchThread = null;

        private BackgroundWorker runDeleteSlidesThread = null;

        private BackgroundWorker runSaveSettings = null;

        /// <summary>
        /// Delegates for the threads.
        /// </summary>
        private delegate void UpdateScreenshotPreviewDelegate();

        private delegate void RunSlideSearchDelegate(DoWorkEventArgs e);

        private delegate void RunDateSearchDelegate(DoWorkEventArgs e);

        private delegate void SaveSettingsDelegate(DoWorkEventArgs e);

        /// <summary>
        /// Default settings used by the command line parser.
        /// </summary>
        private const int CAPTURE_LIMIT_MIN = 0;

        private const int CAPTURE_LIMIT_MAX = 9999;
        private const int CAPTURE_DELAY_DEFAULT_IN_MINUTES = 1;
        private const int IMAGE_RESOLUTION_RATIO_MIN = 1;
        private const int IMAGE_RESOLUTION_RATIO_MAX = 100;
        private const int JPEG_QUALITY_LEVEL_MIN = 1;
        private const int JPEG_QUALITY_LEVEL_MAX = 100;

        /// <summary>
        /// The various regular expressions used in the parsing of the command line arguments.
        /// </summary>
        private const string REGEX_COMMAND_LINE_INITIAL = "^-initial$";

        private const string REGEX_COMMAND_LINE_MACRO = "^-macro=(?<Macro>.+)$";
        private const string REGEX_COMMAND_LINE_FOLDER = "^-folder=(?<Folder>.+)$";
        private const string REGEX_COMMAND_LINE_RATIO = @"^-ratio=(?<Ratio>\d{1,3})$";
        private const string REGEX_COMMAND_LINE_LIMIT = @"^-limit=(?<Limit>\d{1,7})$";
        private const string REGEX_COMMAND_LINE_FORMAT = "^-format=(?<Format>(BMP|EMF|GIF|JPEG|PNG|TIFF|WMF))$";
        private const string REGEX_COMMAND_LINE_STOPAT = @"^-stopat=(?<Hours>\d{2}):(?<Minutes>\d{2}):(?<Seconds>\d{2})$";
        private const string REGEX_COMMAND_LINE_STARTAT = @"^-startat=(?<Hours>\d{2}):(?<Minutes>\d{2}):(?<Seconds>\d{2})$";
        private const string REGEX_COMMAND_LINE_DELAY = @"^-delay=(?<Hours>\d{2}):(?<Minutes>\d{2}):(?<Seconds>\d{2})\.(?<Milliseconds>\d{3})$";
        private const string REGEX_COMMAND_LINE_LOCK = "^-lock$";
        private const string REGEX_COMMAND_LINE_JPEG_LEVEL = @"^-jpeglevel=(?<JpegLevel>\d{1,3})$";
        private const string REGEX_COMMAND_LINE_HIDE_SYSTEM_TRAY_ICON = "^-hideSystemTrayIcon$";

        /// <summary>
        /// Constructor for the main form. Arguments from the command line can be passed to it.
        /// </summary>
        /// <param name="args">Arguments from the command line</param>
        public FormMain(string[] args)
        {
            InitializeComponent();

            if (!Directory.Exists(FileSystem.ApplicationFolder))
            {
                Directory.CreateDirectory(FileSystem.ApplicationFolder);
                Directory.CreateDirectory(FileSystem.SlidesFolder);
            }

            Settings.Initialize();

            Log.Enabled = Convert.ToBoolean(Settings.Application.GetByKey("DebugMode").Value);

            Log.Write("Starting application.");

            LoadSettings();

            Text = (string)Settings.Application.GetByKey("Name").Value;

            if (args.Length > 0)
            {
                ParseCommandLineArguments(args);
            }
        }

        /// <summary>
        /// When this form loads we'll need to delete slides and then search for dates and slides.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            InitializeThreads();

            DeleteSlides();
            SearchDates();

            // Changing the value of this property will automatically call SearchSlides.
            toolStripComboBoxImageFormatFilter.SelectedIndex = Convert.ToInt32(Settings.User.GetByKey("ImageFormatFilterIndex").Value);

            RunTriggersOfConditionType(TriggerConditionType.ApplicationStartup);
        }

        private void InitializeThreads()
        {
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

            runSlideSearchThread = new BackgroundWorker
            {
                WorkerReportsProgress = false,
                WorkerSupportsCancellation = true
            };
            runSlideSearchThread.DoWork += new DoWorkEventHandler(DoWork_runSlideSearchThread);
        }

        /// <summary>
        /// Loads the user's saved settings.
        /// </summary>
        private void LoadSettings()
        {
            try
            {
                Log.Write("Loading settings.");

                Settings.User.Load();

                Log.Write("Settings loaded.");

                Log.Write("Initializing image format collection.");
                _imageFormatCollection = new ImageFormatCollection();

                Log.Write("Initializing image format filter drop down menu.");
                LoadImageFormatFilterDropDownMenu();

                Log.Write("Initializing slideshow module.");

                Slideshow.Initialize();
                Slideshow.OnPlaying += new EventHandler(Slideshow_Playing);

                Log.Write("Initializing editor collection.");

                formEditor.EditorCollection.Load();

                Log.Write("Loaded " + formEditor.EditorCollection.Count + " editors.");

                Log.Write("Initializing trigger collection.");

                formTrigger.TriggerCollection.Load();

                Log.Write("Loaded " + formTrigger.TriggerCollection.Count + " triggers.");

                Log.Write("Initializing region collection.");

                formRegion.RegionCollection.Load();

                Log.Write("Loaded " + formRegion.RegionCollection.Count + " regions.");

                Log.Write("Building screenshot preview context menu.");

                BuildScreenshotPreviewContextualMenu();

                Log.Write(
                    "Loading screenshots into the screenshot collection to generate a history of what was captured.");

                ScreenshotCollection.Load();

                Log.Write("Setting screenshots directory.");

                if (Directory.Exists((string) Settings.User.GetByKey("ScreenshotsDirectory").Value))
                {
                    textBoxFolder.Text = (string) Settings.User.GetByKey("ScreenshotsDirectory").Value;
                }
                else
                {
                    textBoxFolder.Text = FileSystem.ScreenshotsFolder;
                    Directory.CreateDirectory(FileSystem.ScreenshotsFolder);
                }

                Log.Write("Setting screenshots macro.");

                if (string.IsNullOrEmpty((string) Settings.User.GetByKey("Macro").Value))
                {
                    textBoxMacro.Text = MacroParser.UserMacro;
                }
                else
                {
                    textBoxMacro.Text = (string) Settings.User.GetByKey("Macro").Value;
                }

                comboBoxScheduleImageFormat.Items.Clear();
                toolStripMenuItemStartScreenCapture.DropDownItems.Clear();
                toolStripSplitButtonStartScreenCapture.DropDownItems.Clear();

                Log.Write("Building image format list in system tray menu.");

                foreach (ImageFormat imageFormat in _imageFormatCollection)
                {
                    comboBoxScheduleImageFormat.Items.Add(imageFormat.Name);

                    ToolStripMenuItem startScreenCaptureMenuItemForSplitButton =
                        new ToolStripMenuItem(imageFormat.Name);
                    startScreenCaptureMenuItemForSplitButton.Click +=
                        new EventHandler(Click_toolStripMenuItemStartScreenCapture);

                    ToolStripMenuItem startScreenCaptureMenuItemForSystemTrayMenu =
                        new ToolStripMenuItem(imageFormat.Name);
                    startScreenCaptureMenuItemForSystemTrayMenu.Click +=
                        new EventHandler(Click_toolStripMenuItemStartScreenCapture);

                    toolStripMenuItemStartScreenCapture.DropDownItems.Add(startScreenCaptureMenuItemForSystemTrayMenu);
                    toolStripSplitButtonStartScreenCapture.DropDownItems.Add(startScreenCaptureMenuItemForSplitButton);
                }

                comboBoxScheduleImageFormat.SelectedItem = Settings.User.GetByKey("ScheduleImageFormat").Value;

                int interval = Convert.ToInt32(Settings.User.GetByKey("Interval").Value);
                int slideshowDelay = Convert.ToInt32(Settings.User.GetByKey("SlideshowDelay").Value);

                decimal intervalHours = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(interval)).Hours);
                decimal intervalMinutes =
                    Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(interval)).Minutes);
                decimal intervalSeconds =
                    Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(interval)).Seconds);
                decimal intervalMilliseconds =
                    Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(interval)).Milliseconds);

                decimal slideshowDelayHours =
                    Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(slideshowDelay)).Hours);
                decimal slideshowDelayMinutes =
                    Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(slideshowDelay)).Minutes);
                decimal slideshowDelaySeconds =
                    Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(slideshowDelay)).Seconds);
                decimal slideshowDelayMilliseconds =
                    Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(slideshowDelay)).Milliseconds);

                numericUpDownHoursInterval.Value = intervalHours;
                numericUpDownMinutesInterval.Value = intervalMinutes;
                numericUpDownSecondsInterval.Value = intervalSeconds;
                numericUpDownMillisecondsInterval.Value = intervalMilliseconds;

                numericUpDownSlideshowDelayHours.Value = slideshowDelayHours;
                numericUpDownSlideshowDelayMinutes.Value = slideshowDelayMinutes;
                numericUpDownSlideshowDelaySeconds.Value = slideshowDelaySeconds;
                numericUpDownSlideshowDelayMilliseconds.Value = slideshowDelayMilliseconds;

                numericUpDownSlideSkip.Value = Convert.ToInt32(Settings.User.GetByKey("SlideSkip").Value);
                numericUpDownCaptureLimit.Value = Convert.ToInt32(Settings.User.GetByKey("CaptureLimit").Value);
                numericUpDownImageResolutionRatio.Value =
                    Convert.ToInt32(Settings.User.GetByKey("ImageResolutionRatio").Value);

                checkBoxSlideSkip.Checked = Convert.ToBoolean(Settings.User.GetByKey("SlideSkipCheck").Value);
                checkBoxCaptureLimit.Checked = Convert.ToBoolean(Settings.User.GetByKey("CaptureLimitCheck").Value);
                checkBoxInitialScreenshot.Checked =
                    Convert.ToBoolean(Settings.User.GetByKey("TakeInitialScreenshotCheck").Value);

                checkBoxPassphraseLock.Checked =
                    Convert.ToBoolean(Settings.User.GetByKey("LockScreenCaptureSession").Value);

                textBoxPassphrase.Text = Settings.User.GetByKey("Passphrase").Value.ToString();

                if (textBoxPassphrase.Text.Length > 0)
                {
                    textBoxPassphrase.ReadOnly = true;
                    buttonSetPassphrase.Enabled = false;
                    checkBoxPassphraseLock.Enabled = true;
                }
                else
                {
                    checkBoxPassphraseLock.Checked = false;
                    checkBoxPassphraseLock.Enabled = false;
                }

                toolStripMenuItemShowSystemTrayIcon.Checked =
                    Convert.ToBoolean(Settings.User.GetByKey("ShowSystemTrayIcon").Value);
                toolStripMenuItemStartWhenWindowsStarts.Checked =
                    Convert.ToBoolean(Settings.User.GetByKey("StartWhenWindowsStartsCheck").Value);

                checkBoxScheduleStopAt.Checked = Convert.ToBoolean(Settings.User.GetByKey("CaptureStopAtCheck").Value);
                checkBoxScheduleStartAt.Checked =
                    Convert.ToBoolean(Settings.User.GetByKey("CaptureStartAtCheck").Value);

                checkBoxSaturday.Checked = Convert.ToBoolean(Settings.User.GetByKey("CaptureOnSaturdayCheck").Value);
                checkBoxSunday.Checked = Convert.ToBoolean(Settings.User.GetByKey("CaptureOnSundayCheck").Value);
                checkBoxMonday.Checked = Convert.ToBoolean(Settings.User.GetByKey("CaptureOnMondayCheck").Value);
                checkBoxTuesday.Checked = Convert.ToBoolean(Settings.User.GetByKey("CaptureOnTuesdayCheck").Value);
                checkBoxWednesday.Checked = Convert.ToBoolean(Settings.User.GetByKey("CaptureOnWednesdayCheck").Value);
                checkBoxThursday.Checked = Convert.ToBoolean(Settings.User.GetByKey("CaptureOnThursdayCheck").Value);
                checkBoxFriday.Checked = Convert.ToBoolean(Settings.User.GetByKey("CaptureOnFridayCheck").Value);

                checkBoxScheduleOnTheseDays.Checked =
                    Convert.ToBoolean(Settings.User.GetByKey("CaptureOnTheseDaysCheck").Value);

                dateTimePickerScheduleStopAt.Value =
                    DateTime.Parse(Settings.User.GetByKey("CaptureStopAtValue").Value.ToString());
                dateTimePickerScheduleStartAt.Value =
                    DateTime.Parse(Settings.User.GetByKey("CaptureStartAtValue").Value.ToString());

                textBoxScreen1Name.Text = Settings.User.GetByKey("Screen1Name").Value.ToString();
                textBoxScreen2Name.Text = Settings.User.GetByKey("Screen2Name").Value.ToString();
                textBoxScreen3Name.Text = Settings.User.GetByKey("Screen3Name").Value.ToString();
                textBoxScreen4Name.Text = Settings.User.GetByKey("Screen4Name").Value.ToString();
                textBoxScreenActiveWindowName.Text = Settings.User.GetByKey("Screen5Name").Value.ToString();

                int count = 0;

                if (Screen.AllScreens.Length == 1)
                {
                    tabControlScreens.SelectedTab = tabPageScreen1;
                }

                foreach (Screen screen in Screen.AllScreens)
                {
                    count++;

                    if (count <= ScreenCapture.SCREEN_MAX)
                    {
                        switch (count)
                        {
                            case 1:
                                numericUpDownScreen1X.Value =
                                    Convert.ToInt32(Settings.User.GetByKey("Screen1X").Value) > 0
                                        ? Convert.ToInt32(Settings.User.GetByKey("Screen1X").Value)
                                        : screen.Bounds.X;
                                numericUpDownScreen1Y.Value =
                                    Convert.ToInt32(Settings.User.GetByKey("Screen1Y").Value) > 0
                                        ? Convert.ToInt32(Settings.User.GetByKey("Screen1Y").Value)
                                        : screen.Bounds.Y;
                                numericUpDownScreen1Width.Value =
                                    Convert.ToInt32(Settings.User.GetByKey("Screen1Width").Value) > 0
                                        ? Convert.ToInt32(Settings.User.GetByKey("Screen1Width").Value)
                                        : screen.Bounds.Width;
                                numericUpDownScreen1Height.Value =
                                    Convert.ToInt32(Settings.User.GetByKey("Screen1Height").Value) > 0
                                        ? Convert.ToInt32(Settings.User.GetByKey("Screen1Height").Value)
                                        : screen.Bounds.Height;
                                break;

                            case 2:
                                numericUpDownScreen2X.Value =
                                    Convert.ToInt32(Settings.User.GetByKey("Screen2X").Value) > 0
                                        ? Convert.ToInt32(Settings.User.GetByKey("Screen2X").Value)
                                        : screen.Bounds.X;
                                numericUpDownScreen2Y.Value =
                                    Convert.ToInt32(Settings.User.GetByKey("Screen2Y").Value) > 0
                                        ? Convert.ToInt32(Settings.User.GetByKey("Screen2Y").Value)
                                        : screen.Bounds.Y;
                                numericUpDownScreen2Width.Value =
                                    Convert.ToInt32(Settings.User.GetByKey("Screen2Width").Value) > 0
                                        ? Convert.ToInt32(Settings.User.GetByKey("Screen2Width").Value)
                                        : screen.Bounds.Width;
                                numericUpDownScreen2Height.Value =
                                    Convert.ToInt32(Settings.User.GetByKey("Screen2Height").Value) > 0
                                        ? Convert.ToInt32(Settings.User.GetByKey("Screen2Height").Value)
                                        : screen.Bounds.Height;
                                break;

                            case 3:
                                numericUpDownScreen3X.Value =
                                    Convert.ToInt32(Settings.User.GetByKey("Screen3X").Value) > 0
                                        ? Convert.ToInt32(Settings.User.GetByKey("Screen3X").Value)
                                        : screen.Bounds.X;
                                numericUpDownScreen3Y.Value =
                                    Convert.ToInt32(Settings.User.GetByKey("Screen3Y").Value) > 0
                                        ? Convert.ToInt32(Settings.User.GetByKey("Screen3Y").Value)
                                        : screen.Bounds.Y;
                                numericUpDownScreen3Width.Value =
                                    Convert.ToInt32(Settings.User.GetByKey("Screen3Width").Value) > 0
                                        ? Convert.ToInt32(Settings.User.GetByKey("Screen3Width").Value)
                                        : screen.Bounds.Width;
                                numericUpDownScreen3Height.Value =
                                    Convert.ToInt32(Settings.User.GetByKey("Screen3Height").Value) > 0
                                        ? Convert.ToInt32(Settings.User.GetByKey("Screen3Height").Value)
                                        : screen.Bounds.Height;
                                break;

                            case 4:
                                numericUpDownScreen4X.Value =
                                    Convert.ToInt32(Settings.User.GetByKey("Screen4X").Value) > 0
                                        ? Convert.ToInt32(Settings.User.GetByKey("Screen4X").Value)
                                        : screen.Bounds.X;
                                numericUpDownScreen4Y.Value =
                                    Convert.ToInt32(Settings.User.GetByKey("Screen4Y").Value) > 0
                                        ? Convert.ToInt32(Settings.User.GetByKey("Screen4Y").Value)
                                        : screen.Bounds.Y;
                                numericUpDownScreen4Width.Value =
                                    Convert.ToInt32(Settings.User.GetByKey("Screen4Width").Value) > 0
                                        ? Convert.ToInt32(Settings.User.GetByKey("Screen4Width").Value)
                                        : screen.Bounds.Width;
                                numericUpDownScreen4Height.Value =
                                    Convert.ToInt32(Settings.User.GetByKey("Screen4Height").Value) > 0
                                        ? Convert.ToInt32(Settings.User.GetByKey("Screen4Height").Value)
                                        : screen.Bounds.Height;
                                break;
                        }
                    }
                }

                if (Convert.ToInt32(Settings.User.GetByKey("ImageFormatFilterIndex").Value) < 0)
                {
                    Settings.User.GetByKey("ImageFormatFilterIndex").Value = 0;
                }

                ScreenCapture.ImageFormat = Settings.User.GetByKey("StartButtonImageFormat").Value.ToString();

                numericUpDownJpegQualityLevel.Value = Convert.ToInt32(Settings.User.GetByKey("JpegQualityLevel").Value);

                numericUpDownDaysOld.Value = Convert.ToInt32(Settings.User.GetByKey("DaysOldWhenRemoveSlides").Value);

                checkBoxCaptureScreen1.Checked = Convert.ToBoolean(Settings.User.GetByKey("CaptureScreen1").Value);
                checkBoxCaptureScreen2.Checked = Convert.ToBoolean(Settings.User.GetByKey("CaptureScreen2").Value);
                checkBoxCaptureScreen3.Checked = Convert.ToBoolean(Settings.User.GetByKey("CaptureScreen3").Value);
                checkBoxCaptureScreen4.Checked = Convert.ToBoolean(Settings.User.GetByKey("CaptureScreen4").Value);
                checkBoxCaptureActiveWindow.Checked =
                    Convert.ToBoolean(Settings.User.GetByKey("CaptureActiveWindow").Value);

                checkBoxAutoReset.Checked = Convert.ToBoolean(Settings.User.GetByKey("AutoReset").Value);

                checkBoxMouse.Checked = Convert.ToBoolean(Settings.User.GetByKey("Mouse").Value);

                EnableStartScreenCapture();

                CaptureLimitCheck();
            }
            catch (Exception ex)
            {
                Log.Write("FormMain::LoadSettings", ex);
            }
        }

        /// <summary>
        /// Saves the user's current settings so we can load them at a later time when the user executes the application.
        /// </summary>
        private void SaveSettings()
        {
            if (runSaveSettings == null)
            {
                runSaveSettings = new BackgroundWorker
                {
                    WorkerReportsProgress = false,
                    WorkerSupportsCancellation = true
                };
                runSaveSettings.DoWork += new DoWorkEventHandler(DoWork_runSaveSettingsThread);
            }
            else
            {
                if (!runSaveSettings.IsBusy)
                {
                    runSaveSettings.RunWorkerAsync();
                }
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
                // Hide the system tray icon.
                notifyIcon.Visible = false;

                HideInterface();

                if (runDateSearchThread != null && runDateSearchThread.IsBusy)
                {
                    runDateSearchThread.CancelAsync();
                }

                if (runSlideSearchThread != null && runSlideSearchThread.IsBusy)
                {
                    runSlideSearchThread.CancelAsync();
                }

                if (runDeleteSlidesThread != null && runDeleteSlidesThread.IsBusy)
                {
                    runDeleteSlidesThread.CancelAsync();
                }

                formEditor.EditorCollection.Save();

                ScreenshotCollection.Save();

                formTrigger.TriggerCollection.Save();

                formRegion.RegionCollection.Save();

                // Exit.
                Environment.Exit(0);
            }
            else
            {
                SaveSettings();

                RunTriggersOfConditionType(TriggerConditionType.InterfaceClosing);

                // If there isn't a Trigger for "InterfaceClosing" that performs an action
                // then make sure we cancel this event so that nothing happens. We want the user
                // to use a Trigger, and decide what they want to do, when closing the interface window.
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Set the image format and search for slides whenever the image format filter gets changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectedIndexChanged_toolStripComboBoxImageFormatFilter(object sender, EventArgs e)
        {
            if (toolStripComboBoxImageFormatFilter.SelectedIndex == 0)
            {
                Settings.User.GetByKey("ImageFormatFilter").Value = "*.*";
            }

            Regex rgx = new Regex(@"^(?<ImageFormatFilter>\*\.(?<ImageFormat>([a-z]{3,4})))$");

            if (rgx.IsMatch(toolStripComboBoxImageFormatFilter.Items[toolStripComboBoxImageFormatFilter.SelectedIndex].ToString()))
            {
                Settings.User.GetByKey("ImageFormatFilter").Value = rgx.Match(toolStripComboBoxImageFormatFilter.Items[toolStripComboBoxImageFormatFilter.SelectedIndex].ToString()).Groups["ImageFormatFilter"].Value;
            }

            Settings.User.GetByKey("ImageFormatFilterIndex").Value = toolStripComboBoxImageFormatFilter.SelectedIndex;

            SearchSlides();
        }

        /// <summary>
        /// Just in case the user gives us an empty folder path or forgets to include the trailing backslash.
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        private string CorrectDirectoryPath(string folder)
        {
            if (folder.Length == 0)
            {
                folder = FileSystem.ScreenshotsFolder;
            }

            if (!folder.EndsWith(@"\"))
            {
                folder += @"\";
            }

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            Directory.SetCurrentDirectory(folder);

            return folder;
        }

        /// <summary>
        /// Search for all the date-stamped folders storing slides. They should be in the format yyyy-mm-dd.
        /// Any folders found matching this format are then bolded in the calendar so the user
        /// understands that these were the days when screen capture sessions had been running.
        /// </summary>
        private void SearchDates()
        {
            ClearPreview();
            DisableToolStripButtons();

            monthCalendar.BoldedDates = null;

            if (runDateSearchThread != null && !runDateSearchThread.IsBusy)
            {
                runDateSearchThread.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Deletes slides.
        /// </summary>
        private void DeleteSlides()
        {
            if (runDeleteSlidesThread != null && !runDeleteSlidesThread.IsBusy)
            {
                runDeleteSlidesThread.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Searches for slides.
        /// </summary>
        private void SearchSlides()
        {
            listBoxSlides.BeginUpdate();

            ClearPreview();
            DisableToolStripButtons();

            if (runSlideSearchThread != null && !runSlideSearchThread.IsBusy)
            {
                runSlideSearchThread.RunWorkerAsync();
            }

            listBoxSlides.EndUpdate();

            // It's very important here to disable all the slideshow controls if there were
            // no files found. There's no point keeping the controls enabled if there are no files.
            if (listBoxSlides.Items.Count == 0)
            {
                toolStripButtonFirstSlide.Enabled = false;
                toolStripButtonPreviousSlide.Enabled = false;
                toolStripButtonPlaySlideshow.Enabled = false;
                toolStripButtonNextSlide.Enabled = false;
                toolStripButtonLastSlide.Enabled = false;
            }
        }

        /// <summary>
        /// This thread is responsible for finding slides (copies of screenshots that were taken on particular days)
        /// so we can import them into the slideshow ready for the user to play through what they were doing on the computer.
        /// </summary>
        /// <param name="e"></param>
        private void RunSlideSearch(DoWorkEventArgs e)
        {
            if (listBoxSlides.InvokeRequired)
            {
                listBoxSlides.Invoke(new RunSlideSearchDelegate(RunSlideSearch), new object[] { e });
            }
            else
            {
                string[] files = FileSystem.GetFiles(FileSystem.SlidesFolder, monthCalendar.SelectionStart.ToString(MacroParser.DateFormat));

                if (files != null && files.Length > 0)
                {
                    listBoxSlides.Items.AddRange(files);
                }

                // If we do find files representing slides then make sure the user can use the slideshow.
                if (listBoxSlides.Items.Count > 0)
                {
                    monthCalendar.Enabled = true;
                    toolStripComboBoxImageFormatFilter.Enabled = true;

                    listBoxSlides.SelectedIndex = listBoxSlides.Items.Count - 1;

                    toolStripButtonNextSlide.Enabled = true;
                    toolStripButtonLastSlide.Enabled = true;

                    EnablePlaySlideshow();

                    UpdatePreview();
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
                monthCalendar.Invoke(new RunDateSearchDelegate(RunDateSearch), new object[] { e });
            }
            else
            {
                ArrayList selectedFolders = new ArrayList();

                string[] dirs = Directory.GetDirectories(FileSystem.SlidesFolder);

                // Go through each directory found and make sure that the sub-directories match with the format yyyy-MM-dd.
                for (int i = 0; i < dirs.Length; i++)
                {
                    Regex rgx = new Regex(@"^(?<Year>\d{4})-(?<Month>\d{2})-(?<Day>\d{2})$");

                    string dirPath = Path.GetFileName(dirs[i]);

                    if (rgx.IsMatch(dirPath) && Directory.Exists(dirs[i]) && Directory.GetFiles(dirs[i]).Length > 0 && !selectedFolders.Contains(Path.GetFileName(dirs[i]).ToString()))
                    {
                        selectedFolders.Add(Path.GetFileName(dirs[i]).ToString());
                    }
                }

                // Also make sure that the dates in the calendar are set to bold for each
                // of the folders that are found.
                DateTime[] boldedDates = new DateTime[selectedFolders.Count];

                for (int i = 0; i < selectedFolders.Count; i++)
                {
                    boldedDates.SetValue(ConvertFilterToDateTime(selectedFolders[i].ToString()), i);
                }

                monthCalendar.BoldedDates = boldedDates;
            }
        }

        /// <summary>
        /// This thread is responsible for deleting slides older than a specified number of days.
        /// </summary>
        /// <param name="e"></param>
        private void RunDeleteSlides(DoWorkEventArgs e)
        {
            string[] dirs = Directory.GetDirectories(FileSystem.SlidesFolder);

            for (int i = 0; i < dirs.Length; i++)
            {
                Regex rgx = new Regex(@"^(?<Year>\d{4})-(?<Month>\d{2})-(?<Day>\d{2})$");

                string dirPath = Path.GetFileName(dirs[i]);

                if (rgx.IsMatch(dirPath))
                {
                    DateTime dateTimeOfDir = new DateTime(Convert.ToInt32(rgx.Match(dirPath).Groups["Year"].Value),
                                    Convert.ToInt32(rgx.Match(dirPath).Groups["Month"].Value),
                                    Convert.ToInt32(rgx.Match(dirPath).Groups["Day"].Value));

                    int daysToSubtract = (int)numericUpDownDaysOld.Value;

                    if (daysToSubtract > 0 && dateTimeOfDir <= DateTime.Now.Date.AddDays(-daysToSubtract))
                    {
                        FileSystem.DeleteFilesInFolder(dirs[i]);
                    }
                }
            }
        }

        private void SaveSettings(DoWorkEventArgs e)
        {
            try
            {
                if (textBoxFolder.InvokeRequired)
                {
                    textBoxFolder.Invoke(new SaveSettingsDelegate(SaveSettings), new object[] { e });
                }
                else
                {
                    Log.Write("Saving settings.");

                    Settings.User.GetByKey("ScreenshotsDirectory").Value = CorrectDirectoryPath(textBoxFolder.Text);
                    Settings.User.GetByKey("ScheduleImageFormat").Value = comboBoxScheduleImageFormat.Text;
                    Settings.User.GetByKey("SlideSkip").Value = numericUpDownSlideSkip.Value;
                    Settings.User.GetByKey("CaptureLimit").Value = numericUpDownCaptureLimit.Value;
                    Settings.User.GetByKey("ImageResolutionRatio").Value = numericUpDownImageResolutionRatio.Value;
                    Settings.User.GetByKey("ImageFormatFilterIndex").Value = toolStripComboBoxImageFormatFilter.SelectedIndex;
                    Settings.User.GetByKey("Interval").Value = GetCaptureDelay();
                    Settings.User.GetByKey("SlideshowDelay").Value = GetSlideshowDelay();
                    Settings.User.GetByKey("SlideSkipCheck").Value = checkBoxSlideSkip.Checked;
                    Settings.User.GetByKey("CaptureLimitCheck").Value = checkBoxCaptureLimit.Checked;
                    Settings.User.GetByKey("TakeInitialScreenshotCheck").Value = checkBoxInitialScreenshot.Checked;
                    Settings.User.GetByKey("ShowSystemTrayIcon").Value = toolStripMenuItemShowSystemTrayIcon.Checked;
                    Settings.User.GetByKey("StartWhenWindowsStartsCheck").Value = toolStripMenuItemStartWhenWindowsStarts.Checked;
                    Settings.User.GetByKey("CaptureStopAtCheck").Value = checkBoxScheduleStopAt.Checked;
                    Settings.User.GetByKey("CaptureStartAtCheck").Value = checkBoxScheduleStartAt.Checked;
                    Settings.User.GetByKey("CaptureOnSundayCheck").Value = checkBoxSunday.Checked;
                    Settings.User.GetByKey("CaptureOnMondayCheck").Value = checkBoxMonday.Checked;
                    Settings.User.GetByKey("CaptureOnTuesdayCheck").Value = checkBoxTuesday.Checked;
                    Settings.User.GetByKey("CaptureOnWednesdayCheck").Value = checkBoxWednesday.Checked;
                    Settings.User.GetByKey("CaptureOnThursdayCheck").Value = checkBoxThursday.Checked;
                    Settings.User.GetByKey("CaptureOnFridayCheck").Value = checkBoxFriday.Checked;
                    Settings.User.GetByKey("CaptureOnSaturdayCheck").Value = checkBoxSaturday.Checked;
                    Settings.User.GetByKey("CaptureOnTheseDaysCheck").Value = checkBoxScheduleOnTheseDays.Checked;
                    Settings.User.GetByKey("CaptureStopAtValue").Value = dateTimePickerScheduleStopAt.Value;
                    Settings.User.GetByKey("CaptureStartAtValue").Value = dateTimePickerScheduleStartAt.Value;
                    Settings.User.GetByKey("Screen1X").Value = numericUpDownScreen1X.Value;
                    Settings.User.GetByKey("Screen1Y").Value = numericUpDownScreen1Y.Value;
                    Settings.User.GetByKey("Screen1Width").Value = numericUpDownScreen1Width.Value;
                    Settings.User.GetByKey("Screen1Height").Value = numericUpDownScreen1Height.Value;
                    Settings.User.GetByKey("Screen2X").Value = numericUpDownScreen2X.Value;
                    Settings.User.GetByKey("Screen2Y").Value = numericUpDownScreen2Y.Value;
                    Settings.User.GetByKey("Screen2Width").Value = numericUpDownScreen2Width.Value;
                    Settings.User.GetByKey("Screen2Height").Value = numericUpDownScreen2Height.Value;
                    Settings.User.GetByKey("Screen3X").Value = numericUpDownScreen3X.Value;
                    Settings.User.GetByKey("Screen3Y").Value = numericUpDownScreen3Y.Value;
                    Settings.User.GetByKey("Screen3Width").Value = numericUpDownScreen3Width.Value;
                    Settings.User.GetByKey("Screen3Height").Value = numericUpDownScreen3Height.Value;
                    Settings.User.GetByKey("Screen4X").Value = numericUpDownScreen4X.Value;
                    Settings.User.GetByKey("Screen4Y").Value = numericUpDownScreen4Y.Value;
                    Settings.User.GetByKey("Screen4Width").Value = numericUpDownScreen4Width.Value;
                    Settings.User.GetByKey("Screen4Height").Value = numericUpDownScreen4Height.Value;
                    Settings.User.GetByKey("Screen1Name").Value = textBoxScreen1Name.Text;
                    Settings.User.GetByKey("Screen2Name").Value = textBoxScreen2Name.Text;
                    Settings.User.GetByKey("Screen3Name").Value = textBoxScreen3Name.Text;
                    Settings.User.GetByKey("Screen4Name").Value = textBoxScreen4Name.Text;
                    Settings.User.GetByKey("Screen5Name").Value = textBoxScreenActiveWindowName.Text;
                    Settings.User.GetByKey("LockScreenCaptureSession").Value = checkBoxPassphraseLock.Checked;
                    Settings.User.GetByKey("Macro").Value = textBoxMacro.Text;
                    Settings.User.GetByKey("JpegQualityLevel").Value = numericUpDownJpegQualityLevel.Value;
                    Settings.User.GetByKey("DaysOldWhenRemoveSlides").Value = numericUpDownDaysOld.Value;
                    Settings.User.GetByKey("CaptureScreen1").Value = checkBoxCaptureScreen1.Checked;
                    Settings.User.GetByKey("CaptureScreen2").Value = checkBoxCaptureScreen2.Checked;
                    Settings.User.GetByKey("CaptureScreen3").Value = checkBoxCaptureScreen3.Checked;
                    Settings.User.GetByKey("CaptureScreen4").Value = checkBoxCaptureScreen4.Checked;
                    Settings.User.GetByKey("CaptureActiveWindow").Value = checkBoxCaptureActiveWindow.Checked;
                    Settings.User.GetByKey("AutoReset").Value = checkBoxAutoReset.Checked;
                    Settings.User.GetByKey("Mouse").Value = checkBoxMouse.Checked;
                    Settings.User.GetByKey("StartButtonImageFormat").Value = ScreenCapture.ImageFormat;
                    Settings.User.GetByKey("Passphrase").Value = textBoxPassphrase.Text;

                    Settings.User.Save();

                    Log.Write("Settings saved.");
                }
            }
            catch (Exception ex)
            {
                Log.Write("FormMain::RunSaveApplicationSettings", ex);
            }
        }

        /// <summary>
        /// Converts the filter string into a DateTime object. Used by the RunDateSearch thread so we can set bolded dates in the calendar.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private DateTime ConvertFilterToDateTime(string filter)
        {
            return new DateTime(Convert.ToInt32(filter.Substring(0, 4)), Convert.ToInt32(filter.Substring(5, 2)), Convert.ToInt32(filter.Substring(8, 2)));
        }

        /// <summary>
        /// Shows the interface.
        /// </summary>
        private void ShowInterface()
        {
            Log.Write("Showing interface.");

            SearchDates();
            SearchSlides();

            if (ScreenCapture.LockScreenCaptureSession && !formEnterPassphrase.Visible)
            {
                formEnterPassphrase.ShowDialog(this);
            }

            // This is intentional. Do not rewrite these statements as an if/else
            // because as soon as lockScreenCaptureSession is set to false we want
            // to continue with normal functionality.
            if (!ScreenCapture.LockScreenCaptureSession)
            {
                checkBoxPassphraseLock.Checked = false;
                Settings.User.GetByKey("LockScreenCaptureSession").Value = false;
                SaveSettings();

                Opacity = 100;
                toolStripMenuItemShowInterface.Enabled = false;
                toolStripMenuItemHideInterface.Enabled = true;

                Show();

                Visible = true;
                ShowInTaskbar = true;

                // If the window is mimimized then show it when the user wants to open the window.
                if (WindowState == FormWindowState.Minimized)
                {
                    WindowState = FormWindowState.Normal;
                }

                RunTriggersOfConditionType(TriggerConditionType.InterfaceShowing);

                Focus();
            }
        }

        /// <summary>
        /// Hides the interface.
        /// </summary>
        private void HideInterface()
        {
            SaveSettings();

            Log.Write("Hiding interface.");

            // Pause the slideshow if you find it still playing.
            if (Slideshow.Playing)
            {
                PauseSlideshow();
            }

            Opacity = 0;
            toolStripMenuItemShowInterface.Enabled = true;
            toolStripMenuItemHideInterface.Enabled = false;

            Hide();
            Visible = false;
            ShowInTaskbar = false;

            RunTriggersOfConditionType(TriggerConditionType.InterfaceHiding);
        }

        /// <summary>
        /// Stops the screen capture session that's currently running.
        /// </summary>
        private void StopScreenCapture()
        {
            if (timerScreenCapture.Enabled)
            {
                Log.Write("Stopping screen capture.");

                if (ScreenCapture.LockScreenCaptureSession && !formEnterPassphrase.Visible)
                {
                    formEnterPassphrase.ShowDialog(this);
                }

                // This is intentional. Do not rewrite these statements as an if/else
                // because as soon as lockScreenCaptureSession is set to false we want
                // to continue with normal functionality.
                if (!ScreenCapture.LockScreenCaptureSession)
                {
                    checkBoxPassphraseLock.Checked = false;
                    Settings.User.GetByKey("LockScreenCaptureSession").Value = false;
                    SaveSettings();

                    ScreenCapture.Count = 0;
                    timerScreenCapture.Enabled = false;

                    ScreenCapture.Running = false;

                    DisableStopScreenCapture();
                    EnableStartScreenCapture();

                    SearchDates();
                    SearchSlides();

                    RunTriggersOfConditionType(TriggerConditionType.ScreenCaptureStopped);
                }
            }
        }

        /// <summary>
        /// Plays the slideshow.
        /// </summary>
        private void PlaySlideshow()
        {
            int slideshowDelay = GetSlideshowDelay();

            DisableControls();

            if (listBoxSlides.Items.Count > 0 && slideshowDelay > 0)
            {
                if (Slideshow.Index == Slideshow.Count - 1)
                {
                    Slideshow.First();
                    listBoxSlides.SelectedIndex = Slideshow.Index;
                }

                toolStripButtonPlaySlideshow.Image = Properties.Resources.player_pause;

                Slideshow.Interval = slideshowDelay;
                Slideshow.SlideSkipCheck = checkBoxSlideSkip.Checked;
                Slideshow.SlideSkip = (int)numericUpDownSlideSkip.Value;

                Slideshow.Play();
            }
        }

        /// <summary>
        /// Pauses the slideshow.
        /// </summary>
        private void PauseSlideshow()
        {
            EnableControls();

            if (listBoxSlides.Items.Count > 0)
            {
                toolStripButtonPlaySlideshow.Image = Properties.Resources.player_play;

                Slideshow.Stop();
            }
        }

        /// <summary>
        /// Stops the slideshow.
        /// </summary>
        private void StopSlideshow()
        {
            EnableControls();

            toolStripButtonPlaySlideshow.Image = Properties.Resources.player_play;

            Slideshow.Stop();
        }

        private void StartScreenCapture()
        {
            StartScreenCapture(ScreenCapture.ImageFormat);
        }

        /// <summary>
        /// Starts a screen capture session.
        /// </summary>
        private void StartScreenCapture(string format)
        {
            if (!timerScreenCapture.Enabled)
            {
                SaveSettings();

                if (!string.IsNullOrEmpty(textBoxFolder.Text) && Directory.Exists(textBoxFolder.Text))
                {
                    Log.Write("Starting new screen capture session in \"" + textBoxFolder.Text + "\"");

                    textBoxFolder.Text = CorrectDirectoryPath(textBoxFolder.Text);

                    Log.Write("Macro being used is \"" + textBoxMacro.Text + "\"");

                    // Stop the slideshow if it's currently playing.
                    if (Slideshow.Playing)
                    {
                        Slideshow.Stop();
                    }

                    // Stop the folder search thread if it's busy.
                    if (runDateSearchThread != null && runDateSearchThread.IsBusy)
                    {
                        runDateSearchThread.CancelAsync();
                    }

                    // Stop the file search thread if it's busy.
                    if (runSlideSearchThread != null && runSlideSearchThread.IsBusy)
                    {
                        runSlideSearchThread.CancelAsync();
                    }

                    DisableStartScreenCapture();
                    EnableStopScreenCapture();

                    // Setup the properties for the screen capture class.
                    ScreenCapture.Folder = textBoxFolder.Text;
                    ScreenCapture.Macro = textBoxMacro.Text;
                    ScreenCapture.ImageFormat = format;
                    ScreenCapture.Delay = GetCaptureDelay();
                    ScreenCapture.Limit = checkBoxCaptureLimit.Checked ? (int)numericUpDownCaptureLimit.Value : 0;
                    ScreenCapture.Ratio = (int)numericUpDownImageResolutionRatio.Value;

                    if (checkBoxPassphraseLock.Checked)
                    {
                        ScreenCapture.LockScreenCaptureSession = true;
                    }
                    else
                    {
                        ScreenCapture.LockScreenCaptureSession = false;
                    }

                    ScreenCapture.Running = true;

                    ScreenCapture.DateTimeStartCapture = DateTime.Now;

                    RunTriggersOfConditionType(TriggerConditionType.ScreenCaptureStarted);

                    if (checkBoxInitialScreenshot.Checked)
                    {
                        Log.Write("Taking initial screenshots.");

                        TakeScreenshot();
                    }

                    // Start taking screenshots.
                    Log.Write("Starting screen capture.");

                    timerScreenCapture.Interval = GetCaptureDelay();
                    timerScreenCapture.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Whenever the user clicks on a screenshot in the list of screenshots then make sure to update the preview of screenshots.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectedIndexChanged_listBoxSlides(object sender, EventArgs e)
        {
            Slideshow.Index = listBoxSlides.SelectedIndex;
            Slideshow.Count = listBoxSlides.Items.Count;

            UpdatePreview();
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
        /// Returns the screen capture delay. This value will be used as the screen capture timer's interval property.
        /// </summary>
        /// <returns></returns>
        private int GetCaptureDelay()
        {
            return ConvertIntoMilliseconds((int)numericUpDownHoursInterval.Value, (int)numericUpDownMinutesInterval.Value, (int)numericUpDownSecondsInterval.Value, (int)numericUpDownMillisecondsInterval.Value);
        }

        /// <summary>
        /// Returns the slideshow delay. This value will be used as the slideshow timer's interval property.
        /// </summary>
        /// <returns></returns>
        private int GetSlideshowDelay()
        {
            return ConvertIntoMilliseconds((int)numericUpDownSlideshowDelayHours.Value, (int)numericUpDownSlideshowDelayMinutes.Value, (int)numericUpDownSlideshowDelaySeconds.Value, (int)numericUpDownSlideshowDelayMilliseconds.Value);
        }

        /// <summary>
        /// Disables the appropriate controls when playing the slideshow.
        /// </summary>
        private void DisableControls()
        {
            monthCalendar.Enabled = false;
            toolStripComboBoxImageFormatFilter.Enabled = false;

            numericUpDownSlideshowDelayHours.Enabled = false;
            numericUpDownSlideshowDelayMinutes.Enabled = false;
            numericUpDownSlideshowDelaySeconds.Enabled = false;
            numericUpDownSlideshowDelayMilliseconds.Enabled = false;

            numericUpDownSlideSkip.Enabled = false;
            checkBoxSlideSkip.Enabled = false;

            toolStripButtonPreview.Enabled = false;
            toolStripMenuItemStartScreenCapture.Enabled = false;
            toolStripSplitButtonStartScreenCapture.Enabled = false;
        }

        /// <summary>
        /// Enables the appropriate controls when the slideshow is paused or stopped.
        /// </summary>
        private void EnableControls()
        {
            monthCalendar.Enabled = true;
            toolStripComboBoxImageFormatFilter.Enabled = true;

            numericUpDownSlideshowDelayHours.Enabled = true;
            numericUpDownSlideshowDelayMinutes.Enabled = true;
            numericUpDownSlideshowDelaySeconds.Enabled = true;
            numericUpDownSlideshowDelayMilliseconds.Enabled = true;

            numericUpDownSlideSkip.Enabled = true;
            checkBoxSlideSkip.Enabled = true;

            if (!timerScreenCapture.Enabled)
            {
                toolStripButtonPreview.Enabled = true;
                toolStripMenuItemStartScreenCapture.Enabled = true;
                toolStripSplitButtonStartScreenCapture.Enabled = true;
            }
        }

        /// <summary>
        /// Clears the screenshots preview when searching for files and folders.
        /// </summary>
        private void ClearPreview()
        {
            Slideshow.Clear();
            listBoxSlides.Items.Clear();
        }

        /// <summary>
        /// Disables the tool strip buttons when searching for files and folders.
        /// </summary>
        private void DisableToolStripButtons()
        {
            toolStripButtonFirstSlide.Enabled = false;
            toolStripButtonPreviousSlide.Enabled = false;
            toolStripButtonPlaySlideshow.Enabled = false;
            toolStripButtonNextSlide.Enabled = false;
            toolStripButtonLastSlide.Enabled = false;
        }

        /// <summary>
        /// Updates the screenshots preview.
        /// </summary>
        private void UpdatePreview()
        {
            if (!toolStripButtonPreview.Checked)
            {
                if (InvokeRequired)
                {
                    Invoke(new UpdateScreenshotPreviewDelegate(UpdatePreview));
                }
                else
                {
                    if (Slideshow.Index == (Slideshow.Count - 1))
                    {
                        StopSlideshow();
                    }

                    if (Slideshow.Index == 0)
                    {
                        toolStripButtonFirstSlide.Enabled = false;
                        toolStripButtonPreviousSlide.Enabled = false;
                    }
                    else
                    {
                        toolStripButtonFirstSlide.Enabled = true;
                        toolStripButtonPreviousSlide.Enabled = true;
                    }

                    if ((Slideshow.Count - 1) <= Slideshow.Index)
                    {
                        toolStripButtonNextSlide.Enabled = false;
                        toolStripButtonLastSlide.Enabled = false;
                    }
                    else
                    {
                        toolStripButtonNextSlide.Enabled = true;
                        toolStripButtonLastSlide.Enabled = true;
                    }

                    if (Slideshow.Index >= 0 && Slideshow.Index <= (Slideshow.Count - 1))
                    {
                        Slideshow.SelectedSlide = listBoxSlides.Items[Slideshow.Index].ToString();

                        DisplayImages(false);
                    }
                }
            }
        }

        /// <summary>
        /// Loads the image format filter drop down menu for the Slideshow module.
        /// </summary>
        private void LoadImageFormatFilterDropDownMenu()
        {
            toolStripComboBoxImageFormatFilter.Items.Clear();
            toolStripComboBoxImageFormatFilter.Items.Add("*.*");

            foreach (ImageFormat imageFormat in _imageFormatCollection)
            {
                toolStripComboBoxImageFormatFilter.Items.Add("*" + imageFormat.Extension);
            }
        }

        /// <summary>
        /// Shows the slideshow.
        /// </summary>
        private void ShowSlideshow()
        {
            if (Slideshow.Playing)
            {
                Slideshow.Stop();
            }

            LoadImageFormatFilterDropDownMenu();

            tabControlModules.SelectedTab = tabControlModules.TabPages["tabPageSlideshow"];

            if ((int)Settings.User.GetByKey("ImageFormatFilterIndex").Value < 0)
            {
                Settings.User.GetByKey("ImageFormatFilterIndex").Value = 0;
            }

            toolStripComboBoxImageFormatFilter.SelectedIndex = (int)Settings.User.GetByKey("ImageFormatFilterIndex").Value;
        }

        /// <summary>
        /// Shows the slideshow when a date on the calendar has been selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateSelected_monthCalendar(object sender, DateRangeEventArgs e)
        {
            ShowSlideshow();
        }

        /// <summary>
        /// Shows the first slide in the slideshow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_toolStripButtonFirstSlide(object sender, EventArgs e)
        {
            Slideshow.First();
            listBoxSlides.SelectedIndex = Slideshow.Index;
        }

        /// <summary>
        /// Shows the previous slide in the slideshow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_toolStripButtonPreviousSlide(object sender, EventArgs e)
        {
            Slideshow.Previous();
            listBoxSlides.SelectedIndex = Slideshow.Index;
        }

        /// <summary>
        /// Plays the slideshow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_toolStripButtonPlaySlideshow(object sender, EventArgs e)
        {
            if (Slideshow.Playing)
            {
                PauseSlideshow();
            }
            else
            {
                PlaySlideshow();
            }
        }

        /// <summary>
        /// Shows the next slide in the slideshow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_toolStripButtonNextSlide(object sender, EventArgs e)
        {
            Slideshow.Next();
            listBoxSlides.SelectedIndex = Slideshow.Index;
        }

        /// <summary>
        /// Shows the last slide in the slideshow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_toolStripButtonLastSlide(object sender, EventArgs e)
        {
            Slideshow.Last();
            listBoxSlides.SelectedIndex = Slideshow.Index;
        }

        /// <summary>
        /// Starts a screen capture session based on the image format selected by the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_toolStripMenuItemStartScreenCapture(object sender, EventArgs e)
        {
            string imageFormat = ScreenCapture.DefaultImageFormat;

            if (!sender.ToString().Equals(toolStripSplitButtonStartScreenCapture.Text))
            {
                imageFormat = sender.ToString();
            }

            if (!string.IsNullOrEmpty(imageFormat))
            {
                StartScreenCapture(imageFormat);
            }
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
        /// Exits the application.
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
            Log.Write("Exiting application.");

            if (ScreenCapture.LockScreenCaptureSession && !formEnterPassphrase.Visible)
            {
                formEnterPassphrase.ShowDialog(this);
            }

            // This is intentional. Do not rewrite these statements as an if/else
            // because as soon as lockScreenCaptureSession is set to false we want
            // to continue with normal functionality.
            if (!ScreenCapture.LockScreenCaptureSession)
            {
                RunTriggersOfConditionType(TriggerConditionType.ApplicationExit);

                checkBoxPassphraseLock.Checked = false;
                Settings.User.GetByKey("LockScreenCaptureSession").Value = false;
                SaveSettings();

                // Hide the system tray icon.
                notifyIcon.Visible = false;

                HideInterface();

                if (runDateSearchThread != null && runDateSearchThread.IsBusy)
                {
                    runDateSearchThread.CancelAsync();
                }

                if (runSlideSearchThread != null && runSlideSearchThread.IsBusy)
                {
                    runSlideSearchThread.CancelAsync();
                }

                if (runDeleteSlidesThread != null && runDeleteSlidesThread.IsBusy)
                {
                    runDeleteSlidesThread.CancelAsync();
                }

                formEditor.EditorCollection.Save();

                ScreenshotCollection.Save();

                formTrigger.TriggerCollection.Save();

                formRegion.RegionCollection.Save();

                // Exit.
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Runs the slide search thread.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoWork_runSlideSearchThread(object sender, DoWorkEventArgs e)
        {
            RunSlideSearch(e);
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

        private void DoWork_runSaveSettingsThread(object sender, DoWorkEventArgs e)
        {
            SaveSettings(e);
        }

        /// <summary>
        /// Updates the list of screenshots with the current slideshow index when the slideshow is playing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Slideshow_Playing(object sender, EventArgs e)
        {
            listBoxSlides.SelectedIndex = Slideshow.Index;
        }

        /// <summary>
        /// Figures out if the "Play Slideshow" control should be enabled or disabled.
        /// </summary>
        private void EnablePlaySlideshow()
        {
            if (GetSlideshowDelay() > 0 && listBoxSlides.Items.Count > 0)
            {
                toolStripButtonPlaySlideshow.Enabled = true;
            }
            else
            {
                toolStripButtonPlaySlideshow.Enabled = false;
            }
        }

        /// <summary>
        /// Figures out if the "Start Screen Capture" controls should be enabled or disabled.
        /// </summary>
        private void EnableStartScreenCapture()
        {
            if (GetCaptureDelay() > 0)
            {
                toolStripMenuItemStartScreenCapture.Enabled = true;
                toolStripSplitButtonStartScreenCapture.Enabled = true;
                toolStripButtonPreview.Enabled = true;
                textBoxFolder.Enabled = true;
                textBoxMacro.Enabled = true;
                buttonBrowseFolder.Enabled = true;

                numericUpDownHoursInterval.Enabled = true;
                checkBoxInitialScreenshot.Enabled = true;
                numericUpDownMinutesInterval.Enabled = true;
                checkBoxCaptureLimit.Enabled = true;
                numericUpDownCaptureLimit.Enabled = true;
                numericUpDownSecondsInterval.Enabled = true;
                numericUpDownImageResolutionRatio.Enabled = true;
                numericUpDownMillisecondsInterval.Enabled = true;
                numericUpDownJpegQualityLevel.Enabled = true;
                toolStripButtonPreview.Enabled = true;

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
                comboBoxScheduleImageFormat.Enabled = true;
                dateTimePickerScheduleStartAt.Enabled = true;
                dateTimePickerScheduleStopAt.Enabled = true;
            }
            else
            {
                DisableStartScreenCapture();
            }
        }

        /// <summary>
        /// Enables the "Stop Screen Capture" controls.
        /// </summary>
        private void EnableStopScreenCapture()
        {
            toolStripButtonStopScreenCapture.Enabled = true;
            toolStripMenuItemStopScreenCapture.Enabled = true;

            numericUpDownHoursInterval.Enabled = false;
            checkBoxInitialScreenshot.Enabled = false;
            numericUpDownMinutesInterval.Enabled = false;
            checkBoxCaptureLimit.Enabled = false;
            numericUpDownCaptureLimit.Enabled = false;
            numericUpDownSecondsInterval.Enabled = false;
            numericUpDownImageResolutionRatio.Enabled = false;
            numericUpDownMillisecondsInterval.Enabled = false;
            numericUpDownJpegQualityLevel.Enabled = false;
            toolStripButtonPreview.Enabled = false;

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
            comboBoxScheduleImageFormat.Enabled = false;
            dateTimePickerScheduleStartAt.Enabled = false;
            dateTimePickerScheduleStopAt.Enabled = false;
        }

        /// <summary>
        /// Disables the "Stop Screen Capture" controls.
        /// </summary>
        private void DisableStopScreenCapture()
        {
            toolStripButtonStopScreenCapture.Enabled = false;
            toolStripMenuItemStopScreenCapture.Enabled = false;
        }

        /// <summary>
        /// Disables the "Start Screen Capture" controls.
        /// </summary>
        private void DisableStartScreenCapture()
        {
            toolStripMenuItemStartScreenCapture.Enabled = false;
            toolStripSplitButtonStartScreenCapture.Enabled = false;
            toolStripButtonPreview.Enabled = false;
            textBoxFolder.Enabled = false;
            textBoxMacro.Enabled = false;
            buttonBrowseFolder.Enabled = false;
        }

        /// <summary>
        /// Plays the slideshow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClick_toolStripButtonPlaySlideshow(object sender, EventArgs e)
        {
            PlaySlideshow();
        }

        /// <summary>
        /// Opens the standard Windows folder browser for the user to choose a folder for containing the screenshots.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_buttonBrowseFolder(object sender, EventArgs e)
        {
            FolderBrowserDialog browser = new FolderBrowserDialog();

            if (browser.ShowDialog() == DialogResult.OK)
            {
                textBoxFolder.Text = browser.SelectedPath;
            }
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
            MessageBox.Show(Settings.Application.GetByKey("Name").Value + " " + Settings.Application.GetByKey("Version").Value + " (\"Clara\")\nDeveloped by Gavin Kendall (2008 - 2018)", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Opens Windows Explorer to show the location of the selected slide.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_toolStripMenuItemShowSlideLocation(object sender, EventArgs e)
        {
            if (listBoxSlides.SelectedIndex > -1)
            {
                string selectedSlide = FileSystem.GetImageFilePath(Slideshow.SelectedSlide, Slideshow.SelectedScreen == 0 ? 1 : Slideshow.SelectedScreen);

                if (File.Exists(selectedSlide))
                {
                    Process.Start(FileSystem.FileManager, "/select,\"" + selectedSlide + "\"");
                }
            }
        }

        /// <summary>
        /// Opens Windows Explorer to show the location of the selected screenshot.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_toolStripMenuItemShowScreenshotLocation(object sender, EventArgs e)
        {
            if (listBoxSlides.SelectedIndex > -1)
            {
                Screenshot selectedScreenshot = ScreenshotCollection.GetBySlidename(Slideshow.SelectedSlide, Slideshow.SelectedScreen == 0 ? 1 : Slideshow.SelectedScreen);

                if (selectedScreenshot != null && !string.IsNullOrEmpty(selectedScreenshot.Path) && File.Exists(selectedScreenshot.Path))
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
                Log.Write("Parsing command line arguments.");

                #region Default Values for Command Line Arguments/Options

                bool isScheduled = false;

                checkBoxInitialScreenshot.Checked = false;

                checkBoxCaptureLimit.Checked = false;
                numericUpDownCaptureLimit.Value = CAPTURE_LIMIT_MIN;

                numericUpDownHoursInterval.Value = 0;
                numericUpDownMinutesInterval.Value = CAPTURE_DELAY_DEFAULT_IN_MINUTES;
                numericUpDownSecondsInterval.Value = 0;
                numericUpDownMillisecondsInterval.Value = 0;

                numericUpDownImageResolutionRatio.Value = IMAGE_RESOLUTION_RATIO_MAX;

                int jpegLevel = JPEG_QUALITY_LEVEL_MAX;
                numericUpDownJpegQualityLevel.Value = JPEG_QUALITY_LEVEL_MAX;

                textBoxFolder.Text = FileSystem.ScreenshotsFolder;
                textBoxMacro.Text = MacroParser.UserMacro;

                comboBoxScheduleImageFormat.SelectedItem = ScreenCapture.DefaultImageFormat;

                toolStripSplitButtonStartScreenCapture.Text = ScreenCapture.DefaultImageFormat;

                checkBoxScheduleStopAt.Checked = false;
                checkBoxScheduleStartAt.Checked = false;
                checkBoxScheduleOnTheseDays.Checked = false;

                toolStripMenuItemShowSystemTrayIcon.Checked = true;

                #endregion Default Values for Command Line Arguments/Options

                Regex rgxCommandLineLock = new Regex(REGEX_COMMAND_LINE_LOCK);
                Regex rgxCommandLineRatio = new Regex(REGEX_COMMAND_LINE_RATIO);
                Regex rgxCommandLineLimit = new Regex(REGEX_COMMAND_LINE_LIMIT);
                Regex rgxCommandLineFormat = new Regex(REGEX_COMMAND_LINE_FORMAT);
                Regex rgxCommandLineMacro = new Regex(REGEX_COMMAND_LINE_MACRO);
                Regex rgxCommandLineFolder = new Regex(REGEX_COMMAND_LINE_FOLDER);
                Regex rgxCommandLineInitial = new Regex(REGEX_COMMAND_LINE_INITIAL);
                Regex rgxCommandLineCaptureDelay = new Regex(REGEX_COMMAND_LINE_DELAY);
                Regex rgxCommandLineScheduleStopAt = new Regex(REGEX_COMMAND_LINE_STOPAT);
                Regex rgxCommandLineScheduleStartAt = new Regex(REGEX_COMMAND_LINE_STARTAT);
                Regex rgxCommandLineJpegLevel = new Regex(REGEX_COMMAND_LINE_JPEG_LEVEL);
                Regex rgxCommandLineHideSystemTrayIcon = new Regex(REGEX_COMMAND_LINE_HIDE_SYSTEM_TRAY_ICON);

                #region Command Line Argument Parsing

                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i] != null)
                    {
                        Log.Write("Parsing command line argument at index " + i + " --> " + args[i]);
                    }

                    if (rgxCommandLineFolder.IsMatch(args[i]))
                    {
                        textBoxFolder.Text = CorrectDirectoryPath(rgxCommandLineFolder.Match(args[i]).Groups["Folder"].Value.ToString());
                    }

                    if (rgxCommandLineMacro.IsMatch(args[i]))
                    {
                        textBoxMacro.Text = rgxCommandLineMacro.Match(args[i]).Groups["Macro"].Value;
                    }

                    if (rgxCommandLineInitial.IsMatch(args[i]))
                    {
                        checkBoxInitialScreenshot.Checked = true;
                    }

                    if (rgxCommandLineRatio.IsMatch(args[i]))
                    {
                        int cmdRatio = Convert.ToInt32(rgxCommandLineRatio.Match(args[i]).Groups["Ratio"].Value);

                        if (cmdRatio >= IMAGE_RESOLUTION_RATIO_MIN && cmdRatio <= IMAGE_RESOLUTION_RATIO_MAX)
                        {
                            numericUpDownImageResolutionRatio.Value = cmdRatio;
                        }
                    }

                    if (rgxCommandLineLimit.IsMatch(args[i]))
                    {
                        int cmdLimit = Convert.ToInt32(rgxCommandLineLimit.Match(args[i]).Groups["Limit"].Value);

                        if (cmdLimit >= CAPTURE_LIMIT_MIN && cmdLimit <= CAPTURE_LIMIT_MAX)
                        {
                            numericUpDownCaptureLimit.Value = cmdLimit;
                            checkBoxCaptureLimit.Checked = true;
                        }
                    }

                    if (rgxCommandLineFormat.IsMatch(args[i]))
                    {
                        comboBoxScheduleImageFormat.SelectedItem = rgxCommandLineFormat.Match(args[i]).Groups["Format"].Value;

                        toolStripSplitButtonStartScreenCapture.Text =
                            rgxCommandLineFormat.Match(args[i]).Groups["Format"].Value;
                    }

                    if (rgxCommandLineCaptureDelay.IsMatch(args[i]))
                    {
                        int hours = Convert.ToInt32(rgxCommandLineCaptureDelay.Match(args[i]).Groups["Hours"].Value);
                        int minutes = Convert.ToInt32(rgxCommandLineCaptureDelay.Match(args[i]).Groups["Minutes"].Value);
                        int seconds = Convert.ToInt32(rgxCommandLineCaptureDelay.Match(args[i]).Groups["Seconds"].Value);
                        int milliseconds = Convert.ToInt32(rgxCommandLineCaptureDelay.Match(args[i]).Groups["Milliseconds"].Value);

                        numericUpDownHoursInterval.Value = hours;
                        numericUpDownMinutesInterval.Value = minutes;
                        numericUpDownSecondsInterval.Value = seconds;
                        numericUpDownMillisecondsInterval.Value = milliseconds;
                    }

                    if (rgxCommandLineScheduleStartAt.IsMatch(args[i]))
                    {
                        isScheduled = true;

                        int hours = Convert.ToInt32(rgxCommandLineScheduleStartAt.Match(args[i]).Groups["Hours"].Value);
                        int minutes = Convert.ToInt32(rgxCommandLineScheduleStartAt.Match(args[i]).Groups["Minutes"].Value);
                        int seconds = Convert.ToInt32(rgxCommandLineScheduleStartAt.Match(args[i]).Groups["Seconds"].Value);

                        dateTimePickerScheduleStartAt.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hours, minutes, seconds);

                        checkBoxScheduleStartAt.Checked = true;
                    }

                    if (rgxCommandLineScheduleStopAt.IsMatch(args[i]))
                    {
                        isScheduled = true;

                        int hours = Convert.ToInt32(rgxCommandLineScheduleStopAt.Match(args[i]).Groups["Hours"].Value);
                        int minutes = Convert.ToInt32(rgxCommandLineScheduleStopAt.Match(args[i]).Groups["Minutes"].Value);
                        int seconds = Convert.ToInt32(rgxCommandLineScheduleStopAt.Match(args[i]).Groups["Seconds"].Value);

                        dateTimePickerScheduleStopAt.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hours, minutes, seconds);

                        checkBoxScheduleStopAt.Checked = true;
                    }

                    if (rgxCommandLineLock.IsMatch(args[i]) && textBoxPassphrase.Text.Length > 0)
                    {
                        checkBoxPassphraseLock.Checked = true;
                    }

                    if (rgxCommandLineJpegLevel.IsMatch(args[i]))
                    {
                        int cmdJpegLevel = Convert.ToInt32(rgxCommandLineJpegLevel.Match(args[i]).Groups["JpegLevel"].Value);

                        if (cmdJpegLevel >= JPEG_QUALITY_LEVEL_MIN && cmdJpegLevel <= JPEG_QUALITY_LEVEL_MAX)
                        {
                            jpegLevel = cmdJpegLevel;
                        }
                    }

                    if (rgxCommandLineHideSystemTrayIcon.IsMatch(args[i]))
                    {
                        toolStripMenuItemShowSystemTrayIcon.Checked = false;
                    }
                }

                #endregion Command Line Argument Parsing

                ScreenCapture.RunningFromCommandLine = true;

                numericUpDownJpegQualityLevel.Value = jpegLevel;

                InitializeThreads();

                if (isScheduled)
                {
                    EnableSchedule();
                }
                else
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

            ToolStripMenuItem showSlideLocationToolStripItem = new ToolStripMenuItem("Show Slide Location");
            showSlideLocationToolStripItem.Click += new EventHandler(Click_toolStripMenuItemShowSlideLocation);

            ToolStripMenuItem showScreenshotLocationToolStripItem = new ToolStripMenuItem("Show Screenshot Location");
            showScreenshotLocationToolStripItem.Click += new EventHandler(Click_toolStripMenuItemShowScreenshotLocation);

            ToolStripMenuItem addNewEditorToolStripItem = new ToolStripMenuItem("Add New Editor ...");
            addNewEditorToolStripItem.Click += new EventHandler(Click_addEditorToolStripMenuItem);

            contextMenuStripScreenshotPreview.Items.Add(showSlideLocationToolStripItem);
            contextMenuStripScreenshotPreview.Items.Add(showScreenshotLocationToolStripItem);
            contextMenuStripScreenshotPreview.Items.Add(new ToolStripSeparator());
            contextMenuStripScreenshotPreview.Items.Add(addNewEditorToolStripItem);

            toolStripSplitButtonScreen1Edit.DropDown.Items.Clear();
            toolStripSplitButtonScreen2Edit.DropDown.Items.Clear();
            toolStripSplitButtonScreen3Edit.DropDown.Items.Clear();
            toolStripSplitButtonScreen4Edit.DropDown.Items.Clear();
            toolStripSplitButtonActiveWindowEdit.DropDown.Items.Clear();

            toolStripSplitButtonScreen1Edit.DropDown.Items.Add("Add New Editor ...", null, Click_addEditorToolStripMenuItem);
            toolStripSplitButtonScreen2Edit.DropDown.Items.Add("Add New Editor ...", null, Click_addEditorToolStripMenuItem);
            toolStripSplitButtonScreen3Edit.DropDown.Items.Add("Add New Editor ...", null, Click_addEditorToolStripMenuItem);
            toolStripSplitButtonScreen4Edit.DropDown.Items.Add("Add New Editor ...", null, Click_addEditorToolStripMenuItem);
            toolStripSplitButtonActiveWindowEdit.DropDown.Items.Add("Add New Editor ...", null, Click_addEditorToolStripMenuItem);

            BuildEditorsList();
            BuildTriggersList();
            BuildRegionsList();
        }

        private void BuildEditorsList()
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
                    // ****************** EDITORS LIST IN CONTEXTUAL MENU *************************
                    // Add the Editor to the screenshot preview contextual menu and to each "Edit"
                    // button at the top of the individual preview image.

                    contextMenuStripScreenshotPreview.Items.Add(editor.Name, Icon.ExtractAssociatedIcon(editor.Application).ToBitmap(), Click_runEditor);
                    toolStripSplitButtonScreen1Edit.DropDown.Items.Add(editor.Name, Icon.ExtractAssociatedIcon(editor.Application).ToBitmap(), Click_runEditor);
                    toolStripSplitButtonScreen2Edit.DropDown.Items.Add(editor.Name, Icon.ExtractAssociatedIcon(editor.Application).ToBitmap(), Click_runEditor);
                    toolStripSplitButtonScreen3Edit.DropDown.Items.Add(editor.Name, Icon.ExtractAssociatedIcon(editor.Application).ToBitmap(), Click_runEditor);
                    toolStripSplitButtonScreen4Edit.DropDown.Items.Add(editor.Name, Icon.ExtractAssociatedIcon(editor.Application).ToBitmap(), Click_runEditor);
                    toolStripSplitButtonActiveWindowEdit.DropDown.Items.Add(editor.Name, Icon.ExtractAssociatedIcon(editor.Application).ToBitmap(), Click_runEditor);
                    // ****************************************************************************

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

        private void BuildTriggersList()
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

        private void BuildRegionsList()
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
                buttonChangeRegion.Click += new EventHandler(Click_buttonChangeRegion);
                tabPageRegions.Controls.Add(buttonChangeRegion);

                // Move down the Regions tab page so we're ready to loop around again and add the next Region to it.
                yPosRegion += Y_POS_REGION_INCREMENT;
            }
        }

        #region Click Event Handlers

        /// <summary>
        /// Opens the main screenshots folder in Windows Explorer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_buttonOpenFolder(object sender, EventArgs e)
        {
            if (Directory.Exists(textBoxFolder.Text))
            {
                Process.Start(FileSystem.FileManager, textBoxFolder.Text);
            }
        }

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
                    CheckBox checkBox = (CheckBox)control;

                    if (checkBox.Checked)
                    {
                        Editor editor = formEditor.EditorCollection.Get((Editor)checkBox.Tag);
                        formEditor.EditorCollection.Remove(editor);
                    }
                }
            }

            if (countBeforeRemoval > formEditor.EditorCollection.Count)
            {
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
            if (listBoxSlides.SelectedIndex > -1)
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
            Button buttonSelected = (Button)sender;

            if (buttonSelected.Tag != null)
            {
                formEditor.EditorObject = (Editor)buttonSelected.Tag;

                formEditor.ShowDialog(this);

                if (formEditor.DialogResult == DialogResult.OK)
                {
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
                RunEditor(editor, ScreenshotCollection.GetBySlidename(Slideshow.SelectedSlide, Slideshow.SelectedScreen == 0 ? 1 : Slideshow.SelectedScreen));
            }
        }

        /// <summary>
        /// Executes a chosen image editor from a Trigger.
        /// </summary>
        /// <param name="editor">The image editor to execute.</param>
        private void RunEditor(Editor editor, TriggerActionType triggerActionType)
        {
            if (editor != null && triggerActionType == TriggerActionType.RunEditor && ScreenCapture.Running)
            {
                for (int i = 0; i <= ScreenCapture.SCREEN_MAX; i++)
                {
                    RunEditor(editor, ScreenshotCollection.GetBySlidename(ScreenshotCollection.GetByIndex(ScreenshotCollection.Count - 1).Slidename, i));
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
                BuildScreenshotPreviewContextualMenu();

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
                    CheckBox checkBox = (CheckBox)control;

                    if (checkBox.Checked)
                    {
                        Trigger trigger = formTrigger.TriggerCollection.Get((Trigger)checkBox.Tag);
                        formTrigger.TriggerCollection.Remove(trigger);
                    }
                }
            }

            if (countBeforeRemoval > formTrigger.TriggerCollection.Count)
            {
                BuildScreenshotPreviewContextualMenu();

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
            Button buttonSelected = (Button)sender;

            if (buttonSelected.Tag != null)
            {
                formTrigger.TriggerObject = (Trigger)buttonSelected.Tag;

                formTrigger.EditorCollection = formEditor.EditorCollection;

                formTrigger.ShowDialog(this);

                if (formTrigger.DialogResult == DialogResult.OK)
                {
                    BuildScreenshotPreviewContextualMenu();

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

            formRegion.ShowDialog(this);

            if (formRegion.DialogResult == DialogResult.OK)
            {
                BuildScreenshotPreviewContextualMenu();

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
                    CheckBox checkBox = (CheckBox)control;

                    if (checkBox.Checked)
                    {
                        Region region = formRegion.RegionCollection.Get((Region)checkBox.Tag);
                        formRegion.RegionCollection.Remove(region);
                    }
                }
            }

            if (countBeforeRemoval > formRegion.RegionCollection.Count)
            {
                BuildScreenshotPreviewContextualMenu();

                formRegion.RegionCollection.Save();
            }
        }

        /// <summary>
        /// Shows the "Change Region" window to enable the user to edit a chosen Region.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_buttonChangeRegion(object sender, EventArgs e)
        {
            Button buttonSelected = (Button)sender;

            if (buttonSelected.Tag != null)
            {
                formRegion.RegionObject = (Region)buttonSelected.Tag;

                formRegion.ShowDialog(this);

                if (formRegion.DialogResult == DialogResult.OK)
                {
                    BuildScreenshotPreviewContextualMenu();

                    formRegion.RegionCollection.Save();
                }
            }
        }

        #endregion Region

        #region Schedule

        /// <summary>
        /// Turns on scheduled screen capturing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_buttonScheduleSet(object sender, EventArgs e)
        {
            EnableSchedule();
        }

        /// <summary>
        /// Turns off scheduled screen capturing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_buttonScheduleClear(object sender, EventArgs e)
        {
            DisableSchedule();
        }

        #endregion Schedule

        #region Reset

        /// <summary>
        /// Resets the X, Y, Width, and Height values for every screen.
        /// </summary>
        private void AutoReset()
        {
            if (checkBoxAutoReset.Checked)
            {
                Click_buttonScreen1Reset(null, null);
                Click_buttonScreen2Reset(null, null);
                Click_buttonScreen3Reset(null, null);
                Click_buttonScreen4Reset(null, null);
            }
        }

        /// <summary>
        /// Resets the X, Y, Width, and Height values for Screen 1.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_buttonScreen1Reset(object sender, EventArgs e)
        {
            int count = 0;

            foreach (Screen screen in Screen.AllScreens)
            {
                count++;

                if (count <= ScreenCapture.SCREEN_MAX && count == 1)
                {
                    numericUpDownScreen1X.Value = screen.Bounds.X;
                    numericUpDownScreen1Y.Value = screen.Bounds.Y;

                    numericUpDownScreen1Width.Value = screen.Bounds.Width;
                    numericUpDownScreen1Height.Value = screen.Bounds.Height;
                }
            }
        }

        /// <summary>
        /// Resets the X, Y, Width, and Height values for Screen 2.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_buttonScreen2Reset(object sender, EventArgs e)
        {
            int count = 0;

            foreach (Screen screen in Screen.AllScreens)
            {
                count++;

                if (count <= ScreenCapture.SCREEN_MAX && count == 2)
                {
                    numericUpDownScreen2X.Value = screen.Bounds.X;
                    numericUpDownScreen2Y.Value = screen.Bounds.Y;

                    numericUpDownScreen2Width.Value = screen.Bounds.Width;
                    numericUpDownScreen2Height.Value = screen.Bounds.Height;
                }
            }
        }

        /// <summary>
        /// Resets the X, Y, Width, and Height values for Screen 3.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_buttonScreen3Reset(object sender, EventArgs e)
        {
            int count = 0;

            foreach (Screen screen in Screen.AllScreens)
            {
                count++;

                if (count <= ScreenCapture.SCREEN_MAX && count == 3)
                {
                    numericUpDownScreen3X.Value = screen.Bounds.X;
                    numericUpDownScreen3Y.Value = screen.Bounds.Y;

                    numericUpDownScreen3Width.Value = screen.Bounds.Width;
                    numericUpDownScreen3Height.Value = screen.Bounds.Height;
                }
            }
        }

        /// <summary>
        /// Resets the X, Y, Width, and Height values for Screen 4.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_buttonScreen4Reset(object sender, EventArgs e)
        {
            int count = 0;

            foreach (Screen screen in Screen.AllScreens)
            {
                count++;

                if (count <= ScreenCapture.SCREEN_MAX && count == 4)
                {
                    numericUpDownScreen4X.Value = screen.Bounds.X;
                    numericUpDownScreen4Y.Value = screen.Bounds.Y;

                    numericUpDownScreen4Width.Value = screen.Bounds.Width;
                    numericUpDownScreen4Height.Value = screen.Bounds.Height;
                }
            }
        }

        #endregion Reset

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
                Settings.User.GetByKey("Passphrase").Value = textBoxPassphrase.Text;
                SaveSettings();

                textBoxPassphrase.ReadOnly = true;
                buttonSetPassphrase.Enabled = false;

                checkBoxPassphraseLock.Enabled = true;
            }
        }

        /// <summary>
        /// Clears the passphrase chosen by the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_buttonClearPassphrase(object sender, EventArgs e)
        {
            textBoxPassphrase.Clear();
            textBoxPassphrase.ReadOnly = false;

            checkBoxPassphraseLock.Enabled = false;
            checkBoxPassphraseLock.Checked = false;

            Settings.User.GetByKey("LockScreenCaptureSession").Value = false;
            Settings.User.GetByKey("Passphrase").Value = string.Empty;
            SaveSettings();

            textBoxPassphrase.Focus();
        }

        #endregion Passphrase

        #endregion Click Event Handlers

        /// <summary>
        /// Determines which screen tab is selected (All Screens, Screen 1, Screen 2, Screen 3, Screen 4, or Active Window).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectedIndexChanged_tabControlScreens(object sender, EventArgs e)
        {
            Slideshow.SelectedScreen = tabControlScreens.SelectedIndex <= (ScreenCapture.SCREEN_MAX + 1) ? tabControlScreens.SelectedIndex : 1;
        }

        /// <summary>
        /// The timer for showing a preview of what a screen capture session would look like.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tick_timerPreviewCapture(object sender, EventArgs e)
        {
            TakePreviewScreenshots();
        }

        /// <summary>
        /// The timer for taking screenshots.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tick_timerScreenCapture(object sender, EventArgs e)
        {
            if (!timerScreenCapture.Enabled)
            {
                StopScreenCapture();
            }

            if (ScreenCapture.Limit >= ScreenCapture.CAPTURE_LIMIT_MIN && ScreenCapture.Limit <= ScreenCapture.CAPTURE_LIMIT_MAX)
            {
                if (ScreenCapture.Count < ScreenCapture.Limit)
                {
                    TakeScreenshot();
                }

                if (ScreenCapture.Count == ScreenCapture.Limit)
                {
                    RunTriggersOfConditionType(TriggerConditionType.LimitReached);
                }
            }
            else
            {
                TakeScreenshot();
            }
        }

        /// <summary>
        /// Takes a screenshot of each available screen.
        /// </summary>
        private void TakeScreenshot()
        {
            int count = 0;
            string screenName = string.Empty;

            AutoReset();

            ScreenCapture.DateTimePreviousScreenshot = DateTime.Now;

            // Save a copy of an empty screenshot image file so that we can retrieve it later in the Slideshow.
            if (CaptureScreenAllowed(1) || CaptureScreenAllowed(2) || CaptureScreenAllowed(3) || CaptureScreenAllowed(4) || CaptureScreenAllowed(5))
            {
                ScreenCapture.Save(FileSystem.SlidesFolder + MacroParser.ParseTags(_imageFormatCollection, MacroParser.ScreenshotListMacro, null));
            }

            // Active Window
            if (CaptureScreenAllowed(5))
            {
                ScreenCapture.TakeScreenshot(_imageFormatCollection, null, FileSystem.SlidesFolder + MacroParser.ParseTags(_imageFormatCollection, MacroParser.ApplicationMacro, "5"), 5, ScreenshotType.Application, (long)numericUpDownJpegQualityLevel.Value, checkBoxMouse.Checked);
                ScreenCapture.TakeScreenshot(_imageFormatCollection, null, ScreenCapture.Folder + MacroParser.ParseTags(_imageFormatCollection, ScreenCapture.Macro, textBoxScreenActiveWindowName.Text), 5, ScreenshotType.User, (long)numericUpDownJpegQualityLevel.Value, checkBoxMouse.Checked);
            }

            // All screens.
            foreach (Screen screen in Screen.AllScreens)
            {
                count++;

                if (CaptureScreenAllowed(count) && count <= ScreenCapture.SCREEN_MAX)
                {
                    SetupScreenPosition(screen, count);
                    SetupScreenSize(screen, count);

                    switch (count)
                    {
                        case 1:
                            screenName = textBoxScreen1Name.Text;
                            break;

                        case 2:
                            screenName = textBoxScreen2Name.Text;
                            break;

                        case 3:
                            screenName = textBoxScreen3Name.Text;
                            break;

                        case 4:
                            screenName = textBoxScreen4Name.Text;
                            break;
                    }

                    if (!string.IsNullOrEmpty(screenName))
                    {
                        ScreenCapture.TakeScreenshot(_imageFormatCollection, screen, FileSystem.SlidesFolder + MacroParser.ParseTags(_imageFormatCollection, MacroParser.ApplicationMacro, count.ToString()), count, ScreenshotType.Application, (long)numericUpDownJpegQualityLevel.Value, checkBoxMouse.Checked);
                        ScreenCapture.TakeScreenshot(_imageFormatCollection, screen, ScreenCapture.Folder + MacroParser.ParseTags(_imageFormatCollection, ScreenCapture.Macro, screenName), count, ScreenshotType.User, (long)numericUpDownJpegQualityLevel.Value, checkBoxMouse.Checked);
                    }
                }
            }

            ScreenCapture.Count++;

            RunTriggersOfConditionType(TriggerConditionType.ScreenshotTaken);

            RunRegionCaptures();
        }

        /// <summary>
        /// Determines if we are allowed to capture a particular screen.
        /// </summary>
        /// <param name="screenNumber">The screen number.</param>
        /// <returns></returns>
        private bool CaptureScreenAllowed(int screenNumber)
        {
            bool capture = false;

            switch (screenNumber)
            {
                case 1:
                    capture = string.IsNullOrEmpty(textBoxScreen1Name.Text) || !checkBoxCaptureScreen1.Checked ? false : true;
                    break;

                case 2:
                    capture = string.IsNullOrEmpty(textBoxScreen2Name.Text) || !checkBoxCaptureScreen2.Checked ? false : true;
                    break;

                case 3:
                    capture = string.IsNullOrEmpty(textBoxScreen3Name.Text) || !checkBoxCaptureScreen3.Checked ? false : true;
                    break;

                case 4:
                    capture = string.IsNullOrEmpty(textBoxScreen4Name.Text) || !checkBoxCaptureScreen4.Checked ? false : true;
                    break;

                case 5:
                    capture = string.IsNullOrEmpty(textBoxScreenActiveWindowName.Text) || !checkBoxCaptureActiveWindow.Checked ? false : true;
                    break;
            }

            return capture;
        }

        /// <summary>
        /// Takes the screenshots as a preview.
        /// </summary>
        private void TakePreviewScreenshots()
        {
            AutoReset();

            DisplayImages(true);
        }

        /// <summary>
        /// Determines what values we'll set for the X and Y coordinates of a particular screen when the user wants to do a region capture.
        /// </summary>
        /// <param name="screen">The screen object.</param>
        /// <param name="screenNumber">The screen number.</param>
        private void SetupScreenPosition(Screen screen, int screenNumber)
        {
            int x = 0;
            int y = 0;

            switch (screenNumber)
            {
                case 1:
                    x = (int)numericUpDownScreen1X.Value > 0 ? (int)numericUpDownScreen1X.Value : screen.Bounds.X;
                    y = (int)numericUpDownScreen1Y.Value > 0 ? (int)numericUpDownScreen1Y.Value : screen.Bounds.Y;
                    break;

                case 2:
                    x = (int)numericUpDownScreen2X.Value > 0 ? (int)numericUpDownScreen2X.Value : screen.Bounds.X;
                    y = (int)numericUpDownScreen2Y.Value > 0 ? (int)numericUpDownScreen2Y.Value : screen.Bounds.Y;
                    break;

                case 3:
                    x = (int)numericUpDownScreen3X.Value > 0 ? (int)numericUpDownScreen3X.Value : screen.Bounds.X;
                    y = (int)numericUpDownScreen3Y.Value > 0 ? (int)numericUpDownScreen3Y.Value : screen.Bounds.Y;
                    break;

                case 4:
                    x = (int)numericUpDownScreen4X.Value > 0 ? (int)numericUpDownScreen4X.Value : screen.Bounds.X;
                    y = (int)numericUpDownScreen4Y.Value > 0 ? (int)numericUpDownScreen4Y.Value : screen.Bounds.Y;
                    break;
            }

            ScreenCapture.X = x;
            ScreenCapture.Y = y;
        }

        /// <summary>
        /// Determines what values we'll set for the Width and Height dimensions of a particular screen when the user wants to do a region capture.
        /// </summary>
        /// <param name="screen">The screen object.</param>
        /// <param name="screenNumber">The screen number.</param>
        private void SetupScreenSize(Screen screen, int screenNumber)
        {
            int width = 0;
            int height = 0;

            switch (screenNumber)
            {
                case 1:
                    width = (int)numericUpDownScreen1Width.Value > 0 ? (int)numericUpDownScreen1Width.Value : screen.Bounds.Width;
                    height = (int)numericUpDownScreen1Height.Value > 0 ? (int)numericUpDownScreen1Height.Value : screen.Bounds.Height;
                    break;

                case 2:
                    width = (int)numericUpDownScreen2Width.Value > 0 ? (int)numericUpDownScreen2Width.Value : screen.Bounds.Width;
                    height = (int)numericUpDownScreen2Height.Value > 0 ? (int)numericUpDownScreen2Height.Value : screen.Bounds.Height;
                    break;

                case 3:
                    width = (int)numericUpDownScreen3Width.Value > 0 ? (int)numericUpDownScreen3Width.Value : screen.Bounds.Width;
                    height = (int)numericUpDownScreen3Height.Value > 0 ? (int)numericUpDownScreen3Height.Value : screen.Bounds.Height;
                    break;

                case 4:
                    width = (int)numericUpDownScreen4Width.Value > 0 ? (int)numericUpDownScreen4Width.Value : screen.Bounds.Width;
                    height = (int)numericUpDownScreen4Height.Value > 0 ? (int)numericUpDownScreen4Height.Value : screen.Bounds.Height;
                    break;
            }

            ScreenCapture.Width = width;
            ScreenCapture.Height = height;
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
        /// Displays the screenshot images.
        /// </summary>
        /// <param name="preview"></param>
        private void DisplayImages(bool preview)
        {
            ArrayList images = new ArrayList();

            int count = 0;

            foreach (Screen screen in Screen.AllScreens)
            {
                count++;

                if (count <= ScreenCapture.SCREEN_MAX)
                {
                    SetupScreenPosition(screen, count);
                    SetupScreenSize(screen, count);

                    if (preview && CaptureScreenAllowed(count))
                    {
                        Bitmap bitmap = ScreenCapture.GetScreenBitmap(screen, (int)numericUpDownImageResolutionRatio.Value, ScreenCapture.ImageFormat, checkBoxMouse.Checked);

                        if (bitmap != null)
                        {
                            images.Add(bitmap);
                        }
                    }
                }
            }

            if (!preview)
            {
                images = FileSystem.GetImages(Slideshow.SelectedSlide, monthCalendar.SelectionStart);
            }

            if (images.Count >= 1)
            {
                if (CaptureScreenAllowed(1) || !preview)
                {
                    pictureBoxScreenshotPreviewMonitor1.Image = (Image)images[0];
                }
                else
                {
                    pictureBoxScreenshotPreviewMonitor1.Image = null;
                }

                pictureBoxScreenshotPreviewMonitor2.Image = null;
                pictureBoxScreenshotPreviewMonitor3.Image = null;
                pictureBoxScreenshotPreviewMonitor4.Image = null;
            }

            if (images.Count >= 2)
            {
                if (CaptureScreenAllowed(2) || !preview)
                {
                    pictureBoxScreenshotPreviewMonitor2.Image = (Image)images[1];
                }
                else
                {
                    pictureBoxScreenshotPreviewMonitor2.Image = null;
                }

                pictureBoxScreenshotPreviewMonitor3.Image = null;
                pictureBoxScreenshotPreviewMonitor4.Image = null;
            }

            if (images.Count >= 3)
            {
                if (CaptureScreenAllowed(3) || !preview)
                {
                    pictureBoxScreenshotPreviewMonitor3.Image = (Image)images[2];
                }
                else
                {
                    pictureBoxScreenshotPreviewMonitor3.Image = null;
                }

                pictureBoxScreenshotPreviewMonitor4.Image = null;
            }

            if (images.Count >= 4)
            {
                if (CaptureScreenAllowed(4) || !preview)
                {
                    pictureBoxScreenshotPreviewMonitor4.Image = (Image)images[3];
                }
                else
                {
                    pictureBoxScreenshotPreviewMonitor4.Image = null;
                }
            }

            if (preview && CaptureScreenAllowed(5))
            {
                pictureBoxActiveWindow.Image = (Image)ScreenCapture.GetActiveWindowBitmap();
            }
            else
            {
                if (images.Count >= 5)
                {
                    pictureBoxActiveWindow.Image = (Image)images[4];
                }
            }

            pictureBoxScreen1.Image = pictureBoxScreenshotPreviewMonitor1.Image;
            pictureBoxScreen2.Image = pictureBoxScreenshotPreviewMonitor2.Image;
            pictureBoxScreen3.Image = pictureBoxScreenshotPreviewMonitor3.Image;
            pictureBoxScreen4.Image = pictureBoxScreenshotPreviewMonitor4.Image;

            GC.Collect();
        }

        /// <summary>
        /// Checks the capture limit.
        /// </summary>
        private void CaptureLimitCheck()
        {
            if (checkBoxCaptureLimit.Checked)
            {
                numericUpDownCaptureLimit.Enabled = true;

                ScreenCapture.Count = 0;
                ScreenCapture.Limit = (int)numericUpDownCaptureLimit.Value;
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
                        StartScreenCapture(comboBoxScheduleImageFormat.SelectedItem.ToString());
                    }
                }
                else
                {
                    if ((DateTime.Now.Hour == dateTimePickerScheduleStartAt.Value.Hour) &&
                        (DateTime.Now.Minute == dateTimePickerScheduleStartAt.Value.Minute) &&
                        (DateTime.Now.Second == dateTimePickerScheduleStartAt.Value.Second))
                    {
                        StartScreenCapture(comboBoxScheduleImageFormat.SelectedItem.ToString());
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
        /// Enables the "Play Slideshow" control if it should be enabled.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValueChanged_numericUpDownSlideshowDelay(object sender, EventArgs e)
        {
            EnablePlaySlideshow();
        }

        /// <summary>
        /// Shows the appropriate set of controls for the associated module tab that's selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectedIndexChanged_tabControlModules(object sender, EventArgs e)
        {
            toolStripSlideshow.Visible = false;
            toolStripScreenCapture.Visible = false;

            switch (tabControlModules.SelectedTab.Text)
            {
                case "Screen":
                    toolStripScreenCapture.Visible = true;
                    toolStripScreenCapture.BringToFront();
                    break;

                case "Slideshow":
                    toolStripSlideshow.Visible = true;
                    toolStripSlideshow.BringToFront();
                    break;
            }
        }

        /// <summary>
        /// Takes screenshots as a preview (or clears the preview images) depending on the value of capture delay.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValueChanged_previewInterval(object sender, EventArgs e)
        {
            int demoInterval = GetCaptureDelay();

            if (demoInterval > 0)
            {
                timerPreviewCapture.Interval = demoInterval;

                if (toolStripButtonPreview.Checked)
                {
                    if (checkBoxInitialScreenshot.Checked)
                    {
                        TakePreviewScreenshots();
                    }

                    timerPreviewCapture.Enabled = true;
                }
                else
                {
                    timerPreviewCapture.Enabled = false;

                    UpdatePreview();
                }
            }
            else
            {
                timerPreviewCapture.Enabled = false;

                UpdatePreview();
            }
        }

        private void EnablePreview()
        {
            toolStripButtonPreview.Checked = true;
        }

        private void DisablePreview()
        {
            toolStripButtonPreview.Checked = false;
        }

        /// <summary>
        /// Turns on scheduled screen capturing.
        /// </summary>
        private void EnableSchedule()
        {
            buttonScheduleSet.Enabled = false;
            buttonScheduleClear.Enabled = true;

            timerScheduledCaptureStop.Enabled = true;
            timerScheduledCaptureStart.Enabled = true;
        }

        /// <summary>
        /// Turns off scheduled screen capturing.
        /// </summary>
        private void DisableSchedule()
        {
            buttonScheduleSet.Enabled = true;
            buttonScheduleClear.Enabled = false;

            timerScheduledCaptureStop.Enabled = false;
            timerScheduledCaptureStart.Enabled = false;
        }

        /// Show or hide the system tray icon depending on the option selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckedChanged_toolStripMenuItemShowSystemTrayIcon(object sender, EventArgs e)
        {
            notifyIcon.Visible = toolStripMenuItemShowSystemTrayIcon.Checked;
        }

        /// <summary>
        /// Determine if we need to show "Lock: On" or "Lock: Off" and if we need to lock the screen capture session or not.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckedChanged_checkBoxPassphraseLock(object sender, EventArgs e)
        {
            if (checkBoxPassphraseLock.Checked)
            {
                ScreenCapture.LockScreenCaptureSession = true;
            }
            else
            {
                ScreenCapture.LockScreenCaptureSession = false;
            }
        }

        /// <summary>
        /// Determines when we enable the "Set" button or disable the "Lock" checkbox (and "Set" button) for passphrase.
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
                checkBoxPassphraseLock.Enabled = false;
                checkBoxPassphraseLock.Checked = false;

                buttonSetPassphrase.Enabled = false;
            }
        }

        /// <summary>
        /// Takes screenshots when preview is enabled otherwise the preview images will be cleared.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckedChanged_toolStripButtonPreview(object sender, EventArgs e)
        {
            if (GetCaptureDelay() > 0 && toolStripButtonPreview.Checked)
            {
                if (checkBoxInitialScreenshot.Checked)
                {
                    TakePreviewScreenshots();
                }
            }
            else
            {
                UpdatePreview();
            }

            timerPreviewCapture.Enabled = toolStripButtonPreview.Checked;
        }

        /// <summary>
        /// Deletes old slides that we don't need anymore (to save on disk space).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tick_timerDeleteSlides(object sender, EventArgs e)
        {
            DeleteSlides();
        }

        private void CheckedChanged_toolStripMenuItemStartWhenWindowsStarts(object sender, EventArgs e)
        {
            try
            {
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

                if (toolStripMenuItemStartWhenWindowsStarts.Checked)
                {
                    registryKey.SetValue("AutoScreenCapture", Application.ExecutablePath);
                }
                else
                {
                    registryKey.DeleteValue("AutoScreenCapture");
                }
            }
            catch (Exception ex)
            {
                Log.Write("FormMain::toolStripMenuItemStartWhenWindowsStarts_CheckedChanged", ex);
            }
        }

        private void SaveSettings(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void Click_buttonRestoreDefaults(object sender, EventArgs e)
        {
            Log.Write("Restoring default settings.");

            numericUpDownHoursInterval.Value = 0;
            numericUpDownMinutesInterval.Value = CAPTURE_DELAY_DEFAULT_IN_MINUTES;
            numericUpDownSecondsInterval.Value = 0;
            numericUpDownMillisecondsInterval.Value = 0;

            numericUpDownSlideshowDelayHours.Value = 0;
            numericUpDownSlideshowDelayMinutes.Value = 0;
            numericUpDownSlideshowDelaySeconds.Value = 1;
            numericUpDownSlideshowDelayMilliseconds.Value = 0;

            numericUpDownSlideSkip.Value = 10;
            numericUpDownCaptureLimit.Value = CAPTURE_LIMIT_MIN;
            numericUpDownImageResolutionRatio.Value = IMAGE_RESOLUTION_RATIO_MAX;

            checkBoxSlideSkip.Checked = false;
            checkBoxCaptureLimit.Checked = false;
            checkBoxInitialScreenshot.Checked = true;

            toolStripMenuItemShowSystemTrayIcon.Checked = true;
            toolStripMenuItemStartWhenWindowsStarts.Checked = true;

            checkBoxScheduleStopAt.Checked = false;
            checkBoxScheduleStartAt.Checked = false;
            comboBoxScheduleImageFormat.SelectedItem = ScreenCapture.DefaultImageFormat;

            checkBoxSaturday.Checked = false;
            checkBoxSunday.Checked = false;
            checkBoxMonday.Checked = false;
            checkBoxTuesday.Checked = false;
            checkBoxWednesday.Checked = false;
            checkBoxThursday.Checked = false;
            checkBoxFriday.Checked = false;

            checkBoxScheduleOnTheseDays.Checked = false;

            dateTimePickerScheduleStopAt.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 0, 0);
            dateTimePickerScheduleStartAt.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);

            numericUpDownJpegQualityLevel.Value = JPEG_QUALITY_LEVEL_MAX;

            numericUpDownDaysOld.Value = 10;

            checkBoxCaptureScreen1.Checked = true;
            checkBoxCaptureScreen2.Checked = true;
            checkBoxCaptureScreen3.Checked = true;
            checkBoxCaptureScreen4.Checked = true;
            checkBoxCaptureActiveWindow.Checked = true;

            checkBoxAutoReset.Checked = true;

            checkBoxMouse.Checked = true;

            DisableSchedule();

            ScreenCapture.ImageFormat = ScreenCapture.DefaultImageFormat;

            Log.Write("Default settings restored.");

            SaveSettings();
        }

        private void pictureBoxScreenshotPreviewMonitor1_DoubleClick(object sender, EventArgs e)
        {
            tabControlScreens.SelectedIndex = 1;
        }

        private void pictureBoxScreenshotPreviewMonitor2_DoubleClick(object sender, EventArgs e)
        {
            tabControlScreens.SelectedIndex = 2;
        }

        private void pictureBoxScreenshotPreviewMonitor3_DoubleClick(object sender, EventArgs e)
        {
            tabControlScreens.SelectedIndex = 3;
        }

        private void pictureBoxScreenshotPreviewMonitor4_DoubleClick(object sender, EventArgs e)
        {
            tabControlScreens.SelectedIndex = 4;
        }

        private void RunTriggersOfConditionType(TriggerConditionType conditionType)
        {
            foreach (Trigger trigger in formTrigger.TriggerCollection)
            {
                if (trigger.ConditionType == conditionType)
                {
                    // These actions need to directly correspond with the TriggerActionType class.
                    switch (trigger.ActionType)
                    {
                        case TriggerActionType.DisablePreview:
                            DisablePreview();
                            break;

                        case TriggerActionType.DisableSchedule:
                            DisableSchedule();
                            break;

                        case TriggerActionType.EnablePreview:
                            EnablePreview();
                            break;

                        case TriggerActionType.EnableSchedule:
                            EnableSchedule();
                            break;

                        case TriggerActionType.ExitApplication:
                            ExitApplication();
                            break;

                        case TriggerActionType.HideInterface:
                            HideInterface();
                            break;

                        case TriggerActionType.PlaySlideshow:
                            PlaySlideshow();
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
                    }
                }
            }
        }

        private void RunRegionCaptures()
        {
            foreach (Region region in formRegion.RegionCollection)
            {
                ScreenCapture.TakeScreenshot(_imageFormatCollection,
                    region.X,
                    region.Y,
                    region.Width,
                    region.Height,
                    ScreenCapture.Folder + MacroParser.ParseTags(_imageFormatCollection, region.Macro, region.Name),
                    (long) numericUpDownJpegQualityLevel.Value, checkBoxMouse.Checked);
            }
        }

        /// <summary>
        /// Displays the remaining time for when the next screenshot will be taken
        /// when the mouse pointer moves over the system tray icon.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon_MouseMove(object sender, MouseEventArgs e)
        {
            if (ScreenCapture.Running)
            {
                int remainingHours = ScreenCapture.TimeRemainingForNextScreenshot.Hours;
                int remainingMinutes = ScreenCapture.TimeRemainingForNextScreenshot.Minutes;
                int remainingSeconds = ScreenCapture.TimeRemainingForNextScreenshot.Seconds;
                int remainingMilliseconds = ScreenCapture.TimeRemainingForNextScreenshot.Milliseconds;

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
                                       ScreenCapture.DateTimeNextScreenshot.ToLongTimeString();
                }

                notifyIcon.Text = "Next screenshot (" + ScreenCapture.ImageFormat + ") in " + remainingTimeStr;
            }
            else
            {
                notifyIcon.Text = Settings.Application.GetByKey("Name").Value + " (" + Settings.Application.GetByKey("Version").Value + ")";
            }
        }
    }
}