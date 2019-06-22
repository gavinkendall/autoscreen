//-----------------------------------------------------------------------
// <copyright file="MacroParser.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public static class MacroParser
    {
        /// <summary>
        /// 
        /// </summary>
        public static ScreenCapture screenCapture;

        /// <summary>
        /// 
        /// </summary>
        public static readonly string YearFormat = "yyyy";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string MonthFormat = "MM";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string DayFormat = "dd";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string HourFormat = "HH";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string MinuteFormat = "mm";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string SecondFormat = "ss";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string MillisecondFormat = "fff";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string DateFormat = YearFormat + "-" + MonthFormat + "-" + DayFormat;

        /// <summary>
        /// 
        /// </summary>
        public static readonly string TimeFormat = HourFormat + ":" + MinuteFormat + ":" + SecondFormat + "." + MillisecondFormat;

        /// <summary>
        /// 
        /// </summary>
        public static readonly string TimeFormatForWindows = HourFormat + "-" + MinuteFormat + "-" + SecondFormat + "-" + MillisecondFormat;

        /// <summary>
        /// 
        /// </summary>
        public static readonly string DefaultMacro = @"%date%\%name%\%date%_%time%.%format%";

        /// <summary>
        /// Replaces tags (such as "%year%") with an appropriate value (such as "2019").
        /// </summary>
        /// <param name="name">The name of a region or screen when parsing the %name% tag.</param>
        /// <param name="macro">The macro to parse. A macro usually includes tags such as %count% and %date%.</param>
        /// <param name="format">The image format to use as an image file extension when parsing the %format% tag.</param>
        /// <returns>A parsed macro containing the appropriate values of respective tags in the provided macro.</returns>
        public static string ParseTags(string name, string macro, int screenNumber, ImageFormat format)
        {
            macro = !string.IsNullOrEmpty(name) ? macro.Replace(MacroTagSpec.Name, name) : macro;
            macro = macro.Replace(MacroTagSpec.ScreenNumber, screenNumber.ToString());
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
            macro = macro.Replace(MacroTagSpec.User, Environment.UserName);
            macro = macro.Replace(MacroTagSpec.Machine, Environment.MachineName);

            return macro;
        }
    }
}