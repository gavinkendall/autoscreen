//-----------------------------------------------------------------------
// <copyright file="ScreenCapture.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A class for handling screen capture functionality.</summary>
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
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace AutoScreenCapture
{
    /// <summary>
    /// Enum that describes the flags being passed from screen saving function result.
    /// Written by Oskars Grauzis (https://github.com/grauziitisos)
    /// </summary>
    public enum ScreenSavingErrorLevels
    {
        /// <summary>
        /// No error occured
        /// </summary>
        None = 0,

        /// <summary>
        /// Directory name for screenshot with given path could not be found"
        /// </summary>
        DirNotFound = 2,

        /// <summary>
        /// Could not save screenshot with given path because its image is not significantly different compared with the previous image
        /// </summary>
        ImageDiffNotSignificant = 4,

        /// <summary>
        /// Cannot write to given path because the user may not have the appropriate permissions to access the path
        /// </summary>
        UserNotEnoughPermissions = 8,

        /// <summary>
        /// Running screen capture session has stopped because application setting StopOnLowDiskError was set to True when the available disk space on any drive was lower than the value of LowDiskPercentageThreshold
        /// </summary>
        StopOnLowDiskError = 16,

        /// <summary>
        /// Unable to save screenshot for given path because the drive is not found or not ready
        /// </summary>
        DriveNotReady = 32,

        /// <summary>
        /// The path is too long. The path is given, but the length exceeds what Windows can handle so the file could not be saved. There is probably an exception error from Windows explaining why
        /// </summary>
        PathLengthExceeded = 64,

        /// <summary>
        /// Another exception caught when trying to save. User should check the log for the logged exception message details.
        /// </summary>
        ExceptionCaught = 128
    }

    /// <summary>
    /// This class is responsible for getting bitmap images of the screen area. It also saves screenshots to the file system.
    /// </summary>
    public class ScreenCapture
    {
        private readonly Log _log;
        private readonly Config _config;
        private readonly FileSystem _fileSystem;

        [StructLayout(LayoutKind.Sequential)]
        private struct CURSORINFO
        {
            public int cbSize;
            public int flags;
            public IntPtr hCursor;
            public POINTAPI ptScreenPos;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINTAPI
        {
            public int x;
            public int y;
        }

        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll")]
        private static extern bool DeleteDC(IntPtr hDC);

        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        [DllImport("gdi32.dll")]
        private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport("user32.dll")]
        private static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern void GetWindowRect(IntPtr hWnd, out Rectangle rect);

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);

        [DllImport("user32.dll")]
        private static extern bool GetCursorInfo(out CURSORINFO pci);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool DrawIconEx(IntPtr hdc, int xLeft, int yTop, IntPtr hIcon, int cxWidth, int cyHeight, int istepIfAniCur, IntPtr hbrFlickerFreeDraw, int diFlags);

        private const int CURSOR_SHOWING = 0x0001;
        private const int DI_NORMAL = 0x0003;

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("gdi32.dll")]
        private static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, CopyPixelOperation dwRop);

        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        private static WINDOWPLACEMENT GetPlacement(IntPtr hwnd)
        {
            WINDOWPLACEMENT placement = new WINDOWPLACEMENT();
            placement.length = Marshal.SizeOf(placement);
            GetWindowPlacement(hwnd, ref placement);
            return placement;
        }

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        internal enum ShowWindowCommands : int
        {
            Hide = 0,
            Normal = 1,
            Minimized = 2,
            Maximized = 3,
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        internal struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public ShowWindowCommands showCmd;
            public Point ptMinPosition;
            public Point ptMaxPosition;
            public Rectangle rcNormalPosition;
        }

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        private const int ENUM_CURRENT_SETTINGS = -1;

        [DllImport("user32.dll")]
        private static extern bool EnumDisplaySettings(string lpszDeviceName, int iModeNum, ref DEVMODE lpDevMode);

        [StructLayout(LayoutKind.Sequential)]
        private struct DEVMODE
        {
            private const int CCHDEVICENAME = 0x20;
            private const int CCHFORMNAME = 0x20;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            private readonly string dmDeviceName;
            private readonly short dmSpecVersion;
            private readonly short dmDriverVersion;
            internal short dmSize;
            private readonly short dmDriverExtra;
            private readonly int dmFields;
            private readonly int dmPositionX;
            private readonly int dmPositionY;
            private readonly ScreenOrientation dmDisplayOrientation;
            private readonly int dmDisplayFixedOutput;
            private readonly short dmColor;
            private readonly short dmDuplex;
            private readonly short dmYResolution;
            private readonly short dmTTOption;
            private readonly short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            private readonly string dmFormName;
            private readonly short dmLogPixels;
            private readonly int dmBitsPerPel;
            internal readonly int dmPelsWidth;
            internal readonly int dmPelsHeight;
            private readonly int dmDisplayFlags;
            private readonly int dmDisplayFrequency;
            private readonly int dmICMMethod;
            private readonly int dmICMIntent;
            private readonly int dmMediaType;
            private readonly int dmDitherType;
            private readonly int dmReserved1;
            private readonly int dmReserved2;
            private readonly int dmPanningWidth;
            private readonly int dmPanningHeight;
        }

        private DEVMODE _dm;

        private DeviceOptions _device;

        /// <summary>
        /// A struct containing a Windows Forms screen object, display device width, and display device height.
        /// </summary>
        public struct DeviceOptions
        {
            /// <summary>
            /// A Windows Forms screen object.
            /// </summary>
            public System.Windows.Forms.Screen screen;

            /// <summary>
            /// The width of the display device.
            /// </summary>
            public int width;

            /// <summary>
            /// The height of the display device.
            /// </summary>
            public int height;
        }

        /// <summary>
        /// The minimum capture limit.
        /// </summary>
        public const int CAPTURE_LIMIT_MIN = 1;

        /// <summary>
        /// The maximum capture limit.
        /// </summary>
        public const int CAPTURE_LIMIT_MAX = 9999;

        /// <summary>
        /// The image format as defined by the configuration file (autoscreen.conf).
        /// </summary>
        public static string ImageFormat;

        /// <summary>
        /// Default maximum characters allowd for active window title.
        /// </summary>
        private const int MAX_CHARS = 48000;

        /// <summary>
        /// The minimum image resolution ratio.
        /// </summary>
        private const int IMAGE_RESOLUTION_RATIO_MIN = 1;

        /// <summary>
        /// The maximum image resolution ratio.
        /// </summary>
        public const int IMAGE_RESOLUTION_RATIO_MAX = 100;

        /// <summary>
        /// The interval delay for the timer when a screen capture session is running.
        /// </summary>
        public int Interval { get; set; }

        /// <summary>
        /// Determines if we compare the hash of the latest screenshot with the previous screeenshot before saving and collect the hash of each screenshot for comparison before emailing from trigger.
        /// </summary>
        public bool OptimizeScreenCapture { get; set; }

        /// <summary>
        /// The limit on how many screen capture cycles we go through during a screen capture session.
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// The number of screen capture cycles we've gone through during a screen capture session.
        /// </summary>
        public int CycleCount { get; set; }

        /// <summary>
        /// The numer of times the user did a "Capture Now".
        /// </summary>
        public int CaptureNowCount { get; set; }

        /// <summary>
        /// Determines if the screen capture session is locked.
        /// </summary>
        public bool LockScreenCaptureSession { get; set; }

        /// <summary>
        /// Determines if we automatically start a screen capture session immediately when command line arguments are used.
        /// </summary>
        public bool AutoStartFromCommandLine { get; set; }

        /// <summary>
        /// Determines if a screen capture session is currently running.
        /// </summary>
        public bool Running { get; set; }

        /// <summary>
        ///  Determines if the application encountered an error.
        /// </summary>
        public bool ApplicationError { get; set; }

        /// <summary>
        /// Determines if the application encountered an event that isn't an error but we still need to inform the user about.
        /// </summary>
        public bool ApplicationWarning { get; set; }

        /// <summary>
        /// Determines if we had an error during screen capture.
        /// </summary>
        public bool CaptureError { get; set; }

        /// <summary>
        /// The date/time when the user started a screen capture session.
        /// </summary>
        public DateTime DateTimeStartCapture { get; set; }

        /// <summary>
        /// The date/time when screenshots are taken.
        /// </summary>
        public DateTime DateTimeScreenshotsTaken { get; set; }

        /// <summary>
        /// The date/time when the previous cycle of screenshots were taken.
        /// </summary>
        public DateTime DateTimePreviousCycle { get; set; }

        /// <summary>
        /// The date/time of the next screen capture cycle.
        /// If we're still waiting for the very first screenshots to be taken then calculate from the date/time when the user started a screen capture session
        /// otherwise calculate from the date/time when the previous screenshots were taken.
        /// </summary>
        public DateTime DateTimeNextCycle { get { return DateTimePreviousCycle.Ticks == 0 ? DateTimeStartCapture.AddMilliseconds(Interval) : DateTimePreviousCycle.AddMilliseconds(Interval); } }

        /// <summary>
        /// The time remaining between now and the next screenshot that will be taken.
        /// </summary>
        public TimeSpan TimeRemainingForNextScreenshot { get { return DateTimeNextCycle.Subtract(DateTime.Now).Duration(); } }

        /// <summary>
        /// The title of the active window.
        /// </summary>
        public string ActiveWindowTitle { get; set; }

        /// <summary>
        /// The process name associated with the active window.
        /// </summary>
        public string ActiveWindowProcessName { get; set; }

        /// <summary>
        /// Determines if we're capturing the screen now or let a scheduled capture occur.
        /// </summary>
        public bool CaptureNow { get; set; }

        /// <summary>
        /// The scope of the screen capture.
        /// </summary>
        public string Scope { get; set; }

        /// <summary>
        /// A class for handling screen capture methods.
        /// </summary>
        public ScreenCapture(Config config, FileSystem fileSystem, Log log)
        {
            _log = log;
            _config = config;
            _fileSystem = fileSystem;

            _dm = new DEVMODE();
            _device = new DeviceOptions();

            Scope = "All Screens and Regions";
        }

        private ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            var encoders = ImageCodecInfo.GetImageEncoders();
            return encoders.FirstOrDefault(t => t.MimeType == mimeType);
        }

        private int NotEnoughDiskSpace(Screenshot screenshot, int returnFlag, double freeDiskSpacePercentage, int lowDiskPercentageThreshold, long freeDisk, long lowDiskBytesThreshold)
        {
            // There is not enough disk space on the drive so log a few error messages.

            _log.WriteErrorMessage($"Unable to save screenshot due to lack of available disk space on drive for \"{screenshot.FilePath}\"");

            // Get the preferred "LowDiskMode".
            // 0 == We check the available disk space as a percentage.
            // 1 == We check the available disk space as a number of bytes.
            int lowDiskMode = Convert.ToInt32(_config.Settings.Application.GetByKey("LowDiskMode").Value); 

            // Percentage.
            if (lowDiskMode == 0)
            {
                _log.WriteErrorMessage("LowDiskMode is set to 0 so we check for percentage threshold of free disk space");
                _log.WriteErrorMessage($"Free disk space percentage: {freeDiskSpacePercentage}%");
                _log.WriteErrorMessage($"Configured low disk percentage threshold: {lowDiskPercentageThreshold}%");
            }

            // Number of bytes.
            if (lowDiskMode == 1)
            {
                _log.WriteErrorMessage("LowDiskMode is set to 1 so we check for bytes threshold of free disk space");
                _log.WriteErrorMessage($"Free disk space in bytes: {freeDisk} bytes");
                _log.WriteErrorMessage($"Configured low disk bytes threshold: {lowDiskBytesThreshold} bytes");
            }

            bool stopOnLowDiskError = Convert.ToBoolean(_config.Settings.Application.GetByKey("StopOnLowDiskError").Value);

            if (stopOnLowDiskError)
            {
                _log.WriteErrorMessage("Running screen capture session has stopped because application setting StopOnLowDiskError is set to True when the available disk space on any drive is too low");

                ApplicationError = true;

                return returnFlag | (int)ScreenSavingErrorLevels.StopOnLowDiskError;
            }

            ApplicationWarning = true;

            return returnFlag & (int)ScreenSavingErrorLevels.None;
        }

        private int SaveToFile(Security security, int jpegQuality, Screenshot screenshot, ScreenshotCollection screenshotCollection)
        {
            int returnFlag = 0;
            string dirName = _fileSystem.GetDirectoryName(screenshot.FilePath);

            if (string.IsNullOrEmpty(dirName))
            {
                _log.WriteDebugMessage("Directory name for screenshot with path \"" + screenshot.FilePath + "\" could not be found");

                return returnFlag | (int)ScreenSavingErrorLevels.DirNotFound;
            }

            try
            {
                if (!_fileSystem.DirectoryExists(dirName))
                {
                    _fileSystem.CreateDirectory(dirName);

                    _log.WriteDebugMessage("Directory \"" + dirName + "\" did not exist so it was created");
                }

                // Attempt to process the screenshot before it's saved to disk.
                // This means we'll check for image diff tolerance (if Optimize Screen Capture is enabled).
                if (screenshotCollection.Process(screenshot))
                {
                    // Save the screenshot to disk if it processed successfully.
                    SaveToFile(screenshot, security, jpegQuality);

                    return returnFlag & (int)ScreenSavingErrorLevels.None;
                }
                else
                {
                    _log.WriteDebugMessage("Could not save screenshot with ID \"" + screenshot.Id + "\" and path \"" + screenshot.FilePath + "\" because the image difference percentage with the previous screenshot's image (" + screenshot.DiffPercentageWithPreviousImage + "%) wasn't significant enough");

                    return returnFlag | (int)ScreenSavingErrorLevels.ImageDiffNotSignificant;
                }
            }
            catch
            {
                // We don't want to stop the screen capture session at this point because there may be other components that
                // can write to their given paths. If this is a misconfigured path for a particular component then just log an error.
                _log.WriteErrorMessage($"Cannot write to \"{screenshot.FilePath}\" because the user may not have the appropriate permissions to access the path");

                return returnFlag | (int)ScreenSavingErrorLevels.UserNotEnoughPermissions;
            }
        }

        private void SaveToFile(Screenshot screenshot, Security security, int jpegQuality)
        {
            try
            {
                if (screenshot.Bitmap != null && screenshot.Format != null && !string.IsNullOrEmpty(screenshot.FilePath))
                {
                    if (screenshot.Format.Name.Equals("JPEG"))
                    {
                        var encoderParams = new EncoderParameters(1);
                        encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, jpegQuality);

                        var encoderInfo = GetEncoderInfo("image/jpeg");

                        screenshot.Bitmap.Save(screenshot.FilePath, encoderInfo, encoderParams);
                    }
                    else
                    {
                        screenshot.Bitmap.Save(screenshot.FilePath, screenshot.Format.Format);
                    }

                    if (screenshot.Encrypt)
                    {
                        string key = security.EncryptFile(screenshot.FilePath, screenshot.FilePath + "-encrypted");

                        if (!string.IsNullOrEmpty(key))
                        {
                            if (_fileSystem.FileExists(screenshot.FilePath))
                            {
                                if (_fileSystem.DeleteFile(screenshot.FilePath))
                                {
                                    _fileSystem.MoveFile(screenshot.FilePath + "-encrypted", screenshot.FilePath);

                                    screenshot.Key = key;
                                    screenshot.Encrypt = false;
                                    screenshot.Encrypted = true;
                                }
                            }
                        }
                    }

                    screenshot.Bitmap.Dispose();
                    screenshot.Bitmap = null;

                    screenshot.SavedToDisk = true;

                    _log.WriteMessage("Screenshot (id = " + screenshot.Id + ", viewid = " + screenshot.ViewId + ", encrypted = " + screenshot.Encrypted.ToString() + ") saved to \"" + screenshot.FilePath + "\"");
                }
            }
            catch
            {
                // We want to write to the error file instead of writing an exception just in case the user
                // has ExitOnError set and the exception causes the application to exit.
                _log.WriteErrorMessage("There was an error encountered when saving the screenshot image");
            }
        }

        /// <summary>
        /// Gets the resolution of the display device associated with the screen.
        /// </summary>
        /// <param name="screen">The Windows Forms screen object associated with the display device.</param>
        /// <returns>A struct having the screen, display device width, and display device height.</returns>
        public DeviceOptions GetDevice(System.Windows.Forms.Screen screen)
        {
            _dm.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));

            EnumDisplaySettings(screen.DeviceName, ENUM_CURRENT_SETTINGS, ref _dm);

            _device.screen = screen;
            _device.width = _dm.dmPelsWidth;
            _device.height = _dm.dmPelsHeight;

            return _device;
        }

        /// <summary>
        /// Gets the screenshot image from an image file. This is used when we want to show screenshot images.
        /// </summary>
        /// <param name="path">The path of an image file (such as a JPEG or PNG file).</param>
        /// <returns>The image of the file.</returns>
        public Image GetImageByPath(string path)
        {
            try
            {
                Image image = null;

                if (!string.IsNullOrEmpty(path) && _fileSystem.FileExists(path))
                {
                    image = _fileSystem.GetImage(path);
                }

                CaptureError = false;

                return image;
            }
            catch (ArgumentException)
            {
                // This is likely an image associated with an encrypted screenshot
                // which was determined as being "normal" but it's actually encrypted
                // because the screenshot references weren't saved properly.
                return null;
            }
            catch (Exception ex)
            {
                _log.WriteExceptionMessage("ScreenCapture::GetImageByPath", ex);

                CaptureError = true;

                return null;
            }
        }

        /// <summary>
        /// Gets the bitmap image of the screen based on X, Y, Width, and Height. This is used by Screens and Regions.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="component">The component of the source.</param>
        /// <param name="captureMethod">The screen capture method to use.</param>
        /// <param name="x">The X value of the bitmap.</param>
        /// <param name="y">The Y value of the bitmap.</param>
        /// <param name="width">The Width value of the bitmap.</param>
        /// <param name="height">The Height value of the bitmap.</param>
        /// <param name="resolutionRatio">The resolution ratio to apply to the bitmap.</param>
        /// <param name="mouse">Determines if the mouse pointer should be included in the bitmap.</param>
        /// <returns>A bitmap image representing what we captured.</returns>
        public Bitmap GetScreenBitmap(int source, int component, int captureMethod, int x, int y, int width, int height, int resolutionRatio, bool mouse)
        {
            try
            {
                CaptureError = false;

                Bitmap bmpSource = null;
                Bitmap bmpDestination = null;

                _log.WriteDebugMessage($"Attempting to capture screen image using source={source}, component={component}, captureMethod={captureMethod}, x={x}, y={y}, width={width}, height={height}, resolutionRatio={resolutionRatio}, mouse={mouse}");

                if (width > 0 && height > 0)
                {
                    if (resolutionRatio < IMAGE_RESOLUTION_RATIO_MIN || resolutionRatio > IMAGE_RESOLUTION_RATIO_MAX)
                    {
                        resolutionRatio = 100;
                    }

                    float imageResolutionRatio = (float)resolutionRatio / 100;

                    int destinationWidth = (int)(width * imageResolutionRatio);
                    int destinationHeight = (int)(height * imageResolutionRatio);

                    _log.WriteDebugMessage($"destinationWidth={destinationWidth}, destinationHeight={destinationHeight}");

                    if (source > 0 && component > -1)
                    {
                        try
                        {
                            // Test if we can acquire the actual screen from Windows and if we can't just let this
                            // method catch the out of bounds exception error.
                            System.Windows.Forms.Screen screen = System.Windows.Forms.Screen.AllScreens[component];
                        }
                        catch
                        {
                            return null;
                        }
                    }

                    if (captureMethod == 0) // System.Drawing.Graphics.CopyFromScreen (.NET)
                    {
                        _log.WriteDebugMessage("Using captureMethod 0 ... System.Drawing.Graphics.CopyFromScreen (.NET)");

                        Size blockRegionSize = new Size(width, height);

                        bmpSource = new Bitmap(width, height);
                        bmpDestination = new Bitmap(destinationWidth, destinationHeight);

                        using (Graphics graphicsSource = Graphics.FromImage(bmpSource))
                        {
                            graphicsSource.CopyFromScreen(x, y, 0, 0, blockRegionSize, CopyPixelOperation.SourceCopy);

                            Graphics graphicsDestination = Graphics.FromImage(bmpDestination);
                            graphicsDestination.DrawImage(bmpSource, 0, 0, destinationWidth, destinationHeight);

                            // The mouse pointer gets really weird if we go under 100 resolution ratio so we'll keep the resolution ratio at 100 if the mouse option is enabled.
                            if (mouse && resolutionRatio == 100)
                            {
                                CURSORINFO pci;
                                pci.cbSize = Marshal.SizeOf(typeof(CURSORINFO));

                                if (GetCursorInfo(out pci))
                                {
                                    if (pci.flags == CURSOR_SHOWING)
                                    {
                                        var hdc = graphicsDestination.GetHdc();
                                        DrawIconEx(hdc, pci.ptScreenPos.x - x, pci.ptScreenPos.y - y, pci.hCursor, 0, 0, 0, IntPtr.Zero, DI_NORMAL);
                                        graphicsDestination.ReleaseHdc();
                                    }
                                }
                            }
                        }

                        bmpSource.Dispose();
                    }
                    else if (captureMethod == 1) // BitBlt (gdi32.dll)
                    {
                        _log.WriteDebugMessage("Using captureMethod 1 ... BitBlt (gdi32.dll)");

                        IntPtr handle = GetDesktopWindow();
                        GetWindowRect(handle, out Rectangle rect);

                        IntPtr hdcSrc = GetWindowDC(handle);
                        IntPtr hdcDest = CreateCompatibleDC(hdcSrc);
                        IntPtr hBitmap = CreateCompatibleBitmap(hdcSrc, width, height);
                        IntPtr hOld = SelectObject(hdcDest, hBitmap);

                        BitBlt(hdcDest, 0, 0, width, height, hdcSrc, x, y, CopyPixelOperation.SourceCopy | CopyPixelOperation.CaptureBlt);

                        if (mouse)
                        {
                            CURSORINFO pci;
                            pci.cbSize = Marshal.SizeOf(typeof(CURSORINFO));

                            if (GetCursorInfo(out pci))
                            {
                                if (pci.flags == CURSOR_SHOWING)
                                {
                                    DrawIconEx(hdcDest, pci.ptScreenPos.x - x, pci.ptScreenPos.y - y, pci.hCursor, 0, 0, 0, IntPtr.Zero, DI_NORMAL);
                                }
                            }
                        }

                        SelectObject(hdcDest, hOld);
                        DeleteDC(hdcDest);
                        ReleaseDC(handle, hdcSrc);

                        bmpDestination = Image.FromHbitmap(hBitmap);

                        DeleteObject(hBitmap);
                    }

                    return bmpDestination;
                }

                return null;
            }
            catch (Exception)
            {
                // Just return nothing if there's an exception.
                return null;
            }
        }

        /// <summary>
        /// Gets the bitmap image of the active window.
        /// </summary>
        /// <param name="resolutionRatio">The resolution ratio.</param>
        /// <param name="mouse">Determines if we should capture the mouse pointer.</param>
        /// <returns>A bitmap image representing the active window.</returns>
        public Bitmap GetActiveWindowBitmap(int resolutionRatio, bool mouse)
        {
            try
            {
                GetWindowRect(GetForegroundWindow(), out Rectangle rect);

                int width = rect.Width - rect.X;
                int height = rect.Height - rect.Y;

                if (width > 0 && height > 0)
                {
                    // We don't need to log this message if the resolution ratio is 0 since if it is 0 then it's most likely a test of the screen capture method.
                    if (resolutionRatio > 0)
                    {
                        _log.WriteDebugMessage($"Attempting to capture active window image using width={width}, height={height}, resolutionRatio={resolutionRatio}, mouse={mouse}");
                    }

                    if (resolutionRatio < IMAGE_RESOLUTION_RATIO_MIN || resolutionRatio > IMAGE_RESOLUTION_RATIO_MAX)
                    {
                        resolutionRatio = 100;
                    }

                    float imageResolutionRatio = (float)resolutionRatio / 100;

                    int destinationWidth = (int)(width * imageResolutionRatio);
                    int destinationHeight = (int)(height * imageResolutionRatio);

                    Bitmap bmpSource = new Bitmap(width, height);
                    Bitmap bmpDestination = new Bitmap(destinationWidth, destinationHeight);

                    using (Graphics graphicsSource = Graphics.FromImage(bmpSource))
                    {
                        graphicsSource.CopyFromScreen(new Point(rect.X, rect.Y), new Point(0, 0), new Size(width, height), CopyPixelOperation.SourceCopy);

                        Graphics graphicsDestination = Graphics.FromImage(bmpDestination);
                        graphicsDestination.DrawImage(bmpSource, 0, 0, destinationWidth, destinationHeight);

                        // The mouse pointer gets really weird if we go under 100 resolution ratio so we'll keep the resolution ratio at 100 if the mouse option is enabled.
                        if (mouse && resolutionRatio == 100)
                        {
                            CURSORINFO pci;
                            pci.cbSize = Marshal.SizeOf(typeof(CURSORINFO));

                            if (GetCursorInfo(out pci))
                            {
                                if (pci.flags == CURSOR_SHOWING)
                                {
                                    var hdc = graphicsDestination.GetHdc();
                                    DrawIconEx(hdc, pci.ptScreenPos.x - rect.X, pci.ptScreenPos.y - rect.Y, pci.hCursor, 0, 0, 0, IntPtr.Zero, DI_NORMAL);
                                    graphicsDestination.ReleaseHdc();
                                }
                            }
                        }
                    }

                    bmpSource.Dispose();

                    CaptureError = false;

                    return bmpDestination;
                }

                _log.WriteDebugMessage("There were no values provided for width and height so an active window image could not be captured");

                return null;
            }
            catch (Exception)
            {
                // Just return nothing if there's an exception.
                return null;
            }
        }

        /// <summary>
        /// Gets the title of the active window.
        /// </summary>
        /// <returns>The title of the active window.</returns>
        public string GetActiveWindowTitle()
        {
            try
            {
                IntPtr handle;
                int chars = MAX_CHARS;

                StringBuilder buffer = new StringBuilder(chars);

                handle = GetForegroundWindow();

                if (GetWindowText(handle, buffer, chars) > 0)
                {
                    CaptureError = false;

                    // Make sure to strip out the backslash if it's in the window title.
                    return buffer.ToString().Replace(@"\", string.Empty);
                }

                CaptureError = false;

                return "(system)";
            }
            catch (Exception ex)
            {
                _log.WriteExceptionMessage("ScreenCapture::GetActiveWindowTitle", ex);

                CaptureError = true;

                return null;
            }
        }

        /// <summary>
        /// Gets the process name of the application associated with the active window.
        /// </summary>
        /// <returns>The process name of the application associated with the active window.</returns>
        public string GetActiveWindowProcessName()
        {
            try
            {
                IntPtr hwnd = GetForegroundWindow();
                GetWindowThreadProcessId(hwnd, out uint pid);
                Process p = Process.GetProcessById((int)pid);
                return p.ProcessName;
            }
            catch (Exception ex)
            {
                _log.WriteExceptionMessage("ScreenCapture::GetActiveWindowProcessName", ex);

                return null;
            }
        }

        /// <summary>
        /// Gets the bitmap images for the avaialble screens.
        /// </summary>
        /// <param name="source">The source index.</param>
        /// <param name="component">The component index.</param>
        /// <param name="captureMethod">The screen capture method to use.</param>
        /// <param name="autoAdapt">Determines if this is an AutoAdapt screen.</param>
        /// <param name="x">The X value of the bitmap.</param>
        /// <param name="y">The Y value of the bitmap.</param>
        /// <param name="width">The Width value of the bitmap.</param>
        /// <param name="height">The Height value of the bitmap.</param>
        /// <param name="resolutionRatio">The resolution ratio of the bitmap. A lower value makes the bitmap more blurry.</param>
        /// <param name="mouse">Determines if we include the mouse pointer in the captured bitmap.</param>
        /// <param name="bitmap">The bitmap to operate on.</param>
        /// <returns>A boolean to indicate if we were successful in getting a bitmap.</returns>
        public bool GetScreenImages(int source, int component, int captureMethod, bool autoAdapt, int x, int y, int width, int height, int resolutionRatio, bool mouse, out Bitmap bitmap)
        {
            try
            {
                CaptureError = false;

                bitmap = source == 0 && component == 0 && !autoAdapt
                    ? GetActiveWindowBitmap(resolutionRatio, mouse)
                    : GetScreenBitmap(source, component, captureMethod, x, y, width, height, resolutionRatio, mouse);

                if (bitmap != null)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _log.WriteExceptionMessage("ScreenCapture::GetScreenImages", ex);

                bitmap = null;

                CaptureError = true;

                return false;
            }
        }

        /// <summary>
        /// Saves the captured bitmap image as a screenshot to an image file.
        /// </summary>
        /// <param name="security">The security class.</param>
        /// <param name="jpegQuality">The JPEG quality setting for JPEG images being saved.</param>
        /// <param name="screenshot">The screenshot to save.</param>
        /// <param name="screenshotCollection">A collection of screenshot objects.</param>
        /// <returns>A boolean to determine if we successfully saved the screenshot.</returns>
        public int SaveScreenshot(Security security, int jpegQuality, Screenshot screenshot, ScreenshotCollection screenshotCollection)
        {
            int returnFlag = 0;

            try
            {
                int filepathLengthLimit = Convert.ToInt32(_config.Settings.Application.GetByKey("FilepathLengthLimit").Value);

                if (!string.IsNullOrEmpty(screenshot.FilePath))
                {
                    if (screenshot.FilePath.Length > filepathLengthLimit)
                    {
                        _log.WriteMessage($"File path length exceeds the configured length of {filepathLengthLimit} characters so value was truncated. Correct the value for the FilepathLengthLimit application setting to prevent truncation");
                        screenshot.FilePath = screenshot.FilePath.Substring(0, filepathLengthLimit);
                    }

                    // This is a normal path used in Windows (such as "C:\screenshots\").
                    if (!screenshot.FilePath.StartsWith(_fileSystem.PathDelimiter))
                    {
                        if (_fileSystem.DriveReady(screenshot.FilePath))
                        {
                            // The low disk mode we want to use (either check by percentage (0) or check the number of available bytes (1)).
                            int lowDiskMode = Convert.ToInt32(_config.Settings.Application.GetByKey("LowDiskMode").Value);

                            // Low disk space threshold by percentage.
                            int lowDiskPercentageThreshold = Convert.ToInt32(_config.Settings.Application.GetByKey("LowDiskPercentageThreshold").Value);
                            double freeDiskSpacePercentage = _fileSystem.FreeDiskSpacePercentage(screenshot.FilePath);

                            // Low disk space threshold in bytes.
                            long lowDiskBytesThreshold = Convert.ToInt64(_config.Settings.Application.GetByKey("LowDiskBytesThreshold").Value);
                            long freeDiskSpaceBytes = _fileSystem.FreeDiskSpace(screenshot.FilePath);

                            _log.WriteDebugMessage("Percentage of free disk space on drive for \"" + screenshot.FilePath + "\" is " + (int)freeDiskSpacePercentage + "% and low disk percentage threshold is set to " + lowDiskPercentageThreshold + "%");
                            _log.WriteDebugMessage("Bytes of free disk space on drive for \"" + screenshot.FilePath + "\" is " + freeDiskSpaceBytes + " and low disk bytes threshold is set to " + lowDiskBytesThreshold);

                            // Check low disk space by percentage.
                            if (lowDiskMode == 0)
                            {
                                if (freeDiskSpacePercentage < lowDiskPercentageThreshold)
                                {
                                    return NotEnoughDiskSpace(screenshot, returnFlag, freeDiskSpacePercentage, lowDiskPercentageThreshold, freeDiskSpaceBytes, lowDiskBytesThreshold);
                                }
                            }

                            // Check low disk space in bytes.
                            if (lowDiskMode == 1)
                            {
                                if (freeDiskSpaceBytes < lowDiskBytesThreshold)
                                {
                                    return NotEnoughDiskSpace(screenshot, returnFlag, freeDiskSpacePercentage, lowDiskPercentageThreshold, freeDiskSpaceBytes, lowDiskBytesThreshold);
                                }
                            }

                            return SaveToFile(security, jpegQuality, screenshot, screenshotCollection);
                        }
                        else
                        {
                            // Drive isn't ready so log an error message.
                            _log.WriteErrorMessage($"Unable to save screenshot for \"{screenshot.FilePath}\" because the drive is not found or not ready");

                            return returnFlag | (int)ScreenSavingErrorLevels.DriveNotReady;
                        }
                    }
                    else
                    {
                        // This is a UNC network share path (such as "\\SERVER\screenshots\").
                        return SaveToFile(security, jpegQuality, screenshot, screenshotCollection);
                    }
                }

                return returnFlag & (int)ScreenSavingErrorLevels.None;
            }
            catch (PathTooLongException ex)
            {
                _log.WriteErrorMessage($"The path is too long. I see the path is \"{screenshot.FilePath}\" but the length exceeds what Windows can handle so the file could not be saved. There is probably an exception error from Windows explaining why");
                _log.WriteExceptionMessage("ScreenCapture::SaveScreenshot", ex);

                // This shouldn't be an error that should stop a screen capture session.
                return returnFlag | (int)ScreenSavingErrorLevels.PathLengthExceeded;
            }
            catch (Exception ex)
            {
                _log.WriteExceptionMessage("ScreenCapture::SaveScreenshot", ex);

                return returnFlag | (int)ScreenSavingErrorLevels.ExceptionCaught;
            }
        }

        /// <summary>
        /// Sets the application focus to the defined application using a given process name.
        /// </summary>
        /// <param name="applicationFocus">The name of the process of the application to focus.</param>
        public void SetApplicationFocus(string applicationFocus)
        {
            if (string.IsNullOrEmpty(applicationFocus))
            {
                return;
            }

            Process[] process = Process.GetProcessesByName(applicationFocus);

            foreach (var item in process)
            {
                var proc = Process.GetProcessById(item.Id);

                IntPtr handle = proc.MainWindowHandle;
                SetForegroundWindow(handle);

                var placement = GetPlacement(proc.MainWindowHandle);

                if (placement.showCmd == ShowWindowCommands.Minimized)
                {
                    ShowWindowAsync(proc.MainWindowHandle, (int)ShowWindowCommands.Normal);
                }
            }
        }
    }
}