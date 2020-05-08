//-----------------------------------------------------------------------
// <copyright file="FormMain-Settings.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>Methods for loading settings and saving settings.</summary>
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

                Log.WriteMessage("Loading user settings");
                Settings.User.Load();
                Log.WriteDebugMessage("User settings loaded");

                Log.WriteDebugMessage("Attempting upgrade of user settings from old version of application (if needed)");
                Settings.User.Upgrade();

                Log.WriteMessage("Initializing screen capture");
                _screenCapture = new ScreenCapture();

                Log.WriteMessage("Initializing image format collection");
                _imageFormatCollection = new ImageFormatCollection();

                Log.WriteMessage("Initializing editor collection");
                formEditor.EditorCollection.LoadXmlFileAndAddEditors();
                Log.WriteDebugMessage("Number of editors loaded = " + formEditor.EditorCollection.Count);

                Log.WriteMessage("Initializing trigger collection");
                formTrigger.TriggerCollection.LoadXmlFileAndAddTriggers();
                Log.WriteDebugMessage("Number of triggers loaded = " + formTrigger.TriggerCollection.Count);

                Log.WriteMessage("Initializing region collection");
                formRegion.RegionCollection.LoadXmlFileAndAddRegions(_imageFormatCollection);
                Log.WriteDebugMessage("Number of regions loaded = " + formRegion.RegionCollection.Count);

                Log.WriteMessage("Initializing screen collection");
                formScreen.ScreenCollection.LoadXmlFileAndAddScreens(_imageFormatCollection);
                Log.WriteDebugMessage("Number of screens loaded = " + formScreen.ScreenCollection.Count);

                Log.WriteMessage("Initializing tag collection");
                formTag.TagCollection.LoadXmlFileAndAddTags();
                Log.WriteDebugMessage("Number of tags loaded = " + formTag.TagCollection.Count);

                Log.WriteMessage("Initializing schedule collection");
                formSchedule.ScheduleCollection.LoadXmlFileAndAddSchedules();
                Log.WriteDebugMessage("Number of schedules loaded = " + formSchedule.ScheduleCollection.Count);

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
                _screenshotCollection = new ScreenshotCollection(_imageFormatCollection, formScreen.ScreenCollection);

                _screenshotCollection.LoadXmlFile();

                int screenCaptureInterval = Convert.ToInt32(Settings.User.GetByKey("IntScreenCaptureInterval", defaultValue: 60000).Value);
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

                numericUpDownCaptureLimit.Value = Convert.ToInt32(Settings.User.GetByKey("IntCaptureLimit", defaultValue: 0).Value);
                Log.WriteDebugMessage("IntCaptureLimit = " + numericUpDownCaptureLimit.Value);

                checkBoxCaptureLimit.Checked = Convert.ToBoolean(Settings.User.GetByKey("BoolCaptureLimit", defaultValue: false).Value);
                Log.WriteDebugMessage("BoolCaptureLimit = " + checkBoxCaptureLimit.Checked);

                checkBoxInitialScreenshot.Checked = Convert.ToBoolean(Settings.User.GetByKey("BoolTakeInitialScreenshot", defaultValue: false).Value);
                Log.WriteDebugMessage("BoolTakeInitialScreenshot = " + checkBoxInitialScreenshot.Checked);

                notifyIcon.Visible = Convert.ToBoolean(Settings.User.GetByKey("BoolShowSystemTrayIcon", defaultValue: true).Value);
                Log.WriteDebugMessage("BoolShowSystemTrayIcon = " + notifyIcon.Visible);

                numericUpDownKeepScreenshotsForDays.Value = Convert.ToDecimal(Settings.User.GetByKey("IntKeepScreenshotsForDays", defaultValue: 30).Value);
                Log.WriteDebugMessage("IntKeepScreenshotsForDays = " + numericUpDownKeepScreenshotsForDays.Value);

                comboBoxScreenshotLabel.Text = Settings.User.GetByKey("StringScreenshotLabel", defaultValue: string.Empty).Value.ToString();
                Log.WriteDebugMessage("StringScreenshotLabel = " + comboBoxScreenshotLabel.Text);

                checkBoxScreenshotLabel.Checked = Convert.ToBoolean(Settings.User.GetByKey("BoolApplyScreenshotLabel", defaultValue: false).Value);

                EnableStartCapture();

                CaptureLimitCheck();

                Log.WriteDebugMessage("Settings loaded");
            }
            catch (Exception ex)
            {
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
                Log.WriteDebugMessage(":: SaveSettings Start ::");

                Log.WriteDebugMessage("Saving settings");

                Settings.User.GetByKey("IntScreenCaptureInterval", defaultValue: 60000).Value = GetScreenCaptureInterval();
                Settings.User.GetByKey("IntCaptureLimit", defaultValue: 0).Value = numericUpDownCaptureLimit.Value;
                Settings.User.GetByKey("BoolCaptureLimit", defaultValue: false).Value = checkBoxCaptureLimit.Checked;
                Settings.User.GetByKey("BoolTakeInitialScreenshot", defaultValue: false).Value = checkBoxInitialScreenshot.Checked;
                Settings.User.GetByKey("IntKeepScreenshotsForDays", defaultValue: 30).Value = numericUpDownKeepScreenshotsForDays.Value;
                Settings.User.GetByKey("StringScreenshotLabel", defaultValue: string.Empty).Value = comboBoxScreenshotLabel.Text;
                Settings.User.GetByKey("BoolApplyScreenshotLabel", defaultValue: false).Value = checkBoxScreenshotLabel.Checked;

                Settings.User.Save();

                Log.WriteDebugMessage("Settings saved");

                Log.WriteDebugMessage(":: SaveSettings End ::");
            }
            catch (Exception ex)
            {
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
