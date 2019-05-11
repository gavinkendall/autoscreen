//-----------------------------------------------------------------------
// <copyright file="ImageFormatCollection.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    using System.Collections;
    using System.Collections.Generic;

    public class ImageFormatCollection : IEnumerable<ImageFormat>
    {
        private readonly List<ImageFormat> _imageFormatList = new List<ImageFormat>();

        public ImageFormatCollection()
        {
            Add(new ImageFormat(ImageFormatSpec.NAME_BMP, ImageFormatSpec.EXTENSION_BMP));
            Add(new ImageFormat(ImageFormatSpec.NAME_EMF, ImageFormatSpec.EXTENSION_EMF));
            Add(new ImageFormat(ImageFormatSpec.NAME_GIF, ImageFormatSpec.EXTENSION_GIF));
            Add(new ImageFormat(ImageFormatSpec.NAME_JPEG, ImageFormatSpec.EXTENSION_JPEG));
            Add(new ImageFormat(ImageFormatSpec.NAME_PNG, ImageFormatSpec.EXTENSION_PNG));
            Add(new ImageFormat(ImageFormatSpec.NAME_TIFF, ImageFormatSpec.EXTENSION_TIFF));
            Add(new ImageFormat(ImageFormatSpec.NAME_WMF, ImageFormatSpec.EXTENSION_WMF));
        }

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

        public void Add(ImageFormat imageFormat)
        {
            _imageFormatList.Add(imageFormat);

            Log.Write("Image format added: " + imageFormat.Name + " (" + imageFormat.Extension + ")");
        }

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