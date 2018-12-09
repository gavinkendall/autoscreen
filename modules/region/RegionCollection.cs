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

        private const string REGION_NAME = "name";
        private const string REGION_X = "x";
        private const string REGION_Y = "y";
        private const string REGION_WIDTH = "width";
        private const string REGION_HEIGHT = "height";
        private const string REGION_MACRO = "macro";
        private const string REGION_XPATH = "/" + XML_FILE_ROOT_NODE + "/" + XML_FILE_REGIONS_NODE + "/" + XML_FILE_REGION_NODE;

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

        public Region GetByIndex(int index)
        {
            return (Region)_regionList[index];
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
        public void Load()
        {
            if (File.Exists(FileSystem.ApplicationFolder + FileSystem.RegionsFile))
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(FileSystem.ApplicationFolder + FileSystem.RegionsFile);

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
                                case REGION_NAME:
                                    xReader.Read();
                                    region.Name = xReader.Value;
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

                                case REGION_MACRO:
                                    xReader.Read();
                                    region.Macro = xReader.Value;
                                    break;
                            }
                        }
                    }

                    xReader.Close();

                    if (!string.IsNullOrEmpty(region.Name))
                    {
                        Add(region);
                    }
                }
            }
        }

        /// <summary>
        /// Saves the regions.
        /// </summary>
        public void Save()
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

            using (XmlWriter xWriter = XmlWriter.Create(FileSystem.ApplicationFolder + FileSystem.RegionsFile, xSettings))
            {
                xWriter.WriteStartDocument();
                xWriter.WriteStartElement(XML_FILE_ROOT_NODE);
                xWriter.WriteStartElement(XML_FILE_REGIONS_NODE);

                foreach (object obj in _regionList)
                {
                    Region region = (Region)obj;

                    xWriter.WriteStartElement(XML_FILE_REGION_NODE);
                    xWriter.WriteElementString(REGION_NAME, region.Name);
                    xWriter.WriteElementString(REGION_X, region.X.ToString());
                    xWriter.WriteElementString(REGION_Y, region.Y.ToString());
                    xWriter.WriteElementString(REGION_WIDTH, region.Width.ToString());
                    xWriter.WriteElementString(REGION_HEIGHT, region.Height.ToString());
                    xWriter.WriteElementString(REGION_MACRO, region.Macro);

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