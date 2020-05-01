//-----------------------------------------------------------------------
// <copyright file="Security.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace AutoScreenCapture
{
    /// <summary>
    /// A class for security-related methods.
    /// </summary>
    public static class Security
    {
        /// <summary>
        /// Accepts plaintext and returns a SHA-512 hash of it.
        /// </summary>
        /// <param name="text">Any text to hash.</param>
        /// <returns>SHA-512 hash of the given text.</returns>
        public static string Hash(string text)
        {
            var sha512 = new SHA512Managed();
            return Regex.Replace(BitConverter.ToString(sha512.ComputeHash(Encoding.Default.GetBytes(text))), "-", "").ToLower();
        }
    }
}
