//-----------------------------------------------------------------------
// <copyright file="HelpTip.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    /// <summary>
    /// This class is used for passing a help tip message from any class that doesn't have access to the help bar.
    /// </summary>
    public static class HelpTip
    {
        /// <summary>
        /// Any message to be displayed in the help bar.
        /// </summary>
        public static string Message { get; set; }
    }
}
