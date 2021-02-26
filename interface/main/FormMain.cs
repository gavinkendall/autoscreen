//-----------------------------------------------------------------------
// <copyright file="FormMain.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2021 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>The main interface form for setting up sub-forms, showing the interface, hiding the interface, displaying dates in the calendar, and searching for screenshots.</summary>
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
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using Gavin.Kendall.SFTP;

namespace AutoScreenCapture
{
    /// <summary>
    /// The application's main window.
    /// </summary>
    public partial class FormMain : Form
    {
        // The "About Auto Screen Capture" form.
        private FormAbout _formAbout;

        // The "Auto Screen Capture - Help" form.
        private FormHelp _formHelp;

        // The "Email Settings" form.
        private FormEmailSettings _formEmailSettings;

        // The "File Transfer Settings" form.
        private FormFileTransferSettings _formFileTransferSettings;

        // SFTP client.
        private SftpClient _sftpClient = null;

        // The various forms that are used for modules.
        private FormMacroTag _formMacroTag;
        private FormRegion _formRegion;
        private FormScreen _formScreen;
        private FormEditor _formEditor;
        private FormTrigger _formTrigger;
        private FormSchedule _formSchedule;

        // Screeshot Properties
        private FormScreenshotMetadata _formScreenshotMetadata = new FormScreenshotMetadata();

        // The form to display when challenging the user for the passphrase in order to unlock the running screen capture session.
        private FormEnterPassphrase _formEnterPassphrase;

        // A small window is shown when the user selects "Show Screen Capture Status" from the system tray icon menu.
        private FormScreenCaptureStatus _formScreenCaptureStatus;

        // The Dynamic Regex Validator tool.
        private FormDynamicRegexValidator _formDynamicRegexValidator = new FormDynamicRegexValidator();

        // Keyboard Shortcuts
        private HotKeyMap _hotKeyMap;
        private FormKeyboardShortcuts _formKeyboardShortcuts;
        private string _keyboardShortcutStartScreenCaptureKeyUserSetting;
        private string _keyboardShortcutStopScreenCaptureKeyUserSetting;
        private string _keyboardShortcutCaptureNowArchiveKeyUserSetting;
        private string _keyboardShortcutCaptureNowEditKeyUserSetting;
        private string _keyboardShortcutRegionSelectClipboardKeyUserSetting;
        private string _keyboardShortcutRegionSelectAutoSaveKeyUserSetting;
        private string _keyboardShortcutRegionSelectEditKeyUserSetting;

        private Log _log;
        private Config _config;
        private FileSystem _fileSystem;
        private Security _security;
        private Slideshow _slideShow;
        private DataConvert _dataConvert;
        private ScreenCapture _screenCapture;
        private MacroParser _macroParser;
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

        /// <summary>
        /// Constructor for the main form.
        /// </summary>
        public FormMain(Config config)
        {
            _config = config;
            _fileSystem = config.FileSystem;
            _log = config.Log;

            if (Environment.OSVersion.Version.Major >= 6)
            {
                AutoScaleMode = AutoScaleMode.Dpi;
                Font = new Font(Font.Name, 8.25f * 96f / CreateGraphics().DpiX, Font.Style, Font.Unit, Font.GdiCharSet, Font.GdiVerticalFont);
            }

            InitializeComponent();

            _security = new Security();
            _slideShow = new Slideshow();
            _dataConvert = new DataConvert();

            RegisterKeyboardShortcuts();
            _hotKeyMap.KeyPressed += new EventHandler<KeyPressedEventArgs>(hotKey_KeyPressed);

            LoadSettings();

            Text = (string)_config.Settings.Application.GetByKey("Name", _config.Settings.ApplicationName).Value;

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
            string name = _config.Settings.Application.GetByKey("Name", _config.Settings.ApplicationName).Value.ToString();
            string version = _config.Settings.Application.GetByKey("Version", _config.Settings.ApplicationVersion).Value.ToString();

            bool firstRun = Convert.ToBoolean(_config.Settings.User.GetByKey("FirstRun", _config.Settings.DefaultSettings.FirstRun).Value);

            string welcome = "Welcome to " + name + " (" + version + ")";

            if (firstRun)
            {
                welcome += " - Please click the Help button to learn how to use the application";
            }

            HelpMessage(welcome);

            // Start the scheduled capture timer.
            timerScheduledCapture.Interval = 1000;
            timerScheduledCapture.Enabled = true;
            timerScheduledCapture.Start();

            LoadHelpTips();

            ShowInfo();

            SearchFilterValues();
            SearchDates();
            SearchScreenshots();

            _log.WriteDebugMessage("Running triggers of condition type ApplicationStartup");
            RunTriggersOfConditionType(TriggerConditionType.ApplicationStartup);
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

                HideSystemTrayIcon();

                _log.WriteDebugMessage("Hiding interface on forced application exit because Windows is shutting down");
                HideInterface();

                _log.WriteDebugMessage("Saving screenshots on forced application exit because Windows is shutting down");
                _screenshotCollection.SaveToXmlFile(_config);

                if (runDateSearchThread != null && runDateSearchThread.IsBusy)
                {
                    runDateSearchThread.CancelAsync();
                }

                if (runScreenshotSearchThread != null && runScreenshotSearchThread.IsBusy)
                {
                    runScreenshotSearchThread.CancelAsync();
                }

                _log.WriteMessage("Bye!");

                // Exit.
                Environment.Exit(0);
            }
            else
            {
                _log.WriteDebugMessage("Running triggers of condition type InterfaceClosing");
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
            _log.WriteDebugMessage("Searching for dates");

            if (runDateSearchThread == null)
            {
                runDateSearchThread = new BackgroundWorker
                {
                    WorkerReportsProgress = false,
                    WorkerSupportsCancellation = true
                };

                runDateSearchThread.DoWork += new DoWorkEventHandler(DoWork_runDateSearchThread);
            }

            if (!runDateSearchThread.IsBusy)
            {
                runDateSearchThread.RunWorkerAsync();
            }

        }

        private void DeleteSlides()
        {
            _log.WriteDebugMessage("Deleting slides directory from old version of application (if needed)");

            if (runDeleteSlidesThread == null)
            {
                runDeleteSlidesThread = new BackgroundWorker
                {
                    WorkerReportsProgress = false,
                    WorkerSupportsCancellation = true
                };

                runDeleteSlidesThread.DoWork += new DoWorkEventHandler(DoWork_runDeleteSlidesThread);
            }

            if (!runDeleteSlidesThread.IsBusy)
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
                monthCalendar.Invoke(new RunDateSearchDelegate(RunDateSearch), new object[] { e });
            }
            else
            {
                if (_screenshotCollection != null)
                {
                    List<string> dates = new List<string>();
                    dates = _screenshotCollection.GetDatesByFilter(comboBoxFilterType.Text, comboBoxFilterValue.Text);

                    DateTime[] boldedDates = new DateTime[dates.Count];

                    for (int i = 0; i < dates.Count; i++)
                    {
                        boldedDates.SetValue(ConvertDateStringToDateTime(dates[i].ToString()), i);
                    }

                    monthCalendar.BoldedDates = boldedDates;
                }
            }
        }

        /// <summary>
        /// This thread is responsible for deleting all the slides remaining from an old version of the application
        /// since we no longer use slides or support the Slideshow module going forward.
        /// </summary>
        /// <param name="e"></param>
        private void RunDeleteSlides(DoWorkEventArgs e)
        {
            _fileSystem.DeleteFilesInDirectory(_fileSystem.SlidesFolder);
        }

        /// <summary>
        /// Shows the interface.
        /// </summary>
        private void ShowInterface()
        {
            try
            {
                if (ShowInTaskbar)
                {
                    return;
                }

                _log.WriteDebugMessage("Showing interface");

                if (_screenCapture.LockScreenCaptureSession && !_formEnterPassphrase.Visible)
                {
                    _log.WriteDebugMessage("Screen capture session is locked. Challenging user to enter correct passphrase to unlock");
                    _formEnterPassphrase.ShowDialog(this);
                }

                // This is intentional. Do not rewrite these statements as an if/else
                // because as soon as lockScreenCaptureSession is set to false we want
                // to continue with normal functionality.
                if (!_screenCapture.LockScreenCaptureSession)
                {
                    _config.Settings.User.GetByKey("Passphrase", _config.Settings.DefaultSettings.Passphrase).Value = string.Empty;
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

                    _log.WriteDebugMessage("Running triggers of condition type InterfaceShowing");
                    RunTriggersOfConditionType(TriggerConditionType.InterfaceShowing);
                }
            }
            catch (Exception ex)
            {
                _screenCapture.ApplicationError = true;
                _log.WriteExceptionMessage("FormMain::ShowInterface", ex);
            }
        }

        /// <summary>
        /// Hides the interface.
        /// </summary>
        private void HideInterface()
        {
            try
            {
                _log.WriteDebugMessage("Hiding interface");

                Opacity = 0;

                Hide();
                Visible = false;
                ShowInTaskbar = false;

                _log.WriteDebugMessage("Running triggers of condition type InterfaceHiding");
                RunTriggersOfConditionType(TriggerConditionType.InterfaceHiding);
            }
            catch (Exception ex)
            {
                _screenCapture.ApplicationError = true;
                _log.WriteExceptionMessage("FormMain::HideInterface", ex);
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
        /// Shows or hides the interface depending on if the interface is already visible or not.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemShowHideInterface_Click(object sender, EventArgs e)
        {
            if (Visible)
            {
                HideInterface();
            }
            else
            {
                ShowInterface();
            }
        }

        /// <summary>
        /// Hides the interface.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemHideInterface_Click(object sender, EventArgs e)
        {
            HideInterface();
        }

        /// <summary>
        /// Shows a small screen capture status window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemScreenCaptureStatus_Click(object sender, EventArgs e)
        {
            if (!_formScreenCaptureStatus.Visible)
            {
                _formScreenCaptureStatus.Show();
            }
            else
            {
                _formScreenCaptureStatus.Focus();
                _formScreenCaptureStatus.BringToFront();
            }
        }

        /// <summary>
        /// Shows the Dynamic Regex Validator.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemDynamicRegexValidator_Click(object sender, EventArgs e)
        {
            if (!_formDynamicRegexValidator.Visible)
            {
                _formDynamicRegexValidator.Show();
            }
            else
            {
                _formDynamicRegexValidator.Focus();
                _formDynamicRegexValidator.BringToFront();
            }
        }

        /// <summary>
        /// Shows the "About" window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemAbout_Click(object sender, EventArgs e)
        {
            if (!_formAbout.Visible)
            {
                _formAbout.Show();
            }
            else
            {
                _formAbout.Focus();
                _formAbout.BringToFront();
            }
        }

        /// <summary>
        /// Shows the "Email Settings" window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemEmailSettings_Click(object sender, EventArgs e)
        {
            ShowInterface();

            if (!_formEmailSettings.Visible)
            {
                _formEmailSettings.ShowDialog(this);
            }
            else
            {
                _formEmailSettings.Focus();
                _formEmailSettings.BringToFront();
            }

            if (_formEmailSettings.DialogResult == DialogResult.OK)
            {
                BuildViewTabPages();
            }
        }

        /// <summary>
        /// Shows the "File Transfer Settings" window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemFileTransferSettings_Click(object sender, EventArgs e)
        {
            ShowInterface();

            if (!_formFileTransferSettings.Visible)
            {
                _formFileTransferSettings.ShowDialog(this);
            }
            else
            {
                _formFileTransferSettings.Focus();
                _formFileTransferSettings.BringToFront();
            }

            if (_formFileTransferSettings.DialogResult == DialogResult.OK)
            {
                BuildViewTabPages();
            }
        }

        private void checkBoxActiveWindowTitle_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxActiveWindowTitle.Checked)
            {
                textBoxActiveWindowTitle.Enabled = true;
                radioButtonCaseSensitiveMatch.Enabled = true;
                radioButtonCaseInsensitiveMatch.Enabled = true;
                radioButtonRegularExpressionMatch.Enabled = true;
            }
            else
            {
                textBoxActiveWindowTitle.Enabled = false;
                radioButtonCaseSensitiveMatch.Enabled = false;
                radioButtonCaseInsensitiveMatch.Enabled = false;
                radioButtonRegularExpressionMatch.Enabled = false;
            }
        }

        private void buttonApplicationFocusTest_Click(object sender, EventArgs e)
        {
            SaveSettings();

            DoApplicationFocus();
        }

        private void buttonApplicationFocusRefresh_Click(object sender, EventArgs e)
        {
            SaveSettings();

            RefreshApplicationFocusList();
        }

        private void DoApplicationFocus()
        {
            int delayBefore = (int)numericUpDownApplicationFocusDelayBefore.Value;
            int delayAfter = (int)numericUpDownApplicationFocusDelayAfter.Value;

            if (delayBefore > 0)
            {
                System.Threading.Thread.Sleep(delayBefore);
            }

            _screenCapture.SetApplicationFocus(comboBoxProcessList.Text);

            if (delayAfter > 0)
            {
                System.Threading.Thread.Sleep(delayAfter);
            }
        }

        private void SetApplicationFocus(string applicationFocus)
        {
            if (string.IsNullOrEmpty(applicationFocus))
            {
                _config.Settings.User.SetValueByKey("ApplicationFocus", string.Empty);
            }
            else
            {
                applicationFocus = applicationFocus.Trim();

                _config.Settings.User.SetValueByKey("ApplicationFocus", applicationFocus);

                if (!_config.Settings.User.Save(_config.Settings, _fileSystem))
                {
                    _screenCapture.ApplicationError = true;
                }
            }

            RefreshApplicationFocusList();

            DoApplicationFocus();
        }
    }
}