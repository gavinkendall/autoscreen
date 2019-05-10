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
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// 
    /// </summary>
    public class EditorCollection : IEnumerable<Editor>
    {
        private readonly List<Editor> _editorList = new List<Editor>();

        private const string XML_FILE_INDENT_CHARS = "   ";
        private const string XML_FILE_EDITOR_NODE = "editor";
        private const string XML_FILE_EDITORS_NODE = "editors";
        private const string XML_FILE_ROOT_NODE = "autoscreen";

        private const string EDITOR_NAME = "name";
        private const string EDITOR_ARGUMENTS = "arguments";
        private const string EDITOR_APPLICATION = "application";
        private const string EDITOR_XPATH = "/" + XML_FILE_ROOT_NODE + "/" + XML_FILE_EDITORS_NODE + "/" + XML_FILE_EDITOR_NODE;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="editor"></param>
        public void Add(Editor editor)
        {
            _editorList.Add(editor);

            Log.Write("Editor added: " + editor.Name + " (" + editor.Application + " " + editor.Arguments + ")");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="editor"></param>
        public void Remove(Editor editor)
        {
            _editorList.Remove(editor);

            Log.Write("Editor removed: " + editor.Name + " (" + editor.Application + " " + editor.Arguments + ")");
        }

        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get { return _editorList.Count; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="editorToFind"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
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
            if (File.Exists(FileSystem.ApplicationFolder + FileSystem.EditorsFile))
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(FileSystem.ApplicationFolder + FileSystem.EditorsFile);

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

            if (File.Exists(FileSystem.ApplicationFolder + FileSystem.EditorsFile))
            {
                File.Delete(FileSystem.ApplicationFolder + FileSystem.EditorsFile);
            }

            using (XmlWriter xWriter = XmlWriter.Create(FileSystem.ApplicationFolder + FileSystem.EditorsFile, xSettings))
            {
                xWriter.WriteStartDocument();
                xWriter.WriteStartElement(XML_FILE_ROOT_NODE);
                xWriter.WriteAttributeString("app", "version", XML_FILE_ROOT_NODE, Settings.ApplicationVersion);
                xWriter.WriteAttributeString("app", "codename", XML_FILE_ROOT_NODE, Settings.ApplicationCodename);
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