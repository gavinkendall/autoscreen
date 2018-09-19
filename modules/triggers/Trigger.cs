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
        public Trigger()
        {

        }

        public Trigger(string name, TriggerCondition condition, TriggerAction action, string editor)
        {
            Name = name;
            Condition = condition;
            Action = action;
            Editor = editor;
        }

        public string Name { get; set; }

        public TriggerCondition Condition { get; set; }

        public TriggerAction Action { get; set; }

        public string Editor { get; set; }
    }
}
