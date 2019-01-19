//-----------------------------------------------------------------------
// <copyright file="TriggerAction.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    public class TriggerAction
    {
        public TriggerActionType Type { get; set; }
        public string Description { get; set; }

        public TriggerAction(TriggerActionType type, string description)
        {
            Type = type;
            Description = description;
        }
    }
}