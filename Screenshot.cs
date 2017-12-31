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

        public Screenshot(string date, string path)
        {

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
    }
}
