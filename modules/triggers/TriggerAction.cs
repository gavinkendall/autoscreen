//-----------------------------------------------------------------------
// <copyright file="TriggerAction.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A trigger action is used to perform a particular action based on a trigger condition.</summary>
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