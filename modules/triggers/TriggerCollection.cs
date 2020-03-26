//-----------------------------------------------------------------------
// <copyright file="TriggerCollection.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
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

        private const string TRIGGER_XPATH = "/" + XML_FILE_ROOT_NODE + "/" + XML_FILE_TRIGGERS_NODE + "/" + XML_FILE_TRIGGER_NODE;

        private static string AppCodename { get; set; }
        private static string AppVersion { get; set; }

        /// <summary>
        /// Loads the triggers.
        /// </summary>
        public void LoadXmlFileAndAddTriggers()
        {
            try
            {
                if (File.Exists(FileSystem.TriggersFile))
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
                            if (xReader.IsStartElement())
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
                                }
                            }
                        }

                        xReader.Close();

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
                    Log.Write($"WARNING: {FileSystem.TriggersFile} not found. Creating default triggers");

                    // Setup a few "built in" triggers by default.
                    Add(new Trigger("Application Startup -> Show", TriggerConditionType.ApplicationStartup,
                        TriggerActionType.ShowInterface, string.Empty));

                    Add(new Trigger("Capture Started -> Hide", TriggerConditionType.ScreenCaptureStarted,
                        TriggerActionType.HideInterface, string.Empty));

                    Add(new Trigger("Capture Stopped -> Show", TriggerConditionType.ScreenCaptureStopped,
                        TriggerActionType.ShowInterface, string.Empty));

                    Add(new Trigger("Interface Closing -> Exit", TriggerConditionType.InterfaceClosing,
                        TriggerActionType.ExitApplication, string.Empty));

                    Add(new Trigger("Limit Reached -> Stop", TriggerConditionType.LimitReached,
                        TriggerActionType.StopScreenCapture, string.Empty));

                    SaveToXmlFile();
                }
            }
            catch (Exception ex)
            {
                Log.Write("TriggerCollection::LoadXmlFileAndAddTriggers", ex);
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

                    if (File.Exists(FileSystem.ConfigFile))
                    {
                        using (StreamWriter sw = File.AppendText(FileSystem.ConfigFile))
                        {
                            sw.WriteLine("TriggersFile=" + FileSystem.TriggersFile);
                        }
                    }
                }

                if (File.Exists(FileSystem.TriggersFile))
                {
                    File.Delete(FileSystem.TriggersFile);
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
                Log.Write("TriggerCollection::SaveToXmlFile", ex);
            }
        }
    }
}