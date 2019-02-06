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
        public string Date { get; set; }
        public string Path { get; set; }
        public ImageFormat Format { get; set; }
        public string Slide { get; set; }

        public Screenshot()
        {

        }

        public Screenshot(DateTime dateTime, string path, ImageFormat format)
        {
            Date = dateTime.ToString(MacroParser.DateFormat);
            Path = path;
            Format = format;
            Slide = dateTime.ToString(MacroParser.DateFormat) + " " + dateTime.ToString(MacroParser.TimeFormat) + " " + format.Name;

            string directory = System.IO.Path.GetDirectoryName(path);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            Directory.SetCurrentDirectory(directory);
        }
    }
}