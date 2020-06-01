//-----------------------------------------------------------------------
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
        public DateTime TimeOfDayMorningStart { get; set; }

        /// <summary>
        /// The end date/time of the morning for the Time of Day tag type.
        /// </summary>
        public DateTime TimeOfDayMorningEnd { get; set; }

        /// <summary>
        /// The start date/time of the afternoon for the Time of Day tag type.
        /// </summary>
        public DateTime TimeOfDayAfternoonStart { get; set; }

        /// <summary>
        /// The end date/time of the afternoon for the Time of Day tag type.
        /// </summary>
        public DateTime TimeOfDayAfternoonEnd { get; set; }

        /// <summary>
        /// The start date/time of the evening for the Time of Day tag type.
        /// </summary>
        public DateTime TimeOfDayEveningStart { get; set; }

        /// <summary>
        /// The end date/time of the evening for the Time of Day tag type.
        /// </summary>
        public DateTime TimeOfDayEveningEnd { get; set; }

        /// <summary>
        /// The morning value for the Time of Day tag type.
        /// </summary>
        public string TimeOfDayMorningValue { get; set; }

        /// <summary>
        /// The afternoon value for the Time of Day tag type.
        /// </summary>
        public string TimeOfDayAfternoonValue { get; set; }

        /// <summary>
        /// The evening value for the Time of Day tag type.
        /// </summary>
        public string TimeOfDayEveningValue { get; set; }

        /// <summary>
        /// Determines if a tag is active or inactive.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Determines if the time in the evening should extend to the early hours of the next morning.
        /// </summary>
        public bool EveningExtendsToNextMorning { get; set; }

        private void SetDefaultValues()
        {
            DateTimeFormatValue = MacroParser.DateFormat + "_" + MacroParser.TimeFormatForWindows;

            // Morning
            TimeOfDayMorningStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0); // 12am
            TimeOfDayMorningEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 11, 59, 59); // 11:59:59am
            TimeOfDayMorningValue = "morning at %hour%-%minute%-%second%";

            // Afternoon
            TimeOfDayAfternoonStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0); // 12pm
            TimeOfDayAfternoonEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 59, 59); // 5:59:59pm
            TimeOfDayAfternoonValue = "afternoon at %hour%-%minute%-%second%";

            // Evening
            TimeOfDayEveningStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 18, 0, 0); // 6pm
            TimeOfDayEveningEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59); // 11:59:59pm
            TimeOfDayEveningValue = "evening at %hour%-%minute%-%second%";

            EveningExtendsToNextMorning = false;

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
        /// <param name="timeOfDayMorningStart">The start time of the morning.</param>
        /// <param name="timeOfDayMorningEnd">The end time of the morning.</param>
        /// <param name="timeOfDayMorningValue">The macro value for the morning.</param>
        /// <param name="timeOfDayAfternoonStart"></param>
        /// <param name="timeOfDayAfternoonEnd"></param>
        /// <param name="timeOfDayAfternoonValue">The macro value for the afternoon.</param>
        /// <param name="timeOfDayEveningStart"></param>
        /// <param name="timeOfDayEveningEnd"></param>
        /// <param name="timeOfDayEveningValue">The macro value for the evening.</param>
        /// <param name="eveningExtendsToNextMorning">Determines if we extend the evening time into the next morning.</param>
        /// <param name="active">Determines the status of the tag (whether it be active or inactive).</param>
        /// <param name="notes"></param>
        public Tag(string name, string description, TagType tagType,
            string dateTimeFormatValue,
            DateTime timeOfDayMorningStart,
            DateTime timeOfDayMorningEnd,
            string timeOfDayMorningValue,
            DateTime timeOfDayAfternoonStart,
            DateTime timeOfDayAfternoonEnd,
            string timeOfDayAfternoonValue,
            DateTime timeOfDayEveningStart,
            DateTime timeOfDayEveningEnd,
            string timeOfDayEveningValue,
            bool eveningExtendsToNextMorning,
            bool active, string notes)
        {
            Name = name;
            Description = description;
            Type = tagType;

            DateTimeFormatValue = dateTimeFormatValue;

            TimeOfDayMorningStart = timeOfDayMorningStart;
            TimeOfDayMorningEnd = timeOfDayMorningEnd;

            TimeOfDayAfternoonStart = timeOfDayAfternoonStart;
            TimeOfDayAfternoonEnd = timeOfDayAfternoonEnd;

            TimeOfDayEveningStart = timeOfDayEveningStart;
            TimeOfDayEveningEnd = timeOfDayEveningEnd;

            TimeOfDayMorningValue = timeOfDayMorningValue;
            TimeOfDayAfternoonValue = timeOfDayAfternoonValue;
            TimeOfDayEveningValue = timeOfDayEveningValue;

            EveningExtendsToNextMorning = eveningExtendsToNextMorning;

            Active = active;

            Notes = notes;
        }
    }
}
