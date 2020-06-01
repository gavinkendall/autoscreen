//-----------------------------------------------------------------------
// <copyright file="TriggerAction.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A trigger action is used to perform a particular action based on a trigger condition.</summary>
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
    /// A class representing a trigger action.
    /// </summary>
    public class TriggerAction
    {
        /// <summary>
        /// The type of trigger action.
        /// </summary>
        public TriggerActionType Type { get; set; }

        /// <summary>
        /// The description of the trigger action.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The constructor for the trigger action.
        /// </summary>
        /// <param name="type">The type of the trigger action.</param>
        /// <param name="description">The description of the trigger action. This is a user-friendly version of the trigger action type and can't be changed by the user.</param>
        public TriggerAction(TriggerActionType type, string description)
        {
            Type = type;
            Description = description;
        }
    }
}