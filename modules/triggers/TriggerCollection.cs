//-----------------------------------------------------------------------
// <copyright file="TriggerCollection.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A collection of triggers.</summary>
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
using System.Text;
using System.Xml;

namespace AutoScreenCapture
{
    /// <summary>
    /// A collection class to store and manage Trigger objects.
    /// </summary>
    public class TriggerCollection : CollectionTemplate<Trigger>
    {
        private const string XML_FILE_INDENT_CHARS = "   ";
        private const string XML_FILE_TRIGGER_NODE = "trigger";
        private const string XML_FILE_TRIGGERS_NODE = "triggers";
        private const string XML_FILE_ROOT_NODE = "autoscreen";

        private const string TRIGGER_NAME = "name";
        private const string TRIGGER_CONDITION = "condition";
        private const string TRIGGER_ACTION = "action";
        private const string TRIGGER_EDITOR = "editor";
        private const string TRIGGER_ENABLE = "enable";
        private const string TRIGGER_DATE = "date";
        private const string TRIGGER_TIME = "time";
        private const string TRIGGER_DAY = "day";
        private const string TRIGGER_DAYS = "days";
        private const string TRIGGER_SCREEN_CAPTURE_INTERVAL = "screen_capture_interval";
        
        // 2.3.3.9 and older
        private const string TRIGGER_MODULE_ITEM = "module_item";

        // 2.3.4.0 and newer
        private const string TRIGGER_VALUE = "value";

        // 2.4.1.9 introduced Cycle Count for Trigger
        private const string TRIGGER_CYCLE_COUNT = "cycle_count";

        // 2.4.2.3
        private const string TRIGGER_DURATION = "duration";
        private const string TRIGGER_DURATION_TYPE = "duration_type";

        private readonly string TRIGGER_XPATH;

        private string AppCodename { get; set; }
        private string AppVersion { get; set; }

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public TriggerCollection()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("/");
            sb.Append(XML_FILE_ROOT_NODE);
            sb.Append("/");
            sb.Append(XML_FILE_TRIGGERS_NODE);
            sb.Append("/");
            sb.Append(XML_FILE_TRIGGER_NODE);

            TRIGGER_XPATH = sb.ToString();
        }

        /// <summary>
        /// Loads the triggers.
        /// </summary>
        public bool LoadXmlFileAndAddTriggers(Config config, FileSystem fileSystem, Log log)
        {
            try
            {
                if (fileSystem.FileExists(fileSystem.TriggersFile))
                {
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load(fileSystem.TriggersFile);

                    AppVersion = xDoc.SelectSingleNode("/autoscreen").Attributes["app:version"]?.Value;
                    AppCodename = xDoc.SelectSingleNode("/autoscreen").Attributes["app:codename"]?.Value;

                    XmlNodeList xTriggers = xDoc.SelectNodes(TRIGGER_XPATH);

                    Version v2300 = null;
                    Version v2340 = null;
                    Version v2404 = null;
                    Version v2419 = null;
                    Version configVersion = null;

                    // Change the data for each Trigger that's being loaded if we've detected that
                    // the XML document is from an older version of the application.
                    if (config.Settings.VersionManager.IsOldAppVersion(config.Settings, AppCodename, AppVersion))
                    {
                        log.WriteDebugMessage("An old version of the triggers.xml file was detected. Attempting upgrade to new schema.");

                        v2300 = config.Settings.VersionManager.Versions.Get(Settings.CODENAME_BOOMBAYAH, Settings.CODEVERSION_BOOMBAYAH);
                        v2340 = config.Settings.VersionManager.Versions.Get(Settings.CODENAME_BOOMBAYAH, "2.3.4.0");
                        v2404 = config.Settings.VersionManager.Versions.Get(Settings.CODENAME_BLADE, "2.4.0.4");
                        v2419 = config.Settings.VersionManager.Versions.Get(Settings.CODENAME_BLADE, "2.4.1.9");

                        configVersion = config.Settings.VersionManager.Versions.Get(AppCodename, AppVersion);
                    }

                    foreach (XmlNode xTrigger in xTriggers)
                    {
                        Trigger trigger = new Trigger();
                        XmlNodeReader xReader = new XmlNodeReader(xTrigger);

                        while (xReader.Read())
                        {
                            if (xReader.IsStartElement() && !xReader.IsEmptyElement)
                            {
                                switch (xReader.Name)
                                {
                                    case TRIGGER_NAME:
                                        xReader.Read();
                                        trigger.Name = xReader.Value;
                                        break;

                                    case TRIGGER_CONDITION:
                                        xReader.Read();

                                        string nodeValue = xReader.Value;

                                        if (v2340 != null && configVersion != null && configVersion.VersionNumber < v2340.VersionNumber)
                                        {
                                           // 2.3.4.0 changes the trigger condition of "ScreenshotTaken" to "AfterScreenshotTaken"
                                            // because of the new condition "BeforeScreenshotTaken" introduced so now we separate those conditions.
                                            if (nodeValue.Equals("ScreenshotTaken"))
                                            {
                                                log.WriteDebugMessage("Boombayah 2.3.3.4 or older detected when parsing for trigger condition type ScreenshotsTaken so converting to condition type to AfterScreenshotTaken");
                                                nodeValue = TriggerConditionType.AfterScreenshotTaken.ToString();
                                            }
                                        }

                                        TriggerConditionType triggerConditionType;

                                        if (Enum.TryParse(nodeValue, out triggerConditionType))
                                        {
                                            trigger.ConditionType = triggerConditionType;
                                        }
                                        
                                        break;

                                    case TRIGGER_ACTION:
                                        xReader.Read();

                                        string triggerActionNode = xReader.Value;

                                        if (v2419 != null && configVersion != null && configVersion.VersionNumber < v2419.VersionNumber)
                                        {
                                            if (triggerActionNode.Equals("DeleteScreenshots"))
                                            {
                                                log.WriteDebugMessage("Blade 2.4.1.8 or older detected when parsing for trigger action type DeleteScreenshots so converting action type to DeleteScreenshotsByDays");
                                                triggerActionNode = TriggerActionType.DeleteScreenshotsByDays.ToString();
                                            }
                                        }
                                        
                                        TriggerActionType triggerActionType;

                                        if (Enum.TryParse(triggerActionNode, out triggerActionType))
                                        {
                                            trigger.ActionType = triggerActionType;
                                        }

                                        break;

                                    // This is purely for backwards compatibility with older versions.
                                    // We no longer use Editor. We use ModuleItem now (since 2.3.0.0).
                                    case TRIGGER_EDITOR:
                                        xReader.Read();
                                        trigger.Value = xReader.Value;
                                        break;

                                    case TRIGGER_ENABLE:
                                    case "active": // Any version older than 2.4.0.0 used "active" instead of "enable".
                                        xReader.Read();
                                        trigger.Enable = Convert.ToBoolean(xReader.Value);
                                        break;

                                    case TRIGGER_DATE:
                                        xReader.Read();
                                        trigger.Date = Convert.ToDateTime(xReader.Value);
                                        break;

                                    case TRIGGER_TIME:
                                        xReader.Read();
                                        trigger.Time = Convert.ToDateTime(xReader.Value);
                                        break;

                                    case TRIGGER_DAY:
                                        xReader.Read();
                                        trigger.Day = xReader.Value;
                                        break;

                                    case TRIGGER_DAYS:
                                        xReader.Read();
                                        trigger.Days = Convert.ToInt32(xReader.Value);
                                        break;

                                    case TRIGGER_SCREEN_CAPTURE_INTERVAL:
                                        xReader.Read();
                                        trigger.ScreenCaptureInterval = Convert.ToInt32(xReader.Value);
                                        break;

                                        // 2.3.3.9 and older
                                    case TRIGGER_MODULE_ITEM:
                                        xReader.Read();
                                        trigger.Value = xReader.Value;
                                        break;

                                        // 2.3.4.0 and newer
                                    case TRIGGER_VALUE:
                                        xReader.Read();
                                        trigger.Value = xReader.Value;
                                        break;

                                        // 2.4.1.9
                                    case TRIGGER_CYCLE_COUNT:
                                        xReader.Read();
                                        trigger.CycleCount = Convert.ToInt32(xReader.Value);
                                        break;

                                        // 2.4.2.3
                                    case TRIGGER_DURATION:
                                        xReader.Read();
                                        trigger.Duration = Convert.ToInt32(xReader.Value);
                                        break;

                                        // 2.4.2.3
                                    case TRIGGER_DURATION_TYPE:
                                        xReader.Read();
                                        trigger.DurationType = Convert.ToInt32(xReader.Value);
                                        break;
                                }
                            }
                        }

                        xReader.Close();

                        if (v2300 != null && configVersion != null && configVersion.VersionNumber < v2300.VersionNumber)
                        {
                            log.WriteDebugMessage("Dalek 2.2.4.6 or older detected");

                            // These are new properties for Trigger that were introduced in 2.3.0.0
                            // so any version before 2.3.0.0 needs to have them during an upgrade.
                            trigger.Enable = true;
                            trigger.Date = DateTime.Now;
                            trigger.Time = DateTime.Now;
                            trigger.ScreenCaptureInterval = 0;
                        }

                        if (!string.IsNullOrEmpty(trigger.Name))
                        {
                            Add(trigger);
                        }
                    }

                    if (v2340 != null && configVersion != null && configVersion.VersionNumber < v2340.VersionNumber)
                    {
                        log.WriteDebugMessage("Boombayah 2.3.3.4 or older detected");

                        int days = 30;

                        if (config.Settings.VersionManager.OldUserSettings.KeyExists("DaysOldWhenRemoveSlides"))
                        {
                            days = Convert.ToInt32(config.Settings.VersionManager.OldUserSettings.GetByKey("DaysOldWhenRemoveSlides").Value.ToString());
                        }

                        if (config.Settings.VersionManager.OldUserSettings.KeyExists("IntKeepScreenshotsForDays"))
                        {
                            days = Convert.ToInt32(config.Settings.VersionManager.OldUserSettings.GetByKey("IntKeepScreenshotsForDays").Value.ToString());
                        }

                        if (config.Settings.VersionManager.OldUserSettings.KeyExists("KeepScreenshotsForDays"))
                        {
                            days = Convert.ToInt32(config.Settings.VersionManager.OldUserSettings.GetByKey("KeepScreenshotsForDays").Value.ToString());
                        }

                        Trigger triggerBeforeScreenshotSavedDeleteScreenshots = new Trigger()
                        {
                            Enable = false,
                            Name = "Delete Screenshots " + days + " Days Old",
                            ConditionType = TriggerConditionType.BeforeScreenshotReferencesSaved,
                            ActionType = TriggerActionType.DeleteScreenshotsByDays,
                            Date = DateTime.Now,
                            Time = DateTime.Now,
                            ScreenCaptureInterval = 0,
                            Days = days
                        };

                        Add(triggerBeforeScreenshotSavedDeleteScreenshots);
                    }

                    if (v2404 != null && configVersion != null && configVersion.VersionNumber <= v2404.VersionNumber)
                    {
                        log.WriteDebugMessage("Blade 2.4.0.4 or older detected");

                        Trigger triggerSystemTrayIconDoubleClickShowOrHideInterface = new Trigger()
                        {
                            Enable = true,
                            Name = "Icon Double Click",
                            ConditionType = TriggerConditionType.SystemTrayIconDoubleClick,
                            ActionType = TriggerActionType.ShowOrHideInterface,
                            Date = DateTime.Now,
                            Time = DateTime.Now,
                            ScreenCaptureInterval = 0
                        };

                        Add(triggerSystemTrayIconDoubleClickShowOrHideInterface);
                    }

                    if (config.Settings.VersionManager.IsOldAppVersion(config.Settings, AppCodename, AppVersion))
                    {
                        SaveToXmlFile(config, fileSystem, log);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                if (fileSystem.FileExists(fileSystem.TriggersFile))
                {
                    fileSystem.DeleteFile(fileSystem.TriggersFile);

                    log.WriteErrorMessage("The file \"" + fileSystem.TriggersFile + "\" had to be deleted because an error was encountered. You may need to force quit the application and run it again.");
                }

                log.WriteExceptionMessage("TriggerCollection::LoadXmlFileAndAddTriggers", ex);

                return false;
            }
        }

        /// <summary>
        /// Saves the triggers.
        /// </summary>
        public bool SaveToXmlFile(Config config, FileSystem fileSystem, Log log)
        {
            try
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

                fileSystem.DeleteFile(fileSystem.TriggersFile);

                using (XmlWriter xWriter = XmlWriter.Create(fileSystem.TriggersFile, xSettings))
                {
                    xWriter.WriteStartDocument();
                    xWriter.WriteStartElement(XML_FILE_ROOT_NODE);
                    xWriter.WriteAttributeString("app", "version", XML_FILE_ROOT_NODE, config.Settings.ApplicationVersion);
                    xWriter.WriteAttributeString("app", "codename", XML_FILE_ROOT_NODE, config.Settings.ApplicationCodename);
                    xWriter.WriteStartElement(XML_FILE_TRIGGERS_NODE);

                    foreach (Trigger trigger in base.Collection)
                    {
                        xWriter.WriteStartElement(XML_FILE_TRIGGER_NODE);

                        xWriter.WriteElementString(TRIGGER_ENABLE, trigger.Enable.ToString());
                        xWriter.WriteElementString(TRIGGER_NAME, trigger.Name);
                        xWriter.WriteElementString(TRIGGER_CONDITION, trigger.ConditionType.ToString());
                        xWriter.WriteElementString(TRIGGER_ACTION, trigger.ActionType.ToString());
                        xWriter.WriteElementString(TRIGGER_DATE, trigger.Date.ToString());
                        xWriter.WriteElementString(TRIGGER_TIME, trigger.Time.ToString());
                        xWriter.WriteElementString(TRIGGER_DAY, string.IsNullOrEmpty(trigger.Day) ? "Weekday" : trigger.Day.ToString());
                        xWriter.WriteElementString(TRIGGER_DAYS, trigger.Days.ToString());
                        xWriter.WriteElementString(TRIGGER_SCREEN_CAPTURE_INTERVAL, trigger.ScreenCaptureInterval.ToString());
                        xWriter.WriteElementString(TRIGGER_VALUE, trigger.Value);
                        xWriter.WriteElementString(TRIGGER_CYCLE_COUNT, trigger.CycleCount.ToString());
                        xWriter.WriteElementString(TRIGGER_DURATION, trigger.Duration.ToString());
                        xWriter.WriteElementString(TRIGGER_DURATION_TYPE, trigger.DurationType.ToString());

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
                log.WriteExceptionMessage("TriggerCollection::SaveToXmlFile", ex);

                return false;
            }
        }
    }
}