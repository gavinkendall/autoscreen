/////////////////////////////////////////////////////////////////////////////////
// Auto Screen Capture 2.0.5
// AutoScreenCapture2.File.cs
//
// Wednesday, 2 July 2008 - Wednesday, 26 June 2013
// Copyright 2008-2013 Gavin Kendall (gavinkendall@gmail.com)

using System;
using System.Text;
using System.Drawing;
using System.Collections.Generic;

namespace AutoScreenCapture2
{
    public class File
    {
        public File(string path, string date, string time, int screen, string extension, Bitmap image)
        {
            m_path = path;
            m_date = date;
            m_time = time;
            m_screen = screen;
            m_extension = extension;
            m_image = image;

            m_name = date + " " + time + " " + extension.ToUpper();
        }

        private string m_path;
        public string Path
        {
            get { return m_path; }
        }

        private string m_name;
        public string Name
        {
            get { return m_name; }
        }

        private string m_date;
        public string Date
        {
            get { return m_date; }
        }

        private string m_time;
        public string Time
        {
            get { return m_time; }
        }

        private int m_screen;
        public int Screen
        {
            get { return m_screen; }
        }

        private string m_extension;
        public string Extension
        {
            get { return m_extension; }
        }

        private Bitmap m_image;
        public Bitmap Image
        {
            get { return m_image; }
        }
    }
}
