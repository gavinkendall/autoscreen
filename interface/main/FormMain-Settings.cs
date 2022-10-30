//-----------------------------------------------------------------------
// <copyright file="FormMain-Settings.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>Methods for loading settings and saving settings.</summary>
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
using System.Windows.Forms;

namespace AutoScreenCapture
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// Loads the user's saved settings.
        /// </summary>
        private void LoadSettings()
        {
            try
            {
                _macroParser = _config.MacroParser;

                _log.WriteDebugMessage("It looks like the application successfully parsed your \"" + _fileSystem.ConfigFile + "\" file");

                _log.WriteDebugMessage("Initializing screen capture");
                _screenCapture = new ScreenCapture(_config, _fileSystem, _log);

                _log.WriteDebugMessage("Initializing image format collection");
                _imageFormatCollection = new ImageFormatCollection();

                _log.WriteDebugMessage("Initializing forms");
                _formAbout = new FormAbout();
                _formHelp = new FormHelp();
                _formDynamicRegexValidator = new FormDynamicRegexValidator();
                _formScreenshotMetadata = new FormScreenshotMetadata();
                _formScreenCaptureStatusWithLabelSwitcher = new FormScreenCaptureStatusWithLabelSwitcher(_config, _fileSystem);
                _formMacroTag = new FormMacroTag(_macroParser);
                _formRegion = new FormRegion(_screenCapture, _macroParser, _fileSystem, _config, _log);
                _formScreen = new FormScreen(_screenCapture, _macroParser, _fileSystem, _config, _log);
                _formEditor = new FormEditor(_config, _fileSystem);
                _formEmailSettings = new FormEmailSettings(_config, _fileSystem, _log);
                _formFileTransferSettings = new FormFileTransferSettings(_config, _fileSystem, _log);
                _formRegionSelectOptions = new FormRegionSelectOptions(_config, _fileSystem, _imageFormatCollection);
                _formCaptureNowOptions = new FormCaptureNowOptions(_config, _fileSystem);
                _formSchedule = new FormSchedule(_formScreen, _formRegion);
                _formTrigger = new FormTrigger(_fileSystem);
                _formEnterPassphrase = new FormEnterPassphrase(_security, _config, _log);
                _formScreenCaptureStatus = new FormScreenCaptureStatus();

                // Command Deck
                _formCommandDeck = new FormCommandDeck();

                // Auto Screen Capture For Beginners
                _formAutoScreenCaptureForBeginners = new FormAutoScreenCaptureForBeginners(_config);

                // Command Deck Event Handlers
                _formCommandDeck.Clipboard += toolStripMenuItemRegionSelectClipboard_Click;
                _formCommandDeck.ClipboardAutoSave += toolStripMenuItemRegionSelectClipboardAutoSave_Click;
                _formCommandDeck.ClipboardAutoSaveEdit += toolStripMenuItemRegionSelectClipboardAutoSaveEdit_Click;
                _formCommandDeck.ClipboardFloatingScreenshot += toolStripMenuItemRegionSelectClipboardFloatingScreenshot_Click;
                _formCommandDeck.FloatingScreenshot += toolStripMenuItemRegionSelectFloatingScreenshot_Click;
                _formCommandDeck.AddRegion += toolStripMenuItemRegionSelectAddRegion_Click;
                _formCommandDeck.AddRegionExpress += toolStripMenuItemRegionSelectAddRegionExpress_Click;

                // Start/Stop Schedule Event Handlers
                _formSchedule.StartSchedule += _formSchedule_StartSchedule;
                _formSchedule.StopSchedule += _formSchedule_StopSchedule;

                // Start/Stop Button Event Handlers for Command Deck, Screen Capture Status, and Screen Capture Status With Label Switcher
                _formCommandDeck.buttonStartStopScreenCapture.Click += buttonStartStopScreenCapture_Click; // Command Deck
                _formScreenCaptureStatus.buttonStartStopScreenCapture.Click += buttonStartStopScreenCapture_Click; // Screen Capture Status
                _formScreenCaptureStatusWithLabelSwitcher.buttonStartStopScreenCapture.Click += buttonStartStopScreenCapture_Click; // Screen Capture Status With Label Switcher

                // Start/Stop Button Tool Tips
                ToolTip toolTipButtonStartStopScreenCaptureCommandDeck = new ToolTip();
                toolTipButtonStartStopScreenCaptureCommandDeck.SetToolTip(_formCommandDeck.buttonStartStopScreenCapture, "Start/Stop Screen Capture");

                ToolTip toolTipButtonStartStopScreenCaptureScreenCaptureStatus = new ToolTip();
                toolTipButtonStartStopScreenCaptureScreenCaptureStatus.SetToolTip(_formScreenCaptureStatus.buttonStartStopScreenCapture, "Start/Stop Screen Capture");

                ToolTip toolTipButtonStartStopScreenCaptureLabelSwitcher = new ToolTip();
                toolTipButtonStartStopScreenCaptureLabelSwitcher.SetToolTip(_formScreenCaptureStatusWithLabelSwitcher.buttonStartStopScreenCapture, "Start/Stop Screen Capture");

                // Capture Now / Archive Button Event Handler for Command Deck
                _formCommandDeck.buttonCaptureNow.Click += toolStripMenuItemCaptureNowArchive_Click;

                // Capture Now / Edit Button Event Handler for Command Deck
                _formCommandDeck.buttonCaptureNowEdit.Click += toolStripMenuItemCaptureNowEdit_Click;

                // Capture Now / Archive Tool Tip for Command Deck
                ToolTip toolTipButtonCaptureNowCommandDeck = new ToolTip();
                toolTipButtonCaptureNowCommandDeck.SetToolTip(_formCommandDeck.buttonCaptureNow, "Capture Now / Archive");

                // Capture Now / Edit Tool Tip for Command Deck
                ToolTip toolTipButtonCaptureNowEditCommandDeck = new ToolTip();
                toolTipButtonCaptureNowEditCommandDeck.SetToolTip(_formCommandDeck.buttonCaptureNowEdit, "Capture Now / Edit");

                // Region Select Tool Tip for Command Deck
                ToolTip toolTipShowHideRegionSelectCommandDeck = new ToolTip();
                toolTipShowHideRegionSelectCommandDeck.SetToolTip(_formCommandDeck.buttonShowHideRegionSelect, "Show/Hide Region Select");

                // Auto Screen Capture For Beginners
                _formAutoScreenCaptureForBeginners.buttonStartScreenCapture.Click += buttonStartScreenCaptureForBeginners_Click;
                _formAutoScreenCaptureForBeginners.buttonStopScreenCapture.Click += buttonStopScreenCaptureForBeginners_Click;
                _formAutoScreenCaptureForBeginners.buttonExitApplication.Click += buttonExitAutoScreenCaptureForBeginners_Click;

                _log.WriteDebugMessage("Initializing email manager");
                _emailManager = new EmailManager(_log);

                if (_config.CleanStartup)
                {
                    _log.WriteDebugMessage("CleanStartup detected so we will not load XML data");
                }
                else
                {
                    _log.WriteDebugMessage("Initializing editor collection");

                    if (!_formEditor.EditorCollection.LoadXmlFileAndAddEditors(_config, _fileSystem, _log))
                    {
                        _screenCapture.ApplicationError = true;
                    }

                    _log.WriteDebugMessage("Number of editors loaded = " + _formEditor.EditorCollection.Count);

                    _log.WriteDebugMessage("Initializing trigger collection");

                    if (!_formTrigger.TriggerCollection.LoadXmlFileAndAddTriggers(_config, _fileSystem, _log))
                    {
                        _screenCapture.ApplicationError = true;
                    }

                    _log.WriteDebugMessage("Number of triggers loaded = " + _formTrigger.TriggerCollection.Count);

                    // Make sure that what we're about to do is only possible with 2.5 Limoncello (or a later version).
                    // It's because, when introducing the ShowInterface user setting in 2.5 and upgrading from an old version,
                    // the old version behaved in such a way that "ShowInterface" didn't exist yet. So we have to simulate the old behaviour.
                    if (_config.Settings.User.AppCodename.Equals(Settings.CODENAME_LIMONCELLO))
                    {
                        // **** 2.5 LIMONCELLO BEHAVIOUR ****
                        // If the ShowInterface user setting is set to False then make sure to disable all Triggers that use the ShowInterface action.
                        // (This would have been set in autoscreen.conf)
                        if (!Convert.ToBoolean(_config.Settings.User.GetByKey("ShowInterface").Value))
                        {
                            foreach (Trigger trigger in _formTrigger.TriggerCollection)
                            {
                                if (trigger.ActionType == TriggerActionType.ShowInterface)
                                {
                                    trigger.Enable = false;
                                }
                            }
                        }

                        // **** 2.5 LIMONCELLO BEHAVIOUR ****
                        // If the ShowSystemTrayIcon user setting is set to False then make sure to disable all Triggers that use the ShowSystemTrayIcon action.
                        // (This would have been set in autoscreen.conf)
                        if (!Convert.ToBoolean(_config.Settings.User.GetByKey("ShowSystemTrayIcon").Value))
                        {
                            foreach (Trigger trigger in _formTrigger.TriggerCollection)
                            {
                                if (trigger.ActionType == TriggerActionType.ShowSystemTrayIcon)
                                {
                                    trigger.Enable = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        // **** UPGRADE PATH FROM 2.4 BLADE TO 2.5 LIMONCELLO ****
                        // At this point we'll have "ShowInterface" set to "False" if we were following an upgrade path from an old version of the application.
                        // This might not represent the true intended purpose when we look at the triggers. So go through the triggers looking for any triggers that
                        // want to show the interface (and the system tray icon) on application startup and correct the ShowInterface (new in 2.5) and ShowSystemTrayIcon (from 2.4)
                        // user settings appropriately.
                        foreach (Trigger trigger in _formTrigger.TriggerCollection)
                        {
                            if (trigger.ConditionType == TriggerConditionType.ApplicationStartup &&
                                trigger.ActionType == TriggerActionType.ShowInterface)
                            {
                                _config.Settings.User.SetValueByKey("ShowInterface", true);
                            }

                            if (trigger.ConditionType == TriggerConditionType.ApplicationStartup &&
                                trigger.ActionType == TriggerActionType.ShowSystemTrayIcon)
                            {
                                _config.Settings.User.SetValueByKey("ShowSystemTrayIcon", true);
                            }
                        }

                        // Make sure to save the ShowInterface and ShowSystemTrayIcon settings
                        // so that the interface and system tray icon appear on the next run of the application.
                        if (!_config.Settings.User.Save(_config.Settings, _fileSystem))
                        {
                            _screenCapture.ApplicationError = true;
                        }
                    }

                    _log.WriteDebugMessage("Initializing region collection");

                    if (!_formRegion.RegionCollection.LoadXmlFileAndAddRegions(_imageFormatCollection, _config, _fileSystem, _log))
                    {
                        _screenCapture.ApplicationError = true;
                    }

                    // Add regions to the Screen form so we can select them as a source.
                    _formScreen.RegionCollection = _formRegion.RegionCollection;
                    _log.WriteDebugMessage("Number of regions loaded = " + _formRegion.RegionCollection.Count);

                    _log.WriteDebugMessage("Initializing screen collection");

                    if (!_formScreen.ScreenCollection.LoadXmlFileAndAddScreens(_imageFormatCollection, _config, _macroParser, _fileSystem, _log))
                    {
                        _screenCapture.ApplicationError = true;
                    }

                    _log.WriteDebugMessage("Number of screens loaded = " + _formScreen.ScreenCollection.Count);

                    _log.WriteDebugMessage("Initializing tag collection");

                    if (!_formMacroTag.MacroTagCollection.LoadXmlFileAndAddTags(_config, _macroParser, _fileSystem, _log))
                    {
                        _screenCapture.ApplicationError = true;
                    }

                    _log.WriteDebugMessage("Number of tags loaded = " + _formMacroTag.MacroTagCollection.Count);

                    _log.WriteDebugMessage("Initializing schedule collection");

                    if (!_formSchedule.ScheduleCollection.LoadXmlFileAndAddSchedules(_config, _fileSystem, _log))
                    {
                        _screenCapture.ApplicationError = true;
                    }

                    _log.WriteDebugMessage("Number of schedules loaded = " + _formSchedule.ScheduleCollection.Count);
                }

                _log.WriteDebugMessage("Building screens module");
                BuildScreensModule();

                _log.WriteDebugMessage("Building editors module");
                BuildEditorsModule();

                _log.WriteDebugMessage("Building triggers module");
                BuildTriggersModule();

                _log.WriteDebugMessage("Building regions module");
                BuildRegionsModule();

                _log.WriteDebugMessage("Building tags module");
                BuildMacroTagsModule();

                _log.WriteDebugMessage("Building schedules module");
                BuildSchedulesModule();

                _log.WriteDebugMessage("Building screenshot preview context menu");
                BuildScreenshotPreviewContextualMenu();

                // Add the ScheduleTimer_Tick event to each schedule.
                // This ensures that each schedule can run on its own interval when taking screenshots
                // and acts independently from the main timer (depending on the logic chosen for the schedule).
                foreach (Schedule schedule in _formSchedule.ScheduleCollection)
                {
                    schedule.Timer.Tag = schedule;
                    schedule.Timer.Interval = schedule.ScreenCaptureInterval;
                    schedule.Timer.Enabled = false;
                    schedule.Timer.Tick += ScheduleTimer_Tick;
                }

                tabControlViews.SelectedIndex = 0;

                Setting selectedTabPageIndexSetting = _config.Settings.User.GetByKey("SelectedTabPageIndex");

                if (selectedTabPageIndexSetting != null)
                {
                    // Set the tab page we want to look at. By default it's going to be index 0 for the "Dashboard" tab page.
                    tabControlViews.SelectedIndex = Convert.ToInt32(_config.Settings.User.GetByKey("SelectedTabPageIndex").Value);
                }

                if (tabControlViews.SelectedIndex < 0)
                {
                    tabControlViews.SelectedIndex = 0;
                }

                tabControlModules.SelectedIndex = 0;

                Setting selectedModuleIndexSetting = _config.Settings.User.GetByKey("SelectedModuleIndex");

                if (selectedModuleIndexSetting != null)
                {
                    // Set the module we want to look at.
                    tabControlModules.SelectedIndex = Convert.ToInt32(_config.Settings.User.GetByKey("SelectedModuleIndex").Value);
                }

                Setting modulesPanelOpenSetting = _config.Settings.User.GetByKey("ModulesPanelOpen");

                if (modulesPanelOpenSetting != null)
                {
                    bool modulesPanelOpen = Convert.ToBoolean(modulesPanelOpenSetting.Value);

                    if (modulesPanelOpen)
                    {
                        panelModules.Width = 274;

                        labelHelp.Width -= 274;

                        if (tabControlViews.Width > 796)
                        {
                            tabControlViews.Width -= 274;
                        }

                        buttonResizeModulesPanel.Text = ">";
                    }
                    else
                    {
                        panelModules.Width = 0;

                        labelHelp.Width += 274;
                        tabControlViews.Width += 274;

                        buttonResizeModulesPanel.Text = "<";
                    }
                }

                // Preview

                _preview = false;

                Setting previewSetting = _config.Settings.User.GetByKey("Preview");

                if (previewSetting != null)
                {
                    _preview = Convert.ToBoolean(_config.Settings.User.GetByKey("Preview").Value);
                }

                _log.WriteDebugMessage("Preview = " + _preview.ToString());

                _dashboardGroupBoxSize = 250;

                Setting dashboardGroupBoxSizeSetting = _config.Settings.User.GetByKey("DashboardGroupBoxSize");

                if (dashboardGroupBoxSizeSetting != null)
                {
                    _dashboardGroupBoxSize = Convert.ToInt32(_config.Settings.User.GetByKey("DashboardGroupBoxSize").Value);
                }

                _log.WriteDebugMessage("DashboardGroupBoxSize = " + _dashboardGroupBoxSize);
                _log.WriteDebugMessage("Building view tab pages");
                BuildViewTabPages();

                // Screenshots Collection
                _log.WriteDebugMessage("Initializing screenshot collection");
                _screenshotCollection = new ScreenshotCollection(_imageFormatCollection, _formScreen.ScreenCollection, _screenCapture, _config, _fileSystem, _log, _security);
                _screenshotCollection.LoadXmlFile(_config);

                // Encryptor / Decryptor
                _formEncryptorDecryptor = new FormEncryptorDecryptor(_log, _config, _security, _fileSystem, _screenshotCollection);
                _formEncryptorDecryptor.screenshotsEncrypted += ScreenshotsEncrypted;
                _formEncryptorDecryptor.screenshotsDecrypted += ScreenshotsDecrypted;

                int screenCaptureInterval = 60000;

                Setting screenCaptureIntervalSetting = _config.Settings.User.GetByKey("ScreenCaptureInterval");

                if (screenCaptureIntervalSetting != null)
                {
                    screenCaptureInterval = Convert.ToInt32(_config.Settings.User.GetByKey("ScreenCaptureInterval").Value);
                }

                _log.WriteDebugMessage("ScreenCaptureInterval = " + screenCaptureInterval);

                // Set the interval for "Special Schedule"
                _formSchedule.ScheduleCollection.SpecialScheduleScreenCaptureInterval = screenCaptureInterval;

                _log.WriteDebugMessage("Assigning screen capture interval value to its appropriate hour, minute, second, and millisecond variables");

                decimal screenCaptureIntervalHours = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(screenCaptureInterval)).Hours);
                _log.WriteDebugMessage("Hours = " + screenCaptureIntervalHours);

                decimal screenCaptureIntervalMinutes = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(screenCaptureInterval)).Minutes);
                _log.WriteDebugMessage("Minutes = " + screenCaptureIntervalMinutes);

                decimal screenCaptureIntervalSeconds = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(screenCaptureInterval)).Seconds);
                _log.WriteDebugMessage("Seconds = " + screenCaptureIntervalSeconds);

                decimal screenCaptureIntervalMilliseconds = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(screenCaptureInterval)).Milliseconds);
                _log.WriteDebugMessage("Milliseconds = " + screenCaptureIntervalMilliseconds);

                // Auto Screen Capture For Beginners
                _formAutoScreenCaptureForBeginners.textBoxScreenshotsFolder.Text = _fileSystem.ScreenshotsFolder;
                _formAutoScreenCaptureForBeginners.textBoxFilenamePattern.Text = _fileSystem.FilenamePattern;
                _formAutoScreenCaptureForBeginners.numericUpDownHoursInterval.Value = screenCaptureIntervalHours;
                _formAutoScreenCaptureForBeginners.numericUpDownMinutesInterval.Value = screenCaptureIntervalMinutes;
                _formAutoScreenCaptureForBeginners.numericUpDownSecondsInterval.Value = screenCaptureIntervalSeconds;
                _formAutoScreenCaptureForBeginners.numericUpDownMillisecondsInterval.Value = screenCaptureIntervalMilliseconds;

                // Setup
                _formSetup = new FormSetup(_log, _security, _config, _fileSystem, _screenCapture, _formScreenCaptureStatusWithLabelSwitcher, _formScreen, _formRegion, _formMacroTag.MacroTagCollection, _macroParser, _screenshotCollection);

                _formSetup.numericUpDownHoursInterval.Value = screenCaptureIntervalHours;
                _formSetup.numericUpDownMinutesInterval.Value = screenCaptureIntervalMinutes;
                _formSetup.numericUpDownSecondsInterval.Value = screenCaptureIntervalSeconds;
                _formSetup.numericUpDownMillisecondsInterval.Value = screenCaptureIntervalMilliseconds;

                // Optimize Screen Capture
                _formSetup.checkBoxOptimizeScreenCapture.Checked = false;

                Setting optimizeScreenCaptureSetting = _config.Settings.User.GetByKey("OptimizeScreenCapture");

                if (optimizeScreenCaptureSetting != null)
                {
                    _formSetup.checkBoxOptimizeScreenCapture.Checked = Convert.ToBoolean(_config.Settings.User.GetByKey("OptimizeScreenCapture").Value);
                }

                _formSetup.trackBarImageDiffTolerance.Value = 20;

                Setting imageDiffToleranceSetting = _config.Settings.User.GetByKey("ImageDiffTolerance");

                if (imageDiffToleranceSetting != null)
                {
                    _formSetup.trackBarImageDiffTolerance.Value = Convert.ToInt32(_config.Settings.User.GetByKey("ImageDiffTolerance").Value);
                }

                _formSetup.numericUpDownCaptureLimit.Value = 0;

                Setting captureLimitSetting = _config.Settings.User.GetByKey("CaptureLimit");

                if (captureLimitSetting != null)
                {
                    _formSetup.numericUpDownCaptureLimit.Value = Convert.ToInt32(_config.Settings.User.GetByKey("CaptureLimit").Value);
                }

                _log.WriteDebugMessage("CaptureLimit = " + _formSetup.numericUpDownCaptureLimit.Value);

                _formSetup.checkBoxCaptureLimit.Checked = false;

                Setting captureLimitCheckSetting = _config.Settings.User.GetByKey("CaptureLimitCheck");

                if (captureLimitCheckSetting != null)
                {
                    _formSetup.checkBoxCaptureLimit.Checked = Convert.ToBoolean(_config.Settings.User.GetByKey("CaptureLimitCheck").Value);
                }

                _log.WriteDebugMessage("CaptureLimitCheck = " + _formSetup.checkBoxCaptureLimit.Checked);

                _formSetup.checkBoxInitialScreenshot.Checked = true;

                Setting takeInitialScreenshotSetting = _config.Settings.User.GetByKey("TakeInitialScreenshot");

                if (takeInitialScreenshotSetting != null)
                {
                    _formSetup.checkBoxInitialScreenshot.Checked = Convert.ToBoolean(_config.Settings.User.GetByKey("TakeInitialScreenshot").Value);
                }

                _log.WriteDebugMessage("TakeInitialScreenshot = " + _formSetup.checkBoxInitialScreenshot.Checked);

                // Active Window Title

                _formSetup.checkBoxActiveWindowTitleComparisonCheck.Checked = false;

                Setting activeWindowTitleCaptureCheckSetting = _config.Settings.User.GetByKey("ActiveWindowTitleCaptureCheck");

                if (activeWindowTitleCaptureCheckSetting != null)
                {
                    _formSetup.checkBoxActiveWindowTitleComparisonCheck.Checked = Convert.ToBoolean(_config.Settings.User.GetByKey("ActiveWindowTitleCaptureCheck").Value);
                }

                _formSetup.checkBoxActiveWindowTitleComparisonCheckReverse.Checked = false;

                Setting activeWindowTitleNoMatchCheckSetting = _config.Settings.User.GetByKey("ActiveWindowTitleNoMatchCheck");

                if (activeWindowTitleNoMatchCheckSetting != null)
                {
                    _formSetup.checkBoxActiveWindowTitleComparisonCheckReverse.Checked = Convert.ToBoolean(_config.Settings.User.GetByKey("ActiveWindowTitleNoMatchCheck").Value);
                }

                _formSetup.textBoxActiveWindowTitle.Text = string.Empty;

                Setting activeWindowTitleCaptureTextSetting = _config.Settings.User.GetByKey("ActiveWindowTitleCaptureText");

                if (activeWindowTitleCaptureTextSetting != null)
                {
                    _formSetup.textBoxActiveWindowTitle.Text = _config.Settings.User.GetByKey("ActiveWindowTitleCaptureText").Value.ToString();
                }

                _formSetup.textBoxActiveWindowTitleTest.Text = string.Empty;

                Setting activeWindowTitleSampleTextSetting = _config.Settings.User.GetByKey("ActiveWindowTitleSampleText");

                if (activeWindowTitleSampleTextSetting != null)
                {
                    _formSetup.textBoxActiveWindowTitleTest.Text = _config.Settings.User.GetByKey("ActiveWindowTitleSampleText").Value.ToString();
                }

                int activeWindowTitleMatchType = 2;

                Setting activeWindowTitleMatchTypeSetting = _config.Settings.User.GetByKey("ActiveWindowTitleMatchType");

                if (activeWindowTitleMatchTypeSetting != null)
                {
                    activeWindowTitleMatchType = Convert.ToInt32(_config.Settings.User.GetByKey("ActiveWindowTitleMatchType").Value);
                }

                switch (activeWindowTitleMatchType)
                {
                    case 1:
                        _formSetup.radioButtonCaseSensitiveMatch.Checked = true;
                        break;

                    case 2:
                        _formSetup.radioButtonCaseInsensitiveMatch.Checked = true;
                        break;

                    case 3:
                        _formSetup.radioButtonRegularExpressionMatch.Checked = true;
                        break;
                }

                // Labels

                _formSetup.checkBoxScreenshotLabel.Checked = false;

                Setting applyScreenshotLabelSetting = _config.Settings.User.GetByKey("ApplyScreenshotLabel");

                if (applyScreenshotLabelSetting != null)
                {
                    _formSetup.checkBoxScreenshotLabel.Checked = Convert.ToBoolean(_config.Settings.User.GetByKey("ApplyScreenshotLabel").Value);
                }

                PopulateLabelList();

                // Application Focus
                _formSetup.RefreshApplicationFocusList();

                EnableStartCapture();

                CaptureLimitCheck();

                // Set the filter type to be the last selected index that was saved (and if no setting is found just set it to selected index 0).
                // IMPORTANT: Do not attempt to set the same index for Filter Value here as that control is populated based on the selected index of Filter Type.
                comboBoxFilterType.SelectedIndex = 0;

                Setting filterTypeSetting = _config.Settings.User.GetByKey("FilterType");

                if (filterTypeSetting != null)
                {
                    comboBoxFilterType.SelectedIndex = Convert.ToInt32(_config.Settings.User.GetByKey("FilterType").Value);
                }

                monthCalendar.SelectionStart = DateTime.Now;

                Setting selectedCalendarDaySetting = _config.Settings.User.GetByKey("SelectedCalendarDay");

                if (selectedCalendarDaySetting != null)
                {
                    monthCalendar.SelectionStart = Convert.ToDateTime(_config.Settings.User.GetByKey("SelectedCalendarDay").Value);
                }

                // SFTP
                _formFileTransferSettings.checkBoxDeleteLocalFileAfterSuccessfulUpload.Checked = false;

                Setting sftpDeleteLocalFileAfterSuccessfulUploadSetting = _config.Settings.User.GetByKey("SFTPDeleteLocalFileAfterSuccessfulUpload");

                if (sftpDeleteLocalFileAfterSuccessfulUploadSetting != null)
                {
                    _formFileTransferSettings.checkBoxDeleteLocalFileAfterSuccessfulUpload.Checked = Convert.ToBoolean(_config.Settings.User.GetByKey("SFTPDeleteLocalFileAfterSuccessfulUpload").Value);
                }

                _formFileTransferSettings.checkBoxKeepFailedUploads.Checked = true;

                Setting sftpKeepFailingUploadsSetting = _config.Settings.User.GetByKey("SFTPKeepFailedUploads");

                if (sftpKeepFailingUploadsSetting != null)
                {
                    _formFileTransferSettings.checkBoxKeepFailedUploads.Checked = Convert.ToBoolean(_config.Settings.User.GetByKey("SFTPKeepFailedUploads").Value);
                }

                _log.WriteDebugMessage("Settings loaded");
            }
            catch (Exception ex)
            {
                _screenCapture.ApplicationError = true;
                _log.WriteExceptionMessage("FormMain-Settings::LoadSettings", ex);
            }
        }

        /// <summary>
        /// Saves the user's settings.
        /// </summary>
        private void SaveSettings()
        {
            try
            {
                _config.Settings.User.SetValueByKey("ScreenCaptureInterval", GetScreenCaptureInterval());
                _config.Settings.User.SetValueByKey("CaptureLimit", _formSetup.numericUpDownCaptureLimit.Value);
                _config.Settings.User.SetValueByKey("CaptureLimitCheck", _formSetup.checkBoxCaptureLimit.Checked);
                _config.Settings.User.SetValueByKey("TakeInitialScreenshot", _formSetup.checkBoxInitialScreenshot.Checked);

                // Optimize Screen Capture
                _config.Settings.User.SetValueByKey("OptimizeScreenCapture", _formSetup.checkBoxOptimizeScreenCapture.Checked);
                _config.Settings.User.SetValueByKey("ImageDiffTolerance", _formSetup.trackBarImageDiffTolerance.Value);

                // Labels
                _config.Settings.User.SetValueByKey("ApplyScreenshotLabel", _formSetup.checkBoxScreenshotLabel.Checked);

                // Active Window Title
                _config.Settings.User.SetValueByKey("ActiveWindowTitleCaptureCheck", _formSetup.checkBoxActiveWindowTitleComparisonCheck.Checked);
                _config.Settings.User.SetValueByKey("ActiveWindowTitleNoMatchCheck", _formSetup.checkBoxActiveWindowTitleComparisonCheckReverse.Checked);
                _config.Settings.User.SetValueByKey("ActiveWindowTitleCaptureText", _formSetup.textBoxActiveWindowTitle.Text.Trim());
                _config.Settings.User.SetValueByKey("ActiveWindowTitleSampleText", _formSetup.textBoxActiveWindowTitleTest.Text.Trim());

                if (_formSetup.radioButtonCaseSensitiveMatch.Checked)
                {
                    _config.Settings.User.SetValueByKey("ActiveWindowTitleMatchType", 1);
                }
                else if (_formSetup.radioButtonCaseInsensitiveMatch.Checked)
                {
                    _config.Settings.User.SetValueByKey("ActiveWindowTitleMatchType", 2);
                }
                else if (_formSetup.radioButtonRegularExpressionMatch.Checked)
                {
                    _config.Settings.User.SetValueByKey("ActiveWindowTitleMatchType", 3);
                }

                // Application Focus
                _config.Settings.User.SetValueByKey("ApplicationFocus", _formSetup.listBoxProcessList.Text);
                _config.Settings.User.SetValueByKey("ApplicationFocusDelayBefore", _formSetup.numericUpDownApplicationFocusDelayBefore.Value);
                _config.Settings.User.SetValueByKey("ApplicationFocusDelayAfter", _formSetup.numericUpDownApplicationFocusDelayAfter.Value);

                if (!_config.Settings.User.Save(_config.Settings, _fileSystem))
                {
                    _screenCapture.ApplicationError = true;
                }
            }
            catch (Exception ex)
            {
                _screenCapture.ApplicationError = true;
                _log.WriteExceptionMessage("FormMain-Settings::SaveSettings", ex);
            }
        }
    }
}
