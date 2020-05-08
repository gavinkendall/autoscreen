//-----------------------------------------------------------------------
// <copyright file="Slide.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A slide is used by the Screenshots module and it's a way to organize which set of screenshots belong to the same date and time.</summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    /// <summary>
    /// A class representing a slide to be used for the list of screenshots in the Screenshots module.
    /// </summary>
    public class Slide
    {
        /// <summary>
        /// The name of the slide.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The date of the slide.
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// The value of the slide.
        /// </summary>
        public string Value { get; set; }
    }
}
