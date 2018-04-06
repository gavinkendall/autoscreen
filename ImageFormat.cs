//////////////////////////////////////////////////////////
// Auto Screen Capture 2.1.2
// autoscreen.ImageFormat.cs
//
// Developed by Gavin Kendall
// Thursday, 15 May 2008 - Thursday, 5 April 2018

namespace autoscreen
{
    public class ImageFormat
    {
        public string Name { get; set; }
        public string Extension { get; set; }
        public System.Drawing.Imaging.ImageFormat Format { get; }

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