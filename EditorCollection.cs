//////////////////////////////////////////////////////////
// Auto Screen Capture 2.0.5
// autoscreen.EditorCollection.cs
//
// Written by Gavin Kendall (gavinkendall@gmail.com)
// Thursday, 15 May 2008 - Wednesday, 27 November 2013

using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace autoscreen
{
    public static class EditorCollection
    {
        private static ArrayList m_editorList = new ArrayList();

        private const string XML_FILE = "editors.xml";
        private const string XML_FILE_INDENT_CHARS = "   ";  
        private const string XML_FILE_EDITOR_NODE = "editor";
        private const string XML_FILE_EDITORS_NODE = "editors";
        private const string XML_FILE_ROOT_NODE = "autoscreen";

        private const string EDITOR_NAME = "name";
        private const string EDITOR_ARGUMENTS = "arguments";
        private const string EDITOR_APPLICATION = "application";
        private const string EDITOR_XPATH = "/" + XML_FILE_ROOT_NODE + "/" + XML_FILE_EDITORS_NODE + "/" + XML_FILE_EDITOR_NODE;

        public static void Add(Editor editor)
        {
            m_editorList.Add(editor);
            Save();
        }

        public static Editor Get(int index)
        {
            return (Editor)m_editorList[index];
        }

        public static int Count
        {
            get { return m_editorList.Count; }
        }

        public static Editor GetByName(string name)
        {
            for (int i = 0; i < m_editorList.Count; i++)
            {
                Editor editor = Get(i);

                if (editor.Name.Equals(name))
                {
                    return (Editor)Get(i);
                }
            }

            return null;
        }

        /// <summary>
        /// Loads the image editors into the application from the editors.xml file.
        /// </summary>
        public static void Load()
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + XML_FILE))
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(AppDomain.CurrentDomain.BaseDirectory + XML_FILE);

                XmlNodeList xeditors = xdoc.SelectNodes(EDITOR_XPATH);

                foreach (XmlNode xeditor in xeditors)
                {
                    Editor editor = new Editor();
                    XmlNodeReader xreader = new XmlNodeReader(xeditor);

                    while (xreader.Read())
                    {
                        if (xreader.IsStartElement())
                        {
                            switch (xreader.Name)
                            {
                                case EDITOR_NAME:
                                    xreader.Read();
                                    editor.Name = xreader.Value;
                                    break;

                                case EDITOR_APPLICATION:
                                    xreader.Read();
                                    editor.Application = xreader.Value;
                                    break;

                                case EDITOR_ARGUMENTS:
                                    xreader.Read();
                                    editor.Arguments = xreader.Value;
                                    break;
                            }
                        }
                    }

                    xreader.Close();

                    if (!string.IsNullOrEmpty(editor.Name) &&
                        !string.IsNullOrEmpty(editor.Application) &&
                        !string.IsNullOrEmpty(editor.Arguments))
                    {
                        EditorCollection.Add(editor);
                    }
                }
            }
        }

        /// <summary>
        /// Saves the image editors to the editors.xml file from the application.
        /// </summary>
        public static void Save()
        {
            XmlWriterSettings xsettings = new XmlWriterSettings();
            xsettings.Indent = true;
            xsettings.CloseOutput = true;
            xsettings.CheckCharacters = true;
            xsettings.Encoding = Encoding.UTF8;
            xsettings.DoNotEscapeUriAttributes = true;
            xsettings.NewLineChars = Environment.NewLine;
            xsettings.IndentChars = XML_FILE_INDENT_CHARS;
            xsettings.NewLineHandling = NewLineHandling.Entitize;
            xsettings.ConformanceLevel = ConformanceLevel.Document;

            using (XmlWriter xwriter = XmlWriter.Create(AppDomain.CurrentDomain.BaseDirectory + XML_FILE, xsettings))
            {
                xwriter.WriteStartDocument();
                xwriter.WriteStartElement(XML_FILE_ROOT_NODE);
                xwriter.WriteStartElement(XML_FILE_EDITORS_NODE);

                foreach (object obj in m_editorList)
                {
                    Editor editor = (Editor)obj;

                    xwriter.WriteStartElement(XML_FILE_EDITOR_NODE);
                    xwriter.WriteElementString(EDITOR_NAME, editor.Name);
                    xwriter.WriteElementString(EDITOR_APPLICATION, editor.Application);
                    xwriter.WriteElementString(EDITOR_ARGUMENTS, editor.Arguments);

                    xwriter.WriteEndElement();
                }

                xwriter.WriteEndElement();
                xwriter.WriteEndElement();
                xwriter.WriteEndDocument();
            }
        }
    }
}
