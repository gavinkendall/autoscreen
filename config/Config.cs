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
        // Image Format
        private const string REGEX_IMAGE_FORMAT = "^ImageFormat=(?<ImageFormat>[A-Z]{4})$";

        // Filename Pattern
        private const string REGEX_FILENAME_PATTERN = "^FilenamePattern=(?<FilenamePattern>.+)$";

        // Folders
        private const string REGEX_SCREENSHOTS_FOLDER = "^ScreenshotsFolder=(?<Path>.+)$";
        private const string REGEX_ERRORS_FOLDER = "^DebugFolder=(?<Path>.+)$";
        private const string REGEX_LOGS_FOLDER = "^LogsFolder=(?<Path>.+)$";

        // Files
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
        private const string REGEX_MACRO_TAGS_FILE = "^MacroTagsFile=(?<Path>.+)$";
        private const string REGEX_SCHEDULES_FILE = "^SchedulesFile=(?<Path>.+)$";

        // Screen
        private const string REGEX_SCREEN = "^Screen::\\[Enable=(?<Enable>False|True), Name=\"(?<Name>.+)\", Folder=\"(?<Folder>.+)\", Macro=\"(?<Macro>.+)\", Source=(?<Source>\\d{1}), Component=(?<Component>\\d{1}), CaptureMethod=(?<CaptureMethod>\\d{1}), X=(?<X>\\d{1,4}), Y=(?<Y>\\d{1,4}), Width=(?<Width>\\d{1,4}), Height=(?<Height>\\d{1,4}), AutoAdapt=(?<AutoAdapt>False|True), Format=(?<Format>JPEG), JPEGQuality=(?<JPEGQuality>\\d{1,3}), ResolutionRatio=(?<ResolutionRatio>\\d{1,3}), MousePointer=(?<MousePointer>False|True), Encrypt=(?<Encrypt>False|True)\\]$";

        // Region
        private const string REGEX_REGION = "^Region::\\[Enable=(?<Enable>False|True), Name=\"(?<Name>.+)\", Folder=\"(?<Folder>.+)\", Macro=\"(?<Macro>.+)\", X=(?<X>\\d{1,4}), Y=(?<Y>\\d{1,4}), Width=(?<Width>\\d{1,4}), Height=(?<Height>\\d{1,4}), Format=(?<Format>JPEG), JPEGQuality=(?<JPEGQuality>\\d{1,3}), MousePointer=(?<MousePointer>False|True), Encrypt=(?<Encrypt>False|True)\\]$";

        // Editor
        private const string REGEX_EDITOR = "^Editor::\\[Name=\"(?< Name >.+)\", ApplicationPath=\"(?<ApplicationPath>.+)\", ApplicationArguments=\"(?<ApplicationArguments>.+)\", Notes=\"(?<Notes>.+)\"\\]$";

        // Schedule
        private const string REGEX_SCHEDULE = "^Schedule::\\[Enable=(?<Enable>False|True), Name=\"(?<Name>.+)\", Scope=\"(?<Scope>.+)\", OneTime=(?<OneTime>False|True), Period=(?<Period>False|True), Logic=(?<Logic>\\d{1}), CaptureAt=(?<CaptureAt>\\d{2}:\\d{2}), StartAt=(?<StartAt>\\d{2}:\\d{2}), StopAt=(?<StopAt>\\d{2}:\\d{2}), Interval=(?<Interval>\\d{2}:\\d{2}:\\d{2}), Monday=(?<Monday>False|True), Tuesday=(?<Tuesday>False|True), Wednesday=(?<Wednesday>False|True), Thursday=(?<Thursday>False|True), Friday=(?<Friday>False|True), Saturday=(?<Saturday>False|True), Sunday=(?<Sunday>False|True), Notes=\"(?<Notes>.+)\"\\]$";

        // Macro Tag
        private const string REGEX_MACRO_TAG = "^MacroTag::\\[Enable=(?<Enable>False|True), Name=\"(?<Name>.+)\", Description=\"(?<Description>.+)\", Type=\"(?<Type>.+)\", DateTimeFormatValue=\"(?<DateTimeFormatValue>.+)\", Macro1TimeRangeStart=(?<Macro1TimeRangeStart>\\d{2}:\\d{2}:\\d{2}), Macro1TimeRangeEnd=(?<Macro1TimeRangeEnd>\\d{2}:\\d{2}:\\d{2}), Macro1TimeRangeMacro=\"(?<Macro1TimeRangeMacro>.*)\", Macro2TimeRangeStart=(?<Macro2TimeRangeStart>\\d{2}:\\d{2}:\\d{2}), Macro2TimeRangeEnd=(?<Macro2TimeRangeEnd>\\d{2}:\\d{2}:\\d{2}), Macro2TimeRangeMacro=\"(?<Macro2TimeRangeMacro>.*)\", Macro3TimeRangeStart=(?<Macro3TimeRangeStart>\\d{2}:\\d{2}:\\d{2}), Macro3TimeRangeEnd=(?<Macro3TimeRangeEnd>\\d{2}:\\d{2}:\\d{2}), Macro3TimeRangeMacro=\"(?<Macro3TimeRangeMacro>.*)\", Macro4TimeRangeStart=(?<Macro4TimeRangeStart>\\d{2}:\\d{2}:\\d{2}), Macro4TimeRangeEnd=(?<Macro4TimeRangeEnd>\\d{2}:\\d{2}:\\d{2}), Macro4TimeRangeMacro=\"(?<Macro4TimeRangeMacro>.*)\", Notes=\"(?<Notes>.*)\"\\]$";

        // Trigger
        private const string REGEX_TRIGGER = "^Trigger::\\[Enable=(?<Enable>False|True), Name=\"(?<Name>.+)\", Condition=\"(?<Condition>.+)\", Action=\"(?<Action>.+)\", Date=(?<Date>\\d{4}:\\d{2}:\\d{2}), Time=(?<Time>\\d{2}:\\d{2}), Day=(?<Day>Weekday|Weekend|Monday|Tuesday|Wednesday|Thursday|Friday|Saturday|Sunday), Days=(?<Days>\\d{1,8}), Interval=(?<Interval>\\d{2}:\\d{2}:\\d{2}), CycleCount=(?<CycleCount=\\d{1,8}), Duration=(?<Duration>\\d{1,8}), DurationType=(?<DurationType>\\d{1}), Value=\"(?<Value>.+)\"\\]$";

        /// <summary>
        /// A collection of macro tags.
        /// </summary>
        //private MacroTagCollection _macroTagCollection;

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
        /// A class for handling screen capture operations.
        /// </summary>
        public ScreenCapture ScreenCapture { get; set; }

        public MacroTagCollection MacroTagCollection { get; set; }

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
        public void Load(FileSystem fileSystem, bool cleanStartup = false)
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

                if (!fileSystem.FileExists(fileSystem.ConfigFile))
                {
                    // do something here if the configuration file doesn't exist
                }

                ParseMacroTags();

                foreach (string line in fileSystem.ReadFromFile(fileSystem.ConfigFile))
                {
                    if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                    {
                        continue;
                    }

                    if (string.IsNullOrEmpty(fileSystem.FilenamePattern))
                    {
                        fileSystem.FilenamePattern = Regex.Match(line, REGEX_FILENAME_PATTERN).Groups["FilenamePattern"].Value;
                    }

                    if (string.IsNullOrEmpty(ScreenCapture.ImageFormat))
                    {
                        ScreenCapture.ImageFormat = Regex.Match(line, REGEX_IMAGE_FORMAT).Groups["ImageFormat"].Value;
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

                // This is for when we didn't find an entry for FilenamePattern.
                // (which can happen if we're running autoscreen.exe with an old autoscreen.conf file not created by version 2.4 or higher)
                if (string.IsNullOrEmpty(fileSystem.FilenamePattern))
                {
                    fileSystem.AppendToFile(fileSystem.ConfigFile, "\nFilenamePattern=" + fileSystem.DefaultFilenamePattern);

                    fileSystem.FilenamePattern = fileSystem.DefaultFilenamePattern;
                }

                CheckAndCreateFolders();

                Settings.Load(fileSystem);

                Log = new Log(Settings, fileSystem, MacroParser);
                ScreenCapture = new ScreenCapture(this, fileSystem, Log);
                Security security = new Security(fileSystem);

                if (string.IsNullOrEmpty(ScreenCapture.ImageFormat))
                {
                    fileSystem.AppendToFile(fileSystem.ConfigFile, "\nImageFormat=" + ScreenCapture.DefaultImageFormat);

                    ScreenCapture.ImageFormat = ScreenCapture.DefaultImageFormat;
                }

                CheckAndCreateFiles(security, ScreenCapture, Log);
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("Config::Load", ex);
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
                    FileSystem.ScreenshotsFolder = MacroParser.ParseTags(FileSystem.DefaultScreenshotsFolder, MacroTagCollection, Log);

                    FileSystem.AppendToFile(FileSystem.ConfigFile, "\nScreenshotsFolder=" + FileSystem.ScreenshotsFolder);

                    if (!FileSystem.DirectoryExists(FileSystem.ScreenshotsFolder))
                    {
                        FileSystem.CreateDirectory(FileSystem.ScreenshotsFolder);
                    }
                }

                if (string.IsNullOrEmpty(FileSystem.ErrorsFolder))
                {
                    FileSystem.ErrorsFolder = MacroParser.ParseTags(FileSystem.DefaultErrorsFolder, MacroTagCollection, Log);

                    FileSystem.AppendToFile(FileSystem.ConfigFile, "\nErrorsFolder=" + FileSystem.ErrorsFolder);

                    if (!FileSystem.DirectoryExists(FileSystem.ErrorsFolder))
                    {
                        FileSystem.CreateDirectory(FileSystem.ErrorsFolder);
                    }
                }

                if (string.IsNullOrEmpty(FileSystem.LogsFolder))
                {
                    FileSystem.LogsFolder = MacroParser.ParseTags(FileSystem.DefaultLogsFolder, MacroTagCollection, Log);

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
                    FileSystem.CommandFile = MacroParser.ParseTags(FileSystem.DefaultCommandFile, MacroTagCollection, log);

                    FileSystem.AppendToFile(FileSystem.ConfigFile, "\nCommandFile=" + FileSystem.CommandFile);

                    if (!FileSystem.FileExists(FileSystem.CommandFile))
                    {
                        FileSystem.CreateFile(FileSystem.CommandFile);
                    }
                }

                if (string.IsNullOrEmpty(FileSystem.ApplicationSettingsFile))
                {
                    FileSystem.ApplicationSettingsFile = MacroParser.ParseTags(FileSystem.DefaultApplicationSettingsFile, MacroTagCollection, log);

                    FileSystem.AppendToFile(FileSystem.ConfigFile, "\nApplicationSettingsFile=" + FileSystem.ApplicationSettingsFile);

                    if (!FileSystem.DirectoryExists(FileSystem.DefaultSettingsFolder))
                    {
                        FileSystem.CreateDirectory(FileSystem.DefaultSettingsFolder);
                    }
                }

                if (string.IsNullOrEmpty(FileSystem.SmtpSettingsFile))
                {
                    FileSystem.SmtpSettingsFile = MacroParser.ParseTags(FileSystem.DefaultSmtpSettingsFile, MacroTagCollection, log);

                    FileSystem.AppendToFile(FileSystem.ConfigFile, "\nSMTPSettingsFile=" + FileSystem.SmtpSettingsFile);

                    if (!FileSystem.DirectoryExists(FileSystem.DefaultSettingsFolder))
                    {
                        FileSystem.CreateDirectory(FileSystem.DefaultSettingsFolder);
                    }
                }

                if (string.IsNullOrEmpty(FileSystem.SftpSettingsFile))
                {
                    FileSystem.SftpSettingsFile = MacroParser.ParseTags(FileSystem.DefaultSftpSettingsFile, MacroTagCollection, log);

                    FileSystem.AppendToFile(FileSystem.ConfigFile, "\nSFTPSettingsFile=" + FileSystem.SftpSettingsFile);

                    if (!FileSystem.DirectoryExists(FileSystem.DefaultSettingsFolder))
                    {
                        FileSystem.CreateDirectory(FileSystem.DefaultSettingsFolder);
                    }
                }

                if (string.IsNullOrEmpty(FileSystem.UserSettingsFile))
                {
                    FileSystem.UserSettingsFile = MacroParser.ParseTags(FileSystem.DefaultUserSettingsFile, MacroTagCollection, log);

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
                    ScreenCollection screenCollection = new ScreenCollection();

                    ScreenshotCollection screenshotCollection = new ScreenshotCollection(imageFormatCollection, screenCollection, screenCapture, this, FileSystem, log, security);
                    screenshotCollection.SaveToXmlFile(this);
                }

                if (string.IsNullOrEmpty(FileSystem.EditorsFile))
                {
                    // Loading the editor collection will automatically create the default editors and add them to the collection.
                    EditorCollection editorCollection = new EditorCollection();
                    editorCollection.LoadXmlFileAndAddEditors(this, FileSystem, log);
                }

                if (string.IsNullOrEmpty(FileSystem.RegionsFile))
                {
                    RegionCollection regionCollection = new RegionCollection();
                    regionCollection.SaveToXmlFile(Settings, FileSystem, log);
                }

                if (string.IsNullOrEmpty(FileSystem.ScreensFile))
                {
                    // Loading the screen collection will automatically create the available screens and add them to the collection.
                    ScreenCollection screenCollection = new ScreenCollection();
                    screenCollection.LoadXmlFileAndAddScreens(new ImageFormatCollection(), this, MacroParser, FileSystem, log);
                }

                if (string.IsNullOrEmpty(FileSystem.TriggersFile))
                {
                    // Loading triggers will automatically create the default triggers and add them to the collection.
                    TriggerCollection triggerCollection = new TriggerCollection();
                    triggerCollection.LoadXmlFileAndAddTriggers(this, FileSystem, log);
                }

                if (string.IsNullOrEmpty(FileSystem.MacroTagsFile))
                {
                    // Loading macro tags will automatically create the default macro tags and add them to the collection.
                    MacroTagCollection tagCollection = new MacroTagCollection();
                    tagCollection.LoadXmlFileAndAddTags(this, MacroParser, FileSystem, log);
                }

                if (string.IsNullOrEmpty(FileSystem.SchedulesFile))
                {
                    // Loading schedules will automatically create the default schedules and add them to the collection.
                    ScheduleCollection scheduleCollection = new ScheduleCollection();
                    scheduleCollection.LoadXmlFileAndAddSchedules(this, FileSystem, log);
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

            path = MacroParser.ParseTags(path, MacroTagCollection, Log);

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

        private void ParseMacroTags()
        {
            try
            {
                MacroTagCollection = new MacroTagCollection();

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

                        MacroTagCollection.Add(macroTag);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("Config::ParseMacroTags", ex);
            }
        }
    }
}
