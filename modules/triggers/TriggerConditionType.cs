//-----------------------------------------------------------------------
// <copyright file="TriggerConditionType.cs" company="Gavin Kendall">
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
    public enum TriggerConditionType
    {
        /// <summary>
        /// 
        /// </summary>
        ApplicationStartup = 0,

        /// <summary>
        /// 
        /// </summary>
        ApplicationExit = 1,

        /// <summary>
        /// 
        /// </summary>
        InterfaceClosing = 2,

        /// <summary>
        /// 
        /// </summary>
        InterfaceHiding = 3,

        /// <summary>
        /// 
        /// </summary>
        InterfaceShowing = 4,

        /// <summary>
        /// 
        /// </summary>
        LimitReached = 5,

        /// <summary>
        /// 
        /// </summary>
        ScreenCaptureStarted = 6,

        /// <summary>
        /// 
        /// </summary>
        ScreenCaptureStopped = 7,

        /// <summary>
        /// 
        /// </summary>
        ScreenshotTaken = 8
    }
}