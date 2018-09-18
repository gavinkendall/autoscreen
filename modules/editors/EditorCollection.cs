//-----------------------------------------------------------------------
// <copyright file="EditorCollection.cs" company="Gavin Kendall">
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

    public static class EditorCollection
    {
        private static ArrayList _editorList = new ArrayList();

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
            _editorList.Add(editor);

            Log.Write("Added " + editor.Name + " (" + editor.Application + " " + editor.Arguments + ")");
        }

        public static void Remove(Editor editor)
        {
            _editorList.Remove(editor);

            Log.Write("Removed " + editor.Name + " (" + editor.Application + " " + editor.Arguments + ")");
        }

        public static int Count
        {
            get { return _editorList.Count; }
        }

        public static Editor Get(Editor editorToFind)
        {
            for (int i = 0; i < _editorList.Count; i++)
            {
                Editor editor = GetByIndex(i);

                if (editor.Equals(editorToFind))
                {
                    return GetByIndex(i);
                }
            }

            return null;
        }

        public static Editor GetByIndex(int index)
        {
            return (Editor)_editorList[index];
        }

        public static Editor GetByName(string name)
        {
            for (int i = 0; i < _editorList.Count; i++)
            {
                Editor editor = GetByIndex(i);

                if (editor.Name.Equals(name))
                {
                    return GetByIndex(i);
                }
            }

            return null;
        }

        /// <summary>
        /// Loads the image editors.
        /// </summary>
        public static void Load()
        {
            if (File.Exists(FileSystem.UserAppDataLocalDirectory + XML_FILE))
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(FileSystem.UserAppDataLocalDirectory + XML_FILE);

                XmlNodeList xEditors = xDoc.SelectNodes(EDITOR_XPATH);

                foreach (XmlNode xEditor in xEditors)
                {
                    Editor editor = new Editor();
                    XmlNodeReader xReader = new XmlNodeReader(xEditor);

                    while (xReader.Read())
                    {
                        if (xReader.IsStartElement())
                        {
                            switch (xReader.Name)
                            {
                                case EDITOR_NAME:
                                    xReader.Read();
                                    editor.Name = xReader.Value;
                                    break;

                                case EDITOR_APPLICATION:
                                    xReader.Read();
                                    editor.Application = xReader.Value;
                                    break;

                                case EDITOR_ARGUMENTS:
                                    xReader.Read();
                                    editor.Arguments = xReader.Value;
                                    break;
                            }
                        }
                    }

                    xReader.Close();

                    if (!string.IsNullOrEmpty(editor.Name) &&
                        !string.IsNullOrEmpty(editor.Application))
                    {
                        Add(editor);
                    }
                }
            }
        }

        /// <summary>
        /// Saves the image editors.
        /// </summary>
        public static void Save()
        {
            XmlWriterSettings xSettings = new XmlWriterSettings
            {
                Indent = true,
                CloseOutput = true,
                CheckCharacters = true,
                Encoding = Encoding.UTF8,
                NewLineChars = Environment.NewLine,
                IndentChars = XML_FILE_INDENT_CHARS,
                NewLineHandling = NewLineHandling.Entitize,
                ConformanceLevel = ConformanceLevel.Document
            };

            using (XmlWriter xWriter = XmlWriter.Create(FileSystem.UserAppDataLocalDirectory + XML_FILE, xSettings))
            {
                xWriter.WriteStartDocument();
                xWriter.WriteStartElement(XML_FILE_ROOT_NODE);
                xWriter.WriteStartElement(XML_FILE_EDITORS_NODE);

                foreach (object obj in _editorList)
                {
                    Editor editor = (Editor)obj;

                    xWriter.WriteStartElement(XML_FILE_EDITOR_NODE);
                    xWriter.WriteElementString(EDITOR_NAME, editor.Name);
                    xWriter.WriteElementString(EDITOR_APPLICATION, editor.Application);
                    xWriter.WriteElementString(EDITOR_ARGUMENTS, editor.Arguments);

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