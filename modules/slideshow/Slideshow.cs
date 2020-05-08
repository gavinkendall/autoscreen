//-----------------------------------------------------------------------
// <copyright file="Slideshow.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>This class keeps track of what slide the user is currently on.</summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    /// <summary>
    /// A class to handle the indexing and count of slides.
    /// </summary>
    public static class Slideshow
    {
        /// <summary>
        /// The current slide index.
        /// </summary>
        public static int Index { get; set; }

        /// <summary>
        /// The number of slides.
        /// </summary>
        public static int Count { get; set; }

        /// <summary>
        /// The selected slide.
        /// </summary>
        public static Slide SelectedSlide { get; set; }
    }
}