//-----------------------------------------------------------------------
// <copyright file="Screenshot.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2023 Gavin Kendall
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
using System.Drawing;

namespace AutoScreenCapture
{
    /// <summary>
    /// A class representing a screenshot.
    /// </summary>
    public class Screenshot
    {
        /// <summary>
        /// The unique identifier for the individual screenshot object.
        /// </summary>
        public Guid Id { get; set; }

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
        /// The filepath of the screenshot's image file.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// The path of the folder where the screenshot came from (which could be a Screen or a Region).
        /// </summary>
        public string FolderPath { get; set; }

        /// <summary>
        /// The path of the macro (file pattern) which the screenshot extracts from either a Screen or a Region.
        /// </summary>
        public string MacroPath { get; set; }

        /// <summary>
        /// The image format of the screenshot.
        /// </summary>
        public ImageFormat Format { get; set; }

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
        public string Version { get; set; }

        /// <summary>
        /// Determines if the screenshot has been saved to disk.
        /// </summary>
        public bool SavedToDisk { get; set; }

        /// <summary>
        /// Determines if the screenshot reference has been saved to the "screenshots" XML file.
        /// </summary>
        public bool ReferenceSaved { get; set; }

        /// <summary>
        /// The bitmap image associated with the screenshot.
        /// </summary>
        public Bitmap Bitmap { get; set; }

        /// <summary>
        /// The percentage of image difference between the current screenshot's image and the previous screenshot's image.
        /// </summary>
        public int DiffPercentageWithPreviousImage { get; set; }

        /// <summary>
        /// Determines if the screenshot is encrypted.
        /// </summary>
        public bool Encrypted { get; set; }

        /// <summary>
        /// Determines if we should encrypt the screenshot.
        /// </summary>
        public bool Encrypt { get; set; }

        /// <summary>
        /// The key used for encrypting the screenshot.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// The consructor for creating a screenshot (and its associated slide).
        /// </summary>
        public Screenshot()
        {
            Slide = new Slide();
        }

        /// <summary>
        /// The constructor for creating a screenshot.
        /// </summary>
        /// <param name="windowTitle">The title of the active window when the screenshot was taken.</param>
        /// <param name="dateTime">The date/time the screenshot was taken.</param>
        /// <param name="macroParser"></param>
        /// <param name="config"></param>
        public Screenshot(string windowTitle, DateTime dateTime, MacroParser macroParser, Config config)
        {
            if (string.IsNullOrEmpty(windowTitle))
            {
                return;
            }

            // Each screenshot has a unique identifier (since version 2.4) just in case we need a reference to it.
            Id = Guid.NewGuid();

            WindowTitle = windowTitle;

            Date = dateTime.ToString(macroParser.DateFormat);
            Time = dateTime.ToString(macroParser.TimeFormat);
            ReferenceSaved = false;
            Version = config.Settings.ApplicationVersion;

            Slide = new Slide()
            {
                Name = "{date=" + Date + "}{time=" + Time + "}",
                Date = Date,
                Value = Time + " [" + windowTitle + "]"
            };
        }
    }
}