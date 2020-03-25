//-----------------------------------------------------------------------
// <copyright file="EditorCollection.cs" company="Gavin Kendall">
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
    /// A collection class to store and manage Editor objects.
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

        private static string AppCodename { get; set; }
        private static string AppVersion { get; set; }

        /// <summary>
        /// Returns the enumerator for the collection.
        /// </summary>
        /// <returns>A list of Editor objects.</returns>
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
        /// Adds an Editor object to the collection.
        /// </summary>
        /// <param name="editor">An Editor object to add.</param>
        public void Add(Editor editor)
        {
            _editorList.Add(editor);

            Log.Write("Editor added: " + editor.Name + " (" + editor.Application + " " + editor.Arguments + ")");
        }

        /// <summary>
        /// Removes an Editor object from the collection.
        /// </summary>
        /// <param name="editor">The Editor object to remove.</param>
        public void Remove(Editor editor)
        {
            _editorList.Remove(editor);

            Log.Write("Editor removed: " + editor.Name + " (" + editor.Application + " " + editor.Arguments + ")");
        }

        /// <summary>
        /// Gets the number of Editor objects in the collection.
        /// </summary>
        /// <returns>A count of Editor objects.</returns>
        public int Count
        {
            get { return _editorList.Count; }
        }

        /// <summary>
        /// Gets a specific Editor object from the collection.
        /// </summary>
        /// <param name="editorToFind">The Editor object to retrieve.</param>
        /// <returns>An Editor object.</returns>
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
        /// Gets an Editor object based on its name.
        /// </summary>
        /// <param name="name">The name of an Editor object.</param>
        /// <returns>An Editor object.</returns>
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
        /// Loads the image editors from the editors.xml file.
        /// </summary>
        public void LoadXmlFileAndAddEditors()
        {
            try
            {
                if (File.Exists(FileSystem.EditorsFile))
                {
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load(FileSystem.EditorsFile);

                    AppVersion = xDoc.SelectSingleNode("/autoscreen").Attributes["app:version"]?.Value;
                    AppCodename = xDoc.SelectSingleNode("/autoscreen").Attributes["app:codename"]?.Value;

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

                    if (Settings.VersionManager.IsOldAppVersion(AppCodename, AppVersion))
                    {
                        SaveToXmlFile();
                    }
                }
                else
                {
                    Log.Write($"WARNING: {FileSystem.EditorsFile} not found. Unable to load editors");
                }
            }
            catch (Exception ex)
            {
                Log.Write("EditorCollection::LoadXmlFileAndAddEditors", ex);
            }
        }

        /// <summary>
        /// Saves the image editors in the collection to the editors.xml file.
        /// </summary>
        public void SaveToXmlFile()
        {
            try
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

                if (string.IsNullOrEmpty(FileSystem.EditorsFile))
                {
                    FileSystem.EditorsFile = FileSystem.DefaultEditorsFile;

                    if (File.Exists(FileSystem.ConfigFile))
                    {
                        using (StreamWriter sw = File.AppendText(FileSystem.ConfigFile))
                        {
                            sw.WriteLine("EditorsFile=" + FileSystem.EditorsFile);
                        }
                    }
                }

                if (File.Exists(FileSystem.EditorsFile))
                {
                    File.Delete(FileSystem.EditorsFile);
                }

                using (XmlWriter xWriter =
                    XmlWriter.Create(FileSystem.EditorsFile, xSettings))
                {
                    xWriter.WriteStartDocument();
                    xWriter.WriteStartElement(XML_FILE_ROOT_NODE);
                    xWriter.WriteAttributeString("app", "version", XML_FILE_ROOT_NODE, Settings.ApplicationVersion);
                    xWriter.WriteAttributeString("app", "codename", XML_FILE_ROOT_NODE, Settings.ApplicationCodename);
                    xWriter.WriteStartElement(XML_FILE_EDITORS_NODE);

                    foreach (object obj in _editorList)
                    {
                        Editor editor = (Editor) obj;

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
            catch (Exception ex)
            {
                Log.Write("EditorCollection::Save", ex);
            }
        }
    }
}