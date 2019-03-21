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
        public string Path { get; set; }
        public ImageFormat Format { get; set; }
        public int Component { get; set; }
        public Slide Slide { get; set; }
        public string ActiveWindowTitle { get; set; }

        public Screenshot()
        {
        }

        public Screenshot(DateTime dateTime, string path, ImageFormat format, int component, string activeWindowTitle, Guid viewId)
        {
            ViewId = viewId;
            Date = dateTime.ToString(MacroParser.DateFormat);
            Path = path;
            Format = format;
            Component = component;
            ActiveWindowTitle = activeWindowTitle;

            Slide = new Slide()
            {
                Name = "{date=" + dateTime.ToString(MacroParser.DateFormat) + "}{time=" + dateTime.ToString(MacroParser.TimeFormat) + "}",
                Value = dateTime.ToString(MacroParser.TimeFormat) + (!string.IsNullOrEmpty(activeWindowTitle) ? " [" + activeWindowTitle + "]" : string.Empty)
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