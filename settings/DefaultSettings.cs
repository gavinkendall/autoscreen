﻿//-----------------------------------------------------------------------
// <copyright file="DefaultSettings.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>The default settings are defined here.</summary>
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
    /// The default settings for the application and the user.
    /// </summary>
    public class DefaultSettings
    {
        // Default application settings.
        internal readonly bool DebugMode = false;
        internal readonly bool ExitOnError = false;
        internal readonly bool Logging = false;
        internal readonly int LowDiskPercentageThreshold = 1;
        internal readonly int ScreenshotsLoadLimit = 5000;
        internal readonly bool AutoStartFromCommandLine = false;
        internal readonly bool ShowStartupError = true;
        internal readonly int FilepathLengthLimit = 2000;
        internal readonly bool StopOnLowDiskError = true;
        internal readonly int ActiveWindowTitleLengthLimit = 2000;
        internal readonly bool AllowUserToConfigureEmailSettings = true;
        internal readonly bool AllowUserToConfigureFileTransferSettings = true;

        // Default user settings.
        internal readonly int ScreenCaptureInterval = 60000;
        internal readonly int CaptureLimit = 0;
        internal readonly bool CaptureLimitCheck = false;
        internal readonly bool TakeInitialScreenshot = true;
        internal readonly bool ShowSystemTrayIcon = false;
        internal readonly string Passphrase = string.Empty;
        internal readonly string ScreenshotLabel = string.Empty;
        internal readonly bool ApplyScreenshotLabel = false;
        internal readonly string DefaultEditor = string.Empty;
        internal readonly bool FirstRun = true;
        internal readonly int StartScreenCaptureCount = 0;
        internal readonly bool ActiveWindowTitleCaptureCheck = true;
        internal readonly bool ActiveWindowTitleNoMatchCheck = false;
        internal readonly string ActiveWindowTitleCaptureText = string.Empty;
        internal readonly string ActiveWindowTitleSampleText = string.Empty;
        internal readonly string ApplicationFocus = string.Empty;
        internal readonly string AutoSaveFolder = MacroParser.DefaultRegionSelectAutoSaveFolder;
        internal readonly string AutoSaveMacro = MacroParser.DefaultRegionSelectAutoSaveMacro;
        internal readonly string AutoSaveFormat = "JPEG";
        internal readonly int ApplicationFocusDelayBefore = 0;
        internal readonly int ApplicationFocusDelayAfter = 0;
        internal readonly int ActiveWindowTitleMatchType = 2;
        internal readonly bool SaveScreenshotRefs = true;
        internal readonly bool Preview = true;
        internal readonly bool OptimizeScreenCapture = false;
        internal readonly int DashboardGroupBoxSize = 250;
        internal readonly int SelectedTabPageIndex = 0;
        internal readonly int SelectedModuleIndex = 0;
        internal readonly bool CompareWithAnyPreviousImage = true;
        internal readonly bool CompareWithLastImage = false;
        internal readonly bool SFTPDeleteLocalFileAfterSuccessfulUpload = false;
        internal readonly bool SFTPKeepFailedUploads = true;

        // Keyboard Shortcuts.
        internal readonly bool UseKeyboardShortcuts = false;
        internal readonly string KeyboardShortcutStartScreenCaptureModifier1 = "Control";
        internal readonly string KeyboardShortcutStartScreenCaptureModifier2 = "Alt";
        internal readonly string KeyboardShortcutStartScreenCaptureKey = "Z";
        internal readonly string KeyboardShortcutStopScreenCaptureModifier1 = "Control";
        internal readonly string KeyboardShortcutStopScreenCaptureModifier2 = "Alt";
        internal readonly string KeyboardShortcutStopScreenCaptureKey = "X";
        internal readonly string KeyboardShortcutCaptureNowArchiveModifier1 = "Control";
        internal readonly string KeyboardShortcutCaptureNowArchiveModifier2 = "Alt";
        internal readonly string KeyboardShortcutCaptureNowArchiveKey = "A";
        internal readonly string KeyboardShortcutCaptureNowEditModifier1 = "Control";
        internal readonly string KeyboardShortcutCaptureNowEditModifier2 = "Alt";
        internal readonly string KeyboardShortcutCaptureNowEditKey = "E";
        internal readonly string KeyboardShortcutRegionSelectClipboardModifier1 = "Control";
        internal readonly string KeyboardShortcutRegionSelectClipboardModifier2 = "Shift";
        internal readonly string KeyboardShortcutRegionSelectClipboardKey = "C";
        internal readonly string KeyboardShortcutRegionSelectAutoSaveModifier1 = "Control";
        internal readonly string KeyboardShortcutRegionSelectAutoSaveModifier2 = "Shift";
        internal readonly string KeyboardShortcutRegionSelectAutoSaveKey = "S";
        internal readonly string KeyboardShortcutRegionSelectEditModifier1 = "Control";
        internal readonly string KeyboardShortcutRegionSelectEditModifier2 = "Shift";
        internal readonly string KeyboardShortcutRegionSelectEditKey = "E";

        // Email (SMTP) settings.
        internal readonly string EmailServerHost = "smtp.office365.com";
        internal readonly int EmailServerPort = 587;
        internal readonly bool EmailServerEnableSSL = true;
        internal readonly string EmailClientUsername = string.Empty;
        internal readonly string EmailClientPassword = string.Empty;
        internal readonly string EmailMessageFrom = string.Empty;
        internal readonly string EmailMessageTo = string.Empty;
        internal readonly string EmailMessageCC = string.Empty;
        internal readonly string EmailMessageBCC = string.Empty;
        internal readonly string EmailMessageSubject = string.Empty;
        internal readonly string EmailMessageBody = string.Empty;
        internal readonly bool EmailPrompt = true;

        // File Transfer (SFTP) settings.
        internal readonly string FileTransferServerHost = string.Empty;
        internal readonly int FileTransferServerPort = 22;
        internal readonly string FileTransferClientUsername = string.Empty;
        internal readonly string FileTransferClientPassword = string.Empty;
        internal readonly bool FileTransferIsLinuxServer = true;

        // Old default user settings.
        internal readonly bool BoolCaptureStartAt = false;
        internal readonly bool BoolCaptureStopAt = false;
        internal readonly DateTime DateTimeCaptureStartAt = DateTime.Now;
        internal readonly DateTime DateTimeCaptureStopAt = DateTime.Now;
        internal readonly bool BoolCaptureOnTheseDays = false;
        internal readonly bool BoolCaptureOnSunday = false;
        internal readonly bool BoolCaptureOnMonday = false;
        internal readonly bool BoolCaptureOnTuesday = false;
        internal readonly bool BoolCaptureOnWednesday = false;
        internal readonly bool BoolCaptureOnThursday = false;
        internal readonly bool BoolCaptureOnFriday = false;
        internal readonly bool BoolCaptureOnSaturday = false;
    }
}
