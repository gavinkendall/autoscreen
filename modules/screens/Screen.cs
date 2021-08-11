//-----------------------------------------------------------------------
// <copyright file="Screen.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2021 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A class representing a region.</summary>
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
    /// A class representing a screen.
    /// </summary>
    public class Screen
    {
        /// <summary>
        /// A unique identifier for the view which this screen is associated with.
        /// </summary>
        public Guid ViewId { get; set; }

        /// <summary>
        /// The name of the screen.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The folder path for the screen capture.
        /// </summary>
        public string Folder { get; set; }

        /// <summary>
        /// The macro for the screen capture's filename.
        /// </summary>
        public string Macro { get; set; }

        /// <summary>
        /// The component (whether it be the active window or an available screen).
        /// </summary>
        public int Component { get; set; }

        /// <summary>
        /// The image format of the screen capture.
        /// </summary>
        public ImageFormat Format { get; set; }

        /// <summary>
        /// The JPEG quality of the screen capture.
        /// </summary>
        public int JpegQuality { get; set; }

        /// <summary>
        /// Determines if we include the mouse pointer in the screen capture.
        /// </summary>
        public bool Mouse { get; set; }

        /// <summary>
        /// Determines if the screen is enabled or disabled.
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// The X coordinate value for the screen's "left" (horizontal) position.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// The Y coordinate value for the screen's "top" (vertical) position.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// The width of the screen.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// The height of the screen.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// The source of the display properties (whether it be from the screens.xml file or from the Windows operating system).
        /// </summary>
        public int Source { get; set; }

        /// <summary>
        /// The name of the device.
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// The capture method.
        /// </summary>
        public int CaptureMethod { get; set; }

        /// <summary>
        /// "Automatically adapt to display setup".
        /// The position and size will automatically adjust based on your display setup.
        /// Each screen that has AutoAdapt enabled will be part of the AutoAdapt group and
        /// will have an AutoAdaptIndex associated with it (applied by the BuildViewTabPages method).
        /// </summary>
        public bool AutoAdapt { get; set; }

        /// <summary>
        /// The index of the screen participating in the AutoAdapt group of screens (which is any screen with AutoAdapt enabled).
        /// </summary>
        public int AutoAdaptIndex { get; set; }

        /// <summary>
        /// The empty constructor of the screen.
        /// </summary>
        public Screen()
        {

        }
    }
}