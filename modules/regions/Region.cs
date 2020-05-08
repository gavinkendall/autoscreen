//-----------------------------------------------------------------------
// <copyright file="Region.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A class representing a region.</summary>
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
        public bool Enabled { get; set; }

        /// <summary>
        /// The empty constructor of the region.
        /// </summary>
        public Region()
        {

        }

        /// <summary>
        /// The constructor for creating a region.
        /// </summary>
        /// <param name="name">The name of the region.</param>
        /// <param name="folder">The folder path for the region capture.</param>
        /// <param name="macro">The macro for the region capture's filename.</param>
        /// <param name="format">The image format of the region capture.</param>
        /// <param name="jpegQuality">The JPEG quality of the region capture.</param>
        /// <param name="resolutionRatio">The resolution ratio of the region capture.</param>
        /// <param name="mouse">Determines if we include the mouse pointer in the region capture.</param>
        /// <param name="x">The X coordinate of the region capture.</param>
        /// <param name="y">The Y coordinate of the region capture.</param>
        /// <param name="width">The width of the region capture.</param>
        /// <param name="height">The height of the region capture.</param>
        /// <param name="enabled">Determines if the region capture should be active or inactive.</param>
        public Region(string name, string folder, string macro, ImageFormat format, int jpegQuality, int resolutionRatio, bool mouse, int x, int y, int width, int height, bool enabled)
        {
            ViewId = Guid.NewGuid();
            Name = name;
            Folder = FileSystem.CorrectScreenshotsFolderPath(folder);
            Macro = macro;
            Format = format;
            JpegQuality = jpegQuality;
            ResolutionRatio = resolutionRatio;
            Mouse = mouse;
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Enabled = enabled;
        }
    }
}