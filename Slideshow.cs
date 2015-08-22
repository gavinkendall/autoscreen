//////////////////////////////////////////////////////////
// Auto Screen Capture 2.0.5
// autoscreen.Slideshow.cs
//
// Written by Gavin Kendall (gavinkendall@gmail.com)
// Thursday, 15 May 2008 - Wednesday, 27 November 2013

using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

namespace autoscreen
{
    public static class Slideshow
    {
        private static Timer m_timer = new Timer();

        private static int m_imageIndex;
        private static int m_imageIndexCount;

        private static int m_selectedScreen;
        private static string m_selectedSlide;

        private static int m_slideSkip;
        private static bool m_slideSkipCheck;

        public static event EventHandler OnPlaying = delegate { };

        public static void Initialize()
        {
            m_timer.Tick += new System.EventHandler(m_timer_Tick);
        }

        public static void Clear()
        {
            m_imageIndex = 0;
            m_imageIndexCount = 0;
        }

        public static void Play()
        {
            if (m_imageIndex == (m_imageIndexCount - 1))
            {
                m_imageIndex = 0;
            }

            if (!m_timer.Enabled)
            {
                m_timer.Enabled = true;
            }
        }

        public static void Stop()
        {
            if (m_timer.Enabled)
            {
                m_timer.Enabled = false;
            }
        }

        public static void Next()
        {
            if (m_imageIndex < (m_imageIndexCount - 1))
            {
                m_imageIndex++;
            }
        }

        public static void Previous()
        {
            if (m_imageIndex > 0)
            {
                m_imageIndex--;
            }
        }

        public static void First()
        {
            m_imageIndex = 0;
        }

        public static void Last()
        {
            m_imageIndex = (m_imageIndexCount - 1);
        }

        private static void m_timer_Tick(object sender, EventArgs e)
        {
            if (m_imageIndex == (m_imageIndexCount - 1))
            {
                if (m_timer.Enabled)
                {
                    m_timer.Enabled = false;
                }
            }

            if (m_imageIndex < (m_imageIndexCount - 1))
            {
                if (m_slideSkipCheck)
                {
                    m_imageIndex++;
                    m_imageIndex = m_imageIndex + m_slideSkip;
                }
                else
                {
                    m_imageIndex++;
                }
            }

            if (m_imageIndex > (m_imageIndexCount - 1))
            {
                m_imageIndex = (m_imageIndexCount - 1);
            }

            OnPlaying(null, EventArgs.Empty);
        }

        public static int Index
        {
            get { return m_imageIndex; }
            set { m_imageIndex = value; }
        }

        public static int Count
        {
            get { return m_imageIndexCount; }
            set { m_imageIndexCount = value; }
        }

        public static string SelectedSlide
        {
            get { return m_selectedSlide; }
            set { m_selectedSlide = value; }
        }

        public static int SelectedScreen
        {
            get { return m_selectedScreen; }
            set { m_selectedScreen = value; }
        }

        public static bool SlideSkipCheck
        {
            get { return m_slideSkipCheck; }
            set { m_slideSkipCheck = value; }
        }

        public static int SlideSkip
        {
            get { return m_slideSkip; }
            set { m_slideSkip = value; }
        }

        public static bool Playing
        {
            get
            {
                if (m_timer != null)
                {
                    return m_timer.Enabled;
                }
                else
                {
                    return false;
                }
            }
        }

        public static int Interval
        {
            get
            {
                if (m_timer != null)
                {
                    return m_timer.Interval;
                }
                else
                {
                    return 1000;
                }
            }

            set { m_timer.Interval = value; }
        }
    }
}
