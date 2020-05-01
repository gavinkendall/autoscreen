//-----------------------------------------------------------------------
// <copyright file="Version.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
using System;

namespace AutoScreenCapture
{
    /// <summary>
    /// A class representing a version of the application.
    /// </summary>
    public class Version
    {
        /// <summary>
        /// The codename of the application.
        /// </summary>
        public string Codename { get; set; }

        /// <summary>
        /// The version number, represented as a string value, of the application.
        /// </summary>
        public string VersionString { get; set; }

        /// <summary>
        /// The version number, represented as an integer, of the application.
        /// </summary>
        public int VersionNumber { get; }

        /// <summary>
        /// Returns if this version is the current version of the application.
        /// </summary>
        public bool IsCurrentVersion { get; }

        /// <summary>
        /// The constructor for a Version object. Accepts codename and version number (as a string value).
        /// </summary>
        /// <param name="appCodename">The codename of the application.</param>
        /// <param name="appVersion">The version number of the application (as a string value).</param>
        public Version(string appCodename, string appVersion)
        {
            Codename = appCodename;
            VersionString = appVersion;
            VersionNumber = Convert.ToInt32(appVersion.Replace(".", string.Empty));
            IsCurrentVersion = false;
        }

        /// <summary>
        /// The constructor for a Version object. Accepts codename, version number, and if this is the current version.
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
