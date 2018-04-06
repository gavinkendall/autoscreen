//////////////////////////////////////////////////////////
// Auto Screen Capture 2.1.2
// autoscreen.Screenshot.cs
//
// Developed by Gavin Kendall
// Thursday, 15 May 2008 - Friday, 6 April 2018

using System;
using System.IO;

namespace autoscreen
{
    public class Screenshot
    {
        public int Index { get; set; }
        public string Date { get; set; }
        public string Path { get; set; }
        public int Screen { get; set; }
        public string Format { get; set; }
        public string Filename { get; set; }
        public string Slidename { get; set; }

        public Screenshot()
        {
        }

        public Screenshot(DateTime dateTime, string path, int screen, string format, int index)
        {
            Index = index;
            Date = dateTime.ToString(MacroParser.DateFormat);
            Path = path;
            Screen = screen;
            Format = format;
            Filename = System.IO.Path.GetFileName(path);
            Slidename = dateTime.ToString(MacroParser.DateFormat) + " " + dateTime.ToString(MacroParser.TimeFormat) + " " + format;

            string directory = System.IO.Path.GetDirectoryName(path);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            Directory.SetCurrentDirectory(directory);
        }
    }
}