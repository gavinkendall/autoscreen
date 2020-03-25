//-----------------------------------------------------------------------
// <copyright file="VersionManager.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    /// <summary>
    /// 
    /// </summary>
    public class VersionManager
    {
        /// <summary>
        /// 
        /// </summary>
        public VersionCollection Versions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SettingCollection CurrentUserSettings { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SettingCollection OldUserSettings { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="versions"></param>
        /// <param name="currentUserSettings"></param>
        public VersionManager(VersionCollection versions, SettingCollection currentUserSettings)
        {
            Versions = versions;
            CurrentUserSettings = currentUserSettings;
        }

        /// <summary>
        /// Checks to see if we're loading an XML file from an old version of the application.
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
                appCodenameToCheck = "Clara";
                appVersionToCheck = "2.1.8.2";
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
