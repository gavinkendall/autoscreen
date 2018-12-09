using System;

namespace AutoScreenCapture
{
    using System.IO;

    public static class Settings
    {
        public static SettingCollection Application;
        public static SettingCollection User;

        public static void Initialize()
        {
            Application = new SettingCollection();
            Application.Filepath = FileSystem.SettingsFolder + FileSystem.ApplicationSettingsFile;

            User = new SettingCollection();
            User.Filepath = FileSystem.SettingsFolder + FileSystem.UserSettingsFile;

            if (!Directory.Exists(FileSystem.SettingsFolder))
            {
                Directory.CreateDirectory(FileSystem.SettingsFolder);

                Application.Add(new Setting("Name", "Auto Screen Capture"));
                Application.Add(new Setting("Version", "2.1.7.6"));
                Application.Add(new Setting("DebugMode", true));
                Application.Save();

                User.Add(new Setting("ScreenshotsDirectory", FileSystem.ScreenshotsFolder));
                User.Add(new Setting("ScheduleImageFormat", "JPEG"));
                User.Add(new Setting("SlideSkip", 10));
                User.Add(new Setting("CaptureLimit", 0));
                User.Add(new Setting("ImageResolutionRatio", 100));
                User.Add(new Setting("ImageFormatFilter", "*.*"));
                User.Add(new Setting("ImageFormatFilterIndex", 0));
                User.Add(new Setting("Interval", 60000));
                User.Add(new Setting("SlideshowDelay", 1000));
                User.Add(new Setting("SlideSkipCheck", false));
                User.Add(new Setting("CaptureLimitCheck", false));
                User.Add(new Setting("TakeInitialScreenshotCheck", true));
                User.Add(new Setting("ShowSystemTrayIcon", true));
                User.Add(new Setting("StartWhenWindowsStartsCheck", true));
                User.Add(new Setting("CaptureStopAtCheck", false));
                User.Add(new Setting("CaptureStartAtCheck", false));
                User.Add(new Setting("CaptureOnSundayCheck", false));
                User.Add(new Setting("CaptureOnMondayCheck", false));
                User.Add(new Setting("CaptureOnTuesdayCheck", false));
                User.Add(new Setting("CaptureOnWednesdayCheck", false));
                User.Add(new Setting("CaptureOnThursdayCheck", false));
                User.Add(new Setting("CaptureOnFridayCheck", false));
                User.Add(new Setting("CaptureOnSaturdayCheck", false));
                User.Add(new Setting("CaptureOnTheseDaysCheck", false));
                User.Add(new Setting("CaptureStopAtValue",
                    new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 0, 0)));
                User.Add(new Setting("CaptureStartAtValue",
                    new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0)));
                User.Add(new Setting("Screen1X", 0));
                User.Add(new Setting("Screen1Y", 0));
                User.Add(new Setting("Screen1Width", 0));
                User.Add(new Setting("Screen1Height", 0));
                User.Add(new Setting("Screen2X", 0));
                User.Add(new Setting("Screen2Y", 0));
                User.Add(new Setting("Screen2Width", 0));
                User.Add(new Setting("Screen2Height", 0));
                User.Add(new Setting("Screen3X", 0));
                User.Add(new Setting("Screen3Y", 0));
                User.Add(new Setting("Screen3Width", 0));
                User.Add(new Setting("Screen3Height", 0));
                User.Add(new Setting("Screen4X", 0));
                User.Add(new Setting("Screen4Y", 0));
                User.Add(new Setting("Screen4Width", 0));
                User.Add(new Setting("Screen4Height", 0));
                User.Add(new Setting("Screen1Name", "Screen 1"));
                User.Add(new Setting("Screen2Name", "Screen 2"));
                User.Add(new Setting("Screen3Name", "Screen 3"));
                User.Add(new Setting("Screen4Name", "Screen 4"));
                User.Add(new Setting("Screen5Name", "Active Window"));
                User.Add(new Setting("LockScreenCaptureSession", false));
                User.Add(new Setting("Macro", MacroParser.UserMacro));
                User.Add(new Setting("JpegQualityLevel", 100));
                User.Add(new Setting("DaysOldWhenRemoveSlides", 10));
                User.Add(new Setting("CaptureScreen1", true));
                User.Add(new Setting("CaptureScreen2", true));
                User.Add(new Setting("CaptureScreen3", true));
                User.Add(new Setting("CaptureScreen4", true));
                User.Add(new Setting("CaptureActiveWindow", true));
                User.Add(new Setting("AutoReset", true));
                User.Add(new Setting("Mouse", true));
                User.Add(new Setting("StartButtonImageFormat", "JPEG"));
                User.Add(new Setting("Passphrase", string.Empty));
                User.Save();
            }

            Application.Load();
        }
    }
}
