//////////////////////////////////////////////////////////
// Auto Screen Capture 2.0.8
// autoscreen.StringHelper.cs
//
// Written by Gavin Kendall (gavinkendall@gmail.com)
// Thursday, 15 May 2008 - Sunday, 31 December 2017

using System;
using System.Text.RegularExpressions;

namespace autoscreen
{
    public static class MacroParser
    {
        public static readonly string DateFormat = "yyyy-MM-dd";
        public static readonly Regex RegexPath = new Regex(@"(?<PathPrefix>.*)\[(?<Slidename>.*)\](?<PathSuffix>.*)");
        public static readonly string DefaultMacro = "[%year%-%month%-%day%_%hour%-%minute%-%second%-%millisecond%]_%screen%.%format%";

        public static string ParseTags(string path, string imageFormat, string screenName, DateTime dateTimeScreenshotTaken)
        {
            //path = RegexPath.Match(path).Groups["PathPrefix"].Value + RegexPath.Match(path).Groups["Slidename"].Value + RegexPath.Match(path).Groups["PathSuffix"].Value;

            if (!string.IsNullOrEmpty(imageFormat))
            {
                path = path.Replace("%format%", ImageFormatCollection.GetByName(imageFormat).Extension.TrimStart('.'));
            }

            if (!string.IsNullOrEmpty(screenName))
            {
                path = path.Replace("%screen%", screenName);
            }

            path = path.Replace("%year%", dateTimeScreenshotTaken.ToString("yyyy"));
            path = path.Replace("%month%", dateTimeScreenshotTaken.ToString("MM"));
            path = path.Replace("%day%", dateTimeScreenshotTaken.ToString("dd"));
            path = path.Replace("%hour%", dateTimeScreenshotTaken.ToString("HH"));
            path = path.Replace("%minute%", dateTimeScreenshotTaken.ToString("mm"));
            path = path.Replace("%second%", dateTimeScreenshotTaken.ToString("ss"));
            path = path.Replace("%millisecond%", dateTimeScreenshotTaken.ToString("fff"));

            return path;
        }
    }
}
