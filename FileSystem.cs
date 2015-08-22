//////////////////////////////////////////////////////////
// Auto Screen Capture 2.0.5
// autoscreen.FileSystem.cs
//
// Written by Gavin Kendall (gavinkendall@gmail.com)
// Thursday, 15 May 2008 - Wednesday, 27 November 2013

using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace autoscreen
{
    public static class FileSystem
    {
        private static string m_filePath;
        private static string[] m_filePaths;

        private const string REGEX_FILE_NAME = @"^(?<Date>\d{4}-\d{2}-\d{2})_(?<Time>\d{2}-\d{2}-\d{2}-\d{3})\.(?<Extension>[a-z]{3,4})$";
        private const string REGEX_SLIDE_NAME = @"^(?<Date>\d{4}-\d{2}-\d{2})\s(?<Time>\d{2}-\d{2}-\d{2}-\d{3})\s(?<Extension>[A-Z]{3,4})$";

        /// <summary>
        /// Gets the image file path based on the slide name.
        /// </summary>
        /// <param name="slideName">The name of the slide.</param>
        /// <param name="screenNumber">The screen to pull the slide from.</param>
        /// <returns></returns>
        public static string GetImageFilePath(string slideName, int screenNumber)
        {
            string filePath = string.Empty;

            if (!string.IsNullOrEmpty(slideName))
            {
                Regex rgxSlideName = new Regex(REGEX_SLIDE_NAME);

                if (rgxSlideName.IsMatch(slideName))
                {
                    string date = rgxSlideName.Match(Path.GetFileName(slideName)).Groups["Date"].Value;
                    string time = rgxSlideName.Match(Path.GetFileName(slideName)).Groups["Time"].Value;
                    string extension = rgxSlideName.Match(Path.GetFileName(slideName)).Groups["Extension"].Value;

                    filePath = m_filePath + date + "\\" + screenNumber + "\\" + date + "_" + time + "." + extension.ToLower();
                }
            }

            return filePath;
        }

        /// <summary>
        /// Gets the images associated with the slide.
        /// </summary>
        /// <param name="slideName">The name of the slide.</param>
        /// <returns></returns>
        public static ArrayList GetImages(string slideName)
        {
            ArrayList images = new ArrayList();

            for (int i = 1; i <= 5; i++)
            {
                string path = GetImageFilePath(slideName, i);

                if (System.IO.File.Exists(path))
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
                m_filePath = filePath;

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                if (!string.IsNullOrEmpty(monthCalendarFolder))
                {
                    if (Directory.Exists(filePath + monthCalendarFolder))
                    {
                        m_filePaths = Directory.GetFiles(filePath + monthCalendarFolder + "\\1\\", monthCalendarFolder + Properties.Settings.Default.ImageFormatFilter, SearchOption.TopDirectoryOnly);

                        if (m_filePaths != null)
                        {
                            string[] files = new string[m_filePaths.Length];

                            for (int i = 0; i < m_filePaths.Length; i++)
                            {
                                string localFilePath = m_filePaths[i];

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
            }

            return null;
        }
    }
}
