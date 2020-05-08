//-----------------------------------------------------------------------
// <copyright file="TriggerActionType.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>There are various types of trigger actions that the application can use to perform a particular action based on the action type.</summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    /// <summary>
    /// A class representing a trigger action type.
    /// </summary>
    public enum TriggerActionType
    {
        /// <summary>
        /// The action to perform will exit the application.
        /// </summary>
        ExitApplication = 0,

        /// <summary>
        /// The action to perform will hide the interface.
        /// </summary>
        HideInterface = 1,

        /// <summary>
        /// The action to perform will run a chosen image editor.
        /// </summary>
        RunEditor = 2,

        /// <summary>
        /// The action to perform will show the interface.
        /// </summary>
        ShowInterface = 3,

        /// <summary>
        /// The action to perform will start a screen capture session.
        /// </summary>
        StartScreenCapture = 4,

        /// <summary>
        /// The action to perform will stop the running screen capture session.
        /// </summary>
        StopScreenCapture = 5,

        /// <summary>
        /// The action to perform will email the last screenshots captured.
        /// </summary>
        EmailScreenshot = 6
    }
}