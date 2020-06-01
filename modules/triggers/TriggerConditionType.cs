//-----------------------------------------------------------------------
// <copyright file="TriggerConditionType.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
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
        /// The condition to check will be if the application has been started.
        /// </summary>
        ApplicationStartup = 0,

        /// <summary>
        /// The condition to check will be if the application is about to exit.
        /// </summary>
        ApplicationExit = 1,

        /// <summary>
        /// The condition to check will be if the interface window is closing.
        /// </summary>
        InterfaceClosing = 2,

        /// <summary>
        /// The condition to check will be if the interface is being hidden.
        /// </summary>
        InterfaceHiding = 3,

        /// <summary>
        /// The condition to check will be if the interface is being shown.
        /// </summary>
        InterfaceShowing = 4,

        /// <summary>
        /// The condition to check will be if the number of capture cycles has reached the specified limit.
        /// </summary>
        LimitReached = 5,

        /// <summary>
        /// The condition to check will be if a screen capture session has started.
        /// </summary>
        ScreenCaptureStarted = 6,

        /// <summary>
        /// The condition to check will be if the running screen capture session has stopped.
        /// </summary>
        ScreenCaptureStopped = 7,

        /// <summary>
        /// The condition to check will be if screenshots have been taken.
        /// </summary>
        ScreenshotTaken = 8,

        /// <summary>
        /// The condition to check will be if the date and time match with the specified date and time.
        /// </summary>
        DateTime = 9,

        /// <summary>
        /// The condition to check will be if the time matches with the specified time.
        /// </summary>
        Time = 10
    }
}