//-----------------------------------------------------------------------
// <copyright file="MacroTagType.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>An enum representing a macro tag type.</summary>
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
    /// A class representing the type of macro tag being used.
    /// </summary>
    public enum MacroTagType
    {
        /// <summary>
        /// A macro tag for getting the name of the screen.
        /// </summary>
        ScreenName = 0,

        /// <summary>
        /// A macro tag for getting the screen number. For example, the third display would return 3 for the screen number.
        /// </summary>
        ScreenNumber = 1,

        /// <summary>
        /// A macro tag for getting the image format of a screenshot.
        /// </summary>
        ImageFormat = 2,

        /// <summary>
        /// A macro tag for getting the current cycle count. A cycle is considered to be a round of screenshots that have been taken.
        /// </summary>
        ScreenCaptureCycleCount = 3,

        /// <summary>
        /// A macro tag for getting the title of the active window.
        /// </summary>
        ActiveWindowTitle = 4,

        /// <summary>
        /// A macro tag for a date/time format.
        /// </summary>
        DateTimeFormat = 5,

        /// <summary>
        /// A macro tag for getting the name of the user.
        /// </summary>
        User = 6,

        /// <summary>
        /// A macro tag for getting the name of the computer the user is using.
        /// </summary>
        Machine = 7,

        /// <summary>
        /// A macro tag for a series of macros that are parsed during specified time ranges.
        /// </summary>
        TimeRange = 8,

        /// <summary>
        /// A macro tag for a date/time expression (such as "{hour-6}" to indicate 6 hours behind).
        /// </summary>
        DateTimeFormatExpression = 9,

        /// <summary>
        /// A macro tag to represent the quarter of the current year.
        /// </summary>
        QuarterYear = 10,

        /// <summary>
        /// A macro tag for the current X value.
        /// </summary>
        X = 11,

        /// <summary>
        /// A macro tag for the current Y value.
        /// </summary>
        Y = 12,

        /// <summary>
        /// A macro tag for the current width value.
        /// </summary>
        Width = 13,

        /// <summary>
        /// A macro tag for the current height value.
        /// </summary>
        Height = 14,

        /// <summary>
        /// A macro tag for the current process name.
        /// </summary>
        Process = 15,

        /// <summary>
        /// The label that is applied to the saved screenshot.
        /// </summary>
        Label = 16,

        /// <summary>
        /// The number of times the user has used "Capture Now".
        /// </summary>
        CountNow = 17
    }
}
