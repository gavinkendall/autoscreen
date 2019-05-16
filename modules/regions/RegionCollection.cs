//-----------------------------------------------------------------------
// <copyright file="RegionCollection.cs" company="Gavin Kendall">
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

    public class RegionCollection : IEnumerable<Region>
    {
        private readonly List<Region> _regionList = new List<Region>();

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
        private const string REGION_XPATH = "/" + XML_FILE_ROOT_NODE + "/" + XML_FILE_REGIONS_NODE + "/" + XML_FILE_REGION_NODE;

        public string AppCodename { get; set; }
        public string AppVersion { get; set; }

        public List<Region>.Enumerator GetEnumerator()
        {
            return _regionList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Region>)_regionList).GetEnumerator();
        }

        IEnumerator<Region> IEnumerable<Region>.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(Region region)
        {
            _regionList.Add(region);

            Log.Write("Region added: " + region.Name);
        }

        public void Remove(Region region)
        {
            _regionList.Remove(region);

            Log.Write("Region removed: " + region.Name);
        }

        public int Count
        {
            get { return _regionList.Count; }
        }

        public Region Get(Region regionToFind)
        {
            foreach (Region region in _regionList)
            {
                if (region.Equals(regionToFind))
                {
                    return region;
                }
            }

            return null;
        }

        public Region GetByViewId(Guid viewId)
        {
            foreach (Region region in _regionList)
            {
                if (region.ViewId.Equals(viewId))
                {
                    return region;
                }
            }

            return null;
        }

        public Region GetByName(string name)
        {
            foreach (Region region in _regionList)
            {
                if (region.Name.Equals(name))
                {
                    return region;
                }
            }

            return null;
        }

        /// <summary>
        /// Loads the regions.
        /// </summary>
        public void Load(ImageFormatCollection imageFormatCollection)
        {
            try
            {
                if (Directory.Exists(FileSystem.ApplicationFolder) &&
                    File.Exists(FileSystem.ApplicationFolder + FileSystem.RegionsFile))
                {
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load(FileSystem.ApplicationFolder + FileSystem.RegionsFile);

                    AppVersion = xDoc.SelectSingleNode("/autoscreen").Attributes["app:version"]?.Value;
                    AppCodename = xDoc.SelectSingleNode("/autoscreen").Attributes["app:codename"]?.Value;

                    XmlNodeList xRegions = xDoc.SelectNodes(REGION_XPATH);

                    foreach (XmlNode xRegion in xRegions)
                    {
                        Region region = new Region();
                        XmlNodeReader xReader = new XmlNodeReader(xRegion);

                        while (xReader.Read())
                        {
                            if (xReader.IsStartElement())
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
                                }
                            }
                        }

                        xReader.Close();

                        // Change the data for each region that's being loaded if we've detected that
                        // the XML file is from an older version of the application.
                        if (Settings.VersionManager.IsOldAppVersion(AppVersion, AppCodename))
                        {
                            if (Settings.VersionManager.Versions.Get("Clara", "2.1.8.2") != null)
                            {
                                region.ViewId = Guid.NewGuid();

                                // Get the screenshots folder path from the old user settings to be used for the region's folder property.
                                region.Folder = Settings.VersionManager.OldUserSettings
                                    .GetByKey("ScreenshotsDirectory", FileSystem.ScreenshotsFolder).Value.ToString();

                                region.Folder = FileSystem.CorrectDirectoryPath(region.Folder);

                                // 2.1 used "%region%", but 2.2 uses "%name%" for a region's Macro value.
                                region.Macro = region.Macro.Replace("%region%", "%name%");

                                region.Format = imageFormatCollection.GetByName(ImageFormatSpec.NAME_JPEG);
                                region.JpegQuality = 100;
                                region.ResolutionRatio = 100;
                                region.Mouse = false;
                            }
                        }

                        if (!string.IsNullOrEmpty(region.Name))
                        {
                            Add(region);
                        }
                    }

                    // Write out the regions to the XML file now that we've updated the region objects
                    // with their appropriate property values if it was an old version of the application.
                    if (Settings.VersionManager.IsOldAppVersion(AppVersion, AppCodename))
                    {
                        Save();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("RegionCollection::Load", ex);
            }
        }

        /// <summary>
        /// Saves the regions.
        /// </summary>
        public void Save()
        {
            try
            {
                if (Directory.Exists(FileSystem.ApplicationFolder))
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

                    if (File.Exists(FileSystem.ApplicationFolder + FileSystem.RegionsFile))
                    {
                        File.Delete(FileSystem.ApplicationFolder + FileSystem.RegionsFile);
                    }

                    using (XmlWriter xWriter =
                        XmlWriter.Create(FileSystem.ApplicationFolder + FileSystem.RegionsFile, xSettings))
                    {
                        xWriter.WriteStartDocument();
                        xWriter.WriteStartElement(XML_FILE_ROOT_NODE);
                        xWriter.WriteAttributeString("app", "version", XML_FILE_ROOT_NODE, Settings.ApplicationVersion);
                        xWriter.WriteAttributeString("app", "codename", XML_FILE_ROOT_NODE, Settings.ApplicationCodename);
                        xWriter.WriteStartElement(XML_FILE_REGIONS_NODE);

                        foreach (object obj in _regionList)
                        {
                            Region region = (Region) obj;

                            xWriter.WriteStartElement(XML_FILE_REGION_NODE);
                            xWriter.WriteElementString(REGION_VIEWID, region.ViewId.ToString());
                            xWriter.WriteElementString(REGION_NAME, region.Name);
                            xWriter.WriteElementString(REGION_FOLDER, FileSystem.CorrectDirectoryPath(region.Folder));
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
                }
            }
            catch (Exception ex)
            {
                Log.Write("RegionCollection::Save", ex);
            }
        }
    }
}