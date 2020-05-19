//-----------------------------------------------------------------------
// <copyright file="Trigger.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A class representing a trigger.</summary>
//-----------------------------------------------------------------------
using System;

namespace AutoScreenCapture
{
    /// <summary>
    /// A class representing a trigger.
    /// </summary>
    public class Trigger
    {
        /// <summary>
        /// The name of the trigger.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The condition of the trigger.
        /// </summary>
        public TriggerConditionType ConditionType { get; set; }

        /// <summary>
        /// The action of the trigger to perform based on the condition of the trigger.
        /// </summary>
        public TriggerActionType ActionType { get; set; }

        /// <summary>
        /// The editor associated with the trigger.
        /// </summary>
        public string Editor { get; set; }

        /// <summary>
        /// Determines if the trigger is active or inactive.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// The date value based on the Date/Time condition.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The time value based on either the Date/Time condition or the Time condition.
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// The interval to use. This overrides the main interval.
        /// </summary>
        public int ScreenCaptureInterval { get; set; }

        /// <summary>
        /// The empty constructor for the trigger.
        /// </summary>
        public Trigger()
        {

        }
    }
}