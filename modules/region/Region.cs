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
        public Region()
        {
        }

        public Region(string name, int x, int y, int width, int height, string macro)
        {
            Name = name;
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Macro = macro;
        }

        public string Name { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string Macro { get; set; }
    }
}