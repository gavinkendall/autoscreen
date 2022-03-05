//-----------------------------------------------------------------------
// <copyright file="FormMain.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
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
using System.Diagnostics;
using Gavin.Kendall.SFTP;

namespace AutoScreenCapture
{
    /// <summary>
    /// The application's main window.
    /// </summary>
    public partial class FormMain : Form
    {
        // Determines if the application has started so it can run any trigger
        // with the ApplicationStartup condition. This is so we don't accidentally
        // run these types of triggers before command line options get a chance to be parsed.
        private bool _appStarted = false;

        // Preview
        bool _preview = false;

        // Determines if we should show or hide the application's interface.
        private bool _initialVisibilitySet = false;

        // The "About Auto Screen Capture" form.
        private FormAbout _formAbout;

        // The "Auto Screen Capture - Help" form.
        private FormHelp _formHelp;

        // The "Email Settings" form.
        private FormEmailSettings _formEmailSettings;

        // The "File Transfer Settings" form.
        private FormFileTransferSettings _formFileTransferSettings;

        // The "Region Select Options" form.
        private FormRegionSelectOptions _formRegionSelectOptions;

        // SFTP client.
        private SftpClient _sftpClient = null;

        // The various forms that are used for modules.
        private FormMacroTag _formMacroTag;
        private FormRegion _formRegion;
        private FormScreen _formScreen;
        private FormEditor _formEditor;
        private FormTrigger _formTrigger;
        private FormSchedule _formSchedule;

        // Setup.
        private FormSetup _formSetup;

        // Screeshot Properties
        private FormScreenshotMetadata _formScreenshotMetadata;

        // The form to display when challenging the user for the passphrase in order to unlock the running screen capture session.
        private FormEnterPassphrase _formEnterPassphrase;

        // A small window is shown when the user selects "Show Screen Capture Status" from the system tray icon menu.
        private FormScreenCaptureStatus _formScreenCaptureStatus;

        // The Dynamic Regex Validator tool.
        private FormDynamicRegexValidator _formDynamicRegexValidator;

        // The Label Switcher tool.
        private FormLabelSwitcher _formLabelSwitcher;

        // Encryptor / Decryptor tool.
        private FormEncryptorDecryptor _formEncryptorDecryptor;

        // Keyboard Shortcuts
        private HotKeyMap _hotKeyMap;

        // Keyboard Shortcuts - Start Screen Capture
        private string _keyboardShortcutStartScreenCaptureModifier1UserSetting;
        private string _keyboardShortcutStartScreenCaptureModifier2UserSetting;
        private string _keyboardShortcutStartScreenCaptureKeyUserSetting;

        // Keyboard Shortcuts - Stop Screen Capture
        private string _keyboardShortcutStopScreenCaptureModifier1UserSetting;
        private string _keyboardShortcutStopScreenCaptureModifier2UserSetting;
        private string _keyboardShortcutStopScreenCaptureKeyUserSetting;

        // Keyboard Shortcuts - Capture Now Archive
        private string _keyboardShortcutCaptureNowArchiveModifier1UserSetting;
        private string _keyboardShortcutCaptureNowArchiveModifier2UserSetting;
        private string _keyboardShortcutCaptureNowArchiveKeyUserSetting;

        // Keyboard Shortcuts - Capture Now Edit
        private string _keyboardShortcutCaptureNowEditModifier1UserSetting;
        private string _keyboardShortcutCaptureNowEditModifier2UserSetting;
        private string _keyboardShortcutCaptureNowEditKeyUserSetting;

        // Keyboard Shortcuts - Region Select Clipboard
        private string _keyboardShortcutRegionSelectClipboardModifier1UserSetting;
        private string _keyboardShortcutRegionSelectClipboardModifier2UserSetting;
        private string _keyboardShortcutRegionSelectClipboardKeyUserSetting;

        // Keyboard Shorcuts - Region Select Auto Save
        private string _keyboardShortcutRegionSelectAutoSaveModifier1UserSetting;
        private string _keyboardShortcutRegionSelectAutoSaveModifier2UserSetting;
        private string _keyboardShortcutRegionSelectAutoSaveKeyUserSetting;

        // Keyboard Shortcuts - Region Select Edit
        private string _keyboardShortcutRegionSelectEditModifier1UserSetting;
        private string _keyboardShortcutRegionSelectEditModifier2UserSetting;
        private string _keyboardShortcutRegionSelectEditKeyUserSetting;

        // Classes
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
        private EmailManager _emailManager;

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

            _hotKeyMap = new HotKeyMap();
            RegisterKeyboardShortcuts();
            _hotKeyMap.KeyPressed += new EventHandler<KeyPressedEventArgs>(hotKey_KeyPressed);

            LoadSettings();

            Text = _config.Settings.ApplicationName;

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
            bool firstRun = Convert.ToBoolean(_config.Settings.User.GetByKey("FirstRun", _config.Settings.DefaultSettings.FirstRun).Value);

            string welcome = "Welcome to " + _config.Settings.ApplicationName + " " + _config.Settings.ApplicationVersion + " (\"" + _config.Settings.ApplicationCodename + "\")";

            if (firstRun)
            {
                welcome += " - Please click on Help to get started with the application";

                // To be figured out later. I want to have a "Setup Wizard" opened on the first run.
                //_formSetupWizard.ShowDialog(this);
            }

            HelpMessage(welcome);

            LoadHelpTips();

            ShowInfo();

            SearchFilterValues();
            SearchDates();
            SearchScreenshots();

            // Start the scheduled capture timer.
            timerScheduledCapture.Interval = 1000;
            timerScheduledCapture.Enabled = true;
            timerScheduledCapture.Start();

            // Set this to true so anything that needs to be processed at startup will be done in the
            // first tick of the scheduled capture timer. This is when using -hide and -start command line options
            // so we avoid having to show the interface and/or the system tray icon too early during application startup.
            _appStarted = true;
        }

        /// <summary>
        /// Set opacity to the appropriate value and taskbar appearance based on visibility.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnVisibleChanged(EventArgs e)
        {
            // Annoyingly, WinForms likes to show the main form even if Visible is set to false in the Designer code
            // so we have to make sure we only consider the visibility of the main form if either the methods
            // ShowInterface or HideInterface have been called and therefore the _initialVisibilitySet bool variable
            // has been flagged as true by those methods. This fixes a situation where the user could have no Triggers at all so
            // we want to keep the main form invisible if there are no Triggers to trigger either ShowInterface or HideInterface.
            if (!_initialVisibilitySet)
            {
                return;
            }

            if (Visible)
            {
                Opacity = 100;
                ShowInTaskbar = true;
                Show();
                Focus();
            }
            else
            {
                Opacity = 0;
                ShowInTaskbar = false;
                Hide();
            }

            base.OnVisibleChanged(e);
        }

        /// <summary>
        /// When this form is closing we can either exit the application or just close this window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown ||
                e.CloseReason == CloseReason.ApplicationExitCall)
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

                    if (dates != null)
                    {
                        DateTime[] boldedDates = new DateTime[dates.Count];

                        for (int i = 0; i < dates.Count; i++)
                        {
                            boldedDates.SetValue(ConvertDateStringToDateTime(dates[i].ToString()), i);
                        }

                        monthCalendar.BoldedDates = boldedDates;
                    }
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
            _fileSystem.DeleteDirectory(_fileSystem.SlidesFolder);
        }

        /// <summary>
        /// Shows the interface.
        /// </summary>
        private void ShowInterface()
        {
            try
            {
                if (_initialVisibilitySet && Visible)
                {
                    return;
                }

                _log.WriteDebugMessage("Showing interface");

                string passphrase = _config.Settings.User.GetByKey("Passphrase", _config.Settings.DefaultSettings.Passphrase).Value.ToString();

                if (!string.IsNullOrEmpty(passphrase))
                {
                    _screenCapture.LockScreenCaptureSession = true;

                    if (!_formEnterPassphrase.Visible)
                    {
                        _formEnterPassphrase.Text = "Auto Screen Capture - Enter Passphrase (Show Interface)";
                        _formEnterPassphrase.ShowDialog(this);
                    }
                    else
                    {
                        _formEnterPassphrase.Activate();
                    }

                    if (_formEnterPassphrase.DialogResult != DialogResult.OK)
                    {
                        _log.WriteErrorMessage("Passphrase incorrect or not entered. Cannot show interface. Screen capture session has been locked. Interface is now hidden");

                        HideInterface();

                        return;
                    }

                    _screenCapture.LockScreenCaptureSession = false;
                }

                SearchDates();
                SearchScreenshots();

                PopulateLabelList();

                _initialVisibilitySet = true;
                Visible = true;

                Opacity = 100;
                ShowInTaskbar = true;
                Show();
                Activate();

                // If the window is mimimized then show it when the user wants to open the window.
                if (WindowState == FormWindowState.Minimized)
                {
                    WindowState = FormWindowState.Normal;
                }

                _log.WriteDebugMessage("Running triggers of condition type InterfaceShowing");
                RunTriggersOfConditionType(TriggerConditionType.InterfaceShowing);
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

                string passphrase = _config.Settings.User.GetByKey("Passphrase", _config.Settings.DefaultSettings.Passphrase).Value.ToString();

                if (!string.IsNullOrEmpty(passphrase))
                {
                    _screenCapture.LockScreenCaptureSession = true;
                }
                else
                {
                    _screenCapture.LockScreenCaptureSession = false;
                }

                // Close all the sub forms that may still be open.

                if (_formScreenshotMetadata.Visible)
                {
                    _formScreenshotMetadata.Close();
                }

                if (_formEmailSettings.Visible)
                {
                    _formEmailSettings.Close();
                }

                if (_formFileTransferSettings.Visible)
                {
                    _formFileTransferSettings.Close();
                }

                if (_formMacroTag.Visible)
                {
                    _formMacroTag.Close();
                }

                if (_formRegion.Visible)
                {
                    _formRegion.Close();
                }

                if (_formScreen.Visible)
                {
                    _formScreen.Close();
                }

                if (_formEditor.Visible)
                {
                    _formEditor.Close();
                }

                if (_formTrigger.Visible)
                {
                    _formTrigger.Close();
                }

                if (_formSchedule.Visible)
                {
                    _formSchedule.Close();
                }

                if (_formSetup.Visible)
                {
                    _formSetup.Close();
                }

                if (_formEnterPassphrase.Visible)
                {
                    _formEnterPassphrase.Close();
                }

                _initialVisibilitySet = true;
                Visible = false;

                Opacity = 0;
                ShowInTaskbar = false;
                Hide();

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
                _formScreenCaptureStatus.Activate();
            }
        }

        /// <summary>
        /// Shows the "Dynamic Regex Validator" tool.
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
                _formDynamicRegexValidator.Activate();
            }
        }

        /// <summary>
        /// Shows the "Encryptor / Decryptor" tool.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemEncryptorDecryptor_Click(object sender, EventArgs e)
        {
            if (!_formEncryptorDecryptor.Visible)
            {
                _formEncryptorDecryptor.Show();
            }
            else
            {
                _formEncryptorDecryptor.Activate();
            }
        }

        /// <summary>
        /// Shows the "Screen Capture Status With Label Switcher" tool.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemLabelSwitcher_Click(object sender, EventArgs e)
        {
            PopulateLabelList();

            if (!_formLabelSwitcher.Visible)
            {
                _formLabelSwitcher.Show();
            }
            else
            {
                _formLabelSwitcher.Activate();
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
                _formAbout.Activate();
            }
        }

        /// <summary>
        /// Shows the "Email Settings" window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripDropDownButtonEmailSettings_Click(object sender, EventArgs e)
        {
            ShowInterface();

            if (!_formEmailSettings.Visible)
            {
                _formEmailSettings.ShowDialog(this);
            }
            else
            {
                _formEmailSettings.Activate();
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
        private void toolStripDropDownButtonFileTransferSettings_Click(object sender, EventArgs e)
        {
            ShowInterface();

            if (!_formFileTransferSettings.Visible)
            {
                _formFileTransferSettings.ShowDialog(this);
            }
            else
            {
                _formFileTransferSettings.Activate();
            }

            if (_formFileTransferSettings.DialogResult == DialogResult.OK)
            {
                BuildViewTabPages();
            }
        }

        /// <summary>
        /// Opens the appropriate tab in the Setup form based on the selected Setup menu item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemSetup_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem selectedMenuItem = (ToolStripMenuItem)sender;

            _formSetup.ShowTabPage(selectedMenuItem.Text);

            _formSetup.ShowDialog(this);

            if (_formSetup.DialogResult == DialogResult.OK)
            {
                SaveSettings();

                RegisterKeyboardShortcuts();
            }
        }

        /// <summary>
        /// Toggles "Preview".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripDropDownButtonPreview_Click(object sender, EventArgs e)
        {
            _preview = !_preview;

            _config.Settings.User.SetValueByKey("Preview", _preview);

            ShowScreenshotBySlideIndex();

            SaveSettings();
        }

        /// <summary>
        /// Displays the appropriate help tip for the selected module.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControlModules_SelectedIndexChanged(object sender, EventArgs e)
        {
            _config.Settings.User.SetValueByKey("SelectedModuleIndex", tabControlModules.SelectedIndex);

            switch (tabControlModules.SelectedIndex)
            {
                // Screens
                case 0:
                    labelModuleHelp.Text = "Screens are listed here. Each screen represents a display. Have the Preview button enabled while viewing the Dashboard to see what would be captured when starting a screen capture session.";
                    break;

                // Regions
                case 1:
                    labelModuleHelp.Text = "Add, configure, or remove a region. Each region has its own X, Y, Width, and Height values to define an area of a screen. You can either enter these values or use the Region Select button and select the region you want.";
                    break;

                // Editors
                case 2:
                    labelModuleHelp.Text = "An \"Editor\" is an application or script that can open or edit a screenshot. You can add as many applications or scripts as needed. Make sure to use $filepath$ as an argument to represent the filepath of the screenshot being used.";
                    break;

                // Schedules
                case 3:
                    labelModuleHelp.Text = "Create a schedule to take screenshots at a particular time or between a start time and an end time on certain days of the week. Each schedule can also have its own screen capture interval.";
                    break;

                // Macro Tags
                case 4:
                    labelModuleHelp.Text = "A macro tag is included in the filepath for each screenshot so that a certain value (such as the current date and time) is parsed when the screenshot is saved. For example, the %date% macro tag is parsed as the current date.";
                    break;

                // Triggers
                case 5:
                    labelModuleHelp.Text = "Triggers control the behaviour of the application. Each trigger reacts to a defined condition and performs a defined action based on that condition. You could run an editor after each screenshot is taken to edit that screenshot.";
                    break;
            }
        }

        /// <summary>
        /// Opens the program folder.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemOpenProgramFolder_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo("explorer.exe");
                processStartInfo.Arguments = AppDomain.CurrentDomain.BaseDirectory;
                processStartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;

                Process process = new Process
                {
                    StartInfo = processStartInfo,
                };

                process.Start();
            }
            catch (Exception ex)
            {
                _log.WriteExceptionMessage("Unable to open program folder", ex);
            }
        }

        /// <summary>
        /// Exits the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripDropDownButtonExit_Click(object sender, EventArgs e)
        {
            ExitApplication();
        }
    }
}