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
                _macroParser = new MacroParser(_config.Settings);

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
                _log.WriteDebugMessage("DebugFolder=" + _fileSystem.DebugFolder);
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
                _formLabelSwitcher = new FormLabelSwitcher(_config, _fileSystem);
                _formMacroTag = new FormMacroTag(_macroParser);
                _formRegion = new FormRegion(_screenCapture, _macroParser, _fileSystem, _config, _log);
                _formScreen = new FormScreen(_screenCapture, _macroParser, _fileSystem, _config, _log);
                _formEditor = new FormEditor(_config, _fileSystem);
                _formEmailSettings = new FormEmailSettings(_config, _fileSystem, _log);
                _formFileTransferSettings = new FormFileTransferSettings(_config, _fileSystem, _log);
                _formRegionSelectOptions = new FormRegionSelectOptions(_config, _fileSystem, _imageFormatCollection);
                _formSchedule = new FormSchedule(_formScreen, _formRegion);
                _formTrigger = new FormTrigger(_fileSystem);
                _formEnterPassphrase = new FormEnterPassphrase(_security, _config, _log);
                _formScreenCaptureStatus = new FormScreenCaptureStatus();

                _formLabelSwitcher.buttonStartStopScreenCapture.Click += _formLabelSwitcher_buttonStartStopScreenCapture_Click;

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

                // Set the tab page we want to look at. By default it's going to be index 0 for the "Dashboard" tab page.
                tabControlViews.SelectedIndex = Convert.ToInt32(_config.Settings.User.GetByKey("SelectedTabPageIndex", _config.Settings.DefaultSettings.SelectedTabPageIndex).Value);

                if (tabControlViews.SelectedIndex < 0)
                {
                    tabControlViews.SelectedIndex = 0;
                }

                // Set the module we want to look at.
                tabControlModules.SelectedIndex = Convert.ToInt32(_config.Settings.User.GetByKey("SelectedModuleIndex", _config.Settings.DefaultSettings.SelectedModuleIndex).Value);

                // Preview
                _preview = Convert.ToBoolean(_config.Settings.User.GetByKey("Preview", _config.Settings.DefaultSettings.Preview).Value);
                _log.WriteDebugMessage("Preview = " + _preview.ToString());

                _dashboardGroupBoxSize = Convert.ToInt32(_config.Settings.User.GetByKey("DashboardGroupBoxSize", _config.Settings.DefaultSettings.DashboardGroupBoxSize).Value);
                _log.WriteDebugMessage("DashboardGroupBoxSize = " + _dashboardGroupBoxSize);
                _log.WriteDebugMessage("Building view tab pages");
                BuildViewTabPages();

                // Screenshots Collection
                _log.WriteDebugMessage("Initializing screenshot collection");
                _screenshotCollection = new ScreenshotCollection(_imageFormatCollection, _formScreen.ScreenCollection, _screenCapture, _config, _fileSystem, _log);
                _screenshotCollection.LoadXmlFile(_config);

                // Encryptor / Decryptor
                _formEncryptorDecryptor = new FormEncryptorDecryptor(_log, _config, _security, _fileSystem, _screenshotCollection);
                _formEncryptorDecryptor.screenshotsEncrypted += ScreenshotsEncrypted;
                _formEncryptorDecryptor.screenshotsDecrypted += ScreenshotsDecrypted;

                // Region Select Command Deck
                _formRegionSelectCommandDeck = new FormRegionSelectCommandDeck();

                int screenCaptureInterval = Convert.ToInt32(_config.Settings.User.GetByKey("ScreenCaptureInterval", _config.Settings.DefaultSettings.ScreenCaptureInterval).Value);
                _log.WriteDebugMessage("ScreenCaptureInterval = " + screenCaptureInterval);

                if (screenCaptureInterval == 0)
                {
                    screenCaptureInterval = _config.Settings.DefaultSettings.ScreenCaptureInterval;
                    _log.WriteDebugMessage("WARNING: Screen capture interval was found to be 0 so 60,000 milliseconds (or 1 minute) is being used as the default value");
                }

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

                // Setup
                _formSetup = new FormSetup(_log, _security, _config, _fileSystem, _screenCapture, _formLabelSwitcher, _formScreen, _formRegion, _formMacroTag.MacroTagCollection, _macroParser, _screenshotCollection);

                _formSetup.numericUpDownHoursInterval.Value = screenCaptureIntervalHours;
                _formSetup.numericUpDownMinutesInterval.Value = screenCaptureIntervalMinutes;
                _formSetup.numericUpDownSecondsInterval.Value = screenCaptureIntervalSeconds;
                _formSetup.numericUpDownMillisecondsInterval.Value = screenCaptureIntervalMilliseconds;

                // Optimize Screen Capture
                _formSetup.checkBoxOptimizeScreenCapture.Checked = Convert.ToBoolean(_config.Settings.User.GetByKey("OptimizeScreenCapture", _config.Settings.DefaultSettings.OptimizeScreenCapture).Value);
                _formSetup.radioButtonCompareWithAnyPreviousImage.Checked = Convert.ToBoolean(_config.Settings.User.GetByKey("CompareWithAnyPreviousImage", _config.Settings.DefaultSettings.CompareWithAnyPreviousImage).Value);
                _formSetup.radioButtonCompareWithLastImage.Checked = Convert.ToBoolean(_config.Settings.User.GetByKey("CompareWithLastImage", _config.Settings.DefaultSettings.CompareWithLastImage).Value);

                _formSetup.numericUpDownCaptureLimit.Value = Convert.ToInt32(_config.Settings.User.GetByKey("CaptureLimit", _config.Settings.DefaultSettings.CaptureLimit).Value);
                _log.WriteDebugMessage("CaptureLimit = " + _formSetup.numericUpDownCaptureLimit.Value);

                _formSetup.checkBoxCaptureLimit.Checked = Convert.ToBoolean(_config.Settings.User.GetByKey("CaptureLimitCheck", _config.Settings.DefaultSettings.CaptureLimitCheck).Value);
                _log.WriteDebugMessage("CaptureLimitCheck = " + _formSetup.checkBoxCaptureLimit.Checked);

                _formSetup.checkBoxInitialScreenshot.Checked = Convert.ToBoolean(_config.Settings.User.GetByKey("TakeInitialScreenshot", _config.Settings.DefaultSettings.TakeInitialScreenshot).Value);
                _log.WriteDebugMessage("TakeInitialScreenshot = " + _formSetup.checkBoxInitialScreenshot.Checked);

                // Active Window Title
                _formSetup.checkBoxActiveWindowTitleComparisonCheck.Checked = Convert.ToBoolean(_config.Settings.User.GetByKey("ActiveWindowTitleCaptureCheck", _config.Settings.DefaultSettings.ActiveWindowTitleCaptureCheck).Value);
                _formSetup.checkBoxActiveWindowTitleComparisonCheckReverse.Checked = Convert.ToBoolean(_config.Settings.User.GetByKey("ActiveWindowTitleNoMatchCheck", _config.Settings.DefaultSettings.ActiveWindowTitleNoMatchCheck).Value);
                _formSetup.textBoxActiveWindowTitle.Text = _config.Settings.User.GetByKey("ActiveWindowTitleCaptureText", _config.Settings.DefaultSettings.ActiveWindowTitleCaptureText).Value.ToString();
                _formSetup.textBoxActiveWindowTitleTest.Text = _config.Settings.User.GetByKey("ActiveWindowTitleSampleText", _config.Settings.DefaultSettings.ActiveWindowTitleSampleText).Value.ToString();

                int activeWindowTitleMatchType = Convert.ToInt32(_config.Settings.User.GetByKey("ActiveWindowTitleMatchType", _config.Settings.DefaultSettings.ActiveWindowTitleMatchType).Value);

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
                _formSetup.checkBoxScreenshotLabel.Checked = Convert.ToBoolean(_config.Settings.User.GetByKey("ApplyScreenshotLabel", _config.Settings.DefaultSettings.ApplyScreenshotLabel).Value);
                PopulateLabelList();

                // Application Focus
                _formSetup.RefreshApplicationFocusList();

                EnableStartCapture();

                CaptureLimitCheck();

                // Set the filter type to be the last selected index that was saved (and if no setting is found just set it to selected index 0).
                // IMPORTANT: Do not attempt to set the same index for Filter Value here as that control is populated based on the selected index of Filter Type.
                comboBoxFilterType.SelectedIndex = Convert.ToInt32(_config.Settings.User.GetByKey("FilterType", 0).Value);

                monthCalendar.SelectionStart = Convert.ToDateTime(_config.Settings.User.GetByKey("SelectedCalendarDay", DateTime.Now).Value);

                // SFTP
                _formFileTransferSettings.checkBoxDeleteLocalFileAfterSuccessfulUpload.Checked = Convert.ToBoolean(_config.Settings.User.GetByKey("SFTPDeleteLocalFileAfterSuccessfulUpload", _config.Settings.DefaultSettings.SFTPDeleteLocalFileAfterSuccessfulUpload).Value);
                _formFileTransferSettings.checkBoxKeepFailedUploads.Checked = Convert.ToBoolean(_config.Settings.User.GetByKey("SFTPKeepFailedUploads", _config.Settings.DefaultSettings.SFTPKeepFailedUploads).Value);

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
                _config.Settings.User.GetByKey("ScreenCaptureInterval", _config.Settings.DefaultSettings.ScreenCaptureInterval).Value = GetScreenCaptureInterval();
                _config.Settings.User.GetByKey("CaptureLimit", _config.Settings.DefaultSettings.CaptureLimit).Value = _formSetup.numericUpDownCaptureLimit.Value;
                _config.Settings.User.GetByKey("CaptureLimitCheck", _config.Settings.DefaultSettings.CaptureLimitCheck).Value = _formSetup.checkBoxCaptureLimit.Checked;
                _config.Settings.User.GetByKey("TakeInitialScreenshot", _config.Settings.DefaultSettings.TakeInitialScreenshot).Value = _formSetup.checkBoxInitialScreenshot.Checked;

                _config.Settings.User.GetByKey("ApplyScreenshotLabel", _config.Settings.DefaultSettings.ApplyScreenshotLabel).Value = _formSetup.checkBoxScreenshotLabel.Checked;

                // Active Window Title
                _config.Settings.User.GetByKey("ActiveWindowTitleCaptureCheck", _config.Settings.DefaultSettings.ActiveWindowTitleCaptureCheck).Value = _formSetup.checkBoxActiveWindowTitleComparisonCheck.Checked;
                _config.Settings.User.GetByKey("ActiveWindowTitleNoMatchCheck", _config.Settings.DefaultSettings.ActiveWindowTitleNoMatchCheck).Value = _formSetup.checkBoxActiveWindowTitleComparisonCheckReverse.Checked;
                _config.Settings.User.GetByKey("ActiveWindowTitleCaptureText", _config.Settings.DefaultSettings.ActiveWindowTitleCaptureText).Value = _formSetup.textBoxActiveWindowTitle.Text.Trim();
                _config.Settings.User.GetByKey("ActiveWindowTitleSampleText", _config.Settings.DefaultSettings.ActiveWindowTitleSampleText).Value = _formSetup.textBoxActiveWindowTitleTest.Text.Trim();

                if (_formSetup.radioButtonCaseSensitiveMatch.Checked)
                {
                    _config.Settings.User.GetByKey("ActiveWindowTitleMatchType", _config.Settings.DefaultSettings.ActiveWindowTitleMatchType).Value = 1;
                }
                else if (_formSetup.radioButtonCaseInsensitiveMatch.Checked)
                {
                    _config.Settings.User.GetByKey("ActiveWindowTitleMatchType", _config.Settings.DefaultSettings.ActiveWindowTitleMatchType).Value = 2;
                }
                else if (_formSetup.radioButtonRegularExpressionMatch.Checked)
                {
                    _config.Settings.User.GetByKey("ActiveWindowTitleMatchType", _config.Settings.DefaultSettings.ActiveWindowTitleMatchType).Value = 3;
                }

                // Application Focus
                _config.Settings.User.GetByKey("ApplicationFocus", _config.Settings.DefaultSettings.ApplicationFocus).Value = _formSetup.listBoxProcessList.Text;
                _config.Settings.User.GetByKey("ApplicationFocusDelayBefore", _config.Settings.DefaultSettings.ApplicationFocusDelayBefore).Value = (int)_formSetup.numericUpDownApplicationFocusDelayBefore.Value;
                _config.Settings.User.GetByKey("ApplicationFocusDelayAfter", _config.Settings.DefaultSettings.ApplicationFocusDelayAfter).Value = (int)_formSetup.numericUpDownApplicationFocusDelayAfter.Value;

                // Optimize Screen Capture
                _config.Settings.User.GetByKey("OptimizeScreenCapture", _config.Settings.DefaultSettings.OptimizeScreenCapture).Value = _formSetup.checkBoxOptimizeScreenCapture.Checked;
                _config.Settings.User.GetByKey("CompareWithAnyPreviousImage", _config.Settings.DefaultSettings.CompareWithAnyPreviousImage).Value = _formSetup.radioButtonCompareWithAnyPreviousImage.Checked;
                _config.Settings.User.GetByKey("CompareWithLastImage", _config.Settings.DefaultSettings.CompareWithLastImage).Value = _formSetup.radioButtonCompareWithLastImage.Checked;

                // SFTP
                _config.Settings.User.GetByKey("SFTPDeleteLocalFileAfterSuccessfulUpload", _config.Settings.DefaultSettings.SFTPDeleteLocalFileAfterSuccessfulUpload).Value = _formFileTransferSettings.checkBoxDeleteLocalFileAfterSuccessfulUpload.Checked;
                _config.Settings.User.GetByKey("SFTPKeepFailedUploads", _config.Settings.DefaultSettings.SFTPKeepFailedUploads).Value = _formFileTransferSettings.checkBoxKeepFailedUploads.Checked;

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
