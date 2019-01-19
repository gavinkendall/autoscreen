//-----------------------------------------------------------------------
// <copyright file="Trigger.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A class representing a trigger.</summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    public class Trigger
    {
        public string Name { get; set; }
        public TriggerConditionType ConditionType { get; set; }
        public TriggerActionType ActionType { get; set; }
        public string Editor { get; set; }

        public Trigger()
        {
        }

        public Trigger(string name, TriggerConditionType conditionType, TriggerActionType actionType, string editor)
        {
            Name = name;
            ConditionType = conditionType;
            ActionType = actionType;
            Editor = editor;
        }
    }
}