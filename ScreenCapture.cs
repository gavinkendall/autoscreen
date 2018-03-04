//////////////////////////////////////////////////////////
// Auto Screen Capture 2.0.8
// autoscreen.ScreenCapture.cs
//
// Written by Gavin Kendall (gavinkendall@gmail.com)
// Thursday, 15 May 2008 - Friday, 5 January 2018

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace autoscreen
{
    public static class ScreenCapture
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern void GetWindowRect(IntPtr hWnd, out Rectangle rect);

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        public const int SCREEN_MAX = 4;
        private const int MAX_CHARS = 48000;
        public const int CAPTURE_LIMIT_MIN = 1;
        public const int CAPTURE_LIMIT_MAX = 9999;
        private const int IMAGE_RESOLUTION_RATIO_MIN = 1;
        private const int IMAGE_RESOLUTION_RATIO_MAX = 100;

        public static int X { get; set; }
        public static int Y { get; set; }
        public static int Width { get; set; }
        public static int Height { get; set; }

        public static bool runningFromCommandLine = false;
        public static bool lockScreenCaptureSession = false;

        public static Bitmap GetScreenBitmap(Screen screen, int ratio, string format)
        {
            try
            {
                int sourceWidth = Width <= screen.Bounds.Width && Width > 0 ? Width : screen.Bounds.Width;
                int sourceHeight = Height <= screen.Bounds.Height && Height > 0 ? Height : screen.Bounds.Height;

                int destinationWidth = sourceWidth;
                int destinationHeight = sourceHeight;

                Size blockRegionSize = new Size(sourceWidth, sourceHeight);

                if (ratio < IMAGE_RESOLUTION_RATIO_MIN || ratio > IMAGE_RESOLUTION_RATIO_MAX) { ratio = 100; }

                float imageResolutionRatio = (float)ratio / 100;

                destinationWidth = (int)(sourceWidth * imageResolutionRatio);
                destinationHeight = (int)(sourceHeight * imageResolutionRatio);

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
            Rectangle rect = new Rectangle();
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
            int chars = MAX_CHARS;
            IntPtr handle = IntPtr.Zero;

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
                    Bitmap bitmap = screenNumber == 5 ? GetActiveWindowBitmap() : GetScreenBitmap(screen, m_ratio, format);

                    if (bitmap != null)
                    {
                        Screenshot screenshot = new Screenshot(dateTimeScreenshotTaken, path, screenNumber, format, screenshotType == ScreenshotType.User ? ScreenshotCollection.Count : -1);

                        SaveToFile(bitmap, jpegQualityLevel, format, screenshot.Path, screenshotType);

                        ScreenshotCollection.Add(screenshot, screenshotType);
                    }

                    GC.Collect();
                }
            }
            catch (Exception ex)
            {
                Log.Write("ScreenCapture::TakeScreenshot", ex);
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

        private static string m_folder;
        public static string Folder
        {
            set { m_folder = value; }
            get { return m_folder; }
        }

        private static string m_macro;
        public static string Macro
        {
            set { m_macro = value; }
            get { return m_macro; }
        }

        private static int m_ratio;
        public static int Ratio
        {
            set { m_ratio = value; }
            get { return m_ratio; }
        }

        private static string m_format;
        public static string Format
        {
            set { m_format = value; }
            get { return m_format; }
        }

        private static int m_delay;
        public static int Delay
        {
            set { m_delay = value; }
            get { return m_delay; }
        }

        private static int m_limit;
        public static int Limit
        {
            set { m_limit = value; }
            get { return m_limit; }
        }

        private static int m_count;
        public static int Count
        {
            set { m_count = value; }
            get { return m_count; }
        }
    }
}
