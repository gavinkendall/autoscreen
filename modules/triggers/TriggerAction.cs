//-----------------------------------------------------------------------
// <copyright file="TriggerAction.cs" company="Gavin Kendall">
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
    public class TriggerAction
    {
        /// <summary>
        /// 
        /// </summary>
        public TriggerActionType Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="description"></param>
        public TriggerAction(TriggerActionType type, string description)
        {
            Type = type;
            Description = description;
        }
    }
}