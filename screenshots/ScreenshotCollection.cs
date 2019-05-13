//-----------------------------------------------------------------------
// <copyright file="ScreenshotCollection.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------

using System.Linq;

namespace AutoScreenCapture
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text.RegularExpressions;

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
        private const string SCREENSHOT_SCREEN = "screen";
        private const string SCREENSHOT_COMPONENT = "component";
        private const string SCREENSHOT_SLIDENAME = "slidename";
        private const string SCREENSHOT_SLIDEVALUE = "slidevalue";
        private const string SCREENSHOT_WINDOW_TITLE = "windowtitle";
        private const string SCREENSHOT_LABEL = "label";
        private const string SCREENSHOT_XPATH = "/" + XML_FILE_ROOT_NODE + "/" + XML_FILE_SCREENSHOTS_NODE + "/" + XML_FILE_SCREENSHOT_NODE;

        public static string AppCodename { get; set; }
        public static string AppVersion { get; set; }

        public static void Add(Screenshot newScreenshot, ScreenCollection screenCollection, RegionCollection regionCollection)
        {
            _screenshotList.Add(newScreenshot);
        }

        public static void KeepScreenshotsForDays(int days)
        {
            if (days > 0)
            {
                List<Screenshot> screenshotDeletedList = new List<Screenshot>();

                foreach (Screenshot screenshot in _screenshotList)
                {
                    if (Convert.ToDateTime(screenshot.Date) <= DateTime.Now.Date.AddDays(-days))
                    {
                        if (File.Exists(screenshot.Path))
                        {
                            File.Delete(screenshot.Path);
                        }

                        if (!screenshotDeletedList.Contains(screenshot))
                        {
                            screenshotDeletedList.Add(screenshot);
                        }
                    }
                }

                foreach (Screenshot screenshot in screenshotDeletedList)
                {
                    _screenshotList.Remove(screenshot);
                }

                Save();
            }
        }

        public static Screenshot Get(int index)
        {
            return (Screenshot)_screenshotList[index];
        }

        public static int Count
        {
            get { return _screenshotList.Count; }
        }

        public static List<string> GetFilterValueList(string filterType)
        {
            if (filterType.Equals("Image Format"))
            {
                return _screenshotList.Select(x => x.Format.Name).Distinct().ToList();
            }

            if (filterType.Equals("Label"))
            {
                return _screenshotList.Select(x => x.Label).Distinct().ToList();
            }

            if (filterType.Equals("Region"))
            {

            }

            if (filterType.Equals("Screen"))
            {

            }

            if (filterType.Equals("Window Title"))
            {
                return _screenshotList.Select(x => x.WindowTitle).Distinct().ToList();
            }

            return new List<string>();
        }

        public static List<string> GetDates(string filterType, string filterValue)
        {
            if (!string.IsNullOrEmpty(filterValue))
            {
                if (filterType.Equals("Image Format"))
                {
                    return _screenshotList.Where(x => x.Format.Name.Equals(filterValue)).Select(x => x.Date).ToList();
                }

                if (filterType.Equals("Label"))
                {
                    return _screenshotList.Where(x => x.Label.Equals(filterValue)).Select(x => x.Date).ToList();
                }

                if (filterType.Equals("Region"))
                {

                }

                if (filterType.Equals("Screen"))
                {

                }

                if (filterType.Equals("Window Title"))
                {
                    return _screenshotList.Where(x => x.WindowTitle.Equals(filterValue)).Select(x => x.Date).ToList();
                }
            }

            return _screenshotList.Select(x => x.Date).ToList();
        }

        public static List<Slide> GetSlides(string filterType, string filterValue, string date)
        {
            if (!string.IsNullOrEmpty(filterValue))
            {
                if (filterType.Equals("Image Format"))
                {
                    return _screenshotList.Where(x => x.Format.Name.Equals(filterValue) && x.Date.Equals(date)).Select(x => x.Slide).ToList();
                }

                if (filterType.Equals("Label"))
                {
                    return _screenshotList.Where(x => x.Label.Equals(filterValue) && x.Date.Equals(date)).Select(x => x.Slide).ToList();
                }

                if (filterType.Equals("Region"))
                {

                }

                if (filterType.Equals("Screen"))
                {

                }

                if (filterType.Equals("Window Title"))
                {
                    return _screenshotList.Where(x => x.WindowTitle.Equals(filterValue) && x.Date.Equals(date)).Select(x => x.Slide).ToList();
                }
            }

            return _screenshotList.Where(x => x.Date.Equals(date)).Select(x => x.Slide).ToList();
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
        public static void Load(ImageFormatCollection imageFormatCollection, ScreenCollection screenCollection, RegionCollection regionCollection)
        {
            if (Directory.Exists(FileSystem.ApplicationFolder) &&
                File.Exists(FileSystem.ApplicationFolder + FileSystem.ScreenshotsFile))
            {
                xDoc = new XmlDocument();
                xDoc.Load(FileSystem.ApplicationFolder + FileSystem.ScreenshotsFile);

                AppVersion = xDoc.SelectSingleNode("/autoscreen").Attributes["app:version"]?.Value;
                AppCodename = xDoc.SelectSingleNode("/autoscreen").Attributes["app:codename"]?.Value;

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

                                // 2.1 used "screen" for its definition of each display/monitor whereas 2.2 uses "component".
                                // Active Window is now represented by 0 rather than 5.
                                case SCREENSHOT_SCREEN:
                                    if (Settings.VersionManager.IsOldAppVersion(AppVersion, AppCodename) &&
                                        Settings.VersionManager.Versions.Get("Clara", "2.1.8.2") != null)
                                    {
                                        xReader.Read();

                                        screenshot.Screen = Convert.ToInt32(xReader.Value);

                                        screenshot.Component = screenshot.Screen == 5 ? 0 : screenshot.Screen;
                                    }
                                    break;

                                // We still want to support "component" since this was introduced in version 2.2 as the new representation for "screen".
                                case SCREENSHOT_COMPONENT:
                                    xReader.Read();
                                    screenshot.Component = Convert.ToInt32(xReader.Value);

                                    if (screenshot.Component == -1)
                                    {
                                        screenshot.ScreenshotType = ScreenshotType.Region;
                                    }
                                    else if (screenshot.Component == 0)
                                    {
                                        screenshot.ScreenshotType = ScreenshotType.ActiveWindow;
                                    }
                                    else
                                    {
                                        screenshot.ScreenshotType = ScreenshotType.Screen;
                                    }
                                    break;

                                case SCREENSHOT_SLIDENAME:
                                    xReader.Read();
                                    screenshot.Slide.Name = xReader.Value;
                                    break;

                                case SCREENSHOT_SLIDEVALUE:
                                    xReader.Read();
                                    screenshot.Slide.Value = xReader.Value;
                                    break;

                                case SCREENSHOT_WINDOW_TITLE:
                                    xReader.Read();
                                    screenshot.WindowTitle = xReader.Value;
                                    break;

                                case SCREENSHOT_LABEL:
                                    xReader.Read();
                                    screenshot.Label = xReader.Value;
                                    break;
                            }
                        }
                    }

                    xReader.Close();

                    if (Settings.VersionManager.IsOldAppVersion(AppVersion, AppCodename))
                    {
                        if (Settings.VersionManager.Versions.Get("Clara", "2.1.8.2") != null)
                        {
                            // We need to associate the screenshot's view ID with the component's view ID
                            // because this special ID value is used for figuring out what screenshot image to display.
                            screenshot.ViewId = screenCollection.GetByComponent(screenshot.Component).ViewId;

                            string windowTitle = "*Screenshot imported from an old version of Auto Screen Capture*";

                            Regex rgxOldSlidename =
                                new Regex(
                                    @"^(?<Date>\d{4}-\d{2}-\d{2}) (?<Time>(?<Hour>\d{2})-(?<Minute>\d{2})-(?<Second>\d{2})-(?<Millisecond>\d{3}))");

                            string hour = rgxOldSlidename.Match(screenshot.Slide.Name).Groups["Hour"].Value;
                            string minute = rgxOldSlidename.Match(screenshot.Slide.Name).Groups["Minute"].Value;
                            string second = rgxOldSlidename.Match(screenshot.Slide.Name).Groups["Second"].Value;
                            string millisecond = rgxOldSlidename.Match(screenshot.Slide.Name).Groups["Millisecond"]
                                .Value;

                            screenshot.Date = rgxOldSlidename.Match(screenshot.Slide.Name).Groups["Date"].Value;
                            screenshot.Time = hour + ":" + minute + ":" + second + "." + millisecond;

                            screenshot.Slide.Name = "{date=" + screenshot.Date + "}{time=" + screenshot.Time + "}";
                            screenshot.Slide.Value = screenshot.Time + " [" + windowTitle + "]";

                            screenshot.WindowTitle = windowTitle;
                        }
                    }

                    if (!string.IsNullOrEmpty(screenshot.Date) &&
                        !string.IsNullOrEmpty(screenshot.Time) &&
                        !string.IsNullOrEmpty(screenshot.Path) &&
                        screenshot.Format != null &&
                        !string.IsNullOrEmpty(screenshot.Slide.Name) &&
                        !string.IsNullOrEmpty(screenshot.Slide.Value))
                    {
                        Add(screenshot, screenCollection, regionCollection);
                    }
                }

                // Write out the upgraded screenshots (if any were found).
                if (Settings.VersionManager.IsOldAppVersion(AppVersion, AppCodename))
                {
                    Save();
                }
            }
        }

        /// <summary>
        /// Saves the screenshots.
        /// </summary>
        public static void Save()
        {
            if (Directory.Exists(FileSystem.ApplicationFolder))
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

                if (File.Exists(FileSystem.ApplicationFolder + FileSystem.ScreenshotsFile))
                {
                    File.Delete(FileSystem.ApplicationFolder + FileSystem.ScreenshotsFile);
                }

                using (XmlWriter xWriter =
                    XmlWriter.Create(FileSystem.ApplicationFolder + FileSystem.ScreenshotsFile, xSettings))
                {
                    xWriter.WriteStartDocument();
                    xWriter.WriteStartElement(XML_FILE_ROOT_NODE);
                    xWriter.WriteAttributeString("app", "version", XML_FILE_ROOT_NODE, Settings.ApplicationVersion);
                    xWriter.WriteAttributeString("app", "codename", XML_FILE_ROOT_NODE, Settings.ApplicationCodename);
                    xWriter.WriteStartElement(XML_FILE_SCREENSHOTS_NODE);

                    foreach (object obj in _screenshotList)
                    {
                        Screenshot screenshot = (Screenshot) obj;

                        xWriter.WriteStartElement(XML_FILE_SCREENSHOT_NODE);
                        xWriter.WriteElementString(SCREENSHOT_VIEWID, screenshot.ViewId.ToString());
                        xWriter.WriteElementString(SCREENSHOT_DATE, screenshot.Date);
                        xWriter.WriteElementString(SCREENSHOT_TIME, screenshot.Time);
                        xWriter.WriteElementString(SCREENSHOT_PATH, screenshot.Path);
                        xWriter.WriteElementString(SCREENSHOT_FORMAT, screenshot.Format.Name);
                        xWriter.WriteElementString(SCREENSHOT_COMPONENT, screenshot.Component.ToString());
                        xWriter.WriteElementString(SCREENSHOT_SLIDENAME, screenshot.Slide.Name);
                        xWriter.WriteElementString(SCREENSHOT_SLIDEVALUE, screenshot.Slide.Value);
                        xWriter.WriteElementString(SCREENSHOT_WINDOW_TITLE, screenshot.WindowTitle);
                        xWriter.WriteElementString(SCREENSHOT_LABEL, screenshot.Label);

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
}