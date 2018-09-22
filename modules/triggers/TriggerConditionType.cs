//-----------------------------------------------------------------------
// <copyright file="TriggerConditionType.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    public enum TriggerConditionType
    {
        ApplicationStartup = 0,
        ApplicationExit = 1,
        InterfaceClosing = 2,
        InterfaceHiding = 3,
        InterfaceShowing = 4,
        LimitReached = 5,
        ScreenCaptureStarted = 6,
        ScreenCaptureStopped = 7,
        ScreenshotTaken = 8
    }
}