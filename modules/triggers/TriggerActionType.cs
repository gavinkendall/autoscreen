//-----------------------------------------------------------------------
// <copyright file="TriggerActionType.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
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
        /// Enable a chosen screen.
        /// </summary>
        EnableScreen = 8,

        /// <summary>
        /// Enable a chosen region.
        /// </summary>
        EnableRegion = 9,

        /// <summary>
        /// Enable a chosen schedule.
        /// </summary>
        EnableSchedule = 10,

        /// <summary>
        /// Enable a chosen macro tag.
        /// </summary>
        EnableMacroTag = 11,

        /// <summary>
        /// Enable a chosen trigger.
        /// </summary>
        EnableTrigger = 12,

        /// <summary>
        /// Disable a chosen screen.
        /// </summary>
        DisableScreen = 13,

        /// <summary>
        /// Disable a chosen region.
        /// </summary>
        DisableRegion = 14,

        /// <summary>
        /// Disable a chosen schedule.
        /// </summary>
        DisableSchedule = 15,

        /// <summary>
        /// Disable a chosen macro tag.
        /// </summary>
        DisableMacroTag = 16,

        /// <summary>
        /// Disable a chosen trigger.
        /// </summary>
        DisableTrigger = 17,

        /// <summary>
        /// Delete screenshots by days.
        /// </summary>
        DeleteScreenshotsByDays = 18,

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
        SetActiveWindowTitleAsNoMatch = 23,

        /// <summary>
        /// Show the system tray icon.
        /// </summary>
        ShowSystemTrayIcon = 24,

        /// <summary>
        /// Hide the system tray icon.
        /// </summary>
        HideSystemTrayIcon = 25,

        /// <summary>
        /// Take a set of screenshots.
        /// </summary>
        TakeScreenshot = 26,

        /// <summary>
        /// Region Select->Clipboard.
        /// </summary>
        RegionSelectClipboard = 27,

        /// <summary>
        /// Region Select->Clipboard/Auto Save.
        /// </summary>
        RegionSelectClipboardAutoSave = 28,

        /// <summary>
        /// Region Select->Clipboard/Auto Save/Edit.
        /// </summary>
        RegionSelectClipboardAutoSaveEdit = 29,

        /// <summary>
        /// Region Select->Clipboard/Floating Screenshot.
        /// </summary>
        RegionSelectClipboardFloatingScreenshot = 30,

        /// <summary>
        /// Region Select->Floating Screenshot.
        /// </summary>
        RegionSelectFloatingScreenshot = 31,

        /// <summary>
        /// Shows or hides the interface. It's basically a toggle.
        /// </summary>
        ShowOrHideInterface = 32,

        /// <summary>
        /// Starts or stops screen capture.
        /// </summary>
        StartOrStopScreenCapture = 33,

        /// <summary>
        /// Restart the screen capture session.
        /// </summary>
        RestartScreenCapture = 34,

        /// <summary>
        /// Delete screenshots by cycle count.
        /// </summary>
        DeleteScreenshotsByCycleCount = 35,

        /// <summary>
        /// Delete screenshots from the oldest capture cycle.
        /// </summary>
        DeleteScreenshotsFromOldestCaptureCycle = 36
    }
}