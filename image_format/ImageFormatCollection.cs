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
            ImageFormat bmp = new ImageFormat(ImageFormatSpec.NAME_BMP, ImageFormatSpec.EXTENSION_BMP);
            ImageFormat emf = new ImageFormat(ImageFormatSpec.NAME_EMF, ImageFormatSpec.EXTENSION_EMF);
            ImageFormat gif = new ImageFormat(ImageFormatSpec.NAME_GIF, ImageFormatSpec.EXTENSION_GIF);
            ImageFormat jpeg = new ImageFormat(ImageFormatSpec.NAME_JPEG, ImageFormatSpec.EXTENSION_JPEG);
            ImageFormat png = new ImageFormat(ImageFormatSpec.NAME_PNG, ImageFormatSpec.EXTENSION_PNG);
            ImageFormat tiff = new ImageFormat(ImageFormatSpec.NAME_TIFF, ImageFormatSpec.EXTENSION_TIFF);
            ImageFormat wmf = new ImageFormat(ImageFormatSpec.NAME_WMF, ImageFormatSpec.EXTENSION_WMF);

            Add(bmp);
            Add(emf);
            Add(gif);
            Add(jpeg);
            Add(png);
            Add(tiff);
            Add(wmf);
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

            Log.Write("Added " + imageFormat.Name + " (" + imageFormat.Extension + ")");
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