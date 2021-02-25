//-----------------------------------------------------------------------
// <copyright file="ImageFormatCollection.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2021 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>The collection of image formats.</summary>
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
using System.Collections;
using System.Collections.Generic;

namespace AutoScreenCapture
{
    /// <summary>
    /// A collection class to store and manage ImageFormat objects.
    /// </summary>
    public class ImageFormatCollection : IEnumerable<ImageFormat>
    {
        private readonly List<ImageFormat> _imageFormatList;

        /// <summary>
        /// Adds the default image formats to the ImageFormat collection.
        /// </summary>
        public ImageFormatCollection()
        {
            _imageFormatList = new List<ImageFormat>();

            Add(new ImageFormat("BMP", ".bmp"));
            Add(new ImageFormat("EMF", ".emf"));
            Add(new ImageFormat("GIF", ".gif"));
            Add(new ImageFormat("JPEG", ".jpeg"));
            Add(new ImageFormat("PNG", ".png"));
            Add(new ImageFormat("TIFF", ".tiff"));
            Add(new ImageFormat("WMF", ".wmf"));
        }

        /// <summary>
        /// Returns the enumerator for the collection.
        /// </summary>
        /// <returns>A list of ImageFormat objects.</returns>
        public List<ImageFormat>.Enumerator GetEnumerator()
        {
            return _imageFormatList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<ImageFormat>)_imageFormatList).GetEnumerator();
        }

        IEnumerator<ImageFormat> IEnumerable<ImageFormat>.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Adds an ImageFormat object to the collection.
        /// </summary>
        /// <param name="imageFormat">The ImageFormat object to add.</param>
        public void Add(ImageFormat imageFormat)
        {
            _imageFormatList.Add(imageFormat);
        }

        /// <summary>
        /// Gets an ImageFormat object based on its name.
        /// </summary>
        /// <param name="name">The name of an ImageFormat object.</param>
        /// <returns>An ImageFormat object.</returns>
        public ImageFormat GetByName(string name)
        {
            foreach (ImageFormat imageFormat in _imageFormatList)
            {
                if (imageFormat.Name.Equals(name))
                {
                    return imageFormat;
                }
            }

            return null;
        }
    }
}