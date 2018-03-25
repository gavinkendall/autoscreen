//////////////////////////////////////////////////////////
// Auto Screen Capture 2.1.0
// autoscreen.ScreenshotCollection.cs
//
// Developed by Gavin Kendall
// Thursday, 15 May 2008 - Sunday, 25 March 2018

using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace autoscreen
{
    public static class ScreenshotCollection
    {
        private static XmlDocument xDoc = null;
        private static ArrayList m_screenshotList = new ArrayList();

        private const string XML_FILE_INDENT_CHARS = "   ";
        private const string XML_FILE_SCREENSHOT_NODE = "screenshot";
        private const string XML_FILE_SCREENSHOTS_NODE = "screenshots";
        private const string XML_FILE_ROOT_NODE = "autoscreen";

        private const string SCREENSHOT_INDEX = "index";
        private const string SCREENSHOT_DATE = "date";
        private const string SCREENSHOT_PATH = "path";
        private const string SCREENSHOT_SCREEN = "screen";
        private const string SCREENSHOT_FORMAT = "format";
        private const string SCREENSHOT_FILENAME = "filename";
        private const string SCREENSHOT_SLIDENAME = "slidename";
        private const string SCREENSHOT_XPATH = "/" + XML_FILE_ROOT_NODE + "/" + XML_FILE_SCREENSHOTS_NODE + "/" + XML_FILE_SCREENSHOT_NODE;

        public static void Add(Screenshot newScreenshot, ScreenshotType screenshotType)
        {
            // Make sure we only intend to add the new screenshot to the user's XML formatted settings.
            if (screenshotType == ScreenshotType.User)
            {
                XmlNode xScreenshots = xDoc.GetElementsByTagName(XML_FILE_SCREENSHOTS_NODE).Item(0);

                XmlElement xNewScreenshot = xDoc.CreateElement(XML_FILE_SCREENSHOT_NODE);

                XmlElement xNewScreenshotIndex = xDoc.CreateElement(SCREENSHOT_INDEX);
                xNewScreenshotIndex.InnerText = newScreenshot.Index.ToString();

                XmlElement xNewScreenshotDate = xDoc.CreateElement(SCREENSHOT_DATE);
                xNewScreenshotDate.InnerText = newScreenshot.Date;

                XmlElement xNewScreenshotPath = xDoc.CreateElement(SCREENSHOT_PATH);
                xNewScreenshotPath.InnerText = newScreenshot.Path;

                XmlElement xNewScreenshotScreen = xDoc.CreateElement(SCREENSHOT_SCREEN);
                xNewScreenshotScreen.InnerText = newScreenshot.Screen.ToString();

                XmlElement xNewScreenshotFormat = xDoc.CreateElement(SCREENSHOT_FORMAT);
                xNewScreenshotFormat.InnerText = newScreenshot.Format;

                XmlElement xNewScreenshotFilename = xDoc.CreateElement(SCREENSHOT_FILENAME);
                xNewScreenshotFilename.InnerText = newScreenshot.Filename;

                XmlElement xNewScreenshotSlidename = xDoc.CreateElement(SCREENSHOT_SLIDENAME);
                xNewScreenshotSlidename.InnerText = newScreenshot.Slidename;

                // Append all the nodes in the appropriate locations.
                xNewScreenshot.AppendChild(xNewScreenshotIndex);
                xNewScreenshot.AppendChild(xNewScreenshotDate);
                xNewScreenshot.AppendChild(xNewScreenshotPath);
                xNewScreenshot.AppendChild(xNewScreenshotScreen);
                xNewScreenshot.AppendChild(xNewScreenshotFormat);
                xNewScreenshot.AppendChild(xNewScreenshotFilename);
                xNewScreenshot.AppendChild(xNewScreenshotSlidename);
                xScreenshots.AppendChild(xNewScreenshot);

                // Add the new screenshot to the collection of screenshots.
                m_screenshotList.Add(newScreenshot);
            }
        }

        public static Screenshot GetByIndex(int index)
        {
            return (Screenshot)m_screenshotList[index];
        }

        public static int Count
        {
            get { return m_screenshotList.Count; }
        }

        public static Screenshot GetBySlidename(string slidename, int screenNumber)
        {
            Screenshot screenshot = null;

            if (xDoc != null)
            {
                XmlNodeList xScreenshots = xDoc.SelectNodes(SCREENSHOT_XPATH + "[" + SCREENSHOT_SLIDENAME + " = '" + slidename + "' and " + SCREENSHOT_SCREEN + " = '" + screenNumber + "']");

                foreach (XmlNode xScreenshot in xScreenshots)
                {
                    XmlNodeReader xReader = new XmlNodeReader(xScreenshot);

                    while (xReader.Read())
                    {
                        if (xReader.IsStartElement() && xReader.Name.Equals(SCREENSHOT_INDEX))
                        {
                            xReader.Read();

                            if (!string.IsNullOrEmpty(xReader.Value))
                            {
                                screenshot = GetByIndex(Convert.ToInt32(xReader.Value));
                            }
                        }
                    }

                    xReader.Close();
                }
            }

            return screenshot;
        }

        /// <summary>
        /// Loads the screenshots.
        /// </summary>
        public static void Load()
        {
            xDoc = new XmlDocument();
            xDoc.LoadXml(Properties.Settings.Default.Screenshots);

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
                            case SCREENSHOT_INDEX:
                                xReader.Read();
                                screenshot.Index = string.IsNullOrEmpty(xReader.Value) ? -1 : Convert.ToInt32(xReader.Value);
                                break;

                            case SCREENSHOT_DATE:
                                xReader.Read();
                                screenshot.Date = xReader.Value;
                                break;

                            case SCREENSHOT_PATH:
                                xReader.Read();
                                screenshot.Path = xReader.Value;
                                break;

                            case SCREENSHOT_SCREEN:
                                xReader.Read();
                                screenshot.Screen = string.IsNullOrEmpty(xReader.Value) ? 0 : Convert.ToInt32(xReader.Value);
                                break;

                            case SCREENSHOT_FORMAT:
                                xReader.Read();
                                screenshot.Format = xReader.Value;
                                break;

                            case SCREENSHOT_FILENAME:
                                xReader.Read();
                                screenshot.Filename = xReader.Value;
                                break;

                            case SCREENSHOT_SLIDENAME:
                                xReader.Read();
                                screenshot.Slidename = xReader.Value;
                                break;
                        }
                    }
                }

                xReader.Close();

                if (!string.IsNullOrEmpty(screenshot.Date) &&
                    !string.IsNullOrEmpty(screenshot.Path) &&
                    screenshot.Screen > 0 &&
                    !string.IsNullOrEmpty(screenshot.Format) &&
                    !string.IsNullOrEmpty(screenshot.Filename) &&
                    !string.IsNullOrEmpty(screenshot.Slidename))
                {
                    m_screenshotList.Add(screenshot);
                }
            }
        }

        /// <summary>
        /// Saves the screenshots.
        /// </summary>
        public static void Save()
        {
            XmlWriterSettings xSettings = new XmlWriterSettings();
            xSettings.Indent = true;
            xSettings.CloseOutput = true;
            xSettings.CheckCharacters = true;
            xSettings.Encoding = Encoding.UTF8;
            xSettings.DoNotEscapeUriAttributes = true;
            xSettings.NewLineChars = Environment.NewLine;
            xSettings.IndentChars = XML_FILE_INDENT_CHARS;
            xSettings.NewLineHandling = NewLineHandling.Entitize;
            xSettings.ConformanceLevel = ConformanceLevel.Document;

            StringBuilder screenshots = new StringBuilder();

            using (XmlWriter xWriter = XmlWriter.Create(screenshots))
            {
                xWriter.WriteStartDocument();
                xWriter.WriteStartElement(XML_FILE_ROOT_NODE);
                xWriter.WriteStartElement(XML_FILE_SCREENSHOTS_NODE);

                foreach (object obj in m_screenshotList)
                {
                    Screenshot screenshot = (Screenshot)obj;

                    xWriter.WriteStartElement(XML_FILE_SCREENSHOT_NODE);
                    xWriter.WriteElementString(SCREENSHOT_INDEX, screenshot.Index.ToString());
                    xWriter.WriteElementString(SCREENSHOT_DATE, screenshot.Date);
                    xWriter.WriteElementString(SCREENSHOT_PATH, screenshot.Path);
                    xWriter.WriteElementString(SCREENSHOT_SCREEN, screenshot.Screen.ToString());
                    xWriter.WriteElementString(SCREENSHOT_FORMAT, screenshot.Format);
                    xWriter.WriteElementString(SCREENSHOT_FILENAME, screenshot.Filename);
                    xWriter.WriteElementString(SCREENSHOT_SLIDENAME, screenshot.Slidename);

                    xWriter.WriteEndElement();
                }

                xWriter.WriteEndElement();
                xWriter.WriteEndElement();
                xWriter.WriteEndDocument();

                xWriter.Flush();
                xWriter.Close();
            }

            Properties.Settings.Default.Screenshots = screenshots.ToString();
        }
    }
}