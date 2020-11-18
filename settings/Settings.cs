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

        /// <summary>
        /// Any version that is older than "Clara" or a version cannot be determined is considered as "Clara".
        /// </summary>
        public const string CODENAME_CLARA = "Clara"; // Clara introduced the Macro field for customizing the filename pattern of image files when writing them to disk.

        /// <summary>
        /// The version number of any version old than "Clara" or a version that cannot be determined.
        /// </summary>
        public const string CODEVERSION_CLARA = "2.1.8.2";

        private const string CODENAME_DALEK = "Dalek"; // Dalek made it possible to have an unlimited number of screens, apply labels to screenshots, and create user-defined macro tags.

        // The current major version.
        // Boombayah (named after Blackpink's "Boombayah" - https://www.youtube.com/watch?v=bwmSjveL3Lc)
        // introduces new commands to execute from a command line terminal, the ability to issue commands for a running instance of the application,
        // the Schedules module for managing multiple schedules, and a user-friendly dynamic interface that offers help tips in a help bar at the top.
        // There has also been a lot of work in being able to load screenshots in an "on-demand" manner whenever necessary to avoid hanging while reading
        // screenshots data from, what could be, a very large XML document of screenshot nodes.
        // This version also introduces keyboard shortcuts and it populates the Editors module with a list of applications it finds on the user's machine.
        // The startup time is also faster since we no longer initialize the threads in the beginning (that, in turn, reads from the XML documents).
        // New system tray icon menu items such as Region Select / Clipboard, Region Select / Auto Save, and Region Select / Edit also introduced.
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
            _versionCollection = new VersionCollection();

            // This version.
            _versionCollection.Add(new Version(ApplicationCodename, ApplicationVersion, isCurrentVersion: true));

            // Older versions should be listed here.
            // WARNING: Never have any of the individual numbers go beyond 9 unless you want to break the upgrade system! This was an issue introduced in 2.2.0.10.
            _versionCollection.Add(new Version(CODENAME_CLARA, "2.1.8.2")); // "Clara"; the last version in the 2.1 "macro" series. This includes the large user.xml file fix (2.1.7.9)); "the handle is invalid" fix when Windows is locked (2.1.8.0), and the removal of the "start when Windows starts" feature (2.1.8.1); because anti-virus software falsely flags the application as a virus if we do that.
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
            _versionCollection.Add(new Version(CODENAME_DALEK, "2.2.5.0")); // A version that was never released. This was to make startup speed faster but major features implemented for the application (such as Schedules and controlling a running instance of the application from the command line) deserved 2.2.5.0 to become 2.3.0.0! Boombayah!
            _versionCollection.Add(new Version(CODENAME_BOOMBAYAH, "2.3.0.0")); // Faster startup, commands can be issued to a running instance, multiple schedules, more trigger conditions and trigger actions, and help tips in the help bar.
            _versionCollection.Add(new Version(CODENAME_BOOMBAYAH, "2.3.0.1")); // Fixed bug with FilenameLengthLimit application setting.
            _versionCollection.Add(new Version(CODENAME_BOOMBAYAH, "2.3.0.2")); // Keyboard Shortcuts implemented.
            _versionCollection.Add(new Version(CODENAME_BOOMBAYAH, "2.3.0.3")); // Fixed bug when creating a new Trigger.
            _versionCollection.Add(new Version(CODENAME_BOOMBAYAH, "2.3.0.4")); // StopOnLowDiskError setting implemented.
            _versionCollection.Add(new Version(CODENAME_BOOMBAYAH, "2.3.0.5")); // Fixed bug with upgrade path. Changed target profile to be .NET 4 instead of .NET Client 4.
            _versionCollection.Add(new Version(CODENAME_BOOMBAYAH, "2.3.0.6")); // Fixed interface issues with Windows 10 DPI scaling.
            _versionCollection.Add(new Version(CODENAME_BOOMBAYAH, "2.3.0.7")); // Fixed bug with a timed Trigger that needed to reset the timer when changing the screen capture interval.
            _versionCollection.Add(new Version(CODENAME_BOOMBAYAH, "2.3.0.8")); // Fixed DPI Awareness OS Version check.
            _versionCollection.Add(new Version(CODENAME_BOOMBAYAH, "2.3.0.9")); // Removed font changes for DPI Awareness.
            _versionCollection.Add(new Version(CODENAME_BOOMBAYAH, "2.3.1.0")); // Truncates long file paths.
            _versionCollection.Add(new Version(CODENAME_BOOMBAYAH, "2.3.1.1")); // ActiveWindowTitleLengthLimit application setting implemented.
            _versionCollection.Add(new Version(CODENAME_BOOMBAYAH, "2.3.1.2")); // Snagit Editor introduced as a new default image editor if available.
            _versionCollection.Add(new Version(CODENAME_BOOMBAYAH, "2.3.1.3")); // Fixed bug with new Editor throwing null reference exception on changing its properties because Notes was null.
            _versionCollection.Add(new Version(CODENAME_BOOMBAYAH, "2.3.1.4")); // ExitOnError set to True by default.
            _versionCollection.Add(new Version(CODENAME_BOOMBAYAH, "2.3.1.5")); // Region Select / Auto Save implemented.
            _versionCollection.Add(new Version(CODENAME_BOOMBAYAH, "2.3.1.6")); // Region Select Edit implemented and fixed bug with ViewId for new Screens and Regions.
            _versionCollection.Add(new Version(CODENAME_BOOMBAYAH, "2.3.1.7")); // OptimizeScreenCapture implemented.
            _versionCollection.Add(new Version(CODENAME_BOOMBAYAH, "2.3.1.8")); // Region Select implemented for Regions.
            _versionCollection.Add(new Version(CODENAME_BOOMBAYAH, "2.3.1.9")); // Schedules can now have their own interval set for them separate from the main interval. Also fixed bug with screen capture duration info.
            _versionCollection.Add(new Version(CODENAME_BOOMBAYAH, "2.3.2.0")); // Region Select Auto Save region is created if the regions.xml file is not found so you can view screenshots taken with Region Select Auto Save.
            _versionCollection.Add(new Version(CODENAME_BOOMBAYAH, "2.3.2.1")); // Fixed bug with inactive schedules that should not perform any actions when inactive.
            _versionCollection.Add(new Version(CODENAME_BOOMBAYAH, "2.3.2.2")); // Information Window implemented.
            _versionCollection.Add(new Version(CODENAME_BOOMBAYAH, "2.3.2.3")); // Information Window renamed to Show Screen Capture Status
            _versionCollection.Add(new Version(CODENAME_BOOMBAYAH, "2.3.2.4")); // ListboxScreenshots sorted.
            _versionCollection.Add(new Version(CODENAME_BOOMBAYAH, "2.3.2.5")); // Macro tag expressions can now parse date time format.
            _versionCollection.Add(new Version(CODENAME_BOOMBAYAH, "2.3.2.6")); // "Time of Day" Tag is now "Time Range" Tag. Also implemented "Day/Time" Trigger.
            _versionCollection.Add(new Version(CODENAME_BOOMBAYAH, "2.3.2.7")); // Quarter Year tag implemented.
            _versionCollection.Add(new Version(CODENAME_BOOMBAYAH, "2.3.2.8")); // Changelog added to About Auto Screen Capture window. Fixed bug with hidden system tray icon so no notification balloon appears when system tray icon is hidden.
            _versionCollection.Add(new Version(CODENAME_BOOMBAYAH, "2.3.2.9")); // Application Focus implemented for Screen.
            _versionCollection.Add(new Version(CODENAME_BOOMBAYAH, "2.3.3.0")); // Application Focus moved from Screen to Setup. Fixed Application Focus bug with Active Window Title. Renamed user setting keys. New method for capturing device display resolution.
            _versionCollection.Add(new Version(CODENAME_BOOMBAYAH, "2.3.3.1")); // New command line arguments -activeWindowTitle, -applicationFocus, and -label.
            _versionCollection.Add(new Version(CODENAME_BOOMBAYAH, "2.3.3.2")); // Can now run an Editor without arguments or without %filepath% tag when using Run Editor trigger action. Includes changes to version collection, change to how Application Focus behaves when application not found (so now adds the application to the process list regardless), and bug fix applied to threads.

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
                User.Add(new Setting("ScreenCaptureInterval", DefaultSettings.ScreenCaptureInterval));
                User.Add(new Setting("CaptureLimit", DefaultSettings.CaptureLimit));
                User.Add(new Setting("CaptureLimitCheck", DefaultSettings.CaptureLimitCheck));
                User.Add(new Setting("TakeInitialScreenshot", DefaultSettings.TakeInitialScreenshot));
                User.Add(new Setting("ShowSystemTrayIcon", DefaultSettings.ShowSystemTrayIcon));
                User.Add(new Setting("Passphrase", DefaultSettings.Passphrase));
                User.Add(new Setting("KeepScreenshotsForDays", DefaultSettings.KeepScreenshotsForDays));
                User.Add(new Setting("ScreenshotLabel", DefaultSettings.ScreenshotLabel));
                User.Add(new Setting("ApplyScreenshotLabel", DefaultSettings.ApplyScreenshotLabel));
                User.Add(new Setting("DefaultEditor", DefaultSettings.DefaultEditor));
                User.Add(new Setting("FirstRun", DefaultSettings.FirstRun));
                User.Add(new Setting("StartScreenCaptureCount", DefaultSettings.StartScreenCaptureCount));
                User.Add(new Setting("ActiveWindowTitleCaptureCheck", DefaultSettings.ActiveWindowTitleCaptureCheck));
                User.Add(new Setting("ActiveWindowTitleCaptureText", DefaultSettings.ActiveWindowTitleCaptureText));
                User.Add(new Setting("UseKeyboardShortcuts", DefaultSettings.UseKeyboardShortcuts));
                User.Add(new Setting("KeyboardShortcutStartScreenCaptureModifier1", DefaultSettings.KeyboardShortcutStartScreenCaptureModifier1));
                User.Add(new Setting("KeyboardShortcutStartScreenCaptureModifier2", DefaultSettings.KeyboardShortcutStartScreenCaptureModifier2));
                User.Add(new Setting("KeyboardShortcutStartScreenCaptureKey", DefaultSettings.KeyboardShortcutStartScreenCaptureKey));
                User.Add(new Setting("KeyboardShortcutStopScreenCaptureModifier1", DefaultSettings.KeyboardShortcutStopScreenCaptureModifier1));
                User.Add(new Setting("KeyboardShortcutStopScreenCaptureModifier2", DefaultSettings.KeyboardShortcutStopScreenCaptureModifier2));
                User.Add(new Setting("KeyboardShortcutStopScreenCaptureKey", DefaultSettings.KeyboardShortcutStopScreenCaptureKey));
                User.Add(new Setting("KeyboardShortcutCaptureNowArchiveModifier1", DefaultSettings.KeyboardShortcutCaptureNowArchiveModifier1));
                User.Add(new Setting("KeyboardShortcutCaptureNowArchiveModifier2", DefaultSettings.KeyboardShortcutCaptureNowArchiveModifier2));
                User.Add(new Setting("KeyboardShortcutCaptureNowArchiveKey", DefaultSettings.KeyboardShortcutCaptureNowArchiveKey));
                User.Add(new Setting("KeyboardShortcutCaptureNowEditModifier1", DefaultSettings.KeyboardShortcutCaptureNowEditModifier1));
                User.Add(new Setting("KeyboardShortcutCaptureNowEditModifier2", DefaultSettings.KeyboardShortcutCaptureNowEditModifier2));
                User.Add(new Setting("KeyboardShortcutCaptureNowEditKey", DefaultSettings.KeyboardShortcutCaptureNowEditKey));
                User.Add(new Setting("KeyboardShortcutRegionSelectClipboardModifier1", DefaultSettings.KeyboardShortcutRegionSelectClipboardModifier1));
                User.Add(new Setting("KeyboardShortcutRegionSelectClipboardModifier2", DefaultSettings.KeyboardShortcutRegionSelectClipboardModifier2));
                User.Add(new Setting("KeyboardShortcutRegionSelectClipboardKey", DefaultSettings.KeyboardShortcutRegionSelectClipboardKey));
                User.Add(new Setting("AutoSaveFolder", DefaultSettings.AutoSaveFolder));
                User.Add(new Setting("AutoSaveMacro", DefaultSettings.AutoSaveMacro));
                User.Add(new Setting("KeyboardShortcutRegionSelectAutoSaveModifier1", DefaultSettings.KeyboardShortcutRegionSelectAutoSaveModifier1));
                User.Add(new Setting("KeyboardShortcutRegionSelectAutoSaveModifier2", DefaultSettings.KeyboardShortcutRegionSelectAutoSaveModifier2));
                User.Add(new Setting("KeyboardShortcutRegionSelectAutoSaveKey", DefaultSettings.KeyboardShortcutRegionSelectAutoSaveKey));
                User.Add(new Setting("KeyboardShortcutRegionSelectEditModifier1", DefaultSettings.KeyboardShortcutRegionSelectEditModifier1));
                User.Add(new Setting("KeyboardShortcutRegionSelectEditModifier2", DefaultSettings.KeyboardShortcutRegionSelectEditModifier2));
                User.Add(new Setting("KeyboardShortcutRegionSelectEditKey", DefaultSettings.KeyboardShortcutRegionSelectEditKey));

                User.Save();
            }

            Log.DebugMode = Convert.ToBoolean(Application.GetByKey("DebugMode", DefaultSettings.DebugMode).Value);
            Log.LoggingEnabled = Convert.ToBoolean(Application.GetByKey("Logging", DefaultSettings.Logging).Value);
        }
    }
}