//////////////////////////////////////////////////////////
// Auto Screen Capture 2.0.5.2
// autoscreen.StringHelper.cs
//
// Written by Gavin Kendall (gavinkendall@gmail.com)
// Thursday, 15 May 2008 - Wednesday, 1 November 2017

using System;
using System.Text;
using System.Collections.Generic;

namespace autoscreen
{
    public static class StringHelper
    {
        public static string DateFormat = "yyyy-MM-dd";
        public static string TimeFormat = "HH-mm-ss-fff";

        public static string ParseTags(string text)
        {
            return ParseTags(text, null);
        }

        public static string ParseTags(string text, string imageFormat)
        {
            if (!string.IsNullOrEmpty(imageFormat))
            {
                text = text.Replace("%ImageFormat%", imageFormat);
            }

            text = text.Replace("%CurrentDate%", DateTime.Now.ToString(DateFormat));
            text = text.Replace("%CurrentTime%", DateTime.Now.ToString(TimeFormat));
            text = text.Replace("%CurrentTimeFriendly%", DateTime.Now.ToLongTimeString());

            return text;
        }
    }
}
