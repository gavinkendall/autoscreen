//-----------------------------------------------------------------------
// <copyright file="Slide.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A slide is used by the Screenshots module and it's a way to organize which set of screenshots belong to the same date and time.</summary>
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
