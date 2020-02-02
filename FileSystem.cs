//-----------------------------------------------------------------------
// <copyright file="FileSystem.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    using System;
    using System.IO;

    /// <summary>
    /// 
    /// </summary>
    public static class FileSystem
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly static string PathDelimiter = Path.DirectorySeparatorChar.ToString();

        /// <summary>
        /// The file manager to execute whenever the user chooses to open their screenshots folder or edits the selected screenshot.
        /// </summary>
        public static readonly string FileManager = "explorer";

        /// <summary>
        /// 
        /// </summary>
        public static string ConfigFile = AppDomain.CurrentDomain.BaseDirectory + "autoscreen.conf";

        /// <summary>
        /// This is to be backwards compatible with an old version of the application that used the "slides" folder.
        /// </summary>
        public static readonly string SlidesFolder = AppDomain.CurrentDomain.BaseDirectory + "!autoscreen\\slides";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string DefaultEditorsFile = "!autoscreen\\editors.xml";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string DefaultRegionsFile = "!autoscreen\\regions.xml";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string DefaultScreensFile = "!autoscreen\\screens.xml";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string DefaultTriggersFile = "!autoscreen\\triggers.xml";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string DefaultScreenshotsFile = "!autoscreen\\screenshots.xml";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string DefaultTagsFile = "!autoscreen\\tags.xml";

        /// <summary>
        /// 
        /// </summary>
        public static string ScreenshotsFolder;

        /// <summary>
        /// 
        /// </summary>
        public static string DebugFolder;

        /// <summary>
        /// 
        /// </summary>
        public static string LogsFolder;

        /// <summary>
        /// 
        /// </summary>
        public static string ApplicationSettingsFile;

        /// <summary>
        /// 
        /// </summary>
        public static string UserSettingsFile;

        /// <summary>
        /// 
        /// </summary>
        public static string EditorsFile;

        /// <summary>
        /// 
        /// </summary>
        public static string RegionsFile;

        /// <summary>
        /// 
        /// </summary>
        public static string ScreensFile;

        /// <summary>
        /// 
        /// </summary>
        public static string TriggersFile;

        /// <summary>
        /// 
        /// </summary>
        public static string ScreenshotsFile;

        /// <summary>
        /// 
        /// </summary>
        public static string TagsFile;

        /// <summary>
        /// Just in case the user gives us an empty folder path or forgets
        /// to include the trailing backslash for the screenshots folder.
        /// </summary>
        /// <param name="screenshotsFolderPath"></param>
        /// <returns></returns>
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