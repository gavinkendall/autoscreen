//-----------------------------------------------------------------------
// <copyright file="Settings.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    using System;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// 
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly string ApplicationName = "Auto Screen Capture";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string ApplicationVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        /// <summary>
        /// 
        /// </summary>
        public static readonly string ApplicationCodename = "Dalek";

        /// <summary>
        /// 
        /// </summary>
        public static SettingCollection Application;

        /// <summary>
        /// 
        /// </summary>
        public static SettingCollection User;

        /// <summary>
        /// 
        /// </summary>
        public static VersionManager VersionManager;

        /// <summary>
        /// 
        /// </summary>
        private static VersionCollection _versionCollection;

        private const string CODENAME_CLARA = "Clara";
        private const string CODENAME_DALEK = "Dalek";

        /// <summary>
        /// 
        /// </summary>
        public static void Initialize()
        {
            _versionCollection = new VersionCollection();

            // This version.
            _versionCollection.Add(new Version(ApplicationCodename, ApplicationVersion, isCurrentVersion: true));

            // Older versions should be listed here.
            // Never have any of the individual numbers go beyond 9 into double digits unless you want to break the upgrade system!
            _versionCollection.Add(new Version(CODENAME_CLARA, "2.1.8.2")); // Last version that introduced the Macro concept
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.0.0")); // Support for unlimited number of screens
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.0.1")); // Fixed empty window title bug
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.0.2")); // Continue screen capture session when drive not available
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.0.3")); // Changes to how we save screenshots
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.0.4")); // More changes to how we save screenshots
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.0.5")); // Fixes the changes to how we save screenshots
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.0.6")); // Can now select an existing label when applying a label
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.0.7")); // Fixed upgrade path from old versions. Can now filter by Process Name
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.0.8")); // Introduced %user% and %machine% macro tags
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.0.9")); // Fixed upgrade path from older versions
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.1.0")); // Introduced autoscreen.conf file and -log, -debug, and -config command line arguments
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.1.1")); // Can now add an Editor with any extension (not just limited to *.exe)
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.1.2")); // Fixed -passphrase command line argument so it saves when run from the command line
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.2.0")); // Emailing screenshots is now a feature
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.2.1")); // Fixed bugs with autoscreen.conf and ExitOnError
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.2.2")); // Fixed issue with passphrase being accidentally hashed during upgrade
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.2.3")); // Changed default location of autoscreen.conf to be at the root level of autoscreen.exe
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.2.4")); // Macro tags %user% and %machine% can now be used in folder paths and the paths used by the autoscreen.conf file

            Application = new SettingCollection
            {
                Filepath = FileSystem.ApplicationSettingsFile
            };

            User = new SettingCollection
            {
                Filepath = FileSystem.UserSettingsFile
            };

            // Construct the version manager using the version collection and setting collection (containing the user's settings) we just prepared.
            VersionManager = new VersionManager(_versionCollection, User);

            if (Application != null && !string.IsNullOrEmpty(Application.Filepath))
            {
                if (File.Exists(Application.Filepath))
                {
                    Application.Load();

                    Application.GetByKey("Name", defaultValue: Settings.ApplicationName).Value = ApplicationName;
                    Application.GetByKey("Version", defaultValue: Settings.ApplicationVersion).Value = ApplicationVersion;

                    if (!Application.KeyExists("DebugMode"))
                    {
                        Application.Add(new Setting("DebugMode", false));
                    }

                    if (!Application.KeyExists("ExitOnError"))
                    {
                        Application.Add(new Setting("ExitOnError", false));
                    }

                    if (!Application.KeyExists("Logging"))
                    {
                        Application.Add(new Setting("Logging", false));
                    }

                    if (!Application.KeyExists("EmailServerHost"))
                    {
                        Application.Add(new Setting("EmailServerHost", string.Empty));
                    }

                    if (!Application.KeyExists("EmailServerPort"))
                    {
                        Application.Add(new Setting("EmailServerPort", 587));
                    }

                    if (!Application.KeyExists("EmailServerEnableSSL"))
                    {
                        Application.Add(new Setting("EmailServerEnableSSL", true));
                    }

                    if (!Application.KeyExists("EmailClientUsername"))
                    {
                        Application.Add(new Setting("EmailClientUsername", string.Empty));
                    }

                    if (!Application.KeyExists("EmailClientPassword"))
                    {
                        Application.Add(new Setting("EmailClientPassword", string.Empty));
                    }

                    if (!Application.KeyExists("EmailMessageFrom"))
                    {
                        Application.Add(new Setting("EmailMessageFrom", string.Empty));
                    }

                    if (!Application.KeyExists("EmailMessageTo"))
                    {
                        Application.Add(new Setting("EmailMessageTo", string.Empty));
                    }

                    if (!Application.KeyExists("EmailMessageCC"))
                    {
                        Application.Add(new Setting("EmailMessageCC", string.Empty));
                    }

                    if (!Application.KeyExists("EmailMessageBCC"))
                    {
                        Application.Add(new Setting("EmailMessageBCC", string.Empty));
                    }

                    if (!Application.KeyExists("EmailMessageSubject"))
                    {
                        Application.Add(new Setting("EmailMessageSubject", string.Empty));
                    }

                    if (!Application.KeyExists("EmailMessageBody"))
                    {
                        Application.Add(new Setting("EmailMessageBody", string.Empty));
                    }
                }
                else
                {
                    Application.Add(new Setting("Name", ApplicationName));
                    Application.Add(new Setting("Version", ApplicationVersion));
                    Application.Add(new Setting("DebugMode", false));
                    Application.Add(new Setting("Logging", false));
                    Application.Add(new Setting("EmailServerHost", string.Empty));
                    Application.Add(new Setting("EmailServerPort", 587));
                    Application.Add(new Setting("EmailServerEnableSSL", true));
                    Application.Add(new Setting("EmailClientUsername", string.Empty));
                    Application.Add(new Setting("EmailClientPassword", string.Empty));
                    Application.Add(new Setting("EmailMessageFrom", string.Empty));
                    Application.Add(new Setting("EmailMessageTo", string.Empty));
                    Application.Add(new Setting("EmailMessageCC", string.Empty));
                    Application.Add(new Setting("EmailMessageBCC", string.Empty));
                    Application.Add(new Setting("EmailMessageSubject", string.Empty));
                    Application.Add(new Setting("EmailMessageBody", string.Empty));
                }

                Application.Save();
            }

            if (User != null && !string.IsNullOrEmpty(User.Filepath) && !File.Exists(User.Filepath))
            {
                User.Add(new Setting("IntScreenCaptureInterval", 60000));
                User.Add(new Setting("IntCaptureLimit", 0));
                User.Add(new Setting("BoolCaptureLimit", false));
                User.Add(new Setting("BoolTakeInitialScreenshot", false));
                User.Add(new Setting("BoolShowSystemTrayIcon", true));
                User.Add(new Setting("BoolCaptureStopAt", false));
                User.Add(new Setting("BoolCaptureStartAt", false));
                User.Add(new Setting("BoolCaptureOnSunday", false));
                User.Add(new Setting("BoolCaptureOnMonday", false));
                User.Add(new Setting("BoolCaptureOnTuesday", false));
                User.Add(new Setting("BoolCaptureOnWednesday", false));
                User.Add(new Setting("BoolCaptureOnThursday", false));
                User.Add(new Setting("BoolCaptureOnFriday", false));
                User.Add(new Setting("BoolCaptureOnSaturday", false));
                User.Add(new Setting("BoolCaptureOnTheseDays", false));
                User.Add(new Setting("DateTimeCaptureStopAtValue", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 0, 0)));
                User.Add(new Setting("DateTimeCaptureStartAtValue", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0)));
                User.Add(new Setting("StringPassphrase", string.Empty));
                User.Add(new Setting("IntKeepScreenshotsForDays", 30));
                User.Add(new Setting("StringScreenshotLabel", string.Empty));
                User.Add(new Setting("BoolApplyScreenshotLabel", false));

                User.Save();
            }

            Log.DebugMode = Convert.ToBoolean(Application.GetByKey("DebugMode", defaultValue: false).Value);
            Log.Enabled = Convert.ToBoolean(Application.GetByKey("Logging", defaultValue: false).Value);
        }
    }
}