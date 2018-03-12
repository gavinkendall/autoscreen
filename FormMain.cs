//////////////////////////////////////////////////////////
// Auto Screen Capture 2.1.0
// autoscreen.FormMain.cs
//
// Developed by Gavin Kendall
// Thursday, 15 May 2008 - Monday, 12 March 2018

using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace autoscreen
{
    /// <summary>
    /// The application's main window.
    /// </summary>
    public partial class FormMain : Form
    {
        /// <summary>
        /// Other forms.
        /// </summary>
        private FormAddEditor formAddEditor = new FormAddEditor();
        private FormEnterPassphrase formEnterPassphrase = new FormEnterPassphrase();

        /// <summary>
        /// Threads for background operations.
        /// </summary>
        private BackgroundWorker runSlideSearchThread = null;
        private BackgroundWorker runDateSearchThread = null;

        /// <summary>
        /// Delegates for the threads.
        /// </summary>
        private delegate void UpdateScreenshotPreviewDelegate();
        private delegate void RunSlideSearchDelegate(DoWorkEventArgs e);
        private delegate void RunDateSearchDelegate(DoWorkEventArgs e);

        /// <summary>
        /// Default settings used by the command line parser. 
        /// </summary>
        private const int CAPTURE_LIMIT_MIN = 0;
        private const int CAPTURE_LIMIT_MAX = 9999;
        private const int CAPTURE_DELAY_DEFAULT = 60000;
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

        /// <summary>
        /// Constructor for the main form. Arguments from the command line can be passed to it.
        /// </summary>
        /// <param name="args">Arguments from the command line</param>
        public FormMain(string[] args)
        {
            InitializeComponent();

            Text = Properties.Settings.Default.ApplicationName;
            notifyIcon.Text = Properties.Settings.Default.ApplicationName;

            Log.Write("*** " + Properties.Settings.Default.ApplicationName + " (" + Properties.Settings.Default.ApplicationVersion + ") ***");

            LoadApplicationSettings();

            if (args.Length > 0)
            {
                ParseCommandLineArguments(args);
            }
        }

        /// <summary>
        /// When this form loads we'll need to read from the application's properties
        /// in order to prepare it with the user's last saved options.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormViewer_Load(object sender, EventArgs e)
        {
            if (toolStripMenuItemOpenAtApplicationStartup.Checked)
            {
                SearchDates();
                OpenWindow();
            }
        }

        /// <summary>
        /// Loads the user's saved application settings.
        /// </summary>
        private void LoadApplicationSettings()
        {
            Log.Write("Initializing image format collection.");
            ImageFormatCollection.Initialize();

            Log.Write("Initializing image format filter drop down menu.");
            LoadImageFormatFilterDropDownMenu();

            runSlideSearchThread = new BackgroundWorker();
            runSlideSearchThread.WorkerReportsProgress = false;
            runSlideSearchThread.WorkerSupportsCancellation = true;
            runSlideSearchThread.DoWork += new DoWorkEventHandler(runSlideSearchThread_DoWork);

            runDateSearchThread = new BackgroundWorker();
            runDateSearchThread.WorkerReportsProgress = false;
            runDateSearchThread.WorkerSupportsCancellation = true;
            runDateSearchThread.DoWork += new DoWorkEventHandler(runDateSearchThread_DoWork);

            Log.Write("Initializing slideshow module.");

            Slideshow.Initialize();
            Slideshow.OnPlaying += new EventHandler(Slideshow_Playing);

            Log.Write("Loading editors and building screenshot preview context menu.");

            EditorCollection.Load();
            BuildScreenshotPreviewContextMenu();

            Log.Write("Loading screenshots into the screenshot collection to generate a history of what was captured.");

            ScreenshotCollection.Load();

            Log.Write("Setting screenshots directory.");

            if (Directory.Exists(Properties.Settings.Default.ScreenshotsDirectory))
            {
                textBoxFolder.Text = Properties.Settings.Default.ScreenshotsDirectory;
            }
            else
            {
                textBoxFolder.Text = FileSystem.ScreenshotsFolder;
            }

            Log.Write("Setting screenshots macro.");

            if (string.IsNullOrEmpty(Properties.Settings.Default.Macro))
            {
                textBoxMacro.Text = MacroParser.UserMacro;
            }
            else
            {
                textBoxMacro.Text = Properties.Settings.Default.Macro;
            }

            comboBoxScheduleImageFormat.Items.Clear();
            toolStripMenuItemStartScreenCapture.DropDownItems.Clear();
            toolStripSplitButtonStartScreenCapture.DropDownItems.Clear();

            Log.Write("Building image format list in system tray menu.");

            for (int i = 0; i < ImageFormatCollection.Count; i++)
            {
                comboBoxScheduleImageFormat.Items.Add(ImageFormatCollection.Get(i).Name);

                ToolStripItem startScreenCaptureMenuItemForSplitButton = new ToolStripMenuItem(ImageFormatCollection.Get(i).Name);
                startScreenCaptureMenuItemForSplitButton.Click += new EventHandler(toolStripMenuItemStartScreenCapture_Click);

                ToolStripItem startScreenCaptureMenuItemForSystemTrayMenu = new ToolStripMenuItem(ImageFormatCollection.Get(i).Name);
                startScreenCaptureMenuItemForSystemTrayMenu.Click += new EventHandler(toolStripMenuItemStartScreenCapture_Click);

                toolStripMenuItemStartScreenCapture.DropDownItems.Add(startScreenCaptureMenuItemForSystemTrayMenu);
                toolStripSplitButtonStartScreenCapture.DropDownItems.Add(startScreenCaptureMenuItemForSplitButton);
            }

            Log.Write("Loading user settings - interval values and slideshow delays.");

            comboBoxScheduleImageFormat.SelectedItem = Properties.Settings.Default.ScheduleImageFormat;

            int interval = Properties.Settings.Default.Interval;
            int slideshowDelay = Properties.Settings.Default.SlideshowDelay;

            decimal intervalHours = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(interval)).Hours);
            decimal intervalMinutes = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(interval)).Minutes);
            decimal intervalSeconds = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(interval)).Seconds);
            decimal intervalMilliseconds = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(interval)).Milliseconds);

            decimal slideshowDelayHours = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(slideshowDelay)).Hours);
            decimal slideshowDelayMinutes = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(slideshowDelay)).Minutes);
            decimal slideshowDelaySeconds = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(slideshowDelay)).Seconds);
            decimal slideshowDelayMilliseconds = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(slideshowDelay)).Milliseconds);

            numericUpDownHoursInterval.Value = intervalHours;
            numericUpDownMinutesInterval.Value = intervalMinutes;
            numericUpDownSecondsInterval.Value = intervalSeconds;
            numericUpDownMillisecondsInterval.Value = intervalMilliseconds;

            numericUpDownSlideshowDelayHours.Value = slideshowDelayHours;
            numericUpDownSlideshowDelayMinutes.Value = slideshowDelayMinutes;
            numericUpDownSlideshowDelaySeconds.Value = slideshowDelaySeconds;
            numericUpDownSlideshowDelayMilliseconds.Value = slideshowDelayMilliseconds;

            Log.Write("Loading user settings - slide skip, capture limit, image resolution ratio, and initial screenshot.");

            numericUpDownSlideSkip.Value = Properties.Settings.Default.SlideSkip;
            numericUpDownCaptureLimit.Value = Properties.Settings.Default.CaptureLimit;
            numericUpDownImageResolutionRatio.Value = Properties.Settings.Default.ImageResolutionRatio;

            checkBoxSlideSkip.Checked = Properties.Settings.Default.SlideSkipCheck;
            checkBoxCaptureLimit.Checked = Properties.Settings.Default.CaptureLimitCheck;
            checkBoxInitialScreenshot.Checked = Properties.Settings.Default.TakeInitialScreenshotCheck;

            checkBoxPassphraseLock.Checked = Properties.Settings.Default.LockScreenCaptureSession;

            textBoxPassphrase.Text = Properties.Settings.Default.Passphrase;

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

            Log.Write("Loading user settings - option menu items.");

            toolStripMenuItemDebugMode.Checked = Properties.Settings.Default.DebugMode;
            toolStripMenuItemDebugMode.CheckedChanged += toolStripMenuItemDebugMode_CheckedChanged;

            toolStripMenuItemPreviewAtApplicationStartup.Checked = Properties.Settings.Default.DemoModeCheck;
            toolStripMenuItemShowSystemTrayIcon.Checked = Properties.Settings.Default.ShowSystemTrayIcon;
            toolStripMenuItemExitOnCloseWindow.Checked = Properties.Settings.Default.ExitOnCloseWindowCheck;
            toolStripMenuItemScheduleAtApplicationStartup.Checked = Properties.Settings.Default.ScheduleOnAtStartupCheck;
            toolStripMenuItemOpenOnStopScreenCapture.Checked = Properties.Settings.Default.OpenOnScreenCaptureStopCheck;
            toolStripMenuItemOpenAtApplicationStartup.Checked = Properties.Settings.Default.OpenOnApplicationStartupCheck;
            toolStripMenuItemCloseWindowOnStartCapture.Checked = Properties.Settings.Default.CloseWindowOnStartCaptureCheck;
            toolStripMenuItemShowSlideshowOnStopScreenCapture.Checked = Properties.Settings.Default.ShowSlideshowAfterScreenCaptureStopCheck;

            Log.Write("Loading user settings - scheduled screen capture session settings.");

            checkBoxScheduleStopAt.Checked = Properties.Settings.Default.CaptureStopAtCheck;
            checkBoxScheduleStartAt.Checked = Properties.Settings.Default.CaptureStartAtCheck;
            checkBoxScheduleStartOnSchedule.Checked = Properties.Settings.Default.CaptureStartOnScheduleCheck;

            checkBoxSaturday.Checked = Properties.Settings.Default.CaptureOnSaturdayCheck;
            checkBoxSunday.Checked = Properties.Settings.Default.CaptureOnSundayCheck;
            checkBoxMonday.Checked = Properties.Settings.Default.CaptureOnMondayCheck;
            checkBoxTuesday.Checked = Properties.Settings.Default.CaptureOnTuesdayCheck;
            checkBoxWednesday.Checked = Properties.Settings.Default.CaptureOnWednesdayCheck;
            checkBoxThursday.Checked = Properties.Settings.Default.CaptureOnThursdayCheck;
            checkBoxFriday.Checked = Properties.Settings.Default.CaptureOnFridayCheck;

            checkBoxScheduleOnTheseDays.Checked = Properties.Settings.Default.CaptureOnTheseDaysCheck;

            dateTimePickerScheduleStopAt.Value = Properties.Settings.Default.CaptureStopAtValue;
            dateTimePickerScheduleStartAt.Value = Properties.Settings.Default.CaptureStartAtValue;

            textBoxScreen1Name.Text = Properties.Settings.Default.Screen1Name;
            textBoxScreen2Name.Text = Properties.Settings.Default.Screen2Name;
            textBoxScreen3Name.Text = Properties.Settings.Default.Screen3Name;
            textBoxScreen4Name.Text = Properties.Settings.Default.Screen4Name;
            textBoxScreen5Name.Text = Properties.Settings.Default.Screen5Name;

            Log.Write("Loading region capture controls for X, Y, Width, Height, and Reset on each available screen.");

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
                            labelScreen1X.Enabled = true;
                            labelScreen1Y.Enabled = true;
                            labelScreen1Width.Enabled = true;
                            labelScreen1Height.Enabled = true;
                            labelScreen1Name.Enabled = true;

                            numericUpDownScreen1X.Enabled = true;
                            numericUpDownScreen1Y.Enabled = true;
                            numericUpDownScreen1Width.Enabled = true;
                            numericUpDownScreen1Height.Enabled = true;
                            buttonScreen1Reset.Enabled = true;

                            numericUpDownScreen1X.Value = Properties.Settings.Default.Screen1X > 0 ? Properties.Settings.Default.Screen1X : screen.Bounds.X;
                            numericUpDownScreen1Y.Value = Properties.Settings.Default.Screen1Y > 0 ? Properties.Settings.Default.Screen1Y : screen.Bounds.Y;
                            numericUpDownScreen1Width.Value = Properties.Settings.Default.Screen1Width > 0 ? Properties.Settings.Default.Screen1Width : screen.Bounds.Width;
                            numericUpDownScreen1Height.Value = Properties.Settings.Default.Screen1Height > 0 ? Properties.Settings.Default.Screen1Height : screen.Bounds.Height;

                            textBoxScreen1Name.Enabled = true;
                            break;

                        case 2:
                            labelScreen2X.Enabled = true;
                            labelScreen2Y.Enabled = true;
                            labelScreen2Width.Enabled = true;
                            labelScreen2Height.Enabled = true;
                            labelScreen2Name.Enabled = true;

                            numericUpDownScreen2X.Enabled = true;
                            numericUpDownScreen2Y.Enabled = true;
                            numericUpDownScreen2Width.Enabled = true;
                            numericUpDownScreen2Height.Enabled = true;
                            buttonScreen2Reset.Enabled = true;

                            numericUpDownScreen2X.Value = Properties.Settings.Default.Screen2X > 0 ? Properties.Settings.Default.Screen2X : screen.Bounds.X;
                            numericUpDownScreen2Y.Value = Properties.Settings.Default.Screen2Y > 0 ? Properties.Settings.Default.Screen2Y : screen.Bounds.Y;
                            numericUpDownScreen2Width.Value = Properties.Settings.Default.Screen2Width > 0 ? Properties.Settings.Default.Screen2Width : screen.Bounds.Width;
                            numericUpDownScreen2Height.Value = Properties.Settings.Default.Screen2Height > 0 ? Properties.Settings.Default.Screen2Height : screen.Bounds.Height;

                            textBoxScreen2Name.Enabled = true;
                            break;

                        case 3:
                            labelScreen3X.Enabled = true;
                            labelScreen3Y.Enabled = true;
                            labelScreen3Width.Enabled = true;
                            labelScreen3Height.Enabled = true;
                            labelScreen3Name.Enabled = true;

                            numericUpDownScreen3X.Enabled = true;
                            numericUpDownScreen3Y.Enabled = true;
                            numericUpDownScreen3Width.Enabled = true;
                            numericUpDownScreen3Height.Enabled = true;
                            buttonScreen3Reset.Enabled = true;

                            numericUpDownScreen3X.Value = Properties.Settings.Default.Screen3X > 0 ? Properties.Settings.Default.Screen3X : screen.Bounds.X;
                            numericUpDownScreen3Y.Value = Properties.Settings.Default.Screen3Y > 0 ? Properties.Settings.Default.Screen3Y : screen.Bounds.Y;
                            numericUpDownScreen3Width.Value = Properties.Settings.Default.Screen3Width > 0 ? Properties.Settings.Default.Screen3Width : screen.Bounds.Width;
                            numericUpDownScreen3Height.Value = Properties.Settings.Default.Screen3Height > 0 ? Properties.Settings.Default.Screen3Height : screen.Bounds.Height;

                            textBoxScreen3Name.Enabled = true;
                            break;

                        case 4:
                            labelScreen4X.Enabled = true;
                            labelScreen4Y.Enabled = true;
                            labelScreen4Width.Enabled = true;
                            labelScreen4Height.Enabled = true;
                            labelScreen4Name.Enabled = true;

                            numericUpDownScreen4X.Enabled = true;
                            numericUpDownScreen4Y.Enabled = true;
                            numericUpDownScreen4Width.Enabled = true;
                            numericUpDownScreen4Height.Enabled = true;
                            buttonScreen4Reset.Enabled = true;

                            numericUpDownScreen4X.Value = Properties.Settings.Default.Screen4X > 0 ? Properties.Settings.Default.Screen4X : screen.Bounds.X;
                            numericUpDownScreen4Y.Value = Properties.Settings.Default.Screen4Y > 0 ? Properties.Settings.Default.Screen4Y : screen.Bounds.Y;
                            numericUpDownScreen4Width.Value = Properties.Settings.Default.Screen4Width > 0 ? Properties.Settings.Default.Screen4Width : screen.Bounds.Width;
                            numericUpDownScreen4Height.Value = Properties.Settings.Default.Screen4Height > 0 ? Properties.Settings.Default.Screen4Height : screen.Bounds.Height;

                            textBoxScreen4Name.Enabled = true;
                            break;
                    }
                }
            }

            if (Properties.Settings.Default.ImageFormatIndex < 0)
            {
                Properties.Settings.Default.ImageFormatIndex = 0;
            }

            toolStripComboBoxImageFormatFilter.SelectedIndex = Properties.Settings.Default.ImageFormatIndex;

            numericUpDownJpegQualityLevel.Value = Properties.Settings.Default.JpegQualityLevel;

            numericUpDownDaysOld.Value = Properties.Settings.Default.DaysOldWhenRemoveSlides;

            if (toolStripMenuItemPreviewAtApplicationStartup.Checked)
            {
                toolStripButtonPreview.Checked = true;
            }

            EnableStartScreenCapture();

            CaptureLimitCheck();

            if (toolStripMenuItemScheduleAtApplicationStartup.Checked)
            {
                ScheduleSet();
            }
        }

        /// <summary>
        /// Saves the user's current settings so we can load them at a later time when the user executes the application.
        /// </summary>
        private void SaveApplicationSettings()
        {
            Log.Write("Saving application settings.");

            Properties.Settings.Default.ScreenshotsDirectory = CorrectDirectoryPath(textBoxFolder.Text);

            Properties.Settings.Default.ScheduleImageFormat = comboBoxScheduleImageFormat.Text;

            Properties.Settings.Default.SlideSkip = (int)numericUpDownSlideSkip.Value;
            Properties.Settings.Default.CaptureLimit = (int)numericUpDownCaptureLimit.Value;
            Properties.Settings.Default.ImageResolutionRatio = (int)numericUpDownImageResolutionRatio.Value;

            Properties.Settings.Default.ImageFormatIndex = toolStripComboBoxImageFormatFilter.SelectedIndex;

            Properties.Settings.Default.Interval = GetCaptureDelay();
            Properties.Settings.Default.SlideshowDelay = GetSlideshowDelay();

            Properties.Settings.Default.SlideSkipCheck = checkBoxSlideSkip.Checked;
            Properties.Settings.Default.CaptureLimitCheck = checkBoxCaptureLimit.Checked;
            Properties.Settings.Default.TakeInitialScreenshotCheck = checkBoxInitialScreenshot.Checked;

            Properties.Settings.Default.DebugMode = toolStripMenuItemDebugMode.Checked;
            Properties.Settings.Default.ShowSystemTrayIcon = toolStripMenuItemShowSystemTrayIcon.Checked;
            Properties.Settings.Default.DemoModeCheck = toolStripMenuItemPreviewAtApplicationStartup.Checked;
            Properties.Settings.Default.ExitOnCloseWindowCheck = toolStripMenuItemExitOnCloseWindow.Checked;
            Properties.Settings.Default.ScheduleOnAtStartupCheck = toolStripMenuItemScheduleAtApplicationStartup.Checked;
            Properties.Settings.Default.OpenOnScreenCaptureStopCheck = toolStripMenuItemOpenOnStopScreenCapture.Checked;
            Properties.Settings.Default.OpenOnApplicationStartupCheck = toolStripMenuItemOpenAtApplicationStartup.Checked;
            Properties.Settings.Default.CloseWindowOnStartCaptureCheck = toolStripMenuItemCloseWindowOnStartCapture.Checked;
            Properties.Settings.Default.ShowSlideshowAfterScreenCaptureStopCheck = toolStripMenuItemShowSlideshowOnStopScreenCapture.Checked;

            Properties.Settings.Default.CaptureStopAtCheck = checkBoxScheduleStopAt.Checked;
            Properties.Settings.Default.CaptureStartAtCheck = checkBoxScheduleStartAt.Checked;
            Properties.Settings.Default.CaptureStartOnScheduleCheck = checkBoxScheduleStartOnSchedule.Checked;

            Properties.Settings.Default.CaptureOnSundayCheck = checkBoxSunday.Checked;
            Properties.Settings.Default.CaptureOnMondayCheck = checkBoxMonday.Checked;
            Properties.Settings.Default.CaptureOnFridayCheck = checkBoxFriday.Checked;
            Properties.Settings.Default.CaptureOnTuesdayCheck = checkBoxTuesday.Checked;
            Properties.Settings.Default.CaptureOnThursdayCheck = checkBoxThursday.Checked;
            Properties.Settings.Default.CaptureOnSaturdayCheck = checkBoxSaturday.Checked;
            Properties.Settings.Default.CaptureOnWednesdayCheck = checkBoxWednesday.Checked;

            Properties.Settings.Default.CaptureOnTheseDaysCheck = checkBoxScheduleOnTheseDays.Checked;

            Properties.Settings.Default.CaptureStopAtValue = dateTimePickerScheduleStopAt.Value;
            Properties.Settings.Default.CaptureStartAtValue = dateTimePickerScheduleStartAt.Value;

            Properties.Settings.Default.Screen1X = (int)numericUpDownScreen1X.Value;
            Properties.Settings.Default.Screen1Y = (int)numericUpDownScreen1Y.Value;
            Properties.Settings.Default.Screen1Width = (int)numericUpDownScreen1Width.Value;
            Properties.Settings.Default.Screen1Height = (int)numericUpDownScreen1Height.Value;

            Properties.Settings.Default.Screen2X = (int)numericUpDownScreen2X.Value;
            Properties.Settings.Default.Screen2Y = (int)numericUpDownScreen2Y.Value;
            Properties.Settings.Default.Screen2Width = (int)numericUpDownScreen2Width.Value;
            Properties.Settings.Default.Screen2Height = (int)numericUpDownScreen2Height.Value;

            Properties.Settings.Default.Screen3X = (int)numericUpDownScreen3X.Value;
            Properties.Settings.Default.Screen3Y = (int)numericUpDownScreen3Y.Value;
            Properties.Settings.Default.Screen3Width = (int)numericUpDownScreen3Width.Value;
            Properties.Settings.Default.Screen3Height = (int)numericUpDownScreen3Height.Value;

            Properties.Settings.Default.Screen4X = (int)numericUpDownScreen4X.Value;
            Properties.Settings.Default.Screen4Y = (int)numericUpDownScreen4Y.Value;
            Properties.Settings.Default.Screen4Width = (int)numericUpDownScreen4Width.Value;
            Properties.Settings.Default.Screen4Height = (int)numericUpDownScreen4Height.Value;

            Properties.Settings.Default.Screen1Name = textBoxScreen1Name.Text;
            Properties.Settings.Default.Screen2Name = textBoxScreen2Name.Text;
            Properties.Settings.Default.Screen3Name = textBoxScreen3Name.Text;
            Properties.Settings.Default.Screen4Name = textBoxScreen4Name.Text;
            Properties.Settings.Default.Screen5Name = textBoxScreen5Name.Text;

            Properties.Settings.Default.LockScreenCaptureSession = checkBoxPassphraseLock.Checked;

            Properties.Settings.Default.Macro = textBoxMacro.Text;

            Properties.Settings.Default.JpegQualityLevel = (long)numericUpDownJpegQualityLevel.Value;

            Properties.Settings.Default.DaysOldWhenRemoveSlides = (int)numericUpDownDaysOld.Value;

            EditorCollection.Save();
            ScreenshotCollection.Save();

            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// When this form is closing we can either exit the application or just close this window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            if (toolStripMenuItemExitOnCloseWindow.Checked)
            {
                Exit();
            }
            else
            {
                CloseWindow();
            }
        }

        /// <summary>
        /// Set the image format and search for slides whenever the image format filter gets changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripComboBoxImageFormatFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolStripComboBoxImageFormatFilter.SelectedIndex == 0)
            {
                Properties.Settings.Default.ImageFormatFilter = "*.*";
            }

            Regex rgx = new Regex(@"^(?<ImageFormatFilter>\*\.(?<ImageFormat>([a-z]{3,4})))$");

            if (rgx.IsMatch(toolStripComboBoxImageFormatFilter.Items[toolStripComboBoxImageFormatFilter.SelectedIndex].ToString()))
            {
                Properties.Settings.Default.ImageFormatFilter = rgx.Match(toolStripComboBoxImageFormatFilter.Items[toolStripComboBoxImageFormatFilter.SelectedIndex].ToString()).Groups["ImageFormatFilter"].Value;
            }

            Properties.Settings.Default.ImageFormatIndex = toolStripComboBoxImageFormatFilter.SelectedIndex;

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

            /* Remove slides older than specified number of days */

            monthCalendar.BoldedDates = null;

            if (!runDateSearchThread.IsBusy)
            {
                runDateSearchThread.RunWorkerAsync();
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

            if (!runSlideSearchThread.IsBusy)
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
                string[] files = FileSystem.GetFiles(FileSystem.UserAppDataLocalDirectory, monthCalendar.SelectionStart.ToString(MacroParser.DateFormat));

                if (files != null)
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

                string[] dirs = Directory.GetDirectories(FileSystem.UserAppDataLocalDirectory);

                int count = 0;

                foreach (Screen screen in Screen.AllScreens)
                {
                    count++;

                    // Go through each directory found and make sure that the sub-directories match with the format yyyy-MM-dd.
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

                            if (daysToSubtract > 0)
                            {
                                if (dateTimeOfDir <= DateTime.Now.Date.AddDays(-daysToSubtract))
                                {
                                    FileSystem.DeleteFilesInFolder(dirs[i]);
                                }
                            }

                            if (Directory.Exists(dirs[i] + "\\" + count.ToString() + "\\"))
                            {
                                if (!selectedFolders.Contains(Path.GetFileName(dirs[i]).ToString()))
                                {
                                    selectedFolders.Add(Path.GetFileName(dirs[i]).ToString());
                                }
                            }
                        }
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
        /// Converts the filter string into a DateTime object. Used by the RunDateSearch thread so we can set bolded dates in the calendar.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private DateTime ConvertFilterToDateTime(string filter)
        {
            return new DateTime(Convert.ToInt32(filter.Substring(0, 4)), Convert.ToInt32(filter.Substring(5, 2)), Convert.ToInt32(filter.Substring(8, 2)));
        }

        /// <summary>
        /// Shows this window.
        /// </summary>
        private void OpenWindow()
        {
            Log.Write("Opening application window.");

            if (ScreenCapture.lockScreenCaptureSession)
            {
                if (!formEnterPassphrase.Visible)
                {
                    formEnterPassphrase.ShowDialog(this);
                }
            }

            // This is intentional. Do not rewrite these statements as an if/else
            // because as soon as lockScreenCaptureSession is set to false we want
            // to continue with normal functionality.
            if (!ScreenCapture.lockScreenCaptureSession)
            {
                checkBoxPassphraseLock.Checked = false;

                if (toolStripMenuItemOpen.Enabled)
                {
                    SaveApplicationSettings();

                    Opacity = 100;
                    toolStripMenuItemOpen.Enabled = false;
                    toolStripMenuItemClose.Enabled = true;

                    Show();
                    Visible = true;
                    ShowInTaskbar = true;
                    Focus();
                }
            }
        }

        /// <summary>
        /// Hides this window.
        /// </summary>
        private void CloseWindow()
        {
            Log.Write("Closing application window.");

            if (!toolStripMenuItemOpen.Enabled)
            {
                // Pause the slideshow if you find it still playing.
                if (Slideshow.Playing)
                {
                    PauseSlideshow();
                }

                Opacity = 0;
                toolStripMenuItemOpen.Enabled = true;
                toolStripMenuItemClose.Enabled = false;

                Hide();
                Visible = false;
                ShowInTaskbar = false;
            }
        }

        /// <summary>
        /// Stops the screen capture session that's currently running.
        /// </summary>
        private void StopScreenCapture()
        {
            Log.Write("Stopping screen capture.");

            if (ScreenCapture.lockScreenCaptureSession)
            {
                if (!formEnterPassphrase.Visible)
                {
                    formEnterPassphrase.ShowDialog(this);
                }
            }

            // This is intentional. Do not rewrite these statements as an if/else
            // because as soon as lockScreenCaptureSession is set to false we want
            // to continue with normal functionality.
            if (!ScreenCapture.lockScreenCaptureSession)
            {
                checkBoxPassphraseLock.Checked = false;

                ScreenCapture.Count = 0;
                timerScreenCapture.Enabled = false;

                // Let the user know of the last capture that was taken and the status of the session ("Stopped").
                DisplayCaptureStatus(StatusMessage.LAST_CAPTURE_APP, StatusMessage.LAST_CAPTURE_ICON, false);

                DisableStopScreenCapture();
                EnableStartScreenCapture();

                // Some people want to see this window immediately after the session has stopped.
                if (toolStripMenuItemOpenOnStopScreenCapture.Checked)
                {
                    SearchDates();
                    SearchSlides();
                    OpenWindow();
                }

                // Sometimes people want to see the freshly-made screenshots immediately after the session has stopped.
                if (toolStripMenuItemShowSlideshowOnStopScreenCapture.Checked)
                {
                    ShowSlideshow();
                }
            }
        }

        /// <summary>
        /// Plays the slideshow.
        /// </summary>
        private void PlaySlideshow()
        {
            toolStripButtonPreview.Checked = false;
            int slideshowDelay = GetSlideshowDelay();

            DisableControls();
            DisableSystemTrayMenus();

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
            EnableSystemTrayMenus();

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
            EnableSystemTrayMenus();

            toolStripButtonPlaySlideshow.Image = Properties.Resources.player_play;

            Slideshow.Stop();
        }

        /// <summary>
        /// Starts a screen capture session.
        /// </summary>
        /// <param name="folder">The folder where the screenshots should be saved.</param>
        /// <param name="format">The image format the screenshots should be in (such as GIF, JPEG, PNG, etc.).</param>
        /// <param name="delay">The capture delay.</param>
        /// <param name="limit">The number of screenshots that should be taken based on this limit.</param>
        /// <param name="ratio">The image resolution ratio of each screenshot that is taken (can be from 1% to 100%).</param>
        /// <param name="initial">If an initial screenshot should be taken before the timer is started then this boolean needs to be set to true otherwise just set it as false.</param>
        private void StartScreenCapture(string folder, string macro, string format, int delay, int limit, int ratio, bool initial)
        {
            if (Directory.Exists(textBoxFolder.Text))
            {
                Log.Write("Starting new screen capture session in \"" + textBoxFolder.Text + "\"");

                textBoxFolder.Text = CorrectDirectoryPath(textBoxFolder.Text);

                Log.Write("Macro being used is \"" + textBoxMacro.Text + "\"");

                SaveApplicationSettings();

                toolStripButtonPreview.Checked = false;

                if (toolStripMenuItemCloseWindowOnStartCapture.Checked)
                {
                    CloseWindow();
                }

                // Stop the slideshow if it's currently playing.
                if (Slideshow.Playing)
                {
                    Slideshow.Stop();
                }

                // Stop the folder search thread if it's busy.
                if (runDateSearchThread.IsBusy)
                {
                    runDateSearchThread.CancelAsync();
                }

                // Stop the file search thread if it's busy.
                if (runSlideSearchThread.IsBusy)
                {
                    runSlideSearchThread.CancelAsync();
                }

                DisableStartScreenCapture();
                EnableStopScreenCapture();

                // Setup the properties for the screen capture class.
                ScreenCapture.Folder = folder;
                ScreenCapture.Macro = macro;
                ScreenCapture.Format = format;
                ScreenCapture.Delay = delay;
                ScreenCapture.Limit = limit;
                ScreenCapture.Ratio = ratio;

                if (checkBoxPassphraseLock.Checked)
                {
                    ScreenCapture.lockScreenCaptureSession = true;
                }
                else
                {
                    ScreenCapture.lockScreenCaptureSession = false;
                }

                if (initial)
                {
                    Log.Write("Taking initial screenshots.");

                    TakeScreenshot();
                    DisplayCaptureStatus(StatusMessage.LAST_CAPTURE_APP, StatusMessage.LAST_CAPTURE_ICON, true);
                }

                // Start taking screenshots.
                Log.Write("Starting screen capture.");

                timerScreenCapture.Interval = delay;
                timerScreenCapture.Enabled = true;
            }
        }

        /// <summary>
        /// Whenever the user clicks on a screenshot in the list of screenshots then make sure to update the preview of screenshots.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxScreenshots_SelectedIndexChanged(object sender, EventArgs e)
        {
            toolStripButtonPreview.Checked = false;

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

            ClearImages();
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

            for (int i = 0; i < ImageFormatCollection.Count; i++)
            {
                toolStripComboBoxImageFormatFilter.Items.Add("*" + ImageFormatCollection.Get(i).Extension);
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

            if (Properties.Settings.Default.ImageFormatIndex < 0)
            {
                Properties.Settings.Default.ImageFormatIndex = 0;
            }

            toolStripComboBoxImageFormatFilter.SelectedIndex = Properties.Settings.Default.ImageFormatIndex;
        }

        /// <summary>
        /// Shows the slideshow when a date on the calendar has been selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void monthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            ShowSlideshow();
        }

        /// <summary>
        /// Shows the first slide in the slideshow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonFirstSlide_Click(object sender, EventArgs e)
        {
            Slideshow.First();
            listBoxSlides.SelectedIndex = Slideshow.Index;
        }

        /// <summary>
        /// Shows the previous slide in the slideshow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonPreviousSlide_Click(object sender, EventArgs e)
        {
            Slideshow.Previous();
            listBoxSlides.SelectedIndex = Slideshow.Index;
        }

        /// <summary>
        /// Plays the slideshow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonPlaySlideshow_Click(object sender, EventArgs e)
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
        private void toolStripButtonNextSlide_Click(object sender, EventArgs e)
        {
            Slideshow.Next();
            listBoxSlides.SelectedIndex = Slideshow.Index;
        }

        /// <summary>
        /// Shows the last slide in the slideshow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonLastSlide_Click(object sender, EventArgs e)
        {
            Slideshow.Last();
            listBoxSlides.SelectedIndex = Slideshow.Index;
        }

        /// <summary>
        /// Starts a screen capture session based on the image format selected by the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemStartScreenCapture_Click(object sender, EventArgs e)
        {
            string imageFormat = ScreenCapture.DefaultImageFormat;

            if (!sender.ToString().Equals(toolStripSplitButtonStartScreenCapture.Text))
            {
                imageFormat = sender.ToString();
            }

            if (!string.IsNullOrEmpty(imageFormat))
            {
                bool initial = false;
                string folder = textBoxFolder.Text;
                string macro = textBoxMacro.Text;

                int delay = GetCaptureDelay();
                int limit = (int)numericUpDownCaptureLimit.Value;
                int ratio = (int)numericUpDownImageResolutionRatio.Value;

                if (!string.IsNullOrEmpty(folder) && delay > 0)
                {
                    folder = CorrectDirectoryPath(folder);

                    if (Directory.Exists(folder))
                    {
                        if (!checkBoxCaptureLimit.Checked) { limit = 0; }

                        if (checkBoxInitialScreenshot.Checked)
                        {
                            initial = true;
                        }

                        StartScreenCapture(folder, macro, imageFormat, delay, limit, ratio, initial);
                    }
                }
            }
        }

        /// <summary>
        /// Stops the currently running screen capture session.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemStopScreenCapture_Click(object sender, EventArgs e)
        {
            StopScreenCapture();
        }

        /// <summary>
        /// Exits the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            Exit();
        }

        /// <summary>
        /// Exits the application.
        /// </summary>
        private void Exit()
        {
            Log.Write("Exiting application.");

            if (ScreenCapture.lockScreenCaptureSession)
            {
                if (!formEnterPassphrase.Visible)
                {
                    formEnterPassphrase.ShowDialog(this);
                }
            }

            // This is intentional. Do not rewrite these statements as an if/else
            // because as soon as lockScreenCaptureSession is set to false we want
            // to continue with normal functionality.
            if (!ScreenCapture.lockScreenCaptureSession)
            {
                checkBoxPassphraseLock.Checked = false;

                // Save the user's options.
                SaveApplicationSettings();

                // Close this window.
                CloseWindow();

                if (runDateSearchThread.IsBusy)
                {
                    runDateSearchThread.CancelAsync();
                }

                if (runSlideSearchThread.IsBusy)
                {
                    runSlideSearchThread.CancelAsync();
                }

                // Hide the system tray icon.
                notifyIcon.Visible = false;

                // Exit.
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Runs the slide search thread.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void runSlideSearchThread_DoWork(object sender, DoWorkEventArgs e)
        {
            RunSlideSearch(e);
        }

        /// <summary>
        /// Runs the date search thread.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void runDateSearchThread_DoWork(object sender, DoWorkEventArgs e)
        {
            RunDateSearch(e);
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

                checkBoxScheduleStartOnSchedule.Enabled = true;
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
                buttonScheduleSet.Enabled = true;
                buttonScheduleClear.Enabled = true;
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

            checkBoxScheduleStartOnSchedule.Enabled = false;
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
            buttonScheduleSet.Enabled = false;
            buttonScheduleClear.Enabled = false;
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
        /// Displays the screen capture status.
        /// </summary>
        /// <param name="statusApp">The status message for the application's status strip.</param>
        /// <param name="statusIcon">The status message for the application's system tray icon.</param>
        /// <param name="running">Can be "true" for "Running" or "false" for "Stopped".</param>
        private void DisplayCaptureStatus(string statusApp, string statusIcon, bool running)
        {
            string appName = Properties.Settings.Default.ApplicationName;

            if (running)
            {
                appName += " - " + StatusMessage.RUNNING;
            }
            else
            {
                appName += " - " + StatusMessage.STOPPED;
            }

            if (!string.IsNullOrEmpty(statusApp))
            {
                notifyIcon.Text = appName;
            }
        }

        /// <summary>
        /// Plays the slideshow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonPlaySlideshow_ButtonClick(object sender, EventArgs e)
        {
            PlaySlideshow();
        }

        /// <summary>
        /// Opens the standard Windows folder browser for the user to choose a folder for containing the screenshots.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBrowseFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browser = new FolderBrowserDialog();

            if (browser.ShowDialog() == DialogResult.OK)
            {
                textBoxFolder.Text = browser.SelectedPath;
                SearchDates();
            }
        }

        /// <summary>
        /// Opens this window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemOpen_Click(object sender, EventArgs e)
        {
            SearchDates();
            SearchSlides();
            OpenWindow();
        }

        /// <summary>
        /// Closes this window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemClose_Click(object sender, EventArgs e)
        {
            CloseWindow();
        }

        /// <summary>
        /// Shows the "About" window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Properties.Settings.Default.ApplicationName + " " + Properties.Settings.Default.ApplicationVersion + " (\"Clara\")\nDeveloped by Gavin Kendall 2008 - 2018", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Disables the system tray menus.
        /// </summary>
        private void DisableSystemTrayMenus()
        {
            toolStripMenuItemClose.Enabled = false;
            toolStripMenuItemStopScreenCapture.Enabled = false;
            toolStripMenuItemStartScreenCapture.Enabled = false;
        }

        /// <summary>
        /// Enables the system tray menus.
        /// </summary>
        private void EnableSystemTrayMenus()
        {
            toolStripMenuItemClose.Enabled = true;

            if (timerScreenCapture.Enabled)
            {
                toolStripMenuItemStopScreenCapture.Enabled = true;
                toolStripMenuItemStartScreenCapture.Enabled = false;
            }
            else
            {
                toolStripMenuItemStopScreenCapture.Enabled = false;
                toolStripMenuItemStartScreenCapture.Enabled = true;
            }
        }

        /// <summary>
        /// Opens Windows Explorer to show the location of the selected screenshot.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemOpenFileLocation_Click(object sender, EventArgs e)
        {
            if (listBoxSlides.SelectedIndex > -1)
            {
                Screenshot selectedScreenshot = ScreenshotCollection.GetBySlidename(Slideshow.SelectedSlide, Slideshow.SelectedScreen == 0 ? 1 : Slideshow.SelectedScreen);

                if (selectedScreenshot != null && !string.IsNullOrEmpty(selectedScreenshot.Path))
                {
                    if (File.Exists(selectedScreenshot.Path))
                    {
                        Process.Start(FileSystem.FileManager, "/select,\"" + selectedScreenshot.Path + "\"");
                    }
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

                bool initial = false;
                checkBoxInitialScreenshot.Checked = false;

                int limit = CAPTURE_LIMIT_MIN;
                checkBoxCaptureLimit.Checked = false;
                numericUpDownCaptureLimit.Value = CAPTURE_LIMIT_MIN;

                int delay = CAPTURE_DELAY_DEFAULT;
                numericUpDownHoursInterval.Value = 0;
                numericUpDownMinutesInterval.Value = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(delay)).Minutes);
                numericUpDownSecondsInterval.Value = 0;
                numericUpDownMillisecondsInterval.Value = 0;

                int ratio = IMAGE_RESOLUTION_RATIO_MAX;
                numericUpDownImageResolutionRatio.Value = IMAGE_RESOLUTION_RATIO_MAX;

                int jpegLevel = JPEG_QUALITY_LEVEL_MAX;
                numericUpDownJpegQualityLevel.Value = JPEG_QUALITY_LEVEL_MAX;

                string folder = FileSystem.ScreenshotsFolder;
                string macro = MacroParser.UserMacro;

                string imageFormat = ScreenCapture.DefaultImageFormat;
                comboBoxScheduleImageFormat.SelectedItem = ScreenCapture.DefaultImageFormat;

                checkBoxScheduleStopAt.Checked = false;
                checkBoxScheduleStartAt.Checked = false;
                checkBoxScheduleOnTheseDays.Checked = false;

                toolStripMenuItemOpenOnStopScreenCapture.Checked = false;
                toolStripMenuItemOpenAtApplicationStartup.Checked = false;
                toolStripMenuItemCloseWindowOnStartCapture.Checked = true;
                toolStripMenuItemScheduleAtApplicationStartup.Checked = false;
                toolStripMenuItemShowSlideshowOnStopScreenCapture.Checked = false;
                #endregion

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

                #region Command Line Argument Parsing
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i] != null)
                    {
                        Log.Write("Parsing command line argument at index " + i + " --> " + args[i]);
                    }

                    if (rgxCommandLineFolder.IsMatch(args[i]))
                    {
                        folder = CorrectDirectoryPath(rgxCommandLineFolder.Match(args[i]).Groups["Folder"].Value.ToString());
                    }

                    if (rgxCommandLineMacro.IsMatch(args[i]))
                    {
                        macro = rgxCommandLineMacro.Match(args[i]).Groups["Macro"].Value;
                    }

                    if (rgxCommandLineInitial.IsMatch(args[i]))
                    {
                        initial = true;
                        checkBoxInitialScreenshot.Checked = true;
                    }

                    if (rgxCommandLineRatio.IsMatch(args[i]))
                    {
                        int cmdRatio = Convert.ToInt32(rgxCommandLineRatio.Match(args[i]).Groups["Ratio"].Value);

                        if (cmdRatio >= IMAGE_RESOLUTION_RATIO_MIN && cmdRatio <= IMAGE_RESOLUTION_RATIO_MAX)
                        {
                            ratio = cmdRatio;
                        }
                    }

                    if (rgxCommandLineLimit.IsMatch(args[i]))
                    {
                        int cmdLimit = Convert.ToInt32(rgxCommandLineLimit.Match(args[i]).Groups["Limit"].Value);

                        if (cmdLimit >= CAPTURE_LIMIT_MIN && cmdLimit <= CAPTURE_LIMIT_MAX)
                        {
                            limit = cmdLimit;
                            checkBoxCaptureLimit.Checked = true;
                        }
                    }

                    if (rgxCommandLineFormat.IsMatch(args[i]))
                    {
                        imageFormat = rgxCommandLineFormat.Match(args[i]).Groups["Format"].Value;
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

                        delay = ConvertIntoMilliseconds(hours, minutes, seconds, milliseconds);
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
                }
                #endregion

                ScreenCapture.runningFromCommandLine = true;

                textBoxMacro.Text = macro;
                textBoxFolder.Text = folder;
                numericUpDownCaptureLimit.Value = limit;
                numericUpDownJpegQualityLevel.Value = jpegLevel;
                numericUpDownImageResolutionRatio.Value = ratio;
                comboBoxScheduleImageFormat.SelectedItem = imageFormat;

                if (isScheduled)
                {
                    toolStripMenuItemScheduleAtApplicationStartup.Checked = true;
                }
                else
                {
                    StartScreenCapture(folder, macro, imageFormat, delay, limit, ratio, initial);
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
        private void BuildScreenshotPreviewContextMenu()
        {
            contextMenuStripScreenshotPreview.Items.Clear();

            ToolStripItem openFileLocation = new ToolStripMenuItem("Open File Location");
            openFileLocation.Click += new EventHandler(toolStripMenuItemOpenFileLocation_Click);

            ToolStripItem addEditorItem = new ToolStripMenuItem("Add Editor ...");
            addEditorItem.Click += new EventHandler(addEditorToolStripMenuItem_Click);

            contextMenuStripScreenshotPreview.Items.Add(openFileLocation);
            contextMenuStripScreenshotPreview.Items.Add(addEditorItem);
            contextMenuStripScreenshotPreview.Items.Add(new ToolStripSeparator());

            for (int i = 0; i < EditorCollection.Count; i++)
            {
                Editor editor = EditorCollection.Get(i);

                if (editor != null)
                {
                    if (File.Exists(editor.Application))
                    {
                        ToolStripItem editorItem = new ToolStripMenuItem(editor.Name);
                        editorItem.Image = Icon.ExtractAssociatedIcon(editor.Application).ToBitmap();
                        editorItem.Click += new EventHandler(editorRun_Click);

                        contextMenuStripScreenshotPreview.Items.Add(editorItem);
                    }
                }
            }
        }

        #region Click Event Handlers
        /// <summary>
        /// Opens the main screenshots folder in Windows Explorer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOpenFolder_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(textBoxFolder.Text))
            {
                Process.Start(FileSystem.FileManager, textBoxFolder.Text);
            }
        }

        #region Editor
        /// <summary>
        /// Shows the "Add Editor" window to enable the user to add a chosen image editor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formAddEditor.ShowDialog(this);

            BuildScreenshotPreviewContextMenu();
        }

        /// <summary>
        /// Runs the chosen image editor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editorRun_Click(object sender, EventArgs e)
        {
            if (listBoxSlides.SelectedIndex > -1)
            {
                Editor editor = EditorCollection.GetByName(sender.ToString());
                Screenshot selectedScreenshot = ScreenshotCollection.GetBySlidename(Slideshow.SelectedSlide, Slideshow.SelectedScreen == 0 ? 1 : Slideshow.SelectedScreen);

                if (selectedScreenshot != null && !string.IsNullOrEmpty(selectedScreenshot.Path))
                {
                    if (File.Exists(selectedScreenshot.Path))
                    {
                        Process.Start(editor.Application, editor.Arguments.Replace("%screenshot%", "\"" + selectedScreenshot.Path + "\""));
                    }
                }
            }
        }
        #endregion

        #region Schedule
        /// <summary>
        /// Turns on scheduled screen capturing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonScheduleSet_Click(object sender, EventArgs e)
        {
            ScheduleSet();
        }

        /// <summary>
        /// Turns off scheduled screen capturing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonScheduleClear_Click(object sender, EventArgs e)
        {
            ScheduleClear();
        }
        #endregion

        #region Reset
        /// <summary>
        /// Resets the X, Y, Width, and Height values for Screen 1.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonScreen1Reset_Click(object sender, EventArgs e)
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
        private void buttonScreen2Reset_Click(object sender, EventArgs e)
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
        private void buttonScreen3Reset_Click(object sender, EventArgs e)
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
        private void buttonScreen4Reset_Click(object sender, EventArgs e)
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
        #endregion

        #region Passphrase
        /// <summary>
        /// Sets the passphrase chosen by the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSetPassphrase_Click(object sender, EventArgs e)
        {
            if (textBoxPassphrase.Text.Length > 0)
            {
                Properties.Settings.Default.Passphrase = textBoxPassphrase.Text;
                Properties.Settings.Default.Save();

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
        private void buttonClearPassphrase_Click(object sender, EventArgs e)
        {
            textBoxPassphrase.Clear();
            textBoxPassphrase.ReadOnly = false;

            checkBoxPassphraseLock.Enabled = false;
            checkBoxPassphraseLock.Checked = false;

            Properties.Settings.Default.LockScreenCaptureSession = false;
            Properties.Settings.Default.Passphrase = string.Empty;
            Properties.Settings.Default.Save();

            textBoxPassphrase.Focus();
        }
        #endregion
        #endregion

        /// <summary>
        /// Determines which screen tab is selected (All Screens, Screen 1, Screen 2, Screen 3, Screen 4, or Active Window).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControlScreens_SelectedIndexChanged(object sender, EventArgs e)
        {
            Slideshow.SelectedScreen = tabControlScreens.SelectedIndex <= (ScreenCapture.SCREEN_MAX + 1) ? tabControlScreens.SelectedIndex : 1;
        }

        /// <summary>
        /// The timer for showing a preview of what a screen capture session would look like.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerPreviewCapture_Tick(object sender, EventArgs e)
        {
            TakePreviewScreenshots();            
        }

        /// <summary>
        /// The timer for taking screenshots.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerScreenCapture_Tick(object sender, EventArgs e)
        {
            DisplayCaptureStatus(StatusMessage.LAST_CAPTURE_APP, StatusMessage.LAST_CAPTURE_ICON, true);

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
                    StopScreenCapture();
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

            DateTime dateTimeScreenshotTaken = DateTime.Now;

            // Active Window
            if (CaptureScreenAllowed(5))
            {
                ScreenCapture.TakeScreenshot(null, dateTimeScreenshotTaken, ScreenCapture.Format, "5",  FileSystem.UserAppDataLocalDirectory + MacroParser.ParseTags(MacroParser.ApplicationMacro, ScreenCapture.Format, "5", dateTimeScreenshotTaken), 5, ScreenshotType.Application, (long)numericUpDownJpegQualityLevel.Value);
                ScreenCapture.TakeScreenshot(null, dateTimeScreenshotTaken, ScreenCapture.Format, textBoxScreen5Name.Text, ScreenCapture.Folder + MacroParser.ParseTags(ScreenCapture.Macro, ScreenCapture.Format, textBoxScreen5Name.Text, dateTimeScreenshotTaken), 5, ScreenshotType.User, (long)numericUpDownJpegQualityLevel.Value);
            }

            // All screens.
            foreach (Screen screen in Screen.AllScreens)
            {
                count++;

                if (CaptureScreenAllowed(count))
                {
                    if (count <= ScreenCapture.SCREEN_MAX)
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
                            ScreenCapture.TakeScreenshot(screen, dateTimeScreenshotTaken, ScreenCapture.Format, count.ToString(), FileSystem.UserAppDataLocalDirectory + MacroParser.ParseTags(MacroParser.ApplicationMacro, ScreenCapture.Format, count.ToString(), dateTimeScreenshotTaken), count, ScreenshotType.Application, (long)numericUpDownJpegQualityLevel.Value);
                            ScreenCapture.TakeScreenshot(screen, dateTimeScreenshotTaken, ScreenCapture.Format, screenName, ScreenCapture.Folder + MacroParser.ParseTags(ScreenCapture.Macro, ScreenCapture.Format, screenName, dateTimeScreenshotTaken), count, ScreenshotType.User, (long)numericUpDownJpegQualityLevel.Value);
                        }
                    }
                }
            }

            ScreenCapture.Count++;
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
                    capture = string.IsNullOrEmpty(textBoxScreen1Name.Text) ? false : true;
                    break;

                case 2:
                    capture = string.IsNullOrEmpty(textBoxScreen2Name.Text) ? false : true;
                    break;

                case 3:
                    capture = string.IsNullOrEmpty(textBoxScreen3Name.Text) ? false : true;
                    break;

                case 4:
                    capture = string.IsNullOrEmpty(textBoxScreen4Name.Text) ? false : true;
                    break;

                case 5:
                    capture = string.IsNullOrEmpty(textBoxScreen5Name.Text) ? false : true;
                    break;
            }

            return capture;
        }

        /// <summary>
        /// Takes the screenshots as a preview.
        /// </summary>
        private void TakePreviewScreenshots()
        {
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
        private void checkBoxCaptureLimit_CheckedChanged(object sender, EventArgs e)
        {
            CaptureLimitCheck();
        }

        /// <summary>
        /// Clears the preview images.
        /// </summary>
        private void ClearImages()
        {
            pictureBoxScreenshotPreviewMonitor1.Image = null;
            pictureBoxScreenshotPreviewMonitor2.Image = null;
            pictureBoxScreenshotPreviewMonitor3.Image = null;
            pictureBoxScreenshotPreviewMonitor4.Image = null;

            pictureBoxScreen1.Image = null;
            pictureBoxScreen2.Image = null;
            pictureBoxScreen3.Image = null;
            pictureBoxScreen4.Image = null;

            pictureBoxActiveWindow.Image = null;
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

                    if (preview)
                    {
                        Bitmap bitmap = ScreenCapture.GetScreenBitmap(screen, (int)numericUpDownImageResolutionRatio.Value, ScreenCapture.Format);

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

            if (preview)
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
        private void checkBoxScheduleOnTheseDays_CheckedChanged(object sender, EventArgs e)
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
        private void timerScheduledCaptureStart_Tick(object sender, EventArgs e)
        {
            if (checkBoxScheduleStartOnSchedule.Checked)
            {
                if (checkBoxScheduleOnTheseDays.Checked)
                {
                    if ((DateTime.Now.DayOfWeek == DayOfWeek.Saturday && checkBoxSaturday.Checked) ||
                        (DateTime.Now.DayOfWeek == DayOfWeek.Sunday && checkBoxSunday.Checked) ||
                        (DateTime.Now.DayOfWeek == DayOfWeek.Monday && checkBoxMonday.Checked) ||
                        (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday && checkBoxTuesday.Checked) ||
                        (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday && checkBoxWednesday.Checked) ||
                        (DateTime.Now.DayOfWeek == DayOfWeek.Thursday && checkBoxThursday.Checked) ||
                        (DateTime.Now.DayOfWeek == DayOfWeek.Friday && checkBoxFriday.Checked))
                    {
                        bool initial = false;
                        string folder = textBoxFolder.Text;
                        string macro = textBoxMacro.Text;

                        int delay = GetCaptureDelay();
                        int limit = (int)numericUpDownCaptureLimit.Value;
                        int ratio = (int)numericUpDownImageResolutionRatio.Value;

                        if (!checkBoxCaptureLimit.Checked) { limit = 0; }

                        if (checkBoxInitialScreenshot.Checked)
                        {
                            initial = true;
                        }

                        StartScreenCapture(folder, macro, comboBoxScheduleImageFormat.Text, delay, limit, ratio, initial);

                        timerScheduledCaptureStart.Enabled = false;
                    }
                }
                else
                {
                    bool initial = false;
                    string folder = textBoxFolder.Text;
                    string macro = textBoxMacro.Text;

                    int delay = GetCaptureDelay();
                    int limit = (int)numericUpDownCaptureLimit.Value;
                    int ratio = (int)numericUpDownImageResolutionRatio.Value;

                    if (!checkBoxCaptureLimit.Checked) { limit = 0; }

                    if (checkBoxInitialScreenshot.Checked)
                    {
                        initial = true;
                    }

                    StartScreenCapture(folder, macro, comboBoxScheduleImageFormat.Text, delay, limit, ratio, initial);

                    timerScheduledCaptureStart.Enabled = false;
                }
            }
            else if (checkBoxScheduleStartAt.Checked)
            {
                if (checkBoxScheduleOnTheseDays.Checked)
                {
                    if ((DateTime.Now.DayOfWeek == DayOfWeek.Saturday && checkBoxSaturday.Checked) ||
                        (DateTime.Now.DayOfWeek == DayOfWeek.Sunday && checkBoxSunday.Checked) ||
                        (DateTime.Now.DayOfWeek == DayOfWeek.Monday && checkBoxMonday.Checked) ||
                        (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday && checkBoxTuesday.Checked) ||
                        (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday && checkBoxWednesday.Checked) ||
                        (DateTime.Now.DayOfWeek == DayOfWeek.Thursday && checkBoxThursday.Checked) ||
                        (DateTime.Now.DayOfWeek == DayOfWeek.Friday && checkBoxFriday.Checked))
                    {
                        if ((DateTime.Now.Hour == dateTimePickerScheduleStartAt.Value.Hour) &&
                        (DateTime.Now.Minute == dateTimePickerScheduleStartAt.Value.Minute) &&
                        (DateTime.Now.Second == dateTimePickerScheduleStartAt.Value.Second))
                        {
                            bool initial = false;
                            string folder = textBoxFolder.Text;
                            string macro = textBoxMacro.Text;

                            int delay = GetCaptureDelay();
                            int limit = (int)numericUpDownCaptureLimit.Value;
                            int ratio = (int)numericUpDownImageResolutionRatio.Value;

                            if (!checkBoxCaptureLimit.Checked) { limit = 0; }

                            if (checkBoxInitialScreenshot.Checked)
                            {
                                initial = true;
                            }

                            StartScreenCapture(folder, macro, comboBoxScheduleImageFormat.Text, delay, limit, ratio, initial);
                        }
                    }
                }
                else
                {
                    if ((DateTime.Now.Hour == dateTimePickerScheduleStartAt.Value.Hour) &&
                        (DateTime.Now.Minute == dateTimePickerScheduleStartAt.Value.Minute) &&
                        (DateTime.Now.Second == dateTimePickerScheduleStartAt.Value.Second))
                    {
                        bool initial = false;
                        string folder = textBoxFolder.Text;
                        string macro = textBoxMacro.Text;

                        int delay = GetCaptureDelay();
                        int limit = (int)numericUpDownCaptureLimit.Value;
                        int ratio = (int)numericUpDownImageResolutionRatio.Value;

                        if (!checkBoxCaptureLimit.Checked) { limit = 0; }

                        if (checkBoxInitialScreenshot.Checked)
                        {
                            initial = true;
                        }

                        StartScreenCapture(folder, macro, comboBoxScheduleImageFormat.Text, delay, limit, ratio, initial);
                    }
                }
            }
        }

        /// <summary>
        /// The timer used for stopping scheduled screen capture sessions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerScheduledCaptureStop_Tick(object sender, EventArgs e)
        {
            if (checkBoxScheduleStopAt.Checked)
            {
                if (checkBoxScheduleOnTheseDays.Checked)
                {
                    if ((DateTime.Now.DayOfWeek == DayOfWeek.Saturday && checkBoxSaturday.Checked) ||
                        (DateTime.Now.DayOfWeek == DayOfWeek.Sunday && checkBoxSunday.Checked) ||
                        (DateTime.Now.DayOfWeek == DayOfWeek.Monday && checkBoxMonday.Checked) ||
                        (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday && checkBoxTuesday.Checked) ||
                        (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday && checkBoxWednesday.Checked) ||
                        (DateTime.Now.DayOfWeek == DayOfWeek.Thursday && checkBoxThursday.Checked) ||
                        (DateTime.Now.DayOfWeek == DayOfWeek.Friday && checkBoxFriday.Checked))
                    {
                        if ((DateTime.Now.Hour == dateTimePickerScheduleStopAt.Value.Hour) &&
                        (DateTime.Now.Minute == dateTimePickerScheduleStopAt.Value.Minute) &&
                        (DateTime.Now.Second == dateTimePickerScheduleStopAt.Value.Second))
                        {
                            StopScreenCapture();
                        }
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
        private void numericUpDownSlideshowDelay_ValueChanged(object sender, EventArgs e)
        {
            EnablePlaySlideshow();
        }

        /// <summary>
        /// Shows the appropriate set of controls for the associated module tab that's selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControlModules_SelectedIndexChanged(object sender, EventArgs e)
        {
            toolStripSlideshow.Visible = false;
            toolStripScreenCapture.Visible = false;

            switch (tabControlModules.SelectedTab.Text)
            {
                case "Screen Capture":
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
        /// Takes screenshots as a preview (or clears the preview images) depending on the value of the capture delay slider controls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void previewInterval_ValueChanged(object sender, EventArgs e)
        {
            EnableStartScreenCapture();

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

                    ClearImages();
                    UpdatePreview();
                }
            }
            else
            {
                timerPreviewCapture.Enabled = false;

                ClearImages();
                UpdatePreview();
            }
        }

        /// <summary>
        /// Turns on scheduled screen capturing.
        /// </summary>
        private void ScheduleSet()
        {
            buttonScheduleSet.Enabled = false;
            buttonScheduleClear.Enabled = true;

            timerScheduledCaptureStop.Enabled = true;
            timerScheduledCaptureStart.Enabled = true;
        }

        /// <summary>
        /// Turns off scheduled screen capturing.
        /// </summary>
        private void ScheduleClear()
        {
            buttonScheduleSet.Enabled = true;
            buttonScheduleClear.Enabled = false;

            timerScheduledCaptureStop.Enabled = false;
            timerScheduledCaptureStart.Enabled = false;
        }

        /// <summary>
        /// Sets the image format that will be used based on the user's selection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxScheduleImageFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ScreenCapture.Format = comboBoxScheduleImageFormat.Text;
        }
        
        /// <summary>
        /// Show or hide the system tray icon depending on the option selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemShowSystemTrayIcon_CheckedChanged(object sender, EventArgs e)
        {
            notifyIcon.Visible = toolStripMenuItemShowSystemTrayIcon.Checked;
        }

        /// <summary>
        /// Determine if we need to show "Lock: On" or "Lock: Off" and if we need to lock the screen capture session or not.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxPassphraseLock_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPassphraseLock.Checked)
            {
                ScreenCapture.lockScreenCaptureSession = true;
            }
            else
            {
                ScreenCapture.lockScreenCaptureSession = false;
            }
        }

        /// <summary>
        /// Determines when we enable the "Set" button or disable the "Lock" checkbox (and "Set" button) for passphrase.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxPassphrase_TextChanged(object sender, EventArgs e)
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
        /// Enables or disables the checkbox responsible for starting scheduled screen capture sessions whenever the checkbox's state is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxScheduleStartOnSchedule_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxScheduleStartOnSchedule.Checked)
            {
                checkBoxScheduleStartAt.Checked = false;
            }
            else
            {
                checkBoxScheduleStartAt.Checked = true;
            }
        }

        /// <summary>
        /// Enables or disables the checkbox responsible for stopping scheduled screen capture sessions whenever the checkbox's state is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxScheduleStartAt_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxScheduleStartAt.Checked)
            {
                checkBoxScheduleStartOnSchedule.Checked = false;
            }
            else
            {
                checkBoxScheduleStartOnSchedule.Checked = true;
            }
        }

        /// <summary>
        /// Takes screenshots when preview is enabled otherwise the preview images will be cleared.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonPreview_CheckedChanged(object sender, EventArgs e)
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
                ClearImages();
                UpdatePreview();
            }

            timerPreviewCapture.Enabled = toolStripButtonPreview.Checked;
        }

        /// <summary>
        /// Turns on "Debug Mode" so we can write debugging messages to "autoscreen.log".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemDebugMode_CheckedChanged(object sender, EventArgs e)
        {
            Log.Enabled = toolStripMenuItemDebugMode.Checked;
        }
    }
}