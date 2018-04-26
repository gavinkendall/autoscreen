//////////////////////////////////////////////////////////
// Auto Screen Capture 2.1.3
// autoscreen.Editor.cs
//
// Developed by Gavin Kendall
// Thursday, 15 May 2008 - Thursday, 26 April 2018

namespace autoscreen
{
    public class Editor
    {
        public string Name { get; set; }
        public string Application { get; set; }
        public string Arguments { get; set; }

        public Editor()
        {
        }

        public Editor(string name, string application, string arguments)
        {
            Name = name;
            Arguments = arguments;
            Application = application;
        }
    }
}