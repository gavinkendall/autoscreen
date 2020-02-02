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
        private const string REGEX_TAGS_FILE = "^TagsFile=(?<Path>.+)$";

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
                        "# Auto Screen Capture Configuration File",
                        "# Use this file to tell the application what folders and files it should utilize.",
                        "# Each key-value pair can be the name of a folder or file or a path to a folder or file.",
                        "# If only the folder name is given then it will be parsed as the sub-folder of the folder",
                        "# where the executed autoscreen.exe binary is located.", "\n",
                        "# This is the folder where screenshots will be stored by default.",
                        "ScreenshotsFolder=screenshots", "\n",
                        "# If any errors are encountered then you will find them in this folder when DebugMode is enabled.",
                        "DebugFolder=!autoscreen\\debug", "\n",
                        "# Logs are stored in this folder when either Logging or DebugMode is enabled.",
                        "LogsFolder=!autoscreen\\logs", "\n",
                        "# The application settings (such as DebugMode).",
                        "ApplicationSettingsFile=!autoscreen\\settings\\application.xml", "\n",
                        "# Your personal settings.",
                        "UserSettingsFile=!autoscreen\\settings\\user.xml", "\n",
                        "# References to image editors.",
                        "EditorsFile=!autoscreen\\editors.xml", "\n",
                        "# References to regions.",
                        "RegionsFile=!autoscreen\\regions.xml", "\n",
                        "# References to screens.",
                        "ScreensFile=!autoscreen\\screens.xml", "\n",
                        "# References to triggers.",
                        "TriggersFile=!autoscreen\\triggers.xml", "\n",
                        "# References to screenshots.",
                        "ScreenshotsFile=!autoscreen\\screenshots.xml", "\n",
                        "# References to tags.",
                        "TagsFile=!autoscreen\\tags.xml"
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

                    if (GetPath(line, REGEX_TAGS_FILE, out path))
                        FileSystem.TagsFile = path;
                }
            }
            catch (Exception ex)
            {
                Log.Write("Config::Load", ex);
            }
        }

        private static bool GetPath(string line, string regex, out string path)
        {
            if (line.StartsWith("#") || !Regex.IsMatch(line, regex))
            {
                path = null;
                return false;
            }

            path = Regex.Match(line, regex).Groups["Path"].Value;

            TagCollection tagCollection = new TagCollection();
            tagCollection.Add(new Tag("user", TagType.User, string.Empty, DateTime.MinValue, DateTime.MinValue, string.Empty, DateTime.MinValue, DateTime.MinValue, string.Empty, DateTime.MinValue, DateTime.MinValue, string.Empty));
            tagCollection.Add(new Tag("machine", TagType.Machine, string.Empty, DateTime.MinValue, DateTime.MinValue, string.Empty, DateTime.MinValue, DateTime.MinValue, string.Empty, DateTime.MinValue, DateTime.MinValue, string.Empty));

            path = MacroParser.ParseTagsForFolderPath(path, tagCollection);

            if (Path.HasExtension(path))
            {
                string dir = Path.GetDirectoryName(path);

                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
            }
            else
            {
                if (!path.EndsWith(Path.DirectorySeparatorChar.ToString()))
                {
                    path += Path.DirectorySeparatorChar;
                }

                if (!string.IsNullOrEmpty(path) && !Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }

            return true;
        }
    }
}
