//-----------------------------------------------------------------------
// <copyright file="RegionCollection.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A collection of regions.</summary>
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
    /// A collection class to store and manage Region objects.
    /// </summary>
    public class RegionCollection : CollectionTemplate<Region>
    {
        private const string XML_FILE_INDENT_CHARS = "   ";
        private const string XML_FILE_REGION_NODE = "region";
        private const string XML_FILE_REGIONS_NODE = "regions";
        private const string XML_FILE_ROOT_NODE = "autoscreen";

        private const string REGION_VIEWID = "viewid";
        private const string REGION_NAME = "name";
        private const string REGION_FOLDER = "folder";
        private const string REGION_MACRO = "macro";
        private const string REGION_FORMAT = "format";
        private const string REGION_JPEG_QUALITY = "jpeg_quality";
        private const string REGION_MOUSE = "mouse";
        private const string REGION_X = "x";
        private const string REGION_Y = "y";
        private const string REGION_WIDTH = "width";
        private const string REGION_HEIGHT = "height";
        private const string REGION_ENABLE = "enable";
        private const string REGION_ENCRYPT = "encrypt";

        private readonly string REGION_XPATH;

        private string AppCodename { get; set; }
        private string AppVersion { get; set; }

        /// <summary>
        /// Region collection.
        /// </summary>
        public RegionCollection()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("/");
            sb.Append(XML_FILE_ROOT_NODE);
            sb.Append("/");
            sb.Append(XML_FILE_REGIONS_NODE);
            sb.Append("/");
            sb.Append(XML_FILE_REGION_NODE);

            REGION_XPATH = sb.ToString();
        }

        /// <summary>
        /// Loads the regions.
        /// </summary>
        public bool LoadXmlFileAndAddRegions(ImageFormatCollection imageFormatCollection, Config config, FileSystem fileSystem, Log log)
        {
            try
            {
                if (fileSystem.FileExists(fileSystem.RegionsFile))
                {
                    log.WriteDebugMessage("Regions file \"" + fileSystem.RegionsFile + "\" found. Attempting to load XML document");

                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load(fileSystem.RegionsFile);

                    log.WriteDebugMessage("XML document loaded");

                    AppVersion = xDoc.SelectSingleNode("/autoscreen").Attributes["app:version"]?.Value;
                    AppCodename = xDoc.SelectSingleNode("/autoscreen").Attributes["app:codename"]?.Value;

                    XmlNodeList xRegions = xDoc.SelectNodes(REGION_XPATH);

                    foreach (XmlNode xRegion in xRegions)
                    {
                        Region region = new Region();
                        XmlNodeReader xReader = new XmlNodeReader(xRegion);

                        while (xReader.Read())
                        {
                            if (xReader.IsStartElement() && !xReader.IsEmptyElement)
                            {
                                switch (xReader.Name)
                                {
                                    case REGION_VIEWID:
                                        xReader.Read();
                                        region.ViewId = Guid.Parse(xReader.Value);
                                        break;

                                    case REGION_NAME:
                                        xReader.Read();
                                        region.Name = xReader.Value;
                                        break;

                                    case REGION_FOLDER:
                                        xReader.Read();
                                        region.Folder = xReader.Value;
                                        break;

                                    case REGION_MACRO:
                                        xReader.Read();
                                        region.Macro = xReader.Value;
                                        break;

                                    case REGION_FORMAT:
                                        xReader.Read();
                                        region.Format = imageFormatCollection.GetByName(xReader.Value);
                                        break;

                                    case REGION_JPEG_QUALITY:
                                        xReader.Read();
                                        region.JpegQuality = Convert.ToInt32(xReader.Value);
                                        break;

                                    case REGION_MOUSE:
                                        xReader.Read();
                                        region.Mouse = Convert.ToBoolean(xReader.Value);
                                        break;

                                    case REGION_X:
                                        xReader.Read();
                                        region.X = Convert.ToInt32(xReader.Value);
                                        break;

                                    case REGION_Y:
                                        xReader.Read();
                                        region.Y = Convert.ToInt32(xReader.Value);
                                        break;

                                    case REGION_WIDTH:
                                        xReader.Read();
                                        region.Width = Convert.ToInt32(xReader.Value);
                                        break;

                                    case REGION_HEIGHT:
                                        xReader.Read();
                                        region.Height = Convert.ToInt32(xReader.Value);
                                        break;

                                    case REGION_ENABLE:
                                    case "active": // Any version older than 2.4.0.0 used "active" instead of "enable".
                                        xReader.Read();
                                        region.Enable = Convert.ToBoolean(xReader.Value);
                                        break;

                                    case REGION_ENCRYPT:
                                        xReader.Read();
                                        region.Encrypt = Convert.ToBoolean(xReader.Value);
                                        break;
                                }
                            }
                        }

                        xReader.Close();

                        // Change the data for each region that's being loaded if we've detected that
                        // the XML document is from an older version of the application.
                        if (config.Settings.VersionManager.IsOldAppVersion(config.Settings, AppCodename, AppVersion))
                        {
                            log.WriteDebugMessage("An old version of the regions file was detected. Attempting upgrade to new region schema");

                            Version v2182 = config.Settings.VersionManager.Versions.Get(Settings.CODENAME_CLARA, Settings.CODEVERSION_CLARA);
                            Version v2300 = config.Settings.VersionManager.Versions.Get(Settings.CODENAME_BOOMBAYAH, Settings.CODEVERSION_BOOMBAYAH);
                            Version v2338 = config.Settings.VersionManager.Versions.Get(Settings.CODENAME_BOOMBAYAH, "2.3.3.8");
                            Version configVersion = config.Settings.VersionManager.Versions.Get(AppCodename, AppVersion);

                            if (v2182 != null && string.IsNullOrEmpty(AppCodename) && string.IsNullOrEmpty(AppVersion))
                            {
                                log.WriteDebugMessage("Clara 2.1.8.2 or older detected");

                                region.ViewId = Guid.NewGuid();

                                // Get the screenshots folder path from the old user settings to be used for the region's folder property.
                                region.Folder = config.Settings.VersionManager.OldUserSettings.GetByKey("ScreenshotsDirectory", fileSystem.ScreenshotsFolder).Value.ToString();

                                region.Folder = fileSystem.CorrectScreenshotsFolderPath(region.Folder);

                                // 2.1 used "%region%", but 2.2 uses "%name%" for a region's Macro value.
                                region.Macro = region.Macro.Replace("%region%", "%name%");

                                region.Format = imageFormatCollection.GetByName("JPEG");
                                region.JpegQuality = 100;
                                region.Mouse = true;
                                region.Enable = true;
                                region.Encrypt = false;
                            }

                            if (v2300 != null && configVersion != null && configVersion.VersionNumber < v2300.VersionNumber)
                            {
                                log.WriteDebugMessage("Dalek 2.2.4.6 or older detected");

                                // This is a new property for Screen that was introduced in 2.3.0.0
                                // so any version before 2.3.0.0 needs to have it during an upgrade.
                                region.Enable = true;

                                // Introduced in 2.4.0.0
                                region.Encrypt = false;
                            }

                            if (v2338 != null && configVersion != null && configVersion.VersionNumber < v2338.VersionNumber)
                            {
                                log.WriteDebugMessage("Boombayah 2.3.3.7 or older detected");

                                // We don't want to include the old "Region Select / Auto Save" region that's found in 2.3.3.7 (or older)
                                // since 2.3.3.8 replaced it with a more dynamic solution when selecting a screenshot that was saved by using "Region Select -> Clipboard / Auto Save".
                                if (region.Name.Equals("Region Select / Auto Save"))
                                {
                                    region.Name = string.Empty;
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(region.Name))
                        {
                            Add(region);
                        }
                    }

                    // Write out the regions to the XML document now that we've updated the region objects
                    // with their appropriate property values if it was an old version of the application.
                    if (config.Settings.VersionManager.IsOldAppVersion(config.Settings, AppCodename, AppVersion))
                    {
                        log.WriteDebugMessage("Regions file detected as an old version");
                        SaveToXmlFile(config.Settings, fileSystem, log);
                    }
                }
                else
                {
                    log.WriteDebugMessage("WARNING: Unable to load regions");

                    SaveToXmlFile(config.Settings, fileSystem, log);
                }

                return true;
            }
            catch (Exception ex)
            {
                log.WriteExceptionMessage("RegionCollection::Load", ex);

                return false;
            }
        }

        /// <summary>
        /// Saves the regions.
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

                if (string.IsNullOrEmpty(fileSystem.RegionsFile))
                {
                    fileSystem.RegionsFile = fileSystem.DefaultRegionsFile;

                    if (fileSystem.FileExists(fileSystem.ConfigFile))
                    {
                        fileSystem.AppendToFile(fileSystem.ConfigFile, "\nRegionsFile=" + fileSystem.RegionsFile);
                    }
                }

                if (fileSystem.FileExists(fileSystem.RegionsFile))
                {
                    fileSystem.DeleteFile(fileSystem.RegionsFile);
                }

                using (XmlWriter xWriter = XmlWriter.Create(fileSystem.RegionsFile, xSettings))
                {
                    xWriter.WriteStartDocument();
                    xWriter.WriteStartElement(XML_FILE_ROOT_NODE);
                    xWriter.WriteAttributeString("app", "version", XML_FILE_ROOT_NODE, settings.ApplicationVersion);
                    xWriter.WriteAttributeString("app", "codename", XML_FILE_ROOT_NODE, settings.ApplicationCodename);
                    xWriter.WriteStartElement(XML_FILE_REGIONS_NODE);

                    foreach (Region region in base.Collection)
                    {
                        xWriter.WriteStartElement(XML_FILE_REGION_NODE);

                        xWriter.WriteElementString(REGION_ENABLE, region.Enable.ToString());
                        xWriter.WriteElementString(REGION_VIEWID, region.ViewId.ToString());
                        xWriter.WriteElementString(REGION_NAME, region.Name);
                        xWriter.WriteElementString(REGION_FOLDER, fileSystem.CorrectScreenshotsFolderPath(region.Folder));
                        xWriter.WriteElementString(REGION_MACRO, region.Macro);
                        xWriter.WriteElementString(REGION_FORMAT, region.Format.Name);
                        xWriter.WriteElementString(REGION_JPEG_QUALITY, region.JpegQuality.ToString());
                        xWriter.WriteElementString(REGION_MOUSE, region.Mouse.ToString());
                        xWriter.WriteElementString(REGION_X, region.X.ToString());
                        xWriter.WriteElementString(REGION_Y, region.Y.ToString());
                        xWriter.WriteElementString(REGION_WIDTH, region.Width.ToString());
                        xWriter.WriteElementString(REGION_HEIGHT, region.Height.ToString());
                        xWriter.WriteElementString(REGION_ENCRYPT, region.Encrypt.ToString());

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
                log.WriteExceptionMessage("RegionCollection::SaveToXmlFile", ex);

                return false;
            }
        }
    }
}