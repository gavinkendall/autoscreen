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
        DisablePreview = 0,
        DisableSchedule = 1,
        EnableDebugMode = 2,
        EnablePreview = 3,
        EnableSchedule = 4,
        ExitApplication = 5,
        HideInterface = 6,
        RunEditor = 7,
        ShowInterface = 8,
        StartScreenCapture = 9,
        StopScreenCapture = 10
    }
}