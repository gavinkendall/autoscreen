//-----------------------------------------------------------------------
// <copyright file="Config.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2023 Gavin Kendall
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
using System.IO;
using System.Text.RegularExpressions;

namespace AutoScreenCapture
{
    /// <summary>
    /// The configuration file is used to define the folders and files that the application will use.
    /// </summary>
    public class Config
    {
        // Version
        private const String REGEX_VERSION = "^Version=(?<Version>\\d{1}\\.\\d{1}\\.\\d{1}\\.\\d{1})$";

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
        private const string REGEX_SCREEN = "^Screen::\\[Enable=(?<Enable>False|True), Name=\"(?<Name>.+)\", Folder=\"(?<Folder>.+)\", Macro=\"(?<Macro>.+)\", Source=(?<Source>\\d{1}), Component=(?<Component>\\d{1}), CaptureMethod=(?<CaptureMethod>\\d{1}), X=(?<X>\\d{1,4}), Y=(?<Y>\\d{1,4}), Width=(?<Width>\\d{1,4}), Height=(?<Height>\\d{1,4}), AutoAdapt=(?<AutoAdapt>False|True), Format=(?<Format>[A-Z]{3,4}), JPEGQuality=(?<JPEGQuality>\\d{1,3}), ImageDiffTolerance=(?<ImageDiffTolerance>\\d{1,3}), ResolutionRatio=(?<ResolutionRatio>\\d{1,3}), Mouse=(?<Mouse>False|True), Encrypt=(?<Encrypt>False|True)\\]$";

        // Region Definition Regex
        private const string REGEX_REGION = "^Region::\\[Enable=(?<Enable>False|True), Name=\"(?<Name>.+)\", Folder=\"(?<Folder>.+)\", Macro=\"(?<Macro>.+)\", X=(?<X>\\d{1,4}), Y=(?<Y>\\d{1,4}), Width=(?<Width>\\d{1,4}), Height=(?<Height>\\d{1,4}), Format=(?<Format>[A-Z]{3,4}), JPEGQuality=(?<JPEGQuality>\\d{1,3}), ImageDiffTolerance=(?<ImageDiffTolerance>\\d{1,3}), Mouse=(?<Mouse>False|True), Encrypt=(?<Encrypt>False|True)\\]$";

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
        /// <param name="hide">Determines if we start "hidden". This should not show the interface or the system tray icon.</param>
        /// <returns>True if load successful otherwise False if load failed.</returns>
        public bool Load(FileSystem fileSystem, bool cleanStartup = false, bool hide = false)
        {
            try
            {
                CleanStartup = cleanStartup;

                FileSystem = fileSystem;

                Settings = new Settings();
                MacroParser = new MacroParser(Settings);

                Log = new Log(Settings, fileSystem, MacroParser);

                string configDirectory = fileSystem.GetDirectoryName(fileSystem.ConfigFile);

                if (!string.IsNullOrEmpty(configDirectory) && !fileSystem.DirectoryExists(configDirectory))
                {
                    fileSystem.CreateDirectory(configDirectory);
                }

                if (!fileSystem.FileExists(fileSystem.ConfigFile))
                {
                    // Create the default "autoscreen.conf" file from the Visual Studio solution
                    // (that we've added to Properties.Resources) and then attempt to load it.
                    File.WriteAllBytes(fileSystem.ConfigFile, Properties.Resources.autoscreen_conf);
                }

                string version = string.Empty;

                Log.WriteStartupMessage("*** START " + DateTime.Now.ToLongTimeString() + " ***");
                Log.WriteStartupMessage("*** Welcome to Auto Screen Capture ***");
                Log.WriteStartupMessage("Starting application");
                Log.WriteStartupMessage("At this point the application should be able to run normally");
                Log.WriteStartupMessage("but it would be a good idea to check what we found in your autoscreen.conf file");
                Log.WriteStartupMessage("Your autoscreen.conf file is \"" + FileSystem.ConfigFile + "\"");
                Log.WriteStartupMessage("The name and location of it can be changed with the -config command line argument:");
                Log.WriteStartupMessage("autoscreen.exe -config=\"C:\\My Configuration File.conf\"");
                Log.WriteStartupMessage("Checking configuration file version");

                // Look for the version we should be using.
                foreach (string line in fileSystem.ReadFromFile(fileSystem.ConfigFile))
                {
                    // Ignore any empty lines or comments in the configuration file.
                    if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                    {
                        continue;
                    }

                    if (Regex.IsMatch(line, REGEX_VERSION))
                    {
                        version = Regex.Match(line, REGEX_VERSION).Groups["Version"].Value;

                        break;
                    }
                }

                if (string.IsNullOrEmpty(version))
                {
                    Log.WriteStartupMessage("The version could not be determined. You might be using a configuration file that's older than version 2.5 so some of the application's functionality may not work as expected");
                }
                else
                {
                    Log.WriteStartupMessage("Auto Screen Capture is using version " + version + " of the configuration file. This needs to match with the version of autoscreen.exe to avoid application errors and weird application behaviour");
                }

                Log.WriteStartupMessage("Version of autoscreen.exe: " + Settings.ApplicationVersion);
                Log.WriteStartupMessage("Version of autoscreen.conf: " + (string.IsNullOrEmpty(version) ? "???? (I'm guessing you're using an old configuration file that was created by a version of Auto Screen Capture that was older than 2.5.0.0)" : version));

                if (!string.IsNullOrEmpty(version) && !Settings.ApplicationVersion.Equals(version))
                {
                    Log.WriteStartupMessage("The versions are different. Was this intentional?");
                    Log.WriteStartupMessage("It's highly recommended that the application and its corresponding configuration file is using the same version");
                }

                Log.WriteStartupMessage("Initializing Macro Tag collection in preparation for macro tags that might be available when processing folder paths and filepaths");

                _macroTagCollection = new MacroTagCollection();

                if (!string.IsNullOrEmpty(version))
                {
                    Log.WriteStartupMessage("Parsing configuration file for MacroTag definitions");

                    // We need to parse and load all default macro tags before parsing the paths used for files and folders
                    // because they may have macro tags in the paths.
                    ParseMacroTagDefinitions();
                }
                else
                {
                    Log.WriteStartupMessage("Normally, if this was a version of the configuration file that was 2.5 (or higher), we would be parsing Macro Tag definitions but because we couldn't determine the version of the configuration file macro tags may or may not be avaialble for the application");
                }

                Log.WriteStartupMessage("Processing filepaths");

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

                Log.WriteStartupMessage("Initializing settings");

                // Now that we have the File System setup with the filepaths for application, user, SFTP, and SMTP settings we can initialize settings.
                Settings.Initialize(fileSystem);

                Log.WriteStartupMessage("Loading application settings");

                if (fileSystem.FileExists(fileSystem.ApplicationSettingsFile))
                {
                    Log.WriteStartupMessage("An existing application settings file was found at " + fileSystem.ApplicationSettingsFile);

                    Log.WriteStartupMessage("Application settings will be loaded from the application settings file");

                    Log.WriteStartupMessage("Loading application settings from " + fileSystem.ApplicationSettingsFile);

                    // Load existing application settings.
                    Settings.Application.Load(Settings, FileSystem);

                    Log.WriteStartupMessage("Version of autoscreen.exe: " + Settings.ApplicationVersion);
                    Log.WriteStartupMessage("Version of application settings: " + Settings.Application.AppVersion);

                    if (!Settings.Application.AppVersion.Equals(Settings.ApplicationVersion))
                    {
                        Log.WriteStartupMessage("WARNING! The version of the application settings do not match with the version of autoscreen.exe so ... good luck!");
                    }

                    // Check if this is an old version of the application settings.
                    // If so then we're likely handling the old format of the configuration file so parse it "the old way".
                    Version thisVersionOfApplication = Settings.VersionManager.Versions.Get(Settings.ApplicationCodename, Settings.ApplicationVersion);
                    Version versionInApplicationSettings = Settings.VersionManager.Versions.Get(Settings.Application.AppCodename, Settings.Application.AppVersion);

                    if (versionInApplicationSettings.VersionNumber < thisVersionOfApplication.VersionNumber)
                    {
                        Log.WriteStartupMessage("WARNING! It looks like you're using application settings that are older than the application itself so be aware that some of the application's functionality may not work as expected");

                        Log.WriteStartupMessage("Attempting to parse the configuration file using the old format (from before 2.5) so we can use features that are available in version " + Settings.ApplicationVersion);

                        UpgradeConfig();
                    }
                }
                else
                {
                    Log.WriteStartupMessage("An existing application settings file was not found at " + fileSystem.ApplicationSettingsFile);

                    Log.WriteStartupMessage("Application settings will be loaded from the configuration file");

                    Log.WriteStartupMessage("Loading application settings from " + fileSystem.ConfigFile);

                    // Acquire default application settings.
                    ParseDefaultApplicationSettings();

                    Settings.Application.AppCodename = Settings.ApplicationCodename;
                    Settings.Application.AppVersion = version;
                    Settings.Application.Save(Settings, FileSystem);
                }

                // The folders for errors and logs are in the application settings collection.
                Setting errorsFolderSetting = Settings.Application.GetByKey("ErrorsFolder");

                if (errorsFolderSetting != null)
                {
                    fileSystem.ErrorsFolder = ProcessPath(Settings.Application.GetByKey("ErrorsFolder").Value.ToString());
                }
                else
                {
                    fileSystem.ErrorsFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\!autoscreen\errors\";
                }

                Setting logsFolderSetting = Settings.Application.GetByKey("LogsFolder");

                if (logsFolderSetting != null)
                {
                    fileSystem.LogsFolder = ProcessPath(Settings.Application.GetByKey("LogsFolder").Value.ToString());
                }
                else
                {
                    fileSystem.LogsFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\!autoscreen\logs\";
                }

                Log.WriteStartupMessage("ErrorsFolder=" + fileSystem.ErrorsFolder);
                Log.WriteStartupMessage("LogsFolder=" + fileSystem.LogsFolder);

                // The command.txt, screenshots.xml, screens.xml, regions.xml, editors.xml, schedules.xml, macrotags.xml, and triggers.xml filepaths are in application settings.

                Setting commandFileSetting = Settings.Application.GetByKey("CommandFile");

                if (commandFileSetting != null)
                {
                    fileSystem.CommandFile = ProcessPath(Settings.Application.GetByKey("CommandFile").Value.ToString());
                }
                else
                {
                    fileSystem.CommandFile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Auto Screen Capture\!autoscreen\command.txt";
                }

                Setting screenshotsFileSetting = Settings.Application.GetByKey("ScreenshotsFile");

                if (screenshotsFileSetting != null)
                {
                    fileSystem.ScreenshotsFile = ProcessPath(Settings.Application.GetByKey("ScreenshotsFile").Value.ToString());
                }
                else
                {
                    fileSystem.ScreenshotsFile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Auto Screen Capture\!autoscreen\screenshots.xml";
                }

                Setting screensFileSetting = Settings.Application.GetByKey("ScreensFile");

                if (screensFileSetting != null)
                {
                    fileSystem.ScreensFile = ProcessPath(Settings.Application.GetByKey("ScreensFile").Value.ToString());
                }
                else
                {
                    fileSystem.ScreensFile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Auto Screen Capture\!autoscreen\screens.xml";
                }

                Setting regionsFileSetting = Settings.Application.GetByKey("RegionsFile");

                if (regionsFileSetting != null)
                {
                    fileSystem.RegionsFile = ProcessPath(Settings.Application.GetByKey("RegionsFile").Value.ToString());
                }
                else
                {
                    fileSystem.RegionsFile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Auto Screen Capture\!autoscreen\regions.xml";
                }

                Setting editorsFileSetting = Settings.Application.GetByKey("EditorsFile");

                if (editorsFileSetting != null)
                {
                    fileSystem.EditorsFile = ProcessPath(Settings.Application.GetByKey("EditorsFile").Value.ToString());
                }
                else
                {
                    fileSystem.EditorsFile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Auto Screen Capture\!autoscreen\editors.xml";
                }

                Setting schedulesFileSetting = Settings.Application.GetByKey("SchedulesFile");

                if (schedulesFileSetting != null)
                {
                    fileSystem.SchedulesFile = ProcessPath(Settings.Application.GetByKey("SchedulesFile").Value.ToString());
                }
                else
                {
                    fileSystem.SchedulesFile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Auto Screen Capture\!autoscreen\schedules.xml";
                }

                Setting macroTagsFileSetting = Settings.Application.GetByKey("MacroTagsFile");

                if (macroTagsFileSetting != null)
                {
                    fileSystem.MacroTagsFile = ProcessPath(Settings.Application.GetByKey("MacroTagsFile").Value.ToString());
                }
                else
                {
                    fileSystem.MacroTagsFile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Auto Screen Capture\!autoscreen\macrotags.xml";
                }

                Setting triggersFileSetting = Settings.Application.GetByKey("TriggersFile");

                if (triggersFileSetting != null)
                {
                    fileSystem.TriggersFile = ProcessPath(Settings.Application.GetByKey("TriggersFile").Value.ToString());
                }
                else
                {
                    fileSystem.TriggersFile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Auto Screen Capture\!autoscreen\triggers.xml";
                }

                Log.WriteStartupMessage("CommandFile=" + fileSystem.CommandFile);
                Log.WriteStartupMessage("ScreenshotsFile=" + fileSystem.ScreenshotsFile);
                Log.WriteStartupMessage("ScreensFile=" + fileSystem.ScreensFile);
                Log.WriteStartupMessage("RegionsFile=" + fileSystem.RegionsFile);
                Log.WriteStartupMessage("EditorsFile=" + fileSystem.EditorsFile);
                Log.WriteStartupMessage("SchedulesFile=" + fileSystem.SchedulesFile);
                Log.WriteStartupMessage("MacroTagsFile=" + fileSystem.MacroTagsFile);
                Log.WriteStartupMessage("TriggersFile=" + FileSystem.TriggersFile);

                Log.WriteStartupMessage("Loading user settings");

                if (fileSystem.FileExists(fileSystem.UserSettingsFile))
                {
                    Log.WriteStartupMessage("An existing user settings file was found at " + fileSystem.UserSettingsFile);

                    Log.WriteStartupMessage("User settings will be loaded from the user settings file");

                    Log.WriteStartupMessage("Loading user settings from " + fileSystem.UserSettingsFile);

                    // Load existing user settings.
                    Settings.User.Load(Settings, FileSystem);
                }
                else
                {
                    Log.WriteStartupMessage("An existing user settings file was not found at " + fileSystem.UserSettingsFile);

                    Log.WriteStartupMessage("User settings will be loaded from the configuration file");

                    Log.WriteStartupMessage("Loading user settings from " + fileSystem.ConfigFile);

                    // Acquire default user settings.
                    // Any default settings for users (such as screen capture interval, keyboard shortcuts, etc.) get parsed here.
                    ParseDefaultUserSettings();

                    Settings.User.AppCodename = Settings.ApplicationCodename;
                    Settings.User.AppVersion = version;
                    Settings.User.Save(Settings, FileSystem);
                }

                Log.WriteStartupMessage("Getting default image format");

                Setting imageFormatSetting = Settings.User.GetByKey("ImageFormat");

                if (imageFormatSetting != null)
                {
                    ScreenCapture.ImageFormat = imageFormatSetting.Value.ToString();
                }
                else
                {
                    Log.WriteStartupMessage("The specified default image format could not be found so JPEG is going to be used as the default image format");

                    ScreenCapture.ImageFormat = "JPEG";
                }

                Log.WriteStartupMessage("ImageFormat=" + ScreenCapture.ImageFormat);

                if (hide)
                {
                    Settings.User.SetValueByKey("SneakyPastaSnake", true);
                }
                
                Setting sneakyPastaSnake = Settings.User.GetByKey("SneakyPastaSnake");

                if (sneakyPastaSnake != null && sneakyPastaSnake.Value.ToString().Equals("True"))
                {
                    Log.WriteStartupMessage("It looks like you want to be a sneaky pasta snake. Interface and system tray icon will be hidden");

                    Settings.User.SetValueByKey("ShowInterface", false);
                    Settings.User.SetValueByKey("ShowSystemTrayIcon", false);
                }

                Log.WriteStartupMessage("Getting default screenshots folder");

                // The screenshots folder from user settings.
                // Call ProcessPath as we want sub-folders to be created.

                Setting screenshotsFolderSetting = Settings.User.GetByKey("ScreenshotsFolder");

                if (screenshotsFolderSetting != null)
                {
                    fileSystem.ScreenshotsFolder = ProcessPath(Settings.User.GetByKey("ScreenshotsFolder").Value.ToString());
                }
                else
                {
                    fileSystem.ScreenshotsFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Auto Screen Capture\screenshots\";
                }

                Log.WriteStartupMessage("Getting default filename pattern");

                // The default filename pattern if we can't find "FilenamePattern" in the config file (which could happen with a config file from 2.3).
                fileSystem.FilenamePattern = @"%date%\%name%\%date%_%time%.%format%";

                Setting filenamePatternSetting = Settings.User.GetByKey("FilenamePattern");

                if (filenamePatternSetting != null)
                {
                    // The filename pattern from user settings.
                    // Do not call ProcessPath as we don't want the sub-folders to be created yet.
                    fileSystem.FilenamePattern = Settings.User.GetByKey("FilenamePattern").Value.ToString();
                }

                Log.WriteStartupMessage("The default screenshots folder is " + fileSystem.ScreenshotsFolder);

                Log.WriteStartupMessage("The default filename pattern is " + fileSystem.FilenamePattern);

                Log.WriteStartupMessage("Loading SFTP settings (for file transfer operations since we can use SFTP to upload screenshots to a file server)");

                if (fileSystem.FileExists(fileSystem.SftpSettingsFile))
                {
                    Log.WriteStartupMessage("An existing SFTP settings file was found at " + fileSystem.SftpSettingsFile);

                    Log.WriteStartupMessage("SFTP settings will be loaded from the SFTP settings file");

                    Log.WriteStartupMessage("Loading SFTP settings from " + fileSystem.SftpSettingsFile);

                    // Load existing SFTP settings.
                    Settings.SFTP.Load(Settings, FileSystem);
                }
                else
                {
                    Log.WriteStartupMessage("An existing SFTP settings file was not found at " + fileSystem.SftpSettingsFile);

                    Log.WriteStartupMessage("SFTP settings will be loaded from the configuration file");

                    Log.WriteStartupMessage("Loading SFTP settings from " + fileSystem.ConfigFile);

                    // Acquire default SFTP settings.
                    ParseDefaultSFTPSettings();

                    Settings.SFTP.AppCodename = Settings.ApplicationCodename;
                    Settings.SFTP.AppVersion = version;
                    Settings.SFTP.Save(Settings, FileSystem);
                }

                Log.WriteStartupMessage("Loading SMTP settings (for emailing screenshots using an email server or email host)");

                if (fileSystem.FileExists(fileSystem.SmtpSettingsFile))
                {
                    Log.WriteStartupMessage("An existing SMTP settings file was found at " + fileSystem.SmtpSettingsFile);

                    Log.WriteStartupMessage("SMTP settings will be loaded from the SMTP settings file");

                    Log.WriteStartupMessage("Loading SMTP settings from " + fileSystem.SmtpSettingsFile);

                    // Load existing SMTP settings.
                    Settings.SMTP.Load(Settings, FileSystem);
                }
                else
                {
                    Log.WriteStartupMessage("An existing SMTP settings file was not found at " + fileSystem.SmtpSettingsFile);

                    Log.WriteStartupMessage("SMTP settings will be loaded from the configuration file");

                    Log.WriteStartupMessage("Loading SMTP settings from " + fileSystem.ConfigFile);

                    // Acquire default SMTP settings.
                    ParseDefaultSMTPSettings();

                    Settings.SMTP.AppCodename = Settings.ApplicationCodename;
                    Settings.SMTP.AppVersion = version;
                    Settings.SMTP.Save(Settings, FileSystem);
                }

                Log.WriteStartupMessage("Initializing Screen Capture class (this provides methods for capturing screens and saving screenshots)");

                ScreenCapture screenCapture = new ScreenCapture(this, fileSystem, Log);

                Log.WriteStartupMessage("Initializing Security class (we use this for hashing the passphrase, encrypting screenshots, and decrypting screenshots)");

                Security security = new Security(fileSystem);

                // Parse all the definitions in the configuration file for the various types of modules.

                if (!string.IsNullOrEmpty(version))
                {
                    Log.WriteStartupMessage("Parsing configuration file for Screen definitions");
                    ParseScreenDefinitions();

                    Log.WriteStartupMessage("Parsing configuration file for Region definitions");
                    ParseRegionDefinitions();

                    Log.WriteStartupMessage("Parsing configuration file for Editor definitions");
                    ParseEditorDefinitions();

                    Log.WriteStartupMessage("Parsing configuration file for Schedule definitions");
                    ParseScheduleDefinitions();

                    Log.WriteStartupMessage("Parsing configuration file for Trigger definitions");
                    ParseTriggerDefinitions();

                    // Save the data for each collection that's been loaded from the configuration file.

                    // Save the screen collection if the screens data file (screens.xml) cannot be found. This will create the default screens.
                    if (!FileSystem.FileExists(FileSystem.ScreensFile))
                    {
                        Log.WriteStartupMessage("The Screens XML data file (" + FileSystem.ScreensFile + ") could not be found so we're going to use the default Screens from the configuration file");
                        _screenCollection.SaveToXmlFile(Settings, FileSystem, Log);
                    }

                    // Save the region collection if the regions data file (regions.xml) cannot be found. This will create the default regions.
                    if (!FileSystem.FileExists(FileSystem.RegionsFile))
                    {
                        Log.WriteStartupMessage("The Regions XML data file (" + FileSystem.RegionsFile  + ") could not be found so we're going to use the default Regions from the configuration file");
                        _regionCollection.SaveToXmlFile(Settings, FileSystem, Log);
                    }

                    // Save the editor collection if the editors data file (editors.xml) cannot be found. This will create the default editors.
                    if (!FileSystem.FileExists(FileSystem.EditorsFile))
                    {
                        Log.WriteStartupMessage("The Editors XML data file (" + FileSystem.EditorsFile + ") could not be found so we're going to use the default Editors from the configuration file");
                        _editorCollection.SaveToXmlFile(Settings, FileSystem, Log);
                    }

                    // Save the schedule collection if the schedules data file (schedules.xml) cannot be found. This will create the default schedules.
                    if (!FileSystem.FileExists(FileSystem.SchedulesFile))
                    {
                        Log.WriteStartupMessage("The Schedules XML data file (" + FileSystem.SchedulesFile + ") could not be found so we're going to use the default Schedules from the configuration file");
                        _scheduleCollection.SaveToXmlFile(Settings, FileSystem, Log);
                    }

                    // Save the macro tag collection if the macro tags data file (macrotags.xml) cannot be found. This will create the default macro tags.
                    if (!FileSystem.FileExists(FileSystem.MacroTagsFile))
                    {
                        Log.WriteStartupMessage("The Macro Tags XML data file (" + FileSystem.MacroTagsFile + ") could not be found so we're going to use the default Macro Tags from the configuration file");
                        _macroTagCollection.SaveToXmlFile(this, FileSystem, Log);
                    }

                    // Save the trigger collection if the triggers data file (triggers.xml) cannot be found. This will create the default triggers.
                    if (!FileSystem.FileExists(FileSystem.TriggersFile))
                    {
                        Log.WriteStartupMessage("The Triggers XML data file (" + FileSystem.TriggersFile + ") could not be found so we're going to use the default Triggers from the configuration file");
                        _triggerCollection.SaveToXmlFile(this, FileSystem, Log);
                    }
                }
                else
                {
                    Log.WriteStartupMessage("We couldn't determine the version of the configuration file (it might be older than version 2.5) so we didn't try to parse it for definitions to create default Screens, Regions, Editors, Schedules, Macro Tags, and Triggers (which is something we can do with version 2.5 of the configuration file)");
                }

                Settings.VersionManager.OldApplicationSettings = Settings.Application.Clone();

                Settings.VersionManager.OldUserSettings = Settings.User.Clone();

                Settings.UpgradeApplicationSettings(Settings.Application, FileSystem);

                Settings.UpgradeUserSettings(Settings.User, screenCapture, security, FileSystem);

                Settings.UpgradeSftpSettings(Settings.SFTP, FileSystem);

                Settings.UpgradeSmtpSettings(Settings.SMTP, FileSystem);

                if (Convert.ToBoolean(Settings.Application.GetByKey("DebugMode").Value) ||
                    Convert.ToBoolean(Settings.Application.GetByKey("Logging").Value))
                {

                    Log.WriteStartupMessage("It looks like you have Logging and/or DebugMode enabled so you should see log files in " + FileSystem.LogsFolder);
                    Log.WriteStartupMessage("Please continue to follow the logging statements in the log files in that folder");
                }

                Log.WriteStartupMessage("If you're seeing this message then we have successfully loaded the application!");
                Log.WriteStartupMessage("Enjoy Auto Screen Capture!");
                Log.WriteStartupMessage("*** END " + DateTime.Now.ToLongTimeString() + " ***");

                return true;
            }
            catch (Exception ex)
            {
                Log.WriteStartupMessage("An error with the application was encountered when loading the configuration file at " + FileSystem.ConfigFile);
                Log.WriteStartupMessage(ex.Message);
                Log.WriteStartupMessage(ex.StackTrace);

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

                // This is another setting we don't keep in the configuration file since it's handled internally.
                Settings.User.SetValueByKey("SelectedCalendarDay", DateTime.Now);

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
                        string folder = ProcessPath(Regex.Match(line, REGEX_SCREEN).Groups["Folder"].Value); // Create any sub-folders we need.
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
                        int imageDiffTolerance = Convert.ToInt32(Regex.Match(line, REGEX_SCREEN).Groups["ImageDiffTolerance"].Value);
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
                            ImageDiffTolerance = imageDiffTolerance,
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
                        string folder = ProcessPath(Regex.Match(line, REGEX_REGION).Groups["Folder"].Value); // Create any sub-folders we need.
                        string macro = Regex.Match(line, REGEX_REGION).Groups["Macro"].Value;
                        int x = Convert.ToInt32(Regex.Match(line, REGEX_REGION).Groups["X"].Value);
                        int y = Convert.ToInt32(Regex.Match(line, REGEX_REGION).Groups["Y"].Value);
                        int width = Convert.ToInt32(Regex.Match(line, REGEX_REGION).Groups["Width"].Value);
                        int height = Convert.ToInt32(Regex.Match(line, REGEX_REGION).Groups["Height"].Value);
                        string format = Regex.Match(line, REGEX_REGION).Groups["Format"].Value;
                        int jpegQuality = Convert.ToInt32(Regex.Match(line, REGEX_REGION).Groups["JPEGQuality"].Value);
                        int imageDiffTolerance = Convert.ToInt32(Regex.Match(line, REGEX_REGION).Groups["ImageDiffTolerance"].Value);
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
                            ImageDiffTolerance = imageDiffTolerance,
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

        /// <summary>
        /// For when we're upgrading from the old format of the configuration file.
        /// </summary>
        private void UpgradeConfig()
        {
            // Folders
            const string REGEX_SCREENSHOTS_FOLDER = "^ScreenshotsFolder=(?<Path>.+)$";
            const string REGEX_DEBUG_FOLDER = "^DebugFolder=(?<Path>.+)$";
            const string REGEX_LOGS_FOLDER = "^LogsFolder=(?<Path>.+)$";

            // Files
            const string REGEX_COMMAND_FILE = "^CommandFile=(?<Path>.+)$";
            const string REGEX_EDITORS_FILE = "^EditorsFile=(?<Path>.+)$";
            const string REGEX_REGIONS_FILE = "^RegionsFile=(?<Path>.+)$";
            const string REGEX_SCREENS_FILE = "^ScreensFile=(?<Path>.+)$";
            const string REGEX_TRIGGERS_FILE = "^TriggersFile=(?<Path>.+)$";
            const string REGEX_SCREENSHOTS_FILE = "^ScreenshotsFile=(?<Path>.+)$";
            const string REGEX_MACRO_TAGS_FILE = "^MacroTagsFile=(?<Path>.+)$";
            const string REGEX_TAGS_FILE = "^TagsFile=(?<Path>.+)$"; // The old version of Macro Tags (in 2.3 I think)
            const string REGEX_SCHEDULES_FILE = "^SchedulesFile=(?<Path>.+)$";

            // Filename Pattern
            const string REGEX_FILENAME_PATTERN = "^FilenamePattern=(?<Path>.+)$";

            // Parse the old configuration file.
            foreach (string line in FileSystem.ReadFromFile(FileSystem.ConfigFile))
            {
                if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                {
                    continue;
                }

                if (Regex.IsMatch(line, REGEX_SCREENSHOTS_FOLDER))
                {
                    Settings.User.SetValueByKey("ScreenshotsFolder", Regex.Match(line, REGEX_SCREENSHOTS_FOLDER).Groups["Path"].Value);
                }

                if (Regex.IsMatch(line, REGEX_DEBUG_FOLDER))
                {
                    Settings.Application.SetValueByKey("ErrorsFolder", Regex.Match(line, REGEX_DEBUG_FOLDER).Groups["Path"].Value);
                }

                if (Regex.IsMatch(line, REGEX_LOGS_FOLDER))
                {
                    Settings.Application.SetValueByKey("LogsFolder", Regex.Match(line, REGEX_LOGS_FOLDER).Groups["Path"].Value);
                }

                if (Regex.IsMatch(line, REGEX_COMMAND_FILE))
                {
                    Settings.Application.SetValueByKey("CommandFile", Regex.Match(line, REGEX_COMMAND_FILE).Groups["Path"].Value);
                }

                if (Regex.IsMatch(line, REGEX_EDITORS_FILE))
                {
                    Settings.Application.SetValueByKey("EditorsFile", Regex.Match(line, REGEX_EDITORS_FILE).Groups["Path"].Value);
                }

                if (Regex.IsMatch(line, REGEX_REGIONS_FILE))
                {
                    Settings.Application.SetValueByKey("RegionsFile", Regex.Match(line, REGEX_REGIONS_FILE).Groups["Path"].Value);
                }

                if (Regex.IsMatch(line, REGEX_SCREENS_FILE))
                {
                    Settings.Application.SetValueByKey("ScreensFile", Regex.Match(line, REGEX_SCREENS_FILE).Groups["Path"].Value);
                }

                if (Regex.IsMatch(line, REGEX_TRIGGERS_FILE))
                {
                    Settings.Application.SetValueByKey("TriggersFile", Regex.Match(line, REGEX_TRIGGERS_FILE).Groups["Path"].Value);
                }

                if (Regex.IsMatch(line, REGEX_SCREENSHOTS_FILE))
                {
                    Settings.Application.SetValueByKey("ScreenshotsFile", Regex.Match(line, REGEX_SCREENSHOTS_FILE).Groups["Path"].Value);
                }

                if (Regex.IsMatch(line, REGEX_MACRO_TAGS_FILE))
                {
                    Settings.Application.SetValueByKey("MacroTagsFile", Regex.Match(line, REGEX_MACRO_TAGS_FILE).Groups["Path"].Value);
                }

                if (Regex.IsMatch(line, REGEX_TAGS_FILE))
                {
                    Settings.Application.SetValueByKey("MacroTagsFile", Regex.Match(line, REGEX_TAGS_FILE).Groups["Path"].Value);
                }

                if (Regex.IsMatch(line, REGEX_SCHEDULES_FILE))
                {
                    Settings.Application.SetValueByKey("SchedulesFile", Regex.Match(line, REGEX_SCHEDULES_FILE).Groups["Path"].Value);
                }

                if (Regex.IsMatch(line, REGEX_FILENAME_PATTERN))
                {
                    Settings.User.SetValueByKey("FilenamePattern", Regex.Match(line, REGEX_FILENAME_PATTERN).Groups["Path"].Value);
                }
            }
        }
    }
}
