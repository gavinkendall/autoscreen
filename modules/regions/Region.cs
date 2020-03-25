//-----------------------------------------------------------------------
// <copyright file="Region.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A class representing a region.</summary>
//-----------------------------------------------------------------------
using System;

namespace AutoScreenCapture
{
    /// <summary>
    /// 
    /// </summary>
    public class Region
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid ViewId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Folder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Macro { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ImageFormat Format { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int JpegQuality { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ResolutionRatio { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Mouse { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Region()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="folder"></param>
        /// <param name="macro"></param>
        /// <param name="format"></param>
        /// <param name="jpegQuality"></param>
        /// <param name="resolutionRatio"></param>
        /// <param name="mouse"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="enabled"></param>
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