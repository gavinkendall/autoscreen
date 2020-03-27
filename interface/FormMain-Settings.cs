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
                Log.Write("*** Welcome to " + Settings.ApplicationName + " " + Settings.ApplicationVersion + " ***");
                Log.Write("Starting application");
                Log.Write("At this point the application should be able to run normally");
                Log.Write("but it would be a good idea to check what we found in your autoscreen.conf file");
                Log.Write("Your autoscreen.conf file is \"" + FileSystem.ConfigFile + "\"");
                Log.Write("The name and location of it can be changed with the -config command line argument:");
                Log.Write("autoscreen.exe -config=C:\\MyAutoScreenCapture.conf");
                Log.Write("Checking what we loaded from your autoscreen.conf file ...");
                Log.Write("ApplicationSettingsFile=" + FileSystem.ApplicationSettingsFile);
                Log.Write("UserSettingsFile=" + FileSystem.UserSettingsFile);
                Log.Write("DebugFolder=" + FileSystem.DebugFolder);
                Log.Write("LogsFolder=" + FileSystem.LogsFolder);
                Log.Write("ScreenshotsFolder=" + FileSystem.ScreenshotsFolder);
                Log.Write("ScreenshotsFile=" + FileSystem.ScreenshotsFile);
                Log.Write("TriggersFile=" + FileSystem.TriggersFile);
                Log.Write("ScreensFile=" + FileSystem.ScreensFile);
                Log.Write("RegionsFile=" + FileSystem.RegionsFile);
                Log.Write("EditorsFile=" + FileSystem.EditorsFile);
                Log.Write("TagsFile = " + FileSystem.TagsFile);

                Log.Write("It looks like I successfully parsed your \"" + FileSystem.ConfigFile + "\" file.");
                Log.Write("I'm now going to attempt to load your personal settings and any screenshots you have taken.");

                Log.Write("Loading user settings");
                Settings.User.Load();
                Log.Write("User settings loaded");

                Log.Write("Attempting upgrade of user settings from old version of application (if needed)");
                Settings.User.Upgrade();

                Log.Write("Initializing screen capture");
                _screenCapture = new ScreenCapture();

                Log.Write("Initializing image format collection");
                _imageFormatCollection = new ImageFormatCollection();

                Log.Write("Initializing editor collection");
                formEditor.EditorCollection.LoadXmlFileAndAddEditors();
                Log.Write("Number of editors loaded = " + formEditor.EditorCollection.Count);

                Log.Write("Initializing trigger collection");
                formTrigger.TriggerCollection.LoadXmlFileAndAddTriggers();
                Log.Write("Number of triggers loaded = " + formTrigger.TriggerCollection.Count);

                Log.Write("Initializing region collection");
                formRegion.RegionCollection.LoadXmlFileAndAddRegions(_imageFormatCollection);
                Log.Write("Number of regions loaded = " + formRegion.RegionCollection.Count);

                Log.Write("Initializing screen collection");
                formScreen.ScreenCollection.LoadXmlFileAndAddScreens(_imageFormatCollection);
                Log.Write("Number of screens loaded = " + formScreen.ScreenCollection.Count);

                Log.Write("Initializing tag collection");
                formTag.TagCollection.LoadXmlFileAndAddTags();
                Log.Write("Number of tags loaded = " + formTag.TagCollection.Count);

                Log.Write("Building screens module");
                BuildScreensModule();

                Log.Write("Building editors module");
                BuildEditorsModule();

                Log.Write("Building triggers module");
                BuildTriggersModule();

                Log.Write("Building regions module");
                BuildRegionsModule();

                Log.Write("Building tags module");
                BuildTagsModule();

                Log.Write("Building schedules module");
                BuildSchedulesModule();

                Log.Write("Building screenshot preview context menu");
                BuildScreenshotPreviewContextualMenu();

                Log.Write("Building view tab pages");
                BuildViewTabPages();

                Log.Write("Initializing screenshot collection");
                _screenshotCollection = new ScreenshotCollection(_imageFormatCollection, formScreen.ScreenCollection);

                Log.Write("Loading screenshots");
                _screenshotCollection.LoadXmlFile();

                int screenCaptureInterval = Convert.ToInt32(Settings.User.GetByKey("IntScreenCaptureInterval", defaultValue: 60000).Value);
                Log.Write("IntScreenCaptureInterval = " + screenCaptureInterval);

                if (screenCaptureInterval == 0)
                {
                    screenCaptureInterval = 60000;
                    Log.Write("WARNING: Screen capture interval was found to be 0 so 60,000 milliseconds (or 1 minute) is being used as the default value");
                }

                Log.Write("Assigning screen capture interval value to its appropriate hour, minute, second, and millisecond variables");

                decimal screenCaptureIntervalHours = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(screenCaptureInterval)).Hours);
                Log.Write("Hours = " + screenCaptureIntervalHours);

                decimal screenCaptureIntervalMinutes = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(screenCaptureInterval)).Minutes);
                Log.Write("Minutes = " + screenCaptureIntervalMinutes);

                decimal screenCaptureIntervalSeconds = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(screenCaptureInterval)).Seconds);
                Log.Write("Seconds = " + screenCaptureIntervalSeconds);

                decimal screenCaptureIntervalMilliseconds = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(screenCaptureInterval)).Milliseconds);
                Log.Write("Milliseconds = " + screenCaptureIntervalMilliseconds);

                numericUpDownHoursInterval.Value = screenCaptureIntervalHours;
                numericUpDownMinutesInterval.Value = screenCaptureIntervalMinutes;
                numericUpDownSecondsInterval.Value = screenCaptureIntervalSeconds;
                numericUpDownMillisecondsInterval.Value = screenCaptureIntervalMilliseconds;

                numericUpDownCaptureLimit.Value = Convert.ToInt32(Settings.User.GetByKey("IntCaptureLimit", defaultValue: 0).Value);
                Log.Write("IntCaptureLimit = " + numericUpDownCaptureLimit.Value);

                checkBoxCaptureLimit.Checked = Convert.ToBoolean(Settings.User.GetByKey("BoolCaptureLimit", defaultValue: false).Value);
                Log.Write("BoolCaptureLimit = " + checkBoxCaptureLimit.Checked);

                checkBoxInitialScreenshot.Checked = Convert.ToBoolean(Settings.User.GetByKey("BoolTakeInitialScreenshot", defaultValue: false).Value);
                Log.Write("BoolTakeInitialScreenshot = " + checkBoxInitialScreenshot.Checked);

                notifyIcon.Visible = Convert.ToBoolean(Settings.User.GetByKey("BoolShowSystemTrayIcon", defaultValue: true).Value);
                Log.Write("BoolShowSystemTrayIcon = " + notifyIcon.Visible);

                numericUpDownKeepScreenshotsForDays.Value = Convert.ToDecimal(Settings.User.GetByKey("IntKeepScreenshotsForDays", defaultValue: 30).Value);
                Log.Write("IntKeepScreenshotsForDays = " + numericUpDownKeepScreenshotsForDays.Value);

                comboBoxScreenshotLabel.Text = Settings.User.GetByKey("StringScreenshotLabel", defaultValue: string.Empty).Value.ToString();
                Log.Write("StringScreenshotLabel = " + comboBoxScreenshotLabel.Text);

                checkBoxScreenshotLabel.Checked = Convert.ToBoolean(Settings.User.GetByKey("BoolApplyScreenshotLabel", defaultValue: false).Value);

                EnableStartCapture();

                CaptureLimitCheck();
            }
            catch (Exception ex)
            {
                Log.Write("FormMain::LoadSettings", ex);
            }
        }

        /// <summary>
        /// Saves the user's settings.
        /// </summary>
        private void SaveSettings()
        {
            try
            {
                Log.Write("Saving settings");

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

                Log.Write("Settings saved");

                stopwatch.Stop();

                Log.Write("It took " + stopwatch.ElapsedMilliseconds + " milliseconds to save user settings");
            }
            catch (Exception ex)
            {
                Log.Write("FormMain::SaveSettings", ex);
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
