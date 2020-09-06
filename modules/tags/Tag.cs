﻿//-----------------------------------------------------------------------
// <copyright file="Tag.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A class representing a macro tag.</summary>
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

namespace AutoScreenCapture
{
    /// <summary>
    /// A class representing a macro tag.
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// The name of the macro tag.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A short description of the macro tag.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Notes for the user to write in whatever they need to.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// The type of tag.
        /// </summary>
        public TagType Type { get; set; }

        /// <summary>
        /// The date/time format value of the tag.
        /// </summary>
        public string DateTimeFormatValue { get; set; }

        /// <summary>
        /// The start date/time of the morning for the Time of Day tag type.
        /// </summary>
        public DateTime TimeRangeMacro1Start { get; set; }

        /// <summary>
        /// The end date/time of the morning for the Time of Day tag type.
        /// </summary>
        public DateTime TimeRangeMacro1End { get; set; }

        /// <summary>
        /// The start date/time of the afternoon for the Time of Day tag type.
        /// </summary>
        public DateTime TimeRangeMacro2Start { get; set; }

        /// <summary>
        /// The end date/time of the afternoon for the Time of Day tag type.
        /// </summary>
        public DateTime TimeRangeMacro2End { get; set; }

        /// <summary>
        /// The start date/time of the evening for the Time of Day tag type.
        /// </summary>
        public DateTime TimeRangeMacro3Start { get; set; }

        /// <summary>
        /// The end date/time of the evening for the Time of Day tag type.
        /// </summary>
        public DateTime TimeRangeMacro3End { get; set; }

        public DateTime TimeRangeMacro4Start { get; set; }
        public DateTime TimeRangeMacro4End { get; set; }

        /// <summary>
        /// The morning value for the Time of Day tag type.
        /// </summary>
        public string TimeRangeMacro1Macro { get; set; }

        /// <summary>
        /// The afternoon value for the Time of Day tag type.
        /// </summary>
        public string TimeRangeMacro2Macro { get; set; }

        /// <summary>
        /// The evening value for the Time of Day tag type.
        /// </summary>
        public string TimeRangeMacro3Macro { get; set; }

        public string TimeRangeMacro4Macro { get; set; }

        /// <summary>
        /// Determines if a tag is active or inactive.
        /// </summary>
        public bool Active { get; set; }

        private void SetDefaultValues()
        {
            DateTimeFormatValue = MacroParser.DateFormat + "_" + MacroParser.TimeFormatForWindows;

            TimeRangeMacro1Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0); // 12am
            TimeRangeMacro1End = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 11, 59, 59); // 11:59:59am
            TimeRangeMacro1Macro = "morning at %hour%-%minute%-%second%";

            TimeRangeMacro2Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0); // 12pm
            TimeRangeMacro2End = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 59, 59); // 5:59:59pm
            TimeRangeMacro2Macro = "afternoon at %hour%-%minute%-%second%";

            TimeRangeMacro3Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 18, 0, 0); // 6pm
            TimeRangeMacro3End = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59); // 11:59:59pm
            TimeRangeMacro3Macro = "evening at %hour%-%minute%-%second%";

            TimeRangeMacro4Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            TimeRangeMacro4End = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            TimeRangeMacro4Macro = string.Empty;

            Active = false;

            Notes = string.Empty;
        }

        /// <summary>
        /// The empty constructor for creating a tag.
        /// </summary>
        public Tag()
        {
            SetDefaultValues();
        }

        /// <summary>
        /// Creates a tag given its name, tag type, and its status (whether it be active or inactive).
        /// </summary>
        /// <param name="name">The name of the tag.</param>
        /// <param name="description">The description of the tag.</param>
        /// <param name="tagType">The type of tag.</param>
        /// <param name="active">The status of the tag.</param>
        public Tag(string name, string description, TagType tagType, bool active)
        {
            SetDefaultValues();

            Name = name;
            Description = description;
            Type = tagType;
            Active = active;
        }

        /// <summary>
        /// Creates a tag given its name, tag type, date/time format value, and its status.
        /// </summary>
        /// <param name="name">The name of the tag.</param>
        /// <param name="description">The description of the tag.</param>
        /// <param name="tagType">The type of tag.</param>
        /// <param name="dateTimeFormatValue">The date/time format value.</param>
        /// <param name="active">The status of the tag.</param>
        public Tag(string name, string description, TagType tagType, string dateTimeFormatValue, bool active)
        {
            SetDefaultValues();

            Name = name;
            Description = description;
            Type = tagType;
            DateTimeFormatValue = dateTimeFormatValue;
            Active = active;
        }

        /// <summary>
        /// Creates a tag given its name, tag type, and "Time of Day" properties.
        /// </summary>
        /// <param name="name">The name of the tag.</param>
        /// <param name="description">The description of the tag.</param>
        /// <param name="tagType">The type of tag.</param>
        /// <param name="dateTimeFormatValue">The date/time format value.</param>
        /// <param name="timeRangeMacro1Start">The start time for Macro 1.</param>
        /// <param name="timeRangeMacro1End">The end time for Macro 1.</param>
        /// <param name="timeRangeMacro1Macro">The macro for Macro1.</param>
        /// <param name="timeRangeMacro2Start">The start time for Macro 2.</param>
        /// <param name="timeRangeMacro2End">The end time for Macro 2.</param>
        /// <param name="timeRangeMacro2Macro">The macro for Macro 2.</param>
        /// <param name="timeRangeMacro3Start">The start time for Macro 3.</param>
        /// <param name="timeRangeMacro3End">The end time for Macro 3.</param>
        /// <param name="timeRangeMacro3Macro">The macro for Macro 3.</param>
        /// <param name="timeRangeMacro4Start">The start time for Macro 4.</param>
        /// <param name="timeRangeMacro4End">The end time for Macro 4.</param>
        /// <param name="timeRangeMacro4Macro">The macro for Macro 4</param>
        /// <param name="active">Determines the status of the tag (whether it be active or inactive).</param>
        /// <param name="notes">Notes.</param>
        public Tag(string name, string description, TagType tagType,
            string dateTimeFormatValue,
            DateTime timeRangeMacro1Start,
            DateTime timeRangeMacro1End,
            string timeRangeMacro1Macro,
            DateTime timeRangeMacro2Start,
            DateTime timeRangeMacro2End,
            string timeRangeMacro2Macro,
            DateTime timeRangeMacro3Start,
            DateTime timeRangeMacro3End,
            string timeRangeMacro3Macro,
            DateTime timeRangeMacro4Start,
            DateTime timeRangeMacro4End,
            string timeRangeMacro4Macro,
            bool active, string notes)
        {
            Name = name;
            Description = description;
            Type = tagType;

            DateTimeFormatValue = dateTimeFormatValue;

            TimeRangeMacro1Start = timeRangeMacro1Start;
            TimeRangeMacro1End = timeRangeMacro1End;

            TimeRangeMacro2Start = timeRangeMacro2Start;
            TimeRangeMacro2End = timeRangeMacro2End;

            TimeRangeMacro3Start = timeRangeMacro3Start;
            TimeRangeMacro3End = timeRangeMacro3End;

            TimeRangeMacro4Start = timeRangeMacro4Start;
            TimeRangeMacro4End = timeRangeMacro4End;

            TimeRangeMacro1Macro = timeRangeMacro1Macro;
            TimeRangeMacro2Macro = timeRangeMacro2Macro;
            TimeRangeMacro3Macro = timeRangeMacro3Macro;
            TimeRangeMacro4Macro = timeRangeMacro4Macro;

            Active = active;

            Notes = notes;
        }
    }
}
