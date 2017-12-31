//////////////////////////////////////////////////////////
// Auto Screen Capture 2.0.8
// autoscreen.FormMain.cs
//
// Written by Gavin Kendall (gavinkendall@gmail.com)
// Thursday, 15 May 2008 - Sunday, 31 December 2017

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
        /// The screenshots folder to contain all of the screenshots taken.
        /// </summary>
        private static string screenshotsFolder = AppDomain.CurrentDomain.BaseDirectory + @"screenshots\";

        /// <summary>
        /// The default image format is JPEG, but feel free to change this setting.
        /// </summary>
        private static string imageFormat = ImageFormatSpec.NAME_JPEG;

        /// <summary>
        /// The application to execute whenever the user chooses to open their screenshots folder or edits the selected screenshot.
        /// </summary>
        private static string windowsExplorer = "explorer";

        /// <summary>
        /// Other forms.
        /// </summary>
        private FormAddEditor formAddEditor = new FormAddEditor();
        private FormEnterPassphrase formEnterPassphrase = new FormEnterPassphrase();

        /// <summary>
        /// Threads for background operations.
        /// </summary>
        private BackgroundWorker runFileSearchThread = null;
        private BackgroundWorker runFolderSearchThread = null;

        /// <summary>
        /// Delegates for the threads.
        /// </summary>
        private delegate void UpdateScreenshotPreviewDelegate();
        private delegate void RunFileSearchDelegate(DoWorkEventArgs e);
        private delegate void RunFolderSearchDelegate(DoWorkEventArgs e);

        /// <summary>
        /// Default settings used by the command line parser. 
        /// </summary>
        private const int CAPTURE_LIMIT_MIN = 0;
        private const int CAPTURE_LIMIT_MAX = 9999;
        private const int CAPTURE_DELAY_DEFAULT = 60000;
        private const int IMAGE_RESOLUTION_RATIO_MIN = 1;
        private const int IMAGE_RESOLUTION_RATIO_MAX = 100;

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

        /// <summary>
        /// Constructor for the main form. Arugments from the command line can be passed to it.
        /// </summary>
        /// <param name="args">Arguments from the command line</param>
        public FormMain(string[] args)
        {
            InitializeComponent();

            Text = Properties.Settings.Default.ApplicationName;
            notifyIcon.Text = Properties.Settings.Default.ApplicationName;

            Log.Write("Initializing image format collection.");

            ImageFormatCollection.Initialize();

            runFileSearchThread = new BackgroundWorker();
            runFileSearchThread.WorkerReportsProgress = false;
            runFileSearchThread.WorkerSupportsCancellation = true;
            runFileSearchThread.DoWork += new DoWorkEventHandler(runFileSearchThread_DoWork);

            runFolderSearchThread = new BackgroundWorker();
            runFolderSearchThread.WorkerReportsProgress = false;
            runFolderSearchThread.WorkerSupportsCancellation = true;
            runFolderSearchThread.DoWork += new DoWorkEventHandler(runFolderSearchThread_DoWork);

            Log.Write("Initializing slideshow component.");

            Slideshow.Initialize();
            Slideshow.OnPlaying += new EventHandler(Slideshow_Playing);

            Log.Write("Loading editors and building screenshot preview context menu.");

            EditorCollection.Load();
            BuildScreenshotPreviewContextMenu();

            Log.Write("Loading screenshots to generate a history of what was captured.");

            ScreenshotCollection.Load();

            Log.Write("Setting screenshots directory.");

            if (Directory.Exists(Properties.Settings.Default.ScreenshotsDirectory))
            {
                textBoxScreenshotsFolderSearch.Text = Properties.Settings.Default.ScreenshotsDirectory;
            }
            else
            {
                textBoxScreenshotsFolderSearch.Text = screenshotsFolder;
            }

            Log.Write("Setting screenshots macro.");

            if (string.IsNullOrEmpty(Properties.Settings.Default.Macro))
            {
                textBoxMacro.Text = MacroParser.DefaultMacro;
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
            toolStripMenuItemDebugMode.CheckedChanged += ToolStripMenuItemDebugMode_CheckedChanged;

            toolStripMenuItemDemoModeAtApplicationStartup.Checked = Properties.Settings.Default.DemoModeCheck;
            toolStripMenuItemShowSystemTrayIcon.Checked = Properties.Settings.Default.ShowSystemTrayIcon;
            toolStripMenuItemExitOnCloseWindow.Checked = Properties.Settings.Default.ExitOnCloseWindowCheck;
            toolStripMenuItemScheduleAtApplicationStartup.Checked = Properties.Settings.Default.ScheduleOnAtStartupCheck;
            toolStripMenuItemOpenOnStopScreenCapture.Checked = Properties.Settings.Default.OpenOnScreenCaptureStopCheck;
            toolStripMenuItemOpenAtApplicationStartup.Checked = Properties.Settings.Default.OpenOnApplicationStartupCheck;
            toolStripMenuItemCloseWindowOnStartCapture.Checked = Properties.Settings.Default.CloseWindowOnStartCaptureCheck;
            toolStripMenuItemSearchOnStopScreenCapture.Checked = Properties.Settings.Default.SearchScreenshotsOnStopScreenCaptureCheck;
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

            Log.Write("Loading region capture controls for X, Y, Width, Height, and Reset on each available screen.");

            int count = 0;

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

            if (toolStripMenuItemDemoModeAtApplicationStartup.Checked)
            {
                checkBoxAutoReset.Checked = true;
            }

            EnableStartScreenCapture();

            if (args.Length > 0)
            {
                ParseCommandLine(args);
            }

            CaptureLimitCheck();

            if (toolStripMenuItemScheduleAtApplicationStartup.Checked)
            {
                ScheduleSet();
            }
        }

        private void ToolStripMenuItemDebugMode_CheckedChanged(object sender, EventArgs e)
        {
            Log.Enabled = toolStripMenuItemDebugMode.Checked;
        }

        /// <summary>
        /// When this form loads we'll need to read from the application's properties
        /// in order to prepare it with the user's last saved options.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormViewer_Load(object sender, EventArgs e)
        {
            SearchFolders();

            if (toolStripMenuItemOpenAtApplicationStartup.Checked)
            {
                OpenWindow();
            }
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
        /// Set the image format and search for screenshots whenever the image format filter gets changed.
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

            SearchFiles();
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
                folder = screenshotsFolder;
            }

            if (!folder.EndsWith(@"\"))
            {
                folder += @"\";
            }

            Directory.SetCurrentDirectory(folder);

            return folder;
        }

        /// <summary>
        /// Search for all the screenshot folders. They should be in the format yyyy-mm-dd.
        /// Any folders found matching this format are then bolded in the calendar so the user
        /// understands that these were the days when screen capture sessions had been running.
        /// </summary>
        private void SearchFolders()
        {
            textBoxScreenshotsFolderSearch.Text = CorrectDirectoryPath(textBoxScreenshotsFolderSearch.Text);

            Log.Write("Searching for screenshot folders in \"" + textBoxScreenshotsFolderSearch.Text + "\"");

            ClearPreview();
            DisableToolStripButtons();

            monthCalendar.BoldedDates = null;

            if (Directory.Exists(textBoxScreenshotsFolderSearch.Text))
            {
                if (!runFolderSearchThread.IsBusy)
                {
                    runFolderSearchThread.RunWorkerAsync();
                }
            }
        }

        /// <summary>
        /// Searches for the files within the screenshots folder.
        /// </summary>
        private void SearchFiles()
        {
            listBoxScreenshots.BeginUpdate();

            textBoxScreenshotsFolderSearch.Text = CorrectDirectoryPath(textBoxScreenshotsFolderSearch.Text);

            Log.Write("Searching for screenshot files in \"" + textBoxScreenshotsFolderSearch.Text + "\"");
            Log.Write("Macro being used is \"" + textBoxMacro.Text + "\"");

            ClearPreview();
            DisableToolStripButtons();

            if (Directory.Exists(Path.GetDirectoryName(textBoxScreenshotsFolderSearch.Text)))
            {
                if (!runFileSearchThread.IsBusy)
                {
                    runFileSearchThread.RunWorkerAsync();
                }
            }

            listBoxScreenshots.EndUpdate();

            // It's very important here to disable all the slideshow controls if there were
            // no files found. There's no point keeping the controls enabled if there are no files.
            if (listBoxScreenshots.Items.Count == 0)
            {
                toolStripButtonFirstSlide.Enabled = false;
                toolStripButtonPreviousSlide.Enabled = false;
                toolStripButtonPlaySlideshow.Enabled = false;
                toolStripButtonNextSlide.Enabled = false;
                toolStripButtonLastSlide.Enabled = false;
            }
        }

        /// <summary>
        /// This thread is responsible for finding the screenshots that were taken.
        /// </summary>
        /// <param name="e"></param>
        private void RunFileSearch(DoWorkEventArgs e)
        {
            if (listBoxScreenshots.InvokeRequired)
            {
                listBoxScreenshots.Invoke(new RunFileSearchDelegate(RunFileSearch), new object[] { e });
            }
            else
            {
                int count = 0;

                foreach (Screen screen in Screen.AllScreens)
                {
                    count++;

                    // The old way of finding screenshots taken.
                    // This is to maintain backwards compatibility with older versions of Auto Screen Capture.
                    if (Directory.Exists(textBoxScreenshotsFolderSearch.Text + monthCalendar.SelectionStart.ToString(MacroParser.DateFormat) + "\\" + count.ToString()))
                    {
                        string[] files = FileSystem.GetFiles(textBoxScreenshotsFolderSearch.Text, count.ToString(), monthCalendar.SelectionStart.ToString(MacroParser.DateFormat));

                        if (files != null)
                        {
                            listBoxScreenshots.Items.AddRange(files);
                        }
                    }

                    // The new way is to use the screenshot collection class.
                    for (int i = 0; i < ScreenshotCollection.Count; i++)
                    {
                        Screenshot screenshot = ScreenshotCollection.Get(i);

                        if (screenshot != null)
                        {
                            if (File.Exists(screenshot.Path) && screenshot.Screen == count && screenshot.Date == monthCalendar.SelectionStart.ToString(MacroParser.DateFormat))
                            {
                                listBoxScreenshots.Items.Add(screenshot.Filename);
                            }
                        }
                    }
                }

                // If we do find files then make sure the user can use the slideshow.
                if (listBoxScreenshots.Items.Count > 0)
                {
                    monthCalendar.Enabled = true;
                    toolStripComboBoxImageFormatFilter.Enabled = true;

                    listBoxScreenshots.SelectedIndex = 0;

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
        private void RunFolderSearch(DoWorkEventArgs e)
        {
            if (monthCalendar.InvokeRequired)
            {
                monthCalendar.Invoke(new RunFolderSearchDelegate(RunFolderSearch), new object[] { e });
            }
            else
            {
                if (!string.IsNullOrEmpty(textBoxScreenshotsFolderSearch.Text))
                {
                    ArrayList selectedFolders = new ArrayList();

                    // The old way of obtaining the dates from screenshots taken.
                    // This is to maintain backwards compatibility with older versions of Auto Screen Capture.
                    string[] dirs = Directory.GetDirectories(textBoxScreenshotsFolderSearch.Text);

                    int count = 0;

                    foreach (Screen screen in Screen.AllScreens)
                    {
                        count++;

                        // Go through each directory found and make sure that the sub-directories match
                        // with the format yyyy-mm-dd.
                        for (int i = 0; i < dirs.Length; i++)
                        {
                            Regex rgx = new Regex(@"^\d{4}-\d{2}-\d{2}$");

                            if (rgx.IsMatch(Path.GetFileName(dirs[i])))
                            {
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

                    // The new way of obtaining the dates from screenshots taken.
                    foreach (string date in ScreenshotCollection.GetDates())
                    {
                        selectedFolders.Add(date);
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
        }

        /// <summary>
        /// Converts the filter string into a DateTime object. Used by the RunFolderSearch thread so we can set bolded dates in the calendar.
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

                // Make sure to update the calendar with any folders found due to the freshly-made screenshots.
                // We'll let the user decide what folder of screenshots they want to see after the search is done.
                if (toolStripMenuItemSearchOnStopScreenCapture.Checked)
                {
                    SearchFolders();
                }

                // Some people want to see this window immediately after the session has stopped.
                if (toolStripMenuItemOpenOnStopScreenCapture.Checked)
                {
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
            radioButtonModePreview.Checked = false;
            int slideshowDelay = GetSlideshowDelay();

            DisableControls();
            DisableSystemTrayMenus();

            if (listBoxScreenshots.Items.Count > 0 && slideshowDelay > 0)
            {
                if (Slideshow.Index == Slideshow.Count - 1)
                {
                    Slideshow.First();
                    listBoxScreenshots.SelectedIndex = Slideshow.Index;
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

            if (listBoxScreenshots.Items.Count > 0)
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
            Log.Write("Starting new screen capture session.");

            SaveApplicationSettings();

            radioButtonModePreview.Checked = false;

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
            if (runFolderSearchThread.IsBusy)
            {
                runFolderSearchThread.CancelAsync();
            }

            // Stop the file search thread if it's busy.
            if (runFileSearchThread.IsBusy)
            {
                runFileSearchThread.CancelAsync();
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
                TakeScreenshot();
                DisplayCaptureStatus(StatusMessage.LAST_CAPTURE_APP, StatusMessage.LAST_CAPTURE_ICON, true);
            }

            // Start taking screenshots.
            Log.Write("Starting screen capture.");

            timerScreenCapture.Interval = delay;
            timerScreenCapture.Enabled = true;
        }

        /// <summary>
        /// Whenever the user clicks on a screenshot in the list of screenshots then make sure to update the preview of screenshots.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxScreenshots_SelectedIndexChanged(object sender, EventArgs e)
        {
            radioButtonModePreview.Checked = false;

            Slideshow.Index = listBoxScreenshots.SelectedIndex;
            Slideshow.Count = listBoxScreenshots.Items.Count;

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
            textBoxScreenshotsFolderSearch.Enabled = false;
            toolStripComboBoxImageFormatFilter.Enabled = false;

            numericUpDownSlideshowDelayHours.Enabled = false;
            numericUpDownSlideshowDelayMinutes.Enabled = false;
            numericUpDownSlideshowDelaySeconds.Enabled = false;
            numericUpDownSlideshowDelayMilliseconds.Enabled = false;

            numericUpDownHoursInterval.Enabled = false;
            numericUpDownMinutesInterval.Enabled = false;
            numericUpDownSecondsInterval.Enabled = false;
            numericUpDownMillisecondsInterval.Enabled = false;

            numericUpDownSlideSkip.Enabled = false;
            checkBoxSlideSkip.Enabled = false;

            CaptureLimitCheck();
            numericUpDownImageResolutionRatio.Enabled = false;

            checkBoxCaptureLimit.Enabled = false;
            checkBoxInitialScreenshot.Enabled = false;

            toolStripSplitButtonStartScreenCapture.Enabled = false;

            checkBoxAutoReset.Enabled = false;
        }

        /// <summary>
        /// Enables the appropriate controls when the slideshow is paused or stopped.
        /// </summary>
        private void EnableControls()
        {
            monthCalendar.Enabled = true;
            textBoxScreenshotsFolderSearch.Enabled = true;
            toolStripComboBoxImageFormatFilter.Enabled = true;

            numericUpDownSlideshowDelayHours.Enabled = true;
            numericUpDownSlideshowDelayMinutes.Enabled = true;
            numericUpDownSlideshowDelaySeconds.Enabled = true;
            numericUpDownSlideshowDelayMilliseconds.Enabled = true;

            numericUpDownHoursInterval.Enabled = true;
            numericUpDownMinutesInterval.Enabled = true;
            numericUpDownSecondsInterval.Enabled = true;
            numericUpDownMillisecondsInterval.Enabled = true;

            numericUpDownSlideSkip.Enabled = true;
            checkBoxSlideSkip.Enabled = true;

            CaptureLimitCheck();
            numericUpDownImageResolutionRatio.Enabled = true;

            checkBoxCaptureLimit.Enabled = true;
            checkBoxInitialScreenshot.Enabled = true;

            if (!timerScreenCapture.Enabled)
            {
                toolStripSplitButtonStartScreenCapture.Enabled = true;
            }

            checkBoxAutoReset.Enabled = true;
        }

        /// <summary>
        /// Clears the screenshots preview when searching for files and folders.
        /// </summary> 
        private void ClearPreview()
        {
            Slideshow.Clear();
            listBoxScreenshots.Items.Clear();

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
            if (!radioButtonModePreview.Checked)
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
                        Slideshow.SelectedSlide = listBoxScreenshots.Items[Slideshow.Index].ToString();

                        DisplayImages(false);
                    }
                }
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

            toolStripComboBoxImageFormatFilter.Items.Clear();
            toolStripComboBoxImageFormatFilter.Items.Add("*.*");

            for (int i = 0; i < ImageFormatCollection.Count; i++)
            {
                toolStripComboBoxImageFormatFilter.Items.Add("*" + ImageFormatCollection.Get(i).Extension);
            }

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
            listBoxScreenshots.SelectedIndex = Slideshow.Index;
        }

        /// <summary>
        /// Shows the previous slide in the slideshow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonPreviousSlide_Click(object sender, EventArgs e)
        {
            Slideshow.Previous();
            listBoxScreenshots.SelectedIndex = Slideshow.Index;
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
            listBoxScreenshots.SelectedIndex = Slideshow.Index;
        }

        /// <summary>
        /// Shows the last slide in the slideshow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonLastSlide_Click(object sender, EventArgs e)
        {
            Slideshow.Last();
            listBoxScreenshots.SelectedIndex = Slideshow.Index;
        }

        /// <summary>
        /// Starts a screen capture session based on the image format selected by the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemStartScreenCapture_Click(object sender, EventArgs e)
        {
            if (sender.ToString().Equals(toolStripSplitButtonStartScreenCapture.Text))
            {
                imageFormat = ImageFormatSpec.NAME_JPEG;
            }
            else
            {
                imageFormat = sender.ToString();
            }

            if (!string.IsNullOrEmpty(imageFormat))
            {
                bool initial = false;
                string folder = textBoxScreenshotsFolderSearch.Text;
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

                // Cancel the folder search if it's currently running.
                if (runFolderSearchThread.IsBusy)
                {
                    runFolderSearchThread.CancelAsync();
                }

                // Hide the system tray icon.
                notifyIcon.Visible = false;

                // Exit.
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Runs the file search thread.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void runFileSearchThread_DoWork(object sender, DoWorkEventArgs e)
        {
            RunFileSearch(e);
        }
        
        /// <summary>
        /// Runs the folder search thread.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void runFolderSearchThread_DoWork(object sender, DoWorkEventArgs e)
        {
            RunFolderSearch(e);
        }

        /// <summary>
        /// Updates the list of screenshots with the current slideshow index when the slideshow is playing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Slideshow_Playing(object sender, EventArgs e)
        {
            listBoxScreenshots.SelectedIndex = Slideshow.Index;
        }

        /// <summary>
        /// Figures out if the "Play Slideshow" control should be enabled or disabled.
        /// </summary>
        private void EnablePlaySlideshow()
        {
            if (GetSlideshowDelay() > 0 && listBoxScreenshots.Items.Count > 0)
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

                numericUpDownHoursInterval.Enabled = true;
                labelHoursInterval.Enabled = true;
                checkBoxInitialScreenshot.Enabled = true;
                numericUpDownMinutesInterval.Enabled = true;
                labelMinutesInterval.Enabled = true;
                checkBoxCaptureLimit.Enabled = true;
                numericUpDownCaptureLimit.Enabled = true;
                labelLimit.Enabled = true;
                numericUpDownSecondsInterval.Enabled = true;
                labelSecondsInterval.Enabled = true;
                labelAt.Enabled = true;
                numericUpDownImageResolutionRatio.Enabled = true;
                labelPercentResolution.Enabled = true;
                numericUpDownMillisecondsInterval.Enabled = true;
                labelMillisecondsInterval.Enabled = true;
                radioButtonModePreview.Enabled = true;
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
            labelHoursInterval.Enabled = false;
            checkBoxInitialScreenshot.Enabled = false;
            numericUpDownMinutesInterval.Enabled = false;
            labelMinutesInterval.Enabled = false;
            checkBoxCaptureLimit.Enabled = false;
            numericUpDownCaptureLimit.Enabled = false;
            labelLimit.Enabled = false;
            numericUpDownSecondsInterval.Enabled = false;
            labelSecondsInterval.Enabled = false;
            labelAt.Enabled = false;
            numericUpDownImageResolutionRatio.Enabled = false;
            labelPercentResolution.Enabled = false;
            numericUpDownMillisecondsInterval.Enabled = false;
            labelMillisecondsInterval.Enabled = false;
            radioButtonModePreview.Enabled = false;
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
        }

        /// <summary>
        /// Saves the user's current settings so we can load them at a later time when the user executes the application.
        /// </summary>
        private void SaveApplicationSettings()
        {
            Log.Write("Saving application settings.");

            Properties.Settings.Default.ScreenshotsDirectory = CorrectDirectoryPath(textBoxScreenshotsFolderSearch.Text);

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
            Properties.Settings.Default.DemoModeCheck = toolStripMenuItemDemoModeAtApplicationStartup.Checked;
            Properties.Settings.Default.ExitOnCloseWindowCheck = toolStripMenuItemExitOnCloseWindow.Checked;
            Properties.Settings.Default.ScheduleOnAtStartupCheck = toolStripMenuItemScheduleAtApplicationStartup.Checked;
            Properties.Settings.Default.OpenOnScreenCaptureStopCheck = toolStripMenuItemOpenOnStopScreenCapture.Checked;
            Properties.Settings.Default.OpenOnApplicationStartupCheck = toolStripMenuItemOpenAtApplicationStartup.Checked;
            Properties.Settings.Default.CloseWindowOnStartCaptureCheck = toolStripMenuItemCloseWindowOnStartCapture.Checked;
            Properties.Settings.Default.SearchScreenshotsOnStopScreenCaptureCheck = toolStripMenuItemSearchOnStopScreenCapture.Checked;
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

            Properties.Settings.Default.LockScreenCaptureSession = checkBoxPassphraseLock.Checked;

            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Displays the current mode that we're in.
        /// </summary>
        /// <param name="status">Can be "Preview", "Normal", or "Static".</param>
        private void DisplayModeStatus(string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                statusStrip.Items["statusStripLabelMode"].Text = "Mode: " + status;
            }
        }

        /// <summary>
        /// Displays the status of scheduled screen capture sessions.
        /// </summary>
        /// <param name="status">Can be "On" or "Off".</param>
        private void DisplayScheduleStatus(string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                statusStrip.Items["statusStripLabelSchedule"].Text = "Schedule: " + status;
            }
        }

        /// <summary>
        /// Displays the status of the locked in passphrase used to challenge the user when the running
        /// screen capture session is stopped, the main window is opened, or the application is exiting.
        /// </summary>
        /// <param name="status"></param>
        private void DisplayLockStatus(string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                statusStrip.Items["statusStripLabelLock"].Text = "Lock: " + status;
            }
        }

        /// <summary>
        /// Displays the status of the "Exit application when closing this window" option.
        /// </summary>
        /// <param name="status"></param>
        private void DisplayExitStatus(string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                statusStrip.Items["statusStripLabelExit"].Text = "Exit: " + status;
            }
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

                // This isn't entirely accurate. Need to fix.
                //statusStrip.Items["statusStripLabelLastCapture"].Text = "Last capture: " + MacroParser.ParseTags(statusApp, imageFormat, null);
            }

            if (!string.IsNullOrEmpty(statusApp) && !string.IsNullOrEmpty(statusIcon))
            {
                // TODO: Fix this.
                //notifyIcon.Text = appName + "\n" + MacroParser.ParseTags(statusIcon, imageFormat);
                //statusStrip.Items["statusStripLabelLastCapture"].Text = "Last capture: " + MacroParser.ParseTags(statusApp, imageFormat);
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
                textBoxScreenshotsFolderSearch.Text = browser.SelectedPath;
                SearchFolders();
            }
        }

        /// <summary>
        /// Opens this window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemOpen_Click(object sender, EventArgs e)
        {
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
            MessageBox.Show(Properties.Settings.Default.ApplicationName + " " + Properties.Settings.Default.ApplicationVersion + "\n" + Properties.Settings.Default.ApplicationAuthor, "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (listBoxScreenshots.SelectedIndex > -1)
            {
                string selectedFile = FileSystem.GetImageFilePath(Slideshow.SelectedSlide, Slideshow.SelectedScreen == 0 ? 1 : Slideshow.SelectedScreen);

                if (File.Exists(selectedFile))
                {
                    Process.Start(windowsExplorer, "/select,\"" + selectedFile + "\"");
                }
            }
        }

        /// <summary>
        /// Parses the command line and processes the commands the user has chosen from the command line.
        /// </summary>
        /// <param name="args"></param>
        private void ParseCommandLine(string[] args)
        {
            try
            {
                Log.Write("Parsing command line arguments.");

                bool isScheduled = false;

                bool initial = false;
                checkBoxInitialScreenshot.Checked = false;

                int limit = CAPTURE_LIMIT_MIN;
                checkBoxCaptureLimit.Checked = false;
                numericUpDownCaptureLimit.Value = (decimal)CAPTURE_LIMIT_MIN;

                int delay = CAPTURE_DELAY_DEFAULT;
                numericUpDownHoursInterval.Value = 0;
                numericUpDownMinutesInterval.Value = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(delay)).Minutes);
                numericUpDownSecondsInterval.Value = 0;
                numericUpDownMillisecondsInterval.Value = 0;

                int ratio = IMAGE_RESOLUTION_RATIO_MAX;
                numericUpDownImageResolutionRatio.Value = (decimal)IMAGE_RESOLUTION_RATIO_MAX;

                string folder = screenshotsFolder;
                string macro = MacroParser.DefaultMacro;

                imageFormat = ImageFormatSpec.NAME_JPEG;
                comboBoxScheduleImageFormat.SelectedItem = ImageFormatSpec.NAME_JPEG;

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

                checkBoxScheduleStopAt.Checked = false;
                checkBoxScheduleStartAt.Checked = false;
                checkBoxScheduleOnTheseDays.Checked = false;

                toolStripMenuItemOpenOnStopScreenCapture.Checked = false;
                toolStripMenuItemOpenAtApplicationStartup.Checked = false;
                toolStripMenuItemCloseWindowOnStartCapture.Checked = true;
                toolStripMenuItemScheduleAtApplicationStartup.Checked = false;
                toolStripMenuItemShowSlideshowOnStopScreenCapture.Checked = false;

                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i] != null)
                    {
                        Log.Write("Parsing command line argument at index " + i + " --> " + args[i]);
                    }

                    if (rgxCommandLineFolder.IsMatch(args[i]))
                    {
                        folder = CorrectDirectoryPath(rgxCommandLineFolder.Match(args[i]).Groups["Folder"].Value.ToString());
                        textBoxScreenshotsFolderSearch.Text = CorrectDirectoryPath(rgxCommandLineFolder.Match(args[i]).Groups["Folder"].Value.ToString());
                    }

                    if (rgxCommandLineMacro.IsMatch(args[i]))
                    {
                        macro = rgxCommandLineMacro.Match(args[i]).Groups["Macro"].Value;
                        textBoxScreenshotsFolderSearch.Text = rgxCommandLineMacro.Match(args[i]).Groups["Macro"].Value;
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
                            numericUpDownImageResolutionRatio.Value = (decimal)cmdRatio;
                        }
                    }

                    if (rgxCommandLineLimit.IsMatch(args[i]))
                    {
                        int cmdLimit = Convert.ToInt32(rgxCommandLineLimit.Match(args[i]).Groups["Limit"].Value);

                        if (cmdLimit >= CAPTURE_LIMIT_MIN && cmdLimit <= CAPTURE_LIMIT_MAX)
                        {
                            limit = cmdLimit;
                            checkBoxCaptureLimit.Checked = true;
                            numericUpDownCaptureLimit.Value = (decimal)cmdLimit;
                        }
                    }

                    if (rgxCommandLineFormat.IsMatch(args[i]))
                    {
                        imageFormat = rgxCommandLineFormat.Match(args[i]).Groups["Format"].Value;
                        comboBoxScheduleImageFormat.SelectedItem = rgxCommandLineFormat.Match(args[i]).Groups["Format"].Value;
                    }

                    if (rgxCommandLineCaptureDelay.IsMatch(args[i]))
                    {
                        int hours = Convert.ToInt32(rgxCommandLineCaptureDelay.Match(args[i]).Groups["Hours"].Value);
                        int minutes = Convert.ToInt32(rgxCommandLineCaptureDelay.Match(args[i]).Groups["Minutes"].Value);
                        int seconds = Convert.ToInt32(rgxCommandLineCaptureDelay.Match(args[i]).Groups["Seconds"].Value);
                        int milliseconds = Convert.ToInt32(rgxCommandLineCaptureDelay.Match(args[i]).Groups["Milliseconds"].Value);

                        numericUpDownHoursInterval.Value = (decimal)hours;
                        numericUpDownMinutesInterval.Value = (decimal)minutes;
                        numericUpDownSecondsInterval.Value = (decimal)seconds;
                        numericUpDownMillisecondsInterval.Value = (decimal)milliseconds;

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
                }

                ScreenCapture.runningFromCommandLine = true;

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
            ToolStripItem[] editWithMenuItemSearch = contextMenuStripScreenshotPreview.Items.Find("editWithToolStripMenuItem", false);

            if (editWithMenuItemSearch.Length == 1)
            {
                ToolStripMenuItem editWithMenuItem = (ToolStripMenuItem)editWithMenuItemSearch[0];

                if (editWithMenuItem != null)
                {
                    editWithMenuItem.DropDownItems.Clear();

                    ToolStripItem addEditorItem = new ToolStripMenuItem("Add Editor ...");
                    addEditorItem.Click += new EventHandler(addEditorToolStripMenuItem_Click);
                    editWithMenuItem.DropDownItems.Add(addEditorItem);
                    editWithMenuItem.DropDownItems.Add(new ToolStripSeparator());

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
                                editWithMenuItem.DropDownItems.Add(editorItem);
                            }
                        }
                    }
                }
            }
        }

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
            if (listBoxScreenshots.SelectedIndex > -1)
            {
                Editor editor = EditorCollection.GetByName(sender.ToString());
                string selectedFile = FileSystem.GetImageFilePath(Slideshow.SelectedSlide, Slideshow.SelectedScreen == 0 ? 1 : Slideshow.SelectedScreen);

                if (File.Exists(selectedFile))
                {
                    Process.Start(editor.Application, editor.Arguments.Replace("%screenshot%", "\"" + selectedFile + "\""));
                }
            }
        }

        /// <summary>
        /// Opens the main screenshots folder in Windows Explorer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOpenFolder_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(textBoxScreenshotsFolderSearch.Text))
            {
                Process.Start(windowsExplorer, textBoxScreenshotsFolderSearch.Text);
            }
        }

        /// <summary>
        /// Makes sure that we're showing the correct tab.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControlScreens_SelectedIndexChanged(object sender, EventArgs e)
        {
            Slideshow.SelectedScreen = tabControlScreens.SelectedIndex <= ScreenCapture.SCREEN_MAX ? tabControlScreens.SelectedIndex : 1;
        }

        /// <summary>
        /// The timer for "Demo Mode".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerDemoCapture_Tick(object sender, EventArgs e)
        {
            TakeDemoScreenshots();            
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
            string path = string.Empty;
            string screenName = string.Empty;

            foreach (Screen screen in Screen.AllScreens)
            {
                count++;

                if (CaptureTheScreen(count))
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
                            path = ScreenCapture.Folder + MacroParser.ParseTags(ScreenCapture.Macro, ScreenCapture.Format, screenName);
                            ScreenCapture.TakeScreenshot(screen, screenName, path, count);
                        }
                    }
                }
            }

            ScreenCapture.Count++;
        }

        private bool CaptureTheScreen(int screenNumber)
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
            }

            return capture;
        }

        /// <summary>
        /// Takes the screenshots in "Demo Mode".
        /// </summary>
        private void TakeDemoScreenshots()
        {
            DisplayImages(true);
        }

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
        /// <param name="demo"></param>
        private void DisplayImages(bool demo)
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

                    if (demo)
                    {
                        Bitmap bitmap = ScreenCapture.GetScreenBitmap(screen, (int)numericUpDownImageResolutionRatio.Value);

                        if (bitmap != null)
                        {
                            images.Add(bitmap);
                        }
                    }
                }
            }

            if (!demo)
            {
                images = FileSystem.GetImages(Slideshow.SelectedSlide, monthCalendar.SelectionStart);
            }

            if (images.Count >= 1)
            {
                if (CaptureTheScreen(1) || !demo)
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
                if (CaptureTheScreen(2) || !demo)
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
                if (CaptureTheScreen(3) || !demo)
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
                if (CaptureTheScreen(4) || !demo)
                {
                    pictureBoxScreenshotPreviewMonitor4.Image = (Image)images[3];
                }
                else
                {
                    pictureBoxScreenshotPreviewMonitor4.Image = null;
                }
            }

            if (demo)
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
                        string folder = textBoxScreenshotsFolderSearch.Text;
                        string macro = textBoxMacro.Text;

                        int delay = GetCaptureDelay();
                        int limit = (int)numericUpDownCaptureLimit.Value;
                        int ratio = (int)numericUpDownImageResolutionRatio.Value;

                        if (!checkBoxCaptureLimit.Checked) { limit = 0; }

                        if (checkBoxInitialScreenshot.Checked)
                        {
                            initial = true;
                        }

                        StartScreenCapture(folder, macro, imageFormat, delay, limit, ratio, initial);

                        timerScheduledCaptureStart.Enabled = false;
                    }
                }
                else
                {
                    bool initial = false;
                    string folder = textBoxScreenshotsFolderSearch.Text;
                    string macro = textBoxMacro.Text;

                    int delay = GetCaptureDelay();
                    int limit = (int)numericUpDownCaptureLimit.Value;
                    int ratio = (int)numericUpDownImageResolutionRatio.Value;

                    if (!checkBoxCaptureLimit.Checked) { limit = 0; }

                    if (checkBoxInitialScreenshot.Checked)
                    {
                        initial = true;
                    }

                    StartScreenCapture(folder, macro, imageFormat, delay, limit, ratio, initial);

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
                            string folder = textBoxScreenshotsFolderSearch.Text;
                            string macro = textBoxMacro.Text;

                            int delay = GetCaptureDelay();
                            int limit = (int)numericUpDownCaptureLimit.Value;
                            int ratio = (int)numericUpDownImageResolutionRatio.Value;

                            if (!checkBoxCaptureLimit.Checked) { limit = 0; }

                            if (checkBoxInitialScreenshot.Checked)
                            {
                                initial = true;
                            }

                            StartScreenCapture(folder, macro, imageFormat, delay, limit, ratio, initial);
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
                        string folder = textBoxScreenshotsFolderSearch.Text;
                        string macro = textBoxMacro.Text;

                        int delay = GetCaptureDelay();
                        int limit = (int)numericUpDownCaptureLimit.Value;
                        int ratio = (int)numericUpDownImageResolutionRatio.Value;

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
        /// Takes screenshots in "Demo Mode" (or clears the preview images) depending on the value of the capture delay slider controls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void demoInterval_ValueChanged(object sender, EventArgs e)
        {
            EnableStartScreenCapture();

            int demoInterval = GetCaptureDelay();

            if (demoInterval > 0)
            {
                timerDemoCapture.Interval = demoInterval;

                if (radioButtonModePreview.Checked)
                {
                    if (checkBoxInitialScreenshot.Checked)
                    {
                        TakeDemoScreenshots();
                    }

                    timerDemoCapture.Enabled = true;
                }
                else
                {
                    timerDemoCapture.Enabled = false;

                    ClearImages();
                    UpdatePreview();
                }
            }
            else
            {
                timerDemoCapture.Enabled = false;

                ClearImages();
                UpdatePreview();
            }
        }

        /// <summary>
        /// Turns on scheduled screen capturing.
        /// </summary>
        private void ScheduleSet()
        {
            DisplayScheduleStatus(StatusMessage.ON);

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
            DisplayScheduleStatus(StatusMessage.OFF);

            buttonScheduleSet.Enabled = true;
            buttonScheduleClear.Enabled = false;

            timerScheduledCaptureStop.Enabled = false;
            timerScheduledCaptureStart.Enabled = false;
        }

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

        /// <summary>
        /// Sets the image format that will be used based on the user's selection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxScheduleImageFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            imageFormat = comboBoxScheduleImageFormat.Text;
        }

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
                DisplayLockStatus(StatusMessage.ON);
            }
            else
            {
                ScreenCapture.lockScreenCaptureSession = false;
                DisplayLockStatus(StatusMessage.OFF);
            }
        }

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
        /// Determines what status message to show when the checkbox for exiting the application when the window closes is turned on or off.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemExitOnCloseWindow_CheckedChanged(object sender, EventArgs e)
        {
            if (toolStripMenuItemExitOnCloseWindow.Checked)
            {
                DisplayExitStatus(StatusMessage.ON);
            }
            else
            {
                DisplayExitStatus(StatusMessage.OFF);
            }
        }

        /// <summary>
        /// Takes screenshots when "Preview Mode" is enabled otherwise the preview images will be cleared.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonModePreview_CheckedChanged(object sender, EventArgs e)
        {
            if (GetCaptureDelay() > 0 && radioButtonModePreview.Checked)
            {
                DisplayModeStatus(StatusMessage.MODE_PREVIEW);

                if (checkBoxInitialScreenshot.Checked)
                {
                    TakeDemoScreenshots();
                }
            }
            else
            {
                ClearImages();
                UpdatePreview();
            }

            timerDemoCapture.Enabled = radioButtonModePreview.Checked;
        }

        private void radioButtonModeNormal_CheckedChanged(object sender, EventArgs e)
        {
            DisplayModeStatus(StatusMessage.MODE_NORMAL);
        }
    }
}