//-----------------------------------------------------------------------
// <copyright file="TriggerActionType.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    public enum TriggerActionType
    {
        CloseWindow = 0,
        DisablePreview = 1,
        DisableSchedule = 2,
        EnablePreview = 3,
        EnableSchedule = 4,
        ExitApplication = 5,
        OpenWindow = 6,
        RunEditor = 7,
        StartScreenCapture = 8,
        StopScreenCapture = 9
    }
}