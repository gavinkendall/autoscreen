//-----------------------------------------------------------------------
// <copyright file="TriggerCondition.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    public class TriggerCondition
    {
        public TriggerCondition(TriggerConditionType type, string description)
        {
            Type = type;
            Description = description;
        }

        public TriggerConditionType Type { get; set; }

        public string Description { get; set; }
    }
}
