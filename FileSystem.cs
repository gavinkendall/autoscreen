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
        public readonly static string PathDelimiter = "\\";

        /// <summary>
        /// The file manager to execute whenever the user chooses to open their screenshots folder or edits the selected screenshot.
        /// </summary>
        public static readonly string FileManager = "explorer";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string ScreenshotsFolder = AppDomain.CurrentDomain.BaseDirectory + "screenshots\\";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string ApplicationFolder = AppDomain.CurrentDomain.BaseDirectory + "!autoscreen\\";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string DebugFolder = AppDomain.CurrentDomain.BaseDirectory + "!autoscreen\\debug\\";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string LogsFolder = AppDomain.CurrentDomain.BaseDirectory + "!autoscreen\\debug\\logs\\";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string SlidesFolder = AppDomain.CurrentDomain.BaseDirectory + "!autoscreen\\slides\\";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string UserSettingsFile = "user.xml";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string ApplicationSettingsFile = "application.xml";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string SettingsFolder = AppDomain.CurrentDomain.BaseDirectory + "!autoscreen\\settings\\";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string EditorsFile = "editors.xml";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string RegionsFile = "regions.xml";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string ScreensFile = "screens.xml";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string TriggersFile = "triggers.xml";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string ScreenshotsFile = "screenshots_%date%.xml";

        public static readonly string OldScreenshotsFile = "screenshots.xml";

        /// <summary>
        /// Just in case the user gives us an empty folder path or forgets to include the trailing backslash.
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        public static string CorrectDirectoryPath(string folderPath)
        {
            if (folderPath.Length == 0)
            {
                folderPath = FileSystem.ScreenshotsFolder;
            }

            if (!folderPath.EndsWith(@"\"))
            {
                folderPath += @"\";
            }

            return folderPath;
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