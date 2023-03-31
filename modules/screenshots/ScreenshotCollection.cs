//-----------------------------------------------------------------------
// <copyright file="ScreenshotCollection.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2023 Gavin Kendall
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

// A library written by Jakob Krarup (https://www.codeproject.com/Articles/374386/Simple-image-comparison-in-NET)
using XnaFan.ImageComparison;

namespace AutoScreenCapture
{
    /// <summary>
    /// A collection class to store and manage Screenshot objects.
    /// </summary>
    public class ScreenshotCollection
    {
        private XmlDocument xDoc;

        private readonly Log _log;
        private readonly Config _config;
        private readonly Security _security;
        private readonly List<Slide> _slideList;
        private readonly List<string> _slideNameList;
        private readonly List<Screenshot> _screenshotList;
        private readonly List<Guid> _screenshotIdList;
        private readonly ImageFormatCollection _imageFormatCollection;

        private readonly FileSystem _fileSystem;
        private readonly ScreenCapture _screenCapture;
        private readonly ScreenCollection _screenCollection;

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
        private const string SCREENSHOT_IMAGE_DIFF = "image_diff";
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
        /// A list of screenshot filepaths to be used when emailing screenshots so we do not email duplicate screenshots.
        /// </summary>
        public List<string> EmailedScreenshotFilePathList { get; set; }

        /// <summary>
        /// The number of screenshots in the screenshot collection.
        /// </summary>
        public int Count { get { return _screenshotList.Count; } }
                

        /// <summary>
        /// Delete all the screenshots, associated slides, and files based on the given list of screenshots but not the files that are still in the failed uploads dictionary.
        /// </summary>
        /// <param name="screenshotsToDelete">A list of screenshots to delete.</param>
        /// <param name="failedUploads">A dictionary of failed uploads.</param>
        private void DeleteScreenshots(List<Screenshot> screenshotsToDelete, Dictionary<Screenshot, string> failedUploads)
        {
            lock (_screenshotList)
            {
                foreach (Screenshot screenshot in screenshotsToDelete)
                {
                    // Delete the screenshot and slide.
                    _screenshotList.Remove(screenshot);
                    _slideList.Remove(screenshot.Slide);
                    _slideNameList.Remove(screenshot.Slide.Name);

                    // Do not delete the screenshot if it exists in the dictionary of failed uploads.
                    Screenshot[] screenshotsThatFailedToUpload = new Screenshot[failedUploads.Count];
                    failedUploads.Keys.CopyTo(screenshotsThatFailedToUpload, 0);
                    
                    if (screenshotsThatFailedToUpload.Contains(screenshot))
                    {
                        continue;
                    }

                    // Delete the actual image file containing the screenshot.
                    if (_fileSystem.FileExists(screenshot.FilePath))
                    {
                        _fileSystem.DeleteFile(screenshot.FilePath);
                        _log.WriteDebugMessage($"Deleted file \"{screenshot.FilePath}\"");
                    }
                    else
                    {
                        _log.WriteDebugMessage($"File \"{screenshot.FilePath}\" not found");
                    }
                }
            }
        }

        /// <summary>
        /// Deletes screenshot XML nodes from the XML document.
        /// </summary>
        /// <param name="screenshotNodesToDelete">A list of screenshot XML nodes to delete.</param>
        private void DeleteScreenshotNodes(XmlNodeList screenshotNodesToDelete)
        {
            foreach (XmlNode node in screenshotNodesToDelete)
            {
                node.ParentNode.RemoveChild(node);
            }
        }

        /// <summary>
        /// A collection of screenshots.
        /// </summary>
        public ScreenshotCollection(ImageFormatCollection imageFormatCollection, ScreenCollection screenCollection, ScreenCapture screenCapture, Config config, FileSystem fileSystem, Log log, Security security)
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
            _security = security;

            _screenshotList = new List<Screenshot>();
            _log.WriteDebugMessage("Initialized screenshot list");

            _screenshotIdList = new List<Guid>();
            _log.WriteDebugMessage("Initialized screenshot ID list");

            _slideList = new List<Slide>();
            _log.WriteDebugMessage("Initialized slide list");

            _slideNameList = new List<string>();
            _log.WriteDebugMessage("Initialized slide name list");

            _screenCapture.OptimizeScreenCapture = false;

            Setting optimizeScreenCaptureSetting = config.Settings.User.GetByKey("OptimizeScreenCapture");

            if (optimizeScreenCaptureSetting != null)
            {
                _screenCapture.OptimizeScreenCapture = Convert.ToBoolean(config.Settings.User.GetByKey("OptimizeScreenCapture").Value);
            }

            EmailedScreenshotFilePathList = new List<string>();
        }

        /// <summary>
        /// Gets the XML node representing the minimum date when screenshots were taken.
        /// </summary>
        /// <returns></returns>
        public XmlNode GetMinDateFromXMLDocument()
        {
            if (xDoc != null)
            {
                return xDoc.SelectSingleNode(SCREENSHOT_XPATH + "/" + SCREENSHOT_DATE + "[not(. >=../preceding-sibling::" + SCREENSHOT_DATE + ") and not(. >=../following-sibling::" + SCREENSHOT_DATE + ")]");
            }

            return null;
        }

        /// <summary>
        /// Adds the given screenshot to the collection.
        /// </summary>
        /// <param name="screenshot">The screenshot to add to the collection.</param>
        public void Add(Screenshot screenshot)
        {
            lock (_screenshotList)
            {
                // Add the screenshot to the screenshot collection.
                if (screenshot != null && !string.IsNullOrEmpty(screenshot.FilePath))
                {
                    if (!_screenshotList.Contains(screenshot) && !_screenshotIdList.Contains(screenshot.Id))
                    {
                        _screenshotList.Add(screenshot);

                        _screenshotIdList.Add(screenshot.Id);

                        // Slides are a little different because a single slide can potentially have many screenshots associated with it
                        // depending on the number of screens and regions that were used when screenshots were taken at the time.
                        // So you're likely going to see multiple screenshots with the same "slide name".
                        if (screenshot.Slide != null && !string.IsNullOrEmpty(screenshot.Slide.Name))
                        {
                            if (!_slideNameList.Contains(screenshot.Slide.Name))
                            {
                                _slideNameList.Add(screenshot.Slide.Name);

                                // Make sure that "Slide" gets the same data as what "screenshot" has
                                // so we can display the appropriate information in the screenshots list
                                // based on the Filter selection.
                                screenshot.Slide.ImageFormat = screenshot.Format.Name;
                                screenshot.Slide.Label = screenshot.Label;
                                screenshot.Slide.ProcessName = screenshot.ProcessName;
                                screenshot.Slide.WindowTitle = screenshot.WindowTitle;

                                _slideList.Add(screenshot.Slide);
                            }
                        }

                        LastViewId = screenshot.ViewId;
                    }
                }
            }
        }

        /// <summary>
        /// Processes a screenshot.
        /// </summary>
        /// <param name="screenshot">The screenshot to process.</param>
        /// <param name="imageDiffTolerance">The image difference tolerance percentage.</param>
        /// <returns>True if processing the screenshot was successful. False if processing the screenshot was unsuccessful.</returns>
        public bool Process(Screenshot screenshot, int imageDiffTolerance)
        {
            try
            {
                // We want to keep track of what version of the application this screenshot is associated with.
                screenshot.Version = _config.Settings.ApplicationVersion;

                // Check the image difference between the provided screenshot and the previous screenshot of the same view if we're using the Optimize Screen Capture feature.
                if (_screenCapture.OptimizeScreenCapture)
                {
                    Screenshot lastScreenshotOfThisView = GetLastScreenshotOfView(screenshot.ViewId);

                    // Add the screenshot to the collection if no previous screenshot of this view was found.
                    // This is likely because we're taking the first screenshot of this view.
                    if (lastScreenshotOfThisView == null)
                    {
                        return true;
                    }

                    // Prepare the initial image diff value as -1.
                    float imageDiff = -1;

                    // If the previous image is encrypted then we need to decrypt it before doing the image comparison with it.
                    // Then encrypt it again.
                    if (lastScreenshotOfThisView.Encrypted)
                    {
                        // Decrypt the previous screenshot.
                        Screenshot decryptedScreenshot = _security.DecryptScreenshot(lastScreenshotOfThisView);

                        if (decryptedScreenshot != null)
                        {
                            // Get the percentage of difference between the images of the current screenshot the last screenshot of this view.
                            imageDiff = ImageTool.GetPercentageDifference(screenshot.Bitmap, decryptedScreenshot.FilePath);
                        }

                        // Encrypt the screenshot again since we've checked the image difference and have the imageDiff value.
                        _security.EncryptScreenshot(lastScreenshotOfThisView);
                    }
                    else
                    {
                        // Get the percentage of difference between the images of the current screenshot the last screenshot of this view.
                        imageDiff = ImageTool.GetPercentageDifference(screenshot.Bitmap, lastScreenshotOfThisView.FilePath);
                    }

                    // Add the new screenshot to the collection if there was an error with the image comparison
                    // since we'll treat it as if we're not using Optimize Screen Capture.
                    if (imageDiff == -1)
                    {
                        return true;
                    }

                    // Multiply imageDiff by 100. For example, if imageDiff is 0.12109375 then screenshot.DiffPercentageWithPreviousImage will be 12.
                    screenshot.DiffPercentageWithPreviousImage = (int)(Math.Ceiling(imageDiff * 100));

                    _log.WriteDebugMessage("Screenshot's image is " + screenshot.DiffPercentageWithPreviousImage + "% different compared to the previous screenshot's image (your acceptable percentage is " + imageDiffTolerance + "%)");

                    // Check if the image difference is greater than or equal to the user's selected "image diff tolerance".
                    if (screenshot.DiffPercentageWithPreviousImage >= imageDiffTolerance)
                    {
                        return true;
                    }
                }
                else
                {
                    // This is for when we don't care about OptimizeScreenCapture. We simply return true
                    // regardless if it's the exact same image as the previous image.
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _log.WriteExceptionMessage("ScreenshotCollection::Process", ex);

                return false;
            }
        }

        /// <summary>
        /// Removes the given screenshot from the screenshot collection.
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
                    if (screenshot != null && !string.IsNullOrEmpty(screenshot.FilePath))
                    {
                        // Remove the screenshot object from the screenshot collection.
                        _screenshotList.Remove(screenshot);

                        // Get the "screenshots" node which will be used as the parent node.
                        XmlNode xScreenshots = xDoc.SelectSingleNode(SCREENSHOTS_XPATH);

                        // Get the individual screenshot to remove from the parent node by using the path provided.
                        // This keeps backwards compatibility with older versions of the application that don't have the "Id" property.
                        XmlNode xScreenshotToRemove = xScreenshots.SelectSingleNode(SCREENSHOT_XPATH + "[path='" + screenshot.FilePath + "']");

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

                    if (dates != null)
                    {
                        dates.AddRange(_screenshotList.Where(x => x.Format != null && !string.IsNullOrEmpty(x.Format.Name) && x.Format.Name.Equals(filterValue)).Select(x => x.Date));
                    }
                }

                if (filterType.Equals("Label"))
                {
                    dates = LoadXmlFileAndReturnNodeValues("label", filterValue, "date");

                    if (dates != null)
                    {
                        dates.AddRange(_screenshotList.Where(x => x.Label != null && !string.IsNullOrEmpty(x.Label) && x.Label.Equals(filterValue)).Select(x => x.Date));
                    }
                }

                if (filterType.Equals("Process Name"))
                {
                    dates = LoadXmlFileAndReturnNodeValues("processname", filterValue, "date");

                    if (dates != null)
                    {
                        dates.AddRange(_screenshotList.Where(x => x.ProcessName != null && !string.IsNullOrEmpty(x.ProcessName) && x.ProcessName.Equals(filterValue)).Select(x => x.Date));
                    }
                }

                if (filterType.Equals("Window Title"))
                {
                    dates = LoadXmlFileAndReturnNodeValues("windowtitle", filterValue, "date");

                    if (dates != null)
                    {
                        dates.AddRange(_screenshotList.Where(x => x.WindowTitle != null && !string.IsNullOrEmpty(x.WindowTitle) && x.WindowTitle.Equals(filterValue)).Select(x => x.Date));
                    }
                }
            }
            else
            {
                dates = LoadXmlFileAndReturnNodeValues("date", null, "date");

                if (dates != null)
                {
                    dates.AddRange(_screenshotList.Select(x => x.Date));
                }
            }

            if (dates != null)
            {
                dates = dates.Distinct().ToList();
            }

            return dates;
        }

        /// <summary>
        /// Gets the slides associated with the collection of screenshots.
        /// </summary>
        /// <param name="filterType">The type of filter to use.</param>
        /// <param name="filterValue">The filter value to use.</param>
        /// <param name="date">The date to use.</param>
        /// <param name="config">The configuration file to use.</param>
        /// <param name="slideValueToDisplay">The slide value to display in the list of screenshots.</param>
        /// <returns>A list of slides based on the filters being used.</returns>
        public List<Slide> GetSlides(string filterType, string filterValue, string date, Config config, string slideValueToDisplay)
        {
            List<Slide> localSlideList = null;

            if (string.IsNullOrEmpty(date))
            {
                localSlideList = null;
            }

            _log.WriteDebugMessage("Getting slides from screenshot list");

            int screenshotsLoadLimit = Convert.ToInt32(config.Settings.Application.GetByKey("ScreenshotsLoadLimit").Value);

            LoadXmlFileAndAddScreenshots(date, screenshotsLoadLimit, out int nodeLoadCount, out int errorCode);

            if (errorCode == 1)
            {
                localSlideList = null;
            }

            if (errorCode == 2)
            {
                _log.WriteDebugMessage("Cannot load screenshots. The number of screenshots to be loaded exceeded the number allowed set by ScreenshotsLoadLimit (" + screenshotsLoadLimit + ")");

                localSlideList = null;
            }

            if (string.IsNullOrEmpty(filterType) || string.IsNullOrEmpty(filterValue))
            {
                localSlideList = _slideList.Where(x => x.Date.Equals(date)).GroupBy(x => x.Name).Select(x => x.First()).ToList();
            }
            else
            {
                if (filterType.Equals("Image Format"))
                {
                    _log.WriteDebugMessage("Getting slides from screenshot list based on Image Format filter");

                    localSlideList = _screenshotList.Where(x => x.Format != null && !string.IsNullOrEmpty(x.Format.Name) && x.Format.Name.Equals(filterValue) && x.Date.Equals(date)).GroupBy(x => x.Slide.Name).Select(x => x.First().Slide).ToList();
                }

                if (filterType.Equals("Label"))
                {
                    _log.WriteDebugMessage("Getting slides from screenshot list based on Label filter");

                    localSlideList = _screenshotList.Where(x => x.Label != null && !string.IsNullOrEmpty(x.Label) && x.Label.Equals(filterValue) && x.Date.Equals(date)).GroupBy(x => x.Slide.Name).Select(x => x.First().Slide).ToList();
                }

                if (filterType.Equals("Process Name"))
                {
                    _log.WriteDebugMessage("Getting slides from screenshot list based on Process Name filter");

                    localSlideList = _screenshotList.Where(x => x.ProcessName != null && !string.IsNullOrEmpty(x.ProcessName) && x.ProcessName.Equals(filterValue) && x.Date.Equals(date)).GroupBy(x => x.Slide.Name).Select(x => x.First().Slide).ToList();
                }

                if (filterType.Equals("Window Title"))
                {
                    _log.WriteDebugMessage("Getting slides from screenshot list based on Window Title filter");

                    localSlideList = _screenshotList.Where(x => x.WindowTitle != null && !string.IsNullOrEmpty(x.WindowTitle) && x.WindowTitle.Equals(filterValue) && x.Date.Equals(date)).GroupBy(x => x.Slide.Name).Select(x => x.First().Slide).ToList();
                }
            }

            foreach(Slide slide in localSlideList)
            {
                string slideValue = string.Empty;

                if (string.IsNullOrEmpty(slideValueToDisplay))
                {
                    slideValue = slide.WindowTitle + " (" + slide.ProcessName + ") " + slide.ImageFormat;
                }
                else
                {
                    if (slideValueToDisplay.Equals("Image Format") && !string.IsNullOrEmpty(slide.ImageFormat))
                    {
                        slideValue = slide.ImageFormat;
                    }

                    if (slideValueToDisplay.Equals("Label") && !string.IsNullOrEmpty(slide.Label))
                    {
                        slideValue = slide.Label;
                    }

                    if (slideValueToDisplay.Equals("Process Name") && !string.IsNullOrEmpty(slide.ProcessName))
                    {
                        slideValue = slide.ProcessName;
                    }

                    if (slideValueToDisplay.Equals("Window Title") && !string.IsNullOrEmpty(slide.WindowTitle))
                    {
                        slideValue = slide.WindowTitle;
                    }
                }

                slide.Value = Regex.Replace(slide.Value, @"\[.*\]", "[" + slideValue + "]");
            }

            return localSlideList;
        }

        /// <summary>
        /// Gets a screenshot based on its id.
        /// </summary>
        /// <param name="id">The Id of the screenshot to get.</param>
        /// <returns>The screenshot that matches with the given Id.</returns>
        public Screenshot GetScreenshot(Guid id)
        {
            return _screenshotList.Where(x => x.Id.Equals(id)).FirstOrDefault();
        }

        /// <summary>
        /// Gets a screenshot based on the name of its slide and the view ID.
        /// </summary>
        /// <param name="slideName">The name of the slide associated with the screenshot.</param>
        /// <param name="viewId">The view ID of the screenshot.</param>
        /// <returns></returns>
        public Screenshot GetScreenshot(string slideName, Guid viewId)
        {
            try
            {
                if (_screenshotList != null)
                {
                    lock (_screenshotList)
                    {
                        Screenshot screenshot = (Screenshot)_screenshotList.Where(x => x.ViewId.Equals(viewId) && x.Slide.Name.Equals(slideName)).FirstOrDefault();

                        return screenshot;
                    }
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the last screenshot taken.
        /// </summary>
        /// <returns></returns>
        public Screenshot GetLastScreenshot()
        {
            try
            {
                if (_screenshotList != null)
                {
                    lock (_screenshotList)
                    {
                        return _screenshotList.Last();
                    }
                }

                return null;
            }
            catch
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
                if (_screenshotList != null)
                {
                    if (_screenshotList.Count > 0)
                    {
                        lock (_screenshotList)
                        {
                            return _screenshotList.Last(x => x.ViewId.Equals(viewId));
                        }
                    }
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Gets a list of screenshots based on the date and time.
        /// </summary>
        /// <param name="date">The date for when screenshots were taken (the date format should match "yyyy-MM-dd").</param>
        /// <param name="time">The time for when screenshots were taken (the time format should match "HH:mm:ss.fff").</param>
        /// <returns>A list of screenshots.</returns>
        public List<Screenshot> GetScreenshots(string date, string time)
        {
            return _screenshotList.Where(x => x.Date.Equals(date) && x.Time.Equals(time)).ToList();
        }

        /// <summary>
        /// Gets a list of screenshots between a start date/time and an end date/time and based on a filter.
        /// </summary>
        /// <param name="startDate">The date representing the beginning of the date/time range.</param>
        /// <param name="endDate">The date representing the end of the date/time range.</param>
        /// <param name="startTime">The time representing the beginning of the date/time range.</param>
        /// <param name="endTime">The time representing the end of the date/time range.</param>
        /// <param name="filterType">The filter type.</param>
        /// <param name="filterValue">The filter value (which corresponds to the filter type).</param>
        /// <returns>A list of screenshots.</returns>
        public List<Screenshot> GetScreenshots(DateTime startDate, DateTime endDate, DateTime startTime, DateTime endTime, string filterType, string filterValue)
        {
            if (filterType.Equals("Image Format"))
            {
                return _screenshotList.Where(x => DateTime.Parse(x.Date).Date >= startDate.Date &&
                    DateTime.Parse(x.Date).Date <= endDate.Date &&
                    DateTime.Parse(x.Time) >= startTime &&
                    DateTime.Parse(x.Time) <= endTime &&
                    x.Format != null &&
                    !string.IsNullOrEmpty(x.Format.Name) &&
                    x.Format.Name.Equals(filterValue)).ToList();
            }

            if (filterType.Equals("Label"))
            {
                return _screenshotList.Where(x => DateTime.Parse(x.Date).Date >= startDate.Date &&
                    DateTime.Parse(x.Date).Date <= endDate.Date &&
                    DateTime.Parse(x.Time) >= startTime &&
                    DateTime.Parse(x.Time) <= endTime &&
                    x.Label != null &&
                    !string.IsNullOrEmpty(x.Label) &&
                    x.Label.Equals(filterValue)).ToList();
            }

            if (filterType.Equals("Process Name"))
            {
                return _screenshotList.Where(x => DateTime.Parse(x.Date).Date >= startDate.Date &&
                    DateTime.Parse(x.Date).Date <= endDate.Date &&
                    DateTime.Parse(x.Time) >= startTime &&
                    DateTime.Parse(x.Time) <= endTime &&
                    x.ProcessName != null &&
                    !string.IsNullOrEmpty(x.ProcessName) &&
                    x.ProcessName.Equals(filterValue)).ToList();
            }

            if (filterType.Equals("Window Title"))
            {
                return _screenshotList.Where(x => DateTime.Parse(x.Date).Date >= startDate.Date &&
                    DateTime.Parse(x.Date).Date <= endDate.Date &&
                    DateTime.Parse(x.Time) >= startTime &&
                    DateTime.Parse(x.Time) <= endTime &&
                    x.WindowTitle != null &&
                    !string.IsNullOrEmpty(x.WindowTitle) &&
                    x.WindowTitle.Equals(filterValue)).ToList();
            }

            return _screenshotList.Where(x => DateTime.Parse(x.Date).Date >= startDate.Date &&
                    DateTime.Parse(x.Date).Date <= endDate.Date &&
                    DateTime.Parse(x.Time) >= startTime &&
                    DateTime.Parse(x.Time) <= endTime).ToList();
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
                if (_fileSystem.FileExists(_fileSystem.ScreenshotsFile))
                {
                    _fileSystem.DeleteFile(_fileSystem.ScreenshotsFile);

                    _log.WriteErrorMessage("The file \"" + _fileSystem.ScreenshotsFile + "\" had to be deleted because an error was encountered. You may need to force quit the application and run it again.");
                }

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
        /// <param name="screenshotsLoadLimit">The maximum number of screenshots that can be loaded.</param>
        /// <param name="nodeLoadCount">The number of nodes loaded.</param>
        /// <param name="errorCode">The error code that is returned based on what type of error is encountered.</param>
        public void LoadXmlFileAndAddScreenshots(string date, int screenshotsLoadLimit, out int nodeLoadCount, out int errorCode)
        {
            try
            {
                nodeLoadCount = 0;

                errorCode = 0;

                _mutexWriteFile.WaitOne();

                XmlNodeList xScreenshots = null;

                if (string.IsNullOrEmpty(date))
                {
                    errorCode = 1;

                    return;
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

                            nodeLoadCount = xScreenshots.Count;

                            if (xScreenshots.Count >= screenshotsLoadLimit)
                            {
                                errorCode = 2;

                                return;
                            }

                            foreach (XmlNode xScreenshot in xScreenshots)
                            {
                                // This is used for when we want backwards compatibility with older versions
                                // and need to update the screenshot reference data appropriately.
                                Screen screen = null;

                                Screenshot screenshot = new Screenshot();

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
                                                screenshot.FilePath = xReader.Value;
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

                                            case SCREENSHOT_IMAGE_DIFF:
                                                xReader.Read();
                                                screenshot.DiffPercentageWithPreviousImage = Convert.ToInt32(xReader.Value);
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
                                    xPath.InnerText = screenshot.FilePath;

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

                                    XmlElement xImageDiff = xDoc.CreateElement(SCREENSHOT_IMAGE_DIFF);
                                    xImageDiff.InnerText = screenshot.DiffPercentageWithPreviousImage.ToString();

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
                                    xScreenshot.AppendChild(xImageDiff);
                                    xScreenshot.AppendChild(xEncrypted);
                                    xScreenshot.AppendChild(xKey);
                                }

                                if (!string.IsNullOrEmpty(screenshot.Date) &&
                                        !string.IsNullOrEmpty(screenshot.Time) &&
                                        !string.IsNullOrEmpty(screenshot.FilePath) &&
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
            }
            catch (Exception ex)
            {
                if (_fileSystem.FileExists(_fileSystem.ScreenshotsFile))
                {
                    _fileSystem.DeleteFile(_fileSystem.ScreenshotsFile);

                    _log.WriteErrorMessage("The file \"" + _fileSystem.ScreenshotsFile + "\" had to be deleted because an error was encountered. You may need to force quit the application and run it again.");
                }

                _log.WriteExceptionMessage("ScreenshotCollection::LoadXmlFileAndAddScreenshots", ex);

                nodeLoadCount = 0;

                errorCode = -1;
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

                bool saveScreenshotRefs = Convert.ToBoolean(config.Settings.User.GetByKey("SaveScreenshotRefs").Value);

                if (string.IsNullOrEmpty(_fileSystem.ScreenshotsFile))
                {
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
                            xPath.InnerText = screenshot.FilePath;

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

                            XmlElement xImageDiff = xDoc.CreateElement(SCREENSHOT_IMAGE_DIFF);
                            xImageDiff.InnerText = screenshot.DiffPercentageWithPreviousImage.ToString();

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
                            xScreenshot.AppendChild(xImageDiff);
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
        /// <param name="failedUploads">A dictionary of failed uploads.</param>
        public void DeleteScreenshotsByDays(int days, string folder, MacroParser macroParser, MacroTagCollection macroTagCollection, Dictionary<Screenshot, string> failedUploads)
        {
            try
            {
                if (days == 0)
                {
                    lock (_screenshotList)
                    {
                        // Get a list of screenshots to delete.
                        List<Screenshot> screenshotsToDelete = _screenshotList.Where(x => !string.IsNullOrEmpty(x.Date)).ToList();

                        // Delete the screenshots and their associated image files.
                        DeleteScreenshots(screenshotsToDelete, failedUploads);

                        DeleteScreenshotNodes(xDoc.SelectNodes(SCREENSHOT_XPATH));

                        // Delete everything in the specified folder and delete the folder itself.
                        DeleteFolderByDate(folder, DateTime.Now, macroParser, macroTagCollection, _log);
                    }
                }
                else
                {
                    lock (_screenshotList)
                    {
                        // Check what we already have in memory and remove the screenshot object from every list.
                        List<Screenshot> screenshotsToDelete = _screenshotList.Where(x => !string.IsNullOrEmpty(x.Date) && Convert.ToDateTime(x.Date) <= DateTime.Now.Date.AddDays(-days)).ToList();

                        // Delete the screenshots and their associated image files.
                        DeleteScreenshots(screenshotsToDelete, failedUploads);
                    }

                    XmlNode minDateNode = GetMinDateFromXMLDocument();

                    if (minDateNode != null)
                    {
                        // Get the minimum date for the oldest screenshot available.
                        DateTime dtMin = DateTime.Parse(minDateNode.FirstChild.Value).Date;

                        // Get the maximum date we want to consider based on the value of "days".
                        // So this should be the date of "days" old.
                        DateTime dtMax = DateTime.Now.Date.AddDays(-days).Date;

                        // Delete the screenshot XML nodes based on the dates.
                        for (DateTime date = dtMin; date.Date <= dtMax; date = date.AddDays(1))
                        {
                            XmlNodeList screenshotNodesToDeleteByDate = xDoc.SelectNodes(SCREENSHOT_XPATH + "[" + SCREENSHOT_DATE + "='" + date.ToString(macroParser.DateFormat) + "']");

                            DeleteScreenshotNodes(screenshotNodesToDeleteByDate);

                            // Let's see if we can delete the folder by the date of the current iteration if a folder path has been provided.
                            DeleteFolderByDate(folder, date, macroParser, macroTagCollection, _log);
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
                _log.WriteExceptionMessage("ScreenshotCollection::DeleteScreenshotsByDays", ex);
            }
        }

        /// <summary>
        /// Deletes screenshots based on the specified screen capture cycle count. This value needs to be greater than 0.
        /// </summary>
        /// <param name="cycleCount">The cycle count to use when considering when to delete screenshots.</param>
        /// <param name="failedUploads">A dictionary of failed uploads.</param>
        public void DeleteScreenshotsByCycleCount(int cycleCount, Dictionary<Screenshot, string> failedUploads)
        {
            try
            {
                if (cycleCount > 0)
                {
                    lock (_slideList)
                    {
                        List<Slide> slidesToDelete = new List<Slide>();

                        // Get a list of slides representing the screen capture cycles.
                        // This should be in order from oldest to newest so the oldest slide
                        // in this list should be the first slide we encounter (by index).
                        for (int i = 0; i < cycleCount; i++)
                        {
                            Slide slide = _slideList[i];
                            slidesToDelete.Add(slide);
                        }

                        // Now that we have the slides go through each slide and extract the date and time from the slide name.
                        foreach (Slide slide in slidesToDelete)
                        {
                            Regex rgxSlideNameSplitToDateAndTime = new Regex(@"^\{date=(?<Date>\d{4}-\d{2}-\d{2})\}\{time=(?<Time>\d{2}:\d{2}:\d{2}\.\d{3})\}$");

                            if (rgxSlideNameSplitToDateAndTime.IsMatch(slide.Name))
                            {
                                string strDate = rgxSlideNameSplitToDateAndTime.Match(slide.Name).Groups["Date"].Value;
                                string strTime = rgxSlideNameSplitToDateAndTime.Match(slide.Name).Groups["Time"].Value;

                                lock (_screenshotList)
                                {
                                    // Use the extracted date and time to get the list of screenshots we need to delete
                                    // based on when those screenshots were taken.
                                    List<Screenshot> screenshotsToDelete = GetScreenshots(strDate, strTime);

                                    // Delete the screenshots.
                                    DeleteScreenshots(screenshotsToDelete, failedUploads);
                                }

                                // Get a list of screenshot nodes by date and time.
                                XmlNodeList screenshotNodesToDeleteByDateAndTime = xDoc.SelectNodes(SCREENSHOT_XPATH + "[" + SCREENSHOT_DATE + "='" + strDate + "' and " + SCREENSHOT_TIME + "='" + strTime + "']");

                                // Delete the screenshot XNL nodes.
                                DeleteScreenshotNodes(screenshotNodesToDeleteByDateAndTime);
                            }
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
                _log.WriteExceptionMessage("ScreenshotCollection::DeleteScreenshotsByCycleCount", ex);
            }
        }

        /// <summary>
        /// Deletes screenshots from the oldest screen capture cycle. You can use this for a rolling delete given the correctly configured setup.
        /// </summary>
        /// <param name="failedUploads">A dictionary of failed uploads.</param>
        public void DeleteScreenshotsFromOldestCaptureCycle(Dictionary<Screenshot, string> failedUploads)
        {
            try
            {
                lock (_slideList)
                {
                    // Get the first slide in the slide list. This should be the oldest slide we can find.
                    Slide slide = _slideList.First<Slide>();

                    Regex rgxSlideNameSplitToDateAndTime = new Regex(@"^\{date=(?<Date>\d{4}-\d{2}-\d{2})\}\{time=(?<Time>\d{2}:\d{2}:\d{2}\.\d{3})\}$");

                    if (rgxSlideNameSplitToDateAndTime.IsMatch(slide.Name))
                    {
                        string strDate = rgxSlideNameSplitToDateAndTime.Match(slide.Name).Groups["Date"].Value;
                        string strTime = rgxSlideNameSplitToDateAndTime.Match(slide.Name).Groups["Time"].Value;

                        lock (_screenshotList)
                        {
                            // Use the extracted date and time to get the list of screenshots we need to delete
                            // based on when those screenshots were taken.
                            List<Screenshot> screenshotsToDelete = GetScreenshots(strDate, strTime);

                            // Delete the screenshots.
                            DeleteScreenshots(screenshotsToDelete, failedUploads);
                        }

                        // Get a list of screenshot nodes by date and time.
                        XmlNodeList screenshotNodesToDeleteByDateAndTime = xDoc.SelectNodes(SCREENSHOT_XPATH + "[" + SCREENSHOT_DATE + "='" + strDate + "' and " + SCREENSHOT_TIME + "='" + strTime + "']");

                        // Delete the screenshot XNL nodes.
                        DeleteScreenshotNodes(screenshotNodesToDeleteByDateAndTime);
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                _log.WriteErrorMessage("Access to the file system was denied due to insufficient permissions during screenshot deletion operation");
            }
            catch (Exception ex)
            {
                _log.WriteExceptionMessage("ScreenshotCollection::DeleteScreenshotsFromOldestCaptureCycle", ex);
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