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
                        "ScreenshotsFolder=" + FileSystem.DefaultScreenshotsFolder, "",
                        "# If any errors are encountered then you will find them in this folder when DebugMode is enabled.",
                        "DebugFolder=" + FileSystem.DefaultDebugFolder, "",
                        "# Logs are stored in this folder when either Logging or DebugMode is enabled.",
                        "LogsFolder=" + FileSystem.DefaultLogsFolder, "",
                        "# This folder is monitored by the application for commands issued from the command line while it's running.",
                        "CommandFolder=" + FileSystem.DefaultCommandFolder, "",
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

                using (StreamWriter sw = File.AppendText(FileSystem.ConfigFile))
                {
                    sw.WriteLine("\nScreenshotsFolder=" + FileSystem.DefaultScreenshotsFolder);
                }

                if (!Directory.Exists(FileSystem.DefaultScreenshotsFolder))
                {
                    Directory.CreateDirectory(FileSystem.DefaultScreenshotsFolder);
                }
            }

            if (string.IsNullOrEmpty(FileSystem.DebugFolder))
            {
                FileSystem.DebugFolder = FileSystem.DefaultDebugFolder;

                using (StreamWriter sw = File.AppendText(FileSystem.ConfigFile))
                {
                    sw.WriteLine("\nDebugFolder=" + FileSystem.DefaultDebugFolder);
                }

                if (!Directory.Exists(FileSystem.DefaultDebugFolder))
                {
                    Directory.CreateDirectory(FileSystem.DefaultDebugFolder);
                }
            }

            if (string.IsNullOrEmpty(FileSystem.LogsFolder))
            {
                FileSystem.LogsFolder = FileSystem.DefaultLogsFolder;

                using (StreamWriter sw = File.AppendText(FileSystem.ConfigFile))
                {
                    sw.WriteLine("\nLogsFolder=" + FileSystem.DefaultLogsFolder);
                }

                if (!Directory.Exists(FileSystem.DefaultLogsFolder))
                {
                    Directory.CreateDirectory(FileSystem.DefaultLogsFolder);
                }
            }

            if (string.IsNullOrEmpty(FileSystem.CommandFolder))
            {
                FileSystem.CommandFolder = FileSystem.DefaultCommandFolder;

                using (StreamWriter sw = File.AppendText(FileSystem.ConfigFile))
                {
                    sw.WriteLine("\nCommandFolder=" + FileSystem.DefaultCommandFolder);
                }

                if (!Directory.Exists(FileSystem.DefaultCommandFolder))
                {
                    Directory.CreateDirectory(FileSystem.DefaultCommandFolder);
                }
            }
        }

        private static void CheckAndCreateFiles()
        {
            if (string.IsNullOrEmpty(FileSystem.ApplicationSettingsFile))
            {
                FileSystem.ApplicationSettingsFile = FileSystem.DefaultApplicationSettingsFile;

                using (StreamWriter sw = File.AppendText(FileSystem.ConfigFile))
                {
                    sw.WriteLine("\nApplicationSettingsFile=" + FileSystem.DefaultApplicationSettingsFile);
                }

                if (!Directory.Exists(FileSystem.DefaultSettingsFolder))
                {
                    Directory.CreateDirectory(FileSystem.DefaultSettingsFolder);
                }

                SettingCollection applicationSettingsCollection = new SettingCollection
                {
                    Filepath = FileSystem.ApplicationSettingsFile
                };

                applicationSettingsCollection.Save();
            }

            if (string.IsNullOrEmpty(FileSystem.UserSettingsFile))
            {
                FileSystem.UserSettingsFile = FileSystem.DefaultUserSettingsFile;

                using (StreamWriter sw = File.AppendText(FileSystem.ConfigFile))
                {
                    sw.WriteLine("\nUserSettingsFile=" + FileSystem.DefaultUserSettingsFile);
                }

                if (!Directory.Exists(FileSystem.DefaultSettingsFolder))
                {
                    Directory.CreateDirectory(FileSystem.DefaultSettingsFolder);
                }

                SettingCollection userSettingsCollection = new SettingCollection
                {
                    Filepath = FileSystem.ApplicationSettingsFile
                };

                userSettingsCollection.Save();
            }

            if (string.IsNullOrEmpty(FileSystem.ScreenshotsFile))
            {
                ImageFormatCollection imageFormatCollection = new ImageFormatCollection();
                ScreenCollection screenCollection = new ScreenCollection();

                ScreenshotCollection screenshotCollection = new ScreenshotCollection(imageFormatCollection, screenCollection);
                screenshotCollection.SaveToXmlFile(0);
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
