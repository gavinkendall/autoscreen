//-----------------------------------------------------------------------
// <copyright file="TriggerCondition.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    /// <summary>
    /// 
    /// </summary>
    public class TriggerCondition
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="description"></param>
        public TriggerCondition(TriggerConditionType type, string description)
        {
            Type = type;
            Description = description;
        }

        /// <summary>
        /// 
        /// </summary>
        public TriggerConditionType Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
    }
}