//-----------------------------------------------------------------------
// <copyright file="Security.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
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
    /// 
    /// </summary>
    public static class Security
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Hash(string text)
        {
            var sha512 = new SHA512Managed();
            return Regex.Replace(BitConverter.ToString(sha512.ComputeHash(Encoding.Default.GetBytes(text))), "-", "").ToLower();
        }
    }
}
