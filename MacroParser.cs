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
        public static readonly string DefaultMacro = @"%name%\%year%-%month%-%day%_%hour%-%minute%-%second%-%millisecond%.%format%";

        /// <summary>
        /// Replaces tags (such as "%year%") with an appropriate value (such as "2019").
        /// </summary>
        /// <param name="name">The name of a region or screen when parsing the %name% tag.</param>
        /// <param name="macro">The macro to parse. A macro usually includes tags such as %year%, %month%, %day%, %hour%, %minute%, %second%, and %millisecond%.</param>
        /// <param name="format">The image format to use as an image file extension when parsing the %format% tag.</param>
        /// <returns>A parsed macro containing the appropriate values of respective tags in the provided macro.</returns>
        public static string ParseTags(string name, string macro, ImageFormat format)
        {
            macro = macro.Replace("%name%", name);
            macro = macro.Replace("%year%", ScreenCapture.DateTimePreviousScreenshot.ToString("yyyy"));
            macro = macro.Replace("%month%", ScreenCapture.DateTimePreviousScreenshot.ToString("MM"));
            macro = macro.Replace("%day%", ScreenCapture.DateTimePreviousScreenshot.ToString("dd"));
            macro = macro.Replace("%hour%", ScreenCapture.DateTimePreviousScreenshot.ToString("HH"));
            macro = macro.Replace("%minute%", ScreenCapture.DateTimePreviousScreenshot.ToString("mm"));
            macro = macro.Replace("%second%", ScreenCapture.DateTimePreviousScreenshot.ToString("ss"));
            macro = macro.Replace("%millisecond%", ScreenCapture.DateTimePreviousScreenshot.ToString("fff"));
            macro = macro.Replace("%format%", format.Extension.TrimStart('.'));

            return macro;
        }
    }
}