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

                _log.WriteMessage("*** Welcome to " + _config.Settings.ApplicationName + " " + _config.Settings.ApplicationVersion + " (\"" + _config.Settings.ApplicationCodename + "\") ***");
                _log.WriteMessage("Starting application");
                _log.WriteDebugMessage("At this point the application should be able to run normally");
                _log.WriteDebugMessage("but it would be a good idea to check what we found in your autoscreen.conf file");
                _log.WriteDebugMessage("Your autoscreen.conf file is \"" + _fileSystem.ConfigFile + "\"");
                _log.WriteDebugMessage("The name and location of it can be changed with the -config command line argument:");
                _log.WriteDebugMessage("autoscreen.exe -config=C:\\MyAutoScreenCapture.conf");
                _log.WriteDebugMessage("Checking what we loaded from your autoscreen.conf file ...");
                _log.WriteDebugMessage("ApplicationSettingsFile=" + _fileSystem.ApplicationSettingsFile);
                _log.WriteDebugMessage("UserSettingsFile=" + _fileSystem.UserSettingsFile);
                _log.WriteDebugMessage("ErrorsFolder=" + _fileSystem.ErrorsFolder);
                _log.WriteDebugMessage("LogsFolder=" + _fileSystem.LogsFolder);
                _log.WriteDebugMessage("CommandFile=" + _fileSystem.CommandFile);
                _log.WriteDebugMessage("ScreenshotsFolder=" + _fileSystem.ScreenshotsFolder);
                _log.WriteDebugMessage("ScreenshotsFile=" + _fileSystem.ScreenshotsFile);
                _log.WriteDebugMessage("TriggersFile=" + _fileSystem.TriggersFile);
                _log.WriteDebugMessage("ScreensFile=" + _fileSystem.ScreensFile);
                _log.WriteDebugMessage("RegionsFile=" + _fileSystem.RegionsFile);
                _log.WriteDebugMessage("EditorsFile=" + _fileSystem.EditorsFile);
                _log.WriteDebugMessage("MacroTagsFile=" + _fileSystem.MacroTagsFile);
                _log.WriteDebugMessage("FilenamePattern=" + _fileSystem.FilenamePattern);
                _log.WriteDebugMessage("ImageFormat=" + ScreenCapture.ImageFormat);

                _log.WriteDebugMessage("It looks like the application successfully parsed your \"" + _fileSystem.ConfigFile + "\" file");

                _log.WriteDebugMessage("Checking for a user-defined screenshots folder path in user settings and if it differs from the path set by autoscreen.conf");

                // Let's see if the user has their own screenshots folder they would like to use. Check user settings.
                // If they have defined a screenshots folder in user settings then use that folder path otherwise keep using the path from the config file.
                Setting screenshotsFolderDefinedByUser = _config.Settings.User.GetByKey("ScreenshotsFolder", _fileSystem.ScreenshotsFolder, createKeyIfNotFound: false);

                // Set the screenshots folder path to be the user-defined screenshots folder path if it's different from the path used by the configuration file.
                if (screenshotsFolderDefinedByUser != null && !screenshotsFolderDefinedByUser.Value.ToString().Equals(_fileSystem.ScreenshotsFolder))
                {
                    _fileSystem.ScreenshotsFolder = screenshotsFolderDefinedByUser.Value.ToString();

                    _log.WriteDebugMessage("WARNING! A user-defined screenshots folder path was found in user settings and this path is different from what was set in the autoscreen.conf file so the application will be using this path instead!");
                    _log.WriteDebugMessage("The screenshots folder path that will be used is \"" + _fileSystem.ScreenshotsFolder + "\"");
                }
                else
                {
                    _log.WriteDebugMessage("No difference in folder path found. Screenshots folder path from autoscreen.conf file will be used (\"" + _fileSystem.ScreenshotsFolder + "\")");
                }

                // Let's see if the user has their own filename pattern they would like to use. Check user settings.
                // If they have defined a filename pattern in user settings then use that filename pattern otherwise keep using the filename pattern from the config file.
                Setting filenamePatternDefinedByUser = _config.Settings.User.GetByKey("FilenamePattern", _fileSystem.FilenamePattern, createKeyIfNotFound: false);

                if (filenamePatternDefinedByUser != null && !filenamePatternDefinedByUser.Value.ToString().Equals(_fileSystem.FilenamePattern))
                {
                    _fileSystem.FilenamePattern = filenamePatternDefinedByUser.Value.ToString();

                    _log.WriteDebugMessage("WARNING! A user-defined filename pattern was found in user settings and it is different from what was set in the autoscreen.conf file so the application will be using the user-defined filename pattern instead!");
                    _log.WriteDebugMessage("The filename pattern that will be used is \"" + _fileSystem.FilenamePattern + "\"");
                }
                else
                {
                    _log.WriteDebugMessage("No difference in filename pattern found. Filename pattern in autoscreen.conf file will be used (\"" + _fileSystem.FilenamePattern + "\")");
                }

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

                // Command Deck Event Handlers
                _formCommandDeck.Clipboard += toolStripMenuItemRegionSelectClipboard_Click;
                _formCommandDeck.ClipboardAutoSave += toolStripMenuItemRegionSelectClipboardAutoSave_Click;
                _formCommandDeck.ClipboardAutoSaveEdit += toolStripMenuItemRegionSelectClipboardAutoSaveEdit_Click;
                _formCommandDeck.ClipboardFloatingScreenshot += toolStripMenuItemRegionSelectClipboardFloatingScreenshot_Click;
                _formCommandDeck.FloatingScreenshot += toolStripMenuItemRegionSelectFloatingScreenshot_Click;
                _formCommandDeck.AddRegion += toolStripMenuItemRegionSelectAddRegion_Click;

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

                // Capture Now / Archive Tool Tip
                ToolTip toolTipButtonCaptureNowCommandDeck = new ToolTip();
                toolTipButtonCaptureNowCommandDeck.SetToolTip(_formCommandDeck.buttonCaptureNow, "Capture Now / Archive");

                // Capture Now / Edit Tool Tip
                ToolTip toolTipButtonCaptureNowEditCommandDeck = new ToolTip();
                toolTipButtonCaptureNowEditCommandDeck.SetToolTip(_formCommandDeck.buttonCaptureNowEdit, "Capture Now / Edit");

                // Exit for Command Deck
                _formCommandDeck.buttonExit.Click += toolStripMenuItemExit_Click;

                // Exit Tool Tip
                ToolTip toolTipButtonExit = new ToolTip();
                toolTipButtonExit.SetToolTip(_formCommandDeck.buttonExit, "Exit");

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

                // Set the tab page we want to look at. By default it's going to be index 0 for the "Dashboard" tab page.
                tabControlViews.SelectedIndex = Convert.ToInt32(_config.Settings.User.GetByKey("SelectedTabPageIndex").Value);

                if (tabControlViews.SelectedIndex < 0)
                {
                    tabControlViews.SelectedIndex = 0;
                }

                // Set the module we want to look at.
                tabControlModules.SelectedIndex = Convert.ToInt32(_config.Settings.User.GetByKey("SelectedModuleIndex").Value);

                // Preview
                _preview = Convert.ToBoolean(_config.Settings.User.GetByKey("Preview").Value);
                _log.WriteDebugMessage("Preview = " + _preview.ToString());

                _dashboardGroupBoxSize = Convert.ToInt32(_config.Settings.User.GetByKey("DashboardGroupBoxSize").Value);
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

                int screenCaptureInterval = Convert.ToInt32(_config.Settings.User.GetByKey("ScreenCaptureInterval").Value);
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

                // Setup
                _formSetup = new FormSetup(_log, _security, _config, _fileSystem, _screenCapture, _formScreenCaptureStatusWithLabelSwitcher, _formScreen, _formRegion, _formMacroTag.MacroTagCollection, _macroParser, _screenshotCollection);

                _formSetup.numericUpDownHoursInterval.Value = screenCaptureIntervalHours;
                _formSetup.numericUpDownMinutesInterval.Value = screenCaptureIntervalMinutes;
                _formSetup.numericUpDownSecondsInterval.Value = screenCaptureIntervalSeconds;

                // Optimize Screen Capture
                _formSetup.checkBoxOptimizeScreenCapture.Checked = Convert.ToBoolean(_config.Settings.User.GetByKey("OptimizeScreenCapture").Value);
                _formSetup.trackBarImageDiffTolerance.Value = Convert.ToInt32(_config.Settings.User.GetByKey("ImageDiffTolerance").Value);

                _formSetup.numericUpDownCaptureLimit.Value = Convert.ToInt32(_config.Settings.User.GetByKey("CaptureLimit").Value);
                _log.WriteDebugMessage("CaptureLimit = " + _formSetup.numericUpDownCaptureLimit.Value);

                _formSetup.checkBoxCaptureLimit.Checked = Convert.ToBoolean(_config.Settings.User.GetByKey("CaptureLimitCheck").Value);
                _log.WriteDebugMessage("CaptureLimitCheck = " + _formSetup.checkBoxCaptureLimit.Checked);

                _formSetup.checkBoxInitialScreenshot.Checked = Convert.ToBoolean(_config.Settings.User.GetByKey("TakeInitialScreenshot").Value);
                _log.WriteDebugMessage("TakeInitialScreenshot = " + _formSetup.checkBoxInitialScreenshot.Checked);

                // Active Window Title
                _formSetup.checkBoxActiveWindowTitleComparisonCheck.Checked = Convert.ToBoolean(_config.Settings.User.GetByKey("ActiveWindowTitleCaptureCheck").Value);
                _formSetup.checkBoxActiveWindowTitleComparisonCheckReverse.Checked = Convert.ToBoolean(_config.Settings.User.GetByKey("ActiveWindowTitleNoMatchCheck").Value);
                _formSetup.textBoxActiveWindowTitle.Text = _config.Settings.User.GetByKey("ActiveWindowTitleCaptureText").Value.ToString();
                _formSetup.textBoxActiveWindowTitleTest.Text = _config.Settings.User.GetByKey("ActiveWindowTitleSampleText").Value.ToString();

                int activeWindowTitleMatchType = Convert.ToInt32(_config.Settings.User.GetByKey("ActiveWindowTitleMatchType").Value);

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
                _formSetup.checkBoxScreenshotLabel.Checked = Convert.ToBoolean(_config.Settings.User.GetByKey("ApplyScreenshotLabel").Value);
                PopulateLabelList();

                // Application Focus
                _formSetup.RefreshApplicationFocusList();

                EnableStartCapture();

                CaptureLimitCheck();

                // Set the filter type to be the last selected index that was saved (and if no setting is found just set it to selected index 0).
                // IMPORTANT: Do not attempt to set the same index for Filter Value here as that control is populated based on the selected index of Filter Type.
                comboBoxFilterType.SelectedIndex = Convert.ToInt32(_config.Settings.User.GetByKey("FilterType").Value);

                monthCalendar.SelectionStart = Convert.ToDateTime(_config.Settings.User.GetByKey("SelectedCalendarDay").Value);

                // SFTP
                _formFileTransferSettings.checkBoxDeleteLocalFileAfterSuccessfulUpload.Checked = Convert.ToBoolean(_config.Settings.User.GetByKey("SFTPDeleteLocalFileAfterSuccessfulUpload").Value);
                _formFileTransferSettings.checkBoxKeepFailedUploads.Checked = Convert.ToBoolean(_config.Settings.User.GetByKey("SFTPKeepFailedUploads").Value);

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
