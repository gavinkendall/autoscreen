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
        private Security _security;
        private List<Setting> _settingList;

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
        /// A class for handling a collection of settings.
        /// </summary>
        public SettingCollection()
        {
            _security = new Security();
            _settingList = new List<Setting>();

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
            Setting foundSetting = GetByKey(key);

            if (foundSetting == null && createKeyIfNotFound)
            {
                Setting newSetting = new Setting(key, defaultValue);
                Add(newSetting);

                return newSetting;
            }

            return foundSetting;
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
        /// Gets a Setting by its unique key.
        /// </summary>
        /// <param name="key">The unique key of the Setting.</param>
        /// <returns></returns>
        public Setting GetByKey(string key)
        {
            foreach (Setting setting in _settingList)
            {
                if (setting.Key.Equals(key))
                {
                    return setting;
                }
            }

            return null;
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
        /// Renames an old key to a new key while retaining the value from the old key.
        /// </summary>
        /// <param name="oldKey">The name of the old key.</param>
        /// <param name="newKey">The name of the new key.</param>
        public void RenameKey(string oldKey, string newKey)
        {
            if (KeyExists(oldKey))
            {
                Setting oldSetting = GetByKey(oldKey);

                if (oldSetting == null)
                {
                    return;
                }

                object value = oldSetting.Value;

                if (value == null)
                {
                    return;
                }

                SetValueByKey(newKey, value);

                RemoveByKey(oldKey);
            }
        }

        /// <summary>
        /// Loads the settings.
        /// </summary>
        public void Load(Settings settings, FileSystem fileSystem)
        {
            if (string.IsNullOrEmpty(Filepath))
            {
                return;
            }

            if (!fileSystem.FileExists(Filepath))
            {
                Save(settings, fileSystem);
            }

            if (fileSystem.FileExists(Filepath))
            {
                // Check the size of the settings file.
                // Delete the file if it's too big so we don't hang.
                if (fileSystem.FileContentLength(Filepath) > MAX_FILE_SIZE)
                {
                    fileSystem.DeleteFile(Filepath);

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
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        public bool Save(Settings settings, FileSystem fileSystem)
        {
            try
            {
                if (string.IsNullOrEmpty(Filepath))
                {
                    return true;
                }

                XmlWriterSettings xSettings = new XmlWriterSettings();
                xSettings.Indent = true;
                xSettings.CloseOutput = true;
                xSettings.CheckCharacters = true;
                xSettings.Encoding = Encoding.UTF8;
                xSettings.NewLineChars = Environment.NewLine;
                xSettings.IndentChars = XML_FILE_INDENT_CHARS;
                xSettings.NewLineHandling = NewLineHandling.Entitize;
                xSettings.ConformanceLevel = ConformanceLevel.Document;

                if (fileSystem.FileExists(Filepath))
                {
                    fileSystem.DeleteFile(Filepath);
                }

                using (XmlWriter xWriter = XmlWriter.Create(Filepath, xSettings))
                {
                    xWriter.WriteStartDocument();
                    xWriter.WriteStartElement(XML_FILE_ROOT_NODE);
                    xWriter.WriteAttributeString("app", "version", XML_FILE_ROOT_NODE, settings.ApplicationVersion);
                    xWriter.WriteAttributeString("app", "codename", XML_FILE_ROOT_NODE, settings.ApplicationCodename);
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
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Attempts an upgrade on a collection of settings that may have come from an old version of the application.
        /// </summary>
        public void Upgrade(ScreenCapture screenCapture, Config config, FileSystem fileSystem, Log log)
        {
            if (!config.Settings.VersionManager.IsOldAppVersion(config.Settings, AppCodename, AppVersion))
            {
                return;
            }

            var oldUserSettings = (SettingCollection)this.MemberwiseClone();
            oldUserSettings._settingList = new List<Setting>(_settingList);

            config.Settings.VersionManager.OldUserSettings = oldUserSettings;

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

                    passphrase = passphrase.Trim();

                    if (passphrase.Length > 0)
                    {
                        // Starting with version 2.2.0.17 we now hash the passphrase so if we encounter the passphrase
                        // in an older version of the application then make sure to hash it and lock the session before we continue.
                        SetValueByKey("Passphrase", _security.Hash(passphrase));
                        screenCapture.LockScreenCaptureSession = true;
                    }
                }
            }

            // Go through the old settings and get the old values from them to be used for the new settings.
            RenameKey("Interval", "ScreenCaptureInterval");
            RenameKey("IntScreenCaptureInterval", "ScreenCaptureInterval");
            RenameKey("IntCaptureLimit", "CaptureLimit");
            RenameKey("BoolCaptureLimit", "CaptureLimitCheck");
            RenameKey("BoolTakeInitialScreenshot", "TakeInitialScreenshot");
            RenameKey("TakeInitialScreenshotCheck", "TakeInitialScreenshot");
            RenameKey("BoolShowSystemTrayIcon", "ShowSystemTrayIcon");
            RenameKey("DaysOldWhenRemoveSlides", "KeepScreenshotsForDays");
            RenameKey("IntKeepScreenshotsForDays", "KeepScreenshotsForDays");
            RenameKey("StringPassphrase", "Passphrase");
            RenameKey("StringScreenshotLabel", "ScreenshotLabel");
            RenameKey("BoolApplyScreenshotLabel", "ApplyScreenshotLabel");
            RenameKey("StringDefaultEditor", "DefaultEditor");
            RenameKey("BoolFirstRun", "FirstRun");
            RenameKey("IntStartScreenCaptureCount", "StartScreenCaptureCount");
            RenameKey("BoolActiveWindowTitleCaptureCheck", "ActiveWindowTitleCaptureCheck");
            RenameKey("StringActiveWindowTitleCaptureText", "ActiveWindowTitleCaptureText");
            RenameKey("StringAutoSaveFolder", "AutoSaveFolder");
            RenameKey("StringAutoSaveMacro", "AutoSaveMacro");
            RenameKey("BoolUseKeyboardShortcuts", "UseKeyboardShortcuts");

            // Keyboard Shortcuts
            RenameKey("StringKeyboardShortcutStartScreenCaptureModifier1", "KeyboardShortcutStartScreenCaptureModifier1");
            RenameKey("StringKeyboardShortcutStartScreenCaptureModifier2", "KeyboardShortcutStartScreenCaptureModifier2");
            RenameKey("StringKeyboardShortcutStartScreenCaptureKey", "KeyboardShortcutStartScreenCaptureKey");
            RenameKey("StringKeyboardShortcutStopScreenCaptureModifier1", "KeyboardShortcutStopScreenCaptureModifier1");
            RenameKey("StringKeyboardShortcutStopScreenCaptureModifier2", "KeyboardShortcutStopScreenCaptureModifier2");
            RenameKey("StringKeyboardShortcutStopScreenCaptureKey", "KeyboardShortcutStopScreenCaptureKey");
            RenameKey("StringKeyboardShortcutCaptureNowArchiveModifier1", "KeyboardShortcutCaptureNowArchiveModifier1");
            RenameKey("StringKeyboardShortcutCaptureNowArchiveModifier2", "KeyboardShortcutCaptureNowArchiveModifier2");
            RenameKey("StringKeyboardShortcutCaptureNowArchiveKey", "KeyboardShortcutCaptureNowArchiveKey");
            RenameKey("StringKeyboardShortcutCaptureNowEditModifier1", "KeyboardShortcutCaptureNowEditModifier1");
            RenameKey("StringKeyboardShortcutCaptureNowEditModifier2", "KeyboardShortcutCaptureNowEditModifier2");
            RenameKey("StringKeyboardShortcutCaptureNowEditKey", "KeyboardShortcutCaptureNowEditKey");
            RenameKey("StringKeyboardShortcutRegionSelectClipboardModifier1", "KeyboardShortcutRegionSelectClipboardModifier1");
            RenameKey("StringKeyboardShortcutRegionSelectClipboardModifier2", "KeyboardShortcutRegionSelectClipboardModifier2");
            RenameKey("StringKeyboardShortcutRegionSelectClipboardKey", "KeyboardShortcutRegionSelectClipboardKey");
            RenameKey("StringKeyboardShortcutRegionSelectAutoSaveModifier1", "KeyboardShortcutRegionSelectAutoSaveModifier1");
            RenameKey("StringKeyboardShortcutRegionSelectAutoSaveModifier2", "KeyboardShortcutRegionSelectAutoSaveModifier2");
            RenameKey("StringKeyboardShortcutRegionSelectAutoSaveKey", "KeyboardShortcutRegionSelectAutoSaveKey");
            RenameKey("StringKeyboardShortcutRegionSelectEditModifier1", "KeyboardShortcutRegionSelectEditModifier1");
            RenameKey("StringKeyboardShortcutRegionSelectEditModifier2", "KeyboardShortcutRegionSelectEditModifier2");
            RenameKey("StringKeyboardShortcutRegionSelectEditKey", "KeyboardShortcutRegionSelectEditKey");

            // Remove the old settings.
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

            // Now that we've upgraded all the settings we should save them to disk.
            Save(config.Settings, fileSystem);
        }
    }
}