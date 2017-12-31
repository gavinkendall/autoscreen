//////////////////////////////////////////////////////////
// Auto Screen Capture 2.0.8
// autoscreen.StringHelper.cs
//
// Written by Gavin Kendall (gavinkendall@gmail.com)
// Thursday, 15 May 2008 - Friday, 29 December 2017

using System;
using System.Text.RegularExpressions;

namespace autoscreen
{
    public static class MacroParser
    {
        public static readonly string DateFormat = "yyyy-MM-dd";
        public static readonly string DefaultMacro = "[" + AppDomain.CurrentDomain.BaseDirectory + @"screenshots]\%year%-%month%-%day%_%hour%-%minute%-%second%-%millisecond%_%screen%.%format%";

        private static readonly Regex rgxScreenshotsDirectory = new Regex(@"\[(?<ScreenshotsDirectory>.+)\]");

        public static string GetScreenshotsDirectory(string text)
        {
            return rgxScreenshotsDirectory.Match(text).Groups["ScreenshotsDirectory"].Value + "\\";
        }

        public static string ParseTags(string text)
        {
            return ParseTags(text, null);
        }

        public static string ParseTags(string text, string imageFormat)
        {
            if (!string.IsNullOrEmpty(imageFormat))
            {
                text = text.Replace("%format%", imageFormat);
            }

            text = text.Replace("%year%", DateTime.Now.Year.ToString("yyyy"));
            text = text.Replace("%month%", DateTime.Now.Month.ToString("MM"));
            text = text.Replace("%day%", DateTime.Now.Day.ToString("dd"));
            text = text.Replace("%hour%", DateTime.Now.Hour.ToString("HH"));
            text = text.Replace("%minute%", DateTime.Now.Minute.ToString("mm"));
            text = text.Replace("%second%", DateTime.Now.Second.ToString("ss"));
            text = text.Replace("%millisecond%", DateTime.Now.Millisecond.ToString("fff"));

            return text;
        }
    }
}
