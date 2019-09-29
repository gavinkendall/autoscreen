//-----------------------------------------------------------------------
// <copyright file="ScreenCapture.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public class ScreenCapture
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct CURSORINFO
        {
            public Int32 cbSize;
            public Int32 flags;
            public IntPtr hCursor;
            public POINTAPI ptScreenPos;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINTAPI
        {
            public int x;
            public int y;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern void GetWindowRect(IntPtr hWnd, out Rectangle rect);

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="ProcessId"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);

        [DllImport("user32.dll")]
        private static extern bool GetCursorInfo(out CURSORINFO pci);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool DrawIconEx(IntPtr hdc, int xLeft, int yTop, IntPtr hIcon, int cxWidth, int cyHeight, int istepIfAniCur, IntPtr hbrFlickerFreeDraw, int diFlags);

        private const Int32 CURSOR_SHOWING = 0x0001;
        private const Int32 DI_NORMAL = 0x0003;

        /// <summary>
        /// 
        /// </summary>
        public const int CAPTURE_LIMIT_MIN = 1;

        /// <summary>
        /// 
        /// </summary>
        public const int CAPTURE_LIMIT_MAX = 9999;

        private const double MIN_FREE_DISK_SPACE_PERCENTAGE = 3;

        /// <summary>
        /// The default image format.
        /// </summary>
        public const string DefaultImageFormat = ImageFormatSpec.NAME_JPEG;

        private const int MAX_CHARS = 48000;
        private const int IMAGE_RESOLUTION_RATIO_MIN = 1;

        /// <summary>
        /// 
        /// </summary>
        public const int IMAGE_RESOLUTION_RATIO_MAX = 100;

        /// <summary>
        /// 
        /// </summary>
        public  int Delay { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public  int Limit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public  int Count { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static bool LockScreenCaptureSession { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static bool RunningFromCommandLine { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Running { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool PerformingMaintenance { get; set; }

        /// <summary>
        /// The date/time when the user started a screen capture session.
        /// </summary>
        public  DateTime DateTimeStartCapture { get; set; }

        /// <summary>
        /// The date/time when screenshots are taken (or when the previous screenshots were taken).
        /// </summary>
        public  DateTime DateTimePreviousCycle { get; set; }

        /// <summary>
        /// The date/time of the next screen capture cycle.
        /// If we're still waiting for the very first screenshots to be taken then calculate from the date/time when the user started a screen capture session
        /// otherwise calculate from the date/time when the previous screenshots were taken.
        /// </summary>
        public  DateTime DateTimeNextCycle { get { return DateTimePreviousCycle.Ticks == 0 ? DateTimeStartCapture.AddMilliseconds(Delay) : DateTimePreviousCycle.AddMilliseconds(Delay); } }

        /// <summary>
        /// The time remaining between now and the next screenshot that will be taken.
        /// </summary>
        public  TimeSpan TimeRemainingForNextScreenshot { get { return DateTimeNextCycle.Subtract(DateTime.Now).Duration(); } }

        /// <summary>
        /// The title of the active window.
        /// </summary>
        public string ActiveWindowTitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ActiveWindowProcessName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public  Image GetImageByPath(string path)
        {
            Image image = null;

            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    image = Image.FromStream(stream);
                }
            }

            GC.Collect();

            return image;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="resolutionRatio"></param>
        /// <param name="mouse"></param>
        /// <returns></returns>
        public  Bitmap GetScreenBitmap(int x, int y, int width, int height, int resolutionRatio, bool mouse)
        {
            try
            {
                if (width > 0 && height > 0)
                {
                    Size blockRegionSize = new Size(width, height);

                    if (resolutionRatio < IMAGE_RESOLUTION_RATIO_MIN || resolutionRatio > IMAGE_RESOLUTION_RATIO_MAX)
                    {
                        resolutionRatio = 100;
                    }

                    float imageResolutionRatio = (float)resolutionRatio / 100;

                    int destinationWidth = (int)(width * imageResolutionRatio);
                    int destinationHeight = (int)(height * imageResolutionRatio);

                    Bitmap bitmapSource = new Bitmap(width, height);
                    Graphics graphicsSource = Graphics.FromImage(bitmapSource);

                    graphicsSource.CopyFromScreen(x, y, 0, 0, blockRegionSize, CopyPixelOperation.SourceCopy);

                    if (mouse)
                    {
                        CURSORINFO pci;
                        pci.cbSize = Marshal.SizeOf(typeof(CURSORINFO));

                        if (GetCursorInfo(out pci))
                        {
                            if (pci.flags == CURSOR_SHOWING)
                            {
                                var hdc = graphicsSource.GetHdc();
                                DrawIconEx(hdc, pci.ptScreenPos.x - x, pci.ptScreenPos.y - y, pci.hCursor, 0, 0, 0, IntPtr.Zero, DI_NORMAL);
                                graphicsSource.ReleaseHdc();
                            }
                        }
                    }

                    Bitmap bitmapDestination = new Bitmap(destinationWidth, destinationHeight);

                    Graphics graphicsDestination = Graphics.FromImage(bitmapDestination);
                    graphicsDestination.DrawImage(bitmapSource, 0, 0, destinationWidth, destinationHeight);

                    graphicsSource.Flush();
                    graphicsDestination.Flush();

                    GC.Collect();

                    return bitmapDestination;
                }

                return null;
            }
            catch (Exception ex)
            {
                // Don't log an error if Windows is locked at the time a screenshot was taken.
                if (!ex.Message.Equals("The handle is invalid"))
                {
                    Log.Write("ScreenCapture::GetScreenBitmap", ex);
                }

                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public  Bitmap GetActiveWindowBitmap()
        {
            try
            {
                Rectangle rect;
                GetWindowRect(GetForegroundWindow(), out rect);

                int width = rect.Width - rect.X;
                int height = rect.Height - rect.Y;

                if (width > 0 && height > 0)
                {
                    Bitmap bitmap = new Bitmap(width, height);

                    Graphics graphics = Graphics.FromImage(bitmap);

                    graphics.CopyFromScreen(new Point(rect.X, rect.Y), new Point(0, 0), new Size(width, height));

                    GC.Collect();

                    return bitmap;
                }

                return null;
            }
            catch (Exception ex)
            {
                // Don't log an error if Windows is locked at the time a screenshot was taken.
                if (!ex.Message.Equals("The handle is invalid"))
                {
                    Log.Write("ScreenCapture::GetScreenBitmap", ex);
                }

                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetActiveWindowTitle()
        {
            IntPtr handle;
            int chars = MAX_CHARS;

            StringBuilder buffer = new StringBuilder(chars);

            handle = GetForegroundWindow();

            if (GetWindowText(handle, buffer, chars) > 0)
            {
                // Make sure to strip out the backslash if it's in the window title.
                return buffer.ToString().Replace(@"\", string.Empty);
            }

            return "(system)";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetActiveWindowProcessName()
        {
            IntPtr hwnd = GetForegroundWindow();
            uint pid;
            GetWindowThreadProcessId(hwnd, out pid);
            Process p = Process.GetProcessById((int)pid);
            return p.ProcessName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="mouse"></param>
        /// <param name="resolutionRatio"></param>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public bool GetScreenImages(int component, int x, int y, int width, int height, bool mouse, int resolutionRatio, out Bitmap bitmap)
        {
            bitmap = component == 0
                ? GetActiveWindowBitmap()
                : GetScreenBitmap(x, y, width, height, resolutionRatio, mouse);

            GC.Collect();

            if (bitmap != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="format"></param>
        /// <param name="component"></param>
        /// <param name="screenshotType"></param>
        /// <param name="jpegQuality"></param>
        /// <param name="viewId"></param>
        /// <param name="bitmap"></param>
        /// <param name="label"></param>
        /// <param name="windowTitle"></param>
        /// <param name="processName"></param>
        /// <param name="screenCollection"></param>
        /// <param name="regionCollection"></param>
        /// <param name="screenshotCollection"></param>
        /// <returns></returns>
        public bool TakeScreenshot(string path, ImageFormat format, int component, ScreenshotType screenshotType,
            int jpegQuality, Guid viewId, Bitmap bitmap, string label, string windowTitle, string processName,
            ScreenCollection screenCollection, RegionCollection regionCollection, ScreenshotCollection screenshotCollection)
        {
            try
            {
                if (!string.IsNullOrEmpty(path))
                {
                    if (Log.DebugMode)
                        Log.Write("Attempting to write image to file at path \"" + path + "\"");

                    FileInfo fileInfo = new FileInfo(path);

                    if (fileInfo.Directory != null && fileInfo.Directory.Root.Exists)
                    {
                        DriveInfo driveInfo = new DriveInfo(fileInfo.Directory.Root.FullName);

                        if (driveInfo.IsReady)
                        {
                            double freeDiskSpacePercentage = (driveInfo.AvailableFreeSpace / (float) driveInfo.TotalSize) * 100;

                            if (Log.DebugMode)
                                Log.Write("Percentage of free disk space on drive " + fileInfo.Directory.Root.FullName + " is " + (int)freeDiskSpacePercentage + "%");

                            if (freeDiskSpacePercentage > MIN_FREE_DISK_SPACE_PERCENTAGE)
                            {
                                string dirName = Path.GetDirectoryName(path);

                                if (!string.IsNullOrEmpty(dirName))
                                {
                                    if (!Directory.Exists(dirName))
                                    {
                                        Directory.CreateDirectory(dirName);

                                        Log.Write("Directory \"" + dirName + "\" did not exist so it was created");
                                    }

                                    screenshotCollection.Add(new Screenshot(DateTimePreviousCycle, path, format, component, screenshotType, windowTitle, processName, viewId, label));

                                    SaveToFile(path, format, jpegQuality, bitmap);
                                }
                            }
                            else
                            {
                                Log.Write($"ERROR: Unable to save screenshot due to lack of available disk space on drive {fileInfo.Directory.Root.FullName} so screen capture session is being stopped");
                                return false;
                            }
                        }
                        else
                        {
                            Log.Write("WARNING: Unable to save screenshot. Drive not ready");
                        }
                    }
                    else
                    {
                        Log.Write("WARNING: Unable to save screenshot. Directory root does not exist");
                    }
                }
                else
                {
                    Log.Write("No path available");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Write("ScreenCapture::TakeScreenshot", ex);
                return false;
            }
        }

        private  void SaveToFile(string path, ImageFormat format, int jpegQuality, Bitmap bitmap)
        {
            try
            {
                if (bitmap != null && format != null && !string.IsNullOrEmpty(path))
                {
                    if (format.Name.Equals(ImageFormatSpec.NAME_JPEG))
                    {
                        var encoderParams = new EncoderParameters(1);
                        encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, jpegQuality);
                        var encoderInfo = GetEncoderInfo("image/jpeg");
                        bitmap.Save(path, encoderInfo, encoderParams);
                    }
                    else
                    {
                        bitmap.Save(path, format.Format);
                    }

                    Log.Write("Screenshot saved to file at path \"" + path + "\"");
                }
            }
            catch (Exception ex)
            {
                Log.Write("ScreenCapture::SaveToFile", ex);
            }
        }

        private  ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            var encoders = ImageCodecInfo.GetImageEncoders();
            return encoders.FirstOrDefault(t => t.MimeType == mimeType);
        }
    }
}