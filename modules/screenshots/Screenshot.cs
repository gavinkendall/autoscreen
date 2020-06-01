//-----------------------------------------------------------------------
// <copyright file="Screenshot.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A screenshot is associated with either a screen or a region and has a date, a time, and filepath.</summary>
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

namespace AutoScreenCapture
{
    /// <summary>
    /// A class representing a screenshot.
    /// </summary>
    public class Screenshot
    {
        /// <summary>
        /// The unique identifier to associate this screenshot with either a region or a screen.
        /// </summary>
        public Guid ViewId { get; set; }

        /// <summary>
        /// The date when the screenshot was taken.
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// The time when the screenshot was taken.
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// The path of the screenshot's image file.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// The image format of the screenshot.
        /// </summary>
        public ImageFormat Format { get; set; }

        /// <summary>
        /// The screen associated with the screenshot.
        /// </summary>
        public int Screen { get; set; }

        /// <summary>
        /// The component associated with the screenshot.
        /// </summary>
        public int Component { get; set; }

        /// <summary>
        /// The slide associated with the screenshot.
        /// </summary>
        public Slide Slide { get; set; }

        /// <summary>
        /// The title of the active window that was captured for the screenshot.
        /// </summary>
        public string WindowTitle { get; set; }

        /// <summary>
        /// The process name of the active application that was captured for the screenshot.
        /// </summary>
        public string ProcessName { get; set; }

        /// <summary>
        /// The label applied to the screenshot.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        ///  The version of Auto Screen Capture that captured this screenshot.
        /// </summary>
        public string Version { get; private set; }

        /// <summary>
        /// The type of screenshot.
        /// </summary>
        public ScreenshotType ScreenshotType { get; set; }

        /// <summary>
        /// Determines if the screenshot has been written to disk and can be loaded from the "screenshots" XML document.
        /// </summary>
        public bool Saved { get; set; }

        /// <summary>
        /// The empty constructor a screenshot object.
        /// </summary>
        public Screenshot()
        {
            Saved = false;
            Version = Settings.ApplicationVersion;
        }

        /// <summary>
        /// The constructor for creating a screenshot.
        /// </summary>
        /// <param name="dateTime">The date/time the screenshot was taken.</param>
        /// <param name="path">The path of the filename for the screenshot.</param>
        /// <param name="format">The image format of the screenshot.</param>
        /// <param name="component">The component used for the screenshot.</param>
        /// <param name="screenshotType">The type of screenshot.</param>
        /// <param name="windowTitle">The title of the active window when the screenshot was taken.</param>
        /// <param name="processName">The process name of the active application when the screenshot was taken.</param>
        /// <param name="viewId">The view ID associated with either the screen or the region for the screenshot.</param>
        /// <param name="label">The label to be applied to the screenshot.</param>
        public Screenshot(DateTime dateTime, string path, ImageFormat format, int component, ScreenshotType screenshotType, string windowTitle, string processName, Guid viewId, string label)
        {
            if (string.IsNullOrEmpty(windowTitle)) return;

            ViewId = viewId;
            Date = dateTime.ToString(MacroParser.DateFormat);
            Time = dateTime.ToString(MacroParser.TimeFormat);
            Path = path;
            Format = format;
            Component = component;
            ScreenshotType = screenshotType;
            WindowTitle = windowTitle;
            ProcessName = processName + ".exe";
            Label = label;
            Saved = false;
            Version = Settings.ApplicationVersion;

            Slide = new Slide()
            {
                Name = "{date=" + Date + "}{time=" + Time + "}",
                Date = Date,
                Value = Time + " [" + windowTitle + "]"
            };
        }
    }
}