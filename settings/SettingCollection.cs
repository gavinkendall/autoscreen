//-----------------------------------------------------------------------
// <copyright file="SettingCollection.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

        private string AppCodename { get; set; }
        private string AppVersion { get; set; }

        internal string Filepath { get; set; }

        /// <summary>
        /// 
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

        /// <summary>
        /// 
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

        private void SetValueByKey(string key, object value)
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
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public Setting GetByKey(string key, object defaultValue)
        {
            return GetByKey(key, defaultValue, true);
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

                if (File.Exists(Filepath))
                {
                    FileInfo fileInfo = new FileInfo(Filepath);

                    // Check the size of the settings file.
                    // Delete the file if it's too big so we don't hang.
                    if (fileInfo.Length > MAX_FILE_SIZE)
                    {
                        File.Delete(Filepath);

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
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("SettingCollection::Load", ex);
            }
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        public void Save()
        {
            try
            {
                if (string.IsNullOrEmpty(Filepath)) return;

                XmlWriterSettings xSettings = new XmlWriterSettings();
                xSettings.Indent = true;
                xSettings.CloseOutput = true;
                xSettings.CheckCharacters = true;
                xSettings.Encoding = Encoding.UTF8;
                xSettings.NewLineChars = Environment.NewLine;
                xSettings.IndentChars = XML_FILE_INDENT_CHARS;
                xSettings.NewLineHandling = NewLineHandling.Entitize;
                xSettings.ConformanceLevel = ConformanceLevel.Document;

                if (File.Exists(Filepath))
                {
                    File.Delete(Filepath);
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
                        Setting setting = (Setting) obj;

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
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("SettingCollection::Save", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Upgrade()
        {
            try
            {
                if (!Settings.VersionManager.IsOldAppVersion(AppCodename, AppVersion)) return;

                Log.WriteMessage("An old version of Auto Screen Capture was detected. Attempting upgrade");

                var oldUserSettings = (SettingCollection) this.MemberwiseClone();
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
                            SetValueByKey("StringPassphrase", Security.Hash(passphrase));
                            ScreenCapture.LockScreenCaptureSession = true;
                        }
                    }
                }

                // These settings are no longer used starting with version 2.2.0.17
                RemoveByKey("BoolLockScreenCaptureSession");
                RemoveByKey("Passphrase");

                if (Settings.VersionManager.Versions.Get("Clara", "2.1.8.2") != null && string.IsNullOrEmpty(AppCodename) && string.IsNullOrEmpty(AppVersion))
                {
                    Log.WriteDebugMessage("Accurate version information could not be found so assuming upgrade from 2.1.8.2");

                    // Go through the old settings and get the old values from them to be used for the new settings.

                    // 2.1 used a setting named "Interval", but 2.2 uses "IntScreenCaptureInterval".
                    if (KeyExists("Interval"))
                    {
                        SetValueByKey("IntScreenCaptureInterval",
                            Convert.ToInt32(GetByKey("Interval", 60000, createKeyIfNotFound: false).Value));
                    }

                    // 2.1 used a setting named "CaptureLimit", but 2.2 uses "IntCaptureLimit".
                    if (KeyExists("CaptureLimit"))
                    {
                        SetValueByKey("IntCaptureLimit",
                            Convert.ToInt32(GetByKey("CaptureLimit", 0, createKeyIfNotFound: false).Value));
                    }

                    // 2.1 used a setting named "CaptureLimitCheck", but 2.2 uses "BoolCaptureLimit".
                    if (KeyExists("CaptureLimitCheck"))
                    {
                        SetValueByKey("BoolCaptureLimit",
                            Convert.ToBoolean(GetByKey("CaptureLimitCheck", false, createKeyIfNotFound: false).Value));
                    }

                    // 2.1 used a setting named "TakeInitialScreenshotCheck", but 2.2 uses "BoolTakeInitialScreenshot".
                    if (KeyExists("TakeInitialScreenshotCheck"))
                    {
                        SetValueByKey("BoolTakeInitialScreenshot",
                            Convert.ToBoolean(GetByKey("TakeInitialScreenshotCheck", false, createKeyIfNotFound: false).Value));
                    }

                    // 2.1 used a setting named "ShowSystemTrayIcon", but 2.2 uses "BoolShowSystemTrayIcon".
                    if (KeyExists("ShowSystemTrayIcon"))
                    {
                        SetValueByKey("BoolShowSystemTrayIcon",
                            Convert.ToBoolean(GetByKey("ShowSystemTrayIcon", true, createKeyIfNotFound: false).Value));
                    }

                    // 2.1 used a setting named "CaptureStopAtCheck", but 2.2 uses "BoolCaptureStopAt".
                    if (KeyExists("CaptureStopAtCheck"))
                    {
                        SetValueByKey("BoolCaptureStopAt",
                            Convert.ToBoolean(GetByKey("CaptureStopAtCheck", false, createKeyIfNotFound: false).Value));
                    }

                    // 2.1 used a setting named "CaptureStartAtCheck", but 2.2 uses "BoolCaptureStartAt".
                    if (KeyExists("CaptureStartAtCheck"))
                    {
                        SetValueByKey("BoolCaptureStartAt",
                            Convert.ToBoolean(GetByKey("CaptureStartAtCheck", false, createKeyIfNotFound: false).Value));
                    }

                    // 2.1 used a setting named "CaptureOnSundayCheck", but 2.2 uses "BoolCaptureOnSunday".
                    if (KeyExists("CaptureOnSundayCheck"))
                    {
                        SetValueByKey("BoolCaptureOnSunday",
                            Convert.ToBoolean(GetByKey("CaptureOnSundayCheck", false, createKeyIfNotFound: false).Value));
                    }

                    // 2.1 used a setting named "CaptureOnMondayCheck", but 2.2 uses "BoolCaptureOnMonday".
                    if (KeyExists("CaptureOnMondayCheck"))
                    {
                        SetValueByKey("BoolCaptureOnMonday",
                            Convert.ToBoolean(GetByKey("CaptureOnMondayCheck", false, createKeyIfNotFound: false).Value));
                    }

                    // 2.1 used a setting named "CaptureOnTuesdayCheck", but 2.2 uses "BoolCaptureOnTuesday".
                    if (KeyExists("CaptureOnTuesdayCheck"))
                    {
                        SetValueByKey("BoolCaptureOnTuesday",
                            Convert.ToBoolean(GetByKey("CaptureOnTuesdayCheck", false, createKeyIfNotFound: false).Value));
                    }

                    // 2.1 used a setting named "CaptureOnWednesdayCheck", but 2.2 uses "BoolCaptureOnWednesday".
                    if (KeyExists("CaptureOnWednesdayCheck"))
                    {
                        SetValueByKey("BoolCaptureOnWednesday",
                            Convert.ToBoolean(GetByKey("CaptureOnWednesdayCheck", false, createKeyIfNotFound: false).Value));
                    }

                    // 2.1 used a setting named "CaptureOnThursdayCheck", but 2.2 uses "BoolCaptureOnThursday".
                    if (KeyExists("CaptureOnThursdayCheck"))
                    {
                        SetValueByKey("BoolCaptureOnThursday",
                            Convert.ToBoolean(GetByKey("CaptureOnThursdayCheck", false, createKeyIfNotFound: false).Value));
                    }

                    // 2.1 used a setting named "CaptureOnFridayCheck", but 2.2 uses "BoolCaptureOnFriday".
                    if (KeyExists("CaptureOnFridayCheck"))
                    {
                        SetValueByKey("BoolCaptureOnFriday",
                            Convert.ToBoolean(GetByKey("CaptureOnFridayCheck", false, createKeyIfNotFound: false).Value));
                    }

                    // 2.1 used a setting named "CaptureOnSaturdayCheck", but 2.2 uses "BoolCaptureOnSaturday".
                    if (KeyExists("CaptureOnSaturdayCheck"))
                    {
                        SetValueByKey("BoolCaptureOnSaturday",
                            Convert.ToBoolean(GetByKey("CaptureOnSaturdayCheck", false, createKeyIfNotFound: false).Value));
                    }

                    // 2.1 used a setting named "CaptureOnTheseDaysCheck", but 2.2 uses "BoolCaptureOnTheseDays".
                    if (KeyExists("CaptureOnTheseDaysCheck"))
                    {
                        SetValueByKey("BoolCaptureOnTheseDays",
                            Convert.ToBoolean(GetByKey("CaptureOnTheseDaysCheck", false, createKeyIfNotFound: false).Value));
                    }

                    // 2.1 used a setting named "CaptureStopAtValue", but 2.2 uses "DateTimeCaptureStopAt".
                    if (KeyExists("CaptureStopAtValue"))
                    {
                        SetValueByKey("DateTimeCaptureStopAt",
                            Convert.ToDateTime(GetByKey("CaptureStopAtValue", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 0, 0), createKeyIfNotFound: false).Value));
                    }

                    // 2.1 used a setting named "CaptureStartAtValue", but 2.2 uses "DateTimeCaptureStartAt".
                    if (KeyExists("CaptureStartAtValue"))
                    {
                        SetValueByKey("DateTimeCaptureStartAt",
                            Convert.ToDateTime(GetByKey("CaptureStartAtValue", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0), createKeyIfNotFound: false).Value));
                    }

                    // 2.1 used a setting named "Passphrase", but 2.2 uses "StringPassphrase".
                    if (KeyExists("Passphrase"))
                    {
                        SetValueByKey("StringPassphrase",
                            GetByKey("Passphrase", string.Empty, createKeyIfNotFound: false).Value.ToString());
                    }

                    // 2.1 used a setting named "DaysOldWhenRemoveSlides", but 2.2 uses "IntKeepScreenshotsForDays".
                    if (KeyExists("DaysOldWhenRemoveSlides"))
                    {
                        SetValueByKey("IntKeepScreenshotsForDays",
                            Convert.ToInt32(
                                GetByKey("DaysOldWhenRemoveSlides", 30, createKeyIfNotFound: false).Value));
                    }

                    // Remove the old settings.
                    // When upgrading from 2.1 to 2.2 we should end up with about 20 settings instead of 60
                    // because 2.1 was limited to using Screen 1, Screen 2, Screen 3, Screen 4, and Active Window
                    // so there were settings dedicated to all the properties associated with them (such as Name, X, Y, Width, and Height).
                    RemoveByKey("CaptureLimit");
                    RemoveByKey("CaptureLimitCheck");
                    RemoveByKey("TakeInitialScreenshotCheck");
                    RemoveByKey("ShowSystemTrayIcon");
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
                    RemoveByKey("Passphrase");
                    RemoveByKey("ScreenshotsDirectory");
                    RemoveByKey("ScheduleImageFormat");
                    RemoveByKey("SlideSkip");
                    RemoveByKey("ImageResolutionRatio");
                    RemoveByKey("ImageFormatFilter");
                    RemoveByKey("ImageFormatFilterIndex");
                    RemoveByKey("Interval");
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
                    RemoveByKey("DaysOldWhenRemoveSlides");
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
                }

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