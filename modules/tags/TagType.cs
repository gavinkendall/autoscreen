//-----------------------------------------------------------------------
// <copyright file="TagType.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>An enum representing a tag type.</summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    /// <summary>
    /// 
    /// </summary>
    public enum TagType
    {
        /// <summary>
        /// 
        /// </summary>
        ScreenName = 0,

        /// <summary>
        /// 
        /// </summary>
        ScreenNumber = 1,

        /// <summary>
        /// 
        /// </summary>
        ImageFormat = 2,

        /// <summary>
        /// 
        /// </summary>
        ScreenCaptureCycleCount = 3,

        /// <summary>
        /// 
        /// </summary>
        ActiveWindowTitle = 4,

        /// <summary>
        /// 
        /// </summary>
        DateTimeFormat = 5,

        /// <summary>
        /// 
        /// </summary>
        User = 6,

        /// <summary>
        /// 
        /// </summary>
        Machine = 7,

        /// <summary>
        /// 
        /// </summary>
        TimeOfDay = 8,

        /// <summary>
        /// A date/time expression (such as "{hour-6}" to indicate 6 hours behind).
        /// </summary>
        DateTimeFormatExpression = 9
    }
}
