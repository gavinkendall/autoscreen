//////////////////////////////////////////////////////////
// Auto Screen Capture 2.0.5
// autoscreen.ScreenCapture.cs
//
// Written by Gavin Kendall (gavinkendall@gmail.com)
// Thursday, 15 May 2008 - Wednesday, 27 November 2013

using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
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

        private static Timer m_timer = new Timer();

        public static event EventHandler OnCapturing = delegate { };

        public const int SCREEN_MAX = 4;
        private const int MAX_CHARS = 48000;
        private const int CAPTURE_LIMIT_MIN = 1;
        private const int CAPTURE_LIMIT_MAX = 9999;
        private const int IMAGE_RESOLUTION_RATIO_MIN = 1;
        private const int IMAGE_RESOLUTION_RATIO_MAX = 100;

        public static void Initialize()
        {
            m_timer.Tick += new System.EventHandler(m_timer_Tick);
        }

        public static void Start()
        {
            m_timer.Interval = m_delay;
            m_timer.Enabled = true;
        }

        public static void Stop()
        {
            m_count = 0;
            m_timer.Enabled = false;
        }

        public static bool Capturing
        {
            get
            {
                if (m_timer != null)
                {
                    return m_timer.Enabled;
                }
                else
                {
                    return false;
                }
            }
        }

        public static Bitmap GetScreenBitmap(Screen screen, int ratio)
        {
            int sourceWidth = screen.Bounds.Width;
            int sourceHeight = screen.Bounds.Height;

            int destinationWidth = sourceWidth;
            int destinationHeight = sourceHeight;

            if (ratio < IMAGE_RESOLUTION_RATIO_MIN || ratio > IMAGE_RESOLUTION_RATIO_MAX) { ratio = 100; }

            float imageResolutionRatio = (float)ratio / 100;

            destinationWidth = (int)(sourceWidth * imageResolutionRatio);
            destinationHeight = (int)(sourceHeight * imageResolutionRatio);

            Bitmap bitmapSource = new Bitmap(sourceWidth, sourceHeight);
            Graphics graphicsSource = Graphics.FromImage(bitmapSource);
            graphicsSource.CopyFromScreen(screen.Bounds.X, screen.Bounds.Y, 0, 0, screen.Bounds.Size);

            Bitmap bitmapDestination = new Bitmap(destinationWidth, destinationHeight);
            Graphics graphicsDestination = Graphics.FromImage(bitmapDestination);
            graphicsDestination.DrawImage(bitmapSource, 0, 0, destinationWidth, destinationHeight);

            graphicsSource.Flush();
            graphicsDestination.Flush();

            return bitmapDestination;
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

        public static void TakeScreenshot()
        {
            try
            {
                int count = 0;

                string filename = m_folder + StringHelper.ParseTags("%CurrentDate%") + "\\%screen%\\" + StringHelper.ParseTags("%CurrentDate%_%CurrentTime%");

                foreach (Screen screen in Screen.AllScreens)
                {
                    Bitmap bitmap = GetScreenBitmap(screen, m_ratio);

                    count++;

                    if (count <= SCREEN_MAX)
                    {
                        SaveToFile(bitmap, m_format, filename.Replace("%screen%", count.ToString()) + ImageFormatCollection.GetByName(m_format).Extension);
                    }

                    System.GC.Collect();
                }

                SaveToFile(GetActiveWindowBitmap(), m_format, filename.Replace("%screen%", "5") + ImageFormatCollection.GetByName(m_format).Extension);

                m_count++;
            }
            catch (Exception)
            {

            }
        }

        private static void SaveToFile(Bitmap bitmap, string imageFormat, string imagePath)
        {
            try
            {
                if (bitmap != null && !string.IsNullOrEmpty(imageFormat) && !string.IsNullOrEmpty(imagePath))
                {
                    if (!Directory.Exists(Path.GetDirectoryName(imagePath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(imagePath));
                    }

                    bitmap.Save(imagePath, ImageFormatCollection.GetByName(imageFormat).Format);
                }
            }
            catch (Exception)
            {

            }
        }

        private static void m_timer_Tick(object sender, EventArgs e)
        {
            if (m_limit >= CAPTURE_LIMIT_MIN && m_limit <= CAPTURE_LIMIT_MAX)
            {
                if (m_count < m_limit)
                {
                    TakeScreenshot();
                }

                if (m_count == m_limit)
                {
                    Stop();
                }
            }
            else
            {
                TakeScreenshot();
            }

            OnCapturing(null, EventArgs.Empty);
        }

        private static string m_folder;
        public static string Folder
        {
            set { m_folder = value; }
            get { return m_folder; }
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
