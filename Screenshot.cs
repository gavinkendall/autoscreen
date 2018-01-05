//////////////////////////////////////////////////////////
// Auto Screen Capture 2.0.8
// autoscreen.Editor.cs
//
// Written by Gavin Kendall (gavinkendall@gmail.com)
// Thursday, 15 May 2008 - Friday, 5 January 2018

using System.IO;

namespace autoscreen
{
    public class Screenshot
    {
        public Screenshot()
        {

        }

        public Screenshot(string date, string path, int screen, string format)
        {
            m_date = date;
            m_path = MacroParser.RegexPath.Match(path).Groups["PathPrefix"].Value + MacroParser.RegexPath.Match(path).Groups["Slidename"].Value + MacroParser.RegexPath.Match(path).Groups["PathSuffix"].Value;
            m_screen = screen;
            m_format = format;

            m_filename = System.IO.Path.GetFileName(m_path);
            m_slidename = MacroParser.RegexPath.Match(path).Groups["Slidename"].Value + " " + m_format;

            string directory = System.IO.Path.GetDirectoryName(m_path);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            Directory.SetCurrentDirectory(directory);
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
