//-----------------------------------------------------------------------
// <copyright file="Config.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>The Auto Screen Capture Configuration File is a special file for specifying the folders and files to be used by Auto Screen Capture.</summary>
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

        private const string REGEX_COMMAND_FILE = "^CommandFile=(?<Path>.+)$";
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
                if (!FileSystem.DirectoryExists(FileSystem.GetDirectoryName(FileSystem.ConfigFile)))
                {
                    FileSystem.CreateDirectory(FileSystem.GetDirectoryName(FileSystem.ConfigFile));
                }

                if (!FileSystem.FileExists(FileSystem.ConfigFile))
                {
                    string[] linesToWrite =
                    {
                        "# Auto Screen Capture Configuration File",
                        "# Use this file to tell the application what folders and files it should utilize.",
                        "# Each key-value pair can be the name of a folder or file or a path to a folder or file.",
                        "# If only the folder name is given then it will be parsed as the sub-folder of the folder",
                        "# where the executed autoscreen.exe binary is located.", "",
                        "# This is the folder where screenshots will be stored by default.",
                        "ScreenshotsFolder=" + FileSystem.DefaultScreenshotsFolder, "",
                        "# If any errors are encountered then you will find them in this folder when DebugMode is enabled.",
                        "DebugFolder=" + FileSystem.DefaultDebugFolder, "",
                        "# Logs are stored in this folder when either Logging or DebugMode is enabled.",
                        "LogsFolder=" + FileSystem.DefaultLogsFolder, "",
                        "# This file is monitored by the application for commands issued from the command line while it's running.",
                        "CommandFile=" + FileSystem.DefaultCommandFile, "",
                        "# The application settings (such as DebugMode).",
                        "ApplicationSettingsFile=" + FileSystem.DefaultApplicationSettingsFile, "",
                        "# Your personal settings.",
                        "UserSettingsFile=" + FileSystem.DefaultUserSettingsFile, "",
                        "# References to image editors.",
                        "EditorsFile=" + FileSystem.DefaultEditorsFile, "",
                        "# References to regions.",
                        "RegionsFile=" + FileSystem.DefaultRegionsFile, "",
                        "# References to screens.",
                        "ScreensFile=" + FileSystem.DefaultScreensFile, "",
                        "# References to triggers.",
                        "TriggersFile=" + FileSystem.DefaultTriggersFile, "",
                        "# References to screenshots.",
                        "ScreenshotsFile=" + FileSystem.DefaultScreenshotsFile, "",
                        "# References to tags.",
                        "TagsFile=" + FileSystem.DefaultTagsFile, "",
                        "# References to schedules.",
                        "SchedulesFile=" + FileSystem.DefaultSchedulesFile, ""
                    };

                    FileSystem.WriteToFile(FileSystem.ConfigFile, linesToWrite);
                }

                foreach (string line in FileSystem.ReadFromFile(FileSystem.ConfigFile))
                {
                    if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                    {
                        continue;
                    }

                    string path;

                    if (GetPath(line, REGEX_SCREENSHOTS_FOLDER, out path))
                        FileSystem.ScreenshotsFolder = path;

                    if (GetPath(line, REGEX_DEBUG_FOLDER, out path))
                        FileSystem.DebugFolder = path;

                    if (GetPath(line, REGEX_LOGS_FOLDER, out path))
                        FileSystem.LogsFolder = path;

                    if (GetPath(line, REGEX_COMMAND_FILE, out path))
                        FileSystem.CommandFile = path;

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

                CheckAndCreateFolders();

                CheckAndCreateFiles();
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("Config::Load", ex);
            }
        }

        // Check the folders to make sure that each folder was included in the config file and the folder exists.
        private static void CheckAndCreateFolders()
        {
            if (string.IsNullOrEmpty(FileSystem.ScreenshotsFolder))
            {
                FileSystem.ScreenshotsFolder = FileSystem.DefaultScreenshotsFolder;

                FileSystem.AppendToFile(FileSystem.ConfigFile, "\nScreenshotsFolder=" + FileSystem.DefaultScreenshotsFolder);

                if (!FileSystem.DirectoryExists(FileSystem.DefaultScreenshotsFolder))
                {
                    FileSystem.CreateDirectory(FileSystem.DefaultScreenshotsFolder);
                }
            }

            if (string.IsNullOrEmpty(FileSystem.DebugFolder))
            {
                FileSystem.DebugFolder = FileSystem.DefaultDebugFolder;

                FileSystem.AppendToFile(FileSystem.ConfigFile, "\nDebugFolder=" + FileSystem.DefaultDebugFolder);

                if (!FileSystem.DirectoryExists(FileSystem.DefaultDebugFolder))
                {
                    FileSystem.CreateDirectory(FileSystem.DefaultDebugFolder);
                }
            }

            if (string.IsNullOrEmpty(FileSystem.LogsFolder))
            {
                FileSystem.LogsFolder = FileSystem.DefaultLogsFolder;

                FileSystem.AppendToFile(FileSystem.ConfigFile, "\nLogsFolder=" + FileSystem.DefaultLogsFolder);

                if (!FileSystem.DirectoryExists(FileSystem.DefaultLogsFolder))
                {
                    FileSystem.CreateDirectory(FileSystem.DefaultLogsFolder);
                }
            }
        }

        private static void CheckAndCreateFiles()
        {
            if (string.IsNullOrEmpty(FileSystem.CommandFile))
            {
                FileSystem.CommandFile = FileSystem.DefaultCommandFile;

                FileSystem.AppendToFile(FileSystem.ConfigFile, "\nCommandFile=" + FileSystem.DefaultCommandFile);

                if (!FileSystem.FileExists(FileSystem.DefaultCommandFile))
                {
                    FileSystem.CreateFile(FileSystem.DefaultCommandFile);
                }
            }

            if (string.IsNullOrEmpty(FileSystem.ApplicationSettingsFile))
            {
                FileSystem.ApplicationSettingsFile = FileSystem.DefaultApplicationSettingsFile;

                FileSystem.AppendToFile(FileSystem.ConfigFile, "\nApplicationSettingsFile=" + FileSystem.DefaultApplicationSettingsFile);

                if (!FileSystem.DirectoryExists(FileSystem.DefaultSettingsFolder))
                {
                    FileSystem.CreateDirectory(FileSystem.DefaultSettingsFolder);
                }
            }

            if (string.IsNullOrEmpty(FileSystem.UserSettingsFile))
            {
                FileSystem.UserSettingsFile = FileSystem.DefaultUserSettingsFile;

                FileSystem.AppendToFile(FileSystem.ConfigFile, "\nUserSettingsFile=" + FileSystem.DefaultUserSettingsFile);

                if (!FileSystem.DirectoryExists(FileSystem.DefaultSettingsFolder))
                {
                    FileSystem.CreateDirectory(FileSystem.DefaultSettingsFolder);
                }
            }

            Settings.Initialize();

            Log.WriteMessage("Loading user settings");
            Settings.User.Load();
            Log.WriteDebugMessage("User settings loaded");

            Log.WriteDebugMessage("Attempting upgrade of application settings from old version of application (if needed)");
            Settings.UpgradeApplicationSettings(Settings.Application);

            Log.WriteDebugMessage("Attempting upgrade of user settings from old version of application (if needed)");
            Settings.UpgradeUserSettings(Settings.User);

            if (string.IsNullOrEmpty(FileSystem.ScreenshotsFile))
            {
                ImageFormatCollection imageFormatCollection = new ImageFormatCollection();
                ScreenCollection screenCollection = new ScreenCollection();

                ScreenshotCollection screenshotCollection = new ScreenshotCollection(imageFormatCollection, screenCollection);
                screenshotCollection.SaveToXmlFile();
            }

            if (string.IsNullOrEmpty(FileSystem.EditorsFile))
            {
                // Loading the editor collection will automatically create the default editors and add them to the collection.
                EditorCollection editorCollection = new EditorCollection();
                editorCollection.LoadXmlFileAndAddEditors();
            }

            if (string.IsNullOrEmpty(FileSystem.RegionsFile))
            {
                RegionCollection regionCollection = new RegionCollection();
                regionCollection.SaveToXmlFile();
            }

            if (string.IsNullOrEmpty(FileSystem.ScreensFile))
            {
                // Loading the screen collection will automatically create the available screens and add them to the collection.
                ScreenCollection screenCollection = new ScreenCollection();
                screenCollection.LoadXmlFileAndAddScreens(new ImageFormatCollection());
            }

            if (string.IsNullOrEmpty(FileSystem.TriggersFile))
            {
                // Loading triggers will automatically create the default triggers and add them to the collection.
                TriggerCollection triggerCollection = new TriggerCollection();
                triggerCollection.LoadXmlFileAndAddTriggers();
            }

            if (string.IsNullOrEmpty(FileSystem.TagsFile))
            {
                // Loading tags will automatically create the default tags and add them to the collection.
                TagCollection tagCollection = new TagCollection();
                tagCollection.LoadXmlFileAndAddTags();
            }

            if (string.IsNullOrEmpty(FileSystem.SchedulesFile))
            {
                // Loading schedules will automatically create the default schedules and add them to the collection.
                ScheduleCollection scheduleCollection = new ScheduleCollection();
                scheduleCollection.LoadXmlFileAndAddSchedules();
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
            if (!Regex.IsMatch(line, regex))
            {
                path = null;
                return false;
            }

            path = Regex.Match(line, regex).Groups["Path"].Value;

            TagCollection tagCollection = new TagCollection();
            tagCollection.Add(new Tag("user", "The user using this computer (%user%)", TagType.User, active: true));
            tagCollection.Add(new Tag("machine", "The name of the computer (%machine%)", TagType.Machine, active: true));

            path = MacroParser.ParseTags(config: true, path, tagCollection);

            if (FileSystem.HasExtension(path))
            {
                string dir = FileSystem.GetDirectoryName(path);

                if (!string.IsNullOrEmpty(dir) && !FileSystem.DirectoryExists(dir))
                {
                    FileSystem.CreateDirectory(dir);
                }
            }
            else
            {
                if (!path.EndsWith(FileSystem.DirectorySeparatorChar().ToString()))
                {
                    path += FileSystem.DirectorySeparatorChar();
                }

                if (!string.IsNullOrEmpty(path) && !FileSystem.DirectoryExists(path))
                {
                    FileSystem.CreateDirectory(path);
                }
            }

            return true;
        }
    }
}
