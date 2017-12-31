//////////////////////////////////////////////////////////
// Auto Screen Capture 2.0.8
// autoscreen.Editor.cs
//
// Written by Gavin Kendall (gavinkendall@gmail.com)
// Thursday, 15 May 2008 - Sunday, 31 December 2017

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
            m_path = path;
            m_screen = screen;
            m_format = format;

            m_filename = System.IO.Path.GetFileName(path);
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
    }
}
