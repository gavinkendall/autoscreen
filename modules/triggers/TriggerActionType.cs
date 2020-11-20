//-----------------------------------------------------------------------
// <copyright file="TriggerActionType.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
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
        EmailScreenshot = 6,

        /// <summary>
        /// The action to perform will set the screen capture interval.
        /// </summary>
        SetScreenCaptureInterval = 7,

        /// <summary>
        /// The action to perform will activate a chosen screen.
        /// </summary>
        ActivateScreen = 8,

        /// <summary>
        /// The action to perform will activate a chosen region.
        /// </summary>
        ActivateRegion = 9,

        /// <summary>
        /// The action to perform will activate a chosen schedule.
        /// </summary>
        ActivateSchedule = 10,

        /// <summary>
        /// The action to perform will activate a chosen tag.
        /// </summary>
        ActivateTag = 11,

        /// <summary>
        /// The action to perform will activate a chosen trigger.
        /// </summary>
        ActivateTrigger = 12,

        /// <summary>
        /// The action to perform will deactivate a chosen screen.
        /// </summary>
        DeactivateScreen = 13,

        /// <summary>
        /// The action to perform will deactivate a chosen region.
        /// </summary>
        DeactivateRegion = 14,

        /// <summary>
        /// The action to perform will deactivate a chosen schedule.
        /// </summary>
        DeactivateSchedule = 15,

        /// <summary>
        /// The action to perform will deactivate a chosen tag.
        /// </summary>
        DeactivateTag = 16,

        /// <summary>
        /// The action to perform will deactivate a chosen trigger.
        /// </summary>
        DeactivateTrigger = 17
    }
}