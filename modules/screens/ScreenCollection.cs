//-----------------------------------------------------------------------
// <copyright file="ScreenCollection.cs" company="Gavin Kendall">
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
    /// A collection class to store and manage Screen objects.
    /// </summary>
    public class ScreenCollection : CollectionTemplate<Screen>
    {
        private const string XML_FILE_INDENT_CHARS = "   ";
        private const string XML_FILE_SCREEN_NODE = "screen";
        private const string XML_FILE_SCREENS_NODE = "screens";
        private const string XML_FILE_ROOT_NODE = "autoscreen";

        private const string SCREEN_VIEWID = "viewid";
        private const string SCREEN_NAME = "name";
        private const string SCREEN_FOLDER = "folder";
        private const string SCREEN_MACRO = "macro";
        private const string SCREEN_COMPONENT = "component";
        private const string SCREEN_FORMAT = "format";
        private const string SCREEN_JPEG_QUALITY = "jpeg_quality";
        private const string SCREEN_RESOLUTION_RATIO = "resolution_ratio";
        private const string SCREEN_MOUSE = "mouse";
        private const string SCREEN_ENABLED = "enabled";
        private readonly string SCREEN_XPATH;

        private static string AppCodename { get; set; }
        private static string AppVersion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ScreenCollection()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("/");
            sb.Append(XML_FILE_ROOT_NODE);
            sb.Append("/");
            sb.Append(XML_FILE_SCREENS_NODE);
            sb.Append("/");
            sb.Append(XML_FILE_SCREEN_NODE);

            SCREEN_XPATH = sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public Screen GetByComponent(int component)
        {
            foreach (Screen screen in base.Collection)
            {
                if (screen.Component == component)
                {
                    return screen;
                }
            }

            return null;
        }

        /// <summary>
        /// Loads the screens.
        /// </summary>
        public void LoadXmlFileAndAddScreens(ImageFormatCollection imageFormatCollection)
        {
            try
            {
                if (File.Exists(FileSystem.ScreensFile))
                {
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load(FileSystem.ScreensFile);

                    AppVersion = xDoc.SelectSingleNode("/autoscreen").Attributes["app:version"]?.Value;
                    AppCodename = xDoc.SelectSingleNode("/autoscreen").Attributes["app:codename"]?.Value;

                    XmlNodeList xScreens = xDoc.SelectNodes(SCREEN_XPATH);

                    foreach (XmlNode xScreen in xScreens)
                    {
                        Screen screen = new Screen();
                        XmlNodeReader xReader = new XmlNodeReader(xScreen);

                        while (xReader.Read())
                        {
                            if (xReader.IsStartElement())
                            {
                                switch (xReader.Name)
                                {
                                    case SCREEN_VIEWID:
                                        xReader.Read();
                                        screen.ViewId = Guid.Parse(xReader.Value);
                                        break;

                                    case SCREEN_NAME:
                                        xReader.Read();
                                        screen.Name = xReader.Value;
                                        break;

                                    case SCREEN_FOLDER:
                                        xReader.Read();
                                        screen.Folder = xReader.Value;
                                        break;

                                    case SCREEN_MACRO:
                                        xReader.Read();
                                        screen.Macro = xReader.Value;
                                        break;

                                    case SCREEN_COMPONENT:
                                        xReader.Read();
                                        screen.Component = Convert.ToInt32(xReader.Value);
                                        break;

                                    case SCREEN_FORMAT:
                                        xReader.Read();
                                        screen.Format = imageFormatCollection.GetByName(xReader.Value);
                                        break;

                                    case SCREEN_JPEG_QUALITY:
                                        xReader.Read();
                                        screen.JpegQuality = Convert.ToInt32(xReader.Value);
                                        break;

                                    case SCREEN_RESOLUTION_RATIO:
                                        xReader.Read();
                                        screen.ResolutionRatio = Convert.ToInt32(xReader.Value);
                                        break;

                                    case SCREEN_MOUSE:
                                        xReader.Read();
                                        screen.Mouse = Convert.ToBoolean(xReader.Value);
                                        break;

                                    case SCREEN_ENABLED:
                                        xReader.Read();
                                        screen.Enabled = Convert.ToBoolean(xReader.Value);
                                        break;
                                }
                            }
                        }

                        xReader.Close();

                        // Change the data for each Screen that's being loaded if we've detected that
                        // the XML file is from an older version of the application.
                        if (Settings.VersionManager.IsOldAppVersion(AppCodename, AppVersion))
                        {
                            Log.WriteDebugMessage("An old version of the screens.xml file was detected. Attempting upgrade to new schema.");

                            Version v2250 = Settings.VersionManager.Versions.Get("Dalek", "2.2.5.0");
                            Version configVersion = Settings.VersionManager.Versions.Get(AppCodename, AppVersion);

                            if (v2250 != null && configVersion != null && configVersion.VersionNumber < v2250.VersionNumber)
                            {
                                Log.WriteDebugMessage("Dalek 2.2.4.6 or older detected");

                                // This is a new property for Screen that was introduced in 2.2.5.0
                                // so any version before 2.2.5.0 needs to have it during an upgrade.
                                screen.Enabled = true;
                            }
                        }

                        if (!string.IsNullOrEmpty(screen.Name))
                        {
                            Add(screen);
                        }
                    }

                    if (Settings.VersionManager.IsOldAppVersion(AppCodename, AppVersion))
                    {
                        SaveToXmlFile();
                    }
                }
                else
                {
                    Log.WriteDebugMessage($"WARNING: {FileSystem.ScreensFile} not found. Detecting available screens and creating them");

                    // Setup some screens based on what we can find.
                    for (int screenNumber = 1; screenNumber <= System.Windows.Forms.Screen.AllScreens.Length; screenNumber++)
                    {
                        Add(new Screen($"Screen {screenNumber}", FileSystem.ScreenshotsFolder, MacroParser.DefaultMacro, screenNumber,
                            imageFormatCollection.GetByName(ScreenCapture.DefaultImageFormat), 100, 100, mouse: true, enabled: true));

                        Log.WriteDebugMessage($"Screen {screenNumber} created using \"{FileSystem.ScreenshotsFolder}\" for folder path and \"{MacroParser.DefaultMacro}\" for macro.");
                    }

                    SaveToXmlFile();
                }
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("ScreenCollection::LoadXmlFileAndAddScreens", ex);
            }
        }

        /// <summary>
        /// Saves the screens.
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

                if (string.IsNullOrEmpty(FileSystem.ScreensFile))
                {
                    FileSystem.ScreensFile = FileSystem.DefaultScreensFile;

                    if (File.Exists(FileSystem.ConfigFile))
                    {
                        using (StreamWriter sw = File.AppendText(FileSystem.ConfigFile))
                        {
                            sw.WriteLine("ScreensFile=" + FileSystem.ScreensFile);
                        }
                    }
                }

                if (File.Exists(FileSystem.ScreensFile))
                {
                    File.Delete(FileSystem.ScreensFile);
                }

                using (XmlWriter xWriter =
                    XmlWriter.Create(FileSystem.ScreensFile, xSettings))
                {
                    xWriter.WriteStartDocument();
                    xWriter.WriteStartElement(XML_FILE_ROOT_NODE);
                    xWriter.WriteAttributeString("app", "version", XML_FILE_ROOT_NODE, Settings.ApplicationVersion);
                    xWriter.WriteAttributeString("app", "codename", XML_FILE_ROOT_NODE, Settings.ApplicationCodename);
                    xWriter.WriteStartElement(XML_FILE_SCREENS_NODE);

                    foreach (Screen screen in base.Collection)
                    {
                        xWriter.WriteStartElement(XML_FILE_SCREEN_NODE);

                        xWriter.WriteElementString(SCREEN_ENABLED, screen.Enabled.ToString());
                        xWriter.WriteElementString(SCREEN_VIEWID, screen.ViewId.ToString());
                        xWriter.WriteElementString(SCREEN_NAME, screen.Name);
                        xWriter.WriteElementString(SCREEN_FOLDER, FileSystem.CorrectScreenshotsFolderPath(screen.Folder));
                        xWriter.WriteElementString(SCREEN_MACRO, screen.Macro);
                        xWriter.WriteElementString(SCREEN_COMPONENT, screen.Component.ToString());
                        xWriter.WriteElementString(SCREEN_FORMAT, screen.Format.Name);
                        xWriter.WriteElementString(SCREEN_JPEG_QUALITY, screen.JpegQuality.ToString());
                        xWriter.WriteElementString(SCREEN_RESOLUTION_RATIO, screen.ResolutionRatio.ToString());
                        xWriter.WriteElementString(SCREEN_MOUSE, screen.Mouse.ToString());

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
                Log.WriteExceptionMessage("ScreenCollection::SaveToXmlFile", ex);
            }
        }
    }
}