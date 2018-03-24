//////////////////////////////////////////////////////////
// Auto Screen Capture 2.1.0
// autoscreen.Editor.cs
//
// Developed by Gavin Kendall
// Thursday, 15 May 2008 - Monday, 12 March 2018

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