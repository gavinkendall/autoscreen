//-----------------------------------------------------------------------
// <copyright file="Screen.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A class representing a region.</summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    public class Screen
    {
        public string Name { get; set; }
        public string Folder { get; set; }
        public string Macro { get; set; }
        public int Component { get; set; }
        public ImageFormat Format { get; set; }
        public int JpegQuality { get; set; }
        public int ResolutionRatio { get; set; }
        public bool Mouse { get; set; }

        public Screen()
        {
        }

        public Screen(string name, string folder, string macro, int component, ImageFormat format, int jpegQuality, int resolutionRatio, bool mouse)
        {
            Name = name;
            Folder = folder;
            Macro = macro;
            Component = component;
            Format = format;
            JpegQuality = jpegQuality;
            ResolutionRatio = resolutionRatio;
            Mouse = mouse;
        }
    }
}