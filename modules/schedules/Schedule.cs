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
        public bool ModeOneTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ModePeriod { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CaptureAt { get; set; }

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
        public bool Monday { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Tuesday { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Wednesday { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Thursday { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Friday { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Saturday { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Sunday { get; set; }

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
    }
}
