//-----------------------------------------------------------------------
// <copyright file="TriggerCollection.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A collection of triggers.</summary>
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
        private const string TRIGGER_ACTIVE = "active";

        private readonly string TRIGGER_XPATH;

        private static string AppCodename { get; set; }
        private static string AppVersion { get; set; }

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
        public void LoadXmlFileAndAddTriggers()
        {
            try
            {
                if (FileSystem.FileExists(FileSystem.TriggersFile))
                {
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load(FileSystem.TriggersFile);

                    AppVersion = xDoc.SelectSingleNode("/autoscreen").Attributes["app:version"]?.Value;
                    AppCodename = xDoc.SelectSingleNode("/autoscreen").Attributes["app:codename"]?.Value;

                    XmlNodeList xTriggers = xDoc.SelectNodes(TRIGGER_XPATH);

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
                                        trigger.ConditionType =
                                            (TriggerConditionType) Enum.Parse(typeof(TriggerConditionType), xReader.Value);
                                        break;

                                    case TRIGGER_ACTION:
                                        xReader.Read();
                                        trigger.ActionType =
                                            (TriggerActionType) Enum.Parse(typeof(TriggerActionType), xReader.Value);
                                        break;

                                    case TRIGGER_EDITOR:
                                        xReader.Read();
                                        trigger.Editor = xReader.Value;
                                        break;

                                    case TRIGGER_ACTIVE:
                                        xReader.Read();
                                        trigger.Active = Convert.ToBoolean(xReader.Value);
                                        break;
                                }
                            }
                        }

                        xReader.Close();

                        // Change the data for each Trigger that's being loaded if we've detected that
                        // the XML document is from an older version of the application.
                        if (Settings.VersionManager.IsOldAppVersion(AppCodename, AppVersion))
                        {
                            Log.WriteDebugMessage("An old version of the triggers.xml file was detected. Attempting upgrade to new schema.");

                            Version v2300 = Settings.VersionManager.Versions.Get("Boombayah", "2.3.0.0");
                            Version configVersion = Settings.VersionManager.Versions.Get(AppCodename, AppVersion);

                            if (v2300 != null && configVersion != null && configVersion.VersionNumber < v2300.VersionNumber)
                            {
                                Log.WriteDebugMessage("Dalek 2.2.4.6 or older detected");

                                // This is a new property for Trigger that was introduced in 2.3.0.0
                                // so any version before 2.3.0.0 needs to have it during an upgrade.
                                trigger.Active = true;
                            }
                        }

                        if (!string.IsNullOrEmpty(trigger.Name))
                        {
                            Add(trigger);
                        }
                    }

                    if (Settings.VersionManager.IsOldAppVersion(AppCodename, AppVersion))
                    {
                        SaveToXmlFile();
                    }
                }
                else
                {
                    Log.WriteDebugMessage($"WARNING: {FileSystem.TriggersFile} not found. Creating default triggers");

                    // Setup a few "built in" triggers by default.
                    Add(new Trigger("Application Startup -> Show", TriggerConditionType.ApplicationStartup,
                        TriggerActionType.ShowInterface, string.Empty, active: true));

                    Add(new Trigger("Capture Started -> Hide", TriggerConditionType.ScreenCaptureStarted,
                        TriggerActionType.HideInterface, string.Empty, active: true));

                    Add(new Trigger("Capture Stopped -> Show", TriggerConditionType.ScreenCaptureStopped,
                        TriggerActionType.ShowInterface, string.Empty, active: true));

                    Add(new Trigger("Interface Closing -> Hide", TriggerConditionType.InterfaceClosing,
                        TriggerActionType.HideInterface, string.Empty, active: true));

                    Add(new Trigger("Limit Reached -> Stop", TriggerConditionType.LimitReached,
                        TriggerActionType.StopScreenCapture, string.Empty, active: true));

                    SaveToXmlFile();
                }
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("TriggerCollection::LoadXmlFileAndAddTriggers", ex);
            }
        }

        /// <summary>
        /// Saves the triggers.
        /// </summary>
        public void SaveToXmlFile()
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

                if (string.IsNullOrEmpty(FileSystem.TriggersFile))
                {
                    FileSystem.TriggersFile = FileSystem.DefaultTriggersFile;

                    if (FileSystem.FileExists(FileSystem.ConfigFile))
                    {
                        FileSystem.AppendToFile(FileSystem.ConfigFile, "\nTriggersFile=" + FileSystem.TriggersFile);
                    }
                }

                if (FileSystem.FileExists(FileSystem.TriggersFile))
                {
                    FileSystem.DeleteFile(FileSystem.TriggersFile);
                }

                using (XmlWriter xWriter =
                    XmlWriter.Create(FileSystem.TriggersFile, xSettings))
                {
                    xWriter.WriteStartDocument();
                    xWriter.WriteStartElement(XML_FILE_ROOT_NODE);
                    xWriter.WriteAttributeString("app", "version", XML_FILE_ROOT_NODE, Settings.ApplicationVersion);
                    xWriter.WriteAttributeString("app", "codename", XML_FILE_ROOT_NODE, Settings.ApplicationCodename);
                    xWriter.WriteStartElement(XML_FILE_TRIGGERS_NODE);

                    foreach (Trigger trigger in base.Collection)
                    {
                        xWriter.WriteStartElement(XML_FILE_TRIGGER_NODE);

                        xWriter.WriteElementString(TRIGGER_ACTIVE, trigger.Active.ToString());
                        xWriter.WriteElementString(TRIGGER_NAME, trigger.Name);
                        xWriter.WriteElementString(TRIGGER_CONDITION, trigger.ConditionType.ToString());
                        xWriter.WriteElementString(TRIGGER_ACTION, trigger.ActionType.ToString());
                        xWriter.WriteElementString(TRIGGER_EDITOR, trigger.Editor);

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
                Log.WriteExceptionMessage("TriggerCollection::SaveToXmlFile", ex);
            }
        }
    }
}