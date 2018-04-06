//////////////////////////////////////////////////////////
// Auto Screen Capture 2.1.2
// autoscreen.Slideshow.cs
//
// Developed by Gavin Kendall
// Thursday, 15 May 2008 - Thursday, 5 April 2018

using System;
using System.Windows.Forms;

namespace autoscreen
{
    public static class Slideshow
    {
        private static Timer timer = new Timer();

        public static int Index { get; set; }
        public static int Count { get; set; }
        public static string SelectedSlide { get; set; }
        public static int SelectedScreen { get; set; }
        public static bool SlideSkipCheck { get; set; }
        public static int SlideSkip { get; set; }

        public static event EventHandler OnPlaying = delegate { };

        public static void Initialize()
        {
            timer.Tick += new EventHandler(m_timer_Tick);
        }

        public static void Clear()
        {
            Index = 0;
            Count = 0;
        }

        public static void Play()
        {
            Log.Write("Playing slideshow.");

            if (Index == (Count - 1))
            {
                Index = 0;
            }

            if (!timer.Enabled)
            {
                timer.Enabled = true;
            }
        }

        public static void Stop()
        {
            Log.Write("Stopping slideshow.");

            if (timer.Enabled)
            {
                timer.Enabled = false;
            }
        }

        public static void Next()
        {
            if (Index < (Count - 1))
            {
                Index++;
            }
        }

        public static void Previous()
        {
            if (Index > 0)
            {
                Index--;
            }
        }

        public static void First()
        {
            Index = 0;
        }

        public static void Last()
        {
            Index = (Count - 1);
        }

        private static void m_timer_Tick(object sender, EventArgs e)
        {
            if (Index == (Count - 1) && timer.Enabled)
            {
                timer.Enabled = false;
            }

            if (Index < (Count - 1))
            {
                if (SlideSkipCheck)
                {
                    Index++;
                    Index = Index + SlideSkip;
                }
                else
                {
                    Index++;
                }
            }

            if (Index > (Count - 1))
            {
                Index = (Count - 1);
            }

            OnPlaying(null, EventArgs.Empty);
        }

        public static bool Playing
        {
            get
            {
                if (timer != null)
                {
                    return timer.Enabled;
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
                if (timer != null)
                {
                    return timer.Interval;
                }
                else
                {
                    return 1000;
                }
            }

            set { timer.Interval = value; }
        }
    }
}