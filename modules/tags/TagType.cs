//-----------------------------------------------------------------------
// <copyright file="TagType.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>An enum representing a tag type.</summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    /// <summary>
    /// A class representing a type of macro tag being used.
    /// </summary>
    public enum TagType
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
        /// A macro tag for a "time of day" macro that expands into morning, afternoon, and evening macro values.
        /// </summary>
        TimeOfDay = 8,

        /// <summary>
        /// A macro tag for a date/time expression (such as "{hour-6}" to indicate 6 hours behind).
        /// </summary>
        DateTimeFormatExpression = 9
    }
}
