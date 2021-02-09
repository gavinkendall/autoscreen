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

        /// <summary>
        /// Clones the current list of settings and returns the cloned list of settings.
        /// </summary>
        /// <returns>The cloned list of settings.</returns>
        public SettingCollection Clone()
        {
            var clonedSettings = (SettingCollection)this.MemberwiseClone();
            clonedSettings._settingList = new List<Setting>(_settingList);

            return clonedSettings;
        }

        /// <summary>
        /// Removes a Setting from the collection given its key.
        /// </summary>
        /// <param name="key">The key to use for removing the setting from the collection.</param>
        public void RemoveByKey(string key)
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
        /// <param name="setting">The Setting to add to the collection.</param>
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
            Log log = new Log(fileSystem, new MacroParser(settings));

            try
            {
                if (string.IsNullOrEmpty(Filepath)) return;

                if (fileSystem.FileExists(Filepath))
                {
                    // Check the size of the settings file.
                    // Delete the file if it's too big so we don't hang.
                    if (fileSystem.FileContentLength(Filepath) > MAX_FILE_SIZE)
                    {
                        fileSystem.DeleteFile(Filepath);

                        log.WriteDebugMessage("WARNING: User settings file was too big and needed to be deleted");

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
                    log.WriteDebugMessage("WARNING: Unable to load settings");
                }
            }
            catch (Exception ex)
            {
                log.WriteExceptionMessage("SettingCollection::Load", ex);
            }
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        public bool Save(Settings settings, FileSystem fileSystem, Log log)
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
            catch (Exception ex)
            {
                log.WriteExceptionMessage("SettingCollection::Save", ex);

                return false;
            }
        }
    }
}