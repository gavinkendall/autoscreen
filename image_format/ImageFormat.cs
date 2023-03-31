//-----------------------------------------------------------------------
// <copyright file="ImageFormat.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2023 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>The image format being used by each captured screenshot.</summary>
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
    /// An ImageFormat object and its associated properties.
    /// </summary>
    public class ImageFormat
    {
        /// <summary>
        /// The name of the ImageFormat object.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The file extension of the ImageFormat object.
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// The actual image format from Microsoft's System.Drawing.Imaging library.
        /// </summary>
        public System.Drawing.Imaging.ImageFormat Format { get; private set; }

        /// <summary>
        /// Constructs an ImageFormat object given the name and extension.
        /// </summary>
        /// <param name="name">The name of the ImageFormat object.</param>
        /// <param name="extension">The file extension of the ImageFormat object.</param>
        public ImageFormat(string name, string extension)
        {
            Name = name;
            Extension = extension;

            switch (name)
            {
                case "BMP":
                    Format = System.Drawing.Imaging.ImageFormat.Bmp;
                    break;

                case "EMF":
                    Format = System.Drawing.Imaging.ImageFormat.Emf;
                    break;

                case "GIF":
                    Format = System.Drawing.Imaging.ImageFormat.Gif;
                    break;

                case "JPEG":
                    Format = System.Drawing.Imaging.ImageFormat.Jpeg;
                    break;

                case "PNG":
                    Format = System.Drawing.Imaging.ImageFormat.Png;
                    break;

                case "TIFF":
                    Format = System.Drawing.Imaging.ImageFormat.Tiff;
                    break;

                case "WMF":
                    Format = System.Drawing.Imaging.ImageFormat.Wmf;
                    break;
            }
        }
    }
}