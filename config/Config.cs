//-----------------------------------------------------------------------
// <copyright file="Config.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>The Auto Screen Capture Configuration File.</summary>
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
        // Screen Capture Interval
        private const string REGEX_SCREEN_CAPTURE_INTERVAL = "^ScreenCaptureInterval=(?<ScreenCaptureInterval>\\d{1,6})$";

        // Capture Limit
        private const string REGEX_CAPTURE_LIMIT = "^CaptureLimit=(?<CaptureLimit>\\d{1,3})$";

        // Take Initial Screenshot
        private const string REGEX_TAKE_INITIAL_SCREENSHOT = "^TakeInitialScreenshot=(?<TakeInitialScreenshot>False|True)$";

        // Save Screenshot Refs
        private const string REGEX_SAVE_SCREENSHOT_REFS = "^SaveScreenshotRefs=(?<SaveScreenshotRefs>False|True)$";

        // Optimize Screen Capture
        private const string REGEX_OPTIMIZE_SCREEN_CAPTURE = "^OptimizeScreenCapture=(?<OptimizeScreenCapture>False|True)$";

        // Image Diff Tolerance
        private const string REGEX_IMAGE_DIFF_TOLERANCE = "^ImageDiffTolerance=(?<ImageDiffTolerance>\\d{1,3})$";

        // Image Format
        private const string REGEX_IMAGE_FORMAT = "^ImageFormat=(?<ImageFormat>[A-Z]{3,4})$";

        // Filename Pattern
        private const string REGEX_FILENAME_PATTERN = "^FilenamePattern=(?<FilenamePattern>.+)$";

        // Default Editor
        private const string REGEX_DEFAULT_EDITOR = "^DefaultEditor=(?<DefaultEditor>.+)$";

        // Folders
        private const string REGEX_SCREENSHOTS_FOLDER = "^ScreenshotsFolder=(?<Path>.+)$";
        private const string REGEX_ERRORS_FOLDER = "^ErrorsFolder=(?<Path>.+)$";
        private const string REGEX_LOGS_FOLDER = "^LogsFolder=(?<Path>.+)$";

        // Auto Save
        private const string REGEX_AUTO_SAVE_FOLDER = "^AutoSaveFolder=(?<AutoSaveFolder>.+)$";
        private const string REGEX_AUTO_SAVE_MACRO = "^AutoSaveMacro=(?<AutoSaveMacro>.+)$";
        private const string REGEX_AUTO_SAVE_FORMAT = "^AutoSaveFormat=(?<AutoSaveFormat>[A-Z]{3,4})$";

        // Capture Now
        private const string REGEX_CAPTURE_NOW_MACRO = "^CaptureNowMacro=(?<CaptureNowMacro>.+)$";

        // Preview
        private const string REGEX_PREVIEW = "^Preview=(?<Preview>False|True)$";

        // DebugMode
        private const string REGEX_DEBUG_MODE = "^DebugMode=(?<DebugMode>False|True)$";

        // Logging
        private const string REGEX_LOGGING = "^Logging=(?<Logging>False|True)$";

        // SneakyPastaSnake
        private const string REGEX_SNEAKY_PASTA_SNAKE = "^SneakyPastaSnake=(?<SneakyPastaSnake>False|True)$";

        // Commands
        private const string REGEX_COMMAND_FILE = "^CommandFile=(?<Path>.+)$";

        // Settings
        private const string REGEX_APPLICATION_SETTINGS_FILE = "^ApplicationSettingsFile=(?<Path>.+)$";
        private const string REGEX_USER_SETTINGS_FILE = "^UserSettingsFile=(?<Path>.+)$";
        private const string REGEX_SMTP_SETTINGS_FILE = "^SMTPSettingsFile=(?<Path>.+)$";
        private const string REGEX_SFTP_SETTINGS_FILE = "^SFTPSettingsFile=(?<Path>.+)$";

        // Modules
        private const string REGEX_SCREENS_FILE = "^ScreensFile=(?<Path>.+)$";
        private const string REGEX_REGIONS_FILE = "^RegionsFile=(?<Path>.+)$";
        private const string REGEX_EDITORS_FILE = "^EditorsFile=(?<Path>.+)$";
        private const string REGEX_SCHEDULES_FILE = "^SchedulesFile=(?<Path>.+)$";
        private const string REGEX_MACRO_TAGS_FILE = "^MacroTagsFile=(?<Path>.+)$";
        private const string REGEX_TRIGGERS_FILE = "^TriggersFile=(?<Path>.+)$";

        // Screenshots
        private const string REGEX_SCREENSHOTS_FILE = "^ScreenshotsFile=(?<Path>.+)$";

        // Screen Definition Regex
        private const string REGEX_SCREEN = "^Screen::\\[Enable=(?<Enable>False|True), Name=\"(?<Name>.+)\", Folder=\"(?<Folder>.+)\", Macro=\"(?<Macro>.+)\", Source=(?<Source>\\d{1}), Component=(?<Component>\\d{1}), CaptureMethod=(?<CaptureMethod>\\d{1}), X=(?<X>\\d{1,4}), Y=(?<Y>\\d{1,4}), Width=(?<Width>\\d{1,4}), Height=(?<Height>\\d{1,4}), AutoAdapt=(?<AutoAdapt>False|True), Format=(?<Format>[A-Z]{3,4}), JPEGQuality=(?<JPEGQuality>\\d{1,3}), ResolutionRatio=(?<ResolutionRatio>\\d{1,3}), Mouse=(?<Mouse>False|True), Encrypt=(?<Encrypt>False|True)\\]$";

        // Region Definition Regex
        private const string REGEX_REGION = "^Region::\\[Enable=(?<Enable>False|True), Name=\"(?<Name>.+)\", Folder=\"(?<Folder>.+)\", Macro=\"(?<Macro>.+)\", X=(?<X>\\d{1,4}), Y=(?<Y>\\d{1,4}), Width=(?<Width>\\d{1,4}), Height=(?<Height>\\d{1,4}), Format=(?<Format>[A-Z]{3,4}), JPEGQuality=(?<JPEGQuality>\\d{1,3}), Mouse=(?<Mouse>False|True), Encrypt=(?<Encrypt>False|True)\\]$";

        // Editor Definition Regex
        private const string REGEX_EDITOR = "^Editor::\\[Name=\"(?<Name>.+)\", ApplicationPath=\"(?<ApplicationPath>.+)\", ApplicationArguments=\"(?<ApplicationArguments>.+)\", Notes=\"(?<Notes>.*)\"\\]$";

        // Schedule Definition Regex
        private const string REGEX_SCHEDULE = "^Schedule::\\[Enable=(?<Enable>False|True), Name=\"(?<Name>.+)\", Scope=\"(?<Scope>.+)\", OneTime=(?<OneTime>False|True), Period=(?<Period>False|True), Logic=(?<Logic>\\d{1}), CaptureAt=(?<CaptureAt>\\d{2}:\\d{2}), StartAt=(?<StartAt>\\d{2}:\\d{2}), StopAt=(?<StopAt>\\d{2}:\\d{2}), Interval=(?<Interval>\\d{1,6}), Monday=(?<Monday>False|True), Tuesday=(?<Tuesday>False|True), Wednesday=(?<Wednesday>False|True), Thursday=(?<Thursday>False|True), Friday=(?<Friday>False|True), Saturday=(?<Saturday>False|True), Sunday=(?<Sunday>False|True), Notes=\"(?<Notes>.*)\"\\]$";

        // Macro Tag Definition Regex
        private const string REGEX_MACRO_TAG = "^MacroTag::\\[Enable=(?<Enable>False|True), Name=\"(?<Name>.+)\", Description=\"(?<Description>.+)\", Type=\"(?<Type>.+)\", DateTimeFormatValue=\"(?<DateTimeFormatValue>.+)\", Macro1TimeRangeStart=(?<Macro1TimeRangeStart>\\d{2}:\\d{2}:\\d{2}), Macro1TimeRangeEnd=(?<Macro1TimeRangeEnd>\\d{2}:\\d{2}:\\d{2}), Macro1TimeRangeMacro=\"(?<Macro1TimeRangeMacro>.*)\", Macro2TimeRangeStart=(?<Macro2TimeRangeStart>\\d{2}:\\d{2}:\\d{2}), Macro2TimeRangeEnd=(?<Macro2TimeRangeEnd>\\d{2}:\\d{2}:\\d{2}), Macro2TimeRangeMacro=\"(?<Macro2TimeRangeMacro>.*)\", Macro3TimeRangeStart=(?<Macro3TimeRangeStart>\\d{2}:\\d{2}:\\d{2}), Macro3TimeRangeEnd=(?<Macro3TimeRangeEnd>\\d{2}:\\d{2}:\\d{2}), Macro3TimeRangeMacro=\"(?<Macro3TimeRangeMacro>.*)\", Macro4TimeRangeStart=(?<Macro4TimeRangeStart>\\d{2}:\\d{2}:\\d{2}), Macro4TimeRangeEnd=(?<Macro4TimeRangeEnd>\\d{2}:\\d{2}:\\d{2}), Macro4TimeRangeMacro=\"(?<Macro4TimeRangeMacro>.*)\", Notes=\"(?<Notes>.*)\"\\]$";

        // Trigger Definition Regex
        private const string REGEX_TRIGGER = "^Trigger::\\[Enable=(?<Enable>False|True), Name=\"(?<Name>.+)\", Condition=(?<Condition>.+), Action=(?<Action>.+), Date=(?<Date>\\d{4}-\\d{2}-\\d{2}), Time=(?<Time>\\d{2}:\\d{2}), Day=(?<Day>Weekday|Weekend|Monday|Tuesday|Wednesday|Thursday|Friday|Saturday|Sunday), Days=(?<Days>\\d{1,8}), Interval=(?<Interval>\\d{1,6}), CycleCount=(?<CycleCount>\\d{1,8}), Duration=(?<Duration>\\d{1,8}), DurationType=(?<DurationType>\\d{1}), Value=\"(?<Value>.*)\"\\]$";

        /// <summary>
        /// A collection of default screens.
        /// </summary>
        private ScreenCollection _screenCollection;

        /// <summary>
        /// A collection of default regions.
        /// </summary>
        private RegionCollection _regionCollection;

        /// <summary>
        /// A collection of default editors.
        /// </summary>
        private EditorCollection _editorCollection;

        /// <summary>
        /// A collection of default schedules.
        /// </summary>
        private ScheduleCollection _scheduleCollection;

        /// <summary>
        /// A collection of default triggers.
        /// </summary>
        private TriggerCollection _triggerCollection;

        /// <summary>
        /// A collection of default macro tags.
        /// </summary>
        private MacroTagCollection _macroTagCollection;

        /// <summary>
        /// Determines if we do a clean startup. This means we do not load the XML data files. By default we load the XML data files.
        /// </summary>
        public bool CleanStartup { get; set; }

        /// <summary>
        /// The log file to use.
        /// </summary>
        public Log Log { get; set; }

        /// <summary>
        /// The settings for the application or the user.
        /// </summary>
        public Settings Settings { get; set; }

        /// <summary>
        /// Access to file system methods.
        /// </summary>
        public FileSystem FileSystem { get; private set; }

        /// <summary>
        /// A parser for parsing macro tags.
        /// </summary>
        public MacroParser MacroParser { get; set; }

        /// <summary>
        /// A class for handling configuration of the application.
        /// </summary>
        public Config()
        {

        }

        /// <summary>
        /// Loads the configuration file.
        /// </summary>
        /// <param name="fileSystem">The file system to use.</param>
        /// <param name="cleanStartup">Determines if we do a clean startup. This means we do not load the XML data files. By default we load the XML data files.</param>
        /// <returns>True if load successful otherwise False if load failed.</returns>
        public bool Load(FileSystem fileSystem, bool cleanStartup = false)
        {
            try
            {
                CleanStartup = cleanStartup;

                FileSystem = fileSystem;

                Settings = new Settings();
                MacroParser = new MacroParser(Settings);

                string configDirectory = fileSystem.GetDirectoryName(fileSystem.ConfigFile);

                if (!string.IsNullOrEmpty(configDirectory) && !fileSystem.DirectoryExists(configDirectory))
                {
                    fileSystem.CreateDirectory(configDirectory);
                }

                if (fileSystem.FileExists(fileSystem.ConfigFile))
                {
                    // We need to parse and load all default macro tags before parsing the paths used for files and folders
                    // because they may have macro tags in the paths.
                    ParseMacroTagDefinitions();

                    foreach (string line in fileSystem.ReadFromFile(fileSystem.ConfigFile))
                    {
                        if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                        {
                            continue;
                        }

                        string path;

                        if (GetPathAndCreateIfNotFound(line, REGEX_SCREENSHOTS_FOLDER, out path))
                            fileSystem.ScreenshotsFolder = path;

                        if (GetPathAndCreateIfNotFound(line, REGEX_ERRORS_FOLDER, out path))
                            fileSystem.ErrorsFolder = path;

                        if (GetPathAndCreateIfNotFound(line, REGEX_LOGS_FOLDER, out path))
                            fileSystem.LogsFolder = path;

                        if (GetPathAndCreateIfNotFound(line, REGEX_COMMAND_FILE, out path))
                            fileSystem.CommandFile = path;

                        if (GetPathAndCreateIfNotFound(line, REGEX_APPLICATION_SETTINGS_FILE, out path))
                            fileSystem.ApplicationSettingsFile = path;

                        if (GetPathAndCreateIfNotFound(line, REGEX_SMTP_SETTINGS_FILE, out path))
                            fileSystem.SmtpSettingsFile = path;

                        if (GetPathAndCreateIfNotFound(line, REGEX_SFTP_SETTINGS_FILE, out path))
                            fileSystem.SftpSettingsFile = path;

                        if (GetPathAndCreateIfNotFound(line, REGEX_USER_SETTINGS_FILE, out path))
                            fileSystem.UserSettingsFile = path;

                        if (GetPathAndCreateIfNotFound(line, REGEX_EDITORS_FILE, out path))
                            fileSystem.EditorsFile = path;

                        if (GetPathAndCreateIfNotFound(line, REGEX_REGIONS_FILE, out path))
                            fileSystem.RegionsFile = path;

                        if (GetPathAndCreateIfNotFound(line, REGEX_SCREENS_FILE, out path))
                            fileSystem.ScreensFile = path;

                        if (GetPathAndCreateIfNotFound(line, REGEX_TRIGGERS_FILE, out path))
                            fileSystem.TriggersFile = path;

                        if (GetPathAndCreateIfNotFound(line, REGEX_SCREENSHOTS_FILE, out path))
                            fileSystem.ScreenshotsFile = path;

                        if (GetPathAndCreateIfNotFound(line, REGEX_MACRO_TAGS_FILE, out path))
                            fileSystem.MacroTagsFile = path;

                        // This is for backwards compability for versions of the application that are older than 2.4
                        if (string.IsNullOrEmpty(fileSystem.MacroTagsFile) && GetPathAndCreateIfNotFound(line, "^TagsFile=(?<Path>.+)$", out path))
                            fileSystem.MacroTagsFile = path;

                        if (GetPathAndCreateIfNotFound(line, REGEX_SCHEDULES_FILE, out path))
                            fileSystem.SchedulesFile = path;
                    }

                    CheckAndCreateFolders();

                    Settings.Load(fileSystem);

                    ParseDefaultUserSettings();

                    Log = new Log(Settings, fileSystem, MacroParser);
                    ScreenCapture screenCapture = new ScreenCapture(this, fileSystem, Log);
                    Security security = new Security(fileSystem);

                    CheckAndCreateFiles(security, screenCapture, Log);

                    // Parse all the definitions in the configuration file for the various types of modules.
                    ParseScreenDefinitions();
                    ParseRegionDefinitions();
                    ParseEditorDefinitions();
                    ParseScheduleDefinitions();
                    ParseTriggerDefinitions();

                    // Save the data for each collection that's been loaded from the configuration file.

                    // Save the screen collection if the screens data file (screens.xml) cannot be found. This will create the default screens.
                    if (!FileSystem.FileExists(FileSystem.ScreensFile))
                    {
                        _screenCollection.SaveToXmlFile(Settings, FileSystem, Log);
                    }

                    // Save the region collection if the regions data file (regions.xml) cannot be found. This will create the default regions.
                    if (!FileSystem.FileExists(FileSystem.RegionsFile))
                    {
                        _regionCollection.SaveToXmlFile(Settings, FileSystem, Log);
                    }

                    // Save the editor collection if the editors data file (editors.xml) cannot be found. This will create the default editors.
                    if (!FileSystem.FileExists(FileSystem.EditorsFile))
                    {
                        _editorCollection.SaveToXmlFile(Settings, FileSystem, Log);
                    }

                    // Save the schedule collection if the schedules data file (schedules.xml) cannot be found. This will create the default schedules.
                    if (!FileSystem.FileExists(FileSystem.SchedulesFile))
                    {
                        _scheduleCollection.SaveToXmlFile(Settings, FileSystem, Log);
                    }

                    // Save the macro tag collection if the macro tags data file (macrotags.xml) cannot be found. This will create the default macro tags.
                    if (!FileSystem.FileExists(FileSystem.MacroTagsFile))
                    {
                        _macroTagCollection.SaveToXmlFile(this, FileSystem, Log);
                    }

                    // Save the trigger collection if the triggers data file (triggers.xml) cannot be found. This will create the default triggers.
                    if (!FileSystem.FileExists(FileSystem.TriggersFile))
                    {
                        _triggerCollection.SaveToXmlFile(this, FileSystem, Log);
                    }

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("Config::Load", ex);

                return false;
            }
        }

        /// <summary>
        /// Check the folders to make sure that each folder was included in the config file and the folder exists.
        /// </summary>
        private void CheckAndCreateFolders()
        {
            try
            {
                if (string.IsNullOrEmpty(FileSystem.ScreenshotsFolder))
                {
                    FileSystem.ScreenshotsFolder = MacroParser.ParseTags(FileSystem.DefaultScreenshotsFolder, _macroTagCollection, Log);

                    FileSystem.AppendToFile(FileSystem.ConfigFile, "\nScreenshotsFolder=" + FileSystem.ScreenshotsFolder);

                    if (!FileSystem.DirectoryExists(FileSystem.ScreenshotsFolder))
                    {
                        FileSystem.CreateDirectory(FileSystem.ScreenshotsFolder);
                    }
                }

                if (string.IsNullOrEmpty(FileSystem.ErrorsFolder))
                {
                    FileSystem.ErrorsFolder = MacroParser.ParseTags(FileSystem.DefaultErrorsFolder, _macroTagCollection, Log);

                    FileSystem.AppendToFile(FileSystem.ConfigFile, "\nErrorsFolder=" + FileSystem.ErrorsFolder);

                    if (!FileSystem.DirectoryExists(FileSystem.ErrorsFolder))
                    {
                        FileSystem.CreateDirectory(FileSystem.ErrorsFolder);
                    }
                }

                if (string.IsNullOrEmpty(FileSystem.LogsFolder))
                {
                    FileSystem.LogsFolder = MacroParser.ParseTags(FileSystem.DefaultLogsFolder, _macroTagCollection, Log);

                    FileSystem.AppendToFile(FileSystem.ConfigFile, "\nLogsFolder=" + FileSystem.LogsFolder);

                    if (!FileSystem.DirectoryExists(FileSystem.LogsFolder))
                    {
                        FileSystem.CreateDirectory(FileSystem.LogsFolder);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("Config::CheckAndCreateFolders", ex);
            }
        }

        /// <summary>
        /// Check to see if the configured files exist and, if they don't, create them.
        /// There might be a permissions issue if these files are attempting to be created in folders that don't have the correct permissions.
        /// </summary>
        /// <param name="security">The security class to use.</param>
        /// <param name="screenCapture">The screen capture class to use.</param>
        /// <param name="log">The logging class to use.</param>
        private void CheckAndCreateFiles(Security security, ScreenCapture screenCapture, Log log)
        {
            try
            {
                if (string.IsNullOrEmpty(FileSystem.CommandFile))
                {
                    FileSystem.CommandFile = MacroParser.ParseTags(FileSystem.DefaultCommandFile, _macroTagCollection, log);

                    FileSystem.AppendToFile(FileSystem.ConfigFile, "\nCommandFile=" + FileSystem.CommandFile);

                    if (!FileSystem.FileExists(FileSystem.CommandFile))
                    {
                        FileSystem.CreateFile(FileSystem.CommandFile);
                    }
                }

                if (string.IsNullOrEmpty(FileSystem.ApplicationSettingsFile))
                {
                    FileSystem.ApplicationSettingsFile = MacroParser.ParseTags(FileSystem.DefaultApplicationSettingsFile, _macroTagCollection, log);

                    FileSystem.AppendToFile(FileSystem.ConfigFile, "\nApplicationSettingsFile=" + FileSystem.ApplicationSettingsFile);

                    if (!FileSystem.DirectoryExists(FileSystem.DefaultSettingsFolder))
                    {
                        FileSystem.CreateDirectory(FileSystem.DefaultSettingsFolder);
                    }
                }

                if (string.IsNullOrEmpty(FileSystem.SmtpSettingsFile))
                {
                    FileSystem.SmtpSettingsFile = MacroParser.ParseTags(FileSystem.DefaultSmtpSettingsFile, _macroTagCollection, log);

                    FileSystem.AppendToFile(FileSystem.ConfigFile, "\nSMTPSettingsFile=" + FileSystem.SmtpSettingsFile);

                    if (!FileSystem.DirectoryExists(FileSystem.DefaultSettingsFolder))
                    {
                        FileSystem.CreateDirectory(FileSystem.DefaultSettingsFolder);
                    }
                }

                if (string.IsNullOrEmpty(FileSystem.SftpSettingsFile))
                {
                    FileSystem.SftpSettingsFile = MacroParser.ParseTags(FileSystem.DefaultSftpSettingsFile, _macroTagCollection, log);

                    FileSystem.AppendToFile(FileSystem.ConfigFile, "\nSFTPSettingsFile=" + FileSystem.SftpSettingsFile);

                    if (!FileSystem.DirectoryExists(FileSystem.DefaultSettingsFolder))
                    {
                        FileSystem.CreateDirectory(FileSystem.DefaultSettingsFolder);
                    }
                }

                if (string.IsNullOrEmpty(FileSystem.UserSettingsFile))
                {
                    FileSystem.UserSettingsFile = MacroParser.ParseTags(FileSystem.DefaultUserSettingsFile, _macroTagCollection, log);

                    FileSystem.AppendToFile(FileSystem.ConfigFile, "\nUserSettingsFile=" + FileSystem.UserSettingsFile);

                    if (!FileSystem.DirectoryExists(FileSystem.DefaultSettingsFolder))
                    {
                        FileSystem.CreateDirectory(FileSystem.DefaultSettingsFolder);
                    }
                }

                Settings.User.Load(Settings, FileSystem);

                Settings.SMTP.Load(Settings, FileSystem);

                Settings.SFTP.Load(Settings, FileSystem);

                Settings.VersionManager.OldApplicationSettings = Settings.Application.Clone();

                Settings.VersionManager.OldUserSettings = Settings.User.Clone();

                Settings.UpgradeApplicationSettings(Settings.Application, FileSystem);

                Settings.UpgradeUserSettings(Settings.User, screenCapture, security, FileSystem);

                Settings.UpgradeSmtpSettings(Settings.SMTP, FileSystem);

                Settings.UpgradeSftpSettings(Settings.SFTP, FileSystem);

                if (string.IsNullOrEmpty(FileSystem.ScreenshotsFile))
                {
                    ImageFormatCollection imageFormatCollection = new ImageFormatCollection();

                    ScreenshotCollection screenshotCollection = new ScreenshotCollection(imageFormatCollection, _screenCollection, screenCapture, this, FileSystem, log, security);
                    screenshotCollection.SaveToXmlFile(this);
                }
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("Config::CheckAndCreateFiles", ex);
            }
        }

        /// <summary>
        /// Gets the path from the configuration file based on what line is being processed and a regex pattern.
        /// If the path cannot be found then the directory or file will be created.
        /// </summary>
        /// <param name="line">The line to read from the file.</param>
        /// <param name="regex">The regex pattern to use against the line.</param>
        /// <param name="path">The output of the path being returned.</param>
        /// <returns>A boolean to indicate if getting a path was successful or not.</returns>
        private bool GetPathAndCreateIfNotFound(string line, string regex, out string path)
        {
            if (!Regex.IsMatch(line, regex))
            {
                path = null;
                return false;
            }

            path = Regex.Match(line, regex).Groups["Path"].Value;

            path = MacroParser.ParseTags(path, _macroTagCollection, Log);

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

        /// <summary>
        /// Parses default user settings found in the configuration file.
        /// </summary>
        private void ParseDefaultUserSettings()
        {
            try
            {
                foreach (string line in FileSystem.ReadFromFile(FileSystem.ConfigFile))
                {
                    if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                    {
                        continue;
                    }

                    if (Regex.IsMatch(line, REGEX_SCREEN_CAPTURE_INTERVAL))
                    {
                        Settings.User.SetValueByKey("ScreenCaptureInterval", Regex.Match(line, REGEX_SCREEN_CAPTURE_INTERVAL).Groups["ScreenCaptureInterval"].Value);
                    }

                    if (Regex.IsMatch(line, REGEX_CAPTURE_LIMIT))
                    {
                        int captureLimit = Convert.ToInt32(Regex.Match(line, REGEX_CAPTURE_LIMIT).Groups["CaptureLimit"].Value);

                        Settings.User.SetValueByKey("CaptureLimit", Regex.Match(line, REGEX_CAPTURE_LIMIT).Groups["CaptureLimit"].Value);

                        if (captureLimit > 0)
                        {
                            Settings.User.SetValueByKey("CaptureLimitCheck", true);
                        }
                    }

                    if (Regex.IsMatch(line, REGEX_FILENAME_PATTERN))
                    {
                        FileSystem.FilenamePattern = Regex.Match(line, REGEX_FILENAME_PATTERN).Groups["FilenamePattern"].Value;
                    }

                    if (Regex.IsMatch(line, REGEX_IMAGE_FORMAT))
                    {
                        ScreenCapture.ImageFormat = Regex.Match(line, REGEX_IMAGE_FORMAT).Groups["ImageFormat"].Value;
                    }

                    if (Regex.IsMatch(line, REGEX_DEFAULT_EDITOR))
                    {
                        Settings.User.SetValueByKey("DefaultEditor", Regex.Match(line, REGEX_DEFAULT_EDITOR).Groups["DefaultEditor"].Value);
                    }

                    if (Regex.IsMatch(line, REGEX_AUTO_SAVE_FOLDER))
                    {
                        Settings.User.SetValueByKey("AutoSaveFolder", Regex.Match(line, REGEX_AUTO_SAVE_FOLDER).Groups["AutoSaveFolder"].Value);
                    }

                    if (Regex.IsMatch(line, REGEX_AUTO_SAVE_MACRO))
                    {
                        Settings.User.SetValueByKey("AutoSaveMacro", Regex.Match(line, REGEX_AUTO_SAVE_MACRO).Groups["AutoSaveMacro"].Value);
                    }

                    if (Regex.IsMatch(line, REGEX_AUTO_SAVE_FORMAT))
                    {
                        Settings.User.SetValueByKey("AutoSaveFormat", Regex.Match(line, REGEX_AUTO_SAVE_FORMAT).Groups["AutoSaveFormat"].Value);
                    }

                    if (Regex.IsMatch(line, REGEX_CAPTURE_NOW_MACRO))
                    {
                        Settings.User.SetValueByKey("CaptureNowMacro", Regex.Match(line, REGEX_CAPTURE_NOW_MACRO).Groups["CaptureNowMacro"].Value);
                    }

                    if (Regex.IsMatch(line, REGEX_PREVIEW))
                    {
                        Settings.User.SetValueByKey("Preview", Regex.Match(line, REGEX_PREVIEW).Groups["Preview"].Value);
                    }

                    if (Regex.IsMatch(line, REGEX_TAKE_INITIAL_SCREENSHOT))
                    {
                        Settings.User.SetValueByKey("TakeInitialScreenshot", Regex.Match(line, REGEX_TAKE_INITIAL_SCREENSHOT).Groups["TakeInitialScreenshot"].Value);
                    }

                    if (Regex.IsMatch(line, REGEX_SAVE_SCREENSHOT_REFS))
                    {
                        Settings.User.SetValueByKey("SaveScreenshotRefs", Regex.Match(line, REGEX_SAVE_SCREENSHOT_REFS).Groups["SaveScreenshotRefs"].Value);
                    }

                    if (Regex.IsMatch(line, REGEX_OPTIMIZE_SCREEN_CAPTURE))
                    {
                        Settings.User.SetValueByKey("OptimizeScreenCapture", Regex.Match(line, REGEX_OPTIMIZE_SCREEN_CAPTURE).Groups["OptimizeScreenCapture"].Value);
                    }

                    if (Regex.IsMatch(line, REGEX_IMAGE_DIFF_TOLERANCE))
                    {
                        Settings.User.SetValueByKey("ImageDiffTolerance", Regex.Match(line, REGEX_IMAGE_DIFF_TOLERANCE).Groups["ImageDiffTolerance"].Value);
                    }

                    if (Regex.IsMatch(line, REGEX_DEBUG_MODE))
                    {
                        Settings.Application.SetValueByKey("DebugMode", Regex.Match(line, REGEX_DEBUG_MODE).Groups["DebugMode"].Value);
                    }

                    if (Regex.IsMatch(line, REGEX_LOGGING))
                    {
                        Settings.Application.SetValueByKey("Logging", Regex.Match(line, REGEX_LOGGING).Groups["Logging"].Value);
                    }

                    if (Regex.IsMatch(line, REGEX_SNEAKY_PASTA_SNAKE))
                    {
                        bool sneakyPastaSnake = Convert.ToBoolean(Regex.Match(line, REGEX_SNEAKY_PASTA_SNAKE).Groups["SneakyPastaSnake"].Value);

                        Settings.User.SetValueByKey("SneakyPastaSnake", Regex.Match(line, REGEX_SNEAKY_PASTA_SNAKE).Groups["SneakyPastaSnake"].Value);

                        if (sneakyPastaSnake)
                        {
                            // Hide everything if you're a sneaky pasta snake.
                            Settings.User.SetValueByKey("ShowInterface", false);
                            Settings.User.SetValueByKey("ShowSystemTrayIcon", false);
                        }
                    }
                }

                // This is for when we didn't find an entry for FilenamePattern.
                // (which can happen if we're running autoscreen.exe with an old autoscreen.conf file not created by version 2.4 or higher)
                if (string.IsNullOrEmpty(FileSystem.FilenamePattern))
                {
                    FileSystem.AppendToFile(FileSystem.ConfigFile, "\nFilenamePattern=" + FileSystem.DefaultFilenamePattern);

                    FileSystem.FilenamePattern = FileSystem.DefaultFilenamePattern;
                }

                // This is for when we didn't find an entry for ImageFormat.
                // (which can happen if we're running autoscreen.exe with an old autoscreen.conf file not created by version 2.4 or higher)
                if (string.IsNullOrEmpty(ScreenCapture.ImageFormat))
                {
                    FileSystem.AppendToFile(FileSystem.ConfigFile, "\nImageFormat=" + ScreenCapture.DefaultImageFormat);

                    ScreenCapture.ImageFormat = ScreenCapture.DefaultImageFormat;
                }
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("Config::ParseDefaultUserSettings", ex);
            }
        }

        /// <summary>
        /// Parses default screen definitions found in the configuration file.
        /// </summary>
        private void ParseScreenDefinitions()
        {
            try
            {
                _screenCollection = new ScreenCollection();

                foreach (string line in FileSystem.ReadFromFile(FileSystem.ConfigFile))
                {
                    if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                    {
                        continue;
                    }

                    if (Regex.IsMatch(line, REGEX_SCREEN))
                    {
                        bool enable = Convert.ToBoolean(Regex.Match(line, REGEX_SCREEN).Groups["Enable"].Value);
                        string name = Regex.Match(line, REGEX_SCREEN).Groups["Name"].Value;
                        string folder = Regex.Match(line, REGEX_SCREEN).Groups["Folder"].Value;
                        string macro = Regex.Match(line, REGEX_SCREEN).Groups["Macro"].Value;
                        int source = Convert.ToInt32(Regex.Match(line, REGEX_SCREEN).Groups["Source"].Value);
                        int component = Convert.ToInt32(Regex.Match(line, REGEX_SCREEN).Groups["Component"].Value);
                        int captureMethod = Convert.ToInt32(Regex.Match(line, REGEX_SCREEN).Groups["CaptureMethod"].Value);
                        int x = Convert.ToInt32(Regex.Match(line, REGEX_SCREEN).Groups["X"].Value);
                        int y = Convert.ToInt32(Regex.Match(line, REGEX_SCREEN).Groups["Y"].Value);
                        int width = Convert.ToInt32(Regex.Match(line, REGEX_SCREEN).Groups["Width"].Value);
                        int height = Convert.ToInt32(Regex.Match(line, REGEX_SCREEN).Groups["Height"].Value);
                        bool autoAdapt = Convert.ToBoolean(Regex.Match(line, REGEX_SCREEN).Groups["AutoAdapt"].Value);
                        string format = Regex.Match(line, REGEX_SCREEN).Groups["Format"].Value;
                        int jpegQuality = Convert.ToInt32(Regex.Match(line, REGEX_SCREEN).Groups["JPEGQuality"].Value);
                        int resolutionRatio = Convert.ToInt32(Regex.Match(line, REGEX_SCREEN).Groups["ResolutionRatio"].Value);
                        bool mouse = Convert.ToBoolean(Regex.Match(line, REGEX_SCREEN).Groups["Mouse"].Value);
                        bool encrypt = Convert.ToBoolean(Regex.Match(line, REGEX_SCREEN).Groups["Encrypt"].Value);

                        Screen screen = new Screen()
                        {
                            ViewId = Guid.NewGuid(),
                            DeviceName = string.Empty,
                            Enable = enable,
                            Name = name,
                            Folder = folder,
                            Macro = macro,
                            Source = source,
                            Component = component,
                            CaptureMethod = captureMethod,
                            X = x,
                            Y = y,
                            Width = width,
                            Height = height,
                            AutoAdapt = autoAdapt,
                            Format = new ImageFormat(format, "." + format.ToLower()),
                            JpegQuality = jpegQuality,
                            ResolutionRatio = resolutionRatio,
                            Mouse = mouse,
                            Encrypt = encrypt
                        };

                        _screenCollection.Add(screen);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("Config::ParseScreenDefinitions", ex);
            }
        }

        /// <summary>
        /// Parses default region definitions found in the configuration file.
        /// </summary>
        private void ParseRegionDefinitions()
        {
            try
            {
                _regionCollection = new RegionCollection();

                foreach (string line in FileSystem.ReadFromFile(FileSystem.ConfigFile))
                {
                    if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                    {
                        continue;
                    }

                    if (Regex.IsMatch(line, REGEX_REGION))
                    {
                        bool enable = Convert.ToBoolean(Regex.Match(line, REGEX_REGION).Groups["Enable"].Value);
                        string name = Regex.Match(line, REGEX_REGION).Groups["Name"].Value;
                        string folder = Regex.Match(line, REGEX_REGION).Groups["Folder"].Value;
                        string macro = Regex.Match(line, REGEX_REGION).Groups["Macro"].Value;
                        int x = Convert.ToInt32(Regex.Match(line, REGEX_REGION).Groups["X"].Value);
                        int y = Convert.ToInt32(Regex.Match(line, REGEX_REGION).Groups["Y"].Value);
                        int width = Convert.ToInt32(Regex.Match(line, REGEX_REGION).Groups["Width"].Value);
                        int height = Convert.ToInt32(Regex.Match(line, REGEX_REGION).Groups["Height"].Value);
                        string format = Regex.Match(line, REGEX_REGION).Groups["Format"].Value;
                        int jpegQuality = Convert.ToInt32(Regex.Match(line, REGEX_REGION).Groups["JPEGQuality"].Value);
                        bool mouse = Convert.ToBoolean(Regex.Match(line, REGEX_REGION).Groups["Mouse"].Value);
                        bool encrypt = Convert.ToBoolean(Regex.Match(line, REGEX_REGION).Groups["Encrypt"].Value);

                        Region region = new Region()
                        {
                            ViewId = Guid.NewGuid(),
                            Enable = enable,
                            Name = name,
                            Folder = folder,
                            Macro = macro,
                            X = x,
                            Y = y,
                            Width = width,
                            Height = height,
                            Format = new ImageFormat(format, "." + format.ToLower()),
                            JpegQuality = jpegQuality,
                            Mouse = mouse,
                            Encrypt = encrypt
                        };

                        _regionCollection.Add(region);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("Config::ParseRegionDefinitions", ex);
            }
        }

        /// <summary>
        /// Parses default editor definitions found in the configuration file.
        /// </summary>
        private void ParseEditorDefinitions()
        {
            try
            {
                _editorCollection = new EditorCollection();

                foreach (string line in FileSystem.ReadFromFile(FileSystem.ConfigFile))
                {
                    if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                    {
                        continue;
                    }

                    if (Regex.IsMatch(line, REGEX_EDITOR))
                    {
                        string name = Regex.Match(line, REGEX_EDITOR).Groups["Name"].Value;
                        string applicationPath = Regex.Match(line, REGEX_EDITOR).Groups["ApplicationPath"].Value;
                        string applicationArguments = Regex.Match(line, REGEX_EDITOR).Groups["ApplicationArguments"].Value;
                        string notes = Regex.Match(line, REGEX_EDITOR).Groups["Notes"].Value;

                        Editor editor = new Editor()
                        {
                            Name = name,
                            Application = applicationPath,
                            Arguments = applicationArguments,
                            Notes = notes
                        };

                        _editorCollection.Add(editor);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("Config::ParseEditorDefinitions", ex);
            }
        }

        /// <summary>
        /// Parses default schedule definitions found in the configuration file.
        /// </summary>
        private void ParseScheduleDefinitions()
        {
            try
            {
                _scheduleCollection = new ScheduleCollection();

                foreach (string line in FileSystem.ReadFromFile(FileSystem.ConfigFile))
                {
                    if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                    {
                        continue;
                    }

                    if (Regex.IsMatch(line, REGEX_SCHEDULE))
                    {
                        bool enable = Convert.ToBoolean(Regex.Match(line, REGEX_SCHEDULE).Groups["Enable"].Value);
                        string name = Regex.Match(line, REGEX_SCHEDULE).Groups["Name"].Value;
                        string scope = Regex.Match(line, REGEX_SCHEDULE).Groups["Scope"].Value;
                        bool oneTime = Convert.ToBoolean(Regex.Match(line, REGEX_SCHEDULE).Groups["OneTime"].Value);
                        bool period = Convert.ToBoolean(Regex.Match(line, REGEX_SCHEDULE).Groups["Period"].Value);
                        int logic = Convert.ToInt32(Regex.Match(line, REGEX_SCHEDULE).Groups["Logic"].Value);
                        string captureAt = Regex.Match(line, REGEX_SCHEDULE).Groups["CaptureAt"].Value;
                        string startAt = Regex.Match(line, REGEX_SCHEDULE).Groups["StartAt"].Value;
                        string stopAt = Regex.Match(line, REGEX_SCHEDULE).Groups["StopAt"].Value;
                        int interval = Convert.ToInt32(Regex.Match(line, REGEX_SCHEDULE).Groups["Interval"].Value);
                        bool monday = Convert.ToBoolean(Regex.Match(line, REGEX_SCHEDULE).Groups["Monday"].Value);
                        bool tuesday = Convert.ToBoolean(Regex.Match(line, REGEX_SCHEDULE).Groups["Tuesday"].Value);
                        bool wednesday = Convert.ToBoolean(Regex.Match(line, REGEX_SCHEDULE).Groups["Wednesday"].Value);
                        bool thursday = Convert.ToBoolean(Regex.Match(line, REGEX_SCHEDULE).Groups["Thursday"].Value);
                        bool friday = Convert.ToBoolean(Regex.Match(line, REGEX_SCHEDULE).Groups["Friday"].Value);
                        bool saturday = Convert.ToBoolean(Regex.Match(line, REGEX_SCHEDULE).Groups["Saturday"].Value);
                        bool sunday = Convert.ToBoolean(Regex.Match(line, REGEX_SCHEDULE).Groups["Sunday"].Value);
                        string notes = Regex.Match(line, REGEX_EDITOR).Groups["Notes"].Value;

                        Schedule schedule = new Schedule()
                        {
                            Enable = enable,
                            Name = name,
                            Scope = scope,
                            ModeOneTime = oneTime,
                            ModePeriod = period,
                            Logic = logic,
                            CaptureAt = DateTime.Parse(captureAt),
                            StartAt = DateTime.Parse(startAt),
                            StopAt = DateTime.Parse(stopAt),
                            ScreenCaptureInterval = interval,
                            Monday = monday,
                            Tuesday = tuesday,
                            Wednesday = wednesday,
                            Thursday = thursday,
                            Friday = friday,
                            Saturday = saturday,
                            Sunday = sunday,
                            Notes = notes
                        };

                        _scheduleCollection.Add(schedule);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("Config::ParseScheduleDefinitions", ex);
            }
        }

        /// <summary>
        /// Parses default trigger definitions found in the configuration file.
        /// </summary>
        private void ParseTriggerDefinitions()
        {
            try
            {
                _triggerCollection = new TriggerCollection();

                foreach (string line in FileSystem.ReadFromFile(FileSystem.ConfigFile))
                {
                    if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                    {
                        continue;
                    }

                    if (Regex.IsMatch(line, REGEX_TRIGGER))
                    {
                        bool enable = Convert.ToBoolean(Regex.Match(line, REGEX_TRIGGER).Groups["Enable"].Value);
                        string name = Regex.Match(line, REGEX_TRIGGER).Groups["Name"].Value;
                        string condition = Regex.Match(line, REGEX_TRIGGER).Groups["Condition"].Value;
                        string action = Regex.Match(line, REGEX_TRIGGER).Groups["Action"].Value;
                        string date = Regex.Match(line, REGEX_TRIGGER).Groups["Date"].Value;
                        string time = Regex.Match(line, REGEX_TRIGGER).Groups["Time"].Value;
                        string day = Regex.Match(line, REGEX_TRIGGER).Groups["Day"].Value;
                        int days = Convert.ToInt32(Regex.Match(line, REGEX_TRIGGER).Groups["Days"].Value);
                        int interval = Convert.ToInt32(Regex.Match(line, REGEX_TRIGGER).Groups["Interval"].Value);
                        int cycleCount = Convert.ToInt32(Regex.Match(line, REGEX_TRIGGER).Groups["CycleCount"].Value);
                        int duration = Convert.ToInt32(Regex.Match(line, REGEX_TRIGGER).Groups["Duration"].Value);
                        int durationType = Convert.ToInt32(Regex.Match(line, REGEX_TRIGGER).Groups["DurationType"].Value);
                        string value = Regex.Match(line, REGEX_TRIGGER).Groups["Value"].Value;

                        Trigger trigger = new Trigger()
                        {
                            Enable = enable,
                            Name = name,
                            ConditionType = Enum.TryParse(condition, out TriggerConditionType triggerConditionType) ? triggerConditionType : TriggerConditionType.ApplicationStartup,
                            ActionType = Enum.TryParse(action, out TriggerActionType triggerActionType) ? triggerActionType : TriggerActionType.ExitApplication,
                            Date = DateTime.Parse(date),
                            Time = DateTime.Parse(time),
                            Day = day,
                            Days = days,
                            ScreenCaptureInterval = interval,
                            CycleCount = cycleCount,
                            Duration = duration,
                            DurationType = durationType,
                            Value = value
                        };

                        _triggerCollection.Add(trigger);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("Config::ParseTriggerDefinitions", ex);
            }
        }

        /// <summary>
        /// Parses default macro tag definitions found in the configuration file.
        /// </summary>
        private void ParseMacroTagDefinitions()
        {
            try
            {
                _macroTagCollection = new MacroTagCollection();

                foreach (string line in FileSystem.ReadFromFile(FileSystem.ConfigFile))
                {
                    if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                    {
                        continue;
                    }

                    if (Regex.IsMatch(line, REGEX_MACRO_TAG))
                    {
                        bool enable = Convert.ToBoolean(Regex.Match(line, REGEX_MACRO_TAG).Groups["Enable"].Value);
                        string name = Regex.Match(line, REGEX_MACRO_TAG).Groups["Name"].Value;
                        string description = Regex.Match(line, REGEX_MACRO_TAG).Groups["Description"].Value;
                        string type = Regex.Match(line, REGEX_MACRO_TAG).Groups["Type"].Value;
                        string dateTimeFormatValue = Regex.Match(line, REGEX_MACRO_TAG).Groups["DateTimeFormatValue"].Value;
                        DateTime macro1TimeRangeStart = DateTime.Parse(Regex.Match(line, REGEX_MACRO_TAG).Groups["Macro1TimeRangeStart"].Value);
                        DateTime macro1TimeRangeEnd = DateTime.Parse(Regex.Match(line, REGEX_MACRO_TAG).Groups["Macro1TimeRangeEnd"].Value);
                        string macro1TimeRangeMacro = Regex.Match(line, REGEX_MACRO_TAG).Groups["Macro1TimeRangeMacro"].Value;
                        DateTime macro2TimeRangeStart = DateTime.Parse(Regex.Match(line, REGEX_MACRO_TAG).Groups["Macro2TimeRangeStart"].Value);
                        DateTime macro2TimeRangeEnd = DateTime.Parse(Regex.Match(line, REGEX_MACRO_TAG).Groups["Macro2TimeRangeEnd"].Value);
                        string macro2TimeRangeMacro = Regex.Match(line, REGEX_MACRO_TAG).Groups["Macro2TimeRangeMacro"].Value;
                        DateTime macro3TimeRangeStart = DateTime.Parse(Regex.Match(line, REGEX_MACRO_TAG).Groups["Macro3TimeRangeStart"].Value);
                        DateTime macro3TimeRangeEnd = DateTime.Parse(Regex.Match(line, REGEX_MACRO_TAG).Groups["Macro3TimeRangeEnd"].Value);
                        string macro3TimeRangeMacro = Regex.Match(line, REGEX_MACRO_TAG).Groups["Macro3TimeRangeMacro"].Value;
                        DateTime macro4TimeRangeStart = DateTime.Parse(Regex.Match(line, REGEX_MACRO_TAG).Groups["Macro4TimeRangeStart"].Value);
                        DateTime macro4TimeRangeEnd = DateTime.Parse(Regex.Match(line, REGEX_MACRO_TAG).Groups["Macro4TimeRangeEnd"].Value);
                        string macro4TimeRangeMacro = Regex.Match(line, REGEX_MACRO_TAG).Groups["Macro4TimeRangeMacro"].Value;
                        string notes = Regex.Match(line, REGEX_MACRO_TAG).Groups["Notes"].Value;

                        MacroTag macroTag = new MacroTag(MacroParser)
                        {
                            Enable = enable,
                            Name = name,
                            Description = description,
                            DateTimeFormatValue = dateTimeFormatValue,
                            TimeRangeMacro1Start = macro1TimeRangeStart,
                            TimeRangeMacro1End = macro1TimeRangeEnd,
                            TimeRangeMacro1Macro = macro1TimeRangeMacro,
                            TimeRangeMacro2Start = macro2TimeRangeStart,
                            TimeRangeMacro2End = macro2TimeRangeEnd,
                            TimeRangeMacro2Macro = macro2TimeRangeMacro,
                            TimeRangeMacro3Start = macro3TimeRangeStart,
                            TimeRangeMacro3End = macro3TimeRangeEnd,
                            TimeRangeMacro3Macro = macro3TimeRangeMacro,
                            TimeRangeMacro4Start = macro4TimeRangeStart,
                            TimeRangeMacro4End = macro4TimeRangeEnd,
                            TimeRangeMacro4Macro = macro4TimeRangeMacro,
                            Notes = notes
                        };

                        switch (type)
                        {
                            case "ScreenName":
                                macroTag.Type = MacroTagType.ScreenName;
                                break;

                            case "ScreenNumber":
                                macroTag.Type = MacroTagType.ScreenNumber;
                                break;

                            case "ImageFormat":
                                macroTag.Type = MacroTagType.ImageFormat;
                                break;

                            case "ScreenCaptureCycleCount":
                                macroTag.Type = MacroTagType.ScreenCaptureCycleCount;
                                break;

                            case "ActiveWindowTitle":
                                macroTag.Type = MacroTagType.ActiveWindowTitle;
                                break;

                            case "DateTimeFormat":
                                macroTag.Type = MacroTagType.DateTimeFormat;
                                break;

                            case "User":
                                macroTag.Type = MacroTagType.User;
                                break;

                            case "Machine":
                                macroTag.Type = MacroTagType.Machine;
                                break;

                            case "TimeRange":
                                macroTag.Type = MacroTagType.TimeRange;
                                break;

                            case "DateTimeFormatExpression":
                                macroTag.Type = MacroTagType.DateTimeFormatExpression;
                                break;

                            case "QuarterYear":
                                macroTag.Type = MacroTagType.QuarterYear;
                                break;

                            case "X":
                                macroTag.Type = MacroTagType.X;
                                break;

                            case "Y":
                                macroTag.Type = MacroTagType.Y;
                                break;

                            case "Width":
                                macroTag.Type = MacroTagType.Width;
                                break;

                            case "Height":
                                macroTag.Type = MacroTagType.Height;
                                break;

                            case "Process":
                                macroTag.Type = MacroTagType.Process;
                                break;

                            case "Label":
                                macroTag.Type = MacroTagType.Label;
                                break;

                            case "CaptureNowCount":
                                macroTag.Type = MacroTagType.CaptureNowCount;
                                break;
                        }

                        _macroTagCollection.Add(macroTag);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("Config::ParseMacroTagDefinitions", ex);
            }
        }
    }
}
