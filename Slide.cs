/////////////////////////////////////////////////////////////////////////////////
// Auto Screen Capture 2.0.5
// AutoScreenCapture2.Slide.cs
//
// Wednesday, 2 July 2008 - Wednesday, 26 June 2013
// Copyright 2008-2013 Gavin Kendall (gavinkendall@gmail.com)

using System;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;

namespace AutoScreenCapture2
{
    public class Slide
    {
        private string m_name;
        private ArrayList m_files;

        public Slide()
        {

        }

        public ArrayList Files
        {
            set { m_files = value; }
            get { return m_files; }
        }

        public string Name
        {
            set { m_name = value; }
            get { return m_name; }
        }
    }
}
