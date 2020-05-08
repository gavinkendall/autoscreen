//-----------------------------------------------------------------------
// <copyright file="CommandLineRegex.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>The regular expressions used for command line arguments.</summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    /// <summary>
    /// A class that contains all the regex we use for parsing command line arguments given to the application
    /// either from a command line terminal or from the "command" file.
    /// </summary>
    public static class CommandLineRegex
    {
        /// <summary>
        /// Regex for parsing the -config command.
        /// </summary>
        public const string REGEX_COMMAND_LINE_CONFIG = "^-config=(?<ConfigFile>.+)$";

        /// <summary>
        /// Regex for parsing the -initial command.
        /// </summary>
        public const string REGEX_COMMAND_LINE_INITIAL = "^-initial$";

        /// <summary>
        /// Regex for parsing the -initial=on command.
        /// </summary>
        public const string REGEX_COMMAND_LINE_INITIAL_ON = "^-initial=on$";

        /// <summary>
        /// Regex for parsing the -initial=off command.
        /// </summary>
        public const string REGEX_COMMAND_LINE_INITIAL_OFF = "^-initial=off$";

        /// <summary>
        /// Regex for parsing the -limit command.
        /// </summary>
        public const string REGEX_COMMAND_LINE_LIMIT = @"^-limit=(?<Limit>\d{1,7})$";

        /// <summary>
        /// Regex for parsing the -captureat command.
        /// </summary>
        public const string REGEX_COMMAND_LINE_CAPTUREAT = @"^-captureat=(?<Hours>\d{2}):(?<Minutes>\d{2}):(?<Seconds>\d{2})$";

        /// <summary>
        /// Regex for parsing the -stopat command.
        /// </summary>
        public const string REGEX_COMMAND_LINE_STOPAT = @"^-stopat=(?<Hours>\d{2}):(?<Minutes>\d{2}):(?<Seconds>\d{2})$";

        /// <summary>
        /// Regex for parsing the -startat command.
        /// </summary>
        public const string REGEX_COMMAND_LINE_STARTAT = @"^-startat=(?<Hours>\d{2}):(?<Minutes>\d{2}):(?<Seconds>\d{2})$";

        /// <summary>
        /// Regex for parsing the -interval command.
        /// </summary>
        public const string REGEX_COMMAND_LINE_INTERVAL = @"^-interval=(?<Hours>\d{2}):(?<Minutes>\d{2}):(?<Seconds>\d{2})\.(?<Milliseconds>\d{3})$";

        /// <summary>
        /// Regex for parsing the -passphrase command.
        /// </summary>
        public const string REGEX_COMMAND_LINE_PASSPHRASE = "^-passphrase=(?<Passphrase>.+)$";

        /// <summary>
        /// Regex for parsing the -showSystemTrayIcon command.
        /// </summary>
        public const string REGEX_COMMAND_LINE_SHOW_SYSTEM_TRAY_ICON = "^-showSystemTrayIcon$";

        /// <summary>
        /// Regex for parsing the -hideSystemTrayIcon command.
        /// </summary>
        public const string REGEX_COMMAND_LINE_HIDE_SYSTEM_TRAY_ICON = "^-hideSystemTrayIcon$";

        /// <summary>
        /// Regex for parsing the -log command.
        /// </summary>
        public const string REGEX_COMMAND_LINE_LOG = "^-log$";

        /// <summary>
        /// Regex for parsing the -log=on command.
        /// </summary>
        public const string REGEX_COMMAND_LINE_LOG_ON = "^-log=on$";

        /// <summary>
        /// Regex for parsing the -log=off command.
        /// </summary>
        public const string REGEX_COMMAND_LINE_LOG_OFF = "^-log=off$";

        /// <summary>
        /// Regex for parsing the -debug command.
        /// </summary>
        public const string REGEX_COMMAND_LINE_DEBUG = "^-debug$";

        /// <summary>
        /// Regex for parsing the -debug=on command.
        /// </summary>
        public const string REGEX_COMMAND_LINE_DEBUG_ON = "^-debug=on$";

        /// <summary>
        /// Regex for parsing the -debug=off command.
        /// </summary>
        public const string REGEX_COMMAND_LINE_DEBUG_OFF = "^-debug=off$";

        /// <summary>
        /// Regex for parsing the -capture command.
        /// </summary>
        public const string REGEX_COMMAND_LINE_CAPTURE = "^-capture$";

        /// <summary>
        /// Regex for parsing the -start command.
        /// </summary>
        public const string REGEX_COMMAND_LINE_START = "^-start$";

        /// <summary>
        /// Regex for parsing the -stop command.
        /// </summary>
        public const string REGEX_COMMAND_LINE_STOP = "^-stop$";

        /// <summary>
        /// Regex for parsing the -exit command.
        /// </summary>
        public const string REGEX_COMMAND_LINE_EXIT = "^-exit$";
    }
}
