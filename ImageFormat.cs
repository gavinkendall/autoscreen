//////////////////////////////////////////////////////////
// Auto Screen Capture 2.0.5
// autoscreen.ImageFormat.cs
//
// Written by Gavin Kendall (gavinkendall@gmail.com)
// Thursday, 15 May 2008 - Wednesday, 27 November 2013

using System;
using System.Text;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace autoscreen
{
    public class ImageFormat
    {
        public ImageFormat(string name, string extension)
        {
            m_name = name;
            m_extension = extension;

            switch (name)
            {
                case ImageFormatSpec.NAME_BMP:
                    m_format = System.Drawing.Imaging.ImageFormat.Bmp;
                    break;

                case ImageFormatSpec.NAME_EMF:
                    m_format = System.Drawing.Imaging.ImageFormat.Emf;
                    break;

                case ImageFormatSpec.NAME_GIF:
                    m_format = System.Drawing.Imaging.ImageFormat.Gif;
                    break;

                case ImageFormatSpec.NAME_JPEG:
                    m_format = System.Drawing.Imaging.ImageFormat.Jpeg;
                    break;

                case ImageFormatSpec.NAME_PNG:
                    m_format = System.Drawing.Imaging.ImageFormat.Png;
                    break;

                case ImageFormatSpec.NAME_TIFF:
                    m_format = System.Drawing.Imaging.ImageFormat.Tiff;
                    break;

                case ImageFormatSpec.NAME_WMF:
                    m_format = System.Drawing.Imaging.ImageFormat.Wmf;
                    break;
            }
        }

        private string m_name;
        public string Name
        {
            set { m_name = value; }
            get { return m_name; }
        }

        private string m_extension;
        public string Extension
        {
            set { m_extension = value; }
            get { return m_extension; }
        }

        private System.Drawing.Imaging.ImageFormat m_format;
        public System.Drawing.Imaging.ImageFormat Format
        {
            get { return m_format; }
        }
    }
}
