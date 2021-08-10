//-----------------------------------------------------------------------
// <copyright file="Schedule.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2021 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A class representing a schedule.</summary>
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
    /// A class representing a schedule.
    /// </summary>
    public class Schedule
    {
        /// <summary>
        /// The name of the schedule.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Determines if we are taking screenshots only once using CaptureAt.
        /// </summary>
        public bool ModeOneTime { get; set; }

        /// <summary>
        /// Determines if we are taking screenshots between a time range using StartAt and Stop At.
        /// </summary>
        public bool ModePeriod { get; set; }

        /// <summary>
        /// The time when screenshots should be taken only once.
        /// </summary>
        public DateTime CaptureAt { get; set; }

        /// <summary>
        /// The start time of the time range when performing a screen capture in a particular period.
        /// </summary>
        public DateTime StartAt { get; set; }

        /// <summary>
        /// The stop time of the time range when performing a screen capture in a particular period.
        /// </summary>
        public DateTime StopAt { get; set; }

        /// <summary>
        /// The interval to use. This overrides the main interval.
        /// </summary>
        public int ScreenCaptureInterval { get; set; }

        /// <summary>
        /// Determines if we take screenshots on a Monday.
        /// </summary>
        public bool Monday { get; set; }

        /// <summary>
        /// Determines if we take screenshots on a Tuesday.
        /// </summary>
        public bool Tuesday { get; set; }

        /// <summary>
        /// Determines if we take screenshots on a Wednesday.
        /// </summary>
        public bool Wednesday { get; set; }

        /// <summary>
        /// Determines if we take screenshots on a Thursday.
        /// </summary>
        public bool Thursday { get; set; }

        /// <summary>
        /// Determines if we take screenshots on a Friday.
        /// </summary>
        public bool Friday { get; set; }

        /// <summary>
        /// Determines if we take screenshots on a Saturday.
        /// </summary>
        public bool Saturday { get; set; }

        /// <summary>
        /// Determines if we take screenshots on a Sunday.
        /// </summary>
        public bool Sunday { get; set; }

        /// <summary>
        /// Determines if the schedule is enabled or disabled.
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// Notes for the user to write in whatever they need to.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// An empty constructor for a new schedule.
        /// </summary>
        public Schedule()
        {
            Notes = string.Empty;
        }
    }
}
