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
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Linq;
    using System.Threading;
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class ScreenshotCollection
    {
        private XmlDocument xDoc;
        private List<Slide> _slideList;
        private List<string> _slideNameList;
        private List<Screenshot> _screenshotList;

        // Required when multiple threads are writing to the same log file.
        private Mutex _mutexWriteFile = new Mutex();

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

        private const string SCREENSHOTS_XPATH = "/" + XML_FILE_ROOT_NODE + "/" + XML_FILE_SCREENSHOTS_NODE;
        private const string SCREENSHOT_XPATH = "/" + XML_FILE_ROOT_NODE + "/" + XML_FILE_SCREENSHOTS_NODE + "/" + XML_FILE_SCREENSHOT_NODE;

        public  string AppCodename { get; set; }
        public  string AppVersion { get; set; }

        public void Add(Screenshot screenshot)
        {
            lock (_screenshotList)
            {
                if (!_screenshotList.Contains(screenshot))
                {
                    _screenshotList.Add(screenshot);
                }

                if (screenshot.Slide != null && !string.IsNullOrEmpty(screenshot.Slide.Name))
                {
                    if (!_slideNameList.Contains(screenshot.Slide.Name))
                    {
                        _slideNameList.Add(screenshot.Slide.Name);
                        _slideList.Add(screenshot.Slide);
                    }
                }
            }
        }

        public  Screenshot Get(int index)
        {
            return (Screenshot)_screenshotList[index];
        }

        public  int Count
        {
            get { return _screenshotList.Count; }
        }

        public  List<string> GetFilterValueList(string filterType)
        {
            if (filterType.Equals("Image Format"))
            {
                return _screenshotList.Where(x => x.Format != null && !string.IsNullOrEmpty(x.Format.Name)).Select(x => x.Format.Name).Distinct().ToList();
            }

            if (filterType.Equals("Label"))
            {
                return _screenshotList.Where(x => x.Label != null && !string.IsNullOrEmpty(x.Label)).Select(x => x.Label).Distinct().ToList();
            }

            if (filterType.Equals("Window Title"))
            {
                return _screenshotList.Where(x => x.WindowTitle != null && !string.IsNullOrEmpty(x.WindowTitle)).Select(x => x.WindowTitle).Distinct().ToList();
            }

            return new List<string>();
        }

        public  List<string> GetDates(string filterType, string filterValue)
        {
            if (!string.IsNullOrEmpty(filterValue))
            {
                if (filterType.Equals("Image Format"))
                {
                    return _screenshotList.Where(x => x.Format != null && !string.IsNullOrEmpty(x.Format.Name) && x.Format.Name.Equals(filterValue)).Select(x => x.Date).ToList();
                }

                if (filterType.Equals("Label"))
                {
                    return _screenshotList.Where(x => x.Label != null && !string.IsNullOrEmpty(x.Label) && x.Label.Equals(filterValue)).Select(x => x.Date).ToList();
                }

                if (filterType.Equals("Window Title"))
                {
                    return _screenshotList.Where(x => x.WindowTitle != null && !string.IsNullOrEmpty(x.WindowTitle) && x.WindowTitle.Equals(filterValue)).Select(x => x.Date).ToList();
                }
            }

            return _screenshotList.Select(x => x.Date).ToList();
        }

        public  List<Slide> GetSlides(string filterType, string filterValue, string date)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            if (!string.IsNullOrEmpty(filterValue))
            {
                if (filterType.Equals("Image Format"))
                {
                    Log.Write("Getting slides from screenshot list based on Image Format filter");
                    return _screenshotList.Where(x => x.Format != null && !string.IsNullOrEmpty(x.Format.Name) && x.Format.Name.Equals(filterValue) && x.Date.Equals(date)).GroupBy(x => x.Slide.Name).Select(x => x.First().Slide).ToList();
                }

                if (filterType.Equals("Label"))
                {
                    Log.Write("Getting slides from screenshot list based on Label filter");
                    return _screenshotList.Where(x => x.Label != null && !string.IsNullOrEmpty(x.Label) && x.Label.Equals(filterValue) && x.Date.Equals(date)).GroupBy(x => x.Slide.Name).Select(x => x.First().Slide).ToList();
                }

                if (filterType.Equals("Window Title"))
                {
                    Log.Write("Getting slides from screenshot list based on Window Title filter");
                    return _screenshotList.Where(x => x.WindowTitle != null && !string.IsNullOrEmpty(x.WindowTitle) && x.WindowTitle.Equals(filterValue) && x.Date.Equals(date)).GroupBy(x => x.Slide.Name).Select(x => x.First().Slide).ToList();
                }
            }

            Log.Write("Getting slides from slide list");

            List<Slide> slides = _slideList.Where(x => x.Date.Equals(date)).GroupBy(x => x.Name).Select(x => x.First()).ToList();

            stopwatch.Stop();

            Log.Write("It took " + stopwatch.ElapsedMilliseconds + " milliseconds to get " + slides.Count + " slides");

            return slides;
        }

        public  Screenshot GetScreenshot(string slideName, Guid viewId)
        {
            Screenshot foundScreenshot = new Screenshot();

            if (_screenshotList != null)
            {
                foreach (Screenshot screenshot in _screenshotList)
                {
                    if (screenshot.Slide != null && !string.IsNullOrEmpty(screenshot.Slide.Name) && screenshot.Slide.Name.Equals(slideName) && screenshot.ViewId.Equals(viewId))
                    {
                        foundScreenshot = screenshot;
                        break;
                    }
                }
            }

            return foundScreenshot;
        }

        /// <summary>
        /// Loads the screenshots.
        /// </summary>
        public  void Load(ImageFormatCollection imageFormatCollection, ScreenCollection screenCollection, RegionCollection regionCollection)
        {
            try
            {
                _mutexWriteFile.WaitOne();

                Log.Write("Loading screenshots from screenshots.xml");

                Stopwatch stopwatch = new Stopwatch();

                stopwatch.Start();

                if (_screenshotList == null)
                {
                    _screenshotList = new List<Screenshot>();
                    Log.Write("Initialized screenshot list");
                }

                if (_slideList == null)
                {
                    _slideList = new List<Slide>();
                    Log.Write("Initialized slide list");
                }

                if (_slideNameList == null)
                {
                    _slideNameList = new List<string>();
                    Log.Write("Initialized slide name list");
                }

                if (_screenshotList != null && Directory.Exists(FileSystem.ApplicationFolder) && !File.Exists(FileSystem.ApplicationFolder + FileSystem.ScreenshotsFile))
                {
                    Log.Write("Could not find \"" + FileSystem.ApplicationFolder + FileSystem.ScreenshotsFile + "\" so creating it");

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

                        xWriter.WriteEndElement();
                        xWriter.WriteEndElement();
                        xWriter.WriteEndDocument();

                        xWriter.Flush();
                        xWriter.Close();
                    }

                    Log.Write("Created \"" + FileSystem.ApplicationFolder + FileSystem.ScreenshotsFile + "\"");
                }

                if (_screenshotList != null && Directory.Exists(FileSystem.ApplicationFolder) && File.Exists(FileSystem.ApplicationFolder + FileSystem.ScreenshotsFile))
                {
                    xDoc = new XmlDocument();

                    lock (xDoc)
                    {
                        xDoc.Load(FileSystem.ApplicationFolder + FileSystem.ScreenshotsFile);

                        AppVersion = xDoc.SelectSingleNode("/autoscreen").Attributes["app:version"]?.Value;
                        AppCodename = xDoc.SelectSingleNode("/autoscreen").Attributes["app:codename"]?.Value;

                        Log.Write("Getting screenshots from screenshots.xml using XPath query \"" + SCREENSHOT_XPATH + "\"");

                        XmlNodeList xScreeshots = xDoc.SelectNodes(SCREENSHOT_XPATH);

                        if (xScreeshots != null)
                        {
                            Log.Write("Loading each screenshot from screenshots.xml");

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
                                                screenshot.Slide.Date = xReader.Value;
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

                                        Regex rgxOldSlidename = new Regex(@"^(?<Date>\d{4}-\d{2}-\d{2}) (?<Time>(?<Hour>\d{2})-(?<Minute>\d{2})-(?<Second>\d{2})-(?<Millisecond>\d{3}))");

                                        string hour = rgxOldSlidename.Match(screenshot.Slide.Name).Groups["Hour"].Value;
                                        string minute = rgxOldSlidename.Match(screenshot.Slide.Name).Groups["Minute"].Value;
                                        string second = rgxOldSlidename.Match(screenshot.Slide.Name).Groups["Second"].Value;
                                        string millisecond = rgxOldSlidename.Match(screenshot.Slide.Name).Groups["Millisecond"].Value;

                                        screenshot.Date = rgxOldSlidename.Match(screenshot.Slide.Name).Groups["Date"].Value;
                                        screenshot.Time = hour + ":" + minute + ":" + second + "." + millisecond;

                                        screenshot.Slide.Name = "{date=" + screenshot.Date + "}{time=" + screenshot.Time + "}";
                                        screenshot.Slide.Value = screenshot.Time + " [" + windowTitle + "]";

                                        screenshot.WindowTitle = windowTitle;

                                        // Remove all the existing XML child nodes from the old XML screenshot.
                                        xScreenshot.RemoveAll();

                                        // Prepare the new XML child nodes for the old XML screenshot ...

                                        XmlElement xViewId = xDoc.CreateElement(SCREENSHOT_VIEWID);
                                        xViewId.InnerText = screenshot.ViewId.ToString();

                                        XmlElement xDate = xDoc.CreateElement(SCREENSHOT_DATE);
                                        xDate.InnerText = screenshot.Date;

                                        XmlElement xTime = xDoc.CreateElement(SCREENSHOT_TIME);
                                        xTime.InnerText = screenshot.Time;

                                        XmlElement xPath = xDoc.CreateElement(SCREENSHOT_PATH);
                                        xPath.InnerText = screenshot.Path;

                                        XmlElement xFormat = xDoc.CreateElement(SCREENSHOT_FORMAT);
                                        xFormat.InnerText = screenshot.Format.Name;

                                        XmlElement xComponent = xDoc.CreateElement(SCREENSHOT_COMPONENT);
                                        xComponent.InnerText = screenshot.Component.ToString();

                                        XmlElement xSlidename = xDoc.CreateElement(SCREENSHOT_SLIDENAME);
                                        xSlidename.InnerText = screenshot.Slide.Name;

                                        XmlElement xSlidevalue = xDoc.CreateElement(SCREENSHOT_SLIDEVALUE);
                                        xSlidevalue.InnerText = screenshot.Slide.Value;

                                        XmlElement xWindowTitle = xDoc.CreateElement(SCREENSHOT_WINDOW_TITLE);
                                        xWindowTitle.InnerText = screenshot.WindowTitle;

                                        XmlElement xLabel = xDoc.CreateElement(SCREENSHOT_LABEL);
                                        xLabel.InnerText = screenshot.Label;

                                        // Create the new XML child nodes for the old XML screenshot so that it's now in the format of the new XML screenshot.
                                        xScreenshot.AppendChild(xViewId);
                                        xScreenshot.AppendChild(xDate);
                                        xScreenshot.AppendChild(xTime);
                                        xScreenshot.AppendChild(xPath);
                                        xScreenshot.AppendChild(xFormat);
                                        xScreenshot.AppendChild(xComponent);
                                        xScreenshot.AppendChild(xSlidename);
                                        xScreenshot.AppendChild(xSlidevalue);
                                        xScreenshot.AppendChild(xWindowTitle);
                                        xScreenshot.AppendChild(xLabel);
                                    }
                                }

                                if (!string.IsNullOrEmpty(screenshot.Date) &&
                                    !string.IsNullOrEmpty(screenshot.Time) &&
                                    !string.IsNullOrEmpty(screenshot.Path) &&
                                    screenshot.Format != null &&
                                    !string.IsNullOrEmpty(screenshot.Slide.Name) &&
                                    !string.IsNullOrEmpty(screenshot.Slide.Value) &&
                                    !string.IsNullOrEmpty(screenshot.WindowTitle))
                                {
                                    screenshot.Saved = true;
                                    Add(screenshot);
                                }
                            }
                        }
                        else
                        {
                            Log.Write("WARNING: Unable to load screenshots from screenshots.xml");
                        }

                        if (Settings.VersionManager.IsOldAppVersion(AppVersion, AppCodename))
                        {
                            Log.Write("Old application version discovered when loading screenshots.xml");

                            // We'll have to create the app:version and app:codename attributes for screenshots.xml if we're upgrading from "Clara".
                            if (Settings.VersionManager.Versions.Get("Clara", "2.1.8.2") != null)
                            {
                                XmlAttribute attributeVersion = xDoc.CreateAttribute("app", "version", "autoscreen");
                                XmlAttribute attributeCodename = xDoc.CreateAttribute("app", "codename", "autoscreen");

                                attributeVersion.Value = Settings.ApplicationVersion;
                                attributeCodename.Value = Settings.ApplicationCodename;

                                xDoc.SelectSingleNode("/" + XML_FILE_ROOT_NODE).Attributes.Append(attributeVersion);
                                xDoc.SelectSingleNode("/" + XML_FILE_ROOT_NODE).Attributes.Append(attributeCodename);
                            }

                            // Apply the latest version and codename if the version of Auto Screen Capture is older than this version.
                            xDoc.SelectSingleNode("/" + XML_FILE_ROOT_NODE).Attributes["app:version"].Value = Settings.ApplicationVersion;
                            xDoc.SelectSingleNode("/" + XML_FILE_ROOT_NODE).Attributes["app:codename"].Value = Settings.ApplicationCodename;

                            xDoc.Save(FileSystem.ApplicationFolder + FileSystem.ScreenshotsFile);

                            Log.Write("Upgraded screenshots.xml");
                        }
                    }
                }

                stopwatch.Stop();

                Log.Write("It took " + stopwatch.ElapsedMilliseconds + " milliseconds to load " + _screenshotList.Count + " screenshots");
            }
            catch (Exception ex)
            {
                Log.Write("ScreenshotCollection::Load", ex);
            }
            finally
            {
                _mutexWriteFile.ReleaseMutex();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keepScreenshotsForDays"></param>
        public void Save(int keepScreenshotsForDays)
        {
            try
            {
                _mutexWriteFile.WaitOne();

                Stopwatch stopwatch = new Stopwatch();

                stopwatch.Start();

                Log.Write("Saving screenshots to screenshots.xml");

                lock (_screenshotList)
                {
                    if (_screenshotList != null && _screenshotList.Count > 0 && keepScreenshotsForDays > 0)
                    {
                        List<Screenshot> screenshotsToDelete = _screenshotList.Where(x => !string.IsNullOrEmpty(x.Date) && Convert.ToDateTime(x.Date) <= DateTime.Now.Date.AddDays(-keepScreenshotsForDays)).ToList();

                        if (screenshotsToDelete != null && screenshotsToDelete.Count > 0)
                        {
                            foreach (Screenshot screenshot in screenshotsToDelete)
                            {
                                XmlNodeList nodesToDelete = xDoc.SelectNodes(SCREENSHOT_XPATH + "[" + SCREENSHOT_DATE + "='" + screenshot.Date + "']");

                                foreach (XmlNode node in nodesToDelete)
                                {
                                    node.ParentNode.RemoveChild(node);
                                }

                                if (File.Exists(screenshot.Path))
                                {
                                    File.Delete(screenshot.Path);
                                }

                                _screenshotList.Remove(screenshot);
                            }

                            lock (xDoc)
                            {
                                xDoc.Save(FileSystem.ApplicationFolder + FileSystem.ScreenshotsFile);
                            }
                        }
                    }

                    for (int i = 0; i < _screenshotList.Count; i++)
                    {
                        Screenshot screenshot = _screenshotList[i];

                        if (!screenshot.Saved && xDoc != null && screenshot?.Format != null && !string.IsNullOrEmpty(screenshot.Format.Name))
                        {
                            XmlElement xScreenshot = xDoc.CreateElement(XML_FILE_SCREENSHOT_NODE);

                            XmlElement xViedId = xDoc.CreateElement(SCREENSHOT_VIEWID);
                            xViedId.InnerText = screenshot.ViewId.ToString();

                            XmlElement xDate = xDoc.CreateElement(SCREENSHOT_DATE);
                            xDate.InnerText = screenshot.Date;

                            XmlElement xTime = xDoc.CreateElement(SCREENSHOT_TIME);
                            xTime.InnerText = screenshot.Time;

                            XmlElement xPath = xDoc.CreateElement(SCREENSHOT_PATH);
                            xPath.InnerText = screenshot.Path;

                            XmlElement xFormat = xDoc.CreateElement(SCREENSHOT_FORMAT);
                            xFormat.InnerText = screenshot.Format.Name;

                            XmlElement xComponent = xDoc.CreateElement(SCREENSHOT_COMPONENT);
                            xComponent.InnerText = screenshot.Component.ToString();

                            XmlElement xSlidename = xDoc.CreateElement(SCREENSHOT_SLIDENAME);
                            xSlidename.InnerText = screenshot.Slide.Name;

                            XmlElement xSlidevalue = xDoc.CreateElement(SCREENSHOT_SLIDEVALUE);
                            xSlidevalue.InnerText = screenshot.Slide.Value;

                            XmlElement xWindowTitle = xDoc.CreateElement(SCREENSHOT_WINDOW_TITLE);
                            xWindowTitle.InnerText = screenshot.WindowTitle;

                            XmlElement xLabel = xDoc.CreateElement(SCREENSHOT_LABEL);
                            xLabel.InnerText = screenshot.Label;

                            xScreenshot.AppendChild(xViedId);
                            xScreenshot.AppendChild(xDate);
                            xScreenshot.AppendChild(xTime);
                            xScreenshot.AppendChild(xPath);
                            xScreenshot.AppendChild(xFormat);
                            xScreenshot.AppendChild(xComponent);
                            xScreenshot.AppendChild(xSlidename);
                            xScreenshot.AppendChild(xSlidevalue);
                            xScreenshot.AppendChild(xWindowTitle);
                            xScreenshot.AppendChild(xLabel);

                            XmlNode xScreenshots = xDoc.SelectSingleNode(SCREENSHOTS_XPATH);

                            if (xScreenshots != null)
                            {
                                if (xScreenshots.HasChildNodes)
                                {
                                    xScreenshots.InsertAfter(xScreenshot, xScreenshots.LastChild);
                                }
                                else
                                {
                                    xScreenshots.AppendChild(xScreenshot);
                                }

                                lock (xDoc)
                                {
                                    xDoc.Save(FileSystem.ApplicationFolder + FileSystem.ScreenshotsFile);
                                }

                                screenshot.Saved = true;

                                _screenshotList[i] = screenshot;
                            }
                        }
                    }
                }

                stopwatch.Stop();

                Log.Write("It took " + stopwatch.ElapsedMilliseconds + " milliseconds to save screenshots to screenshots.xml");
            }
            finally
            {
                _mutexWriteFile.ReleaseMutex();
            }
        }
    }
}