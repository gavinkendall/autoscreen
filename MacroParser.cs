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
        public static readonly string DefaultMacro = "%year%-%month%-%day%_%hour%-%minute%-%second%-%millisecond%_%screen%.%format%";

        public static string ParseTags(string text)
        {
            return ParseTags(text, null);
        }

        public static string ParseTags(string text, string imageFormat)
        {
            if (!string.IsNullOrEmpty(imageFormat))
            {
                text = text.Replace("%format%", ImageFormatCollection.GetByName(imageFormat).Extension.TrimStart('.'));
            }

            text = text.Replace("%year%", DateTime.Now.ToString("yyyy"));
            text = text.Replace("%month%", DateTime.Now.ToString("MM"));
            text = text.Replace("%day%", DateTime.Now.ToString("dd"));
            text = text.Replace("%hour%", DateTime.Now.ToString("HH"));
            text = text.Replace("%minute%", DateTime.Now.ToString("mm"));
            text = text.Replace("%second%", DateTime.Now.ToString("ss"));
            text = text.Replace("%millisecond%", DateTime.Now.ToString("fff"));

            return text;
        }
    }
}
