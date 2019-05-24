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
        public static ScreenCapture screenCapture;
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
        public static readonly string DefaultMacro = @"%date%\%name%\%date%_%time%.%format%";

        public static string ParseTags(string macro)
        {
            return ParseTags(string.Empty, macro, null);
        }

        /// <summary>
        /// Replaces tags (such as "%year%") with an appropriate value (such as "2019").
        /// </summary>
        /// <param name="name">The name of a region or screen when parsing the %name% tag.</param>
        /// <param name="macro">The macro to parse. A macro usually includes tags such as %count% and %date%.</param>
        /// <param name="format">The image format to use as an image file extension when parsing the %format% tag.</param>
        /// <returns>A parsed macro containing the appropriate values of respective tags in the provided macro.</returns>
        public static string ParseTags(string name, string macro, ImageFormat format)
        {
            macro = !string.IsNullOrEmpty(name) ? macro.Replace(MacroTagSpec.Name, name) : macro;
            macro = format != null && !string.IsNullOrEmpty(format.Name) ? macro.Replace(MacroTagSpec.Format, format.Name.ToLower()) : macro;
            macro = macro.Replace(MacroTagSpec.Date, screenCapture.DateTimePreviousCycle.ToString(DateFormat));
            macro = macro.Replace(MacroTagSpec.Time, screenCapture.DateTimePreviousCycle.ToString(TimeFormatForWindows));
            macro = macro.Replace(MacroTagSpec.Year, screenCapture.DateTimePreviousCycle.ToString(YearFormat));
            macro = macro.Replace(MacroTagSpec.Month, screenCapture.DateTimePreviousCycle.ToString(MonthFormat));
            macro = macro.Replace(MacroTagSpec.Day, screenCapture.DateTimePreviousCycle.ToString(DayFormat));
            macro = macro.Replace(MacroTagSpec.Hour, screenCapture.DateTimePreviousCycle.ToString(HourFormat));
            macro = macro.Replace(MacroTagSpec.Minute, screenCapture.DateTimePreviousCycle.ToString(MinuteFormat));
            macro = macro.Replace(MacroTagSpec.Second, screenCapture.DateTimePreviousCycle.ToString(SecondFormat));
            macro = macro.Replace(MacroTagSpec.Millisecond, screenCapture.DateTimePreviousCycle.ToString(MillisecondFormat));
            macro = macro.Replace(MacroTagSpec.Count, screenCapture.Count.ToString());

            return macro;
        }
    }
}