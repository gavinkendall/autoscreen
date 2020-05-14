//-----------------------------------------------------------------------
// <copyright file="Trigger.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A class representing a trigger.</summary>
//-----------------------------------------------------------------------
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
        /// The empty constructor for the trigger.
        /// </summary>
        public Trigger()
        {
            Active = false;
        }

        /// <summary>
        /// The constructor for the trigger.
        /// </summary>
        /// <param name="name">The name of the trigger.</param>
        /// <param name="conditionType">The condition to check by the trigger in order to perform an action based on this condition.</param>
        /// <param name="actionType">The action to perform based on the condition.</param>
        /// <param name="editor">The editor associated with the trigger.</param>
        /// <param name="active">Determines if the trigger is active or inactive.</param>
        public Trigger(string name, TriggerConditionType conditionType, TriggerActionType actionType, string editor, bool active)
        {
            Name = name;
            ConditionType = conditionType;
            ActionType = actionType;
            Editor = editor;
            Active = active;
        }
    }
}