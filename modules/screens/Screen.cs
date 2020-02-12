//-----------------------------------------------------------------------
// <copyright file="Screen.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A class representing a region.</summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class Screen
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
        public int Component { get; set; }

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
        public bool Enabled { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Screen()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="folder"></param>
        /// <param name="macro"></param>
        /// <param name="component"></param>
        /// <param name="format"></param>
        /// <param name="jpegQuality"></param>
        /// <param name="resolutionRatio"></param>
        /// <param name="mouse"></param>
        /// <param name="enabled"></param>
        public Screen(string name, string folder, string macro, int component, ImageFormat format, int jpegQuality, int resolutionRatio, bool mouse, bool enabled)
        {
            ViewId = Guid.NewGuid();
            Name = name;
            Folder = FileSystem.CorrectScreenshotsFolderPath(folder);
            Macro = macro;
            Component = component;
            Format = format;
            JpegQuality = jpegQuality;
            ResolutionRatio = resolutionRatio;
            Mouse = mouse;
            Enabled = enabled;
        }
    }
}