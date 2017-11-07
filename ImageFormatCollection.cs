//////////////////////////////////////////////////////////
// Auto Screen Capture 2.0.7
// autoscreen.ImageFormatCollection.cs
//
// Written by Gavin Kendall (gavinkendall@gmail.com)
// Thursday, 15 May 2008 - Tuesday, 7 November 2017

using System.Collections;

namespace autoscreen
{
    public static class ImageFormatCollection
    {
        private static ArrayList m_imageFormatList = new ArrayList();

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
            m_imageFormatList.Add(imageFormat);
        }

        public static ImageFormat Get(int index)
        {
            return (ImageFormat)m_imageFormatList[index];
        }

        public static ImageFormat GetByName(string name)
        {
            for (int i = 0; i < m_imageFormatList.Count; i++)
            {
                ImageFormat imageFormat = Get(i);

                if (imageFormat.Name.Equals(name))
                {
                    return (ImageFormat)Get(i);
                }
            }

            return null;
        }

        public static int Count
        {
            get { return m_imageFormatList.Count; }
        }
    }
}
