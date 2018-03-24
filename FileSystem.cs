//////////////////////////////////////////////////////////
// Auto Screen Capture 2.1.0
// autoscreen.FileSystem.cs
//
// Developed by Gavin Kendall
// Thursday, 15 May 2008 - Monday, 12 March 2018

using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;

namespace autoscreen
{
    public static class FileSystem
    {
        private static string _filePath;
        public readonly static string PathDelimiter = "\\";

        private const string REGEX_FILE_NAME = @"^(?<Date>\d{4}-\d{2}-\d{2})_(?<Time>\d{2}-\d{2}-\d{2}-\d{3})\.(?<Extension>[a-z]{3,4})$";
        private const string REGEX_SLIDE_NAME = @"^(?<Date>\d{4}-\d{2}-\d{2})\s(?<Time>\d{2}-\d{2}-\d{2}-\d{3})\s(?<Extension>[A-Z]{3,4})$";

        /// <summary>
        /// The file manager to execute whenever the user chooses to open their screenshots folder or edits the selected screenshot.
        /// </summary>
        public static readonly string FileManager = "explorer";

        public static readonly string ScreenshotsFolder = AppDomain.CurrentDomain.BaseDirectory + "screenshots\\";
        public static readonly string UserAppDataLocalDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\.autoscreen\\";

        /// <summary>
        /// Gets the image file path based on the slide name.
        /// </summary>
        /// <param name="slideName">The name of the slide.</param>
        /// <param name="screenNumber">The screen to pull the slide from.</param>
        /// <returns></returns>
        public static string GetImageFilePath(string slideName, int screenNumber)
        {
            string filePath = string.Empty;

            if (screenNumber <= 0)
            {
                screenNumber = 1;
            }

            if (!string.IsNullOrEmpty(slideName))
            {
                Regex rgxSlideName = new Regex(REGEX_SLIDE_NAME);

                if (rgxSlideName.IsMatch(slideName))
                {
                    string date = rgxSlideName.Match(Path.GetFileName(slideName)).Groups["Date"].Value;
                    string time = rgxSlideName.Match(Path.GetFileName(slideName)).Groups["Time"].Value;
                    string extension = rgxSlideName.Match(Path.GetFileName(slideName)).Groups["Extension"].Value;

                    filePath = _filePath + date + PathDelimiter + screenNumber + PathDelimiter + date + "_" + time + "." + extension.ToLower();
                }
            }

            return filePath;
        }

        /// <summary>
        /// Gets the images associated with the slide.
        /// </summary>
        /// <param name="slideName">The name of the slide.</param>
        /// <returns></returns>
        public static ArrayList GetImages(string slideName, DateTime selectedDate)
        {
            ArrayList images = new ArrayList();

            for (int i = 1; i <= 5; i++)
            {
                string path = GetImageFilePath(slideName, i);

                if (File.Exists(path))
                {
                    images.Add(Bitmap.FromFile(path));
                }
                else
                {
                    images.Add(null);
                }
            }

            return images;
        }

        /// <summary>
        /// Gets an array of file paths based on the given file path and the name of the folder chosen from the calendar.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="monthCalendarFolder">The chosen calendar folder.</param>
        /// <returns></returns>
        public static string[] GetFiles(string filePath, string monthCalendarFolder)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                _filePath = filePath;

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                if (!string.IsNullOrEmpty(monthCalendarFolder) && Directory.Exists(filePath + monthCalendarFolder))
                {
                    string[] filePaths = Directory.GetFiles(filePath + monthCalendarFolder + "\\1\\", monthCalendarFolder + Properties.Settings.Default.ImageFormatFilter, SearchOption.TopDirectoryOnly);

                    if (filePaths != null)
                    {
                        string[] files = new string[filePaths.Length];

                        for (int i = 0; i < filePaths.Length; i++)
                        {
                            string localFilePath = filePaths[i];

                            Regex rgxFileName = new Regex(REGEX_FILE_NAME);

                            if (rgxFileName.IsMatch(Path.GetFileName(localFilePath)))
                            {
                                string date = rgxFileName.Match(Path.GetFileName(localFilePath)).Groups["Date"].Value;
                                string time = rgxFileName.Match(Path.GetFileName(localFilePath)).Groups["Time"].Value;
                                string extension = rgxFileName.Match(Path.GetFileName(localFilePath)).Groups["Extension"].Value;

                                files[i] = date + " " + time + " " + extension.ToUpper();
                            }
                        }

                        return files;
                    }
                }
            }

            return new string[0];
        }

        /// <summary>
        /// Deletes files recursively based on the specified folder.
        /// </summary>
        /// <param name="monthCalendarFolder">The starting "parent" folder (which will also be deleted after everything else inside it is deleted).</param>
        public static void DeleteFilesInFolder(string monthCalendarFolder)
        {
            try
            {
                if (Directory.Exists(monthCalendarFolder))
                {
                    DirectoryInfo di = new DirectoryInfo(monthCalendarFolder);

                    foreach (FileInfo file in di.EnumerateFiles())
                    {
                        file.Delete();
                    }

                    foreach (DirectoryInfo dir in di.EnumerateDirectories())
                    {
                        dir.Delete(true);
                    }

                    Directory.Delete(monthCalendarFolder, true);
                }
            }
            catch (Exception ex)
            {
                Log.Write("FileSystem::DeleteFilesInFolder", ex);
            }
        }
    }
}