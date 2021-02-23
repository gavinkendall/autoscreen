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
    public class Config
    {
        private Log _log;
        private FileSystem _fileSystem;
        private MacroParser _macroParser;

        private const string REGEX_SCREENSHOTS_FOLDER = "^ScreenshotsFolder=(?<Path>.+)$";
        private const string REGEX_DEBUG_FOLDER = "^DebugFolder=(?<Path>.+)$";
        private const string REGEX_LOGS_FOLDER = "^LogsFolder=(?<Path>.+)$";

        private const string REGEX_COMMAND_FILE = "^CommandFile=(?<Path>.+)$";
        private const string REGEX_APPLICATION_SETTINGS_FILE = "^ApplicationSettingsFile=(?<Path>.+)$";
        private const string REGEX_SMTP_SETTINGS_FILE = "^SMTPSettingsFile=(?<Path>.+)$";
        private const string REGEX_SFTP_SETTINGS_FILE = "^SFTPSettingsFile=(?<Path>.+)$";
        private const string REGEX_USER_SETTINGS_FILE = "^UserSettingsFile=(?<Path>.+)$";
        private const string REGEX_EDITORS_FILE = "^EditorsFile=(?<Path>.+)$";
        private const string REGEX_REGIONS_FILE = "^RegionsFile=(?<Path>.+)$";
        private const string REGEX_SCREENS_FILE = "^ScreensFile=(?<Path>.+)$";
        private const string REGEX_TRIGGERS_FILE = "^TriggersFile=(?<Path>.+)$";
        private const string REGEX_SCREENSHOTS_FILE = "^ScreenshotsFile=(?<Path>.+)$";
        private const string REGEX_TAGS_FILE = "^TagsFile=(?<Path>.+)$";
        private const string REGEX_SCHEDULES_FILE = "^SchedulesFile=(?<Path>.+)$";

        /// <summary>
        /// A collection of settings.
        /// </summary>
        public Settings Settings { get; set; }

        /// <summary>
        /// A class for handling configuration.
        /// </summary>
        /// <param name="filesystem"></param>
        /// <param name="macroParser"></param>
        /// <param name="settings"></param>
        /// <param name="log"></param>
        public Config(FileSystem filesystem, MacroParser macroParser, Settings settings, Log log)
        {
            _log = log;
            _fileSystem = filesystem;
            _macroParser = macroParser;

            Settings = settings;
        }

        /// <summary>
        /// Loads the configuration file.
        /// </summary>
        public void Load(ScreenCapture screenCapture, Log log)
        {
            try
            {
                if (!_fileSystem.DirectoryExists(_fileSystem.GetDirectoryName(_fileSystem.ConfigFile)))
                {
                    _fileSystem.CreateDirectory(_fileSystem.GetDirectoryName(_fileSystem.ConfigFile));
                }

                if (!_fileSystem.FileExists(_fileSystem.ConfigFile))
                {
                    string[] linesToWrite =
                    {
                        "# Auto Screen Capture Configuration File",
                        "# Use this file to tell the application what folders and files it should utilize.",
                        "# Each key-value pair can be the name of a folder or file or a path to a folder or file.",
                        "# If only the folder name is given then it will be parsed as the sub-folder of the folder",
                        "# where the executed autoscreen.exe binary is located.", "",
                        "# This is the folder where screenshots will be stored by default.",
                        "ScreenshotsFolder=" + _fileSystem.DefaultScreenshotsFolder, "",
                        "# If any errors are encountered then you will find them in this folder when DebugMode is enabled.",
                        "DebugFolder=" + _fileSystem.DefaultDebugFolder, "",
                        "# Logs are stored in this folder when either Logging or DebugMode is enabled.",
                        "LogsFolder=" + _fileSystem.DefaultLogsFolder, "",
                        "# This file is monitored by the application for commands issued from the command line while it's running.",
                        "CommandFile=" + _fileSystem.DefaultCommandFile, "",
                        "# The application settings (such as DebugMode).",
                        "ApplicationSettingsFile=" + _fileSystem.DefaultApplicationSettingsFile, "",
                        "# Your personal settings.",
                        "UserSettingsFile=" + _fileSystem.DefaultUserSettingsFile, "",
                        "# SMTP settings for emailing screenshots using an email server.",
                        "SMTPSettingsFile=" + _fileSystem.DefaultSmtpSettingsFile, "",
                        "# SFTP settings for uploading screenshots to a file server.",
                        "SFTPSettingsFile=" + _fileSystem.DefaultSftpSettingsFile, "",
                        "# References to image editors.",
                        "EditorsFile=" + _fileSystem.DefaultEditorsFile, "",
                        "# References to regions.",
                        "RegionsFile=" + _fileSystem.DefaultRegionsFile, "",
                        "# References to screens.",
                        "ScreensFile=" + _fileSystem.DefaultScreensFile, "",
                        "# References to triggers.",
                        "TriggersFile=" + _fileSystem.DefaultTriggersFile, "",
                        "# References to screenshots.",
                        "ScreenshotsFile=" + _fileSystem.DefaultScreenshotsFile, "",
                        "# References to tags.",
                        "TagsFile=" + _fileSystem.DefaultTagsFile, "",
                        "# References to schedules.",
                        "SchedulesFile=" + _fileSystem.DefaultSchedulesFile, ""
                    };

                    _fileSystem.WriteToFile(_fileSystem.ConfigFile, linesToWrite);
                }

                foreach (string line in _fileSystem.ReadFromFile(_fileSystem.ConfigFile))
                {
                    if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                    {
                        continue;
                    }

                    string path;

                    if (GetPath(line, REGEX_SCREENSHOTS_FOLDER, out path))
                        _fileSystem.ScreenshotsFolder = path;

                    if (GetPath(line, REGEX_DEBUG_FOLDER, out path))
                        _fileSystem.DebugFolder = path;

                    if (GetPath(line, REGEX_LOGS_FOLDER, out path))
                        _fileSystem.LogsFolder = path;

                    if (GetPath(line, REGEX_COMMAND_FILE, out path))
                        _fileSystem.CommandFile = path;

                    if (GetPath(line, REGEX_APPLICATION_SETTINGS_FILE, out path))
                        _fileSystem.ApplicationSettingsFile = path;

                    if (GetPath(line, REGEX_SMTP_SETTINGS_FILE, out path))
                        _fileSystem.SmtpSettingsFile = path;

                    if (GetPath(line, REGEX_SFTP_SETTINGS_FILE, out path))
                        _fileSystem.SftpSettingsFile = path;

                    if (GetPath(line, REGEX_USER_SETTINGS_FILE, out path))
                        _fileSystem.UserSettingsFile = path;

                    if (GetPath(line, REGEX_EDITORS_FILE, out path))
                        _fileSystem.EditorsFile = path;

                    if (GetPath(line, REGEX_REGIONS_FILE, out path))
                        _fileSystem.RegionsFile = path;

                    if (GetPath(line, REGEX_SCREENS_FILE, out path))
                        _fileSystem.ScreensFile = path;

                    if (GetPath(line, REGEX_TRIGGERS_FILE, out path))
                        _fileSystem.TriggersFile = path;

                    if (GetPath(line, REGEX_SCREENSHOTS_FILE, out path))
                        _fileSystem.ScreenshotsFile = path;

                    if (GetPath(line, REGEX_TAGS_FILE, out path))
                        _fileSystem.TagsFile = path;

                    if (GetPath(line, REGEX_SCHEDULES_FILE, out path))
                        _fileSystem.SchedulesFile = path;
                }

                CheckAndCreateFolders();

                Security security = new Security();

                CheckAndCreateFiles(security, screenCapture, log);
            }
            catch (Exception ex)
            {
                log.WriteExceptionMessage("Config::Load", ex);
            }
        }

        // Check the folders to make sure that each folder was included in the config file and the folder exists.
        private void CheckAndCreateFolders()
        {
            if (string.IsNullOrEmpty(_fileSystem.ScreenshotsFolder))
            {
                _fileSystem.ScreenshotsFolder = _fileSystem.DefaultScreenshotsFolder;

                _fileSystem.AppendToFile(_fileSystem.ConfigFile, "\nScreenshotsFolder=" + _fileSystem.DefaultScreenshotsFolder);

                if (!_fileSystem.DirectoryExists(_fileSystem.DefaultScreenshotsFolder))
                {
                    _fileSystem.CreateDirectory(_fileSystem.DefaultScreenshotsFolder);
                }
            }

            if (string.IsNullOrEmpty(_fileSystem.DebugFolder))
            {
                _fileSystem.DebugFolder = _fileSystem.DefaultDebugFolder;

                _fileSystem.AppendToFile(_fileSystem.ConfigFile, "\nDebugFolder=" + _fileSystem.DefaultDebugFolder);

                if (!_fileSystem.DirectoryExists(_fileSystem.DefaultDebugFolder))
                {
                    _fileSystem.CreateDirectory(_fileSystem.DefaultDebugFolder);
                }
            }

            if (string.IsNullOrEmpty(_fileSystem.LogsFolder))
            {
                _fileSystem.LogsFolder = _fileSystem.DefaultLogsFolder;

                _fileSystem.AppendToFile(_fileSystem.ConfigFile, "\nLogsFolder=" + _fileSystem.DefaultLogsFolder);

                if (!_fileSystem.DirectoryExists(_fileSystem.DefaultLogsFolder))
                {
                    _fileSystem.CreateDirectory(_fileSystem.DefaultLogsFolder);
                }
            }
        }

        private void CheckAndCreateFiles(Security security, ScreenCapture screenCapture, Log log)
        {
            if (string.IsNullOrEmpty(_fileSystem.CommandFile))
            {
                _fileSystem.CommandFile = _fileSystem.DefaultCommandFile;

                _fileSystem.AppendToFile(_fileSystem.ConfigFile, "\nCommandFile=" + _fileSystem.DefaultCommandFile);

                if (!_fileSystem.FileExists(_fileSystem.DefaultCommandFile))
                {
                    _fileSystem.CreateFile(_fileSystem.DefaultCommandFile);
                }
            }

            if (string.IsNullOrEmpty(_fileSystem.ApplicationSettingsFile))
            {
                _fileSystem.ApplicationSettingsFile = _fileSystem.DefaultApplicationSettingsFile;

                _fileSystem.AppendToFile(_fileSystem.ConfigFile, "\nApplicationSettingsFile=" + _fileSystem.DefaultApplicationSettingsFile);

                if (!_fileSystem.DirectoryExists(_fileSystem.DefaultSettingsFolder))
                {
                    _fileSystem.CreateDirectory(_fileSystem.DefaultSettingsFolder);
                }
            }

            if (string.IsNullOrEmpty(_fileSystem.SmtpSettingsFile))
            {
                _fileSystem.SmtpSettingsFile = _fileSystem.DefaultSmtpSettingsFile;

                _fileSystem.AppendToFile(_fileSystem.ConfigFile, "\nSMTPSettingsFile=" + _fileSystem.DefaultSmtpSettingsFile);

                if (!_fileSystem.DirectoryExists(_fileSystem.DefaultSettingsFolder))
                {
                    _fileSystem.CreateDirectory(_fileSystem.DefaultSettingsFolder);
                }
            }

            if (string.IsNullOrEmpty(_fileSystem.SftpSettingsFile))
            {
                _fileSystem.SftpSettingsFile = _fileSystem.DefaultSftpSettingsFile;

                _fileSystem.AppendToFile(_fileSystem.ConfigFile, "\nSFTPSettingsFile=" + _fileSystem.DefaultSftpSettingsFile);

                if (!_fileSystem.DirectoryExists(_fileSystem.DefaultSettingsFolder))
                {
                    _fileSystem.CreateDirectory(_fileSystem.DefaultSettingsFolder);
                }
            }

            if (string.IsNullOrEmpty(_fileSystem.UserSettingsFile))
            {
                _fileSystem.UserSettingsFile = _fileSystem.DefaultUserSettingsFile;

                _fileSystem.AppendToFile(_fileSystem.ConfigFile, "\nUserSettingsFile=" + _fileSystem.DefaultUserSettingsFile);

                if (!_fileSystem.DirectoryExists(_fileSystem.DefaultSettingsFolder))
                {
                    _fileSystem.CreateDirectory(_fileSystem.DefaultSettingsFolder);
                }
            }

            log.WriteMessage("Loading user settings");
            Settings.User.Load(Settings, _fileSystem);

            log.WriteMessage("Loading SMTP settings");
            Settings.SMTP.Load(Settings, _fileSystem);

            log.WriteMessage("Loading SFTP settings");
            Settings.SFTP.Load(Settings, _fileSystem);

            log.WriteDebugMessage("Preparing old application settings for potential upgrade");
            Settings.VersionManager.OldApplicationSettings = Settings.Application.Clone();

            log.WriteDebugMessage("Preparing old user settings for potential upgrade");
            Settings.VersionManager.OldUserSettings = Settings.User.Clone();

            log.WriteDebugMessage("Attempting upgrade of application settings from old version of application (if needed)");
            Settings.UpgradeApplicationSettings(Settings.Application, _fileSystem, log);

            log.WriteDebugMessage("Attempting upgrade of user settings from old version of application (if needed)");
            Settings.UpgradeUserSettings(Settings.User, screenCapture, security, _fileSystem, log);

            log.WriteDebugMessage("Attempting upgrade of SMTP settings from old version of application (if needed)");
            Settings.UpgradeSmtpSettings(Settings.SMTP, _fileSystem, log);

            log.WriteDebugMessage("Attempting upgrade of SFTP settings from old version of application (if needed)");
            Settings.UpgradeSftpSettings(Settings.SFTP, _fileSystem, log);

            if (string.IsNullOrEmpty(_fileSystem.ScreenshotsFile))
            {
                ImageFormatCollection imageFormatCollection = new ImageFormatCollection();
                ScreenCollection screenCollection = new ScreenCollection();

                ScreenshotCollection screenshotCollection = new ScreenshotCollection(imageFormatCollection, screenCollection, screenCapture, this, _fileSystem, log);
                screenshotCollection.SaveToXmlFile(this);
            }

            if (string.IsNullOrEmpty(_fileSystem.EditorsFile))
            {
                // Loading the editor collection will automatically create the default editors and add them to the collection.
                EditorCollection editorCollection = new EditorCollection();
                editorCollection.LoadXmlFileAndAddEditors(this, _fileSystem, log);
            }

            if (string.IsNullOrEmpty(_fileSystem.RegionsFile))
            {
                RegionCollection regionCollection = new RegionCollection();
                regionCollection.SaveToXmlFile(Settings, _fileSystem, log);
            }

            if (string.IsNullOrEmpty(_fileSystem.ScreensFile))
            {
                // Loading the screen collection will automatically create the available screens and add them to the collection.
                ScreenCollection screenCollection = new ScreenCollection();
                screenCollection.LoadXmlFileAndAddScreens(new ImageFormatCollection(), this, _macroParser, screenCapture, _fileSystem, log);
            }

            if (string.IsNullOrEmpty(_fileSystem.TriggersFile))
            {
                // Loading triggers will automatically create the default triggers and add them to the collection.
                TriggerCollection triggerCollection = new TriggerCollection();
                triggerCollection.LoadXmlFileAndAddTriggers(this, _fileSystem, log);
            }

            if (string.IsNullOrEmpty(_fileSystem.TagsFile))
            {
                // Loading tags will automatically create the default tags and add them to the collection.
                MacroTagCollection tagCollection = new MacroTagCollection();
                tagCollection.LoadXmlFileAndAddTags(this, _macroParser, _fileSystem, log);
            }

            if (string.IsNullOrEmpty(_fileSystem.SchedulesFile))
            {
                // Loading schedules will automatically create the default schedules and add them to the collection.
                ScheduleCollection scheduleCollection = new ScheduleCollection();
                scheduleCollection.LoadXmlFileAndAddSchedules(this, _fileSystem, log);
            }
        }

        /// <summary>
        /// Gets the path from the configuration file based on what line is being processed and a regex pattern.
        /// </summary>
        /// <param name="line">The line to read from the file.</param>
        /// <param name="regex">The regex pattern to use against the line.</param>
        /// <param name="path">The output of the path being returned.</param>
        /// <returns>A boolean to indicate if getting a path was successful or not.</returns>
        private bool GetPath(string line, string regex, out string path)
        {
            if (!Regex.IsMatch(line, regex))
            {
                path = null;
                return false;
            }

            path = Regex.Match(line, regex).Groups["Path"].Value;

            MacroTagCollection tagCollection = new MacroTagCollection();
            tagCollection.Add(new MacroTag(_macroParser, "user", "The user using this computer (%user%)", MacroTagType.User, active: true));
            tagCollection.Add(new MacroTag(_macroParser, "machine", "The name of the computer (%machine%)", MacroTagType.Machine, active: true));

            path = _macroParser.ParseTags(config: true, path, tagCollection, _log);

            if (_fileSystem.HasExtension(path))
            {
                string dir = _fileSystem.GetDirectoryName(path);

                if (!string.IsNullOrEmpty(dir) && !_fileSystem.DirectoryExists(dir))
                {
                    _fileSystem.CreateDirectory(dir);
                }
            }
            else
            {
                if (!path.EndsWith(_fileSystem.DirectorySeparatorChar().ToString()))
                {
                    path += _fileSystem.DirectorySeparatorChar();
                }

                if (!string.IsNullOrEmpty(path) && !_fileSystem.DirectoryExists(path))
                {
                    _fileSystem.CreateDirectory(path);
                }
            }

            return true;
        }
    }
}
