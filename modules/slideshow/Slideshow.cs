//-----------------------------------------------------------------------
// <copyright file="Slideshow.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    public static class Slideshow
    {
        public static int Index { get; set; }
        public static int Count { get; set; }
        public static Slide SelectedSlide { get; set; }
    }
}