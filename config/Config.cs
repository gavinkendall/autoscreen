//-----------------------------------------------------------------------
// <copyright file="Config.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace AutoScreenCapture
{
    /// <summary>
    /// The configuration file is used to define the folders and files that the application will use.
    /// </summary>
    public static class Config
    {
        private const string REGEX_SCREENSHOTS_FOLDER = "^ScreenshotsFolder=(?<Path>.+)$";
        private const string REGEX_DEBUG_FOLDER = "^DebugFolder=(?<Path>.+)$";
        private const string REGEX_LOGS_FOLDER = "^LogsFolder=(?<Path>.+)$";
        private const string REGEX_COMMAND_FOLDER = "^CommandFolder=(?<Path>.+)$";

        private const string REGEX_APPLICATION_SETTINGS_FILE = "^ApplicationSettingsFile=(?<Path>.+)$";
        private const string REGEX_USER_SETTINGS_FILE = "^UserSettingsFile=(?<Path>.+)$";
        private const string REGEX_EDITORS_FILE = "^EditorsFile=(?<Path>.+)$";
        private const string REGEX_REGIONS_FILE = "^RegionsFile=(?<Path>.+)$";
        private const string REGEX_SCREENS_FILE = "^ScreensFile=(?<Path>.+)$";
        private const string REGEX_TRIGGERS_FILE = "^TriggersFile=(?<Path>.+)$";
        private const string REGEX_SCREENSHOTS_FILE = "^ScreenshotsFile=(?<Path>.+)$";
        private const string REGEX_TAGS_FILE = "^TagsFile=(?<Path>.+)$";
        private const string REGEX_SCHEDULES_FILE = "^SchedulesFile=(?<Path>.+)$";

        /// <summary>
        /// Loads the configuration file.
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
                        "# where the executed autoscreen.exe binary is located.", "",
                        "# This is the folder where screenshots will be stored by default.",
                        "ScreenshotsFolder=screenshots\\", "",
                        "# If any errors are encountered then you will find them in this folder when DebugMode is enabled.",
                        "DebugFolder=!autoscreen\\debug\\", "",
                        "# Logs are stored in this folder when either Logging or DebugMode is enabled.",
                        "LogsFolder=!autoscreen\\logs\\", "",
                        "# This folder is monitored by the application for commands issued from the command line while it's running.",
                        "CommandFolder=!autoscreen\\command\\", "",
                        "# The application settings (such as DebugMode).",
                        "ApplicationSettingsFile=!autoscreen\\settings\\application.xml", "",
                        "# Your personal settings.",
                        "UserSettingsFile=!autoscreen\\settings\\user.xml", "",
                        "# References to image editors.",
                        "EditorsFile=!autoscreen\\editors.xml", "",
                        "# References to regions.",
                        "RegionsFile=!autoscreen\\regions.xml", "",
                        "# References to screens.",
                        "ScreensFile=!autoscreen\\screens.xml", "",
                        "# References to triggers.",
                        "TriggersFile=!autoscreen\\triggers.xml", "",
                        "# References to screenshots.",
                        "ScreenshotsFile=!autoscreen\\screenshots.xml", "",
                        "# References to tags.",
                        "TagsFile=!autoscreen\\tags.xml", "",
                        "# References to schedules.",
                        "SchedulesFile=!autoscreen\\schedules.xml"
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

                    if (GetPath(line, REGEX_COMMAND_FOLDER, out path))
                        FileSystem.CommandFolder = path;

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

                    if (GetPath(line, REGEX_SCHEDULES_FILE, out path))
                        FileSystem.SchedulesFile = path;
                }
            }
            catch (Exception ex)
            {
                Log.Write("Config::Load", ex);
            }
        }

        /// <summary>
        /// Gets the path from the configuration file based on what line is being processed and a regex pattern.
        /// </summary>
        /// <param name="line">The line to read from the file.</param>
        /// <param name="regex">The regex pattern to use against the line.</param>
        /// <param name="path">The output of the path being returned.</param>
        /// <returns>A boolean to indicate if getting a path was successful or not.</returns>
        private static bool GetPath(string line, string regex, out string path)
        {
            if (line.StartsWith("#") || !Regex.IsMatch(line, regex))
            {
                path = null;
                return false;
            }

            path = Regex.Match(line, regex).Groups["Path"].Value;

            TagCollection tagCollection = new TagCollection();
            tagCollection.Add(new Tag("user", TagType.User, enabled: true));
            tagCollection.Add(new Tag("machine", TagType.Machine, enabled: true));

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
