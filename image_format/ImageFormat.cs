//-----------------------------------------------------------------------
// <copyright file="ImageFormat.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
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