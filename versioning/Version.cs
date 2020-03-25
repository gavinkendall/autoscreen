//-----------------------------------------------------------------------
// <copyright file="Version.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
using System;

namespace AutoScreenCapture
{
    /// <summary>
    /// 
    /// </summary>
    public class Version
    {
        /// <summary>
        /// 
        /// </summary>
        public string Codename { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string VersionString { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int VersionNumber { get; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsCurrentVersion { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appCodename"></param>
        /// <param name="appVersion"></param>
        public Version(string appCodename, string appVersion)
        {
            Codename = appCodename;
            VersionString = appVersion;
            VersionNumber = Convert.ToInt32(appVersion.Replace(".", string.Empty));
            IsCurrentVersion = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appCodename"></param>
        /// <param name="appVersion"></param>
        /// <param name="isCurrentVersion"></param>
        public Version(string appCodename, string appVersion, bool isCurrentVersion)
        {
            Codename = appCodename;
            VersionString = appVersion;
            VersionNumber = Convert.ToInt32(appVersion.Replace(".", string.Empty));
            IsCurrentVersion = isCurrentVersion;
        }
    }
}
