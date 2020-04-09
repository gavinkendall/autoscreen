//-----------------------------------------------------------------------
// <copyright file="FormMain.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using AutoScreenCapture.Properties;

namespace AutoScreenCapture
{
    /// <summary>
    /// The application's main window.
    /// </summary>
    public partial class FormMain : Form
    {
        // The various forms that are used for modules.
        private FormEditor formEditor = new FormEditor();
        private FormTrigger formTrigger = new FormTrigger();
        private FormRegion formRegion = new FormRegion();
        private FormScreen formScreen = new FormScreen();
        private FormTag formTag = new FormTag();
        private FormEnterPassphrase formEnterPassphrase = new FormEnterPassphrase();
        private FormSchedule formSchedule = new FormSchedule();
        private HotKeyMap hotKeyMap = new HotKeyMap();

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
        /// Constructor for the main form. Arguments from the command line can be passed to it.
        /// </summary>
        /// <param name="args">Arguments from the command line</param>
        public FormMain(string[] args)
        {
            InitializeComponent();

            hotKeyMap.KeyPressed +=  new EventHandler<KeyPressedEventArgs>(hotKey_KeyPressed);
            hotKeyMap.RegisterHotKey(AutoScreenCapture.ModifierKeys.Control | AutoScreenCapture.ModifierKeys.Alt, Keys.Z);
            hotKeyMap.RegisterHotKey(AutoScreenCapture.ModifierKeys.Control | AutoScreenCapture.ModifierKeys.Alt, Keys.X);
            hotKeyMap.RegisterHotKey(AutoScreenCapture.ModifierKeys.Control | AutoScreenCapture.ModifierKeys.Alt, Keys.A);
            hotKeyMap.RegisterHotKey(AutoScreenCapture.ModifierKeys.Control | AutoScreenCapture.ModifierKeys.Alt, Keys.E);

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
            HelpMessage("Welcome to " +
                Settings.Application.GetByKey("Name", defaultValue: Settings.ApplicationName).Value + " " +
                Settings.Application.GetByKey("Version", defaultValue: Settings.ApplicationVersion).Value);

            LoadHelpTips();

            ShowInfo();

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

                SystemTrayIconStatusNormal();
                HideSystemTrayIcon();

                Log.Write("Hiding interface on forced application exit because Windows is shutting down");
                HideInterface();

                Log.Write("Saving screenshots on forced application exit because Windows is shutting down");
                _screenshotCollection.SaveToXmlFile((int)numericUpDownKeepScreenshotsForDays.Value);

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
                List<string> dates = _screenshotCollection.GetDatesByFilter(comboBoxFilterType.Text, comboBoxFilterValue.Text);

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
    }
}