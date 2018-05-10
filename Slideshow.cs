//-----------------------------------------------------------------------
// <copyright file="Slideshow.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    using System;
    using System.Windows.Forms;

    public static class Slideshow
    {
        private static Timer slideshowTimer = new Timer();

        public static int Index { get; set; }
        public static int Count { get; set; }
        public static string SelectedSlide { get; set; }
        public static int SelectedScreen { get; set; }
        public static bool SlideSkipCheck { get; set; }
        public static int SlideSkip { get; set; }

        public static event EventHandler OnPlaying = delegate { };

        public static void Initialize()
        {
            slideshowTimer.Tick += new EventHandler(Tick_slideshowTimer);
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

            if (!slideshowTimer.Enabled)
            {
                slideshowTimer.Enabled = true;
            }
        }

        public static void Stop()
        {
            Log.Write("Stopping slideshow.");

            if (slideshowTimer.Enabled)
            {
                slideshowTimer.Enabled = false;
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

        private static void Tick_slideshowTimer(object sender, EventArgs e)
        {
            if (Index == (Count - 1) && slideshowTimer.Enabled)
            {
                slideshowTimer.Enabled = false;
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
                if (slideshowTimer != null)
                {
                    return slideshowTimer.Enabled;
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
                if (slideshowTimer != null)
                {
                    return slideshowTimer.Interval;
                }
                else
                {
                    return 1000;
                }
            }

            set { slideshowTimer.Interval = value; }
        }
    }
}