//-----------------------------------------------------------------------
// <copyright file="Version.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2023 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>This class is used to represent a certain version of Auto Screen Capture so we can check on an old version and then update the old data to a newer data structure if it's required.</summary>
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
        /// The constructor for a Version object. Accepts codename, version number (as a string value), and if this is the current version.
        /// </summary>
        /// <param name="appCodename"></param>
        /// <param name="appVersion"></param>
        /// <param name="isCurrentVersion"></param>
        public Version(string appCodename, string appVersion, bool isCurrentVersion = false)
        {
            if (string.IsNullOrEmpty(appCodename) && string.IsNullOrEmpty(appVersion))
            {
                appCodename = Settings.CODENAME_CLARA;
                appVersion = Settings.CODEVERSION_CLARA;
            }

            Codename = appCodename;
            VersionString = appVersion;
            VersionNumber = Convert.ToInt32(appVersion.Replace(".", string.Empty));
            IsCurrentVersion = isCurrentVersion;
        }
    }
}
