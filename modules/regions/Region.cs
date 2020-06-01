//-----------------------------------------------------------------------
// <copyright file="Region.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
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
    /// A class representing an area of the screen (a region).
    /// </summary>
    public class Region
    {
        /// <summary>
        /// A unique identifier for the view which this region is associated with.
        /// </summary>
        public Guid ViewId { get; set; }

        /// <summary>
        /// The name of the region.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The folder path for the region capture.
        /// </summary>
        public string Folder { get; set; }

        /// <summary>
        /// The macro for the region capture's filename.
        /// </summary>
        public string Macro { get; set; }

        /// <summary>
        /// The image format of the region capture.
        /// </summary>
        public ImageFormat Format { get; set; }

        /// <summary>
        /// The JPEG quality of the region capture.
        /// </summary>
        public int JpegQuality { get; set; }

        /// <summary>
        /// The resolution ratio of the region capture.
        /// </summary>
        public int ResolutionRatio { get; set; }

        /// <summary>
        /// Determines if we include the mouse pointer in the region capture.
        /// </summary>
        public bool Mouse { get; set; }

        /// <summary>
        /// The X coordinate value for the region capture's "left" (horizontal) position.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// The Y coordinate value for the region capture's "top" (vertical) position.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// The width of the region capture.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// The height of the region capture.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Determines if the region is active or inactive.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// The empty constructor of the region.
        /// </summary>
        public Region()
        {

        }
    }
}