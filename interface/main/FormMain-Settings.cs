//-----------------------------------------------------------------------
// <copyright file="FormMain-Settings.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2021 Gavin Kendall
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
                _log.WriteMessage("At this point the application should be able to run normally");
                _log.WriteMessage("but it would be a good idea to check what we found in your autoscreen.conf file");
                _log.WriteMessage("Your autoscreen.conf file is \"" + _fileSystem.ConfigFile + "\"");
                _log.WriteMessage("The name and location of it can be changed with the -config command line argument:");
                _log.WriteMessage("autoscreen.exe -config=C:\\MyAutoScreenCapture.conf");
                _log.WriteMessage("Checking what we loaded from your autoscreen.conf file ...");
                _log.WriteMessage("ApplicationSettingsFile=" + _fileSystem.ApplicationSettingsFile);
                _log.WriteMessage("UserSettingsFile=" + _fileSystem.UserSettingsFile);
                _log.WriteMessage("DebugFolder=" + _fileSystem.DebugFolder);
                _log.WriteMessage("LogsFolder=" + _fileSystem.LogsFolder);
                _log.WriteMessage("CommandFile=" + _fileSystem.CommandFile);
                _log.WriteMessage("ScreenshotsFolder=" + _fileSystem.ScreenshotsFolder);
                _log.WriteMessage("ScreenshotsFile=" + _fileSystem.ScreenshotsFile);
                _log.WriteMessage("TriggersFile=" + _fileSystem.TriggersFile);
                _log.WriteMessage("ScreensFile=" + _fileSystem.ScreensFile);
                _log.WriteMessage("RegionsFile=" + _fileSystem.RegionsFile);
                _log.WriteMessage("EditorsFile=" + _fileSystem.EditorsFile);
                _log.WriteMessage("TagsFile = " + _fileSystem.TagsFile);

                _log.WriteMessage("It looks like I successfully parsed your \"" + _fileSystem.ConfigFile + "\" file.");

                _log.WriteDebugMessage("Initializing screen capture");
                _screenCapture = new ScreenCapture(_config, _fileSystem, _log);

                _log.WriteDebugMessage("Initializing image format collection");
                _imageFormatCollection = new ImageFormatCollection();

                _log.WriteDebugMessage("Initializing forms");
                _formAbout = new FormAbout();
                _formHelp = new FormHelp();
                _formMacroTag = new FormMacroTag(_macroParser);
                _formRegion = new FormRegion(_screenCapture, _macroParser, _fileSystem, _log);
                _formScreen = new FormScreen(_screenCapture, _macroParser, _fileSystem, _log);
                _formEditor = new FormEditor(_config, _fileSystem);
                _formEmailSettings = new FormEmailSettings(_config, _fileSystem);
                _formFileTransferSettings = new FormFileTransferSettings(_config, _fileSystem);
                _formRegionSelectOptions = new FormRegionSelectOptions(_config, _fileSystem, _imageFormatCollection);
                _formSchedule = new FormSchedule();
                _formTrigger = new FormTrigger(_fileSystem);
                _formEnterPassphrase = new FormEnterPassphrase(_security, _config, _log);
                _formScreenCaptureStatus = new FormScreenCaptureStatus();
                _formSetup = new FormSetup(_security, _config, _fileSystem, _screenCapture);
                _formSetupWizard = new FormSetupWizard();

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

                // Preview
                _preview = Convert.ToBoolean(_config.Settings.User.GetByKey("Preview", _config.Settings.DefaultSettings.Preview).Value);
                _log.WriteDebugMessage("Preview = " + _preview.ToString());

                _dashboardGroupBoxSize = Convert.ToInt32(_config.Settings.User.GetByKey("DashboardGroupBoxSize", _config.Settings.DefaultSettings.DashboardGroupBoxSize).Value);
                _log.WriteDebugMessage("DashboardGroupBoxSize = " + _dashboardGroupBoxSize);
                _log.WriteDebugMessage("Building view tab pages");
                BuildViewTabPages();

                _log.WriteDebugMessage("Initializing screenshot collection");
                _screenshotCollection = new ScreenshotCollection(_imageFormatCollection, _formScreen.ScreenCollection, _screenCapture, _config, _fileSystem, _log);

                _screenshotCollection.LoadXmlFile(_config);

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

                _formSetup.numericUpDownHoursInterval.Value = screenCaptureIntervalHours;
                _formSetup.numericUpDownMinutesInterval.Value = screenCaptureIntervalMinutes;
                _formSetup.numericUpDownSecondsInterval.Value = screenCaptureIntervalSeconds;
                _formSetup.numericUpDownMillisecondsInterval.Value = screenCaptureIntervalMilliseconds;

                // Optimize Screen Capture
                _formSetup.checkBoxOptimizeScreenCapture.Checked = Convert.ToBoolean(_config.Settings.User.GetByKey("OptimizeScreenCapture", _config.Settings.DefaultSettings.OptimizeScreenCapture).Value);

                _formSetup.numericUpDownCaptureLimit.Value = Convert.ToInt32(_config.Settings.User.GetByKey("CaptureLimit", _config.Settings.DefaultSettings.CaptureLimit).Value);
                _log.WriteDebugMessage("CaptureLimit = " + _formSetup.numericUpDownCaptureLimit.Value);

                _formSetup.checkBoxCaptureLimit.Checked = Convert.ToBoolean(_config.Settings.User.GetByKey("CaptureLimitCheck", _config.Settings.DefaultSettings.CaptureLimitCheck).Value);
                _log.WriteDebugMessage("CaptureLimitCheck = " + _formSetup.checkBoxCaptureLimit.Checked);

                _formSetup.checkBoxInitialScreenshot.Checked = Convert.ToBoolean(_config.Settings.User.GetByKey("TakeInitialScreenshot", _config.Settings.DefaultSettings.TakeInitialScreenshot).Value);
                _log.WriteDebugMessage("TakeInitialScreenshot = " + _formSetup.checkBoxInitialScreenshot.Checked);

                _formSetup.checkBoxScreenshotLabel.Checked = Convert.ToBoolean(_config.Settings.User.GetByKey("ApplyScreenshotLabel", _config.Settings.DefaultSettings.ApplyScreenshotLabel).Value);

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
                PopulateLabelList();

                // Application Focus
                _formSetup.RefreshApplicationFocusList();

                EnableStartCapture();

                CaptureLimitCheck();

                // Set the tab page we want to look at. By default it's going to be index 0 for the "Dashboard" tab page.
                tabControlViews.SelectedIndex = Convert.ToInt32(_config.Settings.User.GetByKey("SelectedTabPageIndex", _config.Settings.DefaultSettings.SelectedTabPageIndex).Value);

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

                if (_formSetup.listBoxScreenshotLabel.SelectedItem != null)
                {
                    _config.Settings.User.GetByKey("ScreenshotLabel", _config.Settings.DefaultSettings.ScreenshotLabel).Value = _formSetup.listBoxScreenshotLabel.SelectedItem.ToString();
                }

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
