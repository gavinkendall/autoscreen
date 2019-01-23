//-----------------------------------------------------------------------
// <copyright file="Region.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A class representing a region.</summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    public class Region
    {
        public string Name { get; set; }
        public string Folder { get; set; }
        public string Macro { get; set; }
        public ImageFormat Format { get; set; }
        public int JpegQuality { get; set; }
        public int ResolutionRatio { get; set; }
        public bool Mouse { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Region()
        {
        }

        public Region(string name, string folder, string macro, ImageFormat format, int jpegQuality, int resolutionRatio, bool mouse, int x, int y, int width, int height)
        {
            Name = name;
            Folder = folder;
            Macro = macro;
            Format = format;
            JpegQuality = jpegQuality;
            ResolutionRatio = resolutionRatio;
            Mouse = mouse;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }
}