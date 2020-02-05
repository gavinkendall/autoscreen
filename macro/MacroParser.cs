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
        /// 
        /// </summary>
        /// <param name="macro"></param>
        /// <param name="tagCollection"></param>
        /// <returns></returns>
        public static string ParseTagsForFolderPath(string macro, TagCollection tagCollection)
        {
            foreach (Tag tag in tagCollection)
            {
                switch (tag.Type)
                {
                    case TagType.DateTimeFormat:
                        macro = macro.Replace(tag.Name, screenCapture.DateTimePreviousCycle.ToString(tag.DateTimeFormatValue));
                        break;

                    case TagType.User:
                        macro = macro.Replace(tag.Name, Environment.UserName);
                        break;

                    case TagType.Machine:
                        macro = macro.Replace(tag.Name, Environment.MachineName);
                        break;
                }
            }

            return macro;
        }

        /// <summary>
        /// Replaces tags (such as "%year%") with an appropriate value (such as "2019").
        /// </summary>
        /// <param name="name">The name of a region or screen when parsing the %name% tag.</param>
        /// <param name="macro">The macro to parse. A macro usually includes tags such as %count% and %date%.</param>
        /// <param name="screenNumber"></param>
        /// <param name="format">The image format to use as an image file extension when parsing the %format% tag.</param>
        /// <param name="activeWindowTitle">The title of the active window.</param>
        /// <param name="tagCollection">A collection of macro tags to parse.</param>
        /// <returns>A parsed macro containing the appropriate values of respective tags in the provided macro.</returns>
        public static string ParseTagsForFilePath(string name, string macro, int screenNumber, ImageFormat format, string activeWindowTitle, TagCollection tagCollection)
        {
            // Strip out any backslash characters from the name so we avoid creating unnecessary directories based on the name.
            name = name.Replace("\\", string.Empty);

            foreach (Tag tag in tagCollection)
            {
                switch (tag.Type)
                {
                    case TagType.ActiveWindowTitle:
                        macro = macro.Replace(tag.Name, activeWindowTitle);
                        break;

                    case TagType.DateTimeFormat:
                        macro = macro.Replace(tag.Name, screenCapture.DateTimePreviousCycle.ToString(tag.DateTimeFormatValue));
                        break;

                    case TagType.ImageFormat:
                        macro = format != null && !string.IsNullOrEmpty(format.Name) ? macro.Replace(tag.Name, format.Name.ToLower()) : macro;
                        break;

                    case TagType.ScreenCaptureCycleCount:
                        macro = macro.Replace(tag.Name, screenCapture.Count.ToString());
                        break;

                    case TagType.ScreenName:
                        macro = !string.IsNullOrEmpty(name) ? macro.Replace(tag.Name, name) : macro;
                        break;

                    case TagType.ScreenNumber:
                        macro = macro.Replace(tag.Name, screenNumber.ToString());
                        break;

                    case TagType.User:
                        macro = macro.Replace(tag.Name, Environment.UserName);
                        break;

                    case TagType.Machine:
                        macro = macro.Replace(tag.Name, Environment.MachineName);
                        break;

                    case TagType.TimeOfDay:
                        string morningValue = tag.TimeOfDayMorningValue;
                        string afternoonValue = tag.TimeOfDayAfternoonValue;
                        string eveningValue = tag.TimeOfDayEveningValue;

                        // Temporarily make this tag a DateTimeFormat type because we're going to recursively call ParseTagsForFilePath
                        // for each macro tag in the morning, afternoon, and evening fields.
                        tag.Type = TagType.DateTimeFormat;

                        // Recursively call the same method we're in to parse each field as if it was a date/time macro tag.
                        // This can achieve some interesting results such as being able to call the same %timeofday% tag
                        // in onto itself because it will use its own date/time format value, but the intention is to have tags
                        // like %date%, %time%, %hour%, %minute%, and %second% be used in the morning, afternoon, and evening fields.
                        morningValue = ParseTagsForFilePath(name, morningValue, screenNumber, format, activeWindowTitle, tagCollection);
                        afternoonValue = ParseTagsForFilePath(name, afternoonValue, screenNumber, format, activeWindowTitle, tagCollection);
                        eveningValue = ParseTagsForFilePath(name, eveningValue, screenNumber, format, activeWindowTitle, tagCollection);

                        // Now that we have the new parsed values based on date/time macro tags we can set this tag back to its TimeOfDay type.
                        tag.Type = TagType.TimeOfDay;

                        if (screenCapture.DateTimePreviousCycle.TimeOfDay >= tag.TimeOfDayMorningStart.TimeOfDay &&
                            screenCapture.DateTimePreviousCycle.TimeOfDay <= tag.TimeOfDayMorningEnd.TimeOfDay)
                        {
                            macro = macro.Replace(tag.Name, morningValue);
                        }

                        if (screenCapture.DateTimePreviousCycle.TimeOfDay >= tag.TimeOfDayAfternoonStart.TimeOfDay &&
                            screenCapture.DateTimePreviousCycle.TimeOfDay <= tag.TimeOfDayAfternoonEnd.TimeOfDay)
                        {
                            macro = macro.Replace(tag.Name, afternoonValue);
                        }

                        if (screenCapture.DateTimePreviousCycle.TimeOfDay >= tag.TimeOfDayEveningStart.TimeOfDay &&
                            screenCapture.DateTimePreviousCycle.TimeOfDay <= tag.TimeOfDayEveningEnd.TimeOfDay)
                        {
                            macro = macro.Replace(tag.Name, eveningValue);
                        }

                        // Split the evening start time and evening end time into separate checks if the user wants to extend
                        // the time of what they consider "evening" to also include the next morning.
                        if (tag.EveningExtendsToNextMorning)
                        {
                            DateTime dayStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                            DateTime dayEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

                            if (screenCapture.DateTimePreviousCycle.TimeOfDay >= tag.TimeOfDayEveningStart.TimeOfDay &&
                                screenCapture.DateTimePreviousCycle.TimeOfDay <= dayEnd.TimeOfDay)
                            {
                                macro = macro.Replace(tag.Name, eveningValue);
                            }

                            if (screenCapture.DateTimePreviousCycle.TimeOfDay >= dayStart.TimeOfDay &&
                                screenCapture.DateTimePreviousCycle.TimeOfDay <= tag.TimeOfDayEveningEnd.TimeOfDay)
                            {
                                macro = macro.Replace(tag.Name, eveningValue);
                            }
                        }
                        break;
                }
            }

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