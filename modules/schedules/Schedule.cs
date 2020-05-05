//-----------------------------------------------------------------------
// <copyright file="Schedule.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A class representing a schedule.</summary>
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
        public bool Enabled { get; set; }

        /// <summary>
        /// An empty constructor for a new schedule.
        /// </summary>
        public Schedule()
        {

        }
    }
}
