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
        EnablePreview = 2,
        EnableSchedule = 3,
        ExitApplication = 4,
        HideInterface = 5,
        PlaySlideshow = 6, // This was previously "Enable Debug Mode", but that is now controlled by the application.xml settings file
        RunEditor = 7,
        ShowInterface = 8,
        StartScreenCapture = 9,
        StopScreenCapture = 10
    }
}