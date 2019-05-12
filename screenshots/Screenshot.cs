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

    public class Screenshot
    {
        public Guid ViewId { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Path { get; set; }
        public ImageFormat Format { get; set; }
        public int Screen { get; set; }
        public int Component { get; set; }
        public Slide Slide { get; set; }
        public string WindowTitle { get; set; }
        public string Label { get; set; }
        public ScreenshotType ScreenshotType { get; set; }

        public Screenshot()
        {
        }

        public Screenshot(DateTime dateTime, string path, ImageFormat format, int component, ScreenshotType screenshotType, string windowTitle, Guid viewId, string label)
        {
            ViewId = viewId;
            Date = dateTime.ToString(MacroParser.DateFormat);
            Time = dateTime.ToString(MacroParser.TimeFormat);
            Path = path;
            Format = format;
            Component = component;
            ScreenshotType = screenshotType;
            WindowTitle = windowTitle;
            Label = label;

            Slide = new Slide()
            {
                Name = "{date=" + Date + "}{time=" + Time + "}",
                Date = Date,
                Value = Time + (!string.IsNullOrEmpty(windowTitle) ? " [" + windowTitle + "]" : string.Empty)
            };
        }
    }
}