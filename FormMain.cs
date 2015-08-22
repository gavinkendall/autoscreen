//////////////////////////////////////////////////////////
// Auto Screen Capture 2.0.5
// autoscreen.FormMain.cs
//
// Written by Gavin Kendall (gavinkendall@gmail.com)
// Thursday, 15 May 2008 - Wednesday, 27 November 2013

using System;
using System.IO;
using System.Data;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace autoscreen
{
    /// <summary>
    /// The application's main window.
    /// </summary>
    public partial class FormMain : Form
    {
        /// <summary>
        /// The screenshots folder that will be created by default if the user doesn't specify their own folder.
        /// </summary>
        private static string screenshotsFolder = AppDomain.CurrentDomain.BaseDirectory + @"screenshots\";

        /// <summary>
        /// The default image format is PNG, but feel free to change this setting.
        /// </summary>
        private static string imageFormat = ImageFormatSpec.NAME_PNG;

        /// <summary>
        /// The application to execute whenever the user chooses to open their screenshots folder or edits the selected screenshot.
        /// </summary>
        private static string windowsExplorer = "explorer";

        /// <summary>
        /// Other forms.
        /// </summary>
        private Keylogger keylogger = new Keylogger();
        private FormAddEditor formAddEditor = new FormAddEditor();

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
        private const int CAPTURE_DELAY_DEFAULT = 1000;
        private const int IMAGE_RESOLUTION_RATIO_MIN = 1;
        private const int IMAGE_RESOLUTION_RATIO_MAX = 100;

        /// <summary>
        /// The various regular expressions used in the parsing of the command line arguments.
        /// </summary>
        private const string REGEX_COMMAND_LINE_INITIAL = @"^-initial$";
        private const string REGEX_COMMAND_LINE_FOLDER = "^-folder=(?<Folder>.+)$";
        private const string REGEX_COMMAND_LINE_RATIO = @"^-ratio=(?<Ratio>\d{1,3})$";
        private const string REGEX_COMMAND_LINE_LIMIT = @"^-limit=(?<Limit>\d{1,7})$";
        private const string REGEX_COMMAND_LINE_KEYLOG = @"^-keylog=(?<Keylog>[0-9|a-z|A-Z]+\.[a-z|A-Z]{3})$";
        private const string REGEX_COMMAND_LINE_FORMAT = @"^-format=(?<Format>(BMP|EMF|GIF|JPEG|PNG|TIFF|WMF))$";
        private const string REGEX_COMMAND_LINE_STOPAT = @"^-stopat=(?<Hours>\d{2}):(?<Minutes>\d{2}):(?<Seconds>\d{2})$";
        private const string REGEX_COMMAND_LINE_STARTAT = @"^-startat=(?<Hours>\d{2}):(?<Minutes>\d{2}):(?<Seconds>\d{2})$";
        private const string REGEX_COMMAND_LINE_DELAY = @"^-delay=(?<Hours>\d{2}):(?<Minutes>\d{2}):(?<Seconds>\d{2})\.(?<Milliseconds>\d{3})$";

        /// <summary>
        /// Constructor for the main form. Arugments from the command line can be passed to it.
        /// </summary>
        /// <param name="args">Arguments from the command line</param>
        public FormMain(string[] args)
        {
            InitializeComponent();

            this.Text = Properties.Settings.Default.ApplicationName;
            notifyIcon.Text = Properties.Settings.Default.ApplicationName;

            ImageFormatCollection.Initialize();

            runFileSearchThread = new BackgroundWorker();
            runFileSearchThread.WorkerReportsProgress = false;
            runFileSearchThread.WorkerSupportsCancellation = true;
            runFileSearchThread.DoWork += new DoWorkEventHandler(runFileSearchThread_DoWork);

            runFolderSearchThread = new BackgroundWorker();
            runFolderSearchThread.WorkerReportsProgress = false;
            runFolderSearchThread.WorkerSupportsCancellation = true;
            runFolderSearchThread.DoWork += new DoWorkEventHandler(runFolderSearchThread_DoWork);

            Slideshow.Initialize();
            Slideshow.OnPlaying += new EventHandler(Slideshow_Playing);

            ScreenCapture.Initialize();
            ScreenCapture.OnCapturing += new EventHandler(Screen_Capturing);

            EditorCollection.Load();
            BuildScreenshotPreviewContextMenu();

            if (Directory.Exists(Properties.Settings.Default.ScreenshotsDirectory))
            {
                textBoxScreenshotsFolderSearch.Text = Properties.Settings.Default.ScreenshotsDirectory;
            }

            comboBoxScheduleImageFormat.Items.Clear();
            toolStripMenuItemStartScreenCapture.DropDownItems.Clear();
            toolStripSplitButtonStartScreenCapture.DropDownItems.Clear();

            textBoxKeyloggingFile.Text = Properties.Settings.Default.KeyloggingFile;

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

            numericUpDownSlideSkip.Value = Properties.Settings.Default.SlideSkip;
            numericUpDownCaptureLimit.Value = Properties.Settings.Default.CaptureLimit;
            numericUpDownImageResolutionRatio.Value = Properties.Settings.Default.ImageResolutionRatio;

            checkBoxSlideSkip.Checked = Properties.Settings.Default.SlideSkipCheck;
            checkBoxCaptureLimit.Checked = Properties.Settings.Default.CaptureLimitCheck;
            checkBoxInitialScreenshot.Checked = Properties.Settings.Default.TakeInitialScreenshotCheck;

            toolStripMenuItemDemoModeAtApplicationStartup.Checked = Properties.Settings.Default.DemoModeCheck;
            toolStripMenuItemExitOnCloseWindow.Checked = Properties.Settings.Default.ExitOnCloseWindowCheck;
            toolStripMenuItemScheduleAtApplicationStartup.Checked = Properties.Settings.Default.ScheduleOnAtStartupCheck;
            toolStripMenuItemOpenOnStopScreenCapture.Checked = Properties.Settings.Default.OpenOnScreenCaptureStopCheck;
            toolStripMenuItemOpenAtApplicationStartup.Checked = Properties.Settings.Default.OpenOnApplicationStartupCheck;
            toolStripMenuItemCloseWindowOnStartCapture.Checked = Properties.Settings.Default.CloseWindowOnStartCaptureCheck;
            toolStripMenuItemSearchOnStopScreenCapture.Checked = Properties.Settings.Default.SearchScreenshotsOnStopScreenCaptureCheck;
            toolStripMenuItemShowSlideshowOnStopScreenCapture.Checked = Properties.Settings.Default.ShowSlideshowAfterScreenCaptureStopCheck;

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

            checkBoxEnableKeylogging.Checked = Properties.Settings.Default.KeyloggingEnabledCheck;
            checkBoxScheduleOnTheseDays.Checked = Properties.Settings.Default.CaptureOnTheseDaysCheck;

            dateTimePickerScheduleStopAt.Value = Properties.Settings.Default.CaptureStopAtValue;
            dateTimePickerScheduleStartAt.Value = Properties.Settings.Default.CaptureStartAtValue;

            if (toolStripMenuItemDemoModeAtApplicationStartup.Checked)
            {
                checkBoxDemoMode.Checked = true;
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

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
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

            ClearPreview();
            DisableToolStripButtons();

            if (Directory.Exists(textBoxScreenshotsFolderSearch.Text))
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
        /// This thread is responsible for doing the file search.
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
                // It's very easy. Just get the files in the screenshots folder based on the calendar selection.
                string[] files = FileSystem.GetFiles(textBoxScreenshotsFolderSearch.Text, monthCalendar.SelectionStart.ToString(StringHelper.DateFormat));

                if (files != null)
                {
                    listBoxScreenshots.Items.AddRange(files);
                }

                monthCalendar.Enabled = true;
                toolStripComboBoxImageFormatFilter.Enabled = true;

                // If we do find files then make sure the user can use the slideshow.
                if (listBoxScreenshots.Items.Count > 0)
                {
                    listBoxScreenshots.SelectedIndex = 0;

                    toolStripButtonNextSlide.Enabled = true;
                    toolStripButtonLastSlide.Enabled = true;

                    EnablePlaySlideshow();

                    UpdatePreview();
                }
            }
        }

        /// <summary>
        /// This thread is responsible for doing the folder search.
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

                    // Again, this is very easy. Just find the directories within the screenshots folder.
                    string[] dirs = Directory.GetDirectories(textBoxScreenshotsFolderSearch.Text);

                    // Go through each directory found and make sure that the sub-directories match
                    // with the format yyyy-mm-dd.
                    for (int i = 0; i < dirs.Length; i++)
                    {
                        Regex rgx = new Regex(@"^\d{4}-\d{2}-\d{2}$");

                        if (rgx.IsMatch(Path.GetFileName(dirs[i])))
                        {
                            if (Directory.Exists(dirs[i] + "\\1\\"))
                            {
                                if (!selectedFolders.Contains(Path.GetFileName(dirs[i]).ToString()))
                                {
                                    selectedFolders.Add(Path.GetFileName(dirs[i]).ToString());
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
            if (toolStripMenuItemOpen.Enabled)
            {
                this.Opacity = 100;
                toolStripMenuItemOpen.Enabled = false;
                toolStripMenuItemClose.Enabled = true;

                this.Show();
                this.Visible = true;
                this.ShowInTaskbar = true;
            }
        }

        /// <summary>
        /// Hides this window.
        /// </summary>
        private void CloseWindow()
        {
            if (!toolStripMenuItemOpen.Enabled)
            {
                // Pause the slideshow if you find it still playing.
                if (Slideshow.Playing)
                {
                    PauseSlideshow();
                }

                this.Opacity = 0;
                toolStripMenuItemOpen.Enabled = true;
                toolStripMenuItemClose.Enabled = false;

                this.Hide();
                this.Visible = false;
                this.ShowInTaskbar = false;
            }
        }

        /// <summary>
        /// Stops the screen capture session that's currently running.
        /// </summary>
        private void StopScreenCapture()
        {
            ScreenCapture.Stop();
            keylogger.Enabled = false;

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

        /// <summary>
        /// Plays the slideshow.
        /// </summary>
        private void PlaySlideshow()
        {
            checkBoxDemoMode.Checked = false;
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
        private void StartScreenCapture(string folder, string format, int delay, int limit, int ratio, bool initial)
        {
            checkBoxDemoMode.Checked = false;

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
            ScreenCapture.Format = format;
            ScreenCapture.Delay = delay;
            ScreenCapture.Limit = limit;
            ScreenCapture.Ratio = ratio;

            keylogger.Enabled = checkBoxEnableKeylogging.Checked;

            if (initial)
            {
                ScreenCapture.TakeScreenshot();
                DisplayCaptureStatus(StatusMessage.LAST_CAPTURE_APP, StatusMessage.LAST_CAPTURE_ICON, true);
            }

            // Start taking screenshots.
            ScreenCapture.Start();
        }

        /// <summary>
        /// Whenever the user clicks on a screenshot in the list of screenshots then make sure to update the preview of screenshots.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxScreenshots_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBoxDemoMode.Checked = false;

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
            buttonSearch.Enabled = false;
            monthCalendar.Enabled = false;
            buttonOpenFolder.Enabled = false;
            buttonBrowseFolder.Enabled = false;
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
        }

        /// <summary>
        /// Enables the appropriate controls when the slideshow is paused or stopped.
        /// </summary>
        private void EnableControls()
        {
            buttonSearch.Enabled = true;
            monthCalendar.Enabled = true;
            buttonOpenFolder.Enabled = true;
            buttonBrowseFolder.Enabled = true;
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

            if (!ScreenCapture.Capturing)
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
            if (!checkBoxDemoMode.Checked)
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new UpdateScreenshotPreviewDelegate(this.UpdatePreview));
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

                    if (Slideshow.Index == (Slideshow.Count - 1))
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
                imageFormat = ImageFormatSpec.NAME_PNG;
            }
            else
            {
                imageFormat = sender.ToString();
            }

            if (!string.IsNullOrEmpty(imageFormat))
            {
                bool initial = false;
                string folder = textBoxScreenshotsFolderSearch.Text;

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

                        StartScreenCapture(folder, imageFormat, delay, limit, ratio, initial);
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
        /// Displays the screen capture status in the application's status strip and system tray icon while a screen capture session is running.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Screen_Capturing(object sender, EventArgs e)
        {
            DisplayCaptureStatus(StatusMessage.LAST_CAPTURE_APP, StatusMessage.LAST_CAPTURE_ICON, true);

            if (!ScreenCapture.Capturing)
            {
                StopScreenCapture();
            }
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
            textBoxKeyloggingFile.Enabled = true;
            checkBoxEnableKeylogging.Enabled = true;

            if (GetCaptureDelay() > 0)
            {
                toolStripMenuItemStartScreenCapture.Enabled = true;
                toolStripSplitButtonStartScreenCapture.Enabled = true;
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
            textBoxKeyloggingFile.Enabled = false;
            checkBoxEnableKeylogging.Enabled = false;

            toolStripMenuItemStartScreenCapture.Enabled = false;
            toolStripSplitButtonStartScreenCapture.Enabled = false;
        }

        /// <summary>
        /// Searches for the screenshots folders when the "Search" button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSearchScreenshotsDirectory_Click(object sender, EventArgs e)
        {
            SearchFolders();
        }

        /// <summary>
        /// Saves the user's current settings so we can load them at a later time when the user executes the application.
        /// </summary>
        private void SaveApplicationSettings()
        {
            Properties.Settings.Default.ScreenshotsDirectory = CorrectDirectoryPath(textBoxScreenshotsFolderSearch.Text);

            Properties.Settings.Default.ScheduleImageFormat = comboBoxScheduleImageFormat.Text;

            Properties.Settings.Default.SlideSkip = (int)numericUpDownSlideSkip.Value;
            Properties.Settings.Default.CaptureLimit = (int)numericUpDownCaptureLimit.Value;
            Properties.Settings.Default.ImageResolutionRatio = (int)numericUpDownImageResolutionRatio.Value;

            Properties.Settings.Default.KeyloggingFile = textBoxKeyloggingFile.Text;
            Properties.Settings.Default.ImageFormatIndex = toolStripComboBoxImageFormatFilter.SelectedIndex;

            Properties.Settings.Default.Interval = GetCaptureDelay();
            Properties.Settings.Default.SlideshowDelay = GetSlideshowDelay();

            Properties.Settings.Default.SlideSkipCheck = checkBoxSlideSkip.Checked;
            Properties.Settings.Default.CaptureLimitCheck = checkBoxCaptureLimit.Checked;
            Properties.Settings.Default.TakeInitialScreenshotCheck = checkBoxInitialScreenshot.Checked;

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

            Properties.Settings.Default.KeyloggingEnabledCheck = checkBoxEnableKeylogging.Checked;
            Properties.Settings.Default.CaptureOnTheseDaysCheck = checkBoxScheduleOnTheseDays.Checked;

            Properties.Settings.Default.CaptureStopAtValue = dateTimePickerScheduleStopAt.Value;
            Properties.Settings.Default.CaptureStartAtValue = dateTimePickerScheduleStartAt.Value;

            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Displays the status of "Demo Mode".
        /// </summary>
        /// <param name="status">Can be "On" or "Off".</param>
        private void DisplayDemoModeStatus(string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                statusStrip.Items["statusStripLabelDemo"].Text = "Demo: " + status;
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
                statusStrip.Items["statusStripLabelLastCapture"].Text = "Last capture: " + StringHelper.ParseTags(statusApp, imageFormat);
            }

            if (!string.IsNullOrEmpty(statusApp) && !string.IsNullOrEmpty(statusIcon))
            {
                notifyIcon.Text = appName + "\n" + StringHelper.ParseTags(statusIcon, imageFormat);
                statusStrip.Items["statusStripLabelLastCapture"].Text = "Last capture: " + StringHelper.ParseTags(statusApp, imageFormat);
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

            if (ScreenCapture.Capturing)
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
                string selectedFile = FileSystem.GetImageFilePath(Slideshow.SelectedSlide, Slideshow.SelectedScreen);

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
            bool isScheduled = false;

            checkBoxEnableKeylogging.Checked = false;

            bool initial = false;
            checkBoxInitialScreenshot.Checked = false;

            int limit = CAPTURE_LIMIT_MIN;
            checkBoxCaptureLimit.Checked = false;
            numericUpDownCaptureLimit.Value = (decimal)CAPTURE_LIMIT_MIN;

            int delay = CAPTURE_DELAY_DEFAULT;
            numericUpDownHoursInterval.Value = 0;
            numericUpDownMinutesInterval.Value = 0;
            numericUpDownSecondsInterval.Value = CAPTURE_DELAY_DEFAULT / CAPTURE_DELAY_DEFAULT;
            numericUpDownMillisecondsInterval.Value = 0;

            int ratio = IMAGE_RESOLUTION_RATIO_MAX;
            numericUpDownImageResolutionRatio.Value = (decimal)IMAGE_RESOLUTION_RATIO_MAX;

            string folder = screenshotsFolder;

            imageFormat = ImageFormatSpec.NAME_PNG;
            comboBoxScheduleImageFormat.SelectedItem = ImageFormatSpec.NAME_PNG;

            Regex rgxCommandLineRatio = new Regex(REGEX_COMMAND_LINE_RATIO);
            Regex rgxCommandLineLimit = new Regex(REGEX_COMMAND_LINE_LIMIT);
            Regex rgxCommandLineKeylog = new Regex(REGEX_COMMAND_LINE_KEYLOG);
            Regex rgxCommandLineFormat = new Regex(REGEX_COMMAND_LINE_FORMAT);
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
                if (rgxCommandLineFolder.IsMatch(args[i]))
                {
                    folder = CorrectDirectoryPath(rgxCommandLineFolder.Match(args[i]).Groups["Folder"].Value.ToString());
                    textBoxScreenshotsFolderSearch.Text = CorrectDirectoryPath(rgxCommandLineFolder.Match(args[i]).Groups["Folder"].Value.ToString());
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

                if (rgxCommandLineKeylog.IsMatch(args[i]))
                {
                    textBoxKeyloggingFile.Text = rgxCommandLineKeylog.Match(args[i]).Groups["Keylog"].Value;
                    checkBoxEnableKeylogging.Checked = true;
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
            }

            if (isScheduled)
            {
                toolStripMenuItemScheduleAtApplicationStartup.Checked = true;
            }
            else
            {
                StartScreenCapture(folder, imageFormat, delay, limit, ratio, initial);
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
                string selectedFile = FileSystem.GetImageFilePath(Slideshow.SelectedSlide, Slideshow.SelectedScreen);

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
            Slideshow.SelectedScreen = tabControlScreens.SelectedIndex;
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
        /// Takes the screenshots in "Demo Mode".
        /// </summary>
        private void TakeDemoScreenshots()
        {
            DisplayImages(true);
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
        /// Displays the preview images.
        /// </summary>
        /// <param name="demo"></param>
        private void DisplayImages(bool demo)
        {
            ArrayList images = new ArrayList();

            if (demo)
            {
                int count = 0;

                foreach (Screen screen in Screen.AllScreens)
                {
                    count++;

                    if (count <= ScreenCapture.SCREEN_MAX)
                    {
                        images.Add(ScreenCapture.GetScreenBitmap(screen, (int)numericUpDownImageResolutionRatio.Value));
                    }
                }
            }
            else
            {
                images = FileSystem.GetImages(Slideshow.SelectedSlide);
            }

            if (images.Count >= 1)
            {
                pictureBoxScreenshotPreviewMonitor1.Image = (Image)images[0];
                pictureBoxScreenshotPreviewMonitor2.Image = null;
                pictureBoxScreenshotPreviewMonitor3.Image = null;
                pictureBoxScreenshotPreviewMonitor4.Image = null;
            }

            if (images.Count >= 2)
            {
                pictureBoxScreenshotPreviewMonitor2.Image = (Image)images[1];
                pictureBoxScreenshotPreviewMonitor3.Image = null;
                pictureBoxScreenshotPreviewMonitor4.Image = null;
            }

            if (images.Count >= 3)
            {
                pictureBoxScreenshotPreviewMonitor3.Image = (Image)images[2];
                pictureBoxScreenshotPreviewMonitor4.Image = null;
            }

            if (images.Count >= 4)
            {
                pictureBoxScreenshotPreviewMonitor4.Image = (Image)images[3];
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

            System.GC.Collect();
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

                        int delay = GetCaptureDelay();
                        int limit = (int)numericUpDownCaptureLimit.Value;
                        int ratio = (int)numericUpDownImageResolutionRatio.Value;

                        if (!checkBoxCaptureLimit.Checked) { limit = 0; }

                        if (checkBoxInitialScreenshot.Checked)
                        {
                            initial = true;
                        }

                        StartScreenCapture(folder, imageFormat, delay, limit, ratio, initial);

                        timerScheduledCaptureStart.Enabled = false;
                    }
                }
                else
                {
                    bool initial = false;
                    string folder = textBoxScreenshotsFolderSearch.Text;

                    int delay = GetCaptureDelay();
                    int limit = (int)numericUpDownCaptureLimit.Value;
                    int ratio = (int)numericUpDownImageResolutionRatio.Value;

                    if (!checkBoxCaptureLimit.Checked) { limit = 0; }

                    if (checkBoxInitialScreenshot.Checked)
                    {
                        initial = true;
                    }

                    StartScreenCapture(folder, imageFormat, delay, limit, ratio, initial);

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

                            int delay = GetCaptureDelay();
                            int limit = (int)numericUpDownCaptureLimit.Value;
                            int ratio = (int)numericUpDownImageResolutionRatio.Value;

                            if (!checkBoxCaptureLimit.Checked) { limit = 0; }

                            if (checkBoxInitialScreenshot.Checked)
                            {
                                initial = true;
                            }

                            StartScreenCapture(folder, imageFormat, delay, limit, ratio, initial);
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

                        int delay = GetCaptureDelay();
                        int limit = (int)numericUpDownCaptureLimit.Value;
                        int ratio = (int)numericUpDownImageResolutionRatio.Value;

                        if (!checkBoxCaptureLimit.Checked) { limit = 0; }

                        if (checkBoxInitialScreenshot.Checked)
                        {
                            initial = true;
                        }

                        StartScreenCapture(folder, imageFormat, delay, limit, ratio, initial);
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
            checkBoxEnableKeylogging.Visible = false;

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

                case "Keylogger":
                    checkBoxEnableKeylogging.Visible = true;
                    checkBoxEnableKeylogging.BringToFront();
                    break;
            }
        }

        /// <summary>
        /// Starts taking screenshots when "Demo Mode" is on otherwise the preview images will be cleared.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxDemoMode_CheckedChanged(object sender, EventArgs e)
        {
            if (GetCaptureDelay() > 0 && checkBoxDemoMode.Checked)
            {
                DisplayDemoModeStatus(StatusMessage.ON);

                if (checkBoxInitialScreenshot.Checked)
                {
                    TakeDemoScreenshots();
                }
            }
            else
            {
                DisplayDemoModeStatus(StatusMessage.OFF);

                ClearImages();
                UpdatePreview();
            }

            timerDemoCapture.Enabled = checkBoxDemoMode.Checked;
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

                if (checkBoxDemoMode.Checked)
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
        /// Makes sure that the file name of the keylogger's file is always updated when the user changes its name.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxKeyloggingFile_TextChanged(object sender, EventArgs e)
        {
            Regex rgxCommandLineKeylog = new Regex(REGEX_COMMAND_LINE_KEYLOG);

            if (rgxCommandLineKeylog.IsMatch("-keylog=" + textBoxKeyloggingFile.Text))
            {
                textBoxKeyloggingFile.BackColor = Color.White;
                keylogger.File = textBoxScreenshotsFolderSearch.Text + textBoxKeyloggingFile.Text;
            }
            else
            {
                textBoxKeyloggingFile.BackColor = Color.Red;
            }
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
    }
}