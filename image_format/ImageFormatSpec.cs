//-----------------------------------------------------------------------
// <copyright file="ImageFormatSpec.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>This is where we specify the names and file extensions of the image formats that we support.</summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    /// <summary>
    /// The specification of ImageFormat. This is where we specify each ImageFormat object's name and extension.
    /// </summary>
    public static class ImageFormatSpec
    {
        /// <summary>
        /// Bitmap.
        /// </summary>
        public const string NAME_BMP = "BMP";

        /// <summary>
        /// Enhanced MetaFile.
        /// </summary>
        public const string NAME_EMF = "EMF";

        /// <summary>
        /// Graphics Interchange Format.
        /// </summary>
        public const string NAME_GIF = "GIF";

        /// <summary>
        /// Joint Photographic Experts Group.
        /// </summary>
        public const string NAME_JPEG = "JPEG";

        /// <summary>
        /// Portable Network Graphics.
        /// </summary>
        public const string NAME_PNG = "PNG";

        /// <summary>
        /// Tagged Image File Format.
        /// </summary>
        public const string NAME_TIFF = "TIFF";

        /// <summary>
        /// Windows Metafile.
        /// </summary>
        public const string NAME_WMF = "WMF";

        /// <summary>
        /// Bitmap.
        /// </summary>
        public const string EXTENSION_BMP = ".bmp";

        /// <summary>
        /// Enhanced MetaFile.
        /// </summary>
        public const string EXTENSION_EMF = ".emf";

        /// <summary>
        /// Graphics Interchange Format.
        /// </summary>
        public const string EXTENSION_GIF = ".gif";

        /// <summary>
        /// Joint Photographic Experts Group.
        /// </summary>
        public const string EXTENSION_JPEG = ".jpeg";

        /// <summary>
        /// Portable Network Graphics.
        /// </summary>
        public const string EXTENSION_PNG = ".png";

        /// <summary>
        /// Tagged Image File Format.
        /// </summary>
        public const string EXTENSION_TIFF = ".tiff";

        /// <summary>
        /// Windows Metafile.
        /// </summary>
        public const string EXTENSION_WMF = ".wmf";
    }
}