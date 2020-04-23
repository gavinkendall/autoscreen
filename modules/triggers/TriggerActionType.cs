//-----------------------------------------------------------------------
// <copyright file="TriggerActionType.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    /// <summary>
    /// 
    /// </summary>
    public enum TriggerActionType
    {
        /// <summary>
        /// 
        /// </summary>
        ExitApplication = 0,

        /// <summary>
        /// 
        /// </summary>
        HideInterface = 1,

        /// <summary>
        /// 
        /// </summary>
        RunEditor = 2,

        /// <summary>
        /// 
        /// </summary>
        ShowInterface = 3,

        /// <summary>
        /// 
        /// </summary>
        StartScreenCapture = 4,

        /// <summary>
        /// 
        /// </summary>
        StopScreenCapture = 5,

        /// <summary>
        /// 
        /// </summary>
        EmailScreenshot = 6
    }
}