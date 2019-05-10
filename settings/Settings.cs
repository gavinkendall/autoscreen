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
    using System.Xml;

    public static class Settings
    {
        public static readonly string ApplicationName = "Auto Screen Capture";
        public static readonly string ApplicationVersion = "2.2.0.0";
        public static readonly string ApplicationCodename = "Dalek";

        public static SettingCollection Application;
        public static SettingCollection User;

        public static VersionManager VersionManager;
        private static VersionCollection _versionCollection;

        public static void Initialize()
        {
            _versionCollection = new VersionCollection();

            // This version.
            _versionCollection.Add(new Version(ApplicationCodename, ApplicationVersion, isCurrentVersion: true));

            // Older versions should be listed here.
            _versionCollection.Add(new Version("Clara", "2.1.8.2"));

            Application = new SettingCollection();
            Application.Filepath = FileSystem.SettingsFolder + FileSystem.ApplicationSettingsFile;

            User = new SettingCollection();
            User.Filepath = FileSystem.SettingsFolder + FileSystem.UserSettingsFile;

            // Construct the version manager using the version collection and setting collection (containing the user's settings) we just prepared.
            VersionManager = new VersionManager(_versionCollection, User);

            if (!Directory.Exists(FileSystem.SettingsFolder))
            {
                Directory.CreateDirectory(FileSystem.SettingsFolder);
            }

            if (Application != null)
            {
                if (File.Exists(Application.Filepath))
                {
                    Application.Load();

                    Application.GetByKey("Name", defaultValue: Settings.ApplicationName).Value = ApplicationName;
                    Application.GetByKey("Version", defaultValue: Settings.ApplicationVersion).Value = ApplicationVersion;

                    Application.Save();
                }
                else
                {
                    Application.Add(new Setting("Name", ApplicationName));
                    Application.Add(new Setting("Version", ApplicationVersion));
                    Application.Add(new Setting("DebugMode", false));

                    Application.Save();
                }
            }

            if (User != null && !File.Exists(User.Filepath))
            {
                User.Add(new Setting("CaptureLimit", 0));
                User.Add(new Setting("ScreenshotDelay", 60000));
                User.Add(new Setting("CaptureLimitCheck", false));
                User.Add(new Setting("TakeInitialScreenshotCheck", false));
                User.Add(new Setting("ShowSystemTrayIcon", true));
                User.Add(new Setting("CaptureStopAtCheck", false));
                User.Add(new Setting("CaptureStartAtCheck", false));
                User.Add(new Setting("CaptureOnSundayCheck", false));
                User.Add(new Setting("CaptureOnMondayCheck", false));
                User.Add(new Setting("CaptureOnTuesdayCheck", false));
                User.Add(new Setting("CaptureOnWednesdayCheck", false));
                User.Add(new Setting("CaptureOnThursdayCheck", false));
                User.Add(new Setting("CaptureOnFridayCheck", false));
                User.Add(new Setting("CaptureOnSaturdayCheck", false));
                User.Add(new Setting("CaptureOnTheseDaysCheck", false));
                User.Add(new Setting("CaptureStopAtValue", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 0, 0)));
                User.Add(new Setting("CaptureStartAtValue", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0)));
                User.Add(new Setting("LockScreenCaptureSession", false));
                User.Add(new Setting("Passphrase", string.Empty));
                User.Add(new Setting("DeleteScreenshotsOlderThanDays", 0));

                User.Save();
            }
        }
    }
}