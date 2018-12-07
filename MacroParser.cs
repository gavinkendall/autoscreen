//-----------------------------------------------------------------------
// <copyright file="MacroParser.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    public static class MacroParser
    {
        public static readonly string DateFormat = "yyyy-MM-dd";
        public static readonly string TimeFormat = "HH-mm-ss-fff";

        public static readonly string RegionMacro =   @"%region%\%year%-%month%-%day%_%hour%-%minute%-%second%-%millisecond%.%format%";
        public static readonly string UserMacro = @"%screen%\%year%-%month%-%day%_%hour%-%minute%-%second%-%millisecond%.%format%";
        public static readonly string ScreenshotListMacro = @"%year%-%month%-%day%\%year%-%month%-%day%_%hour%-%minute%-%second%-%millisecond%.%format%";
        public static readonly string ApplicationMacro = @"%year%-%month%-%day%\%screen%\%year%-%month%-%day%_%hour%-%minute%-%second%-%millisecond%.%format%";

        public static string ParseTags(ImageFormatCollection imageFormatCollection, string path, string name)
        {
            if (!string.IsNullOrEmpty(ScreenCapture.ImageFormat))
            {
                path = path.Replace("%format%",
                    imageFormatCollection.GetByName(ScreenCapture.ImageFormat).Extension.TrimStart('.'));
            }

            if (!string.IsNullOrEmpty(name))
            {
                path = path.Replace("%region%", name);
                path = path.Replace("%screen%", name);
            }

            path = path.Replace("%year%", ScreenCapture.DateTimePreviousScreenshot.ToString("yyyy"));
            path = path.Replace("%month%", ScreenCapture.DateTimePreviousScreenshot.ToString("MM"));
            path = path.Replace("%day%", ScreenCapture.DateTimePreviousScreenshot.ToString("dd"));
            path = path.Replace("%hour%", ScreenCapture.DateTimePreviousScreenshot.ToString("HH"));
            path = path.Replace("%minute%", ScreenCapture.DateTimePreviousScreenshot.ToString("mm"));
            path = path.Replace("%second%", ScreenCapture.DateTimePreviousScreenshot.ToString("ss"));
            path = path.Replace("%millisecond%", ScreenCapture.DateTimePreviousScreenshot.ToString("fff"));

            return path;
        }
    }
}