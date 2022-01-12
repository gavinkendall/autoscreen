//-----------------------------------------------------------------------
// <copyright file="ScreenCollection.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A collection of screens.</summary>
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
        private const string SCREEN_MOUSE = "mouse";
        private const string SCREEN_ENABLE = "enable";
        private const string SCREEN_X = "x";
        private const string SCREEN_Y = "y";
        private const string SCREEN_WIDTH = "width";
        private const string SCREEN_HEIGHT = "height";
        private const string SCREEN_SOURCE = "source";
        private const string SCREEN_DEVICE_NAME = "device_name";
        private const string SCREEN_AUTO_ADAPT = "auto_adapt";
        private const string SCREEN_CAPTURE_METHOD = "capture_method";
        private const string SCREEN_ENCRYPT = "encrypt";

        private readonly string SCREEN_XPATH;

        private string AppCodename { get; set; }
        private string AppVersion { get; set; }

        private ImageFormatCollection _imageFormatCollection;

        /// <summary>
        /// Adds the available screens to the screen collection based on the current display setup while Auto Screen Capture is starting up for the first time.
        /// </summary>
        /// <param name="macroParser">The macro parser class to use.</param>
        /// <param name="fileSystem">The file system class to use.</param>
        /// <param name="log">The logging class to use.</param>
        private void AddDefaultScreens(MacroParser macroParser, FileSystem fileSystem, Log log)
        {
            int screenNumber = 1;
            const int MAX_DEFAULT_SCREENS = 5;

            for (int i = 0; i < MAX_DEFAULT_SCREENS; i++)
            {
                Add(new Screen()
                {
                    ViewId = Guid.NewGuid(),
                    Name = "Screen " + screenNumber,
                    Folder = fileSystem.ScreenshotsFolder,
                    Macro = fileSystem.FilenamePattern,
                    Component = 0,
                    Format = _imageFormatCollection.GetByName(ScreenCapture.DefaultImageFormat),
                    JpegQuality = 100,
                    Mouse = true,
                    Enable = true,
                    X = 0,
                    Y = 0,
                    Width = 0,
                    Height = 0,
                    Source = 0,
                    DeviceName = string.Empty,
                    AutoAdapt = true, // 2.4.0.0 has a new "Auto Adapt" feature which will automatically figure out the position and resolution for each screen.
                    CaptureMethod = 1,
                    Encrypt = false
                });

                log.WriteDebugMessage($"Screen {screenNumber} created using \"{fileSystem.ScreenshotsFolder}\" for folder path and \"{fileSystem.FilenamePattern}\" for macro.");

                screenNumber++;
            }
        }

        /// <summary>
        /// The empty constructor for the screen collection.
        /// </summary>
        public ScreenCollection()
        {
            _imageFormatCollection = new ImageFormatCollection();

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
        /// Gets a Screen give its source index and component index.
        /// </summary>
        /// <param name="source">The source index.</param>
        /// <param name="component">The component index.</param>
        /// <returns>A Screen object based on its source index and component index.</returns>
        public Screen GetBySourceAndComponent(int source, int component)
        {
            foreach (Screen screen in base.Collection)
            {
                if (screen.Source == source && screen.Component == component)
                {
                    return screen;
                }
            }

            return null;
        }

        /// <summary>
        /// Loads the screens.
        /// </summary>
        /// <param name="imageFormatCollection">The image format collection to use.</param>
        /// <param name="config">The configuration to use.</param>
        /// <param name="macroParser">The macro parser to use.</param>
        /// <param name="fileSystem">The file system to use.</param>
        /// <param name="log">The logging class to use.</param>
        /// <returns>Returns true if we were able to load the screens.xml file and add all the available screens otherwise returns false.</returns>
        public bool LoadXmlFileAndAddScreens(ImageFormatCollection imageFormatCollection, Config config, MacroParser macroParser, FileSystem fileSystem, Log log)
        {
            try
            {
                if (fileSystem.FileExists(fileSystem.ScreensFile))
                {
                    log.WriteDebugMessage("Screens file \"" + fileSystem.ScreensFile + "\" found. Attempting to load XML document");

                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load(fileSystem.ScreensFile);

                    log.WriteDebugMessage("XML document loaded");

                    AppVersion = xDoc.SelectSingleNode("/autoscreen").Attributes["app:version"]?.Value;
                    AppCodename = xDoc.SelectSingleNode("/autoscreen").Attributes["app:codename"]?.Value;

                    XmlNodeList xScreens = xDoc.SelectNodes(SCREEN_XPATH);

                    foreach (XmlNode xScreen in xScreens)
                    {
                        Screen screen = new Screen();
                        XmlNodeReader xReader = new XmlNodeReader(xScreen);

                        while (xReader.Read())
                        {
                            if (xReader.IsStartElement() && !xReader.IsEmptyElement)
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

                                    case SCREEN_MOUSE:
                                        xReader.Read();
                                        screen.Mouse = Convert.ToBoolean(xReader.Value);
                                        break;

                                    case SCREEN_ENABLE:
                                    case "active": // Any version older than 2.4.0.0 used "active" instead of "enable".
                                        xReader.Read();
                                        screen.Enable = Convert.ToBoolean(xReader.Value);
                                        break;

                                    case SCREEN_X:
                                        xReader.Read();
                                        screen.X = Convert.ToInt32(xReader.Value);
                                        break;

                                    case SCREEN_Y:
                                        xReader.Read();
                                        screen.Y = Convert.ToInt32(xReader.Value);
                                        break;

                                    case SCREEN_WIDTH:
                                        xReader.Read();
                                        screen.Width = Convert.ToInt32(xReader.Value);
                                        break;

                                    case SCREEN_HEIGHT:
                                        xReader.Read();
                                        screen.Height = Convert.ToInt32(xReader.Value);
                                        break;

                                    case SCREEN_SOURCE:
                                        xReader.Read();
                                        screen.Source = Convert.ToInt32(xReader.Value);
                                        break;

                                    case SCREEN_DEVICE_NAME:
                                        xReader.Read();
                                        screen.DeviceName = xReader.Value;
                                        break;

                                    case SCREEN_AUTO_ADAPT:
                                        xReader.Read();
                                        screen.AutoAdapt = Convert.ToBoolean(xReader.Value);
                                        break;

                                    case SCREEN_CAPTURE_METHOD:
                                        xReader.Read();
                                        screen.CaptureMethod = Convert.ToInt32(xReader.Value);
                                        break;

                                    case SCREEN_ENCRYPT:
                                        xReader.Read();
                                        screen.Encrypt = Convert.ToBoolean(xReader.Value);
                                        break;
                                }
                            }
                        }

                        xReader.Close();

                        // Change the data for each Screen that's being loaded if we've detected that
                        // the XML document is from an older version of the application.
                        if (config.Settings.VersionManager.IsOldAppVersion(config.Settings, AppCodename, AppVersion))
                        {
                            log.WriteDebugMessage("An old version of the screens.xml file was detected. Attempting upgrade to new schema.");

                            Version v2300 = config.Settings.VersionManager.Versions.Get(Settings.CODENAME_BOOMBAYAH, Settings.CODEVERSION_BOOMBAYAH);
                            Version v2338 = config.Settings.VersionManager.Versions.Get(Settings.CODENAME_BOOMBAYAH, "2.3.3.8");
                            Version configVersion = config.Settings.VersionManager.Versions.Get(AppCodename, AppVersion);

                            if (v2300 != null && configVersion != null && configVersion.VersionNumber < v2300.VersionNumber)
                            {
                                log.WriteDebugMessage("Dalek 2.2.4.6 or older detected");

                                // This is a new property for Screen that was introduced in 2.3.0.0
                                // so any version before 2.3.0.0 needs to have it during an upgrade.
                                screen.Enable = true;
                            }

                            if (v2338 != null && configVersion != null && configVersion.VersionNumber < v2338.VersionNumber)
                            {
                                log.WriteDebugMessage("Boombayah 2.3.3.7 or older detected");

                                int screenIndex = 0;

                                foreach (System.Windows.Forms.Screen screenFromWindows in System.Windows.Forms.Screen.AllScreens)
                                {
                                    if (screen.Component.Equals(screenIndex + 1))
                                    {
                                        screen.X = 0;
                                        screen.Y = 0;
                                        screen.Width = 0;
                                        screen.Height = 0;
                                        screen.Source = 0;
                                        screen.DeviceName = string.Empty;
                                        screen.CaptureMethod = 1;
                                        screen.AutoAdapt = true; // 2.4.0.0 has a new "Auto Adapt" feature which will automatically figure out the position and resolution for each screen.
                                        screen.Encrypt = false;
                                    }

                                    screenIndex++;
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(screen.Name))
                        {
                            Add(screen);
                        }
                    }

                    if (config.Settings.VersionManager.IsOldAppVersion(config.Settings, AppCodename, AppVersion))
                    {
                        log.WriteDebugMessage("Screens file detected as an old version");
                        SaveToXmlFile(config.Settings, fileSystem, log);
                    }
                }
                else
                {
                    log.WriteDebugMessage("WARNING: Unable to load screens");

                    // Active Window
                    if (config.Settings.VersionManager.IsOldAppVersion(config.Settings, AppCodename, AppVersion))
                    {
                        Version v2182 = config.Settings.VersionManager.Versions.Get(Settings.CODENAME_CLARA, Settings.CODEVERSION_CLARA);
                        Version configVersion = config.Settings.VersionManager.Versions.Get(AppCodename, AppVersion);

                        if (v2182 != null && configVersion != null && v2182.VersionNumber == configVersion.VersionNumber)
                        {
                            Add(new Screen()
                            {
                                ViewId = Guid.NewGuid(),
                                Name = "Active Window",
                                Folder = fileSystem.ScreenshotsFolder,
                                Macro = fileSystem.FilenamePattern,
                                Component = 0,
                                Format = _imageFormatCollection.GetByName(ScreenCapture.DefaultImageFormat),
                                JpegQuality = 100,
                                Mouse = true,
                                Enable = true,
                                X = 0,
                                Y = 0,
                                Width = 0,
                                Height = 0,
                                Source = 0,
                                DeviceName = string.Empty,
                                AutoAdapt = false,
                                CaptureMethod = 1, // BitBlt
                                Encrypt = false
                            });
                        }
                    }

                    // Since we're creating the screens.xml file for the first time we'll need to add a default number of screens and have them all part of the "Auto Adapt" group.
                    AddDefaultScreens(macroParser, fileSystem, log);

                    SaveToXmlFile(config.Settings, fileSystem, log);
                }

                return true;
            }
            catch (Exception ex)
            {
                log.WriteExceptionMessage("ScreenCollection::LoadXmlFileAndAddScreens", ex);

                return false;
            }
        }

        /// <summary>
        /// Saves the screens.
        /// </summary>
        public bool SaveToXmlFile(Settings settings, FileSystem fileSystem, Log log)
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

                if (string.IsNullOrEmpty(fileSystem.ScreensFile))
                {
                    fileSystem.ScreensFile = fileSystem.DefaultScreensFile;

                    if (fileSystem.FileExists(fileSystem.ConfigFile))
                    {
                        fileSystem.AppendToFile(fileSystem.ConfigFile, "\nScreensFile=" + fileSystem.ScreensFile);
                    }
                }

                if (fileSystem.FileExists(fileSystem.ScreensFile))
                {
                    fileSystem.DeleteFile(fileSystem.ScreensFile);
                }

                using (XmlWriter xWriter = XmlWriter.Create(fileSystem.ScreensFile, xSettings))
                {
                    xWriter.WriteStartDocument();
                    xWriter.WriteStartElement(XML_FILE_ROOT_NODE);
                    xWriter.WriteAttributeString("app", "version", XML_FILE_ROOT_NODE, settings.ApplicationVersion);
                    xWriter.WriteAttributeString("app", "codename", XML_FILE_ROOT_NODE, settings.ApplicationCodename);
                    xWriter.WriteStartElement(XML_FILE_SCREENS_NODE);

                    foreach (Screen screen in base.Collection)
                    {
                        xWriter.WriteStartElement(XML_FILE_SCREEN_NODE);

                        xWriter.WriteElementString(SCREEN_ENABLE, screen.Enable.ToString());
                        xWriter.WriteElementString(SCREEN_VIEWID, screen.ViewId.ToString());
                        xWriter.WriteElementString(SCREEN_NAME, screen.Name);
                        xWriter.WriteElementString(SCREEN_FOLDER, fileSystem.CorrectScreenshotsFolderPath(screen.Folder));
                        xWriter.WriteElementString(SCREEN_MACRO, screen.Macro);
                        xWriter.WriteElementString(SCREEN_COMPONENT, screen.Component.ToString());
                        xWriter.WriteElementString(SCREEN_FORMAT, screen.Format.Name);
                        xWriter.WriteElementString(SCREEN_JPEG_QUALITY, screen.JpegQuality.ToString());
                        xWriter.WriteElementString(SCREEN_MOUSE, screen.Mouse.ToString());
                        xWriter.WriteElementString(SCREEN_X, screen.X.ToString());
                        xWriter.WriteElementString(SCREEN_Y, screen.Y.ToString());
                        xWriter.WriteElementString(SCREEN_WIDTH, screen.Width.ToString());
                        xWriter.WriteElementString(SCREEN_HEIGHT, screen.Height.ToString());
                        xWriter.WriteElementString(SCREEN_SOURCE, screen.Source.ToString());
                        xWriter.WriteElementString(SCREEN_DEVICE_NAME, screen.DeviceName);
                        xWriter.WriteElementString(SCREEN_AUTO_ADAPT, screen.AutoAdapt.ToString());
                        xWriter.WriteElementString(SCREEN_CAPTURE_METHOD, screen.CaptureMethod.ToString());
                        xWriter.WriteElementString(SCREEN_ENCRYPT, screen.Encrypt.ToString());

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
                log.WriteExceptionMessage("ScreenCollection::SaveToXmlFile", ex);

                return false;
            }
        }
    }
}