//-----------------------------------------------------------------------
// <copyright file="EditorCollection.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
using System;
using System.IO;
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
        private readonly string EDITOR_XPATH;

        private static string AppCodename { get; set; }
        private static string AppVersion { get; set; }

        /// <summary>
        /// 
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
                    Log.WriteDebugMessage($"WARNING: {FileSystem.EditorsFile} not found. Unable to load editors");

                    // Setup default image editors.
                    // This is going to get maintenance heavy. I just know it.

                    // Microsoft Paint
                    if (File.Exists(@"C:\Windows\System32\mspaint.exe"))
                    {
                        Add(new Editor("Microsoft Paint", @"C:\Windows\System32\mspaint.exe", "%screenshot%"));

                        // We'll make Microsoft Paint the default image editor because usually everyone has it already.
                        Settings.User.SetValueByKey("StringDefaultEditor", "Microsoft Paint");
                    }

                    // Microsoft Outlook
                    if (File.Exists(@"C:\Program Files\Microsoft Office\root\Office16\OUTLOOK.EXE"))
                    {
                        Add(new Editor("Microsoft Outlook", @"C:\Program Files\Microsoft Office\root\Office16\OUTLOOK.EXE", "/c ipm.note /a %screenshot%"));
                    }

                    // Chrome
                    if (File.Exists(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe"))
                    {
                        Add(new Editor("Chrome", @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe", "%screenshot%"));
                    }

                    // Firefox
                    if (File.Exists(@"C:\Program Files\Mozilla Firefox\firefox.exe"))
                    {
                        Add(new Editor("Firefox", @"C:\Program Files\Mozilla Firefox\firefox.exe", "%screenshot%"));
                    }

                    // GIMP
                    // We assume GIMP will be in the default location available for all users on 64-bit systems.
                    if (File.Exists(@"C:\Program Files\GIMP 2\bin\gimp-2.10.exe"))
                    {
                        Add(new Editor("GIMP", @"C:\Program Files\GIMP 2\bin\gimp-2.10.exe", "%screenshot%"));
                    }

                    // Glimpse
                    if (File.Exists(@"C:\Program Files (x86)\Glimpse Image Editor\Glimpse 0.1.2\bin\Glimpse.exe"))
                    {
                        Add(new Editor("Glimpse", @"C:\Program Files (x86)\Glimpse Image Editor\Glimpse 0.1.2\bin\Glimpse.exe", "%screenshot%"));
                    }

                    // Clip Studio Paint
                    if (File.Exists(@"C:\Program Files\CELSYS\CLIP STUDIO 1.5\CLIP STUDIO PAINT\CLIPStudioPaint.exe"))
                    {
                        Add(new Editor("Clip Studio Paint", @"C:\Program Files\CELSYS\CLIP STUDIO 1.5\CLIP STUDIO PAINT\CLIPStudioPaint.exe", "%screenshot%"));
                    }

                    SaveToXmlFile();
                }
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("EditorCollection::LoadXmlFileAndAddEditors", ex);
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
                            sw.WriteLine("\nEditorsFile=" + FileSystem.EditorsFile);
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

                    foreach (Editor editor in base.Collection)
                    {
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
                Log.WriteExceptionMessage("EditorCollection::SaveToXmlFile", ex);
            }
        }
    }
}