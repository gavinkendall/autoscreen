//-----------------------------------------------------------------------
// <copyright file="Schedule.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A class representing a schedule.</summary>
//-----------------------------------------------------------------------
using System;

namespace AutoScreenCapture
{
    /// <summary>
    /// 
    /// </summary>
    public class Schedule
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime StartAt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime StopAt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DayOfWeek Day { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Schedule()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public Schedule(string name, bool enabled)
        {
            Name = name;
            Enabled = enabled;
        }
    }
}
