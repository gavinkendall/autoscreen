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
        // Settings
        private const string REGEX_APPLICATION_SETTINGS_FILE = "^ApplicationSettingsFile=(?<Path>.+)$";
        private const string REGEX_USER_SETTINGS_FILE = "^UserSettingsFile=(?<Path>.+)$";
        private const string REGEX_SFTP_SETTINGS_FILE = "^SFTPSettingsFile=(?<Path>.+)$";
        private const string REGEX_SMTP_SETTINGS_FILE = "^SMTPSettingsFile=(?<Path>.+)$";

        // Application Setting Definition Regex
        private const string REGEX_APPLICATION_SETTING = "^ApplicationSetting::\\[Key=\"(?<Key>.+)\", Value=\"(?<Value>.*)\"\\]$";

        // User Setting Definition Regex
        private const string REGEX_USER_SETTING = "^UserSetting::\\[Key=\"(?<Key>.+)\", Value=\"(?<Value>.*)\"\\]$";

        // SFTP Setting Definition Regex
        private const string REGEX_SFTP_SETTING = "^SFTPSetting::\\[Key=\"(?<Key>.+)\", Value=\"(?<Value>.*)\"\\]$";

        // SMTP Setting Definition Regex
        private const string REGEX_SMTP_SETTING = "^SMTPSetting::\\[Key=\"(?<Key>.+)\", Value=\"(?<Value>.*)\"\\]$";

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

                    // Read each line of the configuration file looking for the filepaths of the application, user, SFTP, and SMTP settings files.
                    foreach (string line in fileSystem.ReadFromFile(fileSystem.ConfigFile))
                    {
                        // Ignore any empty lines or comments in the configuration file.
                        if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                        {
                            continue;
                        }

                        // Acquire the filepath for !autoscreen\settings\application.xml and create any sub-folders (that may contain macro tags).
                        if (Regex.IsMatch(line, REGEX_APPLICATION_SETTINGS_FILE))
                        {
                            fileSystem.ApplicationSettingsFile = ProcessPath(Regex.Match(line, REGEX_APPLICATION_SETTINGS_FILE).Groups["Path"].Value);
                        }

                        // Acquire the filepath for !autoscreen\settings\user.xml and create any sub-folders (that may contain macro tags).
                        if (Regex.IsMatch(line, REGEX_USER_SETTINGS_FILE))
                        {
                            fileSystem.UserSettingsFile = ProcessPath(Regex.Match(line, REGEX_USER_SETTINGS_FILE).Groups["Path"].Value);
                        }

                        // Acquire the filepath for !autoscreen\settings\sftp.xml and create any sub-folders (that may contain macro tags).
                        if (Regex.IsMatch(line, REGEX_SFTP_SETTINGS_FILE))
                        {
                            fileSystem.SftpSettingsFile = ProcessPath(Regex.Match(line, REGEX_SFTP_SETTINGS_FILE).Groups["Path"].Value);
                        }

                        // Acquire the filepath for !autoscreen\settings\smtp.xml and create any sub-folders (that may contain macro tags).
                        if (Regex.IsMatch(line, REGEX_SMTP_SETTINGS_FILE))
                        {
                            fileSystem.SmtpSettingsFile = ProcessPath(Regex.Match(line, REGEX_SMTP_SETTINGS_FILE).Groups["Path"].Value);
                        }
                    }

                    // Now that we have the File System setup with the filepaths for application, user, SFTP, and SMTP settings we can initialize settings.
                    Settings.Initialize(fileSystem);

                    if (fileSystem.FileExists(fileSystem.ApplicationSettingsFile))
                    {
                        Settings.Application.Load(Settings, FileSystem);

                        // The folders for errors and logs are in the application settings collection.
                        fileSystem.ErrorsFolder = ProcessPath(Settings.Application.GetByKey("ErrorsFolder").Value.ToString());
                        fileSystem.LogsFolder = ProcessPath(Settings.Application.GetByKey("LogsFolder").Value.ToString());

                        // The command.txt, screenshots.xml, screens.xml, regions.xml, editors.xml, schedules.xml, macrotags.xml, and triggers.xml filepaths are in application settings.
                        fileSystem.CommandFile = ProcessPath(Settings.Application.GetByKey("CommandFile").Value.ToString());
                        fileSystem.ScreenshotsFile = ProcessPath(Settings.Application.GetByKey("ScreenshotsFile").Value.ToString());
                        fileSystem.ScreensFile = ProcessPath(Settings.Application.GetByKey("ScreensFile").Value.ToString());
                        fileSystem.RegionsFile = ProcessPath(Settings.Application.GetByKey("RegionsFile").Value.ToString());
                        fileSystem.EditorsFile = ProcessPath(Settings.Application.GetByKey("EditorsFile").Value.ToString());
                        fileSystem.SchedulesFile = ProcessPath(Settings.Application.GetByKey("SchedulesFile").Value.ToString());
                        fileSystem.MacroTagsFile = ProcessPath(Settings.Application.GetByKey("MacroTagsFile").Value.ToString());
                        fileSystem.TriggersFile = ProcessPath(Settings.Application.GetByKey("TriggersFile").Value.ToString());
                    }
                    else
                    {
                        ParseDefaultApplicationSettings();

                        Settings.Application.Save(Settings, FileSystem);
                    }

                    if (fileSystem.FileExists(fileSystem.UserSettingsFile))
                    {
                        Settings.User.Load(Settings, FileSystem);

                        // The screenshots folder is from user settings.
                        fileSystem.ScreenshotsFolder = ProcessPath(Settings.User.GetByKey("ScreenshotsFolder").Value.ToString());
                    }
                    else
                    {
                        // Any default settings for users (such as screen capture interval, keyboard shortcuts, etc.) get parsed here.
                        ParseDefaultUserSettings();
                    }

                    if (fileSystem.FileExists(fileSystem.SftpSettingsFile))
                    {
                        Settings.SFTP.Load(Settings, FileSystem);
                    }
                    else
                    {
                        // Acquire default SFTP settings.
                        ParseDefaultSFTPSettings();
                    }

                    if (fileSystem.FileExists(fileSystem.SmtpSettingsFile))
                    {
                        Settings.SMTP.Load(Settings, FileSystem);
                    }
                    else
                    {
                        // Acquire default SMTP settings.
                        ParseDefaultSMTPSettings();
                    }

                    Log = new Log(Settings, fileSystem, MacroParser);
                    ScreenCapture screenCapture = new ScreenCapture(this, fileSystem, Log);
                    Security security = new Security(fileSystem);

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

                    Settings.VersionManager.OldApplicationSettings = Settings.Application.Clone();

                    Settings.VersionManager.OldUserSettings = Settings.User.Clone();

                    Settings.UpgradeApplicationSettings(Settings.Application, FileSystem);

                    Settings.UpgradeUserSettings(Settings.User, screenCapture, security, FileSystem);

                    Settings.UpgradeSftpSettings(Settings.SFTP, FileSystem);

                    Settings.UpgradeSmtpSettings(Settings.SMTP, FileSystem);

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
        /// Processes a given folder path or filepath containing sub-folders to create the necessary sub-folders. Macro tags are also parsed.
        /// </summary>
        /// <param name="path">The folder path or a filepath containing sub-folders from the configuration file.</param>
        /// <returns>The processed path that has been parsed for macro tags and includes directory separator character for sub-folders.</returns>
        private string ProcessPath(string path)
        {
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

            return path;
        }

        /// <summary>
        /// Parses default application settings found in the configuration file.
        /// </summary>
        private void ParseDefaultApplicationSettings()
        {
            try
            {
                foreach (string line in FileSystem.ReadFromFile(FileSystem.ConfigFile))
                {
                    if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                    {
                        continue;
                    }

                    if (Regex.IsMatch(line, REGEX_APPLICATION_SETTING))
                    {
                        string key = Regex.Match(line, REGEX_APPLICATION_SETTING).Groups["Key"].Value;
                        string value = Regex.Match(line, REGEX_APPLICATION_SETTING).Groups["Value"].Value;

                        Settings.Application.SetValueByKey(key, value);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("Config::ParseDefaultApplicationSettings", ex);
            }
        }

        private void ParseDefaultUserSettings()
        {
            try
            {
                // We don't want to keep the passphrase in the configuration file so give it an empty value here
                // and let the user set their passphrase (which is a secure hash).
                Settings.User.SetValueByKey("Passphrase", string.Empty);
                Settings.User.SetValueByKey("PassphraseLastUpdated", string.Empty);

                foreach (string line in FileSystem.ReadFromFile(FileSystem.ConfigFile))
                {
                    if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                    {
                        continue;
                    }

                    if (Regex.IsMatch(line, REGEX_USER_SETTING))
                    {
                        string key = Regex.Match(line, REGEX_USER_SETTING).Groups["Key"].Value;
                        string value = Regex.Match(line, REGEX_USER_SETTING).Groups["Value"].Value;

                        Settings.User.SetValueByKey(key, value);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("Config::ParseDefaultUserSettings", ex);
            }
        }

        private void ParseDefaultSFTPSettings()
        {
            try
            {
                foreach (string line in FileSystem.ReadFromFile(FileSystem.ConfigFile))
                {
                    if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                    {
                        continue;
                    }

                    if (Regex.IsMatch(line, REGEX_SFTP_SETTING))
                    {
                        string key = Regex.Match(line, REGEX_SFTP_SETTING).Groups["Key"].Value;
                        string value = Regex.Match(line, REGEX_SFTP_SETTING).Groups["Value"].Value;

                        Settings.SFTP.SetValueByKey(key, value);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("Config::ParseDefaultSFTPSettings", ex);
            }
        }

        private void ParseDefaultSMTPSettings()
        {
            try
            {
                foreach (string line in FileSystem.ReadFromFile(FileSystem.ConfigFile))
                {
                    if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                    {
                        continue;
                    }

                    if (Regex.IsMatch(line, REGEX_SMTP_SETTING))
                    {
                        string key = Regex.Match(line, REGEX_SMTP_SETTING).Groups["Key"].Value;
                        string value = Regex.Match(line, REGEX_SMTP_SETTING).Groups["Value"].Value;

                        Settings.SMTP.SetValueByKey(key, value);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("Config::ParseDefaultSMTPSettings", ex);
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
