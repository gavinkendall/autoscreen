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
                Log.WriteMessage("*** Welcome to " + Settings.ApplicationName + " " + Settings.ApplicationVersion + " (\"" + Settings.ApplicationCodename + "\") ***");
                Log.WriteMessage("Starting application");
                Log.WriteDebugMessage("At this point the application should be able to run normally");
                Log.WriteDebugMessage("but it would be a good idea to check what we found in your autoscreen.conf file");
                Log.WriteDebugMessage("Your autoscreen.conf file is \"" + FileSystem.ConfigFile + "\"");
                Log.WriteDebugMessage("The name and location of it can be changed with the -config command line argument:");
                Log.WriteDebugMessage("autoscreen.exe -config=C:\\MyAutoScreenCapture.conf");
                Log.WriteDebugMessage("Checking what we loaded from your autoscreen.conf file ...");
                Log.WriteDebugMessage("ApplicationSettingsFile=" + FileSystem.ApplicationSettingsFile);
                Log.WriteDebugMessage("UserSettingsFile=" + FileSystem.UserSettingsFile);
                Log.WriteDebugMessage("DebugFolder=" + FileSystem.DebugFolder);
                Log.WriteDebugMessage("LogsFolder=" + FileSystem.LogsFolder);
                Log.WriteDebugMessage("CommandFile=" + FileSystem.CommandFile);
                Log.WriteDebugMessage("ScreenshotsFolder=" + FileSystem.ScreenshotsFolder);
                Log.WriteDebugMessage("ScreenshotsFile=" + FileSystem.ScreenshotsFile);
                Log.WriteDebugMessage("TriggersFile=" + FileSystem.TriggersFile);
                Log.WriteDebugMessage("ScreensFile=" + FileSystem.ScreensFile);
                Log.WriteDebugMessage("RegionsFile=" + FileSystem.RegionsFile);
                Log.WriteDebugMessage("EditorsFile=" + FileSystem.EditorsFile);
                Log.WriteDebugMessage("TagsFile = " + FileSystem.TagsFile);

                Log.WriteDebugMessage("It looks like I successfully parsed your \"" + FileSystem.ConfigFile + "\" file.");
                Log.WriteDebugMessage("I'm now going to attempt to load your personal settings and any screenshots you have taken.");

                Log.WriteMessage("Initializing screen capture");
                _screenCapture = new ScreenCapture();

                Log.WriteMessage("Initializing image format collection");
                _imageFormatCollection = new ImageFormatCollection();

                Log.WriteMessage("Initializing editor collection");
                
                if (!_formEditor.EditorCollection.LoadXmlFileAndAddEditors())
                {
                    _screenCapture.ApplicationError = true;
                }

                Log.WriteDebugMessage("Number of editors loaded = " + _formEditor.EditorCollection.Count);

                Log.WriteMessage("Initializing trigger collection");

                if (!_formTrigger.TriggerCollection.LoadXmlFileAndAddTriggers())
                {
                    _screenCapture.ApplicationError = true;
                }

                Log.WriteDebugMessage("Number of triggers loaded = " + _formTrigger.TriggerCollection.Count);

                Log.WriteMessage("Initializing region collection");

                if (!_formRegion.RegionCollection.LoadXmlFileAndAddRegions(_imageFormatCollection))
                {
                    _screenCapture.ApplicationError = true;
                }

                Log.WriteDebugMessage("Number of regions loaded = " + _formRegion.RegionCollection.Count);

                Log.WriteMessage("Initializing screen collection");

                if (!_formScreen.ScreenCollection.LoadXmlFileAndAddScreens(_imageFormatCollection))
                {
                    _screenCapture.ApplicationError = true;
                }

                Log.WriteDebugMessage("Number of screens loaded = " + _formScreen.ScreenCollection.Count);

                Log.WriteMessage("Initializing tag collection");

                if (!_formTag.TagCollection.LoadXmlFileAndAddTags())
                {
                    _screenCapture.ApplicationError = true;
                }

                Log.WriteDebugMessage("Number of tags loaded = " + _formTag.TagCollection.Count);

                Log.WriteMessage("Initializing schedule collection");

                if (!_formSchedule.ScheduleCollection.LoadXmlFileAndAddSchedules())
                {
                    _screenCapture.ApplicationError = true;
                }

                Log.WriteDebugMessage("Number of schedules loaded = " + _formSchedule.ScheduleCollection.Count);

                Log.WriteMessage("Building screens module");
                BuildScreensModule();

                Log.WriteMessage("Building editors module");
                BuildEditorsModule();

                Log.WriteMessage("Building triggers module");
                BuildTriggersModule();

                Log.WriteMessage("Building regions module");
                BuildRegionsModule();

                Log.WriteMessage("Building tags module");
                BuildTagsModule();

                Log.WriteMessage("Building schedules module");
                BuildSchedulesModule();

                Log.WriteMessage("Building screenshot preview context menu");
                BuildScreenshotPreviewContextualMenu();

                Log.WriteMessage("Building view tab pages");
                BuildViewTabPages();

                Log.WriteMessage("Initializing screenshot collection");
                _screenshotCollection = new ScreenshotCollection(_imageFormatCollection, _formScreen.ScreenCollection);

                _screenshotCollection.LoadXmlFile();

                int screenCaptureInterval = Convert.ToInt32(Settings.User.GetByKey("IntScreenCaptureInterval", DefaultSettings.IntScreenCaptureInterval).Value);
                Log.WriteDebugMessage("IntScreenCaptureInterval = " + screenCaptureInterval);

                if (screenCaptureInterval == 0)
                {
                    screenCaptureInterval = 60000;
                    Log.WriteDebugMessage("WARNING: Screen capture interval was found to be 0 so 60,000 milliseconds (or 1 minute) is being used as the default value");
                }

                Log.WriteDebugMessage("Assigning screen capture interval value to its appropriate hour, minute, second, and millisecond variables");

                decimal screenCaptureIntervalHours = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(screenCaptureInterval)).Hours);
                Log.WriteDebugMessage("Hours = " + screenCaptureIntervalHours);

                decimal screenCaptureIntervalMinutes = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(screenCaptureInterval)).Minutes);
                Log.WriteDebugMessage("Minutes = " + screenCaptureIntervalMinutes);

                decimal screenCaptureIntervalSeconds = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(screenCaptureInterval)).Seconds);
                Log.WriteDebugMessage("Seconds = " + screenCaptureIntervalSeconds);

                decimal screenCaptureIntervalMilliseconds = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(screenCaptureInterval)).Milliseconds);
                Log.WriteDebugMessage("Milliseconds = " + screenCaptureIntervalMilliseconds);

                numericUpDownHoursInterval.Value = screenCaptureIntervalHours;
                numericUpDownMinutesInterval.Value = screenCaptureIntervalMinutes;
                numericUpDownSecondsInterval.Value = screenCaptureIntervalSeconds;
                numericUpDownMillisecondsInterval.Value = screenCaptureIntervalMilliseconds;

                numericUpDownCaptureLimit.Value = Convert.ToInt32(Settings.User.GetByKey("IntCaptureLimit", DefaultSettings.IntCaptureLimit).Value);
                Log.WriteDebugMessage("IntCaptureLimit = " + numericUpDownCaptureLimit.Value);

                checkBoxCaptureLimit.Checked = Convert.ToBoolean(Settings.User.GetByKey("BoolCaptureLimit", DefaultSettings.BoolCaptureLimit).Value);
                Log.WriteDebugMessage("BoolCaptureLimit = " + checkBoxCaptureLimit.Checked);

                checkBoxInitialScreenshot.Checked = Convert.ToBoolean(Settings.User.GetByKey("BoolTakeInitialScreenshot", DefaultSettings.BoolTakeInitialScreenshot).Value);
                Log.WriteDebugMessage("BoolTakeInitialScreenshot = " + checkBoxInitialScreenshot.Checked);

                notifyIcon.Visible = Convert.ToBoolean(Settings.User.GetByKey("BoolShowSystemTrayIcon", DefaultSettings.BoolShowSystemTrayIcon).Value);
                Log.WriteDebugMessage("BoolShowSystemTrayIcon = " + notifyIcon.Visible);

                numericUpDownKeepScreenshotsForDays.Value = Convert.ToDecimal(Settings.User.GetByKey("IntKeepScreenshotsForDays", DefaultSettings.IntKeepScreenshotsForDays).Value);
                Log.WriteDebugMessage("IntKeepScreenshotsForDays = " + numericUpDownKeepScreenshotsForDays.Value);

                comboBoxScreenshotLabel.Text = Settings.User.GetByKey("StringScreenshotLabel", DefaultSettings.StringScreenshotLabel).Value.ToString();
                Log.WriteDebugMessage("StringScreenshotLabel = " + comboBoxScreenshotLabel.Text);

                checkBoxScreenshotLabel.Checked = Convert.ToBoolean(Settings.User.GetByKey("BoolApplyScreenshotLabel", DefaultSettings.BoolApplyScreenshotLabel).Value);

                // The user can compare the current Active Window Title text to compare against what the text they've defined.
                checkBoxActiveWindowTitle.Checked = Convert.ToBoolean(Settings.User.GetByKey("BoolActiveWindowTitleCaptureCheck", DefaultSettings.BoolActiveWindowTitleCaptureCheck).Value);
                textBoxActiveWindowTitle.Text = Settings.User.GetByKey("StringActiveWindowTitleCaptureText", DefaultSettings.StringActiveWindowTitleCaptureText).Value.ToString();

                // Region Select / Auto Save
                textBoxAutoSaveFolder.Text = Settings.User.GetByKey("StringAutoSaveFolder", DefaultSettings.StringAutoSaveFolder).Value.ToString();
                textBoxAutoSaveMacro.Text = Settings.User.GetByKey("StringAutoSaveMacro", DefaultSettings.StringAutoSaveMacro).Value.ToString();

                EnableStartCapture();

                CaptureLimitCheck();

                Log.WriteDebugMessage("Settings loaded");
            }
            catch (Exception ex)
            {
                _screenCapture.ApplicationError = true;
                Log.WriteExceptionMessage("FormMain-Settings::LoadSettings", ex);
            }
        }

        /// <summary>
        /// Saves the user's settings.
        /// </summary>
        private void SaveSettings()
        {
            try
            {
                Settings.User.GetByKey("IntScreenCaptureInterval", DefaultSettings.IntScreenCaptureInterval).Value = GetScreenCaptureInterval();
                Settings.User.GetByKey("IntCaptureLimit", DefaultSettings.IntCaptureLimit).Value = numericUpDownCaptureLimit.Value;
                Settings.User.GetByKey("BoolCaptureLimit", DefaultSettings.BoolCaptureLimit).Value = checkBoxCaptureLimit.Checked;
                Settings.User.GetByKey("BoolTakeInitialScreenshot", DefaultSettings.BoolTakeInitialScreenshot).Value = checkBoxInitialScreenshot.Checked;
                Settings.User.GetByKey("IntKeepScreenshotsForDays", DefaultSettings.IntKeepScreenshotsForDays).Value = numericUpDownKeepScreenshotsForDays.Value;

                // Label.
                Settings.User.GetByKey("StringScreenshotLabel", DefaultSettings.StringScreenshotLabel).Value = comboBoxScreenshotLabel.Text.Trim();
                Settings.User.GetByKey("BoolApplyScreenshotLabel", DefaultSettings.BoolApplyScreenshotLabel).Value = checkBoxScreenshotLabel.Checked;

                // Active Window Title text comparison check.
                Settings.User.GetByKey("BoolActiveWindowTitleCaptureCheck", DefaultSettings.BoolActiveWindowTitleCaptureCheck).Value = checkBoxActiveWindowTitle.Checked;
                Settings.User.GetByKey("StringActiveWindowTitleCaptureText", DefaultSettings.StringActiveWindowTitleCaptureText).Value = textBoxActiveWindowTitle.Text.Trim();

                // Region Select / Auto Save.
                Settings.User.GetByKey("StringAutoSaveFolder", DefaultSettings.StringAutoSaveFolder).Value = textBoxAutoSaveFolder.Text.Trim();
                Settings.User.GetByKey("StringAutoSaveMacro", DefaultSettings.StringAutoSaveMacro).Value = textBoxAutoSaveMacro.Text.Trim();

                if (!Settings.User.Save())
                {
                    _screenCapture.ApplicationError = true;
                }
            }
            catch (Exception ex)
            {
                _screenCapture.ApplicationError = true;
                Log.WriteExceptionMessage("FormMain-Settings::SaveSettings", ex);
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
    }
}
