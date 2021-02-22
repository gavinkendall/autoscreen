//-----------------------------------------------------------------------
// <copyright file="FormMain-Settings.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
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
using System.Diagnostics;
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
                _log.WriteDebugMessage("TagsFile = " + _fileSystem.TagsFile);

                _log.WriteDebugMessage("It looks like I successfully parsed your \"" + _fileSystem.ConfigFile + "\" file.");
                _log.WriteDebugMessage("I'm now going to attempt to load your personal settings and any screenshots you have taken.");

                _log.WriteMessage("Initializing screen capture");
                _screenCapture = new ScreenCapture(_config, _macroParser, _fileSystem, _log);

                _log.WriteMessage("Initializing forms");
                _formAbout = new FormAbout();
                _formHelp = new FormHelp();
                _formMacroTag = new FormMacroTag(_macroParser);
                _formRegion = new FormRegion(_screenCapture, _macroParser, _fileSystem, _log);
                _formScreen = new FormScreen(_screenCapture, _macroParser, _fileSystem, _log);
                _formEditor = new FormEditor(_config, _fileSystem, _log);
                _formEmailSettings = new FormEmailSettings(_config, _fileSystem, _log);
                _formFileTransferSettings = new FormFileTransferSettings(_config, _fileSystem, _log);
                _formSchedule = new FormSchedule();
                _formTrigger = new FormTrigger(_fileSystem);
                _formEnterPassphrase = new FormEnterPassphrase(_screenCapture, _config, _log);
                _formScreenCaptureStatus = new FormScreenCaptureStatus();

                _log.WriteMessage("Initializing image format collection");
                _imageFormatCollection = new ImageFormatCollection();

                _log.WriteMessage("Initializing editor collection");
                
                if (!_formEditor.EditorCollection.LoadXmlFileAndAddEditors(_config, _fileSystem, _log))
                {
                    _screenCapture.ApplicationError = true;
                }

                _log.WriteDebugMessage("Number of editors loaded = " + _formEditor.EditorCollection.Count);

                _log.WriteMessage("Initializing trigger collection");

                if (!_formTrigger.TriggerCollection.LoadXmlFileAndAddTriggers(_config, _fileSystem, _log))
                {
                    _screenCapture.ApplicationError = true;
                }

                _log.WriteDebugMessage("Number of triggers loaded = " + _formTrigger.TriggerCollection.Count);

                _log.WriteMessage("Initializing region collection");

                if (!_formRegion.RegionCollection.LoadXmlFileAndAddRegions(_imageFormatCollection, _config, _fileSystem, _log))
                {
                    _screenCapture.ApplicationError = true;
                }

                // Add regions to the Screen form so we can select them as a source.
                _formScreen.RegionCollection = _formRegion.RegionCollection;
                _log.WriteDebugMessage("Number of regions loaded = " + _formRegion.RegionCollection.Count);

                _log.WriteMessage("Initializing screen collection");

                if (!_formScreen.ScreenCollection.LoadXmlFileAndAddScreens(_imageFormatCollection, _config, _macroParser, _screenCapture, _fileSystem, _log))
                {
                    _screenCapture.ApplicationError = true;
                }

                _log.WriteDebugMessage("Number of screens loaded = " + _formScreen.ScreenCollection.Count);

                _log.WriteMessage("Initializing tag collection");

                if (!_formMacroTag.MacroTagCollection.LoadXmlFileAndAddTags(_config, _macroParser, _fileSystem, _log))
                {
                    _screenCapture.ApplicationError = true;
                }

                _log.WriteDebugMessage("Number of tags loaded = " + _formMacroTag.MacroTagCollection.Count);

                _log.WriteMessage("Initializing schedule collection");

                if (!_formSchedule.ScheduleCollection.LoadXmlFileAndAddSchedules(_config, _fileSystem, _log))
                {
                    _screenCapture.ApplicationError = true;
                }

                _log.WriteDebugMessage("Number of schedules loaded = " + _formSchedule.ScheduleCollection.Count);

                _log.WriteMessage("Building screens module");
                BuildScreensModule();

                _log.WriteMessage("Building editors module");
                BuildEditorsModule();

                _log.WriteMessage("Building triggers module");
                BuildTriggersModule();

                _log.WriteMessage("Building regions module");
                BuildRegionsModule();

                _log.WriteMessage("Building tags module");
                BuildMacroTagsModule();

                _log.WriteMessage("Building schedules module");
                BuildSchedulesModule();

                _log.WriteMessage("Building screenshot preview context menu");
                BuildScreenshotPreviewContextualMenu();

                _log.WriteMessage("Building view tab pages");
                BuildViewTabPages();

                _log.WriteMessage("Initializing screenshot collection");
                _screenshotCollection = new ScreenshotCollection(_imageFormatCollection, _formScreen.ScreenCollection, _screenCapture, _config, _fileSystem, _log);

                _screenshotCollection.LoadXmlFile(_config);

                int screenCaptureInterval = Convert.ToInt32(_config.Settings.User.GetByKey("ScreenCaptureInterval", _config.Settings.DefaultSettings.ScreenCaptureInterval).Value);
                _log.WriteDebugMessage("ScreenCaptureInterval = " + screenCaptureInterval);

                if (screenCaptureInterval == 0)
                {
                    screenCaptureInterval = _config.Settings.DefaultSettings.ScreenCaptureInterval;
                    _log.WriteDebugMessage("WARNING: Screen capture interval was found to be 0 so 60,000 milliseconds (or 1 minute) is being used as the default value");
                }

                _log.WriteDebugMessage("Assigning screen capture interval value to its appropriate hour, minute, second, and millisecond variables");

                decimal screenCaptureIntervalHours = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(screenCaptureInterval)).Hours);
                _log.WriteDebugMessage("Hours = " + screenCaptureIntervalHours);

                decimal screenCaptureIntervalMinutes = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(screenCaptureInterval)).Minutes);
                _log.WriteDebugMessage("Minutes = " + screenCaptureIntervalMinutes);

                decimal screenCaptureIntervalSeconds = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(screenCaptureInterval)).Seconds);
                _log.WriteDebugMessage("Seconds = " + screenCaptureIntervalSeconds);

                decimal screenCaptureIntervalMilliseconds = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(screenCaptureInterval)).Milliseconds);
                _log.WriteDebugMessage("Milliseconds = " + screenCaptureIntervalMilliseconds);

                numericUpDownHoursInterval.Value = screenCaptureIntervalHours;
                numericUpDownMinutesInterval.Value = screenCaptureIntervalMinutes;
                numericUpDownSecondsInterval.Value = screenCaptureIntervalSeconds;
                numericUpDownMillisecondsInterval.Value = screenCaptureIntervalMilliseconds;

                numericUpDownCaptureLimit.Value = Convert.ToInt32(_config.Settings.User.GetByKey("CaptureLimit", _config.Settings.DefaultSettings.CaptureLimit).Value);
                _log.WriteDebugMessage("CaptureLimit = " + numericUpDownCaptureLimit.Value);

                checkBoxCaptureLimit.Checked = Convert.ToBoolean(_config.Settings.User.GetByKey("CaptureLimitCheck", _config.Settings.DefaultSettings.CaptureLimitCheck).Value);
                _log.WriteDebugMessage("CaptureLimitCheck = " + checkBoxCaptureLimit.Checked);

                checkBoxInitialScreenshot.Checked = Convert.ToBoolean(_config.Settings.User.GetByKey("TakeInitialScreenshot", _config.Settings.DefaultSettings.TakeInitialScreenshot).Value);
                _log.WriteDebugMessage("TakeInitialScreenshot = " + checkBoxInitialScreenshot.Checked);

                notifyIcon.Visible = Convert.ToBoolean(_config.Settings.User.GetByKey("ShowSystemTrayIcon", _config.Settings.DefaultSettings.ShowSystemTrayIcon).Value);
                _log.WriteDebugMessage("ShowSystemTrayIcon = " + notifyIcon.Visible);

                comboBoxScreenshotLabel.Text = _config.Settings.User.GetByKey("ScreenshotLabel", _config.Settings.DefaultSettings.ScreenshotLabel).Value.ToString();
                _log.WriteDebugMessage("ScreenshotLabel = " + comboBoxScreenshotLabel.Text);

                checkBoxScreenshotLabel.Checked = Convert.ToBoolean(_config.Settings.User.GetByKey("ApplyScreenshotLabel", _config.Settings.DefaultSettings.ApplyScreenshotLabel).Value);

                // Active Window Title
                checkBoxActiveWindowTitle.Checked = Convert.ToBoolean(_config.Settings.User.GetByKey("ActiveWindowTitleCaptureCheck", _config.Settings.DefaultSettings.ActiveWindowTitleCaptureCheck).Value);
                textBoxActiveWindowTitle.Text = _config.Settings.User.GetByKey("ActiveWindowTitleCaptureText", _config.Settings.DefaultSettings.ActiveWindowTitleCaptureText).Value.ToString();

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

                radioButtonCaseSensitiveMatch.Checked = false;
                radioButtonCaseInsensitiveMatch.Checked = false;
                radioButtonRegularExpressionMatch.Checked = false;

                int activeWindowTitleMatchType = Convert.ToInt32(_config.Settings.User.GetByKey("ActiveWindowTitleMatchType", _config.Settings.DefaultSettings.ActiveWindowTitleMatchType).Value);

                switch (activeWindowTitleMatchType)
                {
                    case 1:
                        radioButtonCaseSensitiveMatch.Checked = true;
                        break;

                    case 2:
                        radioButtonCaseInsensitiveMatch.Checked = true;
                        break;

                    case 3:
                        radioButtonRegularExpressionMatch.Checked = true;
                        break;
                }


                // Application Focus
                RefreshApplicationFocusList();

                // Region Select / Auto Save
                textBoxAutoSaveFolder.Text = _config.Settings.User.GetByKey("AutoSaveFolder", _config.Settings.DefaultSettings.AutoSaveFolder).Value.ToString();
                textBoxAutoSaveMacro.Text = _config.Settings.User.GetByKey("AutoSaveMacro", _config.Settings.DefaultSettings.AutoSaveMacro).Value.ToString();

                EnableStartCapture();

                CaptureLimitCheck();

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
                _config.Settings.User.GetByKey("CaptureLimit", _config.Settings.DefaultSettings.CaptureLimit).Value = numericUpDownCaptureLimit.Value;
                _config.Settings.User.GetByKey("CaptureLimitCheck", _config.Settings.DefaultSettings.CaptureLimitCheck).Value = checkBoxCaptureLimit.Checked;
                _config.Settings.User.GetByKey("TakeInitialScreenshot", _config.Settings.DefaultSettings.TakeInitialScreenshot).Value = checkBoxInitialScreenshot.Checked;

                // Label.
                _config.Settings.User.GetByKey("ScreenshotLabel", _config.Settings.DefaultSettings.ScreenshotLabel).Value = comboBoxScreenshotLabel.Text.Trim();
                _config.Settings.User.GetByKey("ApplyScreenshotLabel", _config.Settings.DefaultSettings.ApplyScreenshotLabel).Value = checkBoxScreenshotLabel.Checked;

                // Active Window Title
                _config.Settings.User.GetByKey("ActiveWindowTitleCaptureCheck", _config.Settings.DefaultSettings.ActiveWindowTitleCaptureCheck).Value = checkBoxActiveWindowTitle.Checked;
                _config.Settings.User.GetByKey("ActiveWindowTitleCaptureText", _config.Settings.DefaultSettings.ActiveWindowTitleCaptureText).Value = textBoxActiveWindowTitle.Text.Trim();

                if (radioButtonCaseSensitiveMatch.Checked)
                {
                    _config.Settings.User.GetByKey("ActiveWindowTitleMatchType", _config.Settings.DefaultSettings.ActiveWindowTitleMatchType).Value = 1;
                }
                else if (radioButtonCaseInsensitiveMatch.Checked)
                {
                    _config.Settings.User.GetByKey("ActiveWindowTitleMatchType", _config.Settings.DefaultSettings.ActiveWindowTitleMatchType).Value = 2;
                }
                else if (radioButtonRegularExpressionMatch.Checked)
                {
                    _config.Settings.User.GetByKey("ActiveWindowTitleMatchType", _config.Settings.DefaultSettings.ActiveWindowTitleMatchType).Value = 3;
                }

                // Application Focus
                _config.Settings.User.GetByKey("ApplicationFocus", _config.Settings.DefaultSettings.ApplicationFocus).Value = comboBoxProcessList.Text;
                _config.Settings.User.GetByKey("ApplicationFocusDelayBefore", _config.Settings.DefaultSettings.ApplicationFocusDelayBefore).Value = (int)numericUpDownApplicationFocusDelayBefore.Value;
                _config.Settings.User.GetByKey("ApplicationFocusDelayAfter", _config.Settings.DefaultSettings.ApplicationFocusDelayAfter).Value = (int)numericUpDownApplicationFocusDelayAfter.Value;

                // Region Select / Auto Save.
                _config.Settings.User.GetByKey("AutoSaveFolder", _config.Settings.DefaultSettings.AutoSaveFolder).Value = textBoxAutoSaveFolder.Text.Trim();
                _config.Settings.User.GetByKey("AutoSaveMacro", _config.Settings.DefaultSettings.AutoSaveMacro).Value = textBoxAutoSaveMacro.Text.Trim();

                if (!_config.Settings.User.Save(_config.Settings, _fileSystem, _log))
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

        /// <summary>
        /// Saves the user's settings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveSettings(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void RefreshApplicationFocusList()
        {
            comboBoxProcessList.Items.Clear();
            comboBoxProcessList.Sorted = true;

            comboBoxProcessList.Items.Add(string.Empty);

            foreach (Process process in Process.GetProcesses())
            {
                if (!comboBoxProcessList.Items.Contains(process.ProcessName))
                {
                    comboBoxProcessList.Items.Add(process.ProcessName);
                }
            }

            string applicationFocus = _config.Settings.User.GetByKey("ApplicationFocus", _config.Settings.DefaultSettings.ApplicationFocus).Value.ToString();

            if (string.IsNullOrEmpty(applicationFocus))
            {
                comboBoxProcessList.SelectedIndex = 0;

                return;
            }

            if (!comboBoxProcessList.Items.Contains(applicationFocus))
            {
                comboBoxProcessList.Items.Add(applicationFocus);
            }

            comboBoxProcessList.SelectedIndex = comboBoxProcessList.Items.IndexOf(applicationFocus);

            numericUpDownApplicationFocusDelayBefore.Value = Convert.ToInt32(_config.Settings.User.GetByKey("ApplicationFocusDelayBefore", _config.Settings.DefaultSettings.ApplicationFocusDelayBefore).Value);
            numericUpDownApplicationFocusDelayAfter.Value = Convert.ToInt32(_config.Settings.User.GetByKey("ApplicationFocusDelayAfter", _config.Settings.DefaultSettings.ApplicationFocusDelayAfter).Value);
        }
    }
}
