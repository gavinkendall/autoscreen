//-----------------------------------------------------------------------
// <copyright file="TriggerConditionType.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2023 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>All the types of conditions that can occur (such as when a screen capture session is being started).</summary>
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
    /// A class representing a trigger condition type.
    /// </summary>
    public enum TriggerConditionType
    {
        /// <summary>
        /// When the application has been started.
        /// </summary>
        ApplicationStartup = 0,

        /// <summary>
        /// When the application is about to exit.
        /// </summary>
        ApplicationExit = 1,

        /// <summary>
        /// When the interface window is closing.
        /// </summary>
        InterfaceClosing = 2,

        /// <summary>
        /// When the interface is being hidden.
        /// </summary>
        InterfaceHiding = 3,

        /// <summary>
        /// When the interface is being shown.
        /// </summary>
        InterfaceShowing = 4,

        /// <summary>
        /// When the number of capture cycles has reached the specified limit.
        /// </summary>
        LimitReached = 5,

        /// <summary>
        /// When a screen capture session has started.
        /// </summary>
        ScreenCaptureStarted = 6,

        /// <summary>
        /// When the running screen capture session has stopped.
        /// </summary>
        ScreenCaptureStopped = 7,

        /// <summary>
        /// When a screenshot was taken successfully.
        /// </summary>
        AfterScreenshotTaken = 8,

        /// <summary>
        /// When the current date and time match with the specified date and time.
        /// </summary>
        DateTime = 9,

        /// <summary>
        /// When the current time matches with the specified time.
        /// </summary>
        Time = 10,

        /// <summary>
        /// When the current day and time matches with the specified day and time.
        /// </summary>
        DayTime = 11,

        /// <summary>
        /// When a screenshot is about to be taken.
        /// </summary>
        BeforeScreenshotTaken = 12,

        /// <summary>
        /// When screenshot references are about to be saved.
        /// </summary>
        BeforeScreenshotReferencesSaved = 13,

        /// <summary>
        /// When screenshot references are saved.
        /// </summary>
        AfterScreenshotReferencesSaved = 14,

        /// <summary>
        /// When the system tray icon is double-clicked.
        /// </summary>
        SystemTrayIconDoubleClick = 15,

        /// <summary>
        /// When the capture cycle elapses.
        /// </summary>
        CaptureCycleElapsed = 16,

        /// <summary>
        /// When the duration from Start Screen Capture has been reached.
        /// </summary>
        DurationFromStartScreenCapture = 17,

        /// <summary>
        /// When the duration from Stop Screen Capture has been reached.
        /// </summary>
        DurationFromStopScreenCapture = 18
    }
}