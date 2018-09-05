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

    public static class ImageFormatCollection
    {
        private static ArrayList _imageFormatList = new ArrayList();

        public static void Initialize()
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

        public static void Add(ImageFormat imageFormat)
        {
            _imageFormatList.Add(imageFormat);

            Log.Write("Added " + imageFormat.Name + " (" + imageFormat.Extension + ")");
        }

        public static ImageFormat Get(int index)
        {
            return (ImageFormat)_imageFormatList[index];
        }

        public static ImageFormat GetByName(string name)
        {
            for (int i = 0; i < _imageFormatList.Count; i++)
            {
                ImageFormat imageFormat = Get(i);

                if (imageFormat.Name.Equals(name))
                {
                    return Get(i);
                }
            }

            return null;
        }

        public static int Count
        {
            get { return _imageFormatList.Count; }
        }
    }
}