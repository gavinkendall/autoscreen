//-----------------------------------------------------------------------
// <copyright file="Settings.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>The default application settings and default user settings are defined here.</summary>
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
using System;

namespace AutoScreenCapture
{
    /// <summary>
    /// A class for all the application-specific and user-specific settings used by Auto Screen Capture.
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// The name of this application.
        /// </summary>
        public static readonly string ApplicationName = DefaultSettings.ApplicationName;

        /// <summary>
        /// The version of this application. This is acquired from the application's assembly.
        /// </summary>
        public static readonly string ApplicationVersion = DefaultSettings.ApplicationVersion;

        // The major versions of Auto Screen Capture.
        // Any version that is older than "Clara" is considered as "Clara".
        private const string CODENAME_CLARA = "Clara"; // Clara introduced the Macro field for customizing the filename pattern of image files when writing them to disk.
        private const string CODENAME_DALEK = "Dalek"; // Dalek made it possible to have an unlimited number of screens, apply labels to screenshots, and create user-defined macro tags.

        // The current major version.
        // Boombayah (named after Blackpink's "Boombayah" - https://www.youtube.com/watch?v=bwmSjveL3Lc)
        // introduces new commands to execute from a command line terminal, the ability to issue commands for a running instance of the application,
        // the Schedules module for managing multiple schedules, and a user-friendly dynamic interface that offers help tips in a help bar at the top.
        // There has also been a lot of work in being able to load screenshots in an "on-demand" manner whenever necessary to avoid hanging while reading
        // screenshots data from, what could be, a very large XML document of screenshot nodes.
        // This version also introduces keyboard shortcuts and it populates the Editors module with a list of applications it finds on the user's machine.
        // The startup time is also faster since we no longer initialize the threads in the beginning (that, in turn, reads from the XML documents).
        private const string CODENAME_BOOMBAYAH = "Boombayah";

        /// <summary>
        /// The codename of this application.
        /// </summary>
        public static readonly string ApplicationCodename = CODENAME_BOOMBAYAH;

        /// <summary>
        /// The application settings collection.
        /// </summary>
        public static SettingCollection Application;

        /// <summary>
        /// The user settings collection.
        /// </summary>
        public static SettingCollection User;

        /// <summary>
        /// The version manager to take care of what to do when an old version is encountered.
        /// </summary>
        public static VersionManager VersionManager;

        /// <summary>
        /// A collection of Version objects representing the various versions of the application.
        /// </summary>
        private static VersionCollection _versionCollection;

        /// <summary>
        /// Creates a new version collection and populates it with all the various versions of the application.
        /// </summary>
        public static void Initialize()
        {
            _versionCollection = new VersionCollection
            {
                // This version.
                new Version(ApplicationCodename, ApplicationVersion, isCurrentVersion: true),

                // Older versions should be listed here.
                // WARNING: Never have any of the individual numbers go beyond 9 unless you want to break the upgrade system! This was an issue introduced in 2.2.0.10.
                new Version(CODENAME_CLARA, "2.1.8.2"), // "Clara"; the last version in the 2.1 "macro" series. This includes the large user.xml file fix (2.1.7.9), "the handle is invalid" fix when Windows is locked (2.1.8.0), and the removal of the "start when Windows starts" feature (2.1.8.1); because anti-virus software falsely flags the application as a virus if we do that.
                new Version(CODENAME_DALEK, "2.2.0.0"), // "Dalek"; support for unlimited number of screens. This was a huge release with the most commits ever in the application's history.
                new Version(CODENAME_DALEK, "2.2.0.1"), // Fixed bug with empty window title which resulted in image files remaining after cleanup.
                new Version(CODENAME_DALEK, "2.2.0.2"), // Application no longer stops current screen capture session if the directory root does not exist or the drive is not ready. This ensures that the current screen capture session will continue even if the drive being referenced is not available for some reason. Useful if you usually save screen images to an external drive but then disconnect from it and want to continue using the laptop's internal drive. Reintroduced the thread for saving screenshots and the lock on xDoc
                new Version(CODENAME_DALEK, "2.2.0.3"), // Active window title is retrieved earlier for less chance in having different titles in different screenshot objects despite being in the same screen capture cycle. Some code cleanup. Documentation being added. Fixed a racing condition issue with KeepScreenshotsForDays and Save in ScreenshotCollection
                new Version(CODENAME_DALEK, "2.2.0.4"), // Expanded scope of lock around screenshot list
                new Version(CODENAME_DALEK, "2.2.0.5"), // Old screenshots are deleted and unsaved screenshots are saved within the same timer
                new Version(CODENAME_DALEK, "2.2.0.6"), // An existing label can now be selected from a drop down list of available labels and applying a label to each screenshot is now determined by a checkbox value
                new Version(CODENAME_DALEK, "2.2.0.7"), // Process Name introduced. Can now filter by an application's process name. Fixed upgrade path from old versions. Removed -debug command line argument. ScreenCapture Count is now reduced whenever there is no image available to capture or active window title is empty to hopefully make the count more accurate when using the count tag. Logging removed in TakeScreenshot and ScreenCapture Save methods to save on disk space when DebugMode enabled
                new Version(CODENAME_DALEK, "2.2.0.8"), // New macro tags for getting the name of the currently logged in user and the name of the machine being used
                new Version(CODENAME_DALEK, "2.2.0.9"), // Fixed upgrade path from older versions
                new Version(CODENAME_DALEK, "2.2.0.10"), // Fixed bug with count value when display is not available
                new Version(CODENAME_DALEK, "2.2.0.11"), // The %screen% tag has been re-introduced to represent the screen number. Fixed bug with taskbar not being captured. Fixed bug with JPEG quality and resolution ratio
                new Version(CODENAME_DALEK, "2.2.0.12"), // Fixed bug with JPEG quality
                new Version(CODENAME_DALEK, "2.2.0.13"), // Fixed null reference error when multiple application instances are started.
                new Version(CODENAME_DALEK, "2.2.0.14"), // Introduced a new tag that gets the title of the active window. Also added a new method method in MacroParser that strips away characters that are invalid in Windows filenames (except for the backslash character since we use that for the directory path).
                new Version(CODENAME_DALEK, "2.2.0.15"), // Strip out backslash if it's in the active window title.
                new Version(CODENAME_DALEK, "2.2.0.16"), // Maintenance timer is turned on when interface is hidden and turned on when interface is shown.
                new Version(CODENAME_DALEK, "2.2.0.17"), // Replaced -lock command line argument with -passphrase and added logic around hashing the passphrase given from the command line.
                new Version(CODENAME_DALEK, "2.2.0.18"), // Performance improvements when saving screenshot references to screenshots.xml file.
                new Version(CODENAME_DALEK, "2.2.0.19"), // Fixing system tray icon messages when mouse hovers over icon during maintenance. Also attempting to fix bug with collection being modified when browsing screenshots.
                new Version(CODENAME_DALEK, "2.2.0.20"), // Tab pages now auto-scroll.
                new Version(CODENAME_DALEK, "2.2.0.21"), // Configure drop down menu finished.
                new Version(CODENAME_DALEK, "2.2.0.22"), // Fixed scheduled start time when running from command line.
                new Version(CODENAME_DALEK, "2.2.1.0"), // Logging is now an application setting and DebugMode has become verbose logging. I've also fixed a few issues when running -config from the command line.
                new Version(CODENAME_DALEK, "2.2.1.1"), // You can now add Batch Scripts, PowerShell Scripts, and any type of file for an Editor. Also removed the "Show system tray icon" option.
                new Version(CODENAME_DALEK, "2.2.1.2"), // Fixed a bug with saving passphrase and hiding system tray icon during command line execution.
                new Version(CODENAME_DALEK, "2.2.2.0"), // Completed work on Email Screenshot. Also included EmailScreenshot action for Triggers and added confirmation dialog boxes when emailing a screenshot from the interface and removing a screen or region. Email icon image added to Email button.
                new Version(CODENAME_DALEK, "2.2.2.1"), // Fixed a few bugs.
                new Version(CODENAME_DALEK, "2.2.2.2"), // Fixed bug with passphrase hash that was being hashed through old version detection by accident.
                new Version(CODENAME_DALEK, "2.2.2.3"), // Moved default location of autoscreen.conf file.
                new Version(CODENAME_DALEK, "2.2.2.4"), // Macro tags %machine% and %user% can now be used in folder paths and all paths of the autoscreen.conf file.
                new Version(CODENAME_DALEK, "2.2.2.5"), // Fixed bug with upgrade system.
                new Version(CODENAME_DALEK, "2.2.2.6"), // Fixed upgrade system. For real this time.
                new Version(CODENAME_DALEK, "2.2.2.7"), // Make sure we do not check the drive information if the path is a shared network path.
                new Version(CODENAME_DALEK, "2.2.2.8"), // Fixed an issue with displaying a screenshot preview.
                new Version(CODENAME_DALEK, "2.2.2.9"), // Double click system tray icon to show or hide interface. Fixed issue with having backslash characters in name and any invalid Windows characters in path.
                new Version(CODENAME_DALEK, "2.2.3.0"), // Apply Label system tray icon menu lists available labels.
                new Version(CODENAME_DALEK, "2.2.3.1"), // Apply Label is made invisible when screen capture session is locked. Fixed bug with parsing command line arguments.
                new Version(CODENAME_DALEK, "2.2.3.2"), // Apply Label fixed to show labels whenever the system tray icon menu is opened.
                new Version(CODENAME_DALEK, "2.2.4.6"), // System tray icon turns green when screen capture session is running. Tags are now user-defined and have their own module.
                new Version(CODENAME_DALEK, "2.2.5.0"), // A version that was never released. This was to make startup speed faster but major features implemented for the application (such as Schedules and controlling a running instance of the application from the command line) deserved 2.2.5.0 to become 2.3.0.0! Boombayah!
                new Version(CODENAME_BOOMBAYAH, "2.3.0.0"), // Faster startup, commands can be issued to a running instance, multiple schedules, more trigger conditions and trigger actions, and help tips in the help bar.
                new Version(CODENAME_BOOMBAYAH, "2.3.0.1"), // Fixed bug with FilenameLengthLimit application setting.
                new Version(CODENAME_BOOMBAYAH, "2.3.0.2"), // Keyboard Shortcuts implemented.
                new Version(CODENAME_BOOMBAYAH, "2.3.0.3"), // Fixed bug when creating a new Trigger.
                new Version(CODENAME_BOOMBAYAH, "2.3.0.4"), // StopOnLowDiskError setting implemented.
                new Version(CODENAME_BOOMBAYAH, "2.3.0.5"), // Fixed bug with upgrade path. Changed target profile to be .NET 4 instead of .NET Client 4.
                new Version(CODENAME_BOOMBAYAH, "2.3.0.6"), // Fixed interface issues with Windows 10 DPI scaling.
                new Version(CODENAME_BOOMBAYAH, "2.3.0.7"), // Fixed bug with a timed Trigger that needed to reset the timer when changing the screen capture interval.
                new Version(CODENAME_BOOMBAYAH, "2.3.0.8"), // Fixed DPI Awareness OS Version check.
                new Version(CODENAME_BOOMBAYAH, "2.3.0.9"), // Removed font changes for DPI Awareness.
                new Version(CODENAME_BOOMBAYAH, "2.3.1.0"), // Truncates long file paths.
                new Version(CODENAME_BOOMBAYAH, "2.3.1.1"), // ActiveWindowTitleLengthLimit application setting implemented.
                new Version(CODENAME_BOOMBAYAH, "2.3.1.2"), // Snagit Editor introduced as a new default image editor if available.
                new Version(CODENAME_BOOMBAYAH, "2.3.1.3"), // Fixed bug with new Editor throwing null reference exception on changing its properties because Notes was null.
                new Version(CODENAME_BOOMBAYAH, "2.3.1.4"), // ExitOnError set to True by default.
                new Version(CODENAME_BOOMBAYAH, "2.3.1.5"), // Region Select / Auto Save implemented.
                new Version(CODENAME_BOOMBAYAH, "2.3.1.6"), // Region Select Edit implemented and fixed bug with ViewId for new Screens and Regions.
                new Version(CODENAME_BOOMBAYAH, "2.3.1.7"), // OptimizeScreenCapture implemented.
                new Version(CODENAME_BOOMBAYAH, "2.3.1.8"), // Region Select implemented for Regions.
                new Version(CODENAME_BOOMBAYAH, "2.3.1.9"), // Schedules can now have their own interval set for them separate from the main interval. Also fixed bug with screen capture duration info.
                new Version(CODENAME_BOOMBAYAH, "2.3.2.0"), // Region Select Auto Save region is created if the regions.xml file is not found so you can view screenshots taken with Region Select Auto Save.
                new Version(CODENAME_BOOMBAYAH, "2.3.2.1") // Fixed bug with inactive schedules that should not perform any actions when inactive.
            };

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
                if (FileSystem.FileExists(Application.Filepath))
                {
                    Application.Load();

                    Application.GetByKey("Name", DefaultSettings.ApplicationName).Value = ApplicationName;
                    Application.GetByKey("Version", DefaultSettings.ApplicationVersion).Value = ApplicationVersion;

                    if (!Application.KeyExists("DebugMode"))
                    {
                        Application.Add(new Setting("DebugMode", DefaultSettings.DebugMode));
                    }

                    if (!Application.KeyExists("ExitOnError"))
                    {
                        Application.Add(new Setting("ExitOnError", DefaultSettings.ExitOnError));
                    }

                    if (!Application.KeyExists("Logging"))
                    {
                        Application.Add(new Setting("Logging", DefaultSettings.Logging));
                    }

                    if (!Application.KeyExists("EmailServerHost"))
                    {
                        Application.Add(new Setting("EmailServerHost", DefaultSettings.EmailServerHost));
                    }

                    if (!Application.KeyExists("EmailServerPort"))
                    {
                        Application.Add(new Setting("EmailServerPort", DefaultSettings.EmailServerPort));
                    }

                    if (!Application.KeyExists("EmailServerEnableSSL"))
                    {
                        Application.Add(new Setting("EmailServerEnableSSL", DefaultSettings.EmailServerEnableSSL));
                    }

                    if (!Application.KeyExists("EmailClientUsername"))
                    {
                        Application.Add(new Setting("EmailClientUsername", DefaultSettings.EmailClientUsername));
                    }

                    if (!Application.KeyExists("EmailClientPassword"))
                    {
                        Application.Add(new Setting("EmailClientPassword", DefaultSettings.EmailClientPassword));
                    }

                    if (!Application.KeyExists("EmailMessageFrom"))
                    {
                        Application.Add(new Setting("EmailMessageFrom", DefaultSettings.EmailMessageFrom));
                    }

                    if (!Application.KeyExists("EmailMessageTo"))
                    {
                        Application.Add(new Setting("EmailMessageTo", DefaultSettings.EmailMessageTo));
                    }

                    if (!Application.KeyExists("EmailMessageCC"))
                    {
                        Application.Add(new Setting("EmailMessageCC", DefaultSettings.EmailMessageCC));
                    }

                    if (!Application.KeyExists("EmailMessageBCC"))
                    {
                        Application.Add(new Setting("EmailMessageBCC", DefaultSettings.EmailMessageBCC));
                    }

                    if (!Application.KeyExists("EmailMessageSubject"))
                    {
                        Application.Add(new Setting("EmailMessageSubject", DefaultSettings.EmailMessageSubject));
                    }

                    if (!Application.KeyExists("EmailMessageBody"))
                    {
                        Application.Add(new Setting("EmailMessageBody", DefaultSettings.EmailMessageBody));
                    }

                    if (!Application.KeyExists("EmailPrompt"))
                    {
                        Application.Add(new Setting("EmailPrompt", DefaultSettings.EmailPrompt));
                    }

                    if (!Application.KeyExists("LowDiskPercentageThreshold"))
                    {
                        Application.Add(new Setting("LowDiskPercentageThreshold", DefaultSettings.LowDiskPercentageThreshold));
                    }

                    if (!Application.KeyExists("ScreenshotsLoadLimit"))
                    {
                        Application.Add(new Setting("ScreenshotsLoadLimit", DefaultSettings.ScreenshotsLoadLimit));
                    }

                    if (!Application.KeyExists("AutoStartFromCommandLine"))
                    {
                        Application.Add(new Setting("AutoStartFromCommandLine", DefaultSettings.AutoStartFromCommandLine));

                        // If this is a version before 2.3.0.0 then set this setting to true because
                        // starting a screen capture session was the old behaviour when running autoscreen.exe
                        // from the command line.
                        if (VersionManager.IsOldAppVersion(Application.AppCodename, Application.AppVersion))
                        {
                            Version v2300 = VersionManager.Versions.Get(CODENAME_BOOMBAYAH, "2.3.0.0");
                            Version configVersion = VersionManager.Versions.Get(Application.AppCodename, Application.AppVersion);

                            if (v2300 != null && configVersion != null && configVersion.VersionNumber < v2300.VersionNumber)
                            {
                                Application.GetByKey("AutoStartFromCommandLine", DefaultSettings.AutoStartFromCommandLine).Value = true;
                            }
                        }
                    }

                    if (!Application.KeyExists("ShowStartupError"))
                    {
                        Application.Add(new Setting("ShowStartupError", DefaultSettings.ShowStartupError));
                    }

                    if (!Application.KeyExists("FilepathLengthLimit"))
                    {
                        Application.Add(new Setting("FilepathLengthLimit", DefaultSettings.FilepathLengthLimit));
                    }

                    if (!Application.KeyExists("StopOnLowDiskError"))
                    {
                        Application.Add(new Setting("StopOnLowDiskError", DefaultSettings.StopOnLowDiskError));
                    }

                    if (!Application.KeyExists("ActiveWindowTitleLengthLimit"))
                    {
                        Application.Add(new Setting("ActiveWindowTitleLengthLimit", DefaultSettings.ActiveWindowTitleLengthLimit));
                    }

                    if (!Application.KeyExists("OptimizeScreenCapture"))
                    {
                        Application.Add(new Setting("OptimizeScreenCapture", DefaultSettings.OptimizeScreenCapture));
                    }
                }
                else
                {
                    Application.Add(new Setting("Name", ApplicationName));
                    Application.Add(new Setting("Version", ApplicationVersion));
                    Application.Add(new Setting("DebugMode", DefaultSettings.DebugMode));
                    Application.Add(new Setting("ExitOnError", DefaultSettings.ExitOnError));
                    Application.Add(new Setting("Logging", DefaultSettings.Logging));
                    Application.Add(new Setting("EmailServerHost", DefaultSettings.EmailServerHost));
                    Application.Add(new Setting("EmailServerPort", DefaultSettings.EmailServerPort));
                    Application.Add(new Setting("EmailServerEnableSSL", DefaultSettings.EmailServerEnableSSL));
                    Application.Add(new Setting("EmailClientUsername", DefaultSettings.EmailClientUsername));
                    Application.Add(new Setting("EmailClientPassword", DefaultSettings.EmailClientPassword));
                    Application.Add(new Setting("EmailMessageFrom", DefaultSettings.EmailMessageFrom));
                    Application.Add(new Setting("EmailMessageTo", DefaultSettings.EmailMessageTo));
                    Application.Add(new Setting("EmailMessageCC", DefaultSettings.EmailMessageCC));
                    Application.Add(new Setting("EmailMessageBCC", DefaultSettings.EmailMessageBCC));
                    Application.Add(new Setting("EmailMessageSubject", DefaultSettings.EmailMessageSubject));
                    Application.Add(new Setting("EmailMessageBody", DefaultSettings.EmailMessageBody));
                    Application.Add(new Setting("EmailPrompt", DefaultSettings.EmailPrompt));
                    Application.Add(new Setting("LowDiskPercentageThreshold", DefaultSettings.LowDiskPercentageThreshold));
                    Application.Add(new Setting("ScreenshotsLoadLimit", DefaultSettings.ScreenshotsLoadLimit));
                    Application.Add(new Setting("AutoStartFromCommandLine", DefaultSettings.AutoStartFromCommandLine));
                    Application.Add(new Setting("ShowStartupError", DefaultSettings.ShowStartupError));
                    Application.Add(new Setting("FilepathLengthLimit", DefaultSettings.FilepathLengthLimit));
                    Application.Add(new Setting("StopOnLowDiskError", DefaultSettings.StopOnLowDiskError));
                    Application.Add(new Setting("ActiveWindowTitleLengthLimit", DefaultSettings.ActiveWindowTitleLengthLimit));
                    Application.Add(new Setting("OptimizeScreenCapture", DefaultSettings.OptimizeScreenCapture));
                }

                Application.Save();
            }

            if (User != null && !string.IsNullOrEmpty(User.Filepath) && !FileSystem.FileExists(User.Filepath))
            {
                User.Add(new Setting("IntScreenCaptureInterval", DefaultSettings.IntScreenCaptureInterval));
                User.Add(new Setting("IntCaptureLimit", DefaultSettings.IntCaptureLimit));
                User.Add(new Setting("BoolCaptureLimit", DefaultSettings.BoolCaptureLimit));
                User.Add(new Setting("BoolTakeInitialScreenshot", DefaultSettings.BoolTakeInitialScreenshot));
                User.Add(new Setting("BoolShowSystemTrayIcon", DefaultSettings.BoolShowSystemTrayIcon));
                User.Add(new Setting("StringPassphrase", DefaultSettings.StringPassphrase));
                User.Add(new Setting("IntKeepScreenshotsForDays", DefaultSettings.IntKeepScreenshotsForDays));
                User.Add(new Setting("StringScreenshotLabel", DefaultSettings.StringScreenshotLabel));
                User.Add(new Setting("BoolApplyScreenshotLabel", DefaultSettings.BoolApplyScreenshotLabel));
                User.Add(new Setting("StringDefaultEditor", DefaultSettings.StringDefaultEditor));
                User.Add(new Setting("BoolFirstRun", DefaultSettings.BoolFirstRun));
                User.Add(new Setting("IntStartScreenCaptureCount", DefaultSettings.IntStartScreenCaptureCount));
                User.Add(new Setting("BoolActiveWindowTitleCaptureCheck", DefaultSettings.BoolActiveWindowTitleCaptureCheck));
                User.Add(new Setting("StringActiveWindowTitleCaptureText", DefaultSettings.StringActiveWindowTitleCaptureText));
                User.Add(new Setting("BoolUseKeyboardShortcuts", DefaultSettings.BoolUseKeyboardShortcuts));
                User.Add(new Setting("StringKeyboardShortcutStartScreenCaptureModifier1", DefaultSettings.StringKeyboardShortcutStartScreenCaptureModifier1));
                User.Add(new Setting("StringKeyboardShortcutStartScreenCaptureModifier2", DefaultSettings.StringKeyboardShortcutStartScreenCaptureModifier2));
                User.Add(new Setting("StringKeyboardShortcutStartScreenCaptureKey", DefaultSettings.StringKeyboardShortcutStartScreenCaptureKey));
                User.Add(new Setting("StringKeyboardShortcutStopScreenCaptureModifier1", DefaultSettings.StringKeyboardShortcutStopScreenCaptureModifier1));
                User.Add(new Setting("StringKeyboardShortcutStopScreenCaptureModifier2", DefaultSettings.StringKeyboardShortcutStopScreenCaptureModifier2));
                User.Add(new Setting("StringKeyboardShortcutStopScreenCaptureKey", DefaultSettings.StringKeyboardShortcutStopScreenCaptureKey));
                User.Add(new Setting("StringKeyboardShortcutCaptureNowArchiveModifier1", DefaultSettings.StringKeyboardShortcutCaptureNowArchiveModifier1));
                User.Add(new Setting("StringKeyboardShortcutCaptureNowArchiveModifier2", DefaultSettings.StringKeyboardShortcutCaptureNowArchiveModifier2));
                User.Add(new Setting("StringKeyboardShortcutCaptureNowArchiveKey", DefaultSettings.StringKeyboardShortcutCaptureNowArchiveKey));
                User.Add(new Setting("StringKeyboardShortcutCaptureNowEditModifier1", DefaultSettings.StringKeyboardShortcutCaptureNowEditModifier1));
                User.Add(new Setting("StringKeyboardShortcutCaptureNowEditModifier2", DefaultSettings.StringKeyboardShortcutCaptureNowEditModifier2));
                User.Add(new Setting("StringKeyboardShortcutCaptureNowEditKey", DefaultSettings.StringKeyboardShortcutCaptureNowEditKey));
                User.Add(new Setting("StringKeyboardShortcutRegionSelectClipboardModifier1", DefaultSettings.StringKeyboardShortcutRegionSelectClipboardModifier1));
                User.Add(new Setting("StringKeyboardShortcutRegionSelectClipboardModifier2", DefaultSettings.StringKeyboardShortcutRegionSelectClipboardModifier2));
                User.Add(new Setting("StringKeyboardShortcutRegionSelectClipboardKey", DefaultSettings.StringKeyboardShortcutRegionSelectClipboardKey));
                User.Add(new Setting("StringAutoSaveFolder", DefaultSettings.StringAutoSaveFolder));
                User.Add(new Setting("StringAutoSaveMacro", DefaultSettings.StringAutoSaveMacro));
                User.Add(new Setting("StringKeyboardShortcutRegionSelectAutoSaveModifier1", DefaultSettings.StringKeyboardShortcutRegionSelectAutoSaveModifier1));
                User.Add(new Setting("StringKeyboardShortcutRegionSelectAutoSaveModifier2", DefaultSettings.StringKeyboardShortcutRegionSelectAutoSaveModifier2));
                User.Add(new Setting("StringKeyboardShortcutRegionSelectAutoSaveKey", DefaultSettings.StringKeyboardShortcutRegionSelectAutoSaveKey));
                User.Add(new Setting("StringKeyboardShortcutRegionSelectEditModifier1", DefaultSettings.StringKeyboardShortcutRegionSelectEditModifier1));
                User.Add(new Setting("StringKeyboardShortcutRegionSelectEditModifier2", DefaultSettings.StringKeyboardShortcutRegionSelectEditModifier2));
                User.Add(new Setting("StringKeyboardShortcutRegionSelectEditKey", DefaultSettings.StringKeyboardShortcutRegionSelectEditKey));

                User.Save();
            }

            Log.DebugMode = Convert.ToBoolean(Application.GetByKey("DebugMode", DefaultSettings.DebugMode).Value);
            Log.LoggingEnabled = Convert.ToBoolean(Application.GetByKey("Logging", DefaultSettings.Logging).Value);
        }
    }
}