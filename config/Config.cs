//-----------------------------------------------------------------------
// <copyright file="Config.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;

    /// <summary>
    /// 
    /// </summary>
    public static class Config
    {
        private const string REGEX_SCREENSHOTS_FOLDER = "^ScreenshotsFolder=(?<Path>.+)$";
        private const string REGEX_DEBUG_FOLDER = "^DebugFolder=(?<Path>.+)$";
        private const string REGEX_LOGS_FOLDER = "^LogsFolder=(?<Path>.+)$";

        private const string REGEX_APPLICATION_SETTINGS_FILE = "^ApplicationSettingsFile=(?<Path>.+)$";
        private const string REGEX_USER_SETTINGS_FILE = "^UserSettingsFile=(?<Path>.+)$";
        private const string REGEX_EDITORS_FILE = "^EditorsFile=(?<Path>.+)$";
        private const string REGEX_REGIONS_FILE = "^RegionsFile=(?<Path>.+)$";
        private const string REGEX_SCREENS_FILE = "^ScreensFile=(?<Path>.+)$";
        private const string REGEX_TRIGGERS_FILE = "^TriggersFile=(?<Path>.+)$";
        private const string REGEX_SCREENSHOTS_FILE = "^ScreenshotsFile=(?<Path>.+)$";

        /// <summary>
        /// 
        /// </summary>
        public static void Load()
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(FileSystem.ConfigFile)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(FileSystem.ConfigFile));
                }

                if (!File.Exists(FileSystem.ConfigFile))
                {
                    string[] linesToWrite =
                    {
                        "ScreenshotsFolder=screenshots",
                        "DebugFolder=!autoscreen\\debug",
                        "LogsFolder=!autoscreen\\debug\\logs",
                        "ApplicationSettingsFile=!autoscreen\\settings\\application.xml",
                        "UserSettingsFile=!autoscreen\\settings\\user.xml",
                        "EditorsFile=!autoscreen\\editors.xml",
                        "RegionsFile=!autoscreen\\regions.xml",
                        "ScreensFile=!autoscreen\\screens.xml",
                        "TriggersFile=!autoscreen\\triggers.xml",
                        "ScreenshotsFile=!autoscreen\\screenshots.xml"
                    };

                    File.WriteAllLines(FileSystem.ConfigFile, linesToWrite);
                }

                foreach (string line in File.ReadAllLines(FileSystem.ConfigFile))
                {
                    string path;

                    if (GetPath(line, REGEX_SCREENSHOTS_FOLDER, out path))
                        FileSystem.ScreenshotsFolder = path;

                    if (GetPath(line, REGEX_DEBUG_FOLDER, out path))
                        FileSystem.DebugFolder = path;

                    if (GetPath(line, REGEX_LOGS_FOLDER, out path))
                        FileSystem.LogsFolder = path;

                    if (GetPath(line, REGEX_APPLICATION_SETTINGS_FILE, out path))
                        FileSystem.ApplicationSettingsFile = path;

                    if (GetPath(line, REGEX_USER_SETTINGS_FILE, out path))
                        FileSystem.UserSettingsFile = path;

                    if (GetPath(line, REGEX_EDITORS_FILE, out path))
                        FileSystem.EditorsFile = path;

                    if (GetPath(line, REGEX_REGIONS_FILE, out path))
                        FileSystem.RegionsFile = path;

                    if (GetPath(line, REGEX_SCREENS_FILE, out path))
                        FileSystem.ScreensFile = path;

                    if (GetPath(line, REGEX_TRIGGERS_FILE, out path))
                        FileSystem.TriggersFile = path;

                    if (GetPath(line, REGEX_SCREENSHOTS_FILE, out path))
                        FileSystem.ScreenshotsFile = path;
                }
            }
            catch (Exception ex)
            {
                Log.Write("Config::Load", ex);
            }
        }

        private static bool GetPath(string line, string regex, out string path)
        {
            if (!Regex.IsMatch(line, regex))
            {
                path = null;
                return false;
            }

            path = Regex.Match(line, regex).Groups["Path"].Value;

            // This is probably a folder.
            if (!Path.HasExtension(path))
            {
                if (!path.EndsWith(Path.DirectorySeparatorChar.ToString()))
                {
                    // And it probably doesn't have a backslash at the end.
                    path += Path.DirectorySeparatorChar;
                }
            }

            // This might be a folder?
            string dir = Path.GetDirectoryName(path);

            // Create the folder if it really is a folder otherwise
            // it's most likely a file instead (in which case just ignore this part).
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            return true;
        }
    }
}
