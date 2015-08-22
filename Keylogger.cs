//////////////////////////////////////////////////////////
// Auto Screen Capture 2.0.5
// autoscreen.Keylogger.cs
//
// Written by Gavin Kendall (gavinkendall@gmail.com)
// Thursday, 15 May 2008 - Wednesday, 27 November 2013

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace autoscreen
{
    public class Keylogger
    {
        public Keylogger()
        {
            m_clean = true;
            KeyboardListener.eventHandler += new EventHandler(KeyboardListener_eventHandler);
        }

        private bool m_enabled;
        public bool Enabled
        {
            get { return m_enabled; }
            set { m_enabled = value; }
        }

        private bool m_clean;
        public bool Clean
        {
            get { return m_clean; }
            set { m_clean = value; }
        }

        private string m_file;
        public string File
        {
            get { return m_file; }
            set { m_file = value; }
        }

        private string m_activeWindowTitle;
        public string ActiveWindowTitle
        {
            get { return m_activeWindowTitle; }
            set { m_activeWindowTitle = value; }
        }

        private string m_lastLoggedMinute;
        public string LastLoggedMinute
        {
            get { return m_lastLoggedMinute; }
            set { m_lastLoggedMinute = value; }
        }

        private void KeyboardListener_eventHandler(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(m_file) && m_enabled)
            {
                bool ignore = false;

                KeyboardListener.KeyboardEventArgs eventArgs = (KeyboardListener.KeyboardEventArgs)e;

                string key = string.Format(eventArgs.KeyData.ToString().ToLower());

                if (eventArgs.m_message == KeyboardListener.keyDown)
                {
                    if (key == "shiftkey") { KeyboardListener.keyShift = true; ignore = true; }
                    if (key == "lbutton, oemclear") { ignore = true; }
                    if (key == "escape") { key = "[esc]"; }
                    if (key == "apps") { key = "[menu]"; }
                    if (key == "up") { key = "[up]"; }
                    if (key == "down") { key = "[down]"; }
                    if (key == "left") { key = "[left]"; }
                    if (key == "right") { key = "[right]"; }
                    if (key == "home") { key = "[home]"; }
                    if (key == "prior" || key == "pageup") { key = "[pg up]"; }
                    if (key == "next" || key == "pagedn") { key = "[pg dn]"; }
                    if (key == "end") { key = "[end]"; }
                    if (key == "tab") { key = "[tab]"; }
                    if (key == "lwin") { key = "[win]"; }
                    if (key == "pause") { key = "[pause]"; }
                    if (key == "snapshot" || key == "printscreen") { key = "[prntscr]"; }
                    if (key == "space") key = " ";
                    if (key == "enter" || key == "return") { key = "[enter]"; }
                    if (key == "insert") { key = "[insert]"; }
                    if (key == "delete") { key = "[delete]"; }
                    if (key == "back") { key = "[backspace]"; }
                    if (key == "controlkey") { key = "[ctrl]"; }
                    if (key == "volumemute") { key = "[mute]"; }
                    if (key == "capital" || key == "caps") { key = "[caps]"; }
                    if (key == "f1") { key = "[f1]"; }
                    if (key == "f2") { key = "[f2]"; }
                    if (key == "f3") { key = "[f3]"; }
                    if (key == "f4") { key = "[f4]"; }
                    if (key == "f5") { key = "[f5]"; }
                    if (key == "f6") { key = "[f6]"; }
                    if (key == "f7") { key = "[f7]"; }
                    if (key == "f8") { key = "[f8]"; }
                    if (key == "f9") { key = "[f9]"; }
                    if (key == "f10") { key = "[f10]"; }
                    if (key == "f11") { key = "[f11]"; }
                    if (key == "f12") { key = "[f12]"; }

                    if (KeyboardListener.keyShift)
                    {
                        if (key == "d1") { key = "!"; }
                        if (key == "d2") { key = "@"; }
                        if (key == "d3") { key = "#"; }
                        if (key == "d4") { key = "$"; }
                        if (key == "d5") { key = "%"; }
                        if (key == "d6") { key = "^"; }
                        if (key == "d7") { key = "&"; }
                        if (key == "d8") { key = "*"; }
                        if (key == "d9") { key = "("; }
                        if (key == "d0") { key = ")"; }
                        if (key == "oemquotes" || key == "oem7") { key = "\""; }
                        if (key == "oemsemicolon" || key == "oem1") { key = ":"; }
                        if (key == "oemcomma") { key = "<"; }
                        if (key == "oemperiod") { key = ">"; }
                        if (key == "oemopenbrackets") { key = "{"; }
                        if (key == "oemclosebrackets" || key == "oem6") { key = "}"; }
                        if (key == "oemquestion") { key = "?"; }
                        if (key == "oempipe" || key == "oem5") { key = "|"; }
                        if (key == "oemtilde") { key = "~"; }
                        if (key == "oemminus") { key = "_"; }
                        if (key == "oemplus") { key = "+"; }
                    }
                    else
                    {
                        if (key == "d1") { key = "1"; }
                        if (key == "d2") { key = "2"; }
                        if (key == "d3") { key = "3"; }
                        if (key == "d4") { key = "4"; }
                        if (key == "d5") { key = "5"; }
                        if (key == "d6") { key = "6"; }
                        if (key == "d7") { key = "7"; }
                        if (key == "d8") { key = "8"; }
                        if (key == "d9") { key = "9"; }
                        if (key == "d0") { key = "0"; }
                        if (key == "oemquotes" || key == "oem7") { key = "'"; }
                        if (key == "oemsemicolon" || key == "oem1") { key = ";"; }
                        if (key == "oemcomma") { key = ","; }
                        if (key == "oemperiod") { key = "."; }
                        if (key == "oemopenbrackets") { key = "["; }
                        if (key == "oemclosebrackets" || key == "oem6") { key = "]"; }
                        if (key == "oemquestion") { key = "/"; }
                        if (key == "oempipe" || key == "oem5") { key = "\\"; }
                        if (key == "oemtilde") { key = "`"; }
                        if (key == "oemminus") { key = "-"; }
                        if (key == "oemplus") { key = "="; }
                    }

                    if (!ignore)
                    {
                        string activeWindowTitle = ScreenCapture.GetActiveWindowTitle();

                        if (!string.IsNullOrEmpty(activeWindowTitle))
                        {
                            using (StreamWriter sw = new StreamWriter(m_file, true))
                            {
                                if (KeyboardListener.keyShift) { key = key.ToUpper(); } else { key = key.ToLower(); }

                                if (activeWindowTitle != m_activeWindowTitle)
                                {
                                    if (!m_clean) { sw.Write(Environment.NewLine + Environment.NewLine); }

                                    m_activeWindowTitle = activeWindowTitle;
                                    sw.Write("===  " + m_activeWindowTitle + "  ===");

                                    m_lastLoggedMinute = DateTime.Now.ToString("HH-mm");
                                    sw.Write(Environment.NewLine + DateTime.Now.ToString(StringHelper.DateFormat + " " + StringHelper.TimeFormat) + " ");

                                    sw.Write(key);
                                }
                                else
                                {
                                    if (DateTime.Now.ToString("HH-mm") != m_lastLoggedMinute)
                                    {
                                        m_lastLoggedMinute = DateTime.Now.ToString("HH-mm");
                                        sw.Write(Environment.NewLine + DateTime.Now.ToString(StringHelper.DateFormat + " " + StringHelper.TimeFormat) + " ");
                                    }

                                    sw.Write(key);
                                }

                                sw.Flush();
                                sw.Close();

                                m_clean = false;
                            }
                        }
                    }
                }

                if (eventArgs.m_message == KeyboardListener.keyUp)
                {
                    if (key == "shiftkey") { KeyboardListener.keyShift = false; ignore = true; }
                }
            }
        }
    }
}
