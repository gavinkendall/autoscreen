﻿//-----------------------------------------------------------------------
// <copyright file="ScreenshotCollection.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A collection of screenshots.</summary>
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
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AutoScreenCapture
{
    /// <summary>
    /// A collection class to store and manage Screenshot objects.
    /// </summary>
    public class ScreenshotCollection
    {
        private XmlDocument xDoc;

        private Log _log;
        private Config _config;
        private List<Slide> _slideList;
        private List<string> _slideNameList;
        private List<Screenshot> _screenshotList;
        private List<string> _screenshotPathList;
        private ImageFormatCollection _imageFormatCollection;

        private FileSystem _fileSystem;
        private ScreenCapture _screenCapture;
        private ScreenCollection _screenCollection;

        // Required when multiple threads are writing to the same file.
        private readonly Mutex _mutexWriteFile = new Mutex();

        private const string XML_FILE_INDENT_CHARS = "   ";
        private const string XML_FILE_SCREENSHOT_NODE = "screenshot";
        private const string XML_FILE_SCREENSHOTS_NODE = "screenshots";
        private const string XML_FILE_ROOT_NODE = "autoscreen";

        private const string SCREENSHOT_VIEWID = "viewid";
        private const string SCREENSHOT_DATE = "date";
        private const string SCREENSHOT_TIME = "time";
        private const string SCREENSHOT_PATH = "path";
        private const string SCREENSHOT_FORMAT = "format";
        private const string SCREENSHOT_SLIDENAME = "slidename";
        private const string SCREENSHOT_SLIDEVALUE = "slidevalue";
        private const string SCREENSHOT_WINDOW_TITLE = "windowtitle";
        private const string SCREENSHOT_PROCESS_NAME = "processname";
        private const string SCREENSHOT_LABEL = "label";
        private const string SCREENSHOT_VERSION = "version";
        private const string SCREENSHOT_HASH = "hash";
        private const string SCREENSHOT_ENCRYPTED = "encrypted";
        private const string SCREENSHOT_KEY = "key";

        // This is used for backwards compatibility with a very old version of the application.
        private const string SCREENSHOT_SCREEN = "screen";

        private readonly string SCREENSHOTS_XPATH;
        private readonly string SCREENSHOT_XPATH;

        private string AppCodename { get; set; }
        private string AppVersion { get; set; }

        /// <summary>
        /// The last View ID used by the screenshot collection.
        /// </summary>
        public Guid LastViewId { get; set; }

        /// <summary>
        /// A list of screenshot hash values to be used when adding screenshots so we do not add duplicate screenshots while running and when OptimizeScreenCapture is set.
        /// </summary>
        public List<string> AddedScreenshotHashList { get; set; }

        /// <summary>
        /// A list of screenshot hash values to be used when emailing screenshots so we do not email duplicate screenshots while running and when OptimizeScreenCapture is set.
        /// </summary>
        public List<string> EmailedScreenshotHashList { get; set; }

        private void AddScreenshotToCollection(Screenshot screenshot)
        {
            lock (_screenshotList)
            {
                // Add the screenshot to the screenshot collection only if the path hasn't been encountered before.
                // This is to prevent having too many screenshot objects in the collection when the user clicks on a day in the calendar.
                // It's assumed that the screenshot's filepath can safely be used as a unique key.
                if (screenshot != null && !string.IsNullOrEmpty(screenshot.Path) && !_screenshotPathList.Contains(screenshot.Path))
                {
                    _screenshotPathList.Add(screenshot.Path);

                    _screenshotList.Add(screenshot);

                    // Slides are a little different because a single slide can potentially have many screenshots associated with it
                    // depending on the number of screens and regions that were used when screenshots were taken at the time.
                    // So you're likely going to see multiple screenshots with the same "slide name".
                    if (screenshot.Slide != null && !string.IsNullOrEmpty(screenshot.Slide.Name))
                    {
                        if (!_slideNameList.Contains(screenshot.Slide.Name))
                        {
                            _slideNameList.Add(screenshot.Slide.Name);
                            _slideList.Add(screenshot.Slide);
                        }
                    }

                    LastViewId = screenshot.ViewId;
                }
            }
        }

        private XmlNode GetMinDateFromXMLDocument()
        {
            if (xDoc != null)
            {
                return xDoc.SelectSingleNode(SCREENSHOT_XPATH + "/" + SCREENSHOT_DATE + "[not(. >=../preceding-sibling::" + SCREENSHOT_DATE + ") and not(. >=../following-sibling::" + SCREENSHOT_DATE + ")]");
            }

            return null;
        }

        /// <summary>
        /// A collection of screenshots.
        /// </summary>
        public ScreenshotCollection(ImageFormatCollection imageFormatCollection, ScreenCollection screenCollection, ScreenCapture screenCapture, Config config, FileSystem fileSystem, Log log)
        {
            StringBuilder sbScreenshots = new StringBuilder();
            sbScreenshots.Append("/");
            sbScreenshots.Append(XML_FILE_ROOT_NODE);
            sbScreenshots.Append("/");
            sbScreenshots.Append(XML_FILE_SCREENSHOTS_NODE);

            StringBuilder sbScreenshot = new StringBuilder();
            sbScreenshot.Append("/");
            sbScreenshot.Append(XML_FILE_ROOT_NODE);
            sbScreenshot.Append("/");
            sbScreenshot.Append(XML_FILE_SCREENSHOTS_NODE);
            sbScreenshot.Append("/");
            sbScreenshot.Append(XML_FILE_SCREENSHOT_NODE);

            SCREENSHOTS_XPATH = sbScreenshots.ToString();
            SCREENSHOT_XPATH = sbScreenshot.ToString();

            _config = config;
            _imageFormatCollection = imageFormatCollection;
            _screenCollection = screenCollection;
            _screenCapture = screenCapture;
            _fileSystem = fileSystem;
            _log = log;

            _screenshotList = new List<Screenshot>();
            _log.WriteDebugMessage("Initialized screenshot list");

            _screenshotPathList = new List<string>();
            _log.WriteDebugMessage("Initialized screenshot path list");

            _slideList = new List<Slide>();
            _log.WriteDebugMessage("Initialized slide list");

            _slideNameList = new List<string>();
            _log.WriteDebugMessage("Initialized slide name list");

            _screenCapture.OptimizeScreenCapture = Convert.ToBoolean(config.Settings.User.GetByKey("OptimizeScreenCapture", config.Settings.DefaultSettings.OptimizeScreenCapture).Value);

            AddedScreenshotHashList = new List<string>();
            EmailedScreenshotHashList = new List<string>();
        }

        /// <summary>
        /// Adds a screenshot to the collection.
        /// </summary>
        /// <param name="screenshot">The screenshot to add to the collection.</param>
        public bool Add(Screenshot screenshot)
        {
            try
            {
                bool result = false;

                // We want to keep track of what version of the application this screenshot is associated with.
                screenshot.Version = _config.Settings.ApplicationVersion;

                // If the bitmap image is null then we've loaded the screenshot from the screenshots.xml file
                // and adding it to the collection otherwise we're adding the screenshot to the collection from a screen capture session.
                if (screenshot.Bitmap == null)
                {
                    AddScreenshotToCollection(screenshot);

                    result = true;
                }
                else
                {
                    if (_screenCapture.OptimizeScreenCapture)
                    {
                        // Apparently we can't hash EMF and WMF images so just add the screenshot to the collection and return true.
                        if (screenshot.Format.Name.Equals("EMF") || screenshot.Format.Name.Equals("WMF"))
                        {
                            AddScreenshotToCollection(screenshot);

                            return true;
                        }

                        // If the screenshot already has a hash then just add it to the collection
                        // and return true because it doesn't need to be hashed again. This could happen
                        // when we're attemping to encrypt an optimized screenshot. We don't want to hash it again
                        // while it's being added back into the collection.
                        if (!string.IsNullOrEmpty(screenshot.Hash))
                        {
                            AddScreenshotToCollection(screenshot);

                            return true;
                        }

                        // This is actually really cool ...

                        // We get the last screenshot of this view.
                        Screenshot lastScreenshotOfThisView = GetLastScreenshotOfView(screenshot.ViewId);

                        // We generate an MD5 hash for the screenshot we're currently handling.
                        screenshot.Hash = _screenCapture.GetMD5Hash(screenshot.Bitmap, screenshot.Format);

                        if (string.IsNullOrEmpty(screenshot.Hash))
                        {
                            return false; // (just in case something went wrong with the hashing)
                        }

                        // Then we compare that hash we just generated with the list of hashes we already have and if that list of hashes
                        // has no knowledge of the new hash then we know it's a new image. So we can add the screenshot to the collection.
                        // This ensures we only care about screenshots that are actually different from each other and ignore screenshots
                        // that are exactly the same as what we've already captured. This is the magic of using MD5 hashes.
                        if (lastScreenshotOfThisView == null || string.IsNullOrEmpty(lastScreenshotOfThisView.Hash) ||
                            !AddedScreenshotHashList.Contains(screenshot.Hash))
                        {
                            AddScreenshotToCollection(screenshot);

                            AddedScreenshotHashList.Add(screenshot.Hash);

                            result = true;
                        }
                    }
                    else
                    {
                        // This is for when we don't care about comparing hashes. We simply add the screenshot
                        // to the collection regardless if it's the exact same image as an image we've already captured.
                        AddScreenshotToCollection(screenshot);

                        result = true;
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                _log.WriteExceptionMessage("ScreenshotCollection::Add", ex);

                return false;
            }
        }

        /// <summary>
        /// Removes a screenshot from the screenshot collection and the internal XML document.
        /// </summary>
        /// <param name="screenshot">The screenshot to remove.</param>
        public void Remove(Screenshot screenshot)
        {
            try
            {
                if (xDoc == null || _screenshotList == null)
                {
                    return;
                }

                lock (_screenshotList)
                {
                    if (screenshot != null && !string.IsNullOrEmpty(screenshot.Path) && _screenshotPathList.Contains(screenshot.Path))
                    {
                        // Remove the screenshot's path from the list of screenshot paths.
                        _screenshotPathList.Remove(screenshot.Path);

                        // Remove the screenshot object from the screenshot collection.
                        _screenshotList.Remove(screenshot);

                        // Get the "screenshots" node which will be used as the parent node.
                        XmlNode xScreenshots = xDoc.SelectSingleNode(SCREENSHOTS_XPATH);

                        // Get the individual screenshot to remove from the parent node by using the path provided.
                        // This keeps backwards compatibility with older versions of the application that don't have the "Id" property.
                        XmlNode xScreenshotToRemove = xScreenshots.SelectSingleNode(SCREENSHOT_XPATH + "[path='" + screenshot.Path + "']");

                        // We could be attempting to remove a screenshot that hasn't had its reference saved
                        // to the "screenshots.xml" file yet so that's why this check is necessary.
                        if (xScreenshotToRemove != null)
                        {
                            // Remove the child node (the individual screenshot) from the parent node (the "screenshots" node).
                            xScreenshots.RemoveChild(xScreenshotToRemove);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.WriteExceptionMessage("ScreenshotCollection::Remove", ex);
            }
        }

        /// <summary>
        /// Gets the filter values. We first try to get the filter values from internal memory and, if we can't, we'll get them from the XML document instead.
        /// </summary>
        /// <param name="filterType">The type of filter to use.</param>
        /// <returns>A list of values depending on the filter that was used.</returns>
        public List<string> GetFilterValueList(string filterType)
        {
            List<string> filterValues = new List<string>();

            if (filterType.Equals("Image Format"))
            {
                filterValues = LoadXmlFileAndReturnNodeValues("format", null, "format");
                filterValues.AddRange(_screenshotList.Where(x => x.Format != null && !string.IsNullOrEmpty(x.Format.Name)).Select(x => x.Format.Name));
            }

            if (filterType.Equals("Label"))
            {
                filterValues = LoadXmlFileAndReturnNodeValues("label", null, "label");
                filterValues.AddRange(_screenshotList.Where(x => x.Label != null && !string.IsNullOrEmpty(x.Label)).Select(x => x.Label));
            }

            if (filterType.Equals("Process Name"))
            {
                filterValues = LoadXmlFileAndReturnNodeValues("processname", null, "processname");
                filterValues.AddRange(_screenshotList.Where(x => x.ProcessName != null && !string.IsNullOrEmpty(x.ProcessName)).Select(x => x.ProcessName));
            }

            if (filterType.Equals("Window Title"))
            {
                filterValues = LoadXmlFileAndReturnNodeValues("windowtitle", null, "windowtitle");
                filterValues.AddRange(_screenshotList.Where(x => x.WindowTitle != null && !string.IsNullOrEmpty(x.WindowTitle)).Select(x => x.WindowTitle));
            }

            filterValues = filterValues.Distinct().ToList();

            return filterValues;
        }

        /// <summary>
        /// Gets the dates for the calendar. We first try to get the dates from internal memory and, if we can't, we'll get them from the XML document instead.
        /// </summary>
        /// <param name="filterType">The filter type to use.</param>
        /// <param name="filterValue">The filter value to use.</param>
        /// <returns>A list of dates depending on the filter type and filter value.</returns>
        public List<string> GetDatesByFilter(string filterType, string filterValue)
        {
            List<string> dates = new List<string>();

            if (!string.IsNullOrEmpty(filterValue))
            {
                if (filterType.Equals("Image Format"))
                {
                    dates = LoadXmlFileAndReturnNodeValues("format", filterValue, "date");
                    dates.AddRange(_screenshotList.Where(x => x.Format != null && !string.IsNullOrEmpty(x.Format.Name) && x.Format.Name.Equals(filterValue)).Select(x => x.Date));
                }

                if (filterType.Equals("Label"))
                {
                    dates = LoadXmlFileAndReturnNodeValues("label", filterValue, "date");
                    dates.AddRange(_screenshotList.Where(x => x.Label != null && !string.IsNullOrEmpty(x.Label) && x.Label.Equals(filterValue)).Select(x => x.Date));
                }

                if (filterType.Equals("Process Name"))
                {
                    dates = LoadXmlFileAndReturnNodeValues("processname", filterValue, "date");
                    dates.AddRange(_screenshotList.Where(x => x.ProcessName != null && !string.IsNullOrEmpty(x.ProcessName) && x.ProcessName.Equals(filterValue)).Select(x => x.Date));
                }

                if (filterType.Equals("Window Title"))
                {
                    dates = LoadXmlFileAndReturnNodeValues("windowtitle", filterValue, "date");
                    dates.AddRange(_screenshotList.Where(x => x.WindowTitle != null && !string.IsNullOrEmpty(x.WindowTitle) && x.WindowTitle.Equals(filterValue)).Select(x => x.Date));
                }
            }
            else
            {
                dates = LoadXmlFileAndReturnNodeValues("date", null, "date");
                dates.AddRange(_screenshotList.Select(x => x.Date));
            }

            dates = dates.Distinct().ToList();

            return dates;
        }

        /// <summary>
        /// Gets the slides associated with the collection of screenshots.
        /// </summary>
        /// <param name="filterType">The type of filter to use.</param>
        /// <param name="filterValue">The filter value to use.</param>
        /// <param name="date">The date to use.</param>
        /// <param name="config">The configuration file to use.</param>
        /// <returns>A list of slides based on the filters being used.</returns>
        public List<Slide> GetSlides(string filterType, string filterValue, string date, Config config)
        {
            if (string.IsNullOrEmpty(date))
            {
                return null;
            }

            _log.WriteDebugMessage("Getting slides from screenshot list");

            if (LoadXmlFileAndAddScreenshots(date, config) != 0)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(filterValue))
            {
                if (filterType.Equals("Image Format"))
                {
                    _log.WriteDebugMessage("Getting slides from screenshot list based on Image Format filter");
                    return _screenshotList.Where(x => x.Format != null && !string.IsNullOrEmpty(x.Format.Name) && x.Format.Name.Equals(filterValue) && x.Date.Equals(date)).GroupBy(x => x.Slide.Name).Select(x => x.First().Slide).ToList();
                }

                if (filterType.Equals("Label"))
                {
                    _log.WriteDebugMessage("Getting slides from screenshot list based on Label filter");
                    return _screenshotList.Where(x => x.Label != null && !string.IsNullOrEmpty(x.Label) && x.Label.Equals(filterValue) && x.Date.Equals(date)).GroupBy(x => x.Slide.Name).Select(x => x.First().Slide).ToList();
                }

                if (filterType.Equals("Process Name"))
                {
                    _log.WriteDebugMessage("Getting slides from screenshot list based on Process Name filter");
                    return _screenshotList.Where(x => x.ProcessName != null && !string.IsNullOrEmpty(x.ProcessName) && x.ProcessName.Equals(filterValue) && x.Date.Equals(date)).GroupBy(x => x.Slide.Name).Select(x => x.First().Slide).ToList();
                }

                if (filterType.Equals("Window Title"))
                {
                    _log.WriteDebugMessage("Getting slides from screenshot list based on Window Title filter");
                    return _screenshotList.Where(x => x.WindowTitle != null && !string.IsNullOrEmpty(x.WindowTitle) && x.WindowTitle.Equals(filterValue) && x.Date.Equals(date)).GroupBy(x => x.Slide.Name).Select(x => x.First().Slide).ToList();
                }
            }

            return _slideList.Where(x => x.Date.Equals(date)).GroupBy(x => x.Name).Select(x => x.First()).ToList();
        }

        /// <summary>
        /// Gets a screenshot based on the name of its slide and the view ID.
        /// </summary>
        /// <param name="slideName">The name of the slide associated with the screenshot.</param>
        /// <param name="viewId">The view ID of the screenshot.</param>
        /// <returns></returns>
        public Screenshot GetScreenshot(string slideName, Guid viewId)
        {
            Screenshot foundScreenshot = new Screenshot();

            if (_screenshotList != null)
            {
                lock (_screenshotList)
                {
                    foreach (Screenshot screenshot in _screenshotList.Where(x => x.ViewId.Equals(viewId)))
                    {
                        if (screenshot.Slide != null && !string.IsNullOrEmpty(screenshot.Slide.Name) && screenshot.Slide.Name.Equals(slideName))
                        {
                            foundScreenshot = screenshot;
                            break;
                        }
                    }
                }
            }

            return foundScreenshot;
        }

        /// <summary>
        /// Gets the last screenshot taken.
        /// </summary>
        /// <returns></returns>
        public Screenshot GetLastScreenshot()
        {
            try
            {
                Screenshot foundScreenshot = new Screenshot();

                if (_screenshotList != null)
                {
                    lock (_screenshotList)
                    {
                        foundScreenshot = _screenshotList.Last();
                    }
                }

                return foundScreenshot;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the last screenshot of a particular view based on View ID.
        /// </summary>
        /// <param name="viewId">The View ID to check for.</param>
        /// <returns>A screenshot object based on the View ID or null if a screenshot cannot be found.</returns>
        public Screenshot GetLastScreenshotOfView(Guid viewId)
        {
            try
            {
                Screenshot foundScreenshot = new Screenshot();

                if (_screenshotList != null)
                {
                    if (_screenshotList.Count > 0)
                    {
                        lock (_screenshotList)
                        {
                            foundScreenshot = _screenshotList.Last(x => x.ViewId.Equals(viewId));
                        }
                    }
                }

                return foundScreenshot;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets a list of screenshots based on the date and time.
        /// </summary>
        /// <param name="date">The date for when screenshots were taken.</param>
        /// <param name="time">The time for when screenshots were taken.</param>
        /// <returns>A list of screenshots.</returns>
        public List<Screenshot> GetScreenshots(string date, string time)
        {
            return _screenshotList.Where(x => x.Date.Equals(date) && x.Time.Equals(time)).ToList();
        }

        /// <summary>
        /// Loads screenshot references from the screenshots.xml file into an XmlDocument so it's available in memory.
        /// The old way of loading screenshots also had the application construct each Screenshot object from an XML screenshot node and add it to the collection. This would take a long time to load for a large screenshots.xml file.
        /// The new way (as of version 2.3.0.0) will only load XML screenshot nodes whenever necessary.
        /// </summary>
        public void LoadXmlFile(Config config)
        {
            try
            {
                _mutexWriteFile.WaitOne();

                if (_screenshotList != null && !_fileSystem.FileExists(_fileSystem.ScreenshotsFile))
                {
                    _log.WriteMessage("Could not find \"" + _fileSystem.ScreenshotsFile + "\" so creating default version");

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

                    using (XmlWriter xWriter = XmlWriter.Create(_fileSystem.ScreenshotsFile, xSettings))
                    {
                        xWriter.WriteStartDocument();
                        xWriter.WriteStartElement(XML_FILE_ROOT_NODE);
                        xWriter.WriteAttributeString("app", "version", XML_FILE_ROOT_NODE, config.Settings.ApplicationVersion);
                        xWriter.WriteAttributeString("app", "codename", XML_FILE_ROOT_NODE, config.Settings.ApplicationCodename);
                        xWriter.WriteStartElement(XML_FILE_SCREENSHOTS_NODE);

                        xWriter.WriteEndElement();
                        xWriter.WriteEndElement();
                        xWriter.WriteEndDocument();

                        xWriter.Flush();
                        xWriter.Close();
                    }

                    _log.WriteMessage("Created \"" + _fileSystem.ScreenshotsFile + "\"");
                }

                if (_fileSystem.FileExists(_fileSystem.ScreenshotsFile))
                {
                    _log.WriteDebugMessage("Screenshots file \"" + _fileSystem.ScreenshotsFile + "\" found. Attempting to load XML document");

                    xDoc = new XmlDocument();

                    lock (xDoc)
                    {
                        xDoc.Load(_fileSystem.ScreenshotsFile);

                        _log.WriteDebugMessage("XML document loaded");
                    }
                }
                else
                {
                    _log.WriteDebugMessage("WARNING: Unable to load screenshots");
                }
            }
            catch (Exception ex)
            {
                _log.WriteExceptionMessage("ScreenshotCollection::LoadXmlFile", ex);
            }
            finally
            {
                _mutexWriteFile.ReleaseMutex();
            }
        }

        /// <summary>
        /// Loads a list of node values from the screenshots.xml file based on a node name.
        /// LoadXmlFileAndReturnNodeValues(nodeName: "label", nodeValue: null, nodeReturn: "label") retrieves all label node values of label nodes.
        /// LoadXmlFileAndReturnNodeValues(nodeName: "label", nodeValue: "My label", nodeReturn: "label") retrieves all label node values of label nodes matching "My label".
        /// LoadXmlFileAndReturnNodeValues(nodeName: "label", nodeValue: "My label", nodeReturn: "date") retrieves all date node values of label nodes matching "My label".
        /// </summary>
        /// <param name="nodeName">The name of the node to search for in the XML document. This will become part of an XPath query.</param>
        /// <param name="nodeValue">The value of the node to search for in the XML document. If given a value, it will be used in an XPath query to search for a specific node value once a node is found using the node name. If given null, the node name will be used to retrieve all nodes matching the node name.</param>
        /// <param name="nodeReturn">The value of the node that will be returned based on the node name.</param>
        /// <returns>A list of node values.</returns>
        private List<string> LoadXmlFileAndReturnNodeValues(string nodeName, string nodeValue, string nodeReturn)
        {
            try
            {
                XmlNodeList xNodes = null;

                if (string.IsNullOrEmpty(nodeName))
                {
                    return null;
                }

                _mutexWriteFile.WaitOne();

                if (xDoc != null)
                {
                    lock (xDoc)
                    {
                        if (string.IsNullOrEmpty(nodeValue))
                        {
                            _log.WriteDebugMessage("Loading node values by " + nodeName + " from \"" + _fileSystem.ScreenshotsFile + "\" using XPath query \"" + SCREENSHOT_XPATH + "/" + nodeName + "\"");

                            xNodes = xDoc.SelectNodes(SCREENSHOT_XPATH + "/" + nodeName);
                        }
                        else
                        {
                            _log.WriteDebugMessage("Loading node values by " + nodeName + " from \"" + _fileSystem.ScreenshotsFile + "\" using XPath query \"" + SCREENSHOT_XPATH + "[" + nodeName + "='" + nodeValue + "']\"");

                            xNodes = xDoc.SelectNodes(SCREENSHOT_XPATH + "[" + nodeName + "='" + nodeValue + "']");
                        }

                        if (xNodes != null)
                        {
                            List<string> nodeValues = new List<string>();

                            foreach (XmlNode xNode in xNodes)
                            {
                                XmlNodeReader xReader = new XmlNodeReader(xNode);

                                while (xReader.Read())
                                {
                                    if (xReader.IsStartElement() && !xReader.IsEmptyElement)
                                    {
                                        if (xReader.Name.Equals(nodeReturn))
                                        {
                                            xReader.Read();

                                            if (!string.IsNullOrEmpty(xReader.Value) && !nodeValues.Contains(xReader.Value))
                                            {
                                                nodeValues.Add(xReader.Value);
                                            }
                                        }
                                    }
                                }

                                xReader.Close();
                            }

                            return nodeValues;
                        }
                        else
                        {
                            _log.WriteDebugMessage("WARNING: Unable to load node values from \"" + _fileSystem.ScreenshotsFile + "\"");
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                _log.WriteExceptionMessage("ScreenshotCollection::LoadXmlFileAndReturnNodeValues", ex);

                return null;
            }
            finally
            {
                _mutexWriteFile.ReleaseMutex();
            }
        }

        /// <summary>
        /// Loads the screenshots taken on a particular day from the screenshots.xml file.
        /// </summary>
        /// <param name="date">The date to load screenshots from.</param>
        /// <param name="config"></param>
        public int LoadXmlFileAndAddScreenshots(string date, Config config)
        {
            try
            {
                _mutexWriteFile.WaitOne();

                XmlNodeList xScreenshots = null;

                if (string.IsNullOrEmpty(date))
                {
                    return 1;
                }

                if (xDoc != null)
                {
                    lock (xDoc)
                    {
                        AppVersion = xDoc.SelectSingleNode("/autoscreen").Attributes["app:version"]?.Value;
                        AppCodename = xDoc.SelectSingleNode("/autoscreen").Attributes["app:codename"]?.Value;

                        _log.WriteDebugMessage("Loading screenshots taken on " + date + " from \"" + _fileSystem.ScreenshotsFile + "\" using XPath query \"" + SCREENSHOT_XPATH + "[date='" + date + "']" + "\"");
                        
                        xScreenshots = xDoc.SelectNodes(SCREENSHOT_XPATH + "[date='" + date + "']");

                        if (xScreenshots != null)
                        {
                            _log.WriteDebugMessage("Loading " + xScreenshots.Count + " screenshots taken on " + date);

                            int screenshotsLoadLimit = Convert.ToInt32(config.Settings.Application.GetByKey("ScreenshotsLoadLimit", config.Settings.DefaultSettings.ScreenshotsLoadLimit).Value);

                            if (xScreenshots.Count >= screenshotsLoadLimit)
                            {
                                HelpTip.Message = "Cannot load screenshots taken on " + date + " as the number of screenshots being loaded (" + xScreenshots.Count + ") exceeds the allowed load limit (" + screenshotsLoadLimit + ")";

                                _log.WriteDebugMessage("Cannot load screenshots. The number of screenshots to be loaded (" + xScreenshots.Count + ") exceeded the number allowed set by ScreenshotsLoadLimit (" + screenshotsLoadLimit + ")");

                                return 2;
                            }

                            foreach (XmlNode xScreenshot in xScreenshots)
                            {
                                // This is used for when we want backwards compatibility with older versions
                                // and need to update the screenshot reference data appropriately.
                                Screen screen = null;

                                Screenshot screenshot = new Screenshot
                                {
                                    Slide = new Slide()
                                };

                                XmlNodeReader xReader = new XmlNodeReader(xScreenshot);

                                // Introduced in 2.4 each screenshot has a unique id so we need to consider
                                // if this screenshot is from 2.4 otherwise an id will be given to it.
                                if (xScreenshot.Attributes != null &&
                                    xScreenshot.Attributes["id"] != null &&
                                    !string.IsNullOrEmpty(xScreenshot.Attributes["id"].InnerText))
                                {
                                    screenshot.Id = new Guid(xScreenshot.Attributes["id"].InnerText);
                                }
                                else
                                {
                                    screenshot.Id = Guid.NewGuid();
                                }

                                while (xReader.Read())
                                {
                                    if (xReader.IsStartElement() && !xReader.IsEmptyElement)
                                    {
                                        switch (xReader.Name)
                                        {
                                            case SCREENSHOT_VERSION:
                                                xReader.Read();
                                                screenshot.Version = xReader.Value;
                                                break;

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
                                                screenshot.Format = _imageFormatCollection.GetByName(xReader.Value);
                                                break;

                                            // 2.1 used "Screen" for its definition of each display/monitor whereas 2.2 uses "Component".
                                            case SCREENSHOT_SCREEN:
                                                if (_config.Settings.VersionManager.IsOldAppVersion(_config.Settings, AppCodename, AppVersion) &&
                                                    _config.Settings.VersionManager.Versions.Get(Settings.CODENAME_CLARA, Settings.CODEVERSION_CLARA) != null &&
                                                    string.IsNullOrEmpty(AppCodename) && string.IsNullOrEmpty(AppVersion))
                                                {
                                                    xReader.Read();

                                                    int screenIndex = Convert.ToInt32(xReader.Value);

                                                    // This was "Active Window" in older versions of Auto Screen Capture
                                                    if (screenIndex == 5)
                                                    {
                                                        screen = _screenCollection.GetBySourceAndComponent(source: 0, component: 0);
                                                    }
                                                    else
                                                    {
                                                        // The screen index from older versions should match with the component index.
                                                        screen = _screenCollection.GetBySourceAndComponent(source: 0, component: screenIndex);
                                                    }
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

                                            case SCREENSHOT_PROCESS_NAME:
                                                xReader.Read();
                                                screenshot.ProcessName = xReader.Value;
                                                break;

                                            case SCREENSHOT_LABEL:
                                                xReader.Read();
                                                screenshot.Label = xReader.Value;
                                                break;

                                            case SCREENSHOT_HASH:
                                                xReader.Read();
                                                screenshot.Hash = xReader.Value;
                                                break;

                                            case SCREENSHOT_ENCRYPTED:
                                                xReader.Read();
                                                screenshot.Encrypted = Convert.ToBoolean(xReader.Value);
                                                break;

                                            case SCREENSHOT_KEY:
                                                xReader.Read();
                                                screenshot.Key = xReader.Value;
                                                break;
                                        }
                                    }
                                }

                                xReader.Close();

                                Version v2182 = _config.Settings.VersionManager.Versions.Get(Settings.CODENAME_CLARA, Settings.CODEVERSION_CLARA);
                                Version configVersion = _config.Settings.VersionManager.Versions.Get(AppCodename, AppVersion);

                                // Clara 2.1.8.2
                                if (v2182 != null && configVersion != null && configVersion.VersionNumber == v2182.VersionNumber)
                                {
                                    if (screen != null)
                                    {
                                        screenshot.ViewId = screen.ViewId;

                                        Regex rgxOldSlidename = new Regex(@"^(?<Date>\d{4}-\d{2}-\d{2}) (?<Time>(?<Hour>\d{2})-(?<Minute>\d{2})-(?<Second>\d{2})-(?<Millisecond>\d{3}))");

                                        string hour = rgxOldSlidename.Match(screenshot.Slide.Name).Groups["Hour"].Value;
                                        string minute = rgxOldSlidename.Match(screenshot.Slide.Name).Groups["Minute"].Value;
                                        string second = rgxOldSlidename.Match(screenshot.Slide.Name).Groups["Second"].Value;
                                        string millisecond = rgxOldSlidename.Match(screenshot.Slide.Name).Groups["Millisecond"].Value;

                                        screenshot.Date = rgxOldSlidename.Match(screenshot.Slide.Name).Groups["Date"].Value;
                                        screenshot.Time = hour + ":" + minute + ":" + second + "." + millisecond;

                                        screenshot.WindowTitle = _config.Settings.ApplicationName;

                                        screenshot.Slide.Name = "{date=" + screenshot.Date + "}{time=" + screenshot.Time + "}";
                                        screenshot.Slide.Value = screenshot.Time + " [*Screenshot imported from Auto Screen Capture 2.1.8.2*]";
                                    }
                                }

                                if (string.IsNullOrEmpty(screenshot.Version))
                                {
                                    // Remove all the existing XML child nodes from the old XML screenshot.
                                    xScreenshot.RemoveAll();

                                    // Prepare the new XML child nodes for the old XML screenshot ...
                                    XmlElement xVersion = xDoc.CreateElement(SCREENSHOT_VERSION);
                                    xVersion.InnerText = _config.Settings.ApplicationVersion; // This indicates we've updated the screenshot to the current version.

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

                                    XmlElement xSlidename = xDoc.CreateElement(SCREENSHOT_SLIDENAME);
                                    xSlidename.InnerText = screenshot.Slide.Name;

                                    XmlElement xSlidevalue = xDoc.CreateElement(SCREENSHOT_SLIDEVALUE);
                                    xSlidevalue.InnerText = screenshot.Slide.Value;

                                    XmlElement xWindowTitle = xDoc.CreateElement(SCREENSHOT_WINDOW_TITLE);
                                    xWindowTitle.InnerText = screenshot.WindowTitle;

                                    XmlElement xProcessName = xDoc.CreateElement(SCREENSHOT_PROCESS_NAME);
                                    xProcessName.InnerText = screenshot.ProcessName;

                                    XmlElement xLabel = xDoc.CreateElement(SCREENSHOT_LABEL);
                                    xLabel.InnerText = screenshot.Label;

                                    XmlElement xHash = xDoc.CreateElement(SCREENSHOT_HASH);
                                    xHash.InnerText = screenshot.Hash;

                                    XmlElement xEncrypted = xDoc.CreateElement(SCREENSHOT_ENCRYPTED);
                                    xEncrypted.InnerText = screenshot.Encrypted.ToString();

                                    XmlElement xKey = xDoc.CreateElement(SCREENSHOT_KEY);
                                    xKey.InnerText = screenshot.Key;

                                    // Create the new XML child nodes for the old XML screenshot so that it's now in the format of the new XML screenshot.
                                    xScreenshot.AppendChild(xVersion);
                                    xScreenshot.AppendChild(xViewId);
                                    xScreenshot.AppendChild(xDate);
                                    xScreenshot.AppendChild(xTime);
                                    xScreenshot.AppendChild(xPath);
                                    xScreenshot.AppendChild(xFormat);
                                    xScreenshot.AppendChild(xSlidename);
                                    xScreenshot.AppendChild(xSlidevalue);
                                    xScreenshot.AppendChild(xWindowTitle);
                                    xScreenshot.AppendChild(xProcessName);
                                    xScreenshot.AppendChild(xLabel);
                                    xScreenshot.AppendChild(xHash);
                                    xScreenshot.AppendChild(xEncrypted);
                                    xScreenshot.AppendChild(xKey);
                                }

                                if (!string.IsNullOrEmpty(screenshot.Date) &&
                                        !string.IsNullOrEmpty(screenshot.Time) &&
                                        !string.IsNullOrEmpty(screenshot.Path) &&
                                        screenshot.Format != null &&
                                        !string.IsNullOrEmpty(screenshot.Slide.Name) &&
                                        !string.IsNullOrEmpty(screenshot.Slide.Value) &&
                                        !string.IsNullOrEmpty(screenshot.WindowTitle))
                                {
                                    // Since we're loading existing screenshots from the XML document each screenshot needs to be flagged as being "ReferenceSaved"
                                    // because they were already saved to the file before loading them. The "ReferenceSaved" flag should only be used when we're
                                    // saving new screenshots to avoid saving the entire screenshots collection.
                                    //
                                    // In other words, any screenshot flagged with a "ReferenceSaved" value of "true" will be treated as if it is already in the file.
                                    // A screenshot flagged with a "ReferenceSaved" value of "false" will be treated as a new screenshot that should be saved to the file.
                                    screenshot.ReferenceSaved = true;

                                    Add(screenshot);
                                }
                            }
                        }
                        else
                        {
                            _log.WriteDebugMessage("WARNING: Unable to load screenshots taken on " + date + " from \"" + _fileSystem.ScreenshotsFile + "\"");
                        }
                    }
                }

                return 0;
            }
            catch (Exception ex)
            {
                _log.WriteExceptionMessage("ScreenshotCollection::LoadXmlFileAndAddScreenshots", ex);

                return -1;
            }
            finally
            {
                _mutexWriteFile.ReleaseMutex();
            }
        }

        /// <summary>
        /// Saves the screenshots in the collection to the screenshots.xml file.
        /// </summary>
        public void SaveToXmlFile(Config config)
        {
            try
            {
                _mutexWriteFile.WaitOne();

                bool saveScreenshotRefs = Convert.ToBoolean(config.Settings.User.GetByKey("SaveScreenshotRefs", config.Settings.DefaultSettings.SaveScreenshotRefs).Value);

                if (string.IsNullOrEmpty(_fileSystem.ScreenshotsFile))
                {
                    _fileSystem.ScreenshotsFile = _fileSystem.DefaultScreenshotsFile;

                    if (xDoc == null)
                    {
                        xDoc = new XmlDocument();

                        XmlElement rootElement = xDoc.CreateElement(XML_FILE_ROOT_NODE);
                        XmlElement xScreenshots = xDoc.CreateElement(XML_FILE_SCREENSHOTS_NODE);

                        XmlAttribute attributeVersion = xDoc.CreateAttribute("app", "version", "autoscreen");
                        XmlAttribute attributeCodename = xDoc.CreateAttribute("app", "codename", "autoscreen");

                        attributeVersion.Value = config.Settings.ApplicationVersion;
                        attributeCodename.Value = config.Settings.ApplicationCodename;

                        rootElement.Attributes.Append(attributeVersion);
                        rootElement.Attributes.Append(attributeCodename);

                        rootElement.AppendChild(xScreenshots);

                        xDoc.AppendChild(rootElement);

                        if (saveScreenshotRefs)
                        {
                            xDoc.Save(_fileSystem.ScreenshotsFile);
                        }
                    }

                    if (_fileSystem.FileExists(_fileSystem.ConfigFile))
                    {
                        _fileSystem.AppendToFile(_fileSystem.ConfigFile, "\nScreenshotsFile=" + _fileSystem.ScreenshotsFile);
                    }
                }

                lock (_screenshotList)
                {
                    for (int i = 0; i < _screenshotList.Count; i++)
                    {
                        Screenshot screenshot = _screenshotList[i];

                        // A new screenshot that needs to be written to the file will have its "ReferenceSaved" property set to "false" so make sure to check that.
                        if (!screenshot.ReferenceSaved && xDoc != null && screenshot?.Format != null && !string.IsNullOrEmpty(screenshot.Format.Name))
                        {
                            XmlElement xScreenshot = xDoc.CreateElement(XML_FILE_SCREENSHOT_NODE);

                            // Version 2.4 introduced the Id property for each screenshot.
                            xScreenshot.SetAttribute("id", screenshot.Id.ToString());

                            // Starting from version 2.3.0.0 we're going to save the version number of the application
                            // with every screenshot that gets saved so, when looking at the screenshots XML document,
                            // we can determine what version each screenshot originated from. If there is no <version></version>
                            // tag then assume you're looking at a screenshot XML node from before version 2.3.0.0!
                            //
                            // It should also be noted that we don't want to "upgrade" the version of the entire XML document
                            // because the document can become very large and we don't want to lose the ability to know if we're
                            // reading from an old version of the document. We don't read from the entire document either (starting with 2.3)
                            // since the new way is reading individual screenshot XML nodes.
                            XmlElement xVersion = xDoc.CreateElement(SCREENSHOT_VERSION);
                            xVersion.InnerText = screenshot.Version;

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

                            XmlElement xSlidename = xDoc.CreateElement(SCREENSHOT_SLIDENAME);
                            xSlidename.InnerText = screenshot.Slide.Name;

                            XmlElement xSlidevalue = xDoc.CreateElement(SCREENSHOT_SLIDEVALUE);
                            xSlidevalue.InnerText = screenshot.Slide.Value;

                            XmlElement xWindowTitle = xDoc.CreateElement(SCREENSHOT_WINDOW_TITLE);
                            xWindowTitle.InnerText = screenshot.WindowTitle;

                            XmlElement xProcessName = xDoc.CreateElement(SCREENSHOT_PROCESS_NAME);
                            xProcessName.InnerText = screenshot.ProcessName;

                            XmlElement xLabel = xDoc.CreateElement(SCREENSHOT_LABEL);
                            xLabel.InnerText = screenshot.Label;

                            XmlElement xHash = xDoc.CreateElement(SCREENSHOT_HASH);
                            xHash.InnerText = screenshot.Hash;

                            XmlElement xEncrypted = xDoc.CreateElement(SCREENSHOT_ENCRYPTED);
                            xEncrypted.InnerText = screenshot.Encrypted.ToString();

                            XmlElement xKey = xDoc.CreateElement(SCREENSHOT_KEY);
                            xKey.InnerText = screenshot.Key;

                            xScreenshot.AppendChild(xVersion);
                            xScreenshot.AppendChild(xViedId);
                            xScreenshot.AppendChild(xDate);
                            xScreenshot.AppendChild(xTime);
                            xScreenshot.AppendChild(xPath);
                            xScreenshot.AppendChild(xFormat);
                            xScreenshot.AppendChild(xSlidename);
                            xScreenshot.AppendChild(xSlidevalue);
                            xScreenshot.AppendChild(xWindowTitle);
                            xScreenshot.AppendChild(xProcessName);
                            xScreenshot.AppendChild(xLabel);
                            xScreenshot.AppendChild(xHash);
                            xScreenshot.AppendChild(xEncrypted);
                            xScreenshot.AppendChild(xKey);

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

                                // Make sure to set this property to "true" so we only write out new screenshots (those with "ReferenceSaved" set to "false").
                                screenshot.ReferenceSaved = true;

                                _screenshotList[i] = screenshot;
                            }
                        }
                    }

                    if (xDoc != null)
                    {
                        lock (xDoc)
                        {
                            if (saveScreenshotRefs)
                            {
                                xDoc.Save(_fileSystem.ScreenshotsFile);
                            }
                        }
                    }
                }
            }
            finally
            {
                _mutexWriteFile.ReleaseMutex();
            }
        }

        /// <summary>
        /// Deletes screenshots based on a number of days. If 0 is provided then all screenshots are deleted.
        /// </summary>
        /// <param name="days">The number of days to consider.</param>
        /// <param name="folder">The folder to delete. The folder path may contain macro tags.</param>
        /// <param name="macroParser">The macro tag parser to use.</param>
        /// <param name="macroTagCollection">A collectino of macro tags.</param>
        /// <param name="log">The log to use.</param>
        public void DeleteScreenshots(int days, string folder, MacroParser macroParser, MacroTagCollection macroTagCollection, Log log)
        {
            try
            {
                if (days == 0)
                {
                    lock (_screenshotList)
                    {
                        List<Screenshot> screenshotsToDelete = _screenshotList.Where(x => !string.IsNullOrEmpty(x.Date)).ToList();

                        foreach (Screenshot screenshot in screenshotsToDelete)
                        {
                            _screenshotList.Remove(screenshot);
                            _slideList.Remove(screenshot.Slide);
                            _slideNameList.Remove(screenshot.Slide.Name);

                            if (_fileSystem.FileExists(screenshot.Path))
                            {
                                _fileSystem.DeleteFile(screenshot.Path);
                                _log.WriteDebugMessage($"Deleted file \"{screenshot.Path}\"");
                            }
                            else
                            {
                                _log.WriteDebugMessage($"File \"{screenshot.Path}\" not found");
                            }
                        }

                        foreach (XmlNode node in xDoc.SelectNodes(SCREENSHOT_XPATH))
                        {
                            node.ParentNode.RemoveChild(node);
                        }

                        // Delete everything in the specified folder and delete the folder itself.
                        DeleteFolderByDate(folder, DateTime.Now, macroParser, macroTagCollection, log);
                    }
                }
                else
                {
                    lock (_screenshotList)
                    {
                        // Check what we already have in memory and remove the screenshot object from every list.
                        List<Screenshot> screenshotsToDelete = _screenshotList.Where(x => !string.IsNullOrEmpty(x.Date) && Convert.ToDateTime(x.Date) <= DateTime.Now.Date.AddDays(-days)).ToList();

                        foreach (Screenshot screenshot in screenshotsToDelete)
                        {
                            _screenshotList.Remove(screenshot);
                            _slideList.Remove(screenshot.Slide);
                            _slideNameList.Remove(screenshot.Slide.Name);
                        }
                    }

                    XmlNode minDateNode = GetMinDateFromXMLDocument();

                    if (minDateNode != null)
                    {
                        DateTime dtMin = DateTime.Parse(minDateNode.FirstChild.Value).Date;
                        DateTime dtMax = DateTime.Now.Date.AddDays(-days).Date;

                        for (DateTime date = dtMin; date.Date <= dtMax; date = date.AddDays(1))
                        {
                            XmlNodeList screenshotNodesToDeleteByDate = xDoc.SelectNodes(SCREENSHOT_XPATH + "[" + SCREENSHOT_DATE + "='" + date.ToString(macroParser.DateFormat) + "']");

                            foreach (XmlNode node in screenshotNodesToDeleteByDate)
                            {
                                string path = node.SelectSingleNode("path").FirstChild.Value;

                                if (_fileSystem.FileExists(path))
                                {
                                    _fileSystem.DeleteFile(path);
                                    _log.WriteDebugMessage($"Deleted file \"{path}\"");
                                }
                                else
                                {
                                    _log.WriteDebugMessage($"File \"{path}\" not found");
                                }

                                node.ParentNode.RemoveChild(node);
                            }

                            // Let's see if we can delete the folder by the date of the current iteration if a folder path has been provided.
                            DeleteFolderByDate(folder, date, macroParser, macroTagCollection, log);
                        }
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                _log.WriteErrorMessage("Access to the file system was denied due to insufficient permissions during screenshot deletion operation");
            }
            catch (Exception ex)
            {
                _log.WriteExceptionMessage("ScreenshotCollection::DeleteScreenshots", ex);
            }
        }

        /// <summary>
        /// Deletes a folder based on a given date.
        /// </summary>
        /// <param name="folder">The path of the folder to delete.</param>
        /// <param name="date">The date to consider when deleting the folder and its contents.</param>
        /// <param name="macroParser">The macro parser to use.</param>
        /// <param name="macroTagCollection">The macro tag collection to use.</param>
        /// <param name="log">The log to use.</param>
        private void DeleteFolderByDate(string folder, DateTime date, MacroParser macroParser, MacroTagCollection macroTagCollection, Log log)
        {
            // Delete the specified folder. It's assumed this is the path of the directory to delete.
            // We will delete every directory and every file in "folder" and then delete the specified "folder" based on its path.
            if (!string.IsNullOrEmpty(folder))
            {
                // Parse for the special $date$ variable that's used to figure out what folder to delete.
                // For example "$date[yyyy-MM-dd]$" will be replaced by the value of "date" in the format yyyy-MM-dd.
                string dateVariableRegex = @"(?<Date>\$date\[(?<DateFormat>[dMy\-]+)\]\$)";

                // Thanks to some regex magic we can accept date formats such as yyyy-MM-dd, dd-MM-yy, ddMMyy, yyMMdd, ddMMyyyy.

                if (Regex.IsMatch(folder, dateVariableRegex))
                {
                    foreach (Match match in Regex.Matches(folder, dateVariableRegex))
                    {
                        string dateVariable = match.Groups["Date"].Value;
                        string dateFormat = match.Groups["DateFormat"].Value;

                        // Get the date in the specified date format from the date variable.
                        // For example if the date is December 1, 2021 then all instances of "$date[yyyy-MM-dd]$" will return "2021-12-01".
                        folder = folder.Replace(dateVariable, date.ToString(dateFormat));
                    }
                }

                // It's possible to use macro tags in the folder path for "folder" so make sure we parse them.
                folder = macroParser.ParseTags(folder, macroTagCollection, log);

                // Once the macro tags were parsed (if any were found) then we delete the folder and everything inside it.
                if (_fileSystem.DirectoryExists(folder))
                {
                    _fileSystem.DeleteDirectory(folder);
                    _log.WriteDebugMessage($"Deleted directory \"{folder}\"");
                }
                else
                {
                    _log.WriteDebugMessage($"Directory \"{folder}\" not found");
                }
            }
        }
    }
}