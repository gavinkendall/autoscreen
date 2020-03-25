//-----------------------------------------------------------------------
// <copyright file="FileSystem.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
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
        public static readonly string SlidesFolder = AppDomain.CurrentDomain.BaseDirectory + "!autoscreen\\slides";

        /// <summary>
        /// The file containing the references to Editors.
        /// </summary>
        public static readonly string DefaultEditorsFile = "!autoscreen\\editors.xml";

        /// <summary>
        /// The file containing the references to Regions.
        /// </summary>
        public static readonly string DefaultRegionsFile = "!autoscreen\\regions.xml";

        /// <summary>
        /// The file containing the references to Screens.
        /// </summary>
        public static readonly string DefaultScreensFile = "!autoscreen\\screens.xml";

        /// <summary>
        /// The file containing the references to Triggers.
        /// </summary>
        public static readonly string DefaultTriggersFile = "!autoscreen\\triggers.xml";

        /// <summary>
        /// The file containing the references to Screenshots.
        /// </summary>
        public static readonly string DefaultScreenshotsFile = "!autoscreen\\screenshots.xml";

        /// <summary>
        /// The file containing the references to Tags.
        /// </summary>
        public static readonly string DefaultTagsFile = "!autoscreen\\tags.xml";

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
        /// Application Settings file.
        /// </summary>
        public static string ApplicationSettingsFile;

        /// <summary>
        /// User Settings file.
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
                Log.Write("FileSystem::DeleteFilesInDirectory", ex);
            }
        }
    }
}