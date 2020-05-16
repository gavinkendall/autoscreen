//-----------------------------------------------------------------------
// <copyright file="FileSystem.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>All the methods used for basic IO operations such as creating files, deleting files, and checking if drives are ready.</summary>
//-----------------------------------------------------------------------
using System;
using System.Drawing;
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
                if (DirectoryExists(dirName))
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

        /// <summary>
        /// Determines if the directory exists for a given path.
        /// </summary>
        /// <param name="path">The path to check if the directory exists.</param>
        /// <returns>True if the directory exists. False if the directory does not exist.</returns>
        public static bool DirectoryExists(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            return Directory.Exists(path);
        }

        /// <summary>
        /// Creates a directory for a given path.
        /// </summary>
        /// <param name="path">The path of the directory to create.</param>
        public static void CreateDirectory(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            Directory.CreateDirectory(path);
        }

        /// <summary>
        /// Determines if the file exists for a given path.
        /// </summary>
        /// <param name="path">The path of the file to check.</param>
        /// <returns>True if the file exists. False if the file does not exist.</returns>
        public static bool FileExists(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            return File.Exists(path);
        }

        /// <summary>
        /// Determines if the file in the given path has a file extension.
        /// </summary>
        /// <param name="path">The path of the file to check.</param>
        /// <returns>True if the file has a file extension. False if the file does not have a file extension.</returns>
        public static bool HasExtension(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            return Path.HasExtension(path);
        }

        /// <summary>
        /// Creates a new file given the path of the file.
        /// </summary>
        /// <param name="path">The path of the file to create.</param>
        public static void CreateFile(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            File.Create(path).Dispose();
        }

        /// <summary>
        /// Gets the free disk space percentage of the drive for a given path.
        /// </summary>
        /// <param name="path">The path of the drive to check.</param>
        /// <returns>The free disk space percentage.</returns>
        public static double FreeDiskSpacePercentage(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return 0;
            }

            FileInfo fileInfo = new FileInfo(path);
            DriveInfo driveInfo = new DriveInfo(fileInfo.Directory.Root.FullName);

            return (driveInfo.AvailableFreeSpace / (float)driveInfo.TotalSize) * 100;
        }

        /// <summary>
        /// Determines if the drive of the given path is ready.
        /// </summary>
        /// <param name="path">The path to check for the drive's status.</param>
        /// <returns>True if the drive is ready. False if the drive is not ready.</returns>
        public static bool DriveReady(string path)
        {
            if (string.IsNullOrEmpty(path) || path.StartsWith(PathDelimiter))
            {
                return false;
            }

            FileInfo fileInfo = new FileInfo(path);
            DriveInfo driveInfo = new DriveInfo(fileInfo.Directory.Root.FullName);

            return driveInfo.IsReady;
        }

        /// <summary>
        /// Gets the image from a given path.
        /// </summary>
        /// <param name="path">The path of the image file to get the image from.</param>
        /// <returns>The image of an image file.</returns>
        public static Image GetImage(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }

            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                return Image.FromStream(stream);
            }
        }

        /// <summary>
        /// Gets the name of the directory from a given path.
        /// </summary>
        /// <param name="path">The path to get the directory name.</param>
        /// <returns>The directory name of the given path.</returns>
        public static string GetDirectoryName(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }

            return Path.GetDirectoryName(path);
        }

        /// <summary>
        /// Gets the filename of the given path.
        /// </summary>
        /// <param name="path">The path to get the filename from.</param>
        /// <returns>The filename of the given path.</returns>
        public static string GetFileName(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }

            return Path.GetFileName(path);
        }

        /// <summary>
        /// Writes a line to a file.
        /// </summary>
        /// <param name="path">The path to the file to be written.</param>
        /// <param name="line">The line of text to write to the file.</param>
        public static void WriteToFile(string path, string line)
        {
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            File.WriteAllText(path, line);
        }

        /// <summary>
        /// Writes multiple lines to a file.
        /// </summary>
        /// <param name="path">The path to the file to be written.</param>
        /// <param name="linesToWrite">The array of lines to write to the file.</param>
        public static void WriteToFile(string path, string[] linesToWrite)
        {
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            File.WriteAllLines(path, linesToWrite);
        }

        /// <summary>
        /// Appends a line to an existing file.
        /// </summary>
        /// <param name="path">The path to the file to be written.</param>
        /// <param name="line">The line to append to the file.</param>
        public static void AppendToFile(string path, string line)
        {
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(line);
                sw.Flush();
                sw.Close();
            }
        }

        /// <summary>
        /// Reads from a file.
        /// </summary>
        /// <param name="path">The path to the file to read from.</param>
        /// <returns>An array of lines representing the lines of text in the file.</returns>
        public static string[] ReadFromFile(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }

            return File.ReadAllLines(path);
        }

        /// <summary>
        /// Gets the directory separator character.
        /// </summary>
        /// <returns>The directory separator character.</returns>
        public static char DirectorySeparatorChar()
        {
            return Path.DirectorySeparatorChar;
        }

        /// <summary>
        /// Deletes a file.
        /// </summary>
        /// <param name="path">The path of the file to delete.</param>
        public static void DeleteFile(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            File.Delete(path);
        }

        /// <summary>
        /// Gets the length of a file's contents.
        /// </summary>
        /// <param name="path">The path to the file to check.</param>
        /// <returns>The number of characters in the file.</returns>
        public static long FileContentLength(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return -1;
            }

            FileInfo fileInfo = new FileInfo(path);
            return fileInfo.Length;
        }
    }
}