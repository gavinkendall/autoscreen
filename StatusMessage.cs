//////////////////////////////////////////////////////////
// Auto Screen Capture 2.0.5.1
// autoscreen.StatusMessage.cs
//
// Written by Gavin Kendall (gavinkendall@gmail.com)
// Thursday, 15 May 2008 - Wednesday, 1 November 2017

using System;
using System.Text;
using System.Collections.Generic;

namespace autoscreen
{
    internal class StatusMessage
    {
        internal const string ON = "On";
        internal const string OFF = "Off";
        internal const string RUNNING = "Running";
        internal const string STOPPED = "Stopped";
        internal const string LAST_CAPTURE_APP = "%CurrentDate% %CurrentTime% (%ImageFormat%)";
        internal const string LAST_CAPTURE_ICON = "Last capture: %CurrentTimeFriendly% (%ImageFormat%)";
    }
}
