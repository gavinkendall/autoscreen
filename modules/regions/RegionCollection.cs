//-----------------------------------------------------------------------
// <copyright file="RegionCollection.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
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
        private const string REGION_RESOLUTION_RATIO = "resolution_ratio";
        private const string REGION_MOUSE = "mouse";
        private const string REGION_X = "x";
        private const string REGION_Y = "y";
        private const string REGION_WIDTH = "width";
        private const string REGION_HEIGHT = "height";
        private const string REGION_ACTIVE = "active";

        private readonly string REGION_XPATH;

        private string AppCodename { get; set; }
        private string AppVersion { get; set; }

        /// <summary>
        /// The empty constructor for the region collection.
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
        public bool LoadXmlFileAndAddRegions(ImageFormatCollection imageFormatCollection)
        {
            try
            {
                if (FileSystem.FileExists(FileSystem.RegionsFile))
                {
                    Log.WriteDebugMessage("Regions file \"" + FileSystem.RegionsFile + "\" found. Attempting to load XML document");

                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load(FileSystem.RegionsFile);

                    Log.WriteDebugMessage("XML document loaded");

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

                                    case REGION_RESOLUTION_RATIO:
                                        xReader.Read();
                                        region.ResolutionRatio = Convert.ToInt32(xReader.Value);
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

                                    case REGION_ACTIVE:
                                        xReader.Read();
                                        region.Active = Convert.ToBoolean(xReader.Value);
                                        break;
                                }
                            }
                        }

                        xReader.Close();

                        // Change the data for each region that's being loaded if we've detected that
                        // the XML document is from an older version of the application.
                        if (Settings.VersionManager.IsOldAppVersion(AppCodename, AppVersion))
                        {
                            Log.WriteDebugMessage("An old version of the regions file was detected. Attempting upgrade to new region schema");

                            Version v2182 = Settings.VersionManager.Versions.Get(Settings.CODENAME_CLARA, Settings.CODEVERSION_CLARA);
                            Version v2300 = Settings.VersionManager.Versions.Get(Settings.CODENAME_BOOMBAYAH, Settings.CODEVERSION_BOOMBAYAH);
                            Version configVersion = Settings.VersionManager.Versions.Get(AppCodename, AppVersion);

                            if (v2182 != null && string.IsNullOrEmpty(AppCodename) && string.IsNullOrEmpty(AppVersion))
                            {
                                Log.WriteDebugMessage("Clara 2.1.8.2 or older detected");

                                region.ViewId = Guid.NewGuid();

                                // Get the screenshots folder path from the old user settings to be used for the region's folder property.
                                region.Folder = Settings.VersionManager.OldSettings.GetByKey("ScreenshotsDirectory", FileSystem.ScreenshotsFolder).Value.ToString();

                                region.Folder = FileSystem.CorrectScreenshotsFolderPath(region.Folder);

                                // 2.1 used "%region%", but 2.2 uses "%name%" for a region's Macro value.
                                region.Macro = region.Macro.Replace("%region%", "%name%");

                                region.Format = imageFormatCollection.GetByName(ImageFormatSpec.NAME_JPEG);
                                region.JpegQuality = 100;
                                region.ResolutionRatio = 100;
                                region.Mouse = true;
                                region.Active = true;
                            }

                            if (v2300 != null && configVersion != null && configVersion.VersionNumber < v2300.VersionNumber)
                            {
                                Log.WriteDebugMessage("Dalek 2.2.4.6 or older detected");

                                // This is a new property for Screen that was introduced in 2.3.0.0
                                // so any version before 2.3.0.0 needs to have it during an upgrade.
                                region.Active = true;
                            }
                        }

                        if (!string.IsNullOrEmpty(region.Name))
                        {
                            Add(region);
                        }
                    }

                    // Write out the regions to the XML document now that we've updated the region objects
                    // with their appropriate property values if it was an old version of the application.
                    if (Settings.VersionManager.IsOldAppVersion(AppCodename, AppVersion))
                    {
                        Log.WriteDebugMessage("Regions file detected as an old version");
                        SaveToXmlFile();
                    }
                }
                else
                {
                    Log.WriteDebugMessage("WARNING: Unable to load regions");

                    SaveToXmlFile();
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("RegionCollection::Load", ex);

                return false;
            }
        }

        /// <summary>
        /// Saves the regions.
        /// </summary>
        public bool SaveToXmlFile()
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

                if (string.IsNullOrEmpty(FileSystem.RegionsFile))
                {
                    FileSystem.RegionsFile = FileSystem.DefaultRegionsFile;

                    if (FileSystem.FileExists(FileSystem.ConfigFile))
                    {
                        FileSystem.AppendToFile(FileSystem.ConfigFile, "\nRegionsFile=" + FileSystem.RegionsFile);
                    }
                }

                FileSystem.DeleteFile(FileSystem.RegionsFile);

                using (XmlWriter xWriter =
                    XmlWriter.Create(FileSystem.RegionsFile, xSettings))
                {
                    xWriter.WriteStartDocument();
                    xWriter.WriteStartElement(XML_FILE_ROOT_NODE);
                    xWriter.WriteAttributeString("app", "version", XML_FILE_ROOT_NODE, Settings.ApplicationVersion);
                    xWriter.WriteAttributeString("app", "codename", XML_FILE_ROOT_NODE, Settings.ApplicationCodename);
                    xWriter.WriteStartElement(XML_FILE_REGIONS_NODE);

                    foreach (Region region in base.Collection)
                    {
                        xWriter.WriteStartElement(XML_FILE_REGION_NODE);

                        xWriter.WriteElementString(REGION_ACTIVE, region.Active.ToString());
                        xWriter.WriteElementString(REGION_VIEWID, region.ViewId.ToString());
                        xWriter.WriteElementString(REGION_NAME, region.Name);
                        xWriter.WriteElementString(REGION_FOLDER, FileSystem.CorrectScreenshotsFolderPath(region.Folder));
                        xWriter.WriteElementString(REGION_MACRO, region.Macro);
                        xWriter.WriteElementString(REGION_FORMAT, region.Format.Name);
                        xWriter.WriteElementString(REGION_JPEG_QUALITY, region.JpegQuality.ToString());
                        xWriter.WriteElementString(REGION_RESOLUTION_RATIO, region.ResolutionRatio.ToString());
                        xWriter.WriteElementString(REGION_MOUSE, region.Mouse.ToString());
                        xWriter.WriteElementString(REGION_X, region.X.ToString());
                        xWriter.WriteElementString(REGION_Y, region.Y.ToString());
                        xWriter.WriteElementString(REGION_WIDTH, region.Width.ToString());
                        xWriter.WriteElementString(REGION_HEIGHT, region.Height.ToString());

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
                Log.WriteExceptionMessage("RegionCollection::SaveToXmlFile", ex);

                return false;
            }
        }
    }
}