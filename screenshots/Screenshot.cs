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
        public int Component { get; set; }
        public Slide Slide { get; set; }

        public Screenshot()
        {
        }

        public Screenshot(DateTime dateTime, string path, ImageFormat format, int component)
        {
            Date = dateTime.ToString(MacroParser.DateFormat);
            Path = path;
            Format = format;
            Component = component;

            Slide = new Slide()
            {
                Name = "{date=" + dateTime.ToString(MacroParser.DateFormat) + "}{time=" + dateTime.ToString(MacroParser.TimeFormat) + "}",
                Value = dateTime.ToString(MacroParser.FriendlyTimeFormat)
            };

            string directory = System.IO.Path.GetDirectoryName(path);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            Directory.SetCurrentDirectory(directory);
        }
    }
}