﻿//-----------------------------------------------------------------------
// <copyright file="EditorCollection.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A collection of editors.</summary>
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
    /// A collection class to store and manage Editor objects.
    /// </summary>
    public class EditorCollection : CollectionTemplate<Editor>
    {
        private const string XML_FILE_INDENT_CHARS = "   ";
        private const string XML_FILE_EDITOR_NODE = "editor";
        private const string XML_FILE_EDITORS_NODE = "editors";
        private const string XML_FILE_ROOT_NODE = "autoscreen";

        private const string EDITOR_NAME = "name";
        private const string EDITOR_ARGUMENTS = "arguments";
        private const string EDITOR_APPLICATION = "application";
        private const string EDITOR_NOTES = "notes";

        private readonly string EDITOR_XPATH;

        private static string AppCodename { get; set; }
        private static string AppVersion { get; set; }

        /// <summary>
        /// The empty constructor for the editor collection.
        /// </summary>
        public EditorCollection()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("/");
            sb.Append(XML_FILE_ROOT_NODE);
            sb.Append("/");
            sb.Append(XML_FILE_EDITORS_NODE);
            sb.Append("/");
            sb.Append(XML_FILE_EDITOR_NODE);

            EDITOR_XPATH = sb.ToString();
        }

        /// <summary>
        /// Loads the image editors from the editors.xml file.
        /// </summary>
        public bool LoadXmlFileAndAddEditors()
        {
            try
            {
                if (FileSystem.FileExists(FileSystem.EditorsFile))
                {
                    Log.WriteDebugMessage("Editors file \"" + FileSystem.EditorsFile + "\" found. Attempting to load XML document");

                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load(FileSystem.EditorsFile);

                    Log.WriteDebugMessage("XML document loaded");

                    AppVersion = xDoc.SelectSingleNode("/autoscreen").Attributes["app:version"]?.Value;
                    AppCodename = xDoc.SelectSingleNode("/autoscreen").Attributes["app:codename"]?.Value;

                    XmlNodeList xEditors = xDoc.SelectNodes(EDITOR_XPATH);

                    foreach (XmlNode xEditor in xEditors)
                    {
                        Editor editor = new Editor();
                        XmlNodeReader xReader = new XmlNodeReader(xEditor);

                        while (xReader.Read())
                        {
                            if (xReader.IsStartElement() && !xReader.IsEmptyElement)
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

                                        string value = xReader.Value;

                                        // Change the data for each Tag that's being loaded if we've detected that
                                        // the XML document is from an older version of the application.
                                        if (Settings.VersionManager.IsOldAppVersion(AppCodename, AppVersion))
                                        {
                                            Log.WriteDebugMessage("An old version of the editors.xml file was detected. Attempting upgrade to new schema.");

                                            Version v2300 = Settings.VersionManager.Versions.Get(Settings.CODENAME_BOOMBAYAH, Settings.CODEVERSION_BOOMBAYAH);
                                            Version configVersion = Settings.VersionManager.Versions.Get(AppCodename, AppVersion);

                                            if (v2300 != null && configVersion != null && configVersion.VersionNumber < v2300.VersionNumber)
                                            {
                                                Log.WriteDebugMessage("Dalek 2.2.4.6 or older detected");

                                                // Starting with 2.3.0.0 the %screenshot% argument tag became the %filepath% argument tag.
                                                value = value.Replace("%screenshot%", "%filepath%");

                                                // Set this editor as the default editor. Version 2.3 requires at least one editor to be the default editor.
                                                Settings.User.SetValueByKey("DefaultEditor", editor.Name);
                                            }
                                        }

                                        editor.Arguments = value;
                                        break;

                                    case EDITOR_NOTES:
                                        xReader.Read();
                                        editor.Notes = xReader.Value;
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
                        Log.WriteDebugMessage("Editors file detected as an old version");
                        SaveToXmlFile();
                    }
                }
                else
                {
                    Log.WriteDebugMessage("WARNING: Unable to load editors");

                    // Setup default image editors.
                    // This is going to get maintenance heavy. I just know it.

                    // Microsoft Paint
                    if (FileSystem.FileExists(@"C:\Windows\System32\mspaint.exe"))
                    {
                        Add(new Editor("Microsoft Paint", @"C:\Windows\System32\mspaint.exe", "%filepath%"));

                        // We'll make Microsoft Paint the default image editor because usually everyone has it already.
                        Settings.User.SetValueByKey("DefaultEditor", "Microsoft Paint");
                    }

                    // Snagit Editor
                    if (FileSystem.FileExists(@"C:\Program Files\TechSmith\Snagit 2020\SnagitEditor.exe"))
                    {
                        Add(new Editor("Snagit Editor", @"C:\Program Files\TechSmith\Snagit 2020\SnagitEditor.exe", "%filepath%"));

                        // If the user has Snagit installed then make the Snagit Editor the default editor.
                        Settings.User.SetValueByKey("DefaultEditor", "Snagit Editor");
                    }

                    // Microsoft Outlook
                    if (FileSystem.FileExists(@"C:\Program Files\Microsoft Office\root\Office16\OUTLOOK.EXE"))
                    {
                        Add(new Editor("Microsoft Outlook", @"C:\Program Files\Microsoft Office\root\Office16\OUTLOOK.EXE", "/c ipm.note /a %filepath%"));
                    }

                    // Chrome
                    if (FileSystem.FileExists(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe"))
                    {
                        Add(new Editor("Chrome", @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe", "%filepath%"));
                    }

                    // Firefox
                    if (FileSystem.FileExists(@"C:\Program Files\Mozilla Firefox\firefox.exe"))
                    {
                        Add(new Editor("Firefox", @"C:\Program Files\Mozilla Firefox\firefox.exe", "%filepath%"));
                    }

                    // GIMP
                    // We assume GIMP will be in the default location available for all users on 64-bit systems.
                    if (FileSystem.FileExists(@"C:\Program Files\GIMP 2\bin\gimp-2.10.exe"))
                    {
                        Add(new Editor("GIMP", @"C:\Program Files\GIMP 2\bin\gimp-2.10.exe", "%filepath%"));
                    }

                    // Glimpse
                    if (FileSystem.FileExists(@"C:\Program Files (x86)\Glimpse Image Editor\Glimpse 0.1.2\bin\Glimpse.exe"))
                    {
                        Add(new Editor("Glimpse", @"C:\Program Files (x86)\Glimpse Image Editor\Glimpse 0.1.2\bin\Glimpse.exe", "%filepath%"));
                    }

                    // Clip Studio Paint
                    if (FileSystem.FileExists(@"C:\Program Files\CELSYS\CLIP STUDIO 1.5\CLIP STUDIO PAINT\CLIPStudioPaint.exe"))
                    {
                        Add(new Editor("Clip Studio Paint", @"C:\Program Files\CELSYS\CLIP STUDIO 1.5\CLIP STUDIO PAINT\CLIPStudioPaint.exe", "%filepath%"));
                    }

                    SaveToXmlFile();
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("EditorCollection::LoadXmlFileAndAddEditors", ex);

                return false;
            }
        }

        /// <summary>
        /// Saves the image editors in the collection to the editors.xml file.
        /// </summary>
        public bool SaveToXmlFile()
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

                    if (FileSystem.FileExists(FileSystem.ConfigFile))
                    {
                        FileSystem.AppendToFile(FileSystem.ConfigFile, "\nEditorsFile=" + FileSystem.EditorsFile);
                    }
                }

                if (FileSystem.FileExists(FileSystem.EditorsFile))
                {
                    FileSystem.DeleteFile(FileSystem.EditorsFile);
                }

                using (XmlWriter xWriter =
                    XmlWriter.Create(FileSystem.EditorsFile, xSettings))
                {
                    xWriter.WriteStartDocument();
                    xWriter.WriteStartElement(XML_FILE_ROOT_NODE);
                    xWriter.WriteAttributeString("app", "version", XML_FILE_ROOT_NODE, Settings.ApplicationVersion);
                    xWriter.WriteAttributeString("app", "codename", XML_FILE_ROOT_NODE, Settings.ApplicationCodename);
                    xWriter.WriteStartElement(XML_FILE_EDITORS_NODE);

                    foreach (Editor editor in base.Collection)
                    {
                        xWriter.WriteStartElement(XML_FILE_EDITOR_NODE);
                        xWriter.WriteElementString(EDITOR_NAME, editor.Name);
                        xWriter.WriteElementString(EDITOR_APPLICATION, editor.Application);
                        xWriter.WriteElementString(EDITOR_ARGUMENTS, editor.Arguments);
                        xWriter.WriteElementString(EDITOR_NOTES, editor.Notes);

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
                Log.WriteExceptionMessage("EditorCollection::SaveToXmlFile", ex);

                return false;
            }
        }
    }
}