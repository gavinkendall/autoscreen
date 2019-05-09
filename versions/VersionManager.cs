//-----------------------------------------------------------------------
// <copyright file="VersionManager.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    using System.Xml;

    public class VersionManager
    {
        public VersionCollection Versions { get; set; }
        public SettingCollection CurrentUserSettings { get; set; }
        public SettingCollection OldUserSettings { get; set; }

        public VersionManager(VersionCollection versions, SettingCollection currentUserSettings)
        {
            Versions = versions;
            CurrentUserSettings = currentUserSettings;
        }

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

        //public void PrepareUpgradePath(SettingCollection oldUserSettings)
        //{
        //    OldUserSettings = oldUserSettings;
        //}
    }
}
