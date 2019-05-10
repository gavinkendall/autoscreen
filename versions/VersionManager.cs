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
        /// 
        /// </summary>
        /// <param name="appCodenameToCheck"></param>
        /// <param name="appVersionToCheck"></param>
        /// <returns></returns>
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

                if (versionInConfig != null &&
                    (versionInConfig.VersionNumber < versionHere.VersionNumber))
                {
                    return true;
                }
            }

            // If we're not sure then just say it isn't an old app version to prevent screwing up data.
            return false;
        }
    }
}
