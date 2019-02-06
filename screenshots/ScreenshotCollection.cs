//-----------------------------------------------------------------------
// <copyright file="ScreenshotCollection.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Text;
    using System.Xml;

    public static class ScreenshotCollection
    {
        private static XmlDocument xDoc = null;
        private static ArrayList _screenshotList = new ArrayList();

        private const string XML_FILE_INDENT_CHARS = "   ";
        private const string XML_FILE_SCREENSHOT_NODE = "screenshot";
        private const string XML_FILE_SCREENSHOTS_NODE = "screenshots";
        private const string XML_FILE_ROOT_NODE = "autoscreen";

        private const string SCREENSHOT_DATE = "date";
        private const string SCREENSHOT_PATH = "path";
        private const string SCREENSHOT_FORMAT = "format";
        private const string SCREENSHOT_SLIDE = "slide";
        private const string SCREENSHOT_XPATH = "/" + XML_FILE_ROOT_NODE + "/" + XML_FILE_SCREENSHOTS_NODE + "/" + XML_FILE_SCREENSHOT_NODE;

        public static void Add(Screenshot newScreenshot)
        {
            try
            {
                XmlNode xScreenshots = xDoc.GetElementsByTagName(XML_FILE_SCREENSHOTS_NODE).Item(0);

                if (xScreenshots != null)
                {
                    XmlElement xNewScreenshot = xDoc.CreateElement(XML_FILE_SCREENSHOT_NODE);

                    XmlElement xNewScreenshotDate = xDoc.CreateElement(SCREENSHOT_DATE);
                    xNewScreenshotDate.InnerText = newScreenshot.Date;

                    XmlElement xNewScreenshotPath = xDoc.CreateElement(SCREENSHOT_PATH);
                    xNewScreenshotPath.InnerText = newScreenshot.Path;

                    XmlElement xNewScreenshotFormat = xDoc.CreateElement(SCREENSHOT_FORMAT);
                    xNewScreenshotFormat.InnerText = newScreenshot.Format.Name;

                    XmlElement xNewScreenshotSlidename = xDoc.CreateElement(SCREENSHOT_SLIDE);
                    xNewScreenshotSlidename.InnerText = newScreenshot.Slide;

                    // Append all the nodes in the appropriate locations.
                    xNewScreenshot.AppendChild(xNewScreenshotDate);
                    xNewScreenshot.AppendChild(xNewScreenshotPath);
                    xNewScreenshot.AppendChild(xNewScreenshotFormat);
                    xNewScreenshot.AppendChild(xNewScreenshotSlidename);
                    xScreenshots.AppendChild(xNewScreenshot);

                    // Add the new screenshot to the collection of screenshots.
                    _screenshotList.Add(newScreenshot);
                }
            }
            catch (Exception ex)
            {
                Log.Write("ScreenshotCollection::Add", ex);
            }
        }

        public static Screenshot GetByIndex(int index)
        {
            return (Screenshot)_screenshotList[index];
        }

        public static int Count
        {
            get { return _screenshotList.Count; }
        }

        public static ArrayList GetDates()
        {
            ArrayList dates = new ArrayList();

            if (xDoc != null)
            {
                XmlNodeList xScreenshots = xDoc.SelectNodes(SCREENSHOT_XPATH + "[" + SCREENSHOT_DATE + "]");

                foreach (XmlNode xScreenshot in xScreenshots)
                {
                    XmlNodeReader xReader = new XmlNodeReader(xScreenshot);

                    while (xReader.Read())
                    {
                        if (xReader.IsStartElement() && xReader.Name.Equals(SCREENSHOT_DATE))
                        {
                            xReader.Read();

                            if (!string.IsNullOrEmpty(xReader.Value) && !dates.Contains(xReader.Value))
                            {
                                dates.Add(xReader.Value);
                            }
                        }
                    }

                    xReader.Close();
                }
            }

            return dates;
        }

        public static ArrayList GetSlidesByDate(string date)
        {
            ArrayList slides = new ArrayList();

            if (xDoc != null)
            {
                XmlNodeList xScreenshots = xDoc.SelectNodes(SCREENSHOT_XPATH + "[" + SCREENSHOT_DATE + " = '" + date + "']");

                foreach (XmlNode xScreenshot in xScreenshots)
                {
                    XmlNodeReader xReader = new XmlNodeReader(xScreenshot);

                    while (xReader.Read())
                    {
                        if (xReader.Name.Equals(SCREENSHOT_SLIDE))
                        {
                            xReader.Read();

                            if (!string.IsNullOrEmpty(xReader.Value) && !slides.Contains(xReader.Value))
                            {
                                slides.Add(xReader.Value);
                            }
                        }
                    }
                }
            }

            return slides;
        }

        /// <summary>
        /// Loads the screenshots.
        /// </summary>
        public static void Load(ImageFormatCollection imageFormatCollection)
        {
            if (File.Exists(FileSystem.ApplicationFolder + FileSystem.ScreenshotsFile))
            {
                xDoc = new XmlDocument();
                xDoc.Load(FileSystem.ApplicationFolder + FileSystem.ScreenshotsFile);

                XmlNodeList xScreeshots = xDoc.SelectNodes(SCREENSHOT_XPATH);

                foreach (XmlNode xScreenshot in xScreeshots)
                {
                    Screenshot screenshot = new Screenshot();
                    XmlNodeReader xReader = new XmlNodeReader(xScreenshot);

                    while (xReader.Read())
                    {
                        if (xReader.IsStartElement())
                        {
                            switch (xReader.Name)
                            {
                                case SCREENSHOT_DATE:
                                    xReader.Read();
                                    screenshot.Date = xReader.Value;
                                    break;

                                case SCREENSHOT_PATH:
                                    xReader.Read();
                                    screenshot.Path = xReader.Value;
                                    break;

                                case SCREENSHOT_FORMAT:
                                    xReader.Read();
                                    screenshot.Format = imageFormatCollection.GetByName(xReader.Value);
                                    break;

                                case SCREENSHOT_SLIDE:
                                    xReader.Read();
                                    screenshot.Slide = xReader.Value;
                                    break;
                            }
                        }
                    }

                    xReader.Close();

                    if (!string.IsNullOrEmpty(screenshot.Date) &&
                        !string.IsNullOrEmpty(screenshot.Path) &&
                        screenshot.Format != null &&
                        !string.IsNullOrEmpty(screenshot.Slide))
                    {
                        _screenshotList.Add(screenshot);
                    }
                }
            }
        }

        /// <summary>
        /// Saves the screenshots.
        /// </summary>
        public static void Save()
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

            using (XmlWriter xWriter = XmlWriter.Create(FileSystem.ApplicationFolder + FileSystem.ScreenshotsFile, xSettings))
            {
                xWriter.WriteStartDocument();
                xWriter.WriteStartElement(XML_FILE_ROOT_NODE);
                xWriter.WriteStartElement(XML_FILE_SCREENSHOTS_NODE);

                foreach (object obj in _screenshotList)
                {
                    Screenshot screenshot = (Screenshot)obj;

                    xWriter.WriteStartElement(XML_FILE_SCREENSHOT_NODE);
                    xWriter.WriteElementString(SCREENSHOT_DATE, screenshot.Date);
                    xWriter.WriteElementString(SCREENSHOT_PATH, screenshot.Path);
                    xWriter.WriteElementString(SCREENSHOT_FORMAT, screenshot.Format.Name);
                    xWriter.WriteElementString(SCREENSHOT_SLIDE, screenshot.Slide);

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