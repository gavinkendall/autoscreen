//////////////////////////////////////////////////////////
// Auto Screen Capture 2.0.8
// autoscreen.EditorCollection.cs
//
// Written by Gavin Kendall (gavinkendall@gmail.com)
// Thursday, 15 May 2008 - Sunday, 31 December 2017

using System;
using System.Xml;
using System.Text;
using System.Collections;

namespace autoscreen
{
    public static class ScreenshotCollection
    {
        private static ArrayList m_screenshotList = new ArrayList();

        private const string XML_FILE_INDENT_CHARS = "   ";
        private const string XML_FILE_SCREENSHOT_NODE = "screenshot";
        private const string XML_FILE_SCREENSHOTS_NODE = "screenshots";
        private const string XML_FILE_ROOT_NODE = "autoscreen";

        private const string SCREENSHOT_DATE = "date";
        private const string SCREENSHOT_PATH = "path";
        private const string SCREENSHOT_XPATH = "/" + XML_FILE_ROOT_NODE + "/" + XML_FILE_SCREENSHOTS_NODE + "/" + XML_FILE_SCREENSHOT_NODE;

        public static void Add(Screenshot screenshot)
        {
            m_screenshotList.Add(screenshot);
            Save();
        }

        public static Screenshot Get(int index)
        {
            return (Screenshot)m_screenshotList[index];
        }

        public static int Count
        {
            get { return m_screenshotList.Count; }
        }

        /// <summary>
        /// Loads the screenshots.
        /// </summary>
        public static void Load()
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(Properties.Settings.Default.Screenshots);

            XmlNodeList xscreenshots = xdoc.SelectNodes(SCREENSHOT_XPATH);

            foreach (XmlNode xscreenshot in xscreenshots)
            {
                Screenshot screenshot = new Screenshot();
                XmlNodeReader xreader = new XmlNodeReader(xscreenshot);

                while (xreader.Read())
                {
                    if (xreader.IsStartElement())
                    {
                        switch (xreader.Name)
                        {
                            case SCREENSHOT_DATE:
                                xreader.Read();
                                screenshot.Date = xreader.Value;
                                break;

                            case SCREENSHOT_PATH:
                                xreader.Read();
                                screenshot.Path = xreader.Value;
                                break;
                        }
                    }
                }

                xreader.Close();

                if (!string.IsNullOrEmpty(screenshot.Date) &&
                    !string.IsNullOrEmpty(screenshot.Path))
                {
                    Add(screenshot);
                }
            }
        }

        /// <summary>
        /// Saves the screenshots.
        /// </summary>
        public static void Save()
        {
            XmlWriterSettings xsettings = new XmlWriterSettings();
            xsettings.Indent = true;
            xsettings.CloseOutput = true;
            xsettings.CheckCharacters = true;
            xsettings.Encoding = Encoding.UTF8;
            xsettings.DoNotEscapeUriAttributes = true;
            xsettings.NewLineChars = Environment.NewLine;
            xsettings.IndentChars = XML_FILE_INDENT_CHARS;
            xsettings.NewLineHandling = NewLineHandling.Entitize;
            xsettings.ConformanceLevel = ConformanceLevel.Document;

            StringBuilder screenshots = new StringBuilder();

            using (XmlWriter xwriter = XmlWriter.Create(screenshots))
            {
                xwriter.WriteStartDocument();
                xwriter.WriteStartElement(XML_FILE_ROOT_NODE);
                xwriter.WriteStartElement(XML_FILE_SCREENSHOTS_NODE);

                foreach (object obj in m_screenshotList)
                {
                    Screenshot screenshot = (Screenshot)obj;

                    xwriter.WriteStartElement(XML_FILE_SCREENSHOT_NODE);
                    xwriter.WriteElementString(SCREENSHOT_DATE, screenshot.Date);
                    xwriter.WriteElementString(SCREENSHOT_PATH, screenshot.Path);

                    xwriter.WriteEndElement();
                }

                xwriter.WriteEndElement();
                xwriter.WriteEndElement();
                xwriter.WriteEndDocument();

                xwriter.Flush();
                xwriter.Close();
            }

            Properties.Settings.Default.Screenshots = screenshots.ToString();
            Properties.Settings.Default.Save();
        }
    }
}
