//-----------------------------------------------------------------------
// <copyright file="VersionCollection.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A collection of versions.</summary>
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
using System.Collections;
using System.Collections.Generic;

namespace AutoScreenCapture
{
    /// <summary>
    /// A collection class to store and manage Version objects.
    /// </summary>
    public class VersionCollection : IEnumerable<Version>
    {
        private readonly List<Version> _versionList;

        /// <summary>
        /// A collection class to store and manage Version objects.
        /// </summary>
        public VersionCollection()
        {
            _versionList = new List<Version>();
        }

        /// <summary>
        /// Returns the enumerator for the collection.
        /// </summary>
        /// <returns>A list of Version objects.</returns>
        public List<Version>.Enumerator GetEnumerator()
        {
            return _versionList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Version>)_versionList).GetEnumerator();
        }

        IEnumerator<Version> IEnumerable<Version>.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Adds a Version object to the collection.
        /// </summary>
        /// <param name="version">The Version object to add.</param>
        public void Add(Version version)
        {
            _versionList.Add(version);
        }

        /// <summary>
        /// Gets a Version object based on an application's codename and version.
        /// </summary>
        /// <param name="appCodename">An application codename.</param>
        /// <param name="appVersion">An application version.</param>
        /// <returns>A Version object.</returns>
        public Version Get(string appCodename, string appVersion)
        {
            if (!string.IsNullOrEmpty(appVersion) &&
                    (appVersion.Equals("2.2.0.10") ||
                     appVersion.Equals("2.2.0.11") ||
                     appVersion.Equals("2.2.0.12") ||
                     appVersion.Equals("2.2.0.13") ||
                     appVersion.Equals("2.2.0.14") ||
                     appVersion.Equals("2.2.0.15") ||
                     appVersion.Equals("2.2.0.16") ||
                     appVersion.Equals("2.2.0.17") ||
                     appVersion.Equals("2.2.0.18") ||
                     appVersion.Equals("2.2.0.19") ||
                     appVersion.Equals("2.2.0.20") ||
                     appVersion.Equals("2.2.0.21") ||
                     appVersion.Equals("2.2.0.22")))
            {
                // If it's any of the "bad" versions then return as 2.2.1.0
                return new Version(Settings.CODENAME_DALEK, "2.2.1.0");
            }

            foreach (Version version in _versionList)
            {
                if (version.Codename.Equals(appCodename) &&
                    version.VersionString.Equals(appVersion))
                {
                    return version;
                }
            }

            // Assume we're handling "Clara" 2.1.8.2 instead of returning null because that will just mess up the upgrade path
            return new Version(Settings.CODENAME_CLARA, Settings.CODEVERSION_CLARA);
        }
    }
}
