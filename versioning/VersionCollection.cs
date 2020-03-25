//-----------------------------------------------------------------------
// <copyright file="VersionCollection.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
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
        private readonly List<Version> _versionList = new List<Version>();

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
            foreach (Version version in _versionList)
            {
                if (version.Codename.Equals(appCodename) &&
                    version.VersionString.Equals(appVersion))
                {
                    return version;
                }
            }

            return null;
        }
    }
}
