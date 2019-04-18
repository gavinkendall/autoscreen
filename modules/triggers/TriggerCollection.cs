//-----------------------------------------------------------------------
// <copyright file="TriggerCollection.cs" company="Gavin Kendall">
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

    public class TriggerCollection : IEnumerable<Trigger>
    {
        private readonly List<Trigger> _triggerList = new List<Trigger>();

        private const string XML_FILE_INDENT_CHARS = "   ";
        private const string XML_FILE_TRIGGER_NODE = "trigger";
        private const string XML_FILE_TRIGGERS_NODE = "triggers";
        private const string XML_FILE_ROOT_NODE = "autoscreen";

        private const string TRIGGER_NAME = "name";
        private const string TRIGGER_CONDITION = "condition";
        private const string TRIGGER_ACTION = "action";
        private const string TRIGGER_EDITOR = "editor";
        private const string TRIGGER_XPATH = "/" + XML_FILE_ROOT_NODE + "/" + XML_FILE_TRIGGERS_NODE + "/" + XML_FILE_TRIGGER_NODE;

        public List<Trigger>.Enumerator GetEnumerator()
        {
            return _triggerList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Trigger>)_triggerList).GetEnumerator();
        }

        IEnumerator<Trigger> IEnumerable<Trigger>.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(Trigger trigger)
        {
            _triggerList.Add(trigger);

            Log.Write("Trigger added: " + trigger.Name);
        }

        public void Remove(Trigger trigger)
        {
            _triggerList.Remove(trigger);

            Log.Write("Trigger removed: " + trigger.Name);
        }

        public int Count
        {
            get { return _triggerList.Count; }
        }

        public Trigger Get(Trigger triggerToFind)
        {
            foreach (Trigger trigger in _triggerList)
            {
                if (trigger.Equals(triggerToFind))
                {
                    return trigger;
                }
            }

            return null;
        }

        public Trigger GetByName(string name)
        {
            foreach (Trigger trigger in _triggerList)
            {
                if (trigger.Name.Equals(name))
                {
                    return trigger;
                }
            }

            return null;
        }

        /// <summary>
        /// Loads the triggers.
        /// </summary>
        public void Load()
        {
            if (File.Exists(FileSystem.ApplicationFolder + FileSystem.TriggersFile))
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(FileSystem.ApplicationFolder + FileSystem.TriggersFile);

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
                                    trigger.ConditionType = (TriggerConditionType)Enum.Parse(typeof(TriggerConditionType), xReader.Value);
                                    break;

                                case TRIGGER_ACTION:
                                    xReader.Read();
                                    trigger.ActionType = (TriggerActionType)Enum.Parse(typeof(TriggerActionType), xReader.Value);
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
            }
            else
            {
                // Setup a few "built in" triggers by default.
                Add(new Trigger("Application Startup -> Show", TriggerConditionType.ApplicationStartup, TriggerActionType.ShowInterface, string.Empty));
                Add(new Trigger("Capture Started -> Hide", TriggerConditionType.ScreenCaptureStarted, TriggerActionType.HideInterface, string.Empty));
                Add(new Trigger("Capture Stopped -> Show", TriggerConditionType.ScreenCaptureStopped, TriggerActionType.ShowInterface, string.Empty));
                Add(new Trigger("Interface Closing -> Exit", TriggerConditionType.InterfaceClosing, TriggerActionType.ExitApplication, string.Empty));
                Add(new Trigger("Limit Reached -> Stop", TriggerConditionType.LimitReached, TriggerActionType.StopScreenCapture, string.Empty));

                Save();
            }
        }

        /// <summary>
        /// Saves the triggers.
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

            using (XmlWriter xWriter = XmlWriter.Create(FileSystem.ApplicationFolder + FileSystem.TriggersFile, xSettings))
            {
                xWriter.WriteStartDocument();
                xWriter.WriteStartElement(XML_FILE_ROOT_NODE);
                xWriter.WriteAttributeString("app", "version", XML_FILE_ROOT_NODE, Settings.ApplicationVersion);
                xWriter.WriteAttributeString("app", "codename", XML_FILE_ROOT_NODE, Settings.ApplicationCodename);
                xWriter.WriteStartElement(XML_FILE_TRIGGERS_NODE);

                foreach (object obj in _triggerList)
                {
                    Trigger trigger = (Trigger)obj;

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
    }
}