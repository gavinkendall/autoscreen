//-----------------------------------------------------------------------
// <copyright file="VersionManager.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>The version manager figures out if we're handling an old version of the application and stores the old user settings just in case we need to update them to be used with a newer settings schema.</summary>
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
namespace AutoScreenCapture
{
    /// <summary>
    /// A version manager which is responsible for taking care of Auto Screen Capture's versioninng capabilities.
    /// </summary>
    public class VersionManager
    {
        /// <summary>
        /// A collection of Version objects.
        /// </summary>
        public VersionCollection Versions { get; set; }

        /// <summary>
        /// A collection of this application's current user settings.
        /// </summary>
        public SettingCollection CurrentUserSettings { get; set; }

        /// <summary>
        /// A collection of old application settings.
        /// </summary>
        public SettingCollection OldApplicationSettings { get; set; }

        /// <summary>
        /// A collection of old user settings.
        /// </summary>
        public SettingCollection OldUserSettings { get; set; }

        /// <summary>
        /// The constructor of the Version Manager that accepts a version collection and a setting collection.
        /// </summary>
        /// <param name="versions">A collection of Version objects.</param>
        /// <param name="currentUserSettings">A collection of current user settings.</param>
        public VersionManager(VersionCollection versions, SettingCollection currentUserSettings)
        {
            Versions = versions;
            CurrentUserSettings = currentUserSettings;
        }

        /// <summary>
        /// Checks to see if we're loading an XML document from an old version of the application.
        /// This is used by the collections for editors, regions, screens, tags, screenshots, triggers, and settings.
        /// </summary>
        /// <param name="appCodenameToCheck">The codename of the application to check.</param>
        /// <param name="appVersionToCheck">The version of the application to check.</param>
        /// <returns>True if we're handling a file from an old version. False if it's the current version or we don't know what version it is.</returns>
        public bool IsOldAppVersion(string appCodenameToCheck, string appVersionToCheck)
        {
            if (string.IsNullOrEmpty(appCodenameToCheck) && string.IsNullOrEmpty(appVersionToCheck))
            {
                // This is likely to be version 2.1 "Clara" before the concept of an "app:version"
                // or the management of application versions even existed in Auto Screen Capture.
                appCodenameToCheck = Settings.CODENAME_CLARA;
                appVersionToCheck = Settings.CODEVERSION_CLARA;
            }

            if (!string.IsNullOrEmpty(appCodenameToCheck) && !string.IsNullOrEmpty(appVersionToCheck))
            {
                Version versionInConfig = Versions.Get(appCodenameToCheck, appVersionToCheck);
                Version versionHere = Versions.Get(Settings.ApplicationCodename, Settings.ApplicationVersion);

                // All of these were a mistake. They should never have had their revision numbers go beyond 9.
                // So if we find any of these versions then treat them as old versions of the application.
                if (versionInConfig != null &&
                    (versionInConfig.VersionString.Equals("2.2.0.10") ||
                     versionInConfig.VersionString.Equals("2.2.0.11") ||
                     versionInConfig.VersionString.Equals("2.2.0.12") ||
                     versionInConfig.VersionString.Equals("2.2.0.13") ||
                     versionInConfig.VersionString.Equals("2.2.0.14") ||
                     versionInConfig.VersionString.Equals("2.2.0.15") ||
                     versionInConfig.VersionString.Equals("2.2.0.16") ||
                     versionInConfig.VersionString.Equals("2.2.0.17") ||
                     versionInConfig.VersionString.Equals("2.2.0.18") ||
                     versionInConfig.VersionString.Equals("2.2.0.19") ||
                     versionInConfig.VersionString.Equals("2.2.0.20") ||
                     versionInConfig.VersionString.Equals("2.2.0.21") ||
                     versionInConfig.VersionString.Equals("2.2.0.22")))
                {
                    return true;
                }

                // Compare the version number in the config file with the version number of this version.
                // It's a basic numerical comparison so 2203 is going to be less than 2224.
                if (versionInConfig != null && (versionInConfig.VersionNumber < versionHere.VersionNumber))
                {
                    return true;
                }
            }

            // If we're not sure then just say it isn't an old app version to prevent screwing up data.
            return false;
        }
    }
}
