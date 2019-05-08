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

    public static class FileSystem
    {
        public readonly static string PathDelimiter = "\\";

        /// <summary>
        /// The file manager to execute whenever the user chooses to open their screenshots folder or edits the selected screenshot.
        /// </summary>
        public static readonly string FileManager = "explorer";

        // Screenshots Folder
        public static readonly string ScreenshotsFolder = AppDomain.CurrentDomain.BaseDirectory + "screenshots\\";

        // Application Folder
        public static readonly string ApplicationFolder = AppDomain.CurrentDomain.BaseDirectory + "!autoscreen\\";

        // Debug
        public static readonly string DebugFolder = AppDomain.CurrentDomain.BaseDirectory + "!autoscreen\\debug\\";

        public static readonly string LogsFolder = AppDomain.CurrentDomain.BaseDirectory + "!autoscreen\\debug\\logs\\";

        // Settings
        public static readonly string UserSettingsFile = "user.xml";

        public static readonly string ApplicationSettingsFile = "application.xml";
        public static readonly string SettingsFolder = AppDomain.CurrentDomain.BaseDirectory + "!autoscreen\\settings\\";

        // Editors, Regions, Triggers, and Screenshots
        public static readonly string EditorsFile = "editors.xml";

        public static readonly string RegionsFile = "regions.xml";
        public static readonly string ScreensFile = "screens.xml";
        public static readonly string TriggersFile = "triggers.xml";
        public static readonly string ScreenshotsFile = "screenshots.xml";

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

            //if (!folderPath.EndsWith(@"\"))
            //{
            //    folderPath += @"\";
            //}

            FileInfo fileInfo = new FileInfo(folderPath);

            if (fileInfo.Directory != null && fileInfo.Directory.Root.Exists)
            {
                DriveInfo driveInfo = new DriveInfo(fileInfo.Directory.Root.FullName);

                if (driveInfo.IsReady)
                {
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    //Directory.SetCurrentDirectory(folderPath);
                }
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