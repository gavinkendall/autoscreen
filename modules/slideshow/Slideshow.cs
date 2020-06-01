//-----------------------------------------------------------------------
// <copyright file="Slideshow.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>This class keeps track of what slide the user is currently on.</summary>
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program. If not, see <https://www.gnu.org/licenses/>.
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