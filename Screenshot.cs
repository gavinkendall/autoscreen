//////////////////////////////////////////////////////////
// Auto Screen Capture 2.1.0
// autoscreen.Screenshot.cs
//
// Developed by Gavin Kendall
// Thursday, 15 May 2008 - Monday, 12 March 2018

using System;
using System.IO;

namespace autoscreen
{
    public class Screenshot
    {
        public Screenshot()
        {

        }

        public Screenshot(DateTime dateTime, string path, int screen, string format, int index)
        {
            m_index = index;
            m_date = dateTime.ToString(MacroParser.DateFormat);
            m_path = path;
            m_screen = screen;
            m_format = format;
            m_filename = System.IO.Path.GetFileName(m_path);
            m_slidename = dateTime.ToString(MacroParser.DateFormat) + " " + dateTime.ToString(MacroParser.TimeFormat) + " " + format;

            string directory = System.IO.Path.GetDirectoryName(m_path);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            Directory.SetCurrentDirectory(directory);
        }

        private int m_index;
        public int Index
        {
            set { m_index = value; }
            get { return m_index; }
        }

        private string m_date;
        public string Date
        {
            set { m_date = value; }
            get { return m_date; }
        }

        private string m_path;
        public string Path
        {
            set { m_path = value; }
            get { return m_path; }
        }

        private int m_screen;
        public int Screen
        {
            set { m_screen = value; }
            get { return m_screen; }
        }

        private string m_format;
        public string Format
        {
            set { m_format = value; }
            get { return m_format; }
        }

        private string m_filename;
        public string Filename
        {
            set { m_filename = value; }
            get { return m_filename; }
        }

        private string m_slidename;
        public string Slidename
        {
            set { m_slidename = value; }
            get { return m_slidename; }
        }
    }
}
