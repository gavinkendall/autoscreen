//-----------------------------------------------------------------------
// <copyright file="Trigger.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2021 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A class representing a trigger.</summary>
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
        /// The day value based on the Day/Time condition.
        /// </summary>
        public string Day { get; set; }

        /// <summary>
        /// The number of days.
        /// </summary>
        public int Days { get; set; }

        /// <summary>
        /// The interval to use. This overrides the main interval.
        /// </summary>
        public int ScreenCaptureInterval { get; set; }

        /// <summary>
        /// This could be any value.
        /// Maybe the name of a Screen, Region, Editor, Schedule, or Macro Tag.
        /// Maybe the label to use when applying a Label.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// The empty constructor for the trigger.
        /// </summary>
        public Trigger()
        {

        }
    }
}