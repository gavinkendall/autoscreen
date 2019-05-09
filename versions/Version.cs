//-----------------------------------------------------------------------
// <copyright file="Version.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    using System;

    public class Version
    {
        public string Codename { get; set; }

        public string VersionString { get; set; }

        public int VersionNumber { get; }

        public bool IsCurrentVersion { get; }

        public Version(string appCodename, string appVersion)
        {
            Codename = appCodename;
            VersionString = appVersion;
            VersionNumber = Convert.ToInt32(appVersion.Replace(".", string.Empty));
            IsCurrentVersion = false;
        }

        public Version(string appCodename, string appVersion, bool isCurrentVersion)
        {
            Codename = appCodename;
            VersionString = appVersion;
            VersionNumber = Convert.ToInt32(appVersion.Replace(".", string.Empty));
            IsCurrentVersion = isCurrentVersion;
        }
    }
}
