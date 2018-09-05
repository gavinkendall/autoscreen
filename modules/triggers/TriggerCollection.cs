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
    using System.IO;
    using System.Collections;
    using System.Text;
    using System.Xml;

    public static class TriggerCollection
    {
        private static ArrayList _triggerList = new ArrayList();

        private const string XML_FILE = "triggers.xml";
        private const string XML_FILE_INDENT_CHARS = "   ";
        private const string XML_FILE_TRIGGER_NODE = "trigger";
        private const string XML_FILE_TRIGGERS_NODE = "triggers";
        private const string XML_FILE_ROOT_NODE = "autoscreen";

        private const string TRIGGER_NAME = "name";
        private const string TRIGGER_XPATH = "/" + XML_FILE_ROOT_NODE + "/" + XML_FILE_TRIGGERS_NODE + "/" + XML_FILE_TRIGGER_NODE;

        public static void Add(Trigger trigger)
        {
            _triggerList.Add(trigger);

            Log.Write("Added " + trigger.Name);
        }

        public static void Remove(Trigger trigger)
        {
            _triggerList.Remove(trigger);

            Log.Write("Removed " + trigger.Name);
        }

        public static int Count
        {
            get { return _triggerList.Count; }
        }

        public static Trigger Get(Trigger triggerToFind)
        {
            for (int i = 0; i < _triggerList.Count; i++)
            {
                Trigger trigger = GetByIndex(i);

                if (trigger.Equals(triggerToFind))
                {
                    return GetByIndex(i);
                }
            }

            return null;
        }

        public static Trigger GetByIndex(int index)
        {
            return (Trigger)_triggerList[index];
        }

        public static Trigger GetByName(string name)
        {
            for (int i = 0; i < _triggerList.Count; i++)
            {
                Trigger trigger = GetByIndex(i);

                if (trigger.Name.Equals(name))
                {
                    return GetByIndex(i);
                }
            }

            return null;
        }

        public static void Load()
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + XML_FILE))
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(AppDomain.CurrentDomain.BaseDirectory + XML_FILE);

                XmlNodeList xtriggers = xdoc.SelectNodes(TRIGGER_XPATH);

                foreach (XmlNode xtrigger in xtriggers)
                {
                    Trigger trigger = new Trigger();
                    XmlNodeReader xreader = new XmlNodeReader(xtrigger);

                    while (xreader.Read())
                    {
                        if (xreader.IsStartElement())
                        {
                            switch (xreader.Name)
                            {
                                case TRIGGER_NAME:
                                    xreader.Read();
                                    trigger.Name = xreader.Value;
                                    break;
                            }
                        }
                    }

                    xreader.Close();

                    if (!string.IsNullOrEmpty(trigger.Name))
                    {
                        Add(trigger);
                    }
                }
            }
        }

        public static void Save()
        {
            XmlWriterSettings xsettings = new XmlWriterSettings();
            xsettings.Indent = true;
            xsettings.CloseOutput = true;
            xsettings.CheckCharacters = true;
            xsettings.Encoding = Encoding.UTF8;
            xsettings.NewLineChars = Environment.NewLine;
            xsettings.IndentChars = XML_FILE_INDENT_CHARS;
            xsettings.NewLineHandling = NewLineHandling.Entitize;
            xsettings.ConformanceLevel = ConformanceLevel.Document;

            using (XmlWriter xwriter = XmlWriter.Create(AppDomain.CurrentDomain.BaseDirectory + XML_FILE, xsettings))
            {
                xwriter.WriteStartDocument();
                xwriter.WriteStartElement(XML_FILE_ROOT_NODE);
                xwriter.WriteStartElement(XML_FILE_TRIGGERS_NODE);

                foreach (object obj in _triggerList)
                {
                    Trigger trigger = (Trigger)obj;

                    xwriter.WriteStartElement(XML_FILE_TRIGGER_NODE);
                    xwriter.WriteElementString(TRIGGER_NAME, trigger.Name);

                    xwriter.WriteEndElement();
                }

                xwriter.WriteEndElement();
                xwriter.WriteEndElement();
                xwriter.WriteEndDocument();

                xwriter.Flush();
                xwriter.Close();
            }
        }
    }
}
