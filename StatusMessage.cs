//////////////////////////////////////////////////////////
// Auto Screen Capture 2.0.8
// autoscreen.StatusMessage.cs
//
// Written by Gavin Kendall (gavinkendall@gmail.com)
// Thursday, 15 May 2008 - Friday, 29 December 2017

namespace autoscreen
{
    internal class StatusMessage
    {
        internal const string ON = "On";
        internal const string OFF = "Off";
        internal const string MODE_PREVIEW = "Preview";
        internal const string MODE_NORMAL = "Normal";
        internal const string MODE_STATIC = "Static";
        internal const string RUNNING = "Running";
        internal const string STOPPED = "Stopped";
        internal const string LAST_CAPTURE_APP = "%CurrentDate% %CurrentTime% (%ImageFormat%)";
        internal const string LAST_CAPTURE_ICON = "Last capture: %CurrentTimeFriendly% (%ImageFormat%)";
    }
}
