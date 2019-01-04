using System;

namespace AutoScreenCapture
{
    using System.IO;

    public static class Settings
    {
        public static readonly string ApplicationName = "Auto Screen Capture";
        public static readonly string ApplicationVersion = "2.1.8.1";

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
            }

            if (Application != null)
            {
                if (File.Exists(Application.Filepath))
                {
                    Application.Load();

                    Application.GetByKey("Name", defaultValue: Settings.ApplicationName).Value = ApplicationName;
                    Application.GetByKey("Version", defaultValue: Settings.ApplicationVersion).Value = ApplicationVersion;

                    Application.Save();
                }
                else
                {
                    Application.Add(new Setting("Name", ApplicationName));
                    Application.Add(new Setting("Version", ApplicationVersion));
                    Application.Add(new Setting("DebugMode", false));

                    Application.Save();
                }
            }

            if (User != null)
            {
                if (File.Exists(User.Filepath))
                {
                    User.Load();
                }
                else
                {
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
                    User.Add(new Setting("TakeInitialScreenshotCheck", false));
                    User.Add(new Setting("ShowSystemTrayIcon", true));
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
                    User.Add(new Setting("CaptureStopAtValue", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 0, 0)));
                    User.Add(new Setting("CaptureStartAtValue", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0)));
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
                    User.Add(new Setting("Schedule", false));

                    User.Save();
                }
            }
        }
    }
}
