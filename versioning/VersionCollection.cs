//-----------------------------------------------------------------------
// <copyright file="VersionCollection.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public class VersionCollection : IEnumerable<Version>
    {
        private readonly List<Version> _versionList = new List<Version>();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="version"></param>
        public void Add(Version version)
        {
            _versionList.Add(version);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appCodename"></param>
        /// <param name="appVersion"></param>
        /// <returns></returns>
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
