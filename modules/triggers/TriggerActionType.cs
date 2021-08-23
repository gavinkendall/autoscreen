//-----------------------------------------------------------------------
// <copyright file="TriggerActionType.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2021 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>There are various types of trigger actions that the application can use to perform a particular action based on the action type.</summary>
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program. If not, see <https://www.gnu.org/licenses/>.
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    /// <summary>
    /// A class representing a trigger action type.
    /// </summary>
    public enum TriggerActionType
    {
        /// <summary>
        /// Exit the application.
        /// </summary>
        ExitApplication = 0,

        /// <summary>
        /// Hide the interface.
        /// </summary>
        HideInterface = 1,

        /// <summary>
        /// Run a chosen image editor.
        /// </summary>
        RunEditor = 2,

        /// <summary>
        /// Show the interface.
        /// </summary>
        ShowInterface = 3,

        /// <summary>
        /// Start a screen capture session.
        /// </summary>
        StartScreenCapture = 4,

        /// <summary>
        /// Stop the running screen capture session.
        /// </summary>
        StopScreenCapture = 5,

        /// <summary>
        /// Email the last screenshots captured.
        /// </summary>
        EmailScreenshot = 6,

        /// <summary>
        /// Set the screen capture interval.
        /// </summary>
        SetScreenCaptureInterval = 7,

        /// <summary>
        /// Activate a chosen screen.
        /// </summary>
        ActivateScreen = 8,

        /// <summary>
        /// Activate a chosen region.
        /// </summary>
        ActivateRegion = 9,

        /// <summary>
        /// Activate a chosen schedule.
        /// </summary>
        ActivateSchedule = 10,

        /// <summary>
        /// Activate a chosen tag.
        /// </summary>
        ActivateTag = 11,

        /// <summary>
        /// Activate a chosen trigger.
        /// </summary>
        ActivateTrigger = 12,

        /// <summary>
        /// Deactivate a chosen screen.
        /// </summary>
        DeactivateScreen = 13,

        /// <summary>
        /// Deactivate a chosen region.
        /// </summary>
        DeactivateRegion = 14,

        /// <summary>
        /// Deactivate a chosen schedule.
        /// </summary>
        DeactivateSchedule = 15,

        /// <summary>
        /// Deactivate a chosen tag.
        /// </summary>
        DeactivateTag = 16,

        /// <summary>
        /// Deactivate a chosen trigger.
        /// </summary>
        DeactivateTrigger = 17,

        /// <summary>
        /// Delete screenshots.
        /// </summary>
        DeleteScreenshots = 18,

        /// <summary>
        /// Set the label with the specific text.
        /// </summary>
        SetLabel = 19,

        /// <summary>
        /// Set the active window title with the specified text and use "Match".
        /// </summary>
        SetActiveWindowTitleAsMatch = 20,

        /// <summary>
        /// Set the application focus with the specified process name.
        /// </summary>
        SetApplicationFocus = 21,

        /// <summary>
        /// Send a screenshot to a file server with SFTP.
        /// </summary>
        FileTransferScreenshot = 22,

        /// <summary>
        /// Set the active window title with the specified text and use "No Match".
        /// </summary>
        SetActiveWindowTitleAsNoMatch = 23
    }
}