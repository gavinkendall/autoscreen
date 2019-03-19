using System;
using System.Xml;

namespace AutoScreenCapture
{
    using System.IO;

    public static class Settings
    {
        public static readonly string ApplicationName = "Auto Screen Capture";
        public static readonly string ApplicationVersion = "2.2.0.0";
        public static readonly string ApplicationCodename = "Dalek";

        public static SettingCollection Application;
        public static SettingCollection User;

        private static VersionCollection _versionCollection;

        public static void Initialize()
        {
            _versionCollection = new VersionCollection();

            // This version.
            _versionCollection.Add(new Version(ApplicationCodename, ApplicationVersion));

            // Older versions.
            _versionCollection.Add(new Version("Clara", "2.1.8.2"));

            Application = new SettingCollection();
            Application.Filepath = FileSystem.SettingsFolder + FileSystem.ApplicationSettingsFile;

            User = new SettingCollection();
            User.Filepath = FileSystem.SettingsFolder + FileSystem.UserSettingsFile;

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

            if (User != null)
            {
                if (File.Exists(User.Filepath))
                {
                    User.Load();
                }
                else
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

                    User.Save();
                }
            }
        }

        /// <summary>
        /// Determines if we're handling data for an old version of the application.
        /// </summary>
        /// <param name="xDoc">The XML document to check.</param>
        /// <param name="appVersion">The application version to check.</param>
        /// <param name="appCodename">The application codename to check.</param>
        /// <returns></returns>
        public static bool IsOldAppVersion(XmlDocument xDoc, out string appVersion, out string appCodename)
        {
            appVersion = xDoc.SelectSingleNode("/autoscreen").Attributes["app:version"]?.Value;
            appCodename = xDoc.SelectSingleNode("/autoscreen").Attributes["app:codename"]?.Value;

            if (!string.IsNullOrEmpty(appVersion) && !string.IsNullOrEmpty(appCodename))
            {
                Version versionInConfigDocument = _versionCollection.Get(appCodename, appVersion);
                Version versionHere = _versionCollection.Get(ApplicationCodename, ApplicationVersion);

                if (versionInConfigDocument != null &&
                    (versionInConfigDocument.VersionNumber < versionHere.VersionNumber))
                {
                    return true;
                }

                return false;
            }

            // This is likely to be version 2.1 "Clara" before the concept of an "app:version"
            // or the management of application versions even existed in Auto Screen Capture.
            appCodename = "Clara";
            appVersion = "2.1.8.2";

            return true;
        }
    }
}