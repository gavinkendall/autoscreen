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
        private FormTag formTag = new FormTag();
        private FormEnterPassphrase formEnterPassphrase = new FormEnterPassphrase();

        private ScreenCapture _screenCapture;
        private ImageFormatCollection _imageFormatCollection;
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
        /// Exits the application from the system tray icon menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_toolStripMenuItemExit(object sender, EventArgs e)
        {
            ExitApplication();
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

        /// <summary>
        /// Figures out if the "Start Capture" controls should be enabled or disabled.
        /// </summary>
        private void EnableStartCapture()
        {
            if (GetScreenCaptureInterval() > 0)
            {
                toolStripSplitButtonStartScreenCapture.Enabled = true;
                toolStripMenuItemStartScreenCapture.Enabled = true;

                groupBoxCaptureDelay.Enabled = true;
                numericUpDownHoursInterval.Enabled = true;
                checkBoxInitialScreenshot.Enabled = true;
                numericUpDownMinutesInterval.Enabled = true;
                checkBoxCaptureLimit.Enabled = true;
                numericUpDownCaptureLimit.Enabled = true;
                numericUpDownSecondsInterval.Enabled = true;
                numericUpDownMillisecondsInterval.Enabled = true;

                labelKeepScreenshots.Enabled = true;
                labelDays.Enabled = true;
                numericUpDownKeepScreenshotsForDays.Enabled = true;

                groupBoxSchedule.Enabled = true;
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

            groupBoxCaptureDelay.Enabled = false;
            numericUpDownHoursInterval.Enabled = false;
            checkBoxInitialScreenshot.Enabled = false;
            numericUpDownMinutesInterval.Enabled = false;
            checkBoxCaptureLimit.Enabled = false;
            numericUpDownCaptureLimit.Enabled = false;
            numericUpDownSecondsInterval.Enabled = false;
            numericUpDownMillisecondsInterval.Enabled = false;

            labelKeepScreenshots.Enabled = false;
            labelDays.Enabled = false;
            numericUpDownKeepScreenshotsForDays.Enabled = false;

            groupBoxSchedule.Enabled = false;
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

        #region Tag

        /// <summary>
        /// Shows the "Add Tag" window to enable the user to add a chosen Tag.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_addTag(object sender, EventArgs e)
        {
            formTag.TagObject = null;

            formTag.ShowDialog(this);

            if (formTag.DialogResult == DialogResult.OK)
            {
                BuildTagsModule();

                formTag.TagCollection.Save();
            }
        }

        /// <summary>
        /// Removes the selected Tags from the Tags tab page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_removeSelectedTags(object sender, EventArgs e)
        {
            int countBeforeRemoval = formTag.TagCollection.Count;

            foreach (Control control in tabPageTags.Controls)
            {
                if (control.GetType().Equals(typeof(CheckBox)))
                {
                    CheckBox checkBox = (CheckBox)control;

                    if (checkBox.Checked)
                    {
                        Tag trigger = formTag.TagCollection.Get((Tag)checkBox.Tag);
                        formTag.TagCollection.Remove(trigger);
                    }
                }
            }

            if (countBeforeRemoval > formTag.TagCollection.Count)
            {
                BuildTagsModule();

                formTag.TagCollection.Save();
            }
        }

        /// <summary>
        /// Shows the "Change Tag" window to enable the user to edit a chosen Tag.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_buttonChangeTag(object sender, EventArgs e)
        {
            Button buttonSelected = (Button)sender;

            if (buttonSelected.Tag != null)
            {
                formTag.TagObject = (Tag)buttonSelected.Tag;

                formTag.ShowDialog(this);

                if (formTag.DialogResult == DialogResult.OK)
                {
                    BuildTagsModule();

                    formTag.TagCollection.Save();
                }
            }
        }

        #endregion Tag

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
            // This timer needs to loop over the Schedules to determine start and stop times.

            //if (checkBoxScheduleStartAt.Checked)
            //{
            //    if (checkBoxScheduleOnTheseDays.Checked)
            //    {
            //        if (((DateTime.Now.DayOfWeek == DayOfWeek.Saturday && checkBoxSaturday.Checked) ||
            //             (DateTime.Now.DayOfWeek == DayOfWeek.Sunday && checkBoxSunday.Checked) ||
            //             (DateTime.Now.DayOfWeek == DayOfWeek.Monday && checkBoxMonday.Checked) ||
            //             (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday && checkBoxTuesday.Checked) ||
            //             (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday && checkBoxWednesday.Checked) ||
            //             (DateTime.Now.DayOfWeek == DayOfWeek.Thursday && checkBoxThursday.Checked) ||
            //             (DateTime.Now.DayOfWeek == DayOfWeek.Friday && checkBoxFriday.Checked)) &&
            //            ((DateTime.Now.Hour == dateTimePickerScheduleStartAt.Value.Hour) &&
            //             (DateTime.Now.Minute == dateTimePickerScheduleStartAt.Value.Minute) &&
            //             (DateTime.Now.Second == dateTimePickerScheduleStartAt.Value.Second)))
            //        {
            //            StartScreenCapture();
            //        }
            //    }
            //    else
            //    {
            //        if ((DateTime.Now.Hour == dateTimePickerScheduleStartAt.Value.Hour) &&
            //            (DateTime.Now.Minute == dateTimePickerScheduleStartAt.Value.Minute) &&
            //            (DateTime.Now.Second == dateTimePickerScheduleStartAt.Value.Second))
            //        {
            //            StartScreenCapture();
            //        }
            //    }
            //}
        }

        /// <summary>
        /// The timer used for stopping scheduled screen capture sessions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tick_timerScheduledCaptureStop(object sender, EventArgs e)
        {
            //if (checkBoxScheduleStopAt.Checked)
            //{
            //    if (checkBoxScheduleOnTheseDays.Checked)
            //    {
            //        if (((DateTime.Now.DayOfWeek == DayOfWeek.Saturday && checkBoxSaturday.Checked) ||
            //             (DateTime.Now.DayOfWeek == DayOfWeek.Sunday && checkBoxSunday.Checked) ||
            //             (DateTime.Now.DayOfWeek == DayOfWeek.Monday && checkBoxMonday.Checked) ||
            //             (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday && checkBoxTuesday.Checked) ||
            //             (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday && checkBoxWednesday.Checked) ||
            //             (DateTime.Now.DayOfWeek == DayOfWeek.Thursday && checkBoxThursday.Checked) ||
            //             (DateTime.Now.DayOfWeek == DayOfWeek.Friday && checkBoxFriday.Checked)) &&
            //            ((DateTime.Now.Hour == dateTimePickerScheduleStopAt.Value.Hour) &&
            //             (DateTime.Now.Minute == dateTimePickerScheduleStopAt.Value.Minute) &&
            //             (DateTime.Now.Second == dateTimePickerScheduleStopAt.Value.Second)))
            //        {
            //            StopScreenCapture();
            //        }
            //    }
            //    else
            //    {
            //        if ((DateTime.Now.Hour == dateTimePickerScheduleStopAt.Value.Hour) &&
            //            (DateTime.Now.Minute == dateTimePickerScheduleStopAt.Value.Minute) &&
            //            (DateTime.Now.Second == dateTimePickerScheduleStopAt.Value.Second))
            //        {
            //            StopScreenCapture();
            //        }
            //    }
            //}
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
    }
}