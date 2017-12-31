//////////////////////////////////////////////////////////
// Auto Screen Capture 2.0.8
// autoscreen.Editor.cs
//
// Written by Gavin Kendall (gavinkendall@gmail.com)
// Thursday, 15 May 2008 - Sunday, 31 December 2017

namespace autoscreen
{
    public class Editor
    {
        public Editor()
        {

        }

        public Editor(string name, string application, string arguments)
        {
            m_name = name;
            m_arguments = arguments;
            m_application = application;
        }

        private string m_name;
        public string Name
        {
            set { m_name = value; }
            get { return m_name; }
        }

        private string m_application;
        public string Application
        {
            set { m_application = value; }
            get { return m_application; }
        }

        private string m_arguments;
        public string Arguments
        {
            set { m_arguments = value; }
            get { return m_arguments; }
        }
    }
}