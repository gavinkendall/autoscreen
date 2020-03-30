//-----------------------------------------------------------------------
// <copyright file="Trigger.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A class representing a trigger.</summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    /// <summary>
    /// 
    /// </summary>
    public class Trigger
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TriggerConditionType ConditionType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TriggerActionType ActionType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Editor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Trigger()
        {
            Enabled = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="conditionType"></param>
        /// <param name="actionType"></param>
        /// <param name="editor"></param>
        public Trigger(string name, TriggerConditionType conditionType, TriggerActionType actionType, string editor, bool enabled)
        {
            Name = name;
            ConditionType = conditionType;
            ActionType = actionType;
            Editor = editor;
            Enabled = enabled;
        }
    }
}