//-----------------------------------------------------------------------
// <copyright file="Settings.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
using System;
using System.IO;
using System.Reflection;

namespace AutoScreenCapture
{
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
            // WARNING: Never have any of the individual numbers go beyond 9 unless you want to break the upgrade system! This was an issue introduced in 2.2.0.10.
            _versionCollection.Add(new Version(CODENAME_CLARA, "2.1.8.2")); // "Clara"; the last version in the 2.1 "macro" series. This includes the large user.xml file fix (2.1.7.9), "the handle is invalid" fix when Windows is locked (2.1.8.0), and the removal of the "start when Windows starts" feature (2.1.8.1); because anti-virus software falsely flags the application as a virus if we do that.
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.0.0")); // "Dalek"; support for unlimited number of screens. This was a huge release with the most commits ever in the application's history.
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.0.1")); // Fixed bug with empty window title which resulted in image files remaining after cleanup.
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.0.2")); // Application no longer stops current screen capture session if the directory root does not exist or the drive is not ready. This ensures that the current screen capture session will continue even if the drive being referenced is not available for some reason. Useful if you usually save screen images to an external drive but then disconnect from it and want to continue using the laptop's internal drive. Reintroduced the thread for saving screenshots and the lock on xDoc
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.0.3")); // Active window title is retrieved earlier for less chance in having different titles in different screenshot objects despite being in the same screen capture cycle. Some code cleanup. Documentation being added. Fixed a racing condition issue with KeepScreenshotsForDays and Save in ScreenshotCollection
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.0.4")); // Expanded scope of lock around screenshot list
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.0.5")); // Old screenshots are deleted and unsaved screenshots are saved within the same timer
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.0.6")); // An existing label can now be selected from a drop down list of available labels and applying a label to each screenshot is now determined by a checkbox value
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.0.7")); // Process Name introduced. Can now filter by an application's process name. Fixed upgrade path from old versions. Removed -debug command line argument. ScreenCapture Count is now reduced whenever there is no image available to capture or active window title is empty to hopefully make the count more accurate when using the count tag. Logging removed in TakeScreenshot and ScreenCapture Save methods to save on disk space when DebugMode enabled
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.0.8")); // New macro tags for getting the name of the currently logged in user and the name of the machine being used
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.0.9")); // Fixed upgrade path from older versions
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.0.10")); // Fixed bug with count value when display is not available
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.0.11")); // The %screen% tag has been re-introduced to represent the screen number. Fixed bug with taskbar not being captured. Fixed bug with JPEG quality and resolution ratio
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.0.12")); // Fixed bug with JPEG quality
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.0.13")); // Fixed null reference error when multiple application instances are started.
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.0.14")); // Introduced a new tag that gets the title of the active window. Also added a new method method in MacroParser that strips away characters that are invalid in Windows filenames (except for the backslash character since we use that for the directory path).
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.0.15")); // Strip out backslash if it's in the active window title.
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.0.16")); // Maintenance timer is turned on when interface is hidden and turned on when interface is shown.
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.0.17")); // Replaced -lock command line argument with -passphrase and added logic around hashing the passphrase given from the command line.
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.0.18")); // Performance improvements when saving screenshot references to screenshots.xml file.
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.0.19")); // Fixing system tray icon messages when mouse hovers over icon during maintenance. Also attempting to fix bug with collection being modified when browsing screenshots.
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.0.20")); // Tab pages now auto-scroll.
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.0.21")); // Configure drop down menu finished.
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.0.22")); // Fixed scheduled start time when running from command line.
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.1.0")); // Logging is now an application setting and DebugMode has become verbose logging. I've also fixed a few issues when running -config from the command line.
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.1.1")); // You can now add Batch Scripts, PowerShell Scripts, and any type of file for an Editor. Also removed the "Show system tray icon" option.
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.1.2")); // Fixed a bug with saving passphrase and hiding system tray icon during command line execution.
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.2.0")); // Completed work on Email Screenshot. Also included EmailScreenshot action for Triggers and added confirmation dialog boxes when emailing a screenshot from the interface and removing a screen or region. Email icon image added to Email button.
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.2.1")); // Fixed a few bugs.
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.2.2")); // Fixed bug with passphrase hash that was being hashed through old version detection by accident.
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.2.3")); // Moved default location of autoscreen.conf file.
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.2.4")); // Macro tags %machine% and %user% can now be used in folder paths and all paths of the autoscreen.conf file.
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.2.5")); // Fixed bug with upgrade system.
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.2.6")); // Fixed upgrade system. For real this time.
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.2.7")); // Make sure we do not check the drive information if the path is a shared network path.
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.2.8")); // Fixed an issue with displaying a screenshot preview.
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.2.9")); // Double click system tray icon to show or hide interface. Fixed issue with having backslash characters in name and any invalid Windows characters in path.
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.3.0")); // Apply Label system tray icon menu lists available labels.
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.3.1")); // Apply Label is made invisible when screen capture session is locked. Fixed bug with parsing command line arguments.
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.3.2")); // Apply Label fixed to show labels whenever the system tray icon menu is opened.
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.4.6")); // System tray icon turns green when screen capture session is running. Tags are now user-defined and have their own module.

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
                        Application.Add(new Setting("EmailServerHost", "smtp.office365.com"));
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

                    if (!Application.KeyExists("LowDiskPercentageThreshold"))
                    {
                        Application.Add(new Setting("LowDiskPercentageThreshold", 1));
                    }
                }
                else
                {
                    Application.Add(new Setting("Name", ApplicationName));
                    Application.Add(new Setting("Version", ApplicationVersion));
                    Application.Add(new Setting("DebugMode", false));
                    Application.Add(new Setting("Logging", false));
                    Application.Add(new Setting("EmailServerHost", "smtp.office365.com"));
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
                    Application.Add(new Setting("LowDiskPercentageThreshold", 1));
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
                User.Add(new Setting("DateTimeCaptureStopAt", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 0, 0)));
                User.Add(new Setting("DateTimeCaptureStartAt", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0)));
                User.Add(new Setting("StringPassphrase", string.Empty));
                User.Add(new Setting("IntKeepScreenshotsForDays", 30));
                User.Add(new Setting("StringScreenshotLabel", string.Empty));
                User.Add(new Setting("BoolApplyScreenshotLabel", false));
                User.Add(new Setting("StringDefaultEditor", string.Empty));

                User.Save();
            }

            Log.DebugMode = Convert.ToBoolean(Application.GetByKey("DebugMode", defaultValue: false).Value);
            Log.Enabled = Convert.ToBoolean(Application.GetByKey("Logging", defaultValue: false).Value);
        }
    }
}