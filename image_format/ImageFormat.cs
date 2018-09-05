//-----------------------------------------------------------------------
// <copyright file="ImageFormat.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    public class ImageFormat
    {
        public string Name { get; set; }
        public string Extension { get; set; }
        public System.Drawing.Imaging.ImageFormat Format { get; private set; }

        public ImageFormat(string name, string extension)
        {
            Name = name;
            Extension = extension;

            switch (name)
            {
                case ImageFormatSpec.NAME_BMP:
                    Format = System.Drawing.Imaging.ImageFormat.Bmp;
                    break;

                case ImageFormatSpec.NAME_EMF:
                    Format = System.Drawing.Imaging.ImageFormat.Emf;
                    break;

                case ImageFormatSpec.NAME_GIF:
                    Format = System.Drawing.Imaging.ImageFormat.Gif;
                    break;

                case ImageFormatSpec.NAME_JPEG:
                    Format = System.Drawing.Imaging.ImageFormat.Jpeg;
                    break;

                case ImageFormatSpec.NAME_PNG:
                    Format = System.Drawing.Imaging.ImageFormat.Png;
                    break;

                case ImageFormatSpec.NAME_TIFF:
                    Format = System.Drawing.Imaging.ImageFormat.Tiff;
                    break;

                case ImageFormatSpec.NAME_WMF:
                    Format = System.Drawing.Imaging.ImageFormat.Wmf;
                    break;
            }
        }
    }
}