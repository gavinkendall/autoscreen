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
    using System.Collections.Generic;

    public static class ScreenshotCollection
    {
        private static XmlDocument xDoc = null;
        private static List<Screenshot> _screenshotList = new List<Screenshot>();

        private const string XML_FILE_INDENT_CHARS = "   ";
        private const string XML_FILE_SCREENSHOT_NODE = "screenshot";
        private const string XML_FILE_SCREENSHOTS_NODE = "screenshots";
        private const string XML_FILE_ROOT_NODE = "autoscreen";

        private const string SCREENSHOT_VIEWID = "viewid";
        private const string SCREENSHOT_DATE = "date";
        private const string SCREENSHOT_TIME = "time";
        private const string SCREENSHOT_PATH = "path";
        private const string SCREENSHOT_FORMAT = "format";
        private const string SCREENSHOT_COMPONENT = "component";
        private const string SCREENSHOT_SLIDENAME = "slidename";
        private const string SCREENSHOT_SLIDEVALUE = "slidevalue";
        private const string SCREENSHOT_ACTIVE_WINDOW_TITLE = "activewindowtitle";
        private const string SCREENSHOT_XPATH = "/" + XML_FILE_ROOT_NODE + "/" + XML_FILE_SCREENSHOTS_NODE + "/" + XML_FILE_SCREENSHOT_NODE;

        public static void Add(Screenshot newScreenshot)
        {
            _screenshotList.Add(newScreenshot);
        }

        public static void DeleteScreenshotsOlderThanDays(int daysOld)
        {
            if (daysOld > 0)
            {
                List<Screenshot> screenshotDeletedList = new List<Screenshot>();

                foreach (Screenshot screenshot in _screenshotList)
                {
                    if (Convert.ToDateTime(screenshot.Date) <= DateTime.Now.Date.AddDays(-daysOld) &&
                        File.Exists(screenshot.Path) && !screenshotDeletedList.Contains(screenshot))
                    {
                        File.Delete(screenshot.Path);

                        screenshotDeletedList.Add(screenshot);
                    }
                }

                foreach (Screenshot screenshot in screenshotDeletedList)
                {
                    _screenshotList.Remove(screenshot);
                }
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

            foreach (Screenshot screenshot in _screenshotList)
            {
                if (!dates.Contains(screenshot.Date))
                {
                    dates.Add(screenshot.Date);
                }
            }

            return dates;
        }

        public static List<Slide> GetSlidesByDate(string date)
        {
            List<Slide> slides = new List<Slide>();
            List<string> slideNames = new List<string>();

            foreach (Screenshot screenshot in _screenshotList)
            {
                if (screenshot.Date.Equals(date) && !slideNames.Contains(screenshot.Slide.Name))
                {
                    slides.Add(screenshot.Slide);
                    slideNames.Add(screenshot.Slide.Name);
                }
            }

            return slides;
        }

        public static Screenshot GetScreenshot(string slideName, Guid viewId)
        {
            Screenshot foundScreenshot = new Screenshot();

            foreach (Screenshot screenshot in _screenshotList)
            {
                if (screenshot.Slide.Name.Equals(slideName) && screenshot.ViewId.Equals(viewId))
                {
                    foundScreenshot = screenshot;
                    break;
                }
            }

            return foundScreenshot;
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
                    screenshot.Slide = new Slide();

                    XmlNodeReader xReader = new XmlNodeReader(xScreenshot);

                    while (xReader.Read())
                    {
                        if (xReader.IsStartElement())
                        {
                            switch (xReader.Name)
                            {
                                case SCREENSHOT_VIEWID:
                                    xReader.Read();
                                    screenshot.ViewId = Guid.Parse(xReader.Value);
                                    break;

                                case SCREENSHOT_DATE:
                                    xReader.Read();
                                    screenshot.Date = xReader.Value;
                                    break;

                                case SCREENSHOT_TIME:
                                    xReader.Read();
                                    screenshot.Time = xReader.Value;
                                    break;

                                case SCREENSHOT_PATH:
                                    xReader.Read();
                                    screenshot.Path = xReader.Value;
                                    break;

                                case SCREENSHOT_FORMAT:
                                    xReader.Read();
                                    screenshot.Format = imageFormatCollection.GetByName(xReader.Value);
                                    break;

                                case SCREENSHOT_COMPONENT:
                                    xReader.Read();
                                    screenshot.Component = Convert.ToInt32(xReader.Value);
                                    break;

                                case SCREENSHOT_SLIDENAME:
                                    xReader.Read();
                                    screenshot.Slide.Name = xReader.Value;
                                    break;

                                case SCREENSHOT_SLIDEVALUE:
                                    xReader.Read();
                                    screenshot.Slide.Value = xReader.Value;
                                    break;

                                case SCREENSHOT_ACTIVE_WINDOW_TITLE:
                                    xReader.Read();
                                    screenshot.ActiveWindowTitle = xReader.Value;
                                    break;
                            }
                        }
                    }

                    xReader.Close();

                    if (!string.IsNullOrEmpty(screenshot.Date) &&
                        !string.IsNullOrEmpty(screenshot.Time) &&
                        !string.IsNullOrEmpty(screenshot.Path) &&
                        screenshot.Format != null &&
                        !string.IsNullOrEmpty(screenshot.Slide.Name) &&
                        !string.IsNullOrEmpty(screenshot.Slide.Value) &&
                        !string.IsNullOrEmpty(screenshot.ActiveWindowTitle))
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
                xWriter.WriteAttributeString("app", "version", XML_FILE_ROOT_NODE, Settings.ApplicationVersion);
                xWriter.WriteAttributeString("app", "codename", XML_FILE_ROOT_NODE, Settings.ApplicationCodename);
                xWriter.WriteStartElement(XML_FILE_SCREENSHOTS_NODE);

                foreach (object obj in _screenshotList)
                {
                    Screenshot screenshot = (Screenshot)obj;

                    xWriter.WriteStartElement(XML_FILE_SCREENSHOT_NODE);
                    xWriter.WriteElementString(SCREENSHOT_VIEWID, screenshot.ViewId.ToString());
                    xWriter.WriteElementString(SCREENSHOT_DATE, screenshot.Date);
                    xWriter.WriteElementString(SCREENSHOT_TIME, screenshot.Time);
                    xWriter.WriteElementString(SCREENSHOT_PATH, screenshot.Path);
                    xWriter.WriteElementString(SCREENSHOT_FORMAT, screenshot.Format.Name);
                    xWriter.WriteElementString(SCREENSHOT_COMPONENT, screenshot.Component.ToString());
                    xWriter.WriteElementString(SCREENSHOT_SLIDENAME, screenshot.Slide.Name);
                    xWriter.WriteElementString(SCREENSHOT_SLIDEVALUE, screenshot.Slide.Value);
                    xWriter.WriteElementString(SCREENSHOT_ACTIVE_WINDOW_TITLE, screenshot.ActiveWindowTitle);

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