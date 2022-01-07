//-----------------------------------------------------------------------
// <copyright file="MacroParser.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
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
    public class MacroParser
    {
        private Settings _settings;
        private MacroTagExpressionParser _macroTagExpressionParser;

        private readonly string WindowsPathRegexPattern = "^(?<DriveOrUNCPrefix>[A-Z]:\\\\{1}|\\\\{1})(?<Path>.+)$";

        /// <summary>
        /// The screen capture class needed for some of the macro tag parsing.
        /// </summary>
        public ScreenCapture screenCapture;

        /// <summary>
        /// The format for years.
        /// </summary>
        public readonly string YearFormat = "yyyy";

        /// <summary>
        /// The format for months.
        /// </summary>
        public readonly string MonthFormat = "MM";

        /// <summary>
        /// The format for days.
        /// </summary>
        public readonly string DayFormat = "dd";

        /// <summary>
        /// The format for hours.
        /// </summary>
        public readonly string HourFormat = "HH";

        /// <summary>
        /// The format for minutes.
        /// </summary>
        public readonly string MinuteFormat = "mm";

        /// <summary>
        /// The format for seconds.
        /// </summary>
        public readonly string SecondFormat = "ss";

        /// <summary>
        /// The format for milliseconds.
        /// </summary>
        public readonly string MillisecondFormat = "fff";

        /// <summary>
        /// The Macro Parser is responsible for parsing some given text looking for "macro tags" and responding with the appropriate value for each macro tag.
        /// </summary>
        /// <param name="settings"></param>
        public MacroParser(Settings settings)
        {
            _settings = settings;
            _macroTagExpressionParser = new MacroTagExpressionParser();
        }

        /// <summary>
        /// Returns a string representation of a date in the format yyyy-MM-dd
        /// </summary>
        public string DateFormat
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
        public string TimeFormat
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
        public string TimeFormatForTrigger
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
        public string TimeFormatForWindows
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(HourFormat);
                sb.Append("-");
                sb.Append(MinuteFormat);
                sb.Append("-");
                sb.Append(SecondFormat);
                sb.Append(".");
                sb.Append(MillisecondFormat);

                return sb.ToString();
            }
        }

        /// <summary>
        /// The default macro to be assigned to the Macro field for a new Screen or Region.
        /// </summary>
        public readonly string DefaultMacro = @"%date%\%name%\%process% (%date% %time%).%format%";

        /// <summary>
        /// The default folder used for Region Select / Auto Save.
        /// </summary>
        public static readonly string DefaultAutoSaveFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        /// <summary>
        /// The default macro used for Region Select / Auto Save.
        /// </summary>
        public static readonly string DefaultAutoSaveMacro = "Screenshot on %date% at %time%.%format%";

        /// <summary>
        /// Replaces a tag (such as "%year%") with an appropriate value (such as "2020").
        /// </summary>
        /// <param name="preview">Determines if this is a preview of a macro. We either use screen capture date/time or DateTime.Now depending on this boolean.</param>
        /// <param name="name">The name of a region or screen when parsing the %name% tag.</param>
        /// <param name="macro">The macro to parse. A macro usually includes tags such as %count% and %date%.</param>
        /// <param name="screenNumber">The screen number. For example, if this is the second display then the screen number is 2.</param>
        /// <param name="x">The current X value.</param>
        /// <param name="y">The current Y value.</param>
        /// <param name="width">The current Width value.</param>
        /// <param name="height">The current Height value.</param>
        /// <param name="format">The image format to use as an image file extension when parsing the %format% tag.</param>
        /// <param name="activeWindowTitle">The title of the active window.</param>
        /// <param name="processName">The name of the current process.</param>
        /// <param name="label">The label that is applied to the saved screenshot.</param>
        /// <param name="tag">The macro tag to use during parsing.</param>
        /// <returns>A parsed macro containing the appropriate values of respective tags in the provided macro.</returns>
        private string ParseTag(bool preview, string name, string macro, int screenNumber, int x, int y, int width, int height, ImageFormat format, string activeWindowTitle, string processName, string label, MacroTag tag)
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

            if (!tag.Enable)
            {
                return macro;
            }

            switch (tag.Type)
            {
                case MacroTagType.ActiveWindowTitle:
                    macro = macro.Replace(tag.Name, activeWindowTitle);
                    break;

                case MacroTagType.DateTimeFormat:
                    macro = macro.Replace(tag.Name, dt.ToString(tag.DateTimeFormatValue));
                    break;

                case MacroTagType.ImageFormat:
                    macro = format != null && !string.IsNullOrEmpty(format.Name) ? macro.Replace(tag.Name, format.Name.ToLower()) : macro;
                    break;

                case MacroTagType.ScreenCaptureCycleCount:
                    macro = macro.Replace(tag.Name, count.ToString());
                    break;

                case MacroTagType.ScreenName:
                    macro = !string.IsNullOrEmpty(name) ? macro.Replace(tag.Name, name) : macro;
                    break;

                case MacroTagType.ScreenNumber:
                    macro = macro.Replace(tag.Name, screenNumber.ToString());
                    break;

                case MacroTagType.User:
                    macro = macro.Replace(tag.Name, Environment.UserName);
                    break;

                case MacroTagType.Machine:
                    macro = macro.Replace(tag.Name, Environment.MachineName);
                    break;

                case MacroTagType.DateTimeFormatExpression:
                    macro = macro.Replace(tag.Name,
                        _macroTagExpressionParser.ParseTagExpressionForDateTimeFormat(dt, tag.DateTimeFormatValue, this));
                    break;

                case MacroTagType.QuarterYear:
                    macro = macro.Replace(tag.Name, ((dt.Month - 1) / 3 + 1).ToString());
                    break;

                case MacroTagType.X:
                    macro = macro.Replace(tag.Name, x.ToString());
                    break;

                case MacroTagType.Y:
                    macro = macro.Replace(tag.Name, y.ToString());
                    break;

                case MacroTagType.Width:
                    macro = macro.Replace(tag.Name, width.ToString());
                    break;

                case MacroTagType.Height:
                    macro = macro.Replace(tag.Name, height.ToString());
                    break;

                case MacroTagType.Process:
                    macro = macro.Replace(tag.Name, processName);
                    break;

                case MacroTagType.Label:
                    macro = macro.Replace(tag.Name, label);
                    break;
            }

            return StripInvalidWindowsCharacters(macro);
        }

        /// <summary>
        /// Replaces series of tags with appropriate values. This is a simpler method that does a simple parsing of the macro without considering the screen and the active window title.
        /// </summary>
        /// <param name="macro">The macro to parse. A macro usually includes tags such as %count% and %date%.</param>
        /// <param name="macroTagCollection">A collection of macro tags to parse.</param>
        /// <param name="log">The log to use.</param>
        /// <returns>A parsed macro containing the appropriate values of respective tags in the provided macro.</returns>
        public string ParseTags(string macro, MacroTagCollection macroTagCollection, Log log)
        {
            return ParseTags(preview: false, macro, null, null, null, string.Empty, macroTagCollection, log);
        }

        /// <summary>
        /// Replaces series of tags with appropriate values.
        /// </summary>
        /// <param name="preview">Determines if this is a preview of a macro. We either use screen capture date/time or DateTime.Now depending on this boolean.</param>
        /// <param name="macro">The macro to parse. A macro usually includes tags such as %count% and %date%.</param>
        /// <param name="screenOrRegion">The screen or region to use when parsing macro tags.</param>
        /// <param name="activeWindowTitle">The title of the active window.</param>
        /// <param name="processName">The name of the current process.</param>
        /// <param name="label">The label that is applied to the saved screenshot.</param>
        /// <param name="macroTagCollection">A collection of macro tags to parse.</param>
        /// <param name="log">The log to use.</param>
        /// <returns>A parsed macro containing the appropriate values of respective tags in the provided macro.</returns>
        public string ParseTags(bool preview, string macro, object screenOrRegion, string activeWindowTitle, string processName, string label, MacroTagCollection macroTagCollection, Log log)
        {
            string name = string.Empty; // The name of a region or screen when parsing the %name% tag.
            int screenNumber = 0; // The screen number. For example, if this is the second display then the screen number is 2.
            int x = 0;
            int y = 0;
            int width = 0;
            int height = 0;
            ImageFormat format = new ImageFormat("JPEG", ".jpeg"); // The image format to use as an image file extension when parsing the %format% tag.

            if (screenOrRegion != null && screenOrRegion is Screen screen)
            {
                name = screen.Name;
                screenNumber = (screen.Source == 0 && screen.Component == 0) ? 0 : (screen.Component + 1);
                x = screen.X;
                y = screen.Y;
                width = screen.Width;
                height = screen.Height;
                format = screen.Format;
            }

            if (screenOrRegion != null && screenOrRegion is Region region)
            {
                name = region.Name;
                screenNumber = -1;
                x = region.X;
                y = region.Y;
                width = region.Width;
                height = region.Height;
                format = region.Format;
            }

            if (_settings != null && _settings.DefaultSettings != null)
            {
                int activeWindowTitleLengthLimit = Convert.ToInt32(_settings.Application.GetByKey("ActiveWindowTitleLengthLimit", _settings.DefaultSettings.ActiveWindowTitleLengthLimit).Value);

                if (!string.IsNullOrEmpty(activeWindowTitle) && activeWindowTitle.Length > activeWindowTitleLengthLimit)
                {
                    log.WriteMessage($"Active Window title length exceeds the configured length of {activeWindowTitleLengthLimit} characters so value was truncated. Correct the value for the ActiveWindowTitleLengthLimit application setting to prevent truncation");
                    activeWindowTitle = activeWindowTitle.Substring(0, activeWindowTitleLengthLimit);
                }
            }

            foreach (MacroTag tag in macroTagCollection)
            {
                if (tag.Type == MacroTagType.TimeRange)
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

                    string macro1Macro = tag.TimeRangeMacro1Macro;
                    string macro2Macro = tag.TimeRangeMacro2Macro;
                    string macro3Macro = tag.TimeRangeMacro3Macro;
                    string macro4Macro = tag.TimeRangeMacro4Macro;

                    // Temporarily make this tag a DateTimeFormat type because we're going to recursively call ParseTagsForFilePath
                    // for each macro tag in the macro 1, macro 2, macro 3, and macro 4 fields.
                    tag.Type = MacroTagType.DateTimeFormat;

                    // Recursively call the same method we're in to parse each TimeRange macro as if it was a date/time macro tag.
                    macro1Macro = ParseTags(preview, macro1Macro, screenOrRegion, activeWindowTitle, processName, label, macroTagCollection, log);
                    macro2Macro = ParseTags(preview, macro2Macro, screenOrRegion, activeWindowTitle, processName, label, macroTagCollection, log);
                    macro3Macro = ParseTags(preview, macro3Macro, screenOrRegion, activeWindowTitle, processName, label, macroTagCollection, log);
                    macro4Macro = ParseTags(preview, macro4Macro, screenOrRegion, activeWindowTitle, processName, label, macroTagCollection, log);

                    // Now that we have the new parsed values based on date/time macro tags we can set this tag back to its TimeRange type.
                    tag.Type = MacroTagType.TimeRange;

                    if (dt.TimeOfDay >= tag.TimeRangeMacro1Start.TimeOfDay &&
                        dt.TimeOfDay <= tag.TimeRangeMacro1End.TimeOfDay)
                    {
                        macro = macro.Replace(tag.Name, macro1Macro);
                    }

                    if (dt.TimeOfDay >= tag.TimeRangeMacro2Start.TimeOfDay &&
                        dt.TimeOfDay <= tag.TimeRangeMacro2End.TimeOfDay)
                    {
                        macro = macro.Replace(tag.Name, macro2Macro);
                    }

                    if (dt.TimeOfDay >= tag.TimeRangeMacro3Start.TimeOfDay &&
                        dt.TimeOfDay <= tag.TimeRangeMacro3End.TimeOfDay)
                    {
                        macro = macro.Replace(tag.Name, macro3Macro);
                    }

                    if (dt.TimeOfDay >= tag.TimeRangeMacro4Start.TimeOfDay &&
                        dt.TimeOfDay <= tag.TimeRangeMacro4End.TimeOfDay)
                    {
                        macro = macro.Replace(tag.Name, macro4Macro);
                    }
                }
                else
                {
                    macro = ParseTag(preview, name, macro, screenNumber, x, y, width, height, format, activeWindowTitle, processName, label, tag);
                }
            }

            return StripInvalidWindowsCharacters(macro);
        }

        /// <summary>
        /// Removes characters from a given string that are invalid to Windows.
        /// </summary>
        /// <param name="text">The string to parse.</param>
        /// <returns>A string that no longer contains invalid Windows characters.</returns>
        private string StripInvalidWindowsCharacters(string text)
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