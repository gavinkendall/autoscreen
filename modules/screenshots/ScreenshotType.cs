//-----------------------------------------------------------------------
// <copyright file="ScreenshotType.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>The type of screenshot can either represent the active window, a region, or a screen.</summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    /// <summary>
    /// A class representing a type of screenshot.
    /// </summary>
    public enum ScreenshotType
    {
        /// <summary>
        /// The type of screenshot represents the active window.
        /// </summary>
        ActiveWindow = 0,

        /// <summary>
        /// The type of screenshot represents a region.
        /// </summary>
        Region = 1,

        /// <summary>
        /// The type of screenshot represents a screen.
        /// </summary>
        Screen = 2
    }
}
