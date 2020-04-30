//-----------------------------------------------------------------------
// <copyright file="FileSystem.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
using System;
using System.IO;

namespace AutoScreenCapture
{
    /// <summary>
    /// Anything involving files and folders are defined in this class.
    /// </summary>
    public static class FileSystem
    {
        /// <summary>
        /// The path delimiter to use.
        /// </summary>
        public readonly static string PathDelimiter = Path.DirectorySeparatorChar.ToString();

        /// <summary>
        /// The file manager to execute whenever the user chooses to open their screenshots folder or edits the selected screenshot.
        /// </summary>
        public static readonly string FileManager = "explorer";

        /// <summary>
        /// The name of the Auto Screen Capture Configuration File.
        /// </summary>
        public static string ConfigFile = AppDomain.CurrentDomain.BaseDirectory + "autoscreen.conf";

        /// <summary>
        /// This is to be backwards compatible with an old version of the application that used the "slides" folder.
        /// </summary>
        public static readonly string SlidesFolder = AppDomain.CurrentDomain.BaseDirectory + "!autoscreen\\slides\\";

        /// <summary>
        /// The file to create when the user runs an instance of autoscreen while existing instances are already running.
        /// </summary>
        public static readonly string StartupErrorFile = AppDomain.CurrentDomain.BaseDirectory + "autoscreen_startup_error.txt";

        /// <summary>
        /// Default settings folder.
        /// </summary>
        public static string DefaultSettingsFolder = AppDomain.CurrentDomain.BaseDirectory + "!autoscreen\\settings\\";

        /// <summary>
        /// Default screenshots folder.
        /// </summary>
        public static string DefaultScreenshotsFolder = AppDomain.CurrentDomain.BaseDirectory + "screenshots\\";

        /// <summary>
        /// Default debug folder.
        /// </summary>
        public static string DefaultDebugFolder = AppDomain.CurrentDomain.BaseDirectory + "!autoscreen\\debug\\";

        /// <summary>
        /// Default logs folder.
        /// </summary>
        public static string DefaultLogsFolder = AppDomain.CurrentDomain.BaseDirectory + "!autoscreen\\logs\\";

        /// <summary>
        /// The default command file if the actual command file cannot be found. This will most likely happen with an
        /// older version of Auto Screen Capture so we need to make sure that, since 2.3.0.0, we have a command file.
        /// </summary>
        public static string DefaultCommandFile = AppDomain.CurrentDomain.BaseDirectory + "!autoscreen\\command.txt";

        /// <summary>
        /// Default application settings file.
        /// </summary>
        public static string DefaultApplicationSettingsFile = AppDomain.CurrentDomain.BaseDirectory + "!autoscreen\\settings\\application.xml";

        /// <summary>
        /// Default user settings file.
        /// </summary>
        public static string DefaultUserSettingsFile = AppDomain.CurrentDomain.BaseDirectory + "!autoscreen\\settings\\user.xml";

        /// <summary>
        /// The file containing the references to Editors.
        /// </summary>
        public static readonly string DefaultEditorsFile = AppDomain.CurrentDomain.BaseDirectory + "!autoscreen\\editors.xml";

        /// <summary>
        /// The file containing the references to Regions.
        /// </summary>
        public static readonly string DefaultRegionsFile = AppDomain.CurrentDomain.BaseDirectory + "!autoscreen\\regions.xml";

        /// <summary>
        /// The file containing the references to Screens.
        /// </summary>
        public static readonly string DefaultScreensFile = AppDomain.CurrentDomain.BaseDirectory + "!autoscreen\\screens.xml";

        /// <summary>
        /// The file containing the references to Triggers.
        /// </summary>
        public static readonly string DefaultTriggersFile = AppDomain.CurrentDomain.BaseDirectory + "!autoscreen\\triggers.xml";

        /// <summary>
        /// The file containing the references to Screenshots.
        /// </summary>
        public static readonly string DefaultScreenshotsFile = AppDomain.CurrentDomain.BaseDirectory + "!autoscreen\\screenshots.xml";

        /// <summary>
        /// The file containing the references to Tags.
        /// </summary>
        public static readonly string DefaultTagsFile = AppDomain.CurrentDomain.BaseDirectory + "!autoscreen\\tags.xml";

        /// <summary>
        /// The file containing the references to Schedules.
        /// </summary>
        public static readonly string DefaultSchedulesFile = AppDomain.CurrentDomain.BaseDirectory + "!autoscreen\\schedules.xml";

        /// <summary>
        /// Screenshots folder.
        /// </summary>
        public static string ScreenshotsFolder;

        /// <summary>
        /// Debug folder.
        /// </summary>
        public static string DebugFolder;

        /// <summary>
        /// Logs folder.
        /// </summary>
        public static string LogsFolder;

        /// <summary>
        /// Command file.
        /// </summary>
        public static string CommandFile;

        /// <summary>
        /// Application settings file.
        /// </summary>
        public static string ApplicationSettingsFile;

        /// <summary>
        /// User settings file.
        /// </summary>
        public static string UserSettingsFile;

        /// <summary>
        /// Editors file.
        /// </summary>
        public static string EditorsFile;

        /// <summary>
        /// Regions file.
        /// </summary>
        public static string RegionsFile;

        /// <summary>
        /// Screens file.
        /// </summary>
        public static string ScreensFile;

        /// <summary>
        /// Triggers file.
        /// </summary>
        public static string TriggersFile;

        /// <summary>
        /// Screenshots file.
        /// </summary>
        public static string ScreenshotsFile;

        /// <summary>
        /// Tags file.
        /// </summary>
        public static string TagsFile;

        /// <summary>
        /// Schedules file.
        /// </summary>
        public static string SchedulesFile;

        /// <summary>
        /// Just in case the user gives us an empty folder path or forgets
        /// to include the trailing backslash for the screenshots folder.
        /// </summary>
        /// <param name="screenshotsFolderPath">The path of the screenshots folder to correct.</param>
        /// <returns>A corrected version of the screenshots folder path.</returns>
        public static string CorrectScreenshotsFolderPath(string screenshotsFolderPath)
        {
            if (string.IsNullOrEmpty(screenshotsFolderPath) || screenshotsFolderPath.Length <= 0)
            {
                screenshotsFolderPath = ScreenshotsFolder;
            }

            if (!screenshotsFolderPath.EndsWith(PathDelimiter))
            {
                screenshotsFolderPath += PathDelimiter;
            }

            return screenshotsFolderPath;
        }

        /// <summary>
        /// Deletes files recursively based on the specified directory.
        /// </summary>
        /// <param name="dirName">The starting "parent" directory (which will also be deleted after everything else inside it is deleted).</param>
        public static void DeleteFilesInDirectory(string dirName)
        {
            try
            {
                if (Directory.Exists(dirName))
                {
                    DirectoryInfo di = new DirectoryInfo(dirName);

                    foreach (FileInfo file in di.EnumerateFiles())
                    {
                        file.Delete();
                    }

                    foreach (DirectoryInfo dir in di.EnumerateDirectories())
                    {
                        dir.Delete(true);
                    }

                    Directory.Delete(dirName, true);
                }
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("FileSystem::DeleteFilesInDirectory", ex);
            }
        }
    }
}