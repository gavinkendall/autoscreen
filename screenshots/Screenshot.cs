//-----------------------------------------------------------------------
// <copyright file="Screenshot.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    using System;
    using System.IO;

    public class Screenshot
    {
        public int Index { get; set; }
        public string Date { get; set; }
        public string Path { get; set; }
        public int Screen { get; set; }
        public ImageFormat Format { get; set; }
        public string Filename { get; set; }
        public string Slidename { get; set; }

        public Screenshot()
        {
        }

        public Screenshot(DateTime dateTime, string path, int screen, ImageFormat format, int index)
        {
            Index = index;
            Date = dateTime.ToString(MacroParser.DateFormat);
            Path = path;
            Screen = screen;
            Format = format;
            Filename = System.IO.Path.GetFileName(path);
            Slidename = dateTime.ToString(MacroParser.DateFormat) + " " + dateTime.ToString(MacroParser.TimeFormat) + " " + format;

            string directory = System.IO.Path.GetDirectoryName(path);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            Directory.SetCurrentDirectory(directory);
        }
    }
}