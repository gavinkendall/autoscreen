//////////////////////////////////////////////////////////
// Auto Screen Capture 2.1.3
// autoscreen.ScreenCapture.cs
//
// Developed by Gavin Kendall
// Thursday, 15 May 2008 - Thursday, 26 April 2018

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace autoscreen
{
    public static class ScreenCapture
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern void GetWindowRect(IntPtr hWnd, out Rectangle rect);

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        public const int SCREEN_MAX = 4;
        public const int CAPTURE_LIMIT_MIN = 1;
        public const int CAPTURE_LIMIT_MAX = 9999;

        /// <summary>
        /// The default image format.
        /// </summary>
        public const string DefaultImageFormat = ImageFormatSpec.NAME_JPEG;

        private const int MAX_CHARS = 48000;
        private const int IMAGE_RESOLUTION_RATIO_MIN = 1;
        private const int IMAGE_RESOLUTION_RATIO_MAX = 100;

        public static int X { get; set; }
        public static int Y { get; set; }
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static string Folder { get; set; }
        public static string Macro { get; set; }
        public static int Ratio { get; set; }
        public static string Format { get; set; }
        public static int Delay { get; set; }
        public static int Limit { get; set; }
        public static int Count { get; set; }
        public static bool RunningFromCommandLine { get; set; }
        public static bool LockScreenCaptureSession { get; set; }

        public static Bitmap GetScreenBitmap(Screen screen, int ratio, string format)
        {
            try
            {
                int sourceWidth = Width <= screen.Bounds.Width && Width > 0 ? Width : screen.Bounds.Width;
                int sourceHeight = Height <= screen.Bounds.Height && Height > 0 ? Height : screen.Bounds.Height;

                Size blockRegionSize = new Size(sourceWidth, sourceHeight);

                if (ratio < IMAGE_RESOLUTION_RATIO_MIN || ratio > IMAGE_RESOLUTION_RATIO_MAX) { ratio = 100; }

                float imageResolutionRatio = (float)ratio / 100;

                int destinationWidth = (int)(sourceWidth * imageResolutionRatio);
                int destinationHeight = (int)(sourceHeight * imageResolutionRatio);

                Bitmap bitmapSource = new Bitmap(sourceWidth, sourceHeight);
                Graphics graphicsSource = Graphics.FromImage(bitmapSource);

                graphicsSource.CopyFromScreen(X, Y, 0, 0, blockRegionSize, CopyPixelOperation.SourceCopy);

                Bitmap bitmapDestination = new Bitmap(destinationWidth, destinationHeight);

                Graphics graphicsDestination = Graphics.FromImage(bitmapDestination);
                graphicsDestination.DrawImage(bitmapSource, 0, 0, destinationWidth, destinationHeight);

                graphicsSource.Flush();
                graphicsDestination.Flush();

                return bitmapDestination;
            }
            catch (Exception ex)
            {
                Log.Write("ScreenCapture::GetScreenBitmap", ex);
                return null;
            }
        }

        public static Bitmap GetActiveWindowBitmap()
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

                return bitmap;
            }
            else
            {
                return null;
            }
        }

        public static string GetActiveWindowTitle()
        {
            IntPtr handle;
            int chars = MAX_CHARS;

            StringBuilder buffer = new StringBuilder(chars);

            handle = GetForegroundWindow();

            if (GetWindowText(handle, buffer, chars) > 0)
            {
                return buffer.ToString();
            }

            return null;
        }

        public static void TakeScreenshot(Screen screen, DateTime dateTimeScreenshotTaken, string format, string screenName, string path, int screenNumber, ScreenshotType screenshotType, long jpegQualityLevel)
        {
            try
            {
                if (!string.IsNullOrEmpty(path))
                {
                    Bitmap bitmap = screenNumber == 5 ? GetActiveWindowBitmap() : GetScreenBitmap(screen, Ratio, format);

                    if (bitmap != null)
                    {
                        Screenshot screenshot = new Screenshot(dateTimeScreenshotTaken, path, screenNumber, format, screenshotType == ScreenshotType.User ? ScreenshotCollection.Count : -1);

                        SaveToFile(bitmap, jpegQualityLevel, format, screenshot.Path, screenshotType);

                        ScreenshotCollection.Add(screenshot, screenshotType);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("ScreenCapture::TakeScreenshot", ex);
            }
        }

        public static void Save(string imagePath)
        {
            try
            {
                if (!string.IsNullOrEmpty(imagePath))
                {
                    if (!Directory.Exists(Path.GetDirectoryName(imagePath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(imagePath));
                    }

                    File.Create(imagePath);
                }
            }
            catch (Exception ex)
            {
                Log.Write("ScreenCapture::Save", ex);
            }
        }

        private static void SaveToFile(Bitmap bitmap, long jpegQualityLevel, string imageFormat, string imagePath, ScreenshotType screenshotType)
        {
            try
            {
                if (bitmap != null && !string.IsNullOrEmpty(imageFormat) && !string.IsNullOrEmpty(imagePath))
                {
                    if (!Directory.Exists(Path.GetDirectoryName(imagePath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(imagePath));
                    }

                    if (screenshotType == ScreenshotType.User)
                    {
                        Log.Write("Screenshot saved: " + imagePath);
                    }

                    if (imageFormat.Equals(ImageFormatSpec.NAME_JPEG))
                    {
                        var encoderParams = new EncoderParameters(1);
                        encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, jpegQualityLevel);
                        var encoderInfo = GetEncoderInfo("image/jpeg");
                        bitmap.Save(imagePath, encoderInfo, encoderParams);
                    }
                    else
                    {
                        bitmap.Save(imagePath, ImageFormatCollection.GetByName(imageFormat).Format);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("ScreenCapture::SaveToFile", ex);
            }
        }

        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            var encoders = ImageCodecInfo.GetImageEncoders();
            return encoders.FirstOrDefault(t => t.MimeType == mimeType);
        }
    }
}