//-----------------------------------------------------------------------
// <copyright file="DefaultSettings.cs" company="Gavin Kendall">
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
using System.Reflection;

namespace AutoScreenCapture
{
    /// <summary>
    /// The default settings for the application and the user.
    /// </summary>
    public static class DefaultSettings
    {
        /// <summary>
        /// The name of this application.
        /// </summary>
        public static readonly string ApplicationName = "Auto Screen Capture";

        /// <summary>
        /// The version of this application. This is acquired from the application's assembly.
        /// </summary>
        public static readonly string ApplicationVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        // Default application settings.
        internal static readonly bool DebugMode = false;
        internal static readonly bool ExitOnError = true;
        internal static readonly bool Logging = false;
        internal static readonly int LowDiskPercentageThreshold = 1;
        internal static readonly int ScreenshotsLoadLimit = 5000;
        internal static readonly bool AutoStartFromCommandLine = false;
        internal static readonly bool ShowStartupError = true;
        internal static readonly int FilepathLengthLimit = 2000;
        internal static readonly bool StopOnLowDiskError = true;
        internal static readonly int ActiveWindowTitleLengthLimit = 2000;
        internal static readonly bool OptimizeScreenCapture = true;
        internal static readonly bool AllowUserToConfigureEmailSettings = true;
        internal static readonly bool AllowUserToConfigureFileTransferSettings = true;

        // Default user settings.
        internal static readonly int ScreenCaptureInterval = 60000;
        internal static readonly int CaptureLimit = 0;
        internal static readonly bool CaptureLimitCheck = false;
        internal static readonly bool TakeInitialScreenshot = false;
        internal static readonly bool ShowSystemTrayIcon = true;
        internal static readonly string Passphrase = string.Empty;
        internal static readonly string ScreenshotLabel = string.Empty;
        internal static readonly bool ApplyScreenshotLabel = false;
        internal static readonly string DefaultEditor = string.Empty;
        internal static readonly bool FirstRun = true;
        internal static readonly int StartScreenCaptureCount = 0;
        internal static readonly bool ActiveWindowTitleCaptureCheck = false;
        internal static readonly string ActiveWindowTitleCaptureText = string.Empty;
        internal static readonly string ApplicationFocus = string.Empty;
        internal static readonly string AutoSaveFolder = MacroParser.DefaultAutoSaveFolder;
        internal static readonly string AutoSaveMacro = MacroParser.DefaultAutoSaveMacro;
        internal static readonly int ApplicationFocusDelayBefore = 0;
        internal static readonly int ApplicationFocusDelayAfter = 0;

        // Keyboard Shortcuts.
        internal static readonly bool UseKeyboardShortcuts = false;
        internal static readonly string KeyboardShortcutStartScreenCaptureModifier1 = "Control";
        internal static readonly string KeyboardShortcutStartScreenCaptureModifier2 = "Alt";
        internal static readonly string KeyboardShortcutStartScreenCaptureKey = "Z";
        internal static readonly string KeyboardShortcutStopScreenCaptureModifier1 = "Control";
        internal static readonly string KeyboardShortcutStopScreenCaptureModifier2 = "Alt";
        internal static readonly string KeyboardShortcutStopScreenCaptureKey = "X";
        internal static readonly string KeyboardShortcutCaptureNowArchiveModifier1 = "Control";
        internal static readonly string KeyboardShortcutCaptureNowArchiveModifier2 = "Alt";
        internal static readonly string KeyboardShortcutCaptureNowArchiveKey = "A";
        internal static readonly string KeyboardShortcutCaptureNowEditModifier1 = "Control";
        internal static readonly string KeyboardShortcutCaptureNowEditModifier2 = "Alt";
        internal static readonly string KeyboardShortcutCaptureNowEditKey = "E";
        internal static readonly string KeyboardShortcutRegionSelectClipboardModifier1 = "Control";
        internal static readonly string KeyboardShortcutRegionSelectClipboardModifier2 = "Shift";
        internal static readonly string KeyboardShortcutRegionSelectClipboardKey = "C";
        internal static readonly string KeyboardShortcutRegionSelectAutoSaveModifier1 = "Control";
        internal static readonly string KeyboardShortcutRegionSelectAutoSaveModifier2 = "Shift";
        internal static readonly string KeyboardShortcutRegionSelectAutoSaveKey = "S";
        internal static readonly string KeyboardShortcutRegionSelectEditModifier1 = "Control";
        internal static readonly string KeyboardShortcutRegionSelectEditModifier2 = "Shift";
        internal static readonly string KeyboardShortcutRegionSelectEditKey = "E";

        // Email (SMTP) settings.
        internal static readonly string EmailServerHost = "smtp.office365.com";
        internal static readonly int EmailServerPort = 587;
        internal static readonly bool EmailServerEnableSSL = true;
        internal static readonly string EmailClientUsername = string.Empty;
        internal static readonly string EmailClientPassword = string.Empty;
        internal static readonly string EmailMessageFrom = string.Empty;
        internal static readonly string EmailMessageTo = string.Empty;
        internal static readonly string EmailMessageCC = string.Empty;
        internal static readonly string EmailMessageBCC = string.Empty;
        internal static readonly string EmailMessageSubject = string.Empty;
        internal static readonly string EmailMessageBody = string.Empty;
        internal static readonly bool EmailPrompt = true;

        // File Transfer (SFTP) settings.
        internal static readonly string FileTransferServerHost = string.Empty;
        internal static readonly int FileTransferServerPort = 22;
        internal static readonly string FileTransferClientUsername = string.Empty;
        internal static readonly string FileTransferClientPassword = string.Empty;

        // Old default user settings.
        internal static readonly bool BoolCaptureStartAt = false;
        internal static readonly bool BoolCaptureStopAt = false;
        internal static readonly DateTime DateTimeCaptureStartAt = DateTime.Now;
        internal static readonly DateTime DateTimeCaptureStopAt = DateTime.Now;
        internal static readonly bool BoolCaptureOnTheseDays = false;
        internal static readonly bool BoolCaptureOnSunday = false;
        internal static readonly bool BoolCaptureOnMonday = false;
        internal static readonly bool BoolCaptureOnTuesday = false;
        internal static readonly bool BoolCaptureOnWednesday = false;
        internal static readonly bool BoolCaptureOnThursday = false;
        internal static readonly bool BoolCaptureOnFriday = false;
        internal static readonly bool BoolCaptureOnSaturday = false;
    }
}
