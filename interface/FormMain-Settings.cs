using System;
using System.Collections.Generic;
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
                Log.WriteMessage("*** Welcome to " + Settings.ApplicationName + " " + Settings.ApplicationVersion + " ***");
                Log.WriteMessage("Starting application");
                Log.WriteMessage("At this point the application should be able to run normally");
                Log.WriteMessage("but it would be a good idea to check what we found in your autoscreen.conf file");
                Log.WriteMessage("Your autoscreen.conf file is \"" + FileSystem.ConfigFile + "\"");
                Log.WriteMessage("The name and location of it can be changed with the -config command line argument:");
                Log.WriteMessage("autoscreen.exe -config=C:\\MyAutoScreenCapture.conf");
                Log.WriteMessage("Checking what we loaded from your autoscreen.conf file ...");
                Log.WriteMessage("ApplicationSettingsFile=" + FileSystem.ApplicationSettingsFile);
                Log.WriteMessage("UserSettingsFile=" + FileSystem.UserSettingsFile);
                Log.WriteMessage("DebugFolder=" + FileSystem.DebugFolder);
                Log.WriteMessage("LogsFolder=" + FileSystem.LogsFolder);
                Log.WriteMessage("ScreenshotsFolder=" + FileSystem.ScreenshotsFolder);
                Log.WriteMessage("ScreenshotsFile=" + FileSystem.ScreenshotsFile);
                Log.WriteMessage("TriggersFile=" + FileSystem.TriggersFile);
                Log.WriteMessage("ScreensFile=" + FileSystem.ScreensFile);
                Log.WriteMessage("RegionsFile=" + FileSystem.RegionsFile);
                Log.WriteMessage("EditorsFile=" + FileSystem.EditorsFile);
                Log.WriteMessage("TagsFile = " + FileSystem.TagsFile);

                Log.WriteMessage("It looks like I successfully parsed your \"" + FileSystem.ConfigFile + "\" file.");
                Log.WriteMessage("I'm now going to attempt to load your personal settings and any screenshots you have taken.");

                Log.WriteMessage("Loading user settings");
                Settings.User.Load();
                Log.WriteMessage("User settings loaded");

                Log.WriteMessage("Attempting upgrade of user settings from old version of application (if needed)");
                Settings.User.Upgrade();

                Log.WriteMessage("Initializing screen capture");
                _screenCapture = new ScreenCapture();

                Log.WriteMessage("Initializing image format collection");
                _imageFormatCollection = new ImageFormatCollection();

                Log.WriteMessage("Initializing editor collection");
                formEditor.EditorCollection.LoadXmlFileAndAddEditors();
                Log.WriteMessage("Number of editors loaded = " + formEditor.EditorCollection.Count);

                Log.WriteMessage("Initializing trigger collection");
                formTrigger.TriggerCollection.LoadXmlFileAndAddTriggers();
                Log.WriteMessage("Number of triggers loaded = " + formTrigger.TriggerCollection.Count);

                Log.WriteMessage("Initializing region collection");
                formRegion.RegionCollection.LoadXmlFileAndAddRegions(_imageFormatCollection);
                Log.WriteMessage("Number of regions loaded = " + formRegion.RegionCollection.Count);

                Log.WriteMessage("Initializing screen collection");
                formScreen.ScreenCollection.LoadXmlFileAndAddScreens(_imageFormatCollection);
                Log.WriteMessage("Number of screens loaded = " + formScreen.ScreenCollection.Count);

                Log.WriteMessage("Initializing tag collection");
                formTag.TagCollection.LoadXmlFileAndAddTags();
                Log.WriteMessage("Number of tags loaded = " + formTag.TagCollection.Count);

                Log.WriteMessage("Initializing schedule collection");
                formSchedule.ScheduleCollection.LoadXmlFileAndAddSchedules();
                Log.WriteMessage("Number of schedules loaded = " + formSchedule.ScheduleCollection.Count);

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

                Log.WriteMessage("Loading screenshots");
                _screenshotCollection.LoadXmlFile();

                int screenCaptureInterval = Convert.ToInt32(Settings.User.GetByKey("IntScreenCaptureInterval", defaultValue: 60000).Value);
                Log.WriteMessage("IntScreenCaptureInterval = " + screenCaptureInterval);

                if (screenCaptureInterval == 0)
                {
                    screenCaptureInterval = 60000;
                    Log.WriteMessage("WARNING: Screen capture interval was found to be 0 so 60,000 milliseconds (or 1 minute) is being used as the default value");
                }

                Log.WriteMessage("Assigning screen capture interval value to its appropriate hour, minute, second, and millisecond variables");

                decimal screenCaptureIntervalHours = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(screenCaptureInterval)).Hours);
                Log.WriteMessage("Hours = " + screenCaptureIntervalHours);

                decimal screenCaptureIntervalMinutes = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(screenCaptureInterval)).Minutes);
                Log.WriteMessage("Minutes = " + screenCaptureIntervalMinutes);

                decimal screenCaptureIntervalSeconds = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(screenCaptureInterval)).Seconds);
                Log.WriteMessage("Seconds = " + screenCaptureIntervalSeconds);

                decimal screenCaptureIntervalMilliseconds = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(screenCaptureInterval)).Milliseconds);
                Log.WriteMessage("Milliseconds = " + screenCaptureIntervalMilliseconds);

                numericUpDownHoursInterval.Value = screenCaptureIntervalHours;
                numericUpDownMinutesInterval.Value = screenCaptureIntervalMinutes;
                numericUpDownSecondsInterval.Value = screenCaptureIntervalSeconds;
                numericUpDownMillisecondsInterval.Value = screenCaptureIntervalMilliseconds;

                numericUpDownCaptureLimit.Value = Convert.ToInt32(Settings.User.GetByKey("IntCaptureLimit", defaultValue: 0).Value);
                Log.WriteMessage("IntCaptureLimit = " + numericUpDownCaptureLimit.Value);

                checkBoxCaptureLimit.Checked = Convert.ToBoolean(Settings.User.GetByKey("BoolCaptureLimit", defaultValue: false).Value);
                Log.WriteMessage("BoolCaptureLimit = " + checkBoxCaptureLimit.Checked);

                checkBoxInitialScreenshot.Checked = Convert.ToBoolean(Settings.User.GetByKey("BoolTakeInitialScreenshot", defaultValue: false).Value);
                Log.WriteMessage("BoolTakeInitialScreenshot = " + checkBoxInitialScreenshot.Checked);

                notifyIcon.Visible = Convert.ToBoolean(Settings.User.GetByKey("BoolShowSystemTrayIcon", defaultValue: true).Value);
                Log.WriteMessage("BoolShowSystemTrayIcon = " + notifyIcon.Visible);

                numericUpDownKeepScreenshotsForDays.Value = Convert.ToDecimal(Settings.User.GetByKey("IntKeepScreenshotsForDays", defaultValue: 30).Value);
                Log.WriteMessage("IntKeepScreenshotsForDays = " + numericUpDownKeepScreenshotsForDays.Value);

                comboBoxScreenshotLabel.Text = Settings.User.GetByKey("StringScreenshotLabel", defaultValue: string.Empty).Value.ToString();
                Log.WriteMessage("StringScreenshotLabel = " + comboBoxScreenshotLabel.Text);

                checkBoxScreenshotLabel.Checked = Convert.ToBoolean(Settings.User.GetByKey("BoolApplyScreenshotLabel", defaultValue: false).Value);

                EnableStartCapture();

                CaptureLimitCheck();
            }
            catch (Exception ex)
            {
                Log.WriteException("FormMain::LoadSettings", ex);
            }
        }

        /// <summary>
        /// Saves the user's settings.
        /// </summary>
        private void SaveSettings()
        {
            try
            {
                Log.WriteMessage("Saving settings");

                Stopwatch stopwatch = new Stopwatch();

                stopwatch.Start();

                Settings.User.GetByKey("IntScreenCaptureInterval", defaultValue: 60000).Value = GetScreenCaptureInterval();
                Settings.User.GetByKey("IntCaptureLimit", defaultValue: 0).Value = numericUpDownCaptureLimit.Value;
                Settings.User.GetByKey("BoolCaptureLimit", defaultValue: false).Value = checkBoxCaptureLimit.Checked;
                Settings.User.GetByKey("BoolTakeInitialScreenshot", defaultValue: false).Value = checkBoxInitialScreenshot.Checked;
                Settings.User.GetByKey("IntKeepScreenshotsForDays", defaultValue: 30).Value = numericUpDownKeepScreenshotsForDays.Value;
                Settings.User.GetByKey("StringScreenshotLabel", defaultValue: string.Empty).Value = comboBoxScreenshotLabel.Text;
                Settings.User.GetByKey("BoolApplyScreenshotLabel", defaultValue: false).Value = checkBoxScreenshotLabel.Checked;

                Settings.User.Save();

                Log.WriteMessage("Settings saved");

                stopwatch.Stop();

                Log.WriteMessage("It took " + stopwatch.ElapsedMilliseconds + " milliseconds to save user settings");
            }
            catch (Exception ex)
            {
                Log.WriteException("FormMain::SaveSettings", ex);
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
