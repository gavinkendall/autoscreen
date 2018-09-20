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
        LimitReached = 2,
        ScreenCaptureSessionStarted = 3,
        ScreenCaptureSessionStopped = 4,
        ScreenshotTaken = 5
    }
}