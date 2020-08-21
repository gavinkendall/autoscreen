//-----------------------------------------------------------------------
// <copyright file="MacroParser.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>Parses text replacing macro tags with the appropriate values.</summary>
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program. If not, see <https://www.gnu.org/licenses/>.
//-----------------------------------------------------------------------
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace AutoScreenCapture
{
    /// <summary>
    /// The Macro Parser is responsible for parsing some given text looking for "macro tags" and responding with the appropriate value for each macro tag.
    /// </summary>
    public static class MacroParser
    {
        private static readonly string WindowsPathRegexPattern = "^(?<DriveOrUNCPrefix>[A-Z]:\\\\{1}|\\\\{1})(?<Path>.+)$";

        /// <summary>
        /// The screen capture class is needed for some of the macro tag parsing.
        /// </summary>
        public static ScreenCapture screenCapture;

        /// <summary>
        /// The format for years.
        /// </summary>
        public static readonly string YearFormat = "yyyy";

        /// <summary>
        /// The format for months.
        /// </summary>
        public static readonly string MonthFormat = "MM";

        /// <summary>
        /// The format for days.
        /// </summary>
        public static readonly string DayFormat = "dd";

        /// <summary>
        /// The format for hours.
        /// </summary>
        public static readonly string HourFormat = "HH";

        /// <summary>
        /// The format for minutes.
        /// </summary>
        public static readonly string MinuteFormat = "mm";

        /// <summary>
        /// The format for seconds.
        /// </summary>
        public static readonly string SecondFormat = "ss";

        /// <summary>
        /// The format for milliseconds.
        /// </summary>
        public static readonly string MillisecondFormat = "fff";

        /// <summary>
        /// Returns a string representation of a date in the format yyyy-MM-dd
        /// </summary>
        public static string DateFormat
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(YearFormat);
                sb.Append("-");
                sb.Append(MonthFormat);
                sb.Append("-");
                sb.Append(DayFormat);

                return sb.ToString();
            }
        }

        /// <summary>
        /// Returns a string representation of a time in the format HH:mm:ss.fff
        /// </summary>
        public static string TimeFormat
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(HourFormat);
                sb.Append(":");
                sb.Append(MinuteFormat);
                sb.Append(":");
                sb.Append(SecondFormat);
                sb.Append(".");
                sb.Append(MillisecondFormat);

                return sb.ToString();
            }
        }

        /// <summary>
        /// Returns a string representation of a time in the format HH:mm:ss
        /// </summary>
        public static string TimeFormatForTrigger
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(HourFormat);
                sb.Append(":");
                sb.Append(MinuteFormat);
                sb.Append(":");
                sb.Append(SecondFormat);

                return sb.ToString();
            }
        }

        /// <summary>
        /// Returns a string representation of a time in the format HH-mm-ss-fff that's safe for filenames in Windows.
        /// </summary>
        public static string TimeFormatForWindows
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(HourFormat);
                sb.Append("-");
                sb.Append(MinuteFormat);
                sb.Append("-");
                sb.Append(SecondFormat);
                sb.Append("-");
                sb.Append(MillisecondFormat);

                return sb.ToString();
            }
        }

        /// <summary>
        /// The default macro to be assigned to the Macro field for a new Screen or Region.
        /// </summary>
        public static readonly string DefaultMacro = @"%date%\%name%\%date%_%time%.%format%";

        /// <summary>
        /// The default folder used for Region Select / Auto Save.
        /// </summary>
        public static readonly string DefaultAutoSaveFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        /// <summary>
        /// The default macro used for Region Select / Auto Save.
        /// </summary>
        public static readonly string DefaultAutoSaveMacro = "%date%_%time%.%format%";

        /// <summary>
        /// Replaces a tag (such as "%year%") with an appropriate value (such as "2020").
        /// </summary>
        /// <param name="preview">Determines if this is a preview of a macro. We either use screen capture date/time or DateTime.Now depending on this boolean.</param>
        /// <param name="name">The name of a region or screen when parsing the %name% tag.</param>
        /// <param name="macro">The macro to parse. A macro usually includes tags such as %count% and %date%.</param>
        /// <param name="screenNumber">The screen number. For example, if this is the second display then the screen number is 2.</param>
        /// <param name="format">The image format to use as an image file extension when parsing the %format% tag.</param>
        /// <param name="activeWindowTitle">The title of the active window.</param>
        /// <param name="tag">The macro tag to use during parsing.</param>
        /// <returns>A parsed macro containing the appropriate values of respective tags in the provided macro.</returns>
        private static string ParseTag(bool preview, string name, string macro, int screenNumber, ImageFormat format, string activeWindowTitle, Tag tag)
        {
            int count;
            DateTime dt;

            if (preview || screenCapture == null)
            {
                count = 1;
                dt = DateTime.Now;
            }
            else
            {
                count = screenCapture.Count;
                dt = screenCapture.DateTimeScreenshotsTaken;
            }

            // Strip out any backslash characters from the name so we avoid creating unnecessary directories based on the name.
            name = name.Replace("\\", string.Empty);

            if (!tag.Active)
            {
                return macro;
            }

            switch (tag.Type)
            {
                case TagType.ActiveWindowTitle:
                    macro = macro.Replace(tag.Name, activeWindowTitle);
                    break;

                case TagType.DateTimeFormat:
                    macro = macro.Replace(tag.Name, dt.ToString(tag.DateTimeFormatValue));
                    break;

                case TagType.ImageFormat:
                    macro = format != null && !string.IsNullOrEmpty(format.Name) ? macro.Replace(tag.Name, format.Name.ToLower()) : macro;
                    break;

                case TagType.ScreenCaptureCycleCount:
                    macro = macro.Replace(tag.Name, count.ToString());
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

                case TagType.DateTimeFormatExpression:
                    macro = macro.Replace(tag.Name,
                        MacroTagExpressionParser.ParseTagExpressionForDateTimeFormat(dt, tag.DateTimeFormatValue));
                    break;
            }

            return StripInvalidWindowsCharacters(macro);
        }

        /// <summary>
        /// Replaces series of tags with appropriate values.
        /// </summary>
        /// <param name="preview">Determines if this is a preview of a macro. We either use screen capture date/time or DateTime.Now depending on this boolean.</param>
        /// <param name="config">Determines if we are parsing tags from the config file or not.</param>
        /// <param name="name">The name of a region or screen when parsing the %name% tag.</param>
        /// <param name="macro">The macro to parse. A macro usually includes tags such as %count% and %date%.</param>
        /// <param name="screenNumber">The screen number. For example, if this is the second display then the screen number is 2.</param>
        /// <param name="format">The image format to use as an image file extension when parsing the %format% tag.</param>
        /// <param name="activeWindowTitle">The title of the active window.</param>
        /// <param name="tagCollection">A collection of macro tags to parse.</param>
        /// <returns>A parsed macro containing the appropriate values of respective tags in the provided macro.</returns>
        public static string ParseTags(bool preview, bool config, string name, string macro, int screenNumber, ImageFormat format, string activeWindowTitle, TagCollection tagCollection)
        {
            if (!config)
            {
                int activeWindowTitleLengthLimit = Convert.ToInt32(Settings.Application.GetByKey("ActiveWindowTitleLengthLimit", DefaultSettings.ActiveWindowTitleLengthLimit).Value);

                if (!string.IsNullOrEmpty(activeWindowTitle) && activeWindowTitle.Length > activeWindowTitleLengthLimit)
                {
                    Log.WriteMessage($"Active Window title length exceeds the configured length of {activeWindowTitleLengthLimit} characters so value was truncated. Correct the value for the ActiveWindowTitleLengthLimit application setting to prevent truncation");
                    activeWindowTitle = activeWindowTitle.Substring(0, activeWindowTitleLengthLimit);
                }
            }

            foreach (Tag tag in tagCollection)
            {
                if (tag.Type == TagType.TimeOfDay)
                {
                    DateTime dt;

                    if (preview || screenCapture == null)
                    {
                        dt = DateTime.Now;
                    }
                    else
                    {
                        dt = screenCapture.DateTimeScreenshotsTaken;
                    }

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
                    morningValue = ParseTags(preview, config, name, morningValue, screenNumber, format, activeWindowTitle, tagCollection);
                    afternoonValue = ParseTags(preview, config, name, afternoonValue, screenNumber, format, activeWindowTitle, tagCollection);
                    eveningValue = ParseTags(preview, config, name, eveningValue, screenNumber, format, activeWindowTitle, tagCollection);

                    // Now that we have the new parsed values based on date/time macro tags we can set this tag back to its TimeOfDay type.
                    tag.Type = TagType.TimeOfDay;

                    if (dt.TimeOfDay >= tag.TimeOfDayMorningStart.TimeOfDay &&
                        dt.TimeOfDay <= tag.TimeOfDayMorningEnd.TimeOfDay)
                    {
                        macro = macro.Replace(tag.Name, morningValue);
                    }

                    if (dt.TimeOfDay >= tag.TimeOfDayAfternoonStart.TimeOfDay &&
                        dt.TimeOfDay <= tag.TimeOfDayAfternoonEnd.TimeOfDay)
                    {
                        macro = macro.Replace(tag.Name, afternoonValue);
                    }

                    if (dt.TimeOfDay >= tag.TimeOfDayEveningStart.TimeOfDay &&
                        dt.TimeOfDay <= tag.TimeOfDayEveningEnd.TimeOfDay)
                    {
                        macro = macro.Replace(tag.Name, eveningValue);
                    }

                    // Split the evening start time and evening end time into separate checks if the user wants to extend
                    // the time of what they consider "evening" to also include the next morning.
                    if (tag.EveningExtendsToNextMorning)
                    {
                        DateTime dayStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                        DateTime dayEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

                        if (dt.TimeOfDay >= tag.TimeOfDayEveningStart.TimeOfDay &&
                            dt.TimeOfDay <= dayEnd.TimeOfDay)
                        {
                            macro = macro.Replace(tag.Name, eveningValue);
                        }

                        if (dt.TimeOfDay >= dayStart.TimeOfDay &&
                            dt.TimeOfDay <= tag.TimeOfDayEveningEnd.TimeOfDay)
                        {
                            macro = macro.Replace(tag.Name, eveningValue);
                        }
                    }
                }
                else
                {
                    macro = ParseTag(preview, name, macro, screenNumber, format, activeWindowTitle, tag);
                }
            }

            return StripInvalidWindowsCharacters(macro);
        }

        /// <summary>
        /// Replaces a series of tags with appropriate values. Assumes you are either parsing a folder path or just need a preview of the returned values.
        /// </summary>
        /// <param name="config">Determines if we are parsing tags from the config file or not.</param>
        /// <param name="macro">The macro to parse. A macro usually includes tags such as %count% and %date%.</param>
        /// <param name="tagCollection">A collection of macro tags to parse.</param>
        /// <returns>A parsed macro containing the appropriate values of respective tags in the provided macro.</returns>
        public static string ParseTags(bool config, string macro, TagCollection tagCollection)
        {
            return ParseTags(preview: true, config, string.Empty, macro, 0,
                new ImageFormat(ImageFormatSpec.NAME_JPEG, ImageFormatSpec.EXTENSION_JPEG), string.Empty, tagCollection);
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