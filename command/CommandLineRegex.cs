//-----------------------------------------------------------------------
// <copyright file="CommandLineRegex.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    /// <summary>
    /// 
    /// </summary>
    public static class CommandLineRegex
    {
        /// <summary>
        /// 
        /// </summary>
        public const string REGEX_COMMAND_LINE_CONFIG = "^-config=(?<ConfigFile>.+)$";

        /// <summary>
        /// 
        /// </summary>
        public const string REGEX_COMMAND_LINE_INITIAL = "^-initial$";

        /// <summary>
        /// 
        /// </summary>
        public const string REGEX_COMMAND_LINE_LIMIT = @"^-limit=(?<Limit>\d{1,7})$";

        /// <summary>
        /// 
        /// </summary>
        public const string REGEX_COMMAND_LINE_CAPTUREAT = @"^-captureat=(?<Hours>\d{2}):(?<Minutes>\d{2}):(?<Seconds>\d{2})$";

        /// <summary>
        /// 
        /// </summary>
        public const string REGEX_COMMAND_LINE_STOPAT = @"^-stopat=(?<Hours>\d{2}):(?<Minutes>\d{2}):(?<Seconds>\d{2})$";

        /// <summary>
        /// 
        /// </summary>
        public const string REGEX_COMMAND_LINE_STARTAT = @"^-startat=(?<Hours>\d{2}):(?<Minutes>\d{2}):(?<Seconds>\d{2})$";

        /// <summary>
        /// 
        /// </summary>
        public const string REGEX_COMMAND_LINE_INTERVAL = @"^-interval=(?<Hours>\d{2}):(?<Minutes>\d{2}):(?<Seconds>\d{2})\.(?<Milliseconds>\d{3})$";

        /// <summary>
        /// 
        /// </summary>
        public const string REGEX_COMMAND_LINE_PASSPHRASE = "^-passphrase=(?<Passphrase>.+)$";

        /// <summary>
        /// 
        /// </summary>
        public const string REGEX_COMMAND_LINE_HIDE_SYSTEM_TRAY_ICON = "^-hideSystemTrayIcon$";

        /// <summary>
        /// 
        /// </summary>
        public const string REGEX_COMMAND_LINE_LOG = "^-log";

        /// <summary>
        /// 
        /// </summary>
        public const string REGEX_COMMAND_LINE_LOG_ON = "^-log=on";

        /// <summary>
        /// 
        /// </summary>
        public const string REGEX_COMMAND_LINE_LOG_OFF = "^-log=off";

        /// <summary>
        /// 
        /// </summary>
        public const string REGEX_COMMAND_LINE_DEBUG = "^-debug";

        /// <summary>
        /// 
        /// </summary>
        public const string REGEX_COMMAND_LINE_DEBUG_ON = "^-debug=on";

        /// <summary>
        /// 
        /// </summary>
        public const string REGEX_COMMAND_LINE_DEBUG_OFF = "^-debug=off";

        /// <summary>
        /// 
        /// </summary>
        public const string REGEX_COMMAND_LINE_CAPTURE = "^-capture";

        /// <summary>
        /// 
        /// </summary>
        public const string REGEX_COMMAND_LINE_START = "^-start";

        /// <summary>
        /// 
        /// </summary>
        public const string REGEX_COMMAND_LINE_STOP = "^-stop";

        /// <summary>
        /// 
        /// </summary>
        public const string REGEX_COMMAND_LINE_EXIT = "^-exit";
    }
}
