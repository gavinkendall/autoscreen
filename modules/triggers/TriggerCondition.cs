//-----------------------------------------------------------------------
// <copyright file="TriggerCondition.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A trigger condition is an event that occurs when the application is in a particular state (such as if the interface window is closing or the running screen capture session is being stopped).</summary>
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
namespace AutoScreenCapture
{
    /// <summary>
    /// A class representing a trigger condition.
    /// </summary>
    public class TriggerCondition
    {
        /// <summary>
        /// The type of trigger condition.
        /// </summary>
        public TriggerConditionType Type { get; set; }

        /// <summary>
        /// The description of the trigger condition.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The constructor for the trigger condition.
        /// </summary>
        /// <param name="type">The type of the trigger condition.</param>
        /// <param name="description">The description of the trigger condition. This is a user-friendly version of the trigger condition type and can't be changed by the user.</param>
        public TriggerCondition(TriggerConditionType type, string description)
        {
            Type = type;
            Description = description;
        }
    }
}