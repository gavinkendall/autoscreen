//-----------------------------------------------------------------------
// <copyright file="DefaultSettings.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>The default application settings and default user settings are defined here.</summary>
//-----------------------------------------------------------------------
using System;
using System.Reflection;

namespace AutoScreenCapture
{
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
        internal static readonly bool ExitOnError = false;
        internal static readonly bool Logging = false;
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
        internal static readonly int LowDiskPercentageThreshold = 1;
        internal static readonly int ScreenshotsLoadLimit = 5000;
        internal static readonly bool AutoStartFromCommandLine = false;
        internal static readonly bool ShowStartupError = true;
        internal static readonly int FilepathLengthLimit = 2000;

        // Default user settings.
        internal static readonly int IntScreenCaptureInterval = 60000;
        internal static readonly int IntCaptureLimit = 0;
        internal static readonly bool BoolCaptureLimit = false;
        internal static readonly bool BoolTakeInitialScreenshot = false;
        internal static readonly bool BoolShowSystemTrayIcon = true;
        internal static readonly string StringPassphrase = string.Empty;
        internal static readonly int IntKeepScreenshotsForDays = 30;
        internal static readonly string StringScreenshotLabel = string.Empty;
        internal static readonly bool BoolApplyScreenshotLabel = false;
        internal static readonly string StringDefaultEditor = string.Empty;
        internal static readonly bool BoolFirstRun = true;
        internal static readonly int IntStartScreenCaptureCount = 0;

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
