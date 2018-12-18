//-----------------------------------------------------------------------
// <copyright file="SettingCollection.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml;

    public class SettingCollection : IEnumerable<Setting>
    {
        private readonly List<Setting> _settingList = new List<Setting>();

        private const string XML_FILE_INDENT_CHARS = "   ";
        private const string XML_FILE_SETTING_NODE = "setting";
        private const string XML_FILE_SETTINGS_NODE = "settings";
        private const string XML_FILE_ROOT_NODE = "autoscreen";

        private const string SETTING_KEY = "key";
        private const string SETTING_VALUE = "value";
        private const string SETTING_XPATH = "/" + XML_FILE_ROOT_NODE + "/" + XML_FILE_SETTINGS_NODE + "/" + XML_FILE_SETTING_NODE;

        public string Filepath { get; set; }

        public List<Setting>.Enumerator GetEnumerator()
        {
            return _settingList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Setting>)_settingList).GetEnumerator();
        }

        IEnumerator<Setting> IEnumerable<Setting>.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(Setting setting)
        {
            _settingList.Add(setting);
        }

        /// <summary>
        /// Gets a setting by its key.
        /// If the setting is found it will return the Setting object.
        /// If the setting is not found a new Setting will be created with the provided key and default value.
        /// </summary>
        /// <param name="key">The key to use for finding an existing Setting or for creating a new Setting.</param>
        /// <param name="defaultValue">The default value to use if the Setting cannot be found.</param>
        /// <returns>Setting object (either existing or new).</returns>
        public Setting GetByKey(string key, object defaultValue)
        {
            foreach (Setting setting in _settingList)
            {
                if (setting.Key.Equals(key))
                {
                    return setting;
                }
            }

            Setting newSetting = new Setting(key, defaultValue);
            Add(newSetting);

            return newSetting;
        }

        /// <summary>
        /// Loads the settings.
        /// </summary>
        public void Load()
        {
            if (File.Exists(Filepath))
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(Filepath);

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
        public void Save()
        {
            XmlWriterSettings xSettings = new XmlWriterSettings();
            xSettings.Indent = true;
            xSettings.CloseOutput = true;
            xSettings.CheckCharacters = true;
            xSettings.Encoding = Encoding.UTF8;
            xSettings.NewLineChars = Environment.NewLine;
            xSettings.IndentChars = XML_FILE_INDENT_CHARS;
            xSettings.NewLineHandling = NewLineHandling.Entitize;
            xSettings.ConformanceLevel = ConformanceLevel.Document;

            using (XmlWriter xWriter = XmlWriter.Create(Filepath, xSettings))
            {
                xWriter.WriteStartDocument();
                xWriter.WriteStartElement(XML_FILE_ROOT_NODE);
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
        }
    }
}