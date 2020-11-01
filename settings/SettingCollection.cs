//-----------------------------------------------------------------------
// <copyright file="SettingCollection.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A collection of settings.</summary>
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
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace AutoScreenCapture
{
    /// <summary>
    /// A collection class to store and manage Setting objects.
    /// </summary>
    public class SettingCollection : IEnumerable<Setting>
    {
        private List<Setting> _settingList = new List<Setting>();

        private const int MAX_FILE_SIZE = 5242880;
        private const string XML_FILE_INDENT_CHARS = "   ";
        private const string XML_FILE_SETTING_NODE = "setting";
        private const string XML_FILE_SETTINGS_NODE = "settings";
        private const string XML_FILE_ROOT_NODE = "autoscreen";

        private const string SETTING_KEY = "key";
        private const string SETTING_VALUE = "value";
        private readonly string SETTING_XPATH;

        /// <summary>
        /// The application's codename from the XML document.
        /// </summary>
        public string AppCodename { get; set; }

        /// <summary>
        /// The application's version from the XML document.
        /// </summary>
        public string AppVersion { get; set; }

        internal string Filepath { get; set; }

        /// <summary>
        /// Empty constructor for the SettingCollection. This prepares the XML node path.
        /// </summary>
        public SettingCollection()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("/");
            sb.Append(XML_FILE_ROOT_NODE);
            sb.Append("/");
            sb.Append(XML_FILE_SETTINGS_NODE);
            sb.Append("/");
            sb.Append(XML_FILE_SETTING_NODE);

            SETTING_XPATH = sb.ToString();
        }

        /// <summary>
        /// Returns the enumerator for the collection.
        /// </summary>
        /// <returns>A list of Setting objects.</returns>
        public List<Setting>.Enumerator GetEnumerator()
        {
            return _settingList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Setting>) _settingList).GetEnumerator();
        }

        IEnumerator<Setting> IEnumerable<Setting>.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void Remove(Setting setting)
        {
            if (KeyExists(setting.Key))
            {
                _settingList.Remove(setting);
            }
        }

        private void RemoveByKey(string key)
        {
            if (KeyExists(key))
            {
                Setting setting = GetByKey(key, null, false);

                Remove(setting);
            }
        }

        /// <summary>
        /// Adds a Setting to the collection.
        /// </summary>
        /// <param name="setting"></param>
        public void Add(Setting setting)
        {
            // Make sure we only add the setting to the list if it doesn't exist in the list yet.
            if (!KeyExists(setting.Key))
            {
                _settingList.Add(setting);
            }
        }

        /// <summary>
        /// Changes the value of a Setting based on its key.
        /// </summary>
        /// <param name="key">The unique key of the Setting.</param>
        /// <param name="value">The new object that will be used for the existing Setting.</param>
        public void SetValueByKey(string key, object value)
        {
            RemoveByKey(key);
            Add(new Setting(key, value));
        }

        /// <summary>
        /// Gets a setting by its key.
        /// If the setting is found it will return the Setting object.
        /// If the setting is not found a new Setting will be created with the provided key and default value.
        /// </summary>
        /// <param name="key">The key to use for finding an existing Setting or for creating a new Setting.</param>
        /// <param name="defaultValue">The default value to use if the Setting cannot be found.</param>
        /// <param name="createKeyIfNotFound">Create a new Setting based on the key name if it does not exist.</param>
        /// <returns>Setting object (either existing or new).</returns>
        public Setting GetByKey(string key, object defaultValue, bool createKeyIfNotFound)
        {
            foreach (Setting setting in _settingList)
            {
                if (setting.Key.Equals(key))
                {
                    return setting;
                }
            }

            if (createKeyIfNotFound)
            {
                Setting newSetting = new Setting(key, defaultValue);
                Add(newSetting);

                return newSetting;
            }

            return null;
        }

        /// <summary>
        /// Gets a Setting by its unique key. This method also creates the Setting by using the given key if the Setting cannot be found in the collection.
        /// </summary>
        /// <param name="key">The unique key of the Setting.</param>
        /// <param name="defaultValue">The default value to use if the Setting cannot be found in the collection.</param>
        /// <returns>A Setting object (whether it be an existing Setting or a new Setting).</returns>
        public Setting GetByKey(string key, object defaultValue)
        {
            return GetByKey(key, defaultValue, createKeyIfNotFound: true);
        }

        /// <summary>
        /// Checks if the setting key exists.
        /// </summary>
        /// <param name="key">The setting key to check.</param>
        /// <returns>Returns true if the key exists. Returns false if the key does not exist.</returns>
        public bool KeyExists(string key)
        {
            foreach (Setting setting in _settingList)
            {
                if (setting.Key.Equals(key))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Loads the settings.
        /// </summary>
        public void Load()
        {
            try
            {
                if (string.IsNullOrEmpty(Filepath)) return;

                if (FileSystem.FileExists(Filepath))
                {
                    // Check the size of the settings file.
                    // Delete the file if it's too big so we don't hang.
                    if (FileSystem.FileContentLength(Filepath) > MAX_FILE_SIZE)
                    {
                        FileSystem.DeleteFile(Filepath);

                        Log.WriteDebugMessage("WARNING: User settings file was too big and needed to be deleted");

                        return;
                    }

                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load(Filepath);

                    AppVersion = xDoc.SelectSingleNode("/autoscreen").Attributes["app:version"]?.Value;
                    AppCodename = xDoc.SelectSingleNode("/autoscreen").Attributes["app:codename"]?.Value;

                    XmlNodeList xSettings = xDoc.SelectNodes(SETTING_XPATH);

                    foreach (XmlNode xSetting in xSettings)
                    {
                        Setting setting = new Setting();
                        XmlNodeReader xReader = new XmlNodeReader(xSetting);

                        while (xReader.Read())
                        {
                            if (xReader.IsStartElement())
                            {
                                switch (xReader.Name)
                                {
                                    case SETTING_KEY:
                                        xReader.Read();
                                        setting.Key = xReader.Value;
                                        break;

                                    case SETTING_VALUE:
                                        xReader.Read();
                                        setting.Value = xReader.Value;
                                        break;
                                }
                            }
                        }

                        xReader.Close();

                        if (!string.IsNullOrEmpty(setting.Key))
                        {
                            Add(setting);
                        }
                    }
                }
                else
                {
                    Log.WriteDebugMessage("WARNING: Unable to load settings");
                }
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("SettingCollection::Load", ex);
            }
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        public bool Save()
        {
            try
            {
                if (string.IsNullOrEmpty(Filepath)) return true;

                XmlWriterSettings xSettings = new XmlWriterSettings();
                xSettings.Indent = true;
                xSettings.CloseOutput = true;
                xSettings.CheckCharacters = true;
                xSettings.Encoding = Encoding.UTF8;
                xSettings.NewLineChars = Environment.NewLine;
                xSettings.IndentChars = XML_FILE_INDENT_CHARS;
                xSettings.NewLineHandling = NewLineHandling.Entitize;
                xSettings.ConformanceLevel = ConformanceLevel.Document;

                if (FileSystem.FileExists(Filepath))
                {
                    FileSystem.DeleteFile(Filepath);
                }

                using (XmlWriter xWriter = XmlWriter.Create(Filepath, xSettings))
                {
                    xWriter.WriteStartDocument();
                    xWriter.WriteStartElement(XML_FILE_ROOT_NODE);
                    xWriter.WriteAttributeString("app", "version", XML_FILE_ROOT_NODE, Settings.ApplicationVersion);
                    xWriter.WriteAttributeString("app", "codename", XML_FILE_ROOT_NODE, Settings.ApplicationCodename);
                    xWriter.WriteStartElement(XML_FILE_SETTINGS_NODE);

                    foreach (object obj in _settingList)
                    {
                        Setting setting = (Setting)obj;

                        xWriter.WriteStartElement(XML_FILE_SETTING_NODE);

                        xWriter.WriteElementString(SETTING_KEY, setting.Key);
                        xWriter.WriteElementString(SETTING_VALUE, setting.Value.ToString());

                        xWriter.WriteEndElement();
                    }

                    xWriter.WriteEndElement();
                    xWriter.WriteEndElement();
                    xWriter.WriteEndDocument();

                    xWriter.Flush();
                    xWriter.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("SettingCollection::Save", ex);

                return false;
            }
        }

        /// <summary>
        /// Attempts an upgrade on a collection of settings that may have come from an old version of the application.
        /// </summary>
        public void Upgrade()
        {
            try
            {
                if (!Settings.VersionManager.IsOldAppVersion(AppCodename, AppVersion)) return;

                Log.WriteMessage("An old version of " + Settings.ApplicationName + " was detected. Attempting upgrade");

                var oldUserSettings = (SettingCollection)this.MemberwiseClone();
                oldUserSettings._settingList = new List<Setting>(_settingList);

                Settings.VersionManager.OldUserSettings = oldUserSettings;

                var versionInConfig = new Version(AppCodename, AppVersion, false);

                if (versionInConfig.VersionString.Equals("2.2.0.0") ||
                    versionInConfig.VersionString.Equals("2.2.0.1") ||
                    versionInConfig.VersionString.Equals("2.2.0.2") ||
                    versionInConfig.VersionString.Equals("2.2.0.3") ||
                    versionInConfig.VersionString.Equals("2.2.0.4") ||
                    versionInConfig.VersionString.Equals("2.2.0.5") ||
                    versionInConfig.VersionString.Equals("2.2.0.6") ||
                    versionInConfig.VersionString.Equals("2.2.0.7") ||
                    versionInConfig.VersionString.Equals("2.2.0.8") ||
                    versionInConfig.VersionString.Equals("2.2.0.9") ||
                    versionInConfig.VersionString.Equals("2.2.0.10") ||
                    versionInConfig.VersionString.Equals("2.2.0.11") ||
                    versionInConfig.VersionString.Equals("2.2.0.12") ||
                    versionInConfig.VersionString.Equals("2.2.0.13") ||
                    versionInConfig.VersionString.Equals("2.2.0.14") ||
                    versionInConfig.VersionString.Equals("2.2.0.15") ||
                    versionInConfig.VersionString.Equals("2.2.0.16"))
                {
                    if (KeyExists("StringPassphrase"))
                    {
                        string passphrase = GetByKey("StringPassphrase", string.Empty, createKeyIfNotFound: false).Value.ToString();

                        if (passphrase.Length > 0)
                        {
                            // Starting with version 2.2.0.17 we now hash the passphrase so if we encounter the passphrase
                            // in an older version of the application then make sure to hash it and lock the session before we continue.
                            SetValueByKey("Passphrase", Security.Hash(passphrase));
                            ScreenCapture.LockScreenCaptureSession = true;
                        }
                    }
                }

                // Go through the old settings and get the old values from them to be used for the new settings.
                SetValueByKey("ScreenCaptureInterval", Convert.ToInt32(GetByKey("Interval", DefaultSettings.ScreenCaptureInterval, createKeyIfNotFound: true).Value));
                SetValueByKey("ScreenCaptureInterval", Convert.ToInt32(GetByKey("IntScreenCaptureInterval", DefaultSettings.ScreenCaptureInterval, createKeyIfNotFound: true).Value));
                SetValueByKey("CaptureLimit", Convert.ToInt32(GetByKey("IntCaptureLimit", DefaultSettings.CaptureLimit, createKeyIfNotFound: true).Value));
                SetValueByKey("CaptureLimitCheck", Convert.ToBoolean(GetByKey("BoolCaptureLimit", DefaultSettings.CaptureLimitCheck, createKeyIfNotFound: true).Value));
                SetValueByKey("TakeInitialScreenshot", Convert.ToBoolean(GetByKey("BoolTakeInitialScreenshot", DefaultSettings.TakeInitialScreenshot, createKeyIfNotFound: true).Value));
                SetValueByKey("TakeInitialScreenshot", Convert.ToBoolean(GetByKey("TakeInitialScreenshotCheck", DefaultSettings.TakeInitialScreenshot, createKeyIfNotFound: true).Value));
                SetValueByKey("ShowSystemTrayIcon", Convert.ToBoolean(GetByKey("BoolShowSystemTrayIcon", DefaultSettings.ShowSystemTrayIcon, createKeyIfNotFound: true).Value));
                SetValueByKey("KeepScreenshotsForDays", Convert.ToInt32(GetByKey("DaysOldWhenRemoveSlides", DefaultSettings.KeepScreenshotsForDays, createKeyIfNotFound: true).Value));
                SetValueByKey("KeepScreenshotsForDays", Convert.ToInt32(GetByKey("IntKeepScreenshotsForDays", DefaultSettings.KeepScreenshotsForDays, createKeyIfNotFound: true).Value));
                SetValueByKey("ScreenshotLabel", Convert.ToString(GetByKey("StringScreenshotLabel", DefaultSettings.ScreenshotLabel, createKeyIfNotFound: true).Value));
                SetValueByKey("ApplyScreenshotLabel", Convert.ToBoolean(GetByKey("BoolApplyScreenshotLabel", DefaultSettings.ApplyScreenshotLabel, createKeyIfNotFound: true).Value));
                SetValueByKey("DefaultEditor", Convert.ToString(GetByKey("StringDefaultEditor", DefaultSettings.DefaultEditor, createKeyIfNotFound: true).Value));
                SetValueByKey("FirstRun", Convert.ToBoolean(GetByKey("BoolFirstRun", DefaultSettings.FirstRun, createKeyIfNotFound: true).Value));
                SetValueByKey("StartScreenCaptureCount", Convert.ToInt32(GetByKey("IntStartScreenCaptureCount", DefaultSettings.StartScreenCaptureCount, createKeyIfNotFound: true).Value));
                SetValueByKey("ActiveWindowTitleCaptureCheck", Convert.ToBoolean(GetByKey("BoolActiveWindowTitleCaptureCheck", DefaultSettings.ActiveWindowTitleCaptureCheck, createKeyIfNotFound: true).Value));
                SetValueByKey("ActiveWindowTitleCaptureText", Convert.ToString(GetByKey("StringActiveWindowTitleCaptureText", DefaultSettings.ActiveWindowTitleCaptureText, createKeyIfNotFound: true).Value));
                SetValueByKey("AutoSaveFolder", Convert.ToString(GetByKey("StringAutoSaveFolder", DefaultSettings.AutoSaveFolder, createKeyIfNotFound: true).Value));
                SetValueByKey("AutoSaveMacro", Convert.ToString(GetByKey("StringAutoSaveMacro", DefaultSettings.AutoSaveMacro, createKeyIfNotFound: true).Value));
                SetValueByKey("UseKeyboardShortcuts", Convert.ToBoolean(GetByKey("BoolUseKeyboardShortcuts", DefaultSettings.UseKeyboardShortcuts, createKeyIfNotFound: true).Value));
                SetValueByKey("KeyboardShortcutStartScreenCaptureModifier1", Convert.ToString(GetByKey("StringKeyboardShortcutStartScreenCaptureModifier1", DefaultSettings.KeyboardShortcutStartScreenCaptureModifier1, createKeyIfNotFound: true).Value));
                SetValueByKey("KeyboardShortcutStartScreenCaptureModifier2", Convert.ToString(GetByKey("StringKeyboardShortcutStartScreenCaptureModifier2", DefaultSettings.KeyboardShortcutStartScreenCaptureModifier2, createKeyIfNotFound: true).Value));
                SetValueByKey("KeyboardShortcutStartScreenCaptureKey", Convert.ToString(GetByKey("StringKeyboardShortcutStartScreenCaptureKey", DefaultSettings.KeyboardShortcutStartScreenCaptureKey, createKeyIfNotFound: true).Value));
                SetValueByKey("KeyboardShortcutStopScreenCaptureModifier1", Convert.ToString(GetByKey("StringKeyboardShortcutStopScreenCaptureModifier1", DefaultSettings.KeyboardShortcutStopScreenCaptureModifier1, createKeyIfNotFound: true).Value));
                SetValueByKey("KeyboardShortcutStopScreenCaptureModifier2", Convert.ToString(GetByKey("StringKeyboardShortcutStopScreenCaptureModifier2", DefaultSettings.KeyboardShortcutStopScreenCaptureModifier2, createKeyIfNotFound: true).Value));
                SetValueByKey("KeyboardShortcutStopScreenCaptureKey", Convert.ToString(GetByKey("StringKeyboardShortcutStopScreenCaptureKey", DefaultSettings.KeyboardShortcutStopScreenCaptureKey, createKeyIfNotFound: true).Value));
                SetValueByKey("KeyboardShortcutCaptureNowArchiveModifier1", Convert.ToString(GetByKey("StringKeyboardShortcutCaptureNowArchiveModifier1", DefaultSettings.KeyboardShortcutCaptureNowArchiveModifier1, createKeyIfNotFound: true).Value));
                SetValueByKey("KeyboardShortcutCaptureNowArchiveModifier2", Convert.ToString(GetByKey("StringKeyboardShortcutCaptureNowArchiveModifier2", DefaultSettings.KeyboardShortcutCaptureNowArchiveModifier2, createKeyIfNotFound: true).Value));
                SetValueByKey("KeyboardShortcutCaptureNowArchiveKey", Convert.ToString(GetByKey("StringKeyboardShortcutCaptureNowArchiveKey", DefaultSettings.KeyboardShortcutCaptureNowArchiveKey, createKeyIfNotFound: true).Value));
                SetValueByKey("KeyboardShortcutCaptureNowEditModifier1", Convert.ToString(GetByKey("StringKeyboardShortcutCaptureNowEditModifier1", DefaultSettings.KeyboardShortcutCaptureNowEditModifier1, createKeyIfNotFound: true).Value));
                SetValueByKey("KeyboardShortcutCaptureNowEditModifier2", Convert.ToString(GetByKey("StringKeyboardShortcutCaptureNowEditModifier2", DefaultSettings.KeyboardShortcutCaptureNowEditModifier2, createKeyIfNotFound: true).Value));
                SetValueByKey("KeyboardShortcutCaptureNowEditKey", Convert.ToString(GetByKey("StringKeyboardShortcutCaptureNowEditKey", DefaultSettings.KeyboardShortcutCaptureNowEditKey, createKeyIfNotFound: true).Value));
                SetValueByKey("KeyboardShortcutRegionSelectClipboardModifier1", Convert.ToString(GetByKey("StringKeyboardShortcutRegionSelectClipboardModifier1", DefaultSettings.KeyboardShortcutRegionSelectClipboardModifier1, createKeyIfNotFound: true).Value));
                SetValueByKey("KeyboardShortcutRegionSelectClipboardModifier2", Convert.ToString(GetByKey("StringKeyboardShortcutRegionSelectClipboardModifier2", DefaultSettings.KeyboardShortcutRegionSelectClipboardModifier2, createKeyIfNotFound: true).Value));
                SetValueByKey("KeyboardShortcutRegionSelectClipboardKey", Convert.ToString(GetByKey("StringKeyboardShortcutRegionSelectClipboardKey", DefaultSettings.KeyboardShortcutRegionSelectClipboardKey, createKeyIfNotFound: true).Value));
                SetValueByKey("KeyboardShortcutRegionSelectAutoSaveModifier1", Convert.ToString(GetByKey("StringKeyboardShortcutRegionSelectAutoSaveModifier1", DefaultSettings.KeyboardShortcutRegionSelectAutoSaveModifier1, createKeyIfNotFound: true).Value));
                SetValueByKey("KeyboardShortcutRegionSelectAutoSaveModifier2", Convert.ToString(GetByKey("StringKeyboardShortcutRegionSelectAutoSaveModifier2", DefaultSettings.KeyboardShortcutRegionSelectAutoSaveModifier2, createKeyIfNotFound: true).Value));
                SetValueByKey("KeyboardShortcutRegionSelectAutoSaveKey", Convert.ToString(GetByKey("StringKeyboardShortcutRegionSelectAutoSaveKey", DefaultSettings.KeyboardShortcutRegionSelectAutoSaveKey, createKeyIfNotFound: true).Value));
                SetValueByKey("KeyboardShortcutRegionSelectEditModifier1", Convert.ToString(GetByKey("StringKeyboardShortcutRegionSelectEditModifier1", DefaultSettings.KeyboardShortcutRegionSelectEditModifier1, createKeyIfNotFound: true).Value));
                SetValueByKey("KeyboardShortcutRegionSelectEditModifier2", Convert.ToString(GetByKey("StringKeyboardShortcutRegionSelectEditModifier2", DefaultSettings.KeyboardShortcutRegionSelectEditModifier2, createKeyIfNotFound: true).Value));
                SetValueByKey("KeyboardShortcutRegionSelectEditKey", Convert.ToString(GetByKey("StringKeyboardShortcutRegionSelectEditKey", DefaultSettings.KeyboardShortcutRegionSelectEditKey, createKeyIfNotFound: true).Value));

                // Remove the old settings.
                RemoveByKey("StringPassphrase");
                RemoveByKey("Interval");
                RemoveByKey("IntScreenCaptureInterval");
                RemoveByKey("IntCaptureLimit");
                RemoveByKey("BoolCaptureLimit");
                RemoveByKey("BoolTakeInitialScreenshot");
                RemoveByKey("TakeInitialScreenshotCheck");
                RemoveByKey("BoolShowSystemTrayIcon");
                RemoveByKey("DaysOldWhenRemoveSlides");
                RemoveByKey("IntKeepScreenshotsForDays");
                RemoveByKey("StringScreenshotLabel");
                RemoveByKey("BoolApplyScreenshotLabel");
                RemoveByKey("StringDefaultEditor");
                RemoveByKey("BoolFirstRun");
                RemoveByKey("IntStartScreenCaptureCount");
                RemoveByKey("BoolActiveWindowTitleCaptureCheck");
                RemoveByKey("StringActiveWindowTitleCaptureText");
                RemoveByKey("StringAutoSaveFolder");
                RemoveByKey("StringAutoSaveMacro");
                RemoveByKey("BoolUseKeyboardShortcuts");
                RemoveByKey("StringKeyboardShortcutStartScreenCaptureModifier1");
                RemoveByKey("StringKeyboardShortcutStartScreenCaptureModifier2");
                RemoveByKey("StringKeyboardShortcutStartScreenCaptureKey");
                RemoveByKey("StringKeyboardShortcutStopScreenCaptureModifier1");
                RemoveByKey("StringKeyboardShortcutStopScreenCaptureModifier2");
                RemoveByKey("StringKeyboardShortcutStopScreenCaptureKey");
                RemoveByKey("StringKeyboardShortcutCaptureNowArchiveModifier1");
                RemoveByKey("StringKeyboardShortcutCaptureNowArchiveModifier2");
                RemoveByKey("StringKeyboardShortcutCaptureNowArchiveKey");
                RemoveByKey("StringKeyboardShortcutCaptureNowEditModifier1");
                RemoveByKey("StringKeyboardShortcutCaptureNowEditModifier2");
                RemoveByKey("StringKeyboardShortcutCaptureNowEditKey");
                RemoveByKey("StringKeyboardShortcutRegionSelectClipboardModifier1");
                RemoveByKey("StringKeyboardShortcutRegionSelectClipboardModifier2");
                RemoveByKey("StringKeyboardShortcutRegionSelectClipboardKey");
                RemoveByKey("StringKeyboardShortcutRegionSelectAutoSaveModifier1");
                RemoveByKey("StringKeyboardShortcutRegionSelectAutoSaveModifier2");
                RemoveByKey("StringKeyboardShortcutRegionSelectAutoSaveKey");
                RemoveByKey("StringKeyboardShortcutRegionSelectEditModifier1");
                RemoveByKey("StringKeyboardShortcutRegionSelectEditModifier2");
                RemoveByKey("StringKeyboardShortcutRegionSelectEditKey");
                RemoveByKey("BoolLockScreenCaptureSession");
                RemoveByKey("BoolCaptureStopAt");
                RemoveByKey("BoolCaptureStartAt");
                RemoveByKey("BoolCaptureOnSunday");
                RemoveByKey("BoolCaptureOnMonday");
                RemoveByKey("BoolCaptureOnTuesday");
                RemoveByKey("BoolCaptureOnWednesday");
                RemoveByKey("BoolCaptureOnThursday");
                RemoveByKey("BoolCaptureOnFriday");
                RemoveByKey("BoolCaptureOnSaturday");
                RemoveByKey("BoolCaptureOnTheseDays");
                RemoveByKey("DateTimeCaptureStopAt");
                RemoveByKey("DateTimeCaptureStartAt");
                RemoveByKey("FilepathLimitLength");
                RemoveByKey("CaptureStopAtCheck");
                RemoveByKey("CaptureStartAtCheck");
                RemoveByKey("CaptureOnSundayCheck");
                RemoveByKey("CaptureOnMondayCheck");
                RemoveByKey("CaptureOnTuesdayCheck");
                RemoveByKey("CaptureOnWednesdayCheck");
                RemoveByKey("CaptureOnThursdayCheck");
                RemoveByKey("CaptureOnFridayCheck");
                RemoveByKey("CaptureOnSaturdayCheck");
                RemoveByKey("CaptureOnTheseDaysCheck");
                RemoveByKey("CaptureStopAtValue");
                RemoveByKey("CaptureStartAtValue");
                RemoveByKey("LockScreenCaptureSession");
                RemoveByKey("ScreenshotsDirectory");
                RemoveByKey("ScheduleImageFormat");
                RemoveByKey("SlideSkip");
                RemoveByKey("ImageResolutionRatio");
                RemoveByKey("ImageFormatFilter");
                RemoveByKey("ImageFormatFilterIndex");
                RemoveByKey("SlideshowDelay");
                RemoveByKey("SlideSkipCheck");
                RemoveByKey("Screen1X");
                RemoveByKey("Screen1Y");
                RemoveByKey("Screen1Width");
                RemoveByKey("Screen1Height");
                RemoveByKey("Screen2X");
                RemoveByKey("Screen2Y");
                RemoveByKey("Screen2Width");
                RemoveByKey("Screen2Height");
                RemoveByKey("Screen3X");
                RemoveByKey("Screen3Y");
                RemoveByKey("Screen3Width");
                RemoveByKey("Screen3Height");
                RemoveByKey("Screen4X");
                RemoveByKey("Screen4Y");
                RemoveByKey("Screen4Width");
                RemoveByKey("Screen4Height");
                RemoveByKey("Screen1Name");
                RemoveByKey("Screen2Name");
                RemoveByKey("Screen3Name");
                RemoveByKey("Screen4Name");
                RemoveByKey("Screen5Name");
                RemoveByKey("Macro");
                RemoveByKey("JpegQualityLevel");
                RemoveByKey("CaptureScreen1");
                RemoveByKey("CaptureScreen2");
                RemoveByKey("CaptureScreen3");
                RemoveByKey("CaptureScreen4");
                RemoveByKey("CaptureActiveWindow");
                RemoveByKey("AutoReset");
                RemoveByKey("Mouse");
                RemoveByKey("StartButtonImageFormat");
                RemoveByKey("Schedule");
                RemoveByKey("DeleteScreenshotsOlderThanDays");
                RemoveByKey("ScreenshotDelay");

                Log.WriteMessage("Upgrade completed.");

                // Now that we've upgraded all the settings we should save them to disk.
                Save();
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("SettingCollection::Upgrade", ex);
            }
        }
    }
}