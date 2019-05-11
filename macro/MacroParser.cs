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
        public static readonly string YearFormat = "yyyy";
        public static readonly string MonthFormat = "MM";
        public static readonly string DayFormat = "dd";
        public static readonly string HourFormat = "HH";
        public static readonly string MinuteFormat = "mm";
        public static readonly string SecondFormat = "ss";
        public static readonly string MillisecondFormat = "fff";
        public static readonly string DateFormat = YearFormat + "-" + MonthFormat + "-" + DayFormat;
        public static readonly string TimeFormat = HourFormat + ":" + MinuteFormat + ":" + SecondFormat + "." + MillisecondFormat;
        public static readonly string TimeFormatForWindows = HourFormat + "-" + MinuteFormat + "-" + SecondFormat + "-" + MillisecondFormat;
        public static readonly string DefaultMacro = @"%name%\screenshot %count% on %date% at %time%.%format%";

        /// <summary>
        /// Replaces tags (such as "%year%") with an appropriate value (such as "2019").
        /// </summary>
        /// <param name="name">The name of a region or screen when parsing the %name% tag.</param>
        /// <param name="macro">The macro to parse. A macro usually includes tags such as %count% and %date%.</param>
        /// <param name="format">The image format to use as an image file extension when parsing the %format% tag.</param>
        /// <returns>A parsed macro containing the appropriate values of respective tags in the provided macro.</returns>
        public static string ParseTags(string name, string macro, ImageFormat format)
        {
            macro = macro.Replace(MacroTagSpec.Name, name);
            macro = macro.Replace(MacroTagSpec.Format, format.Name.ToLower());
            macro = macro.Replace(MacroTagSpec.Date, ScreenCapture.DateTimePreviousScreenshot.ToString(DateFormat));
            macro = macro.Replace(MacroTagSpec.Time, ScreenCapture.DateTimePreviousScreenshot.ToString(TimeFormatForWindows));
            macro = macro.Replace(MacroTagSpec.Year, ScreenCapture.DateTimePreviousScreenshot.ToString(YearFormat));
            macro = macro.Replace(MacroTagSpec.Month, ScreenCapture.DateTimePreviousScreenshot.ToString(MonthFormat));
            macro = macro.Replace(MacroTagSpec.Day, ScreenCapture.DateTimePreviousScreenshot.ToString(DayFormat));
            macro = macro.Replace(MacroTagSpec.Hour, ScreenCapture.DateTimePreviousScreenshot.ToString(HourFormat));
            macro = macro.Replace(MacroTagSpec.Minute, ScreenCapture.DateTimePreviousScreenshot.ToString(MinuteFormat));
            macro = macro.Replace(MacroTagSpec.Second, ScreenCapture.DateTimePreviousScreenshot.ToString(SecondFormat));
            macro = macro.Replace(MacroTagSpec.Millisecond, ScreenCapture.DateTimePreviousScreenshot.ToString(MillisecondFormat));
            macro = macro.Replace(MacroTagSpec.Count, ScreenCapture.Count.ToString());

            return macro;
        }
    }
}