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
    using System.Collections.Generic;

    public class EditorCollection : IEnumerable<Editor>
    {
        private readonly List<Editor> _editorList = new List<Editor>();

        private const string XML_FILE = "editors.xml";
        private const string XML_FILE_INDENT_CHARS = "   ";
        private const string XML_FILE_EDITOR_NODE = "editor";
        private const string XML_FILE_EDITORS_NODE = "editors";
        private const string XML_FILE_ROOT_NODE = "autoscreen";

        private const string EDITOR_NAME = "name";
        private const string EDITOR_ARGUMENTS = "arguments";
        private const string EDITOR_APPLICATION = "application";
        private const string EDITOR_XPATH = "/" + XML_FILE_ROOT_NODE + "/" + XML_FILE_EDITORS_NODE + "/" + XML_FILE_EDITOR_NODE;

        public List<Editor>.Enumerator GetEnumerator()
        {
            return _editorList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Editor>)_editorList).GetEnumerator();
        }

        IEnumerator<Editor> IEnumerable<Editor>.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(Editor editor)
        {
            _editorList.Add(editor);

            Log.Write("Added " + editor.Name + " (" + editor.Application + " " + editor.Arguments + ")");
        }

        public void Remove(Editor editor)
        {
            _editorList.Remove(editor);

            Log.Write("Removed " + editor.Name + " (" + editor.Application + " " + editor.Arguments + ")");
        }

        public int Count
        {
            get { return _editorList.Count; }
        }

        public Editor Get(Editor editorToFind)
        {
            foreach (Editor editor in _editorList)
            {
                if (editor.Equals(editorToFind))
                {
                    return editor;
                }
            }

            return null;
        }

        public Editor GetByName(string name)
        {
            foreach (Editor editor in _editorList)
            {
                if (editor.Name.Equals(name))
                {
                    return editor;
                }
            }

            return null;
        }

        /// <summary>
        /// Loads the image editors.
        /// </summary>
        public void Load()
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
        public void Save()
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