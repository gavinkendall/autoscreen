//-----------------------------------------------------------------------
// <copyright file="MacroParser.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------

using System.Text.RegularExpressions;

namespace AutoScreenCapture
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public static class MacroParser
    {
        private static readonly string WindowsPathRegexPattern = "^(?<DriveOrUNCPrefix>[A-Z]:\\\\{1}|\\\\{1})(?<Path>.+)$";

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
        /// <param name="screenNumber"></param>
        /// <param name="format">The image format to use as an image file extension when parsing the %format% tag.</param>
        /// <param name="activeWindowTitle">The title of the active window.</param>
        /// <returns>A parsed macro containing the appropriate values of respective tags in the provided macro.</returns>
        public static string ParseTags(string name, string macro, int screenNumber, ImageFormat format, string activeWindowTitle)
        {
            // Strip out any backslash characters from the name so we avoid creating unnecessary directories based on the name.
            name = name.Replace("\\", string.Empty);

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
            macro = macro.Replace(MacroTagSpec.Title, activeWindowTitle);
            macro = ParseTagsForUserAndMachine(macro);

            return StripInvalidWindowsCharacters(macro);
        }

        /// <summary>
        /// Replaces %user% and %machine% with the name of the user and the name of the machine respectively.
        /// </summary>
        /// <param name="macro"></param>
        /// <returns></returns>
        public static string ParseTagsForUserAndMachine(string macro)
        {
            macro = macro.Replace(MacroTagSpec.User, Environment.UserName);
            macro = macro.Replace(MacroTagSpec.Machine, Environment.MachineName);

            return StripInvalidWindowsCharacters(macro);
        }

        /// <summary>
        /// Removes characters from a given string that are invalid to Windows.
        /// </summary>
        /// <param name="text">The string to parse.</param>
        /// <returns>A string that no longer contains invalid Windows characters.</returns>
        private static string StripInvalidWindowsCharacters(string text)
        {
            string prefix = string.Empty;

            if (Regex.IsMatch(text, WindowsPathRegexPattern))
            {
                prefix = Regex.Match(text, WindowsPathRegexPattern).Groups["DriveOrUNCPrefix"].Value;
                text = Regex.Match(text, WindowsPathRegexPattern).Groups["Path"].Value;
            }

            text = text.Replace("/", string.Empty);
            text = text.Replace(":", string.Empty);
            text = text.Replace("*", string.Empty);
            text = text.Replace("?", string.Empty);
            text = text.Replace("\"", string.Empty);
            text = text.Replace("<", string.Empty);
            text = text.Replace(">", string.Empty);
            text = text.Replace("|", string.Empty);

            return prefix + text;
        }
    }
}