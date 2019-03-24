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
        public Guid ViewId { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Path { get; set; }
        public ImageFormat Format { get; set; }
        public int Component { get; set; }
        public Slide Slide { get; set; }
        public string WindowTitle { get; set; }

        public Screenshot()
        {
        }

        public Screenshot(DateTime dateTime, string path, ImageFormat format, int component, string windowTitle, Guid viewId)
        {
            ViewId = viewId;
            Date = dateTime.ToString(MacroParser.DateFormat);
            Time = dateTime.ToString(MacroParser.TimeFormat);
            Path = path;
            Format = format;
            Component = component;
            WindowTitle = windowTitle;

            Slide = new Slide()
            {
                Name = "{date=" + Date + "}{time=" + Time + "}",
                Value = Time + (!string.IsNullOrEmpty(windowTitle) ? " [" + windowTitle + "]" : string.Empty)
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