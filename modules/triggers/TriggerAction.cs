//-----------------------------------------------------------------------
// <copyright file="TriggerAction.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
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